namespace WindowsFormsApp1
{
    partial class AddUserForm
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
            this.kryptonDockableWorkspace1 = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonDockingManager1 = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonDockableNavigator1 = new ComponentFactory.Krypton.Docking.KryptonDockableNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDockableNavigator2 = new ComponentFactory.Krypton.Docking.KryptonDockableNavigator();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigator1)).BeginInit();
            this.kryptonDockableNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigator2)).BeginInit();
            this.kryptonDockableNavigator2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonDockableWorkspace1
            // 
            this.kryptonDockableWorkspace1.AutoHiddenHost = false;
            this.kryptonDockableWorkspace1.CompactFlags = ((ComponentFactory.Krypton.Workspace.CompactFlags)(((ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptyCells | ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | ComponentFactory.Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.kryptonDockableWorkspace1.Name = "kryptonDockableWorkspace1";
            // 
            // 
            // 
            this.kryptonDockableWorkspace1.Root.UniqueName = "2E60A774CE2B40B88897A4C9436E9464";
            this.kryptonDockableWorkspace1.Root.WorkspaceControl = this.kryptonDockableWorkspace1;
            this.kryptonDockableWorkspace1.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(800, 450);
            this.kryptonDockableWorkspace1.TabIndex = 0;
            this.kryptonDockableWorkspace1.TabStop = true;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonDockableNavigator2);
            this.kryptonPanel1.Controls.Add(this.kryptonDockableNavigator1);
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kryptonDockableNavigator1
            // 
            this.kryptonDockableNavigator1.Location = new System.Drawing.Point(3, 3);
            this.kryptonDockableNavigator1.Name = "kryptonDockableNavigator1";
            this.kryptonDockableNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1});
            this.kryptonDockableNavigator1.SelectedIndex = 0;
            this.kryptonDockableNavigator1.Size = new System.Drawing.Size(203, 447);
            this.kryptonDockableNavigator1.TabIndex = 0;
            this.kryptonDockableNavigator1.Text = "kryptonDockableNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(201, 420);
            this.kryptonPage1.Text = "kryptonPage1";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "CCF2FFB0EDB846A55AAC25878CE34A42";
            // 
            // kryptonDockableNavigator2
            // 
            this.kryptonDockableNavigator2.Location = new System.Drawing.Point(211, 3);
            this.kryptonDockableNavigator2.Name = "kryptonDockableNavigator2";
            this.kryptonDockableNavigator2.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage3});
            this.kryptonDockableNavigator2.SelectedIndex = 0;
            this.kryptonDockableNavigator2.Size = new System.Drawing.Size(589, 447);
            this.kryptonDockableNavigator2.TabIndex = 1;
            this.kryptonDockableNavigator2.Text = "kryptonDockableNavigator2";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(587, 420);
            this.kryptonPage3.Text = "kryptonPage3";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "4876615FAF0445E29AB808914AD4DAEB";
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonDockableWorkspace1);
            this.Name = "AddUserForm";
            this.Text = "AddUserForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigator1)).EndInit();
            this.kryptonDockableNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigator2)).EndInit();
            this.kryptonDockableNavigator2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace1;
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Docking.KryptonDockableNavigator kryptonDockableNavigator2;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ComponentFactory.Krypton.Docking.KryptonDockableNavigator kryptonDockableNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
    }
}