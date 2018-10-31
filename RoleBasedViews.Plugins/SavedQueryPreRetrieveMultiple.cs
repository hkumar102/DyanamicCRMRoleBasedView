
namespace RoleBasedViews.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    using Microsoft.Xrm.Sdk;
    using Microsoft.Crm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    public class SavedQueryPreRetrieveMultiple : IPlugin
    {
        /// <summary>
        /// The Execute method.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void Execute(IServiceProvider serviceProvider)
        {
            var organizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var orgService = organizationServiceFactory.CreateOrganizationService(pluginContext.UserId);

            if (pluginContext.PrimaryEntityName.Equals("savedquery") 
                && pluginContext.InputParameters.ContainsKey("Query")
                && pluginContext.InputParameters["Query"] is QueryExpression)
            {
                var query = (QueryExpression)pluginContext.InputParameters["Query"];

                // get the returnedtypecode condition
                var returnedTypeCodeCondition = query.Criteria.Conditions.SingleOrDefault(c => c.AttributeName.Equals("returnedtypecode"));
                if (returnedTypeCodeCondition != null)
                {
                    if (returnedTypeCodeCondition.Values[0].GetType() != typeof(int))
                        return;

                    var objectTypeCode = (int)returnedTypeCodeCondition.Values[0];

                    try
                    {
                        // get the user configuration for role based views
                        var userConfiguration = PluginHelper.GetUserRoleViewConfigurationEntity(orgService, pluginContext.InitiatingUserId.ToString());
                        var isEnabled = true;
                        if (userConfiguration != null)
                        {
                            isEnabled = userConfiguration.GetAttributeValue<bool>("rb_isenabled");
                        }
                        
                        if (!isEnabled)
                        {
                            return;
                        }
                        
                        // get user security roles
                        var userRoles = PluginHelper.GetUserSecurityRoles(orgService, pluginContext.InitiatingUserId.ToString());
                        // check to see whether the view configuration for the user has been done.
                        
                        var viewConfigurations = PluginHelper.GetRoleViewConfigurationForEntity(orgService, userRoles, objectTypeCode);

                        if (viewConfigurations != null)
                        {
                            var hiddenViewsList = new List<string>();

                            foreach (var viewConfig in viewConfigurations)
                            {
                                var hiddenViewField = viewConfig.Attributes.ContainsKey("rb_hiddenviews") ? (string)viewConfig.Attributes["rb_hiddenviews"] : null;

                                if (hiddenViewField != null)
                                {
                                    hiddenViewsList.AddRange(hiddenViewField.Split('@'));
                                }
                            }

                            var results = from h in hiddenViewsList
                                              group h by h into g
                                              select new {Role = g.Key, RoleCount = g.ToList()};
                            hiddenViewsList = results.Where(r => r.RoleCount.Count == userRoles.Length).Select(r => r.Role).ToList();

                            if (hiddenViewsList.Count > default(int))
                            {
                                // set the condition for hiding the views
                                var hideViewCondition = new ConditionExpression("name", ConditionOperator.NotIn, hiddenViewsList.Distinct().ToArray());
                                query.Criteria.AddCondition(hideViewCondition);
                            }

                            if (userRoles.Length > 1)
                            {
                                if (userConfiguration != null)
                                {                                    
                                    var userRole = userConfiguration.GetAttributeValue<string>("rb_rolename");

                                    // get the default view for the role
                                    var userRoleConfig = viewConfigurations.SingleOrDefault(v => v.GetAttributeValue<string>("rb_rolename").Equals(userRole, StringComparison.OrdinalIgnoreCase));

                                    if (userRoleConfig != null)
                                    {                                        
                                        var defaultViewName = (string)userRoleConfig["rb_viewname"];

                                        if (!string.IsNullOrEmpty(defaultViewName))
                                        {
                                            pluginContext.SharedVariables.Add("DefaultViewName", defaultViewName);
                                        }
                                    }
                                }
                            }
                            else if(userRoles.Length == 1)
                            {
                                var defaultViewName = (string)viewConfigurations[0]["rb_viewname"];

                                if (defaultViewName != null)
                                {
                                    pluginContext.SharedVariables.Add("DefaultViewName", defaultViewName);
                                }
                            }
                        }
                    }
                    catch (FaultException<OrganizationServiceFault> ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
