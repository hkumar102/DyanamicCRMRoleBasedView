
namespace RoleBasedViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using DataClasses;
    using Helper;
    using Microsoft.Xrm.Sdk;

    /// <summary>
    /// RoleBasedViewForm class.
    /// </summary>
    public partial class RoleBasedViewForm : Form
    {
        #region Page Variables

        /// <summary>
        /// 
        /// </summary>
        CrmConnector _crmConnector;

        /// <summary>
        /// Delegate for the function to retrieve security roles.
        /// </summary>
        /// <param name="service">Instance of type IOrganizationService.</param>
        public delegate List<Role> LoadSecurityRoleDelegate(IOrganizationService service);

        /// <summary>
        /// Delegate for the function to retrieve the customizable entities in the system.
        /// </summary>
        /// <param name="service">Instance of type IOrganizationService.</param>
        public delegate List<CrmEntity> LoadEntitiesDelegate(IOrganizationService service);

        /// <summary>
        /// Delegate for the function to retrieve the Views for entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <returns></returns>
        public delegate List<RoleBasedViews.DataClasses.View> LoadViewDelegate(IOrganizationService service, string entityLogicalName);

        /// <summary>
        /// delegate to load the data from roleviewconfiguration entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="role"></param>
        /// <param name="entityLogicalName"></param>
        /// <returns></returns>
        public delegate Entity LoadRoleViewConfigurationDelegate(IOrganizationService service, string role, string entityLogicalName);

        /// <summary>
        /// Delegate for the SaveRoleViewConfiguration Method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="role"></param>
        /// <param name="defaultView"></param>
        /// <param name="hiddenViews"></param>
        public delegate void SaveRoleViewConfigurationDelegate(IOrganizationService service, string entityLogicalName, string role, string defaultView, string hiddenViews);

        /// <summary>
        /// Delegate for the GetUserRoleConfigurations method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public delegate List<Entity> LoadUserRoleConfigurations(IOrganizationService service, List<string> userIds);

        /// <summary>
        /// Delegate for the SaveUserRoleConfiguration method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="usersConfigurations"></param>
        public delegate void SaveUserRoleConfigurationDelegate(IOrganizationService service, List<Entity> usersConfigurations);

        /// <summary>
        /// Delegate for the GetUsersAndSecurityRoles function.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public delegate List<DataClasses.Users> GetUsersSecurityRolesDelegate(IOrganizationService service);

        #endregion

        public RoleBasedViewForm()
        {
            InitializeComponent();

            // Load the Connection Control.
            this.LoadConnectionControl();

            // set the properties.
            this.EntityGridView.AutoGenerateColumns = this.ViewsGridView.AutoGenerateColumns = this.UserConfigurationGridView.AutoGenerateColumns = false;
        }


        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void LoadConnectionControl()
        {
            _crmConnector = new CrmConnector(this);
            _crmConnector.LoadConnector();
        }

        /// <summary>
        /// Method to load the security roles.
        /// </summary>
        private void LoadSecurityRoles()
        {
            this.ShowProgress("Loading security Roles...");

            // unbind the event Handler.
            this.SecurityRoleCombobox.SelectedIndexChanged -= this.SecurityRoleCombobox_SelectedIndexChanged;

            this.SecurityRoleCombobox.DataSource = null;

            LoadSecurityRoleDelegate del = CrmHelper.GetRoles;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, null, null);
            var roles = del.EndInvoke(result);

            this.SecurityRoleCombobox.ValueMember = "RoleId";
            this.SecurityRoleCombobox.DisplayMember = "RoleName";

            this.SecurityRoleCombobox.DataSource = roles;
            this.SecurityRoleCombobox.SelectedItem = null;

            // rebind the event handler
            this.SecurityRoleCombobox.SelectedIndexChanged += this.SecurityRoleCombobox_SelectedIndexChanged;

            this.HideProgress();

            this.SecurityRoleCombobox.Enabled = roles.Count > 0;
        }

        /// <summary>
        /// Method to load the entities.
        /// </summary>
        private void LoadEntities()
        {
            this.ShowProgress("Loading Entities...");

            this.EntityGridView.DataSource = null;

            LoadEntitiesDelegate del = CrmHelper.GetCrmEntities;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, null, null);
            var entities = del.EndInvoke(result);

            // set the datasource
            this.EntityGridView.DataSource = entities;

            this.HideProgress();

            this.EntityGridView.Enabled = entities.Count > 0;
        }

        /// <summary>
        /// Method to show progress bar.
        /// </summary>
        /// <param name="progressText"></param>
        private void ShowProgress(string progressText)
        {
            this.Cursor = Cursors.WaitCursor;
            this.ProgressPanel.Visible = true;
            this.ProgressLabel.Text = progressText;
        }

        /// <summary>
        /// Method to hide the progress icon.
        /// </summary>
        private void HideProgress()
        {
            this.Cursor = Cursors.Default;
            this.ProgressPanel.Visible = false;
        }

        /// <summary>
        /// Method to set the default view.
        /// </summary>
        /// <param name="refreshRequired"></param>
        /// <param name="views"></param>
        private void SetDefaultView(bool refreshRequired, List<DataClasses.View> views)
        {
            var selectedEntityCell = this.EntityGridView.SelectedCells.Count > 0 ? this.EntityGridView.SelectedCells[0] : null;
            if (selectedEntityCell == null)
                return;

            var entityLogicalName = this.EntityGridView.Rows[selectedEntityCell.RowIndex].Cells[selectedEntityCell.ColumnIndex + 1].Value.ToString();

            if (refreshRequired)
            {
                views = CrmHelper.GetViewsForEntity(_crmConnector.Service, entityLogicalName);
            }

            // set the datasource.
            this.ViewsGridView.DataSource = null;
            this.ViewsGridView.DataSource = views;
            this.ViewsGridView.Enabled = views.Count > 0;

            var role = this.SecurityRoleCombobox.SelectedItem as Role;
            if (role == null)
            {
                return;
            }
            this.ShowProgress(string.Format("Loading Role View Configurations."));

            LoadRoleViewConfigurationDelegate del = CrmHelper.GetViewConfigurationsForRoleAndEntity;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, role.RoleName, entityLogicalName, null, null);
            var configEntity = del.EndInvoke(result);

            this.HideProgress();

            // get the row in the View GridView which has the view.
            if (configEntity != default(Entity))
            {
                var defaultView = configEntity.GetAttributeValue<string>("rb_viewname");
                var hiddenViews = configEntity.GetAttributeValue<string>("rb_hiddenviews");
                var hiddenViewsArray = string.IsNullOrEmpty(hiddenViews) ? new string[] { "" } : hiddenViews.Split('@');

                // update the properties.
                var selectedDefaultView = views.SingleOrDefault(v => v.ViewDisplayName.Equals(defaultView, StringComparison.OrdinalIgnoreCase));

                if (selectedDefaultView != null)
                {
                    selectedDefaultView.IsDefaultView = true;
                }

                // set the hidden view(s)
                foreach(var hiddenView in hiddenViewsArray)
                {
                    var findView = views.SingleOrDefault(v => v.ViewDisplayName.Equals(hiddenView, StringComparison.OrdinalIgnoreCase));
                    if (findView != null)
                    {
                        findView.IsVisible = false;
                    }
                }

                this.ViewsGridView.DataSource = null;
                this.ViewsGridView.DataSource = views;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event Handler for the Security Role Combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecurityRoleCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetDefaultView(true, null);
        }

        /// <summary>
        /// Event handler when the Load Metadata image is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMetadataImage_Click(object sender, EventArgs e)
        {
            if (_crmConnector.Service == null)
            {
                MessageBox.Show("Please connect to CRM instance.", "Message!");
                return;
            }

            this.EntityGridView.Enabled = this.SecurityRoleCombobox.Enabled = true;

            this.LoadSecurityRoles();
            this.LoadEntities();
        }

        /// <summary>
        /// Event handler when the EntityGridView cell is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (sender as DataGridView);

            if (dataGridView != null)
            {
                var clickedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (clickedCell != null)
                {
                    var entityLogicalName = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();

                    this.ShowProgress(string.Format("Loading views for entity : {0}", clickedCell.Value.ToString()));
                    this.ViewsGridView.DataSource = null;

                    LoadViewDelegate del = CrmHelper.GetViewsForEntity;
                    IAsyncResult result = del.BeginInvoke(_crmConnector.Service, entityLogicalName, null, null);
                    var views = del.EndInvoke(result);
                    
                    // set the default view
                    this.SetDefaultView(false, views);

                    this.HideProgress();
                }
            }
        }

        /// <summary>
        /// Event handler for the Save Changes Image Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChangesImage_Click(object sender, EventArgs e)
        {
            if (_crmConnector.Service == null)
            {
                MessageBox.Show("Please connect to CRM instance.", "Message!");
                return;
            }

            var role = this.SecurityRoleCombobox.SelectedItem as Role;
            if (role == null)
            {
                MessageBox.Show("Please select security role.", "Message!");
                return;
            }

            var selectedEntityCell = this.EntityGridView.SelectedCells.Count > 0 ? this.EntityGridView.SelectedCells[0] : null;
            if (selectedEntityCell == null)
            {
                MessageBox.Show("Please select an Entity.", "Message!");
            }

            var selectedRole = role.RoleName;
            var selectedEntity = this.EntityGridView.Rows[selectedEntityCell.RowIndex].Cells[selectedEntityCell.ColumnIndex + 1].Value.ToString();

            // get the values selected by the user.
            var defaultView = default(string);
            var hiddenViews = default(string);
            int defaultViewCount = default(int);
            int hiddenViewCount = default(int);

            for (var count = 0; count < this.ViewsGridView.Rows.Count; count++)
            {
                var gridRow = this.ViewsGridView.Rows[count];
                var defaultViewCell = (DataGridViewCheckBoxCell)gridRow.Cells[2];
                var visibleCell = (DataGridViewCheckBoxCell)gridRow.Cells[3];
                var defaultViewCellValue = defaultViewCell.IsInEditMode ? Convert.ToBoolean(defaultViewCell.EditedFormattedValue) : Convert.ToBoolean(defaultViewCell.Value);
                var visibleCellValue = visibleCell.IsInEditMode ? Convert.ToBoolean(visibleCell.EditedFormattedValue) : Convert.ToBoolean(visibleCell.Value);

                if (defaultViewCellValue && !visibleCellValue)
                {
                    MessageBox.Show("Default view selected for the entity and role combination cannot be set to hidden.", "Message!");
                    return;
                }
                if (defaultViewCellValue)
                {
                    defaultViewCount++;
                    defaultView = gridRow.Cells[0].Value.ToString();
                }

                if (!visibleCellValue)
                {
                    hiddenViews += string.Format("{0}@", gridRow.Cells[0].Value.ToString());
                    hiddenViewCount++;
                }
            }

            if (hiddenViews != default(string) && hiddenViews.EndsWith("@"))
            {
                hiddenViews = hiddenViews.TrimEnd('@');
            }

            if (defaultViewCount > 1)
            {
                MessageBox.Show("There can be only one default view for a selected role and Entity combination", "Message!");
                return;
            }

            if (hiddenViewCount == this.ViewsGridView.Rows.Count)
            {
                MessageBox.Show("At least one view should be visible for a role and Entity combination", "Message!");
                return;
            }

            this.ShowProgress("Saving configurations. Please wait...");

            SaveRoleViewConfigurationDelegate del = CrmHelper.SaveRoleViewConfiguration;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, selectedEntity, selectedRole, defaultView, hiddenViews, null, null);
            del.EndInvoke(result);

            this.HideProgress();
        }

        /// <summary>
        /// Method to Load the User Data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadUserData_Click(object sender, EventArgs e)
        {
            if (_crmConnector.Service == null)
            {
                MessageBox.Show("Please connect to CRM instance.", "Message!");
                return;
            }

            this.ShowProgress("Loading Users and their security roles...");
            GetUsersSecurityRolesDelegate del = CrmHelper.GetUsersAndSecurityRoles;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, null, null);
            var usersList = del.EndInvoke(result);
            
            this.ShowProgress("Loading View Configurations for User(s)...");
            LoadUserRoleConfigurations delUserRoles = CrmHelper.GetUserRoleConfigEntities;
            result = delUserRoles.BeginInvoke(_crmConnector.Service, usersList.Select(u => u.UserId.ToString()).ToList(), null, null);
            var userRoleConfigEntities = delUserRoles.EndInvoke(result);
            this.HideProgress();

            // set the datasource of the GridView.
            this.UserConfigurationGridView.DataSource = null;
            this.UserConfigurationGridView.DataSource = usersList;
            this.UserConfigurationGridView.Enabled = usersList.Count > default(int);

            // filter the roles list
            for (var rowCount = 0; rowCount < this.UserConfigurationGridView.Rows.Count; rowCount++)
            {
                var gridRow = this.UserConfigurationGridView.Rows[rowCount];
                var systemUserId = Guid.Parse(gridRow.Cells[0].Value.ToString());
                var userRoleConfig = userRoleConfigEntities.SingleOrDefault(u => u.GetAttributeValue<string>("rb_userid").Trim('{','}').Equals(systemUserId.ToString().Trim('}','{'), StringComparison.OrdinalIgnoreCase));

                var userRoles = usersList.Single(u => u.UserId.Equals(systemUserId)).Roles.Select(r => r.RoleName).Distinct().ToList();
                userRoles.Insert(0, string.Empty);
              
                var combobBoxCell = (DataGridViewComboBoxCell)gridRow.Cells[2];
                combobBoxCell.DataSource = userRoles;

                var checkboxCell = (DataGridViewCheckBoxCell)gridRow.Cells[3];
                var isDirtyCell = (DataGridViewCheckBoxCell)gridRow.Cells[4];

                if (userRoleConfig != null)
                {
                    var roleName = userRoleConfig.GetAttributeValue<String>("rb_rolename");
                    var userId = userRoleConfig.GetAttributeValue<string>("rb_userid");
                    var isEnabled = userRoleConfig.GetAttributeValue<bool>("rb_isenabled");
                    var userRole = userRoles.SingleOrDefault(r => r.Equals(roleName, StringComparison.OrdinalIgnoreCase));
                    var user = usersList.Single(u => u.UserId.Equals(systemUserId));

                    combobBoxCell.Value = userRole;
                    checkboxCell.Value = isEnabled;
                    isDirtyCell.Value = true;
                }
            }
        }

        /// <summary>
        /// Event handler for the Save User Configuration button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveUserConfiguration_Click(object sender, EventArgs e)
        {
            if (_crmConnector.Service == null)
            {
                MessageBox.Show("Please connect to CRM instance.", "Message!");
                return;
            }

            var entityUpdateCollection = new List<Entity>();

            for (var count = 0; count < this.UserConfigurationGridView.Rows.Count; count++)
            {
                var gridRow = this.UserConfigurationGridView.Rows[count];
                var userId = gridRow.Cells[0].Value.ToString();
                                
                var roleName = default(string);
                var rolesComboboxCell = (DataGridViewComboBoxCell)gridRow.Cells[2];
                var selectedRole = (rolesComboboxCell.IsInEditMode ? rolesComboboxCell.EditedFormattedValue : rolesComboboxCell.Value);
                roleName = selectedRole == null ? string.Empty : selectedRole.ToString();

                var checkBoxCell = (DataGridViewCheckBoxCell)gridRow.Cells[3];
                var isEnabled = checkBoxCell.IsInEditMode ? Convert.ToBoolean(checkBoxCell.EditedFormattedValue) : Convert.ToBoolean(checkBoxCell.Value);

                var isDirtyCheckboxCell = (DataGridViewCheckBoxCell)gridRow.Cells[4];
                var isDirty = Convert.ToBoolean(isDirtyCheckboxCell.Value);

                var entity = new Entity("rb_userroleviewconfiguration");
                entity.Attributes["rb_userid"] = userId;
                entity.Attributes["rb_isenabled"] = isEnabled;
                entity.Attributes["rb_rolename"] = roleName;
                entity.Attributes["rb_isdirty"] = isDirty;

                entityUpdateCollection.Add(entity);
            }

            this.ShowProgress("Saving View Configuration for User(s)...");
            SaveUserRoleConfigurationDelegate del = CrmHelper.UpdateUserConfigurationData;
            IAsyncResult result = del.BeginInvoke(_crmConnector.Service, entityUpdateCollection, null, null);
            del.EndInvoke(result);
            this.HideProgress();

            // reload the users grid
            this.LoadUserData_Click(this, new EventArgs());
        }

        #endregion        
    }

}
