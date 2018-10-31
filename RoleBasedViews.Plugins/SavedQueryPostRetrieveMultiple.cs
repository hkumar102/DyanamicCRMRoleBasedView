
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

    public class SavedQueryPostRetrieveMultiple : IPlugin
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
                && pluginContext.InputParameters["Query"] is QueryExpression
                && pluginContext.SharedVariables.ContainsKey("DefaultViewName"))
            {
                tracingService.Trace("Step 0");
                string defaultView = (string)pluginContext.SharedVariables["DefaultViewName"];
                var entityCollection = (EntityCollection)pluginContext.OutputParameters["BusinessEntityCollection"];
                tracingService.Trace(string.Format("Step 1 :- {0} ------ {1}", entityCollection.Entities.Count, defaultView));
                entityCollection = this.ChangeBusinessEntityCollection(entityCollection, defaultView, pluginContext, tracingService);
                tracingService.Trace("Step 2");

                pluginContext.OutputParameters["BusinessEntityCollection"] = entityCollection;
                tracingService.Trace("Step 3");

            }
        }

        /// <summary>
        /// Change the default view to the one set for the user.
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <param name="defaultViewName"></param>
        /// <param name="pluginContext"></param>
        /// <returns></returns>
        private EntityCollection ChangeBusinessEntityCollection(EntityCollection entityCollection, string defaultViewName, IPluginExecutionContext pluginContext, ITracingService tracingService)
        {
            foreach (var entity in entityCollection.Entities)
            {
                if (entity.Attributes.Contains("name"))
                {

                    if (entity.GetAttributeValue<string>("name").Equals(defaultViewName))
                    {
                        entity["isdefault"] = true;
                    }
                    else
                    {
                        entity["isdefault"] = false;
                    }
                }

            }

            return entityCollection;
        }
    }
}
