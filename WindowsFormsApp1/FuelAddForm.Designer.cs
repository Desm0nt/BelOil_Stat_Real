namespace WindowsFormsApp1
{
    partial class FuelAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuelAddForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDown1 = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.warninglbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.QnTextBox = new System.Windows.Forms.TextBox();
            this.UnitTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Наименование";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(37, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Код строки";
            // 
            // numUpDown1
            // 
            this.numUpDown1.DecimalPlaces = 99;
            this.numUpDown1.Enabled = false;
            this.numUpDown1.Location = new System.Drawing.Point(35, 35);
            this.numUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numUpDown1.Name = "numUpDown1";
            this.numUpDown1.Size = new System.Drawing.Size(74, 22);
            this.numUpDown1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(65)))), ((int)(((byte)(135)))));
            this.label6.Location = new System.Drawing.Point(259, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(65)))), ((int)(((byte)(135)))));
            this.label7.Location = new System.Drawing.Point(458, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 14;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(266, 142);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(118, 32);
            this.kryptonButton1.TabIndex = 17;
            this.kryptonButton1.Values.Image = global::WindowsFormsApp1.Properties.Resources.ok;
            this.kryptonButton1.Values.Text = "&Ок";
            this.kryptonButton1.Click += new System.EventHandler(this.OkButton1_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kryptonButton2.Location = new System.Drawing.Point(397, 141);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(96, 32);
            this.kryptonButton2.TabIndex = 18;
            this.kryptonButton2.Values.Image = global::WindowsFormsApp1.Properties.Resources.cancel1;
            this.kryptonButton2.Values.Text = "&Отмена";
            this.kryptonButton2.Click += new System.EventHandler(this.AbortButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(203)))), ((int)(((byte)(239)))));
            this.panel1.Location = new System.Drawing.Point(-4, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 3);
            this.panel1.TabIndex = 19;
            // 
            // nameTextBox
            // 
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.Location = new System.Drawing.Point(128, 38);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(338, 15);
            this.nameTextBox.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(127, 261);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 25;
            // 
            // warninglbl
            // 
            this.warninglbl.AutoSize = true;
            this.warninglbl.BackColor = System.Drawing.Color.Transparent;
            this.warninglbl.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.warninglbl.ForeColor = System.Drawing.Color.Black;
            this.warninglbl.Location = new System.Drawing.Point(129, 108);
            this.warninglbl.Name = "warninglbl";
            this.warninglbl.Size = new System.Drawing.Size(0, 15);
            this.warninglbl.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Qн, ккал/кг";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(307, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Ед. изм.";
            // 
            // QnTextBox
            // 
            this.QnTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QnTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.QnTextBox.Location = new System.Drawing.Point(129, 77);
            this.QnTextBox.Name = "QnTextBox";
            this.QnTextBox.Size = new System.Drawing.Size(156, 15);
            this.QnTextBox.TabIndex = 31;
            // 
            // UnitTextBox
            // 
            this.UnitTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UnitTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnitTextBox.Location = new System.Drawing.Point(310, 77);
            this.UnitTextBox.Name = "UnitTextBox";
            this.UnitTextBox.Size = new System.Drawing.Size(156, 15);
            this.UnitTextBox.TabIndex = 32;
            // 
            // FuelAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(505, 184);
            this.Controls.Add(this.UnitTextBox);
            this.Controls.Add(this.QnTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.warninglbl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FuelAddForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные сотрудника";
            this.Load += new System.EventHandler(this.FuelAddForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label warninglbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox QnTextBox;
        private System.Windows.Forms.TextBox UnitTextBox;
    }
}