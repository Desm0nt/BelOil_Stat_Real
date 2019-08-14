namespace WindowsFormsApp1
{
    partial class ProductsListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsListForm));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonOutlookGrid1 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ed_norm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s111 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.s112 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.type = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
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
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.searchToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.resetToolStripButton2 = new System.Windows.Forms.ToolStripButton();
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
            this.kryptonOutlookGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Code,
            this.Names,
            this.ed,
            this.ed_norm,
            this.s111,
            this.s112,
            this.type});
            this.kryptonOutlookGrid1.FillMode = KryptonOutlookGrid.Classes.FillMode.GROUPSONLY;
            this.kryptonOutlookGrid1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid1.HideColumnOnGrouping = true;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 26);
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(786, 505);
            this.kryptonOutlookGrid1.TabIndex = 0;
            this.kryptonOutlookGrid1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.kryptonOutlookGrid1_CellEnter);
            this.kryptonOutlookGrid1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.kryptonOutlookGrid1_CellMouseDoubleClick);
            this.kryptonOutlookGrid1.Resize += new System.EventHandler(this.kryptonOutlookGrid1_Resize);
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
            // Code
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Code.DefaultCellStyle = dataGridViewCellStyle1;
            this.Code.HeaderText = "#";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Code.Width = 70;
            // 
            // Names
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Names.DefaultCellStyle = dataGridViewCellStyle2;
            this.Names.HeaderText = "Наименование";
            this.Names.Name = "Names";
            this.Names.ReadOnly = true;
            this.Names.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Names.Width = 300;
            // 
            // ed
            // 
            this.ed.HeaderText = "Ед. изм.";
            this.ed.Name = "ed";
            this.ed.ReadOnly = true;
            this.ed.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ed.Width = 115;
            // 
            // ed_norm
            // 
            this.ed_norm.HeaderText = "Ед. изм. нормы";
            this.ed_norm.Name = "ed_norm";
            this.ed_norm.ReadOnly = true;
            this.ed_norm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ed_norm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ed_norm.Width = 115;
            // 
            // s111
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            this.s111.DefaultCellStyle = dataGridViewCellStyle3;
            this.s111.FalseValue = null;
            this.s111.HeaderText = "стр. 111";
            this.s111.IndeterminateValue = null;
            this.s111.Name = "s111";
            this.s111.ReadOnly = true;
            this.s111.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.s111.TrueValue = null;
            this.s111.Width = 70;
            // 
            // s112
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = false;
            this.s112.DefaultCellStyle = dataGridViewCellStyle4;
            this.s112.FalseValue = null;
            this.s112.HeaderText = "стр. 112";
            this.s112.IndeterminateValue = null;
            this.s112.Name = "s112";
            this.s112.ReadOnly = true;
            this.s112.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.s112.TrueValue = null;
            this.s112.Width = 70;
            // 
            // type
            // 
            this.type.HeaderText = "Тип";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.type.Width = 50;
            // 
            // kryptonOutlookGridGroupBox1
            // 
            this.kryptonOutlookGridGroupBox1.AllowDrop = true;
            this.kryptonOutlookGridGroupBox1.Location = new System.Drawing.Point(24, 535);
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(786, 531);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(786, 581);
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
            this.panel1.Size = new System.Drawing.Size(786, 26);
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
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.searchToolStripButton1,
            this.resetToolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(375, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel1.Text = " # ";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(93, 22);
            this.toolStripLabel2.Text = " Наименование";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 25);
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
            // ProductsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(786, 581);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(802, 620);
            this.Name = "ProductsListForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Standard Docking";
            this.Load += new System.EventHandler(this.ProductsListForm_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn ed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ed_norm;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn s111;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn s112;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn type;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
        private System.Windows.Forms.ToolStripButton removeToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton searchToolStripButton1;
        private System.Windows.Forms.ToolStripButton resetToolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}

