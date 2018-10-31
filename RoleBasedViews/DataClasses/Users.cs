
namespace RoleBasedViews.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Users
    {
        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the FullName of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the roles for the user.
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets a value to indicate if the userconfiguration is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
