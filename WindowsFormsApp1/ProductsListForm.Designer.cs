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
            JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridGroupCollection outlookGridGroupCollection1 = new JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridGroupCollection();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonOutlookGrid1 = new JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.KryptonOutlookGrid();
            this.kryptonOutlookGridGroupBox1 = new JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.KryptonOutlookGridGroupBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ed_norm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s111 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.s112 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
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
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Code,
            this.Names,
            this.ed,
            this.ed_norm,
            this.s111,
            this.s112,
            this.type});
            this.kryptonOutlookGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGrid1.FillMode = JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.FillMode.GroupsOnly;
            this.kryptonOutlookGrid1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlRibbon;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid1.HideColumnOnGrouping = true;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(836, 581);
            this.kryptonOutlookGrid1.TabIndex = 0;
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
            // ProductsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(836, 581);
            this.Controls.Add(this.kryptonOutlookGrid1);
            this.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.Name = "ProductsListForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.Text = "Standard Docking";
            this.Load += new System.EventHandler(this.ProductsListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.KryptonOutlookGrid kryptonOutlookGrid1;
        private JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn ed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ed_norm;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn s111;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn s112;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
    }
}

