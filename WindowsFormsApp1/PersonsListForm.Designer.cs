namespace WindowsFormsApp1
{
    partial class PersonsListForm
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
            this.components = new System.ComponentModel.Container();
            KryptonOutlookGrid.Classes.OutlookGridGroupCollection outlookGridGroupCollection1 = new KryptonOutlookGrid.Classes.OutlookGridGroupCollection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonsListForm));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonOutlookGrid1 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            this.kryptonOutlookGridGroupBox1 = new KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.searchToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.resetToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surname = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otchestvo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sign = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone_work = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.subhead = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.org_id = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // kryptonOutlookGrid1
            // 
            this.kryptonOutlookGrid1.AllowDrop = true;
            this.kryptonOutlookGrid1.AllowUserToAddRows = false;
            this.kryptonOutlookGrid1.AllowUserToDeleteRows = false;
            this.kryptonOutlookGrid1.AllowUserToResizeRows = false;
            this.kryptonOutlookGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.surname,
            this.name,
            this.otchestvo,
            this.sign,
            this.post,
            this.phone_work,
            this.phone,
            this.email,
            this.head,
            this.subhead,
            this.org_id});
            this.kryptonOutlookGrid1.FillMode = KryptonOutlookGrid.Classes.FillMode.GROUPSONLY;
            this.kryptonOutlookGrid1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid1.HideColumnOnGrouping = true;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 26);
            this.kryptonOutlookGrid1.MultiSelect = false;
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(1209, 505);
            this.kryptonOutlookGrid1.TabIndex = 0;
            this.kryptonOutlookGrid1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.kryptonOutlookGrid1_CellEnter);
            this.kryptonOutlookGrid1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.kryptonOutlookGrid1_CellMouseDoubleClick);
            this.kryptonOutlookGrid1.Resize += new System.EventHandler(this.kryptonOutlookGrid1_Resize);
            // 
            // kryptonOutlookGridGroupBox1
            // 
            this.kryptonOutlookGridGroupBox1.AllowDrop = true;
            this.kryptonOutlookGridGroupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonOutlookGridGroupBox1.Location = new System.Drawing.Point(24, 535);
            this.kryptonOutlookGridGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kryptonOutlookGridGroupBox1.Name = "kryptonOutlookGridGroupBox1";
            this.kryptonOutlookGridGroupBox1.Size = new System.Drawing.Size(744, 46);
            this.kryptonOutlookGridGroupBox1.TabIndex = 1;
            this.kryptonOutlookGridGroupBox1.Visible = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kryptonOutlookGrid1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1209, 531);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1209, 581);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.back;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1209, 26);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(30, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripButton,
            this.removeToolStripButton,
            this.toolStripSeparator,
            this.editToolStripButton,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(93, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToolStripButton.Image = global::WindowsFormsApp1.Properties.Resources.add;
            this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addToolStripButton.Text = "Добавить";
            this.addToolStripButton.Click += new System.EventHandler(this.addToolStripButton_Click);
            // 
            // removeToolStripButton
            // 
            this.removeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeToolStripButton.Image = global::WindowsFormsApp1.Properties.Resources.remove;
            this.removeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeToolStripButton.Name = "removeToolStripButton";
            this.removeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.removeToolStripButton.Text = "Удалить";
            this.removeToolStripButton.Click += new System.EventHandler(this.removeToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = global::WindowsFormsApp1.Properties.Resources.edit;
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editToolStripButton.Text = "Редактировать";
            this.editToolStripButton.Click += new System.EventHandler(this.editToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.searchToolStripButton1,
            this.resetToolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(254, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel2.Text = " Имя";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(160, 25);
            // 
            // searchToolStripButton1
            // 
            this.searchToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchToolStripButton1.Image = global::WindowsFormsApp1.Properties.Resources.search;
            this.searchToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchToolStripButton1.Name = "searchToolStripButton1";
            this.searchToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.searchToolStripButton1.Text = "Отфильтровать";
            // 
            // resetToolStripButton2
            // 
            this.resetToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetToolStripButton2.Image = global::WindowsFormsApp1.Properties.Resources.reset;
            this.resetToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetToolStripButton2.Name = "resetToolStripButton2";
            this.resetToolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.resetToolStripButton2.Text = "Сбросить фильтры";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.id.Width = 5;
            // 
            // surname
            // 
            this.surname.HeaderText = "Фамилия";
            this.surname.MinimumWidth = 192;
            this.surname.Name = "surname";
            this.surname.ReadOnly = true;
            this.surname.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.surname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.surname.Width = 192;
            // 
            // name
            // 
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.name.Width = 180;
            // 
            // otchestvo
            // 
            this.otchestvo.HeaderText = "Отчество";
            this.otchestvo.Name = "otchestvo";
            this.otchestvo.ReadOnly = true;
            this.otchestvo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.otchestvo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.otchestvo.Width = 158;
            // 
            // sign
            // 
            this.sign.HeaderText = "подпись";
            this.sign.Name = "sign";
            this.sign.ReadOnly = true;
            this.sign.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sign.Width = 105;
            // 
            // post
            // 
            this.post.HeaderText = "Должность";
            this.post.Name = "post";
            this.post.ReadOnly = true;
            this.post.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.post.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.post.Width = 175;
            // 
            // phone_work
            // 
            this.phone_work.HeaderText = "Раб. тел.";
            this.phone_work.Name = "phone_work";
            this.phone_work.ReadOnly = true;
            this.phone_work.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.phone_work.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phone_work.Width = 103;
            // 
            // phone
            // 
            this.phone.HeaderText = "Моб. тел.";
            this.phone.Name = "phone";
            this.phone.ReadOnly = true;
            this.phone.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.phone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phone.Width = 103;
            // 
            // email
            // 
            this.email.HeaderText = "e-mail";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.email.Width = 135;
            // 
            // head
            // 
            this.head.HeaderText = " ";
            this.head.Name = "head";
            this.head.ReadOnly = true;
            this.head.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // subhead
            // 
            this.subhead.HeaderText = " ";
            this.subhead.Name = "subhead";
            this.subhead.ReadOnly = true;
            this.subhead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // org_id
            // 
            this.org_id.HeaderText = "org_id";
            this.org_id.Name = "org_id";
            this.org_id.ReadOnly = true;
            this.org_id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.org_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PersonsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1209, 581);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1225, 620);
            this.Name = "PersonsListForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.PersonsListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private KryptonOutlookGrid.Classes.KryptonOutlookGrid kryptonOutlookGrid1;
        private KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
        private System.Windows.Forms.ToolStripButton removeToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton searchToolStripButton1;
        private System.Windows.Forms.ToolStripButton resetToolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn otchestvo;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn post;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone_work;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn head;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn subhead;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn org_id;
    }
}

