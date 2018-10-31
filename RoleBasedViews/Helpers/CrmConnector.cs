
namespace RoleBasedViews.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using McTools.Xrm.Connection;
    using McTools.Xrm.Connection.WinForms;
    using Microsoft.Xrm.Sdk;

    internal class CrmConnector
    {
        #region Page Variables

        private FormHelper formHelper;
        /// <summary>
        /// variable to hold the instance of the CrmConnectionStatusBar
        /// </summary>
        McTools.Xrm.Connection.WinForms.CrmConnectionStatusBar _crmConnectionStatusBar;

        /// <summary>
        /// variable to hold the instance of the type ConnectionManager.
        /// </summary>
        ConnectionManager _connectionManager;

        /// <summary>
        /// Instance of type FieldSecurityForm.
        /// </summary>
        RoleBasedViewForm _mainFormInstance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the instance of type IOrganizationService.
        /// </summary>
        internal IOrganizationService Service
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CrmConnector
        /// </summary>
        /// <param name="mainFormInstance">Instance of type CrmConnector.</param>
        internal CrmConnector(RoleBasedViewForm mainFormInstance)
        {
            this._mainFormInstance = mainFormInstance;
            this._connectionManager = ConnectionManager.Instance;

            // initializes the event handlers
            this._connectionManager.ConnectionSucceed += new ConnectionManager.ConnectionSucceedEventHandler(_connectionManager_ConnectionSucceed);
            this._connectionManager.ConnectionFailed += new ConnectionManager.ConnectionFailedEventHandler(_connectionManager_ConnectionFailed);
            this._connectionManager.StepChanged += new ConnectionManager.StepChangedEventHandler(_connectionManager_StepChanged);
        }

        #endregion

        #region internal methods

        /// <summary>
        /// Initiliazes the CrmConnectionStatusBar and adds it to the form.
        /// </summary>
        internal void LoadConnector()
        {
            // initialize the ConnectionStatusBar
            formHelper = new FormHelper(this._mainFormInstance);

            // Instantiate and add the connection control to the form
            _crmConnectionStatusBar = new McTools.Xrm.Connection.WinForms.CrmConnectionStatusBar(formHelper, true);
            //this._crmConnectionStatusBar = new CrmConnectionStatusBar(this._connectionManager);
            this._mainFormInstance.Controls.Add(this._crmConnectionStatusBar);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event Handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Instance of type StepChangedEventArgs</param>
        internal void _connectionManager_StepChanged(object sender, StepChangedEventArgs e)
        {
            this._crmConnectionStatusBar.SetMessage(e.CurrentStep);
        }

        /// <summary>
        /// Event Handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Object of type ConnectionFailedEventArgs</param>
        internal void _connectionManager_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            // Set error message
            this._crmConnectionStatusBar.SetMessage("Error: " + e.FailureReason);

            // Clear the current organization service
            this.Service = null;
        }

        /// <summary>
        /// Event Handler
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">ConnectionSucceedEventArgs instance.</param>
        internal void _connectionManager_ConnectionSucceed(object sender, ConnectionSucceedEventArgs e)
        {
            // Store connection Organization Service
            this.Service = e.OrganizationService;

            // Displays connection status
            this._crmConnectionStatusBar.SetConnectionStatus(true, e.ConnectionDetail);

            // Clear the current action message
            this._crmConnectionStatusBar.SetMessage(string.Empty);
        }

        #endregion

    }
}
