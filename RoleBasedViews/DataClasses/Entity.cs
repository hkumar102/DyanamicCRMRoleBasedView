
namespace RoleBasedViews.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// CrmEntity class.
    /// </summary>
    public class CrmEntity
    {
        /// <summary>
        /// Gets or sets the Logical Name for the entity.
        /// </summary>
        public string EntityLogicalName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Display Name of the entity.
        /// </summary>
        public string EntityDisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the object type code
        /// </summary>
        public int? ObjectTypeCode
        {
            get;
            set;
        }
    }
}
