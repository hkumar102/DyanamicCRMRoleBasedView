
namespace RoleBasedViews.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    internal class PluginHelper
    {
        /// <summary>
        /// Gets the roleviewconfiguration for the given entity.
        /// </summary>
        /// <param name="crmService">The IOrganizationServiceInstance</param>
        /// <param name="roles">The security role name array.</param>
        /// <param name="objectTypeCode">the objecttypecode for the entity.</param>
        /// <returns>List of Entity object.</returns>
        internal static List<Entity> GetRoleViewConfigurationForEntity(IOrganizationService crmService, string[] roles, int objectTypeCode)
        {
            var query = new QueryExpression("rb_roleviewconfiguration");
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition(new ConditionExpression("rb_objecttypecode", ConditionOperator.Equal, objectTypeCode));
            query.Criteria.AddCondition(new ConditionExpression("rb_rolename", ConditionOperator.In, roles));

            var entityCollection = crmService.RetrieveMultiple(query);

            return (entityCollection.Entities.Count > default(int) ? entityCollection.Entities.ToList() : null);
        }

        /// <summary>
        /// Get the userroleviewconfiguration for the entity.
        /// </summary>
        /// <param name="crmService"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static Entity GetUserRoleViewConfigurationEntity(IOrganizationService crmService, string userId)
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

        /// <summary>
        /// Gets the security roles for the logged in user.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static string[] GetUserSecurityRoles(IOrganizationService service, string userId)
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                              <entity name='systemuser'>
                                <attribute name='fullname' />
                                <attribute name='systemuserid' />
                                <filter type='and'>
                                  <condition attribute='isdisabled' operator='eq' value='0' />
                                  <condition attribute='systemuserid' operator='eq' value='" + userId + @"'  />
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
            var entityCollection = service.RetrieveMultiple(fetchExpression);
            var userRoles = new List<string>();

            foreach (var entity in entityCollection.Entities)
            {
                // get the role from security role assignment
                if (entity.Attributes.ContainsKey("ur.name") && entity.Attributes.ContainsKey("ur.roleid"))
                {
                    var userRoleNameAlias = entity.GetAttributeValue<AliasedValue>("ur.name");
                    var userRoleIdAlias = entity.GetAttributeValue<AliasedValue>("ur.roleid");

                    userRoles.Add(userRoleNameAlias.Value.ToString());
                }

                // get the role from team role assignment
                if (entity.Attributes.ContainsKey("tr.name") && entity.Attributes.ContainsKey("tr.roleid"))
                {
                    var teamRoleNameAlias = entity.GetAttributeValue<AliasedValue>("tr.name");
                    var teamRoleIdAlias = entity.GetAttributeValue<AliasedValue>("tr.roleid");

                    userRoles.Add(teamRoleNameAlias.Value.ToString());
                }
            }

            return userRoles.ToArray();
        }

        internal static void CreateTestRow(string str, IOrganizationService service)
        {
            var entity = new Entity("new_test");
            entity.Attributes["new_name"] = str;

            service.Create(entity);
        }
    }
}
