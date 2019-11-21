namespace WindowsFormsApp1
{
    partial class AddSourceFuelForm
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
            KryptonOutlookGrid.Classes.OutlookGridGroupCollection outlookGridGroupCollection1 = new KryptonOutlookGrid.Classes.OutlookGridGroupCollection();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kryptonOutlookGrid7 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            this.fid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fNames = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.fed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trade = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.kryptonOutlookGridGroupBox1 = new KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid7)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.panel1.Location = new System.Drawing.Point(-3, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 2);
            this.panel1.TabIndex = 66;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.panel2.Controls.Add(this.kryptonOutlookGrid7);
            this.panel2.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(630, 388);
            this.panel2.TabIndex = 67;
            // 
            // kryptonOutlookGrid7
            // 
            this.kryptonOutlookGrid7.AllowUserToAddRows = false;
            this.kryptonOutlookGrid7.AllowUserToDeleteRows = false;
            this.kryptonOutlookGrid7.AllowUserToOrderColumns = true;
            this.kryptonOutlookGrid7.AllowUserToResizeColumns = false;
            this.kryptonOutlookGrid7.AllowUserToResizeRows = false;
            this.kryptonOutlookGrid7.ColumnHeadersHeight = 19;
            this.kryptonOutlookGrid7.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fid,
            this.fCode,
            this.fNames,
            this.fed,
            this.Qn,
            this.By,
            this.trade});
            this.kryptonOutlookGrid7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGrid7.FillMode = KryptonOutlookGrid.Classes.FillMode.GROUPSONLY;
            this.kryptonOutlookGrid7.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid7.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlRibbon;
            this.kryptonOutlookGrid7.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid7.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGrid7.MultiSelect = false;
            this.kryptonOutlookGrid7.Name = "kryptonOutlookGrid7";
            this.kryptonOutlookGrid7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonOutlookGrid7.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid7.RowHeadersWidth = 10;
            this.kryptonOutlookGrid7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonOutlookGrid7.ShowLines = false;
            this.kryptonOutlookGrid7.Size = new System.Drawing.Size(630, 388);
            this.kryptonOutlookGrid7.TabIndex = 10;
            // 
            // fid
            // 
            this.fid.HeaderText = "id";
            this.fid.Name = "fid";
            this.fid.ReadOnly = true;
            this.fid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fid.Visible = false;
            this.fid.Width = 5;
            // 
            // fCode
            // 
            this.fCode.HeaderText = "#";
            this.fCode.Name = "fCode";
            this.fCode.ReadOnly = true;
            this.fCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.fCode.Width = 50;
            // 
            // fNames
            // 
            this.fNames.HeaderText = "Наименование";
            this.fNames.Name = "fNames";
            this.fNames.ReadOnly = true;
            this.fNames.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fNames.Width = 320;
            // 
            // fed
            // 
            this.fed.HeaderText = "Ед. изм.";
            this.fed.Name = "fed";
            this.fed.ReadOnly = true;
            this.fed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fed.Width = 85;
            // 
            // Qn
            // 
            this.Qn.HeaderText = "Qн, ккал/ед";
            this.Qn.Name = "Qn";
            this.Qn.ReadOnly = true;
            this.Qn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Qn.Width = 65;
            // 
            // By
            // 
            this.By.HeaderText = "By, кг. у.т.";
            this.By.Name = "By";
            this.By.ReadOnly = true;
            this.By.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.By.Width = 65;
            // 
            // trade
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.trade.DefaultCellStyle = dataGridViewCellStyle1;
            this.trade.FalseValue = null;
            this.trade.HeaderText = "о.н.";
            this.trade.IndeterminateValue = null;
            this.trade.Name = "trade";
            this.trade.ReadOnly = true;
            this.trade.TrueValue = null;
            this.trade.Width = 35;
            // 
            // kryptonOutlookGridGroupBox1
            // 
            this.kryptonOutlookGridGroupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonOutlookGridGroupBox1.Location = new System.Drawing.Point(266, 148);
            this.kryptonOutlookGridGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kryptonOutlookGridGroupBox1.Name = "kryptonOutlookGridGroupBox1";
            this.kryptonOutlookGridGroupBox1.Size = new System.Drawing.Size(32, 31);
            this.kryptonOutlookGridGroupBox1.TabIndex = 1;
            this.kryptonOutlookGridGroupBox1.Visible = false;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kryptonButton2.Location = new System.Drawing.Point(523, 401);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(96, 32);
            this.kryptonButton2.TabIndex = 67;
            this.kryptonButton2.Values.Image = global::WindowsFormsApp1.Properties.Resources.cancel1;
            this.kryptonButton2.Values.Text = "&Отмена";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(405, 401);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(99, 32);
            this.kryptonButton1.TabIndex = 66;
            this.kryptonButton1.Values.Image = global::WindowsFormsApp1.Properties.Resources.ok;
            this.kryptonButton1.Values.Text = "&Выбрать";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // AddSourceFuelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(631, 445);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddSourceFuelForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.Text = "Выбор топлива";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private KryptonOutlookGrid.Classes.KryptonOutlookGrid kryptonOutlookGrid7;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn fName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fid;
        private System.Windows.Forms.DataGridViewTextBoxColumn fCode;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn fNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn fed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qn;
        private System.Windows.Forms.DataGridViewTextBoxColumn By;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn trade;
    }
}