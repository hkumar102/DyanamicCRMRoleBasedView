
namespace RoleBasedViews.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// CrmAttribute class.
    /// </summary>
    public class View
    {
        /// <summary>
        /// Gets or sets the Display Name of the View.
        /// </summary>
        public string ViewDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the Logical name of the entity to which the view belongs.
        /// </summary>
        public string ReturnedTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the Guid of the view.
        /// </summary>
        public Guid ViewId { get; set; }

        /// <summary>
        /// Gets or sets an indicator to indicate if it is a default view or not
        /// </summary>
        public bool IsDefaultView { get; set; }

        /// <summary>
        /// Gets or sets a value to indicate if this view would be visible for a particular role.
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
