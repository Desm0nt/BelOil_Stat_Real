namespace WindowsFormsApp1
{
    partial class SelectPersonForm
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
            this.kryptonOutlookGrid1 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flag = new KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonOutlookGridGroupBox1 = new KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.kryptonOutlookGrid1);
            this.panel1.Controls.Add(this.kryptonOutlookGridGroupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 312);
            this.panel1.TabIndex = 0;
            // 
            // kryptonOutlookGrid1
            // 
            this.kryptonOutlookGrid1.AllowUserToAddRows = false;
            this.kryptonOutlookGrid1.AllowUserToDeleteRows = false;
            this.kryptonOutlookGrid1.AllowUserToResizeColumns = false;
            this.kryptonOutlookGrid1.AllowUserToResizeRows = false;
            this.kryptonOutlookGrid1.ColumnHeadersHeight = 18;
            this.kryptonOutlookGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Flag,
            this.Name,
            this.Post});
            this.kryptonOutlookGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonOutlookGrid1.FillMode = KryptonOutlookGrid.Classes.FillMode.GROUPSONLY;
            this.kryptonOutlookGrid1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonOutlookGrid1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlRibbon;
            this.kryptonOutlookGrid1.GroupCollection = outlookGridGroupCollection1;
            this.kryptonOutlookGrid1.Location = new System.Drawing.Point(0, 0);
            this.kryptonOutlookGrid1.Name = "kryptonOutlookGrid1";
            this.kryptonOutlookGrid1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonOutlookGrid1.PreviousSelectedGroupRow = -1;
            this.kryptonOutlookGrid1.RowHeadersWidth = 10;
            this.kryptonOutlookGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonOutlookGrid1.ShowLines = false;
            this.kryptonOutlookGrid1.Size = new System.Drawing.Size(498, 312);
            this.kryptonOutlookGrid1.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Flag
            // 
            this.Flag.HeaderText = "Подпись";
            this.Flag.Name = "Flag";
            this.Flag.ReadOnly = true;
            this.Flag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Flag.Width = 65;
            // 
            // Name
            // 
            this.Name.HeaderText = "Имя";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Width = 200;
            // 
            // Post
            // 
            this.Post.HeaderText = "Должность";
            this.Post.Name = "Post";
            this.Post.ReadOnly = true;
            this.Post.Width = 220;
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.panel2.Controls.Add(this.kryptonButton2);
            this.panel2.Controls.Add(this.kryptonButton1);
            this.panel2.Location = new System.Drawing.Point(0, 314);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(499, 62);
            this.panel2.TabIndex = 1;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kryptonButton2.Location = new System.Drawing.Point(384, 17);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(96, 32);
            this.kryptonButton2.TabIndex = 20;
            this.kryptonButton2.Values.Image = global::WindowsFormsApp1.Properties.Resources.cancel1;
            this.kryptonButton2.Values.Text = "&Отмена";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(266, 17);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(99, 32);
            this.kryptonButton1.TabIndex = 19;
            this.kryptonButton1.Values.Image = global::WindowsFormsApp1.Properties.Resources.ok;
            this.kryptonButton1.Values.Text = "&Выбрать";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // SelectPersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(499, 376);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(515, 415);
            this.MinimumSize = new System.Drawing.Size(515, 415);
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectPersonForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonOutlookGrid1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private KryptonOutlookGrid.Classes.KryptonOutlookGrid kryptonOutlookGrid1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private KryptonOutlookGrid.Controls.KryptonOutlookGridGroupBox kryptonOutlookGridGroupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private KryptonOutlookGrid.CustomColumns.KryptonDataGridViewTextAndImageColumn Flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Post;
    }
}