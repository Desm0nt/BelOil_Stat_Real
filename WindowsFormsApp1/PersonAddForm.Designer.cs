namespace WindowsFormsApp1
{
    partial class PersonAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonAddForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kryptonNumericUpDown2 = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.fnameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.warninglbl = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.otchTextBox = new System.Windows.Forms.TextBox();
            this.postTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Фамилия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(325, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "#";
            // 
            // kryptonNumericUpDown2
            // 
            this.kryptonNumericUpDown2.DecimalPlaces = 99;
            this.kryptonNumericUpDown2.Enabled = false;
            this.kryptonNumericUpDown2.Location = new System.Drawing.Point(344, 79);
            this.kryptonNumericUpDown2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.kryptonNumericUpDown2.Name = "kryptonNumericUpDown2";
            this.kryptonNumericUpDown2.Size = new System.Drawing.Size(74, 22);
            this.kryptonNumericUpDown2.TabIndex = 5;
            this.kryptonNumericUpDown2.ValueChanged += new System.EventHandler(this.kryptonNumericUpDown2_ValueChanged);
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
            this.kryptonButton1.Location = new System.Drawing.Point(208, 299);
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
            this.kryptonButton2.Location = new System.Drawing.Point(345, 299);
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
            this.panel1.Location = new System.Drawing.Point(-4, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 3);
            this.panel1.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Раздел: ";
            // 
            // fnameTextBox
            // 
            this.fnameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fnameTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fnameTextBox.Location = new System.Drawing.Point(22, 36);
            this.fnameTextBox.Name = "fnameTextBox";
            this.fnameTextBox.Size = new System.Drawing.Size(117, 15);
            this.fnameTextBox.TabIndex = 24;
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
            this.warninglbl.ForeColor = System.Drawing.Color.Red;
            this.warninglbl.Location = new System.Drawing.Point(24, 4);
            this.warninglbl.Name = "warninglbl";
            this.warninglbl.Size = new System.Drawing.Size(0, 15);
            this.warninglbl.TabIndex = 26;
            // 
            // typeComboBox
            // 
            this.typeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(85, 130);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 27;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Имя";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Отчество";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.Location = new System.Drawing.Point(167, 36);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(117, 15);
            this.nameTextBox.TabIndex = 31;
            // 
            // otchTextBox
            // 
            this.otchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.otchTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.otchTextBox.Location = new System.Drawing.Point(311, 36);
            this.otchTextBox.Name = "otchTextBox";
            this.otchTextBox.Size = new System.Drawing.Size(117, 15);
            this.otchTextBox.TabIndex = 32;
            // 
            // postTextBox
            // 
            this.postTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.postTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.postTextBox.Location = new System.Drawing.Point(22, 83);
            this.postTextBox.Name = "postTextBox";
            this.postTextBox.Size = new System.Drawing.Size(262, 15);
            this.postTextBox.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Должность";
            // 
            // PersonAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(453, 339);
            this.Controls.Add(this.postTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.otchTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.warninglbl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.fnameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.kryptonNumericUpDown2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonAddForm";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные вида продукции (работы, услуги)";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown kryptonNumericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox fnameTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label warninglbl;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox otchTextBox;
        private System.Windows.Forms.TextBox postTextBox;
        private System.Windows.Forms.Label label11;
    }
}