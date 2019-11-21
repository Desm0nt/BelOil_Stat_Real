namespace WindowsFormsApp1
{
    partial class AddSourceObjectForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kryptonOutlookGrid1 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.fname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonOutlookGridGroupBox1 = new KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.panel1.Location = new System.Drawing.Point(-3, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 2);
            this.panel1.TabIndex = 66;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.panel2.Controls.Add(this.kryptonOutlookGrid1);
            this.panel2.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(453, 388);
            this.panel2.TabIndex = 67;
            // 
            // kryptonOutlookGrid1
            // 
            this.kryptonOutlookGrid1.AllowUserToAddRows = false;
            this.kryptonOutlookGrid1.AllowUserToDeleteRows = false;
            this.kryptonOutlookGrid1.AllowUserToResizeColumns = false;
            this.kryptonOutlookGrid1.AllowUserToResizeRows = false;
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.fname});
            this.kryptonOutlookGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGrid1.FillMode = KryptonOutlookGrid.Classes.FillMode.GROUPSONLY;
            this.kryptonOutlookGrid1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlRibbon;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid1.HideOuterBorders = true;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGrid1.MultiSelect = false;
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.RowHeadersWidth = 10;
            this.kryptonOutlookGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(453, 388);
            this.kryptonOutlookGrid1.TabIndex = 2;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // name
            // 
            this.name.HeaderText = "Наименование";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name.Width = 442;
            // 
            // fname
            // 
            this.fname.HeaderText = "Полное наименование";
            this.fname.Name = "fname";
            this.fname.ReadOnly = true;
            this.fname.Visible = false;
            this.fname.Width = 220;
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
            this.kryptonButton2.Location = new System.Drawing.Point(346, 401);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(96, 32);
            this.kryptonButton2.TabIndex = 67;
            this.kryptonButton2.Values.Image = global::WindowsFormsApp1.Properties.Resources.cancel1;
            this.kryptonButton2.Values.Text = "&Отмена";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(228, 401);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(99, 32);
            this.kryptonButton1.TabIndex = 66;
            this.kryptonButton1.Values.Image = global::WindowsFormsApp1.Properties.Resources.ok;
            this.kryptonButton1.Values.Text = "&Выбрать";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // AddSourceObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(454, 445);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddSourceObjectForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.Text = "Выбор источника";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private KryptonOutlookGrid.Classes.KryptonOutlookGrid kryptonOutlookGrid1;
        private KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn fname;
    }
}