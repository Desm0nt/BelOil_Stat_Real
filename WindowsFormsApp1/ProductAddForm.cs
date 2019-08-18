using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class ProductAddForm : KryptonForm
    {
        public ProductsListForm ParrentForm;
        bool edit = false;
        bool idlock, codelock;
        public DataTables.personTable personTable;
        DataTables.personTable Table;
        List<int> idList, codeList;

        public ProductAddForm()
        {
            InitializeComponent();
            Table = new DataTables.personTable();
            idList = dbOps.GetProdIdList(1);
            codeList = dbOps.GetProdCodeList(1);
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(74, 21);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(74, 21);
            typeComboBox.DisplayMember = "Text";
            typeComboBox.ValueMember = "Value";

            var items = new[] { new { Text = "Топливо", Value = "1" }, new { Text = "Тепловые ресурсы", Value = "2" }, new { Text = "Электрические ресурсы", Value = "3" } };
            typeComboBox.DataSource = items;
            typeComboBox.SelectedIndex = 0;
            typeComboBox.Visible = true;
            Table.type = 1;
        }

        public ProductAddForm(DataTables.personTable table, ProductsListForm parrentForm)
        {
            edit = true;
            InitializeComponent();
            this.ParrentForm = parrentForm;
            Table = table;
            idList = dbOps.GetProdIdList(Table.type);
            codeList = dbOps.GetProdCodeList(Table.type);
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(74, 21);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(74, 21);
            kryptonNumericUpDown1.Value = table.Code;
            kryptonNumericUpDown2.Value = table.Id;
            kryptonTextBox2.Text = table.Name;
            label6.Text = table.Unit;
            label7.Text = table.nUnit;
            kryptonCheckBox1.Checked = table.s111;
            kryptonCheckBox2.Checked = table.s112;
            switch (Table.type)
            {
                case 1:
                    label9.Text = "Топливо";
                    break;
                case 2:
                    label9.Text = "Тепловые ресурсы";
                    break;
                case 3:
                    label9.Text = "Электрические ресурсы";
                    break;
                default:
                    break;
            }
        }

        private void OkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (kryptonNumericUpDown1.Value == 0 || kryptonNumericUpDown2.Value == 0 || String.IsNullOrWhiteSpace(kryptonTextBox2.Text) 
                    || String.IsNullOrWhiteSpace(label6.Text) || String.IsNullOrWhiteSpace(label7.Text))
                    throw new Exception("Все поля должны быть заполнены!");
                if (codelock)
                    throw new Exception("Данный код строки уже используется! Введите другой код строки.");
                if (idlock)
                    throw new Exception("Данный id уже используется! Введите другой id.");
                if (!edit)
                {
                    Table.Id = (int)kryptonNumericUpDown2.Value;
                    Table.type = int.Parse(typeComboBox.SelectedValue.ToString());
                }
                personTable = new DataTables.personTable
                {
                    Id = (int)this.kryptonNumericUpDown2.Value,
                    Code = (int)this.kryptonNumericUpDown1.Value,
                    Name = this.kryptonTextBox2.Text,
                    Unit = this.label6.Text,
                    nUnit = this.label7.Text,
                    s111 = this.kryptonCheckBox1.Checked,
                    s112 = this.kryptonCheckBox2.Checked,
                    type = Table.type
                };
                if (edit)
                {
                    this.DialogResult = DialogResult.OK;
                }
                dbOps.UpdateProdList(personTable, Table.Id);
                this.Close();               
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void CheckNumInput()
        {
            if (codeList.Contains((int)kryptonNumericUpDown1.Value) && (int)kryptonNumericUpDown1.Value!=Table.Code)
            {
                warninglbl.Text = "Данный код строке уже имеется в базе";
                codelock = true;
            }
            else if (idList.Contains((int)kryptonNumericUpDown2.Value) && (int)kryptonNumericUpDown2.Value != Table.Id)
            {
                warninglbl.Text = "Данный id (#) уже имеется в базе";
                idlock = true;
            }
            else
            {
                warninglbl.Text = "";
                codelock = false;
                idlock = false;
            }
        }

        private void AbortButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show("hello world");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show("hello world2");
        }

        #region шаманства отрисовки и обработки  
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnLoad(EventArgs e)
        {
            var btn = new Button();
            var btn2 = new Button();
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new Size(25, textBox1.ClientSize.Height + 2);
            btn.Location = new Point(textBox1.ClientSize.Width - btn.Width, -1);
            btn.Cursor = Cursors.Default;
            btn.Text = "...";
            btn2.BackColor = Color.White;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.FlatAppearance.BorderSize = 0;
            btn2.Size = new Size(25, textBox1.ClientSize.Height + 2);
            btn2.Location = new Point(textBox1.ClientSize.Width - btn2.Width, -1);
            btn2.Cursor = Cursors.Default;
            btn2.Text = "...";
            btn2.Click += btn2_Click;
            btn.Click += btn_Click;
            textBox1.Controls.Add(btn);
            SendMessage(textBox1.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            textBox2.Controls.Add(btn2);
            SendMessage(textBox2.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn2.Width << 16));
            base.OnLoad(e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(textBox1.Location.X - variance, textBox1.Location.Y - variance, textBox1.Width + variance, textBox1.Height + variance));
            g.DrawRectangle(p, new Rectangle(textBox2.Location.X - variance, textBox2.Location.Y - variance, textBox2.Width + variance, textBox2.Height + variance));
            if (typeComboBox.Visible)
                g.DrawRectangle(p, new Rectangle(typeComboBox.Location.X - variance, typeComboBox.Location.Y - variance, typeComboBox.Width + variance, typeComboBox.Height + variance));

        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            idList = dbOps.GetProdIdList(Int32.Parse(typeComboBox.SelectedValue.ToString()));
            codeList = dbOps.GetProdCodeList(Int32.Parse(typeComboBox.SelectedValue.ToString()));
            CheckNumInput();
            Table.type = Int32.Parse(typeComboBox.SelectedValue.ToString());
            kryptonNumericUpDown1_ValueChanged(this.kryptonNumericUpDown1, new EventArgs());
        }

        private void kryptonNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            string tmp = Convert.ToString(this.kryptonNumericUpDown1.Value);
            int length = tmp.Length;
            for (int i = length; i< 4; i++)
                tmp = "0" + tmp;
            tmp = Convert.ToString(Table.type) + tmp;
            kryptonNumericUpDown2.Value = Decimal.Parse(tmp);
            CheckNumInput();
        }
        private void kryptonNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CheckNumInput();
        }
        #endregion
    }
}
