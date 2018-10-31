
namespace RoleBasedViews.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataClasses;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Messages;
    using Microsoft.Xrm.Sdk.Metadata;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// CrmHelper class.
    /// </summary>
    internal class CrmHelper
    {
        #region variables

        private static string[] _restrictedRoles = new string[] { "System Administrator", "System Customizer" };
        //private static string[] _restrictedRoles = new string[] {"System Customizer" };

        #endregion

        /// <summary>
        /// Gets or sets the list of all custom entities.
        /// </summary>
        /// <param name="service">The IOrganization Service.</param>
        /// <returns>List of type CrmEntity.</returns>
        internal static List<CrmEntity> GetCrmEntities(IOrganizationService service)
        {
            var entityList = new List<CrmEntity>();

            RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };

            // Retrieve the MetaData.
            RetrieveAllEntitiesResponse response = (RetrieveAllEntitiesResponse)service.Execute(request);

            foreach (var entity in response.EntityMetadata)
            {
                if (entity.IsCustomizable.Value && entity.CanCreateViews.Value)
                {
                    var entityObj = new DataClasses.CrmEntity
                    {
                        EntityDisplayName = entity.DisplayName.LocalizedLabels[0].Label,
                        EntityLogicalName = entity.LogicalName,
                        ObjectTypeCode = entity.ObjectTypeCode
                    };

                    entityList.Add(entityObj);
                }
            }

            return entityList.OrderBy(o => o.EntityDisplayName).ToList();
        }


        /// <summary>
        /// Method to publish the entity changes.
        /// </summary>
        /// <param name="entityLogicalName"></param>
        /// <param name="service"></param>
        internal static void PublishEntity(string entityLogicalName, IOrganizationService service)
        {
            PublishXmlRequest publishRequest = new PublishXmlRequest();
            publishRequest.ParameterXml = "<importexportxml>" +
            "    <entities>" +
            "        <entity>" + entityLogicalName + "</entity>" +
            "    </entities>" +
            "    <nodes/>" +
            "    <securityroles/>  " +
            "    <settings/>" +
            "    <workflows/>" +
            "</importexportxml>";

            service.Execute(publishRequest);
        }

        /// <summary>
        /// Gets the List of roles with distinct name
        /// </summary>
        /// <param name="service">Instance of IOrganizationService</param>
        /// <returns>List of type Role.</returns>
        internal static List<Role> GetRoles(IOrganizationService service)
        {
            var roleList = new List<Role>();

            var queryExpression = new QueryExpression("role");

            queryExpression.ColumnSet.AddColumns("name", "roleid");
            queryExpression.Criteria = new FilterExpression();

            EntityCollection response = service.RetrieveMultiple(queryExpression);

            foreach (var entity in response.Entities)
            {
                var roleName = (string)entity.Attributes["name"];

                if (!roleList.Any(r => r.RoleName == roleName) && !_restrictedRoles.Contains(roleName))
                {
                    roleList.Add(new Role
                    {
                        RoleName = (string)entity.Attributes["name"],
                        RoleId = entity.Attributes["roleid"].ToString()
                    });
                }
            }

            return roleList.OrderBy(r => r.RoleName).ToList();
        }

        public static Entity GetViewConfigurationsForRoleAndEntity(IOrganizationService service, string role, string entitylogicalname)
        {
            var configuration = default(Entity);

            var query = new QueryExpression("rb_roleviewconfiguration");

            query.ColumnSet.AddColumns("rb_rolename", "rb_viewname", "rb_entitylogicalname", "rb_hiddenviews");

            query.Criteria.AddCondition(new ConditionExpression("rb_rolename", ConditionOperator.Equal, role));
            query.Criteria.AddCondition(new ConditionExpression("rb_entitylogicalname", ConditionOperator.Equal, entitylogicalname));

            var entityCollection = service.RetrieveMultiple(query);

            if (entityCollection.Entities.Count > default(int))
            {
                configuration = entityCollection.Entities[0];
            }

            return configuration;
        }

        public static List<Entity> GetUserRoleConfigEntities(IOrganizationService service, List<string> userIds)
        {
            var userRoleConfigurations = new List<Entity>();
            var userIdConditionBuilder = new System.Text.StringBuilder();

            foreach (var userId in userIds)
            {
                userIdConditionBuilder.Append(string.Format("<value>{0}</value>", userId));
            }

            var fetchXml = string.Format(@"<fetch version='1.0' outputformat='xmlplatform' mapping='logical' distinct='false'>
                             <entity name='rb_userroleviewconfiguration'>
                              <attribute name='rb_userroleviewconfigurationid' /> 
                              <attribute name='rb_name' /> 
                              <attribute name='rb_userid' />
                              <attribute name='rb_isenabled' />
                              <attribute name='rb_rolename' />
                              <attribute name='createdon' /> 
                              <order attribute='rb_name' descending='false' /> 
                             <filter type='and'>
                              <condition attribute='rb_userid' operator='in'>
                                {0}
                              </condition> 
                              </filter>
                              </entity>
                              </fetch>", userIdConditionBuilder.ToString());

            var fetchExpression = new FetchExpression(fetchXml);
            var entityCollection = service.RetrieveMultiple(fetchExpression);

            if (entityCollection.Entities.Count > default(int))
            {
                foreach (var entity in entityCollection.Entities)
                {
                    userRoleConfigurations.Add(entity);
                }
            }

            return userRoleConfigurations;
        }

        /// <summary>
        /// Gets all the views for the entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <returns></returns>
        public static List<View> GetViewsForEntity(IOrganizationService service, string entityLogicalName)
        {
            var viewList = new List<View>();

            var queryExpression = new QueryExpression("savedquery");

            queryExpression.ColumnSet.AddColumns("name", "description", "returnedtypecode", "savedqueryid");
            queryExpression.Criteria.Conditions.Add(new ConditionExpression("returnedtypecode", ConditionOperator.Equal, entityLogicalName));
            //queryExpression.Criteria.Conditions.Add(new ConditionExpression("querytype", ConditionOperator.Equal, 0));

            var views = service.RetrieveMultiple(queryExpression);

            foreach (var view in views.Entities)
            {
                viewList.Add(
                    new View
                    {
                        ReturnedTypeCode = view.GetAttributeValue<string>("returnedtypecode"),
                        ViewDisplayName = view.GetAttributeValue<string>("name"),
                        ViewId = view.GetAttributeValue<Guid>("savedqueryid"),
                        IsDefaultView = false,
                        IsVisible = true
                    });
            }

            return viewList;
        }

        /// <summary>
        /// Saves role view configuration.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="role"></param>
        /// <param name="defaultView"></param>
        /// <param name="hiddenViews"></param>
        public static void SaveRoleViewConfiguration(IOrganizationService service, string entityLogicalName, string role, string defaultView, string hiddenViews)
        {
            var roleViewConfigEntity = GetViewConfigurationsForRoleAndEntity(service, role, entityLogicalName);

            var entity = new Entity("rb_roleviewconfiguration");

            if (roleViewConfigEntity != null)
            {
                roleViewConfigEntity.Attributes["rb_viewname"] = defaultView;
                roleViewConfigEntity.Attributes["rb_hiddenviews"] = hiddenViews;
                service.Update(roleViewConfigEntity);
            }
            else
            {
                entity.Attributes["rb_rolename"] = role;
                entity.Attributes["rb_entitylogicalname"] = entityLogicalName;
                entity.Attributes["rb_viewname"] = defaultView;
                entity.Attributes["rb_hiddenviews"] = hiddenViews;
                entity.Attributes["rb_objecttypecode"] = GetObjectTypeCodeForEntity(service, entityLogicalName);
                service.Create(entity);
            }
        }
         
        /// <summary>
        /// Gets the objecttypecode for an entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <returns></returns>
        internal static int GetObjectTypeCodeForEntity(IOrganizationService service, string entityLogicalName)
        {
            RetrieveEntityRequest request = new RetrieveEntityRequest
            {
                LogicalName = entityLogicalName,
                EntityFilters = EntityFilters.Entity
            };

            var response = (RetrieveEntityResponse)service.Execute(request);

            return response.EntityMetadata.ObjectTypeCode.Value;
        }

        public static List<DataClasses.Users> GetUsersAndSecurityRoles(IOrganizationService service)
        {
            var usersList = new List<Users>();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                              <entity name='systemuser'>
                                <attribute name='fullname' />
                                <attribute name='systemuserid' />
                                <filter type='and'>
                                  <condition attribute='isdisabled' operator='eq' value='0' />
                                </filter>
                                <order attribute='fullname' descending='false' />
                                <link-entity name='systemuserroles' from='systemuserid' to='systemuserid' visible='false' link-type='outer'>
                                  <link-entity name='role' from='roleid' to='roleid' alias='ur' link-type='outer'>
                                    <attribute name='roleid' />
                                    <attribute name='name' />
                                  </link-entity>
                                </link-entity>
                                <link-entity name='teammembership' from='systemuserid' to='systemuserid' visible='false' link-type='outer'>
                                  <link-entity name='team' from='teamid' to='teamid' alias='ut'>
                                    <link-entity name='teamroles' from='teamid' to='teamid' visible='false' link-type='outer'>
                                      <link-entity name='role' from='roleid' to='roleid' alias='tr' link-type='outer'>
                                        <attribute name='roleid' />
                                        <attribute name='name' />
                                      </link-entity>
                                    </link-entity>
                                  </link-entity>
                                </link-entity>
                              </entity>
                            </fetch>";

            var fetchExpression = new FetchExpression(fetchXml);
            var results = service.RetrieveMultiple(fetchExpression);

            foreach (var entity in results.Entities)
            {
                var user = default(Users);

                var systemUserId = entity.GetAttributeValue<Guid>("systemuserid");

                if (usersList.Any(u => u.UserId.Equals(systemUserId)))
                {
                    user = usersList.Single(u => u.UserId.Equals(systemUserId));
                }
                else
                {
                    user = new Users
                    {
                        UserId = systemUserId,
                        IsEnabled = true,
                        Roles = new List<Role>()
                    };

                    usersList.Add(user);
                }

                user.UserName = entity.GetAttributeValue<string>("fullname");

                // get the role from security role assignment
                if (entity.Attributes.ContainsKey("ur.name") &&  entity.Attributes.ContainsKey("ur.roleid"))
                {
                    var userRoleNameAlias = entity.GetAttributeValue<AliasedValue>("ur.name");
                    var userRoleIdAlias = entity.GetAttributeValue<AliasedValue>("ur.roleid");

                    user.Roles.Add(new Role
                    {
                        RoleId = userRoleIdAlias.Value.ToString(),
                        RoleName = userRoleNameAlias.Value.ToString()
                    });
                }

                // get the role from team role assignment
                if (entity.Attributes.ContainsKey("tr.name") && entity.Attributes.ContainsKey("tr.roleid"))
                {
                    var teamRoleNameAlias = entity.GetAttributeValue<AliasedValue>("tr.name");
                    var teamRoleIdAlias = entity.GetAttributeValue<AliasedValue>("tr.roleid");

                    user.Roles.Add(new Role
                    {
                        RoleId = teamRoleIdAlias.Value.ToString(),
                        RoleName = teamRoleNameAlias.Value.ToString()
                    });
                }
            }

            return usersList;
        }

        /// <summary>
        /// Method to update the user configuration data.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userConfigEntities"></param>
        public static void UpdateUserConfigurationData(IOrganizationService service, List<Entity> userConfigEntities)
        {
            foreach (var userConfigEntity in userConfigEntities)
            {
                var userId = userConfigEntity.GetAttributeValue<string>("rb_userid");

                var existingUserConfig = default(Entity);

                if (userConfigEntity.GetAttributeValue<bool>("rb_isdirty"))
                {
                    // check if there is any entry for UserConfiguration for this user.
                    existingUserConfig = CheckIfUserConfigPresent(service, userId);
                }

                if (existingUserConfig != null)
                {
                    var existingIsEnabledValue = existingUserConfig.GetAttributeValue<bool>("rb_isenabled");
                    var existingRole = existingUserConfig.GetAttributeValue<string>("rb_rolename");

                    if (existingIsEnabledValue == userConfigEntity.GetAttributeValue<bool>("rb_isenabled")
                        && existingRole == userConfigEntity.GetAttributeValue<string>("rb_rolename"))
                    {
                        continue;
                    }

                    existingUserConfig["rb_rolename"] = userConfigEntity.GetAttributeValue<string>("rb_rolename");
                    existingUserConfig["rb_isenabled"] = userConfigEntity.GetAttributeValue<bool>("rb_isenabled");

                    service.Update(existingUserConfig);
                }
                else
                {
                    if(string.IsNullOrEmpty(userConfigEntity.GetAttributeValue<string>("rb_rolename")) 
                        && userConfigEntity.GetAttributeValue<bool>("rb_isenabled"))
                    {
                        continue;
                    }

                    if (userConfigEntity.Attributes.ContainsKey("rb_isdirty"))
                    {
                        userConfigEntity.Attributes.Remove("rb_isdirty");
                    }
                    service.Create(userConfigEntity);
                }
            }
        }

        /// <summary>
        /// method to check if there is already any configuration data for the user.
        /// </summary>
        /// <param name="crmService"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static Entity CheckIfUserConfigPresent(IOrganizationService crmService, string userId)
        {
            var query = new QueryExpression
            {
                EntityName = "rb_userroleviewconfiguration",
                ColumnSet = new ColumnSet("rb_userid", "rb_rolename", "rb_isenabled"),
                Criteria = new FilterExpression
                {
                    Conditions = { new ConditionExpression("rb_userid", ConditionOperator.Equal, userId) },
                    FilterOperator = LogicalOperator.And
                }
            };

            var entityCollection = crmService.RetrieveMultiple(query);

            if (entityCollection.Entities.Count > default(int))
            {
                return entityCollection.Entities[0];
            }

            return default(Entity);
        }
    }
}
