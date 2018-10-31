
namespace RoleBasedViews.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    /// <summary>
    /// Role class.
    /// </summary>
    public class Role : IEqualityComparer<Role>
    {
        /// <summary>
        /// Gets or sets the Guid of the Role
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Role
        /// </summary>
        public string RoleName { get; set; }

        public bool Equals(Role x, Role y)
        {
            if (x.RoleName.Equals(y.RoleName))
                return true;

            return false;
        }

        public int GetHashCode(Role obj)
        {
            return obj.RoleId.GetHashCode();
        }
    }
}
