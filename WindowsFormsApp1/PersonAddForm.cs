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
    public partial class PersonAddForm : KryptonForm
    {
        public static List<ListElement> elList;
        public PersonsListForm ParrentForm;
        bool edit = false;
        public DataTables.PersonTable personTable;
        DataTables.PersonTable Table;
        List<int> idList, codeList;

        public PersonAddForm(List<ListElement> ElList)
        {
            InitializeComponent();
            Table = new DataTables.PersonTable();
            idList = dbOps.GetProdIdList(1);
            codeList = dbOps.GetProdCodeList(1);
            this.fnameTextBox.AutoSize = false;
            this.fnameTextBox.Size = new System.Drawing.Size(117, 18);
            this.nameTextBox.AutoSize = false;
            this.nameTextBox.Size = new System.Drawing.Size(117, 18);
            this.otchTextBox.AutoSize = false;
            this.otchTextBox.Size = new System.Drawing.Size(117, 18);
            this.postTextBox.AutoSize = false;
            this.postTextBox.Size = new System.Drawing.Size(117, 18);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(344, 18);
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(164, 18);
            this.textBox3.AutoSize = false;
            this.textBox3.Size = new System.Drawing.Size(164, 18);
            var items = new[] { new { Text = "Топливо", Value = "1" }, new { Text = "Тепловые ресурсы", Value = "2" }, new { Text = "Электрические ресурсы", Value = "3" } };
            var it2 = items.ToList();
            it2.Clear();
            foreach (var a in ElList)
                it2.Add(new { Text = a.Name, Value = a.Id.ToString() });
            typeComboBox.DisplayMember = "Text";
            typeComboBox.ValueMember = "Value";
            typeComboBox.DataSource = it2;
            typeComboBox.SelectedIndex = 0;
            typeComboBox.Visible = true;
            label8.Visible = true;
            Table.Type = 1;
            kryptonNumericUpDown2.Value = dbOps.GetPersonLastID();
        }

        public PersonAddForm(DataTables.PersonTable table, PersonsListForm parrentForm, List<ListElement> ElList)
        {
            edit = true;
            InitializeComponent();
            this.ParrentForm = parrentForm;
            elList = ElList;
            Table = table;
            idList = dbOps.GetProdIdList(Table.Type);
            codeList = dbOps.GetProdCodeList(Table.Type);
            this.fnameTextBox.AutoSize = false;
            this.fnameTextBox.Size = new System.Drawing.Size(117, 18);
            this.nameTextBox.AutoSize = false;
            this.nameTextBox.Size = new System.Drawing.Size(117, 18);
            this.otchTextBox.AutoSize = false;
            this.otchTextBox.Size = new System.Drawing.Size(117, 18);
            this.postTextBox.AutoSize = false;
            this.postTextBox.Size = new System.Drawing.Size(262, 18);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(344, 18);
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(164, 18);
            this.textBox3.AutoSize = false;
            this.textBox3.Size = new System.Drawing.Size(164, 18);
            typeComboBox.Visible = false;
            label8.Visible = false;
            kryptonNumericUpDown2.Value = Table.Id;
            var a = PersonsListForm.elList.FirstOrDefault(n => n.Id == Table.Id_org);
            this.Text = "Данные сотрудника " + Table.Surname + " (" + a.Name + ")";
            fnameTextBox.Text = Table.Surname;
            nameTextBox.Text = Table.Name;
            otchTextBox.Text = Table.Otchestvo;
            postTextBox.Text = Table.Post;
            textBox1.Text = Table.Email;
            textBox2.Text = Table.WPhone;
            textBox3.Text = Table.Phone;
        }

        private void OkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (kryptonNumericUpDown2.Value == 0 || String.IsNullOrWhiteSpace(fnameTextBox.Text)
                    || String.IsNullOrWhiteSpace(nameTextBox.Text) || String.IsNullOrWhiteSpace(otchTextBox.Text))
                    throw new Exception("Все поля должны быть заполнены!");
                if (!edit)
                {
                    Table.Id = (int)kryptonNumericUpDown2.Value;
                    Table.Id_org = Int32.Parse(typeComboBox.SelectedValue.ToString());
                }
                var subh = PersonsListForm.elList.FirstOrDefault(n => n.Id == Table.Id_org);
                personTable = new DataTables.PersonTable
                {
                    Id = Table.Id,
                    Name = this.nameTextBox.Text,
                    Surname = this.fnameTextBox.Text,
                    Otchestvo = this.otchTextBox.Text,
                    Post = this.label7.Text,
                    WPhone = this.textBox2.Text,
                    Phone = this.textBox3.Text,
                    Email = this.textBox3.Text,
                    Id_org = Table.Id_org,
                    Subhead = subh.Group
                };
                if (edit)
                {
                    this.DialogResult = DialogResult.OK;
                }
                //dbOps.UpdateProdList(personTable, Table.Id);
                this.Close();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void AbortButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region шаманства отрисовки и обработки  
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(nameTextBox.Location.X - variance, nameTextBox.Location.Y - variance, nameTextBox.Width + variance, nameTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(fnameTextBox.Location.X - variance, fnameTextBox.Location.Y - variance, fnameTextBox.Width + variance, fnameTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(otchTextBox.Location.X - variance, otchTextBox.Location.Y - variance, otchTextBox.Width + variance, otchTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(postTextBox.Location.X - variance, postTextBox.Location.Y - variance, postTextBox.Width + variance, postTextBox.Height + variance));
            if (typeComboBox.Visible)
                g.DrawRectangle(p, new Rectangle(typeComboBox.Location.X - variance, typeComboBox.Location.Y - variance, typeComboBox.Width + variance, typeComboBox.Height + variance));

        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table.Id_org = Int32.Parse(typeComboBox.SelectedValue.ToString());
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel pan = (Panel)sender;

            ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207))))), ButtonBorderStyle.Solid);
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(textBox1.Location.X - variance, textBox1.Location.Y - variance, textBox1.Width + variance, textBox1.Height + variance));
            g.DrawRectangle(p, new Rectangle(textBox2.Location.X - variance, textBox2.Location.Y - variance, textBox2.Width + variance, textBox2.Height + variance));
            g.DrawRectangle(p, new Rectangle(textBox3.Location.X - variance, textBox3.Location.Y - variance, textBox3.Width + variance, textBox3.Height + variance));
        }
        #endregion
    }
}
