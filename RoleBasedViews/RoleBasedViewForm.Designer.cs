namespace RoleBasedViews
{
    partial class RoleBasedViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleBasedViewForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveChangesImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LoadMetadataImage = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EntityGridView = new System.Windows.Forms.DataGridView();
            this.EntityDisplayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntityLogicalNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectTypeCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ProgressPanel = new System.Windows.Forms.Panel();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ViewsGridView = new System.Windows.Forms.DataGridView();
            this.RoleNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnedTypeCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultViewColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VisibleViewColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RoleIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.SecurityRoleCombobox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.SaveUserConfiguration = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LoadUserData = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserConfigurationGridView = new System.Windows.Forms.DataGridView();
            this.UserIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.EnableConfiguration = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDirty = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveChangesImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadMetadataImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EntityGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.ProgressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewsGridView)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveUserConfiguration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadUserData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserConfigurationGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.SaveChangesImage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LoadMetadataImage);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-4, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 51);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(914, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Save Changes";
            // 
            // SaveChangesImage
            // 
            this.SaveChangesImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveChangesImage.Image = ((System.Drawing.Image)(resources.GetObject("SaveChangesImage.Image")));
            this.SaveChangesImage.Location = new System.Drawing.Point(898, 29);
            this.SaveChangesImage.Name = "SaveChangesImage";
            this.SaveChangesImage.Size = new System.Drawing.Size(16, 16);
            this.SaveChangesImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SaveChangesImage.TabIndex = 3;
            this.SaveChangesImage.TabStop = false;
            this.SaveChangesImage.Click += new System.EventHandler(this.SaveChangesImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(819, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Load Data";
            // 
            // LoadMetadataImage
            // 
            this.LoadMetadataImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadMetadataImage.Image = ((System.Drawing.Image)(resources.GetObject("LoadMetadataImage.Image")));
            this.LoadMetadataImage.Location = new System.Drawing.Point(800, 28);
            this.LoadMetadataImage.Name = "LoadMetadataImage";
            this.LoadMetadataImage.Size = new System.Drawing.Size(16, 16);
            this.LoadMetadataImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LoadMetadataImage.TabIndex = 2;
            this.LoadMetadataImage.TabStop = false;
            this.LoadMetadataImage.Click += new System.EventHandler(this.LoadMetadataImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configure Views for Security Roles";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EntityGridView);
            this.groupBox2.Location = new System.Drawing.Point(15, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 208);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entities";
            // 
            // EntityGridView
            // 
            this.EntityGridView.BackgroundColor = System.Drawing.Color.White;
            this.EntityGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EntityGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EntityDisplayNameColumn,
            this.EntityLogicalNameColumn,
            this.ObjectTypeCodeColumn});
            this.EntityGridView.Enabled = false;
            this.EntityGridView.Location = new System.Drawing.Point(6, 21);
            this.EntityGridView.Name = "EntityGridView";
            this.EntityGridView.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EntityGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.EntityGridView.Size = new System.Drawing.Size(362, 178);
            this.EntityGridView.TabIndex = 3;
            this.EntityGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EntityGridView_CellClick);
            // 
            // EntityDisplayNameColumn
            // 
            this.EntityDisplayNameColumn.DataPropertyName = "EntityDisplayName";
            this.EntityDisplayNameColumn.HeaderText = "Entity";
            this.EntityDisplayNameColumn.Name = "EntityDisplayNameColumn";
            this.EntityDisplayNameColumn.ReadOnly = true;
            this.EntityDisplayNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EntityDisplayNameColumn.Width = 340;
            // 
            // EntityLogicalNameColumn
            // 
            this.EntityLogicalNameColumn.DataPropertyName = "EntityLogicalName";
            this.EntityLogicalNameColumn.HeaderText = "";
            this.EntityLogicalNameColumn.Name = "EntityLogicalNameColumn";
            this.EntityLogicalNameColumn.Visible = false;
            // 
            // ObjectTypeCodeColumn
            // 
            this.ObjectTypeCodeColumn.DataPropertyName = "ObjectTypeCode";
            this.ObjectTypeCodeColumn.HeaderText = "";
            this.ObjectTypeCodeColumn.Name = "ObjectTypeCodeColumn";
            this.ObjectTypeCodeColumn.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ProgressPanel);
            this.groupBox3.Controls.Add(this.ViewsGridView);
            this.groupBox3.Location = new System.Drawing.Point(395, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(625, 208);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Views";
            // 
            // ProgressPanel
            // 
            this.ProgressPanel.BackColor = System.Drawing.Color.White;
            this.ProgressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgressPanel.Controls.Add(this.ProgressLabel);
            this.ProgressPanel.Controls.Add(this.pictureBox2);
            this.ProgressPanel.Location = new System.Drawing.Point(0, 142);
            this.ProgressPanel.Name = "ProgressPanel";
            this.ProgressPanel.Size = new System.Drawing.Size(337, 62);
            this.ProgressPanel.TabIndex = 4;
            this.ProgressPanel.Visible = false;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(50, 23);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(0, 15);
            this.ProgressLabel.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // ViewsGridView
            // 
            this.ViewsGridView.BackgroundColor = System.Drawing.Color.White;
            this.ViewsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ViewsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RoleNameColumn,
            this.ReturnedTypeCodeColumn,
            this.DefaultViewColumn,
            this.VisibleViewColumn,
            this.RoleIdColumn});
            this.ViewsGridView.Enabled = false;
            this.ViewsGridView.Location = new System.Drawing.Point(7, 18);
            this.ViewsGridView.Name = "ViewsGridView";
            this.ViewsGridView.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ViewsGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.ViewsGridView.Size = new System.Drawing.Size(613, 181);
            this.ViewsGridView.TabIndex = 0;
            // 
            // RoleNameColumn
            // 
            this.RoleNameColumn.DataPropertyName = "ViewDisplayName";
            this.RoleNameColumn.HeaderText = "View Name";
            this.RoleNameColumn.Name = "RoleNameColumn";
            this.RoleNameColumn.ReadOnly = true;
            this.RoleNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleNameColumn.Width = 440;
            // 
            // ReturnedTypeCodeColumn
            // 
            this.ReturnedTypeCodeColumn.HeaderText = "ReturnedTypeCode";
            this.ReturnedTypeCodeColumn.Name = "ReturnedTypeCodeColumn";
            this.ReturnedTypeCodeColumn.Visible = false;
            this.ReturnedTypeCodeColumn.Width = 5;
            // 
            // DefaultViewColumn
            // 
            this.DefaultViewColumn.DataPropertyName = "IsDefaultView";
            this.DefaultViewColumn.HeaderText = "Default";
            this.DefaultViewColumn.Name = "DefaultViewColumn";
            this.DefaultViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultViewColumn.Width = 80;
            // 
            // VisibleViewColumn
            // 
            this.VisibleViewColumn.DataPropertyName = "IsVisible";
            this.VisibleViewColumn.HeaderText = "Visible";
            this.VisibleViewColumn.Name = "VisibleViewColumn";
            this.VisibleViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VisibleViewColumn.Width = 70;
            // 
            // RoleIdColumn
            // 
            this.RoleIdColumn.DataPropertyName = "ViewId";
            this.RoleIdColumn.HeaderText = "";
            this.RoleIdColumn.Name = "RoleIdColumn";
            this.RoleIdColumn.Visible = false;
            this.RoleIdColumn.Width = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Select Security Role";
            // 
            // SecurityRoleCombobox
            // 
            this.SecurityRoleCombobox.Enabled = false;
            this.SecurityRoleCombobox.FormattingEnabled = true;
            this.SecurityRoleCombobox.Location = new System.Drawing.Point(128, 60);
            this.SecurityRoleCombobox.Name = "SecurityRoleCombobox";
            this.SecurityRoleCombobox.Size = new System.Drawing.Size(400, 23);
            this.SecurityRoleCombobox.TabIndex = 4;
            this.SecurityRoleCombobox.SelectedIndexChanged += new System.EventHandler(this.SecurityRoleCombobox_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.SaveUserConfiguration);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.LoadUserData);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(15, 349);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 37);
            this.panel2.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(903, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Save Changes";
            // 
            // SaveUserConfiguration
            // 
            this.SaveUserConfiguration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveUserConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("SaveUserConfiguration.Image")));
            this.SaveUserConfiguration.Location = new System.Drawing.Point(885, 10);
            this.SaveUserConfiguration.Name = "SaveUserConfiguration";
            this.SaveUserConfiguration.Size = new System.Drawing.Size(16, 16);
            this.SaveUserConfiguration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SaveUserConfiguration.TabIndex = 7;
            this.SaveUserConfiguration.TabStop = false;
            this.SaveUserConfiguration.Click += new System.EventHandler(this.SaveUserConfiguration_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(792, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Load User Data";
            // 
            // LoadUserData
            // 
            this.LoadUserData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadUserData.Image = ((System.Drawing.Image)(resources.GetObject("LoadUserData.Image")));
            this.LoadUserData.Location = new System.Drawing.Point(772, 9);
            this.LoadUserData.Name = "LoadUserData";
            this.LoadUserData.Size = new System.Drawing.Size(16, 16);
            this.LoadUserData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LoadUserData.TabIndex = 5;
            this.LoadUserData.TabStop = false;
            this.LoadUserData.Click += new System.EventHandler(this.LoadUserData_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(278, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "User Preferences for View Configuration";
            // 
            // UserConfigurationGridView
            // 
            this.UserConfigurationGridView.BackgroundColor = System.Drawing.Color.White;
            this.UserConfigurationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserConfigurationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserIdColumn,
            this.UserNameColumn,
            this.RoleColumn,
            this.EnableConfiguration,
            this.IsDirty});
            this.UserConfigurationGridView.Enabled = false;
            this.UserConfigurationGridView.Location = new System.Drawing.Point(13, 393);
            this.UserConfigurationGridView.Name = "UserConfigurationGridView";
            this.UserConfigurationGridView.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.UserConfigurationGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.UserConfigurationGridView.Size = new System.Drawing.Size(1007, 235);
            this.UserConfigurationGridView.TabIndex = 6;
            // 
            // UserIdColumn
            // 
            this.UserIdColumn.DataPropertyName = "UserId";
            this.UserIdColumn.HeaderText = "";
            this.UserIdColumn.Name = "UserIdColumn";
            this.UserIdColumn.Visible = false;
            this.UserIdColumn.Width = 150;
            // 
            // UserNameColumn
            // 
            this.UserNameColumn.DataPropertyName = "UserName";
            this.UserNameColumn.HeaderText = "User";
            this.UserNameColumn.Name = "UserNameColumn";
            this.UserNameColumn.ReadOnly = true;
            this.UserNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UserNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UserNameColumn.Width = 450;
            // 
            // RoleColumn
            // 
            this.RoleColumn.HeaderText = "Preferred Role for View Configuration";
            this.RoleColumn.Name = "RoleColumn";
            this.RoleColumn.Width = 350;
            // 
            // EnableConfiguration
            // 
            this.EnableConfiguration.DataPropertyName = "IsEnabled";
            this.EnableConfiguration.HeaderText = "View Configuration Enabled";
            this.EnableConfiguration.Name = "EnableConfiguration";
            this.EnableConfiguration.Width = 180;
            // 
            // IsDirty
            // 
            this.IsDirty.HeaderText = "";
            this.IsDirty.Name = "IsDirty";
            this.IsDirty.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1023, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RoleBasedViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 663);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.UserConfigurationGridView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SecurityRoleCombobox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "RoleBasedViewForm";
            this.Text = "Confgure Role based Views";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveChangesImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadMetadataImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EntityGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ProgressPanel.ResumeLayout(false);
            this.ProgressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewsGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveUserConfiguration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadUserData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserConfigurationGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox LoadMetadataImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox SaveChangesImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView ViewsGridView;
        private System.Windows.Forms.DataGridView EntityGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SecurityRoleCombobox;
        private System.Windows.Forms.Panel ProgressPanel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntityDisplayNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntityLogicalNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectTypeCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnedTypeCodeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DefaultViewColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VisibleViewColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleIdColumn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox SaveUserConfiguration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox LoadUserData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView UserConfigurationGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn RoleColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnableConfiguration;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDirty;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}

