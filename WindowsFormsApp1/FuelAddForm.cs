using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class FuelAddForm : KryptonForm
    {
        public FuelListForm ParrentForm;
        bool edit = false;
        public DataTables.FuelsTable fuelTable;
        DataTables.FuelsTable Table;

        public FuelAddForm()
        {
            InitializeComponent();
            Table = new DataTables.FuelsTable();
            this.nameTextBox.AutoSize = false;
            this.nameTextBox.Size = new Size(338, 18);
            this.QnTextBox.AutoSize = false;
            this.QnTextBox.Size = new Size(156, 18);
            this.UnitTextBox.AutoSize = false;
            this.UnitTextBox.Size = new Size(156, 18);
            numUpDown1.Value = dbOps.GetPersonLastID();
        }

        public FuelAddForm(DataTables.FuelsTable table, FuelListForm parrentForm)
        {
            edit = true;
            InitializeComponent();
            this.ParrentForm = parrentForm;
            Table = table;
            this.nameTextBox.AutoSize = false;
            this.nameTextBox.Size = new Size(338, 18);
            this.QnTextBox.AutoSize = false;
            this.QnTextBox.Size = new Size(156, 18);
            this.UnitTextBox.AutoSize = false;
            this.UnitTextBox.Size = new Size(156, 18);
            numUpDown1.Value = Table.id;
            nameTextBox.Text = Table.name;
            QnTextBox.Text = Table.Qn.ToString();
            UnitTextBox.Text = Table.unit;
            warninglbl.Text = "By = " + Table.B_y.ToString() + "кг у.т./кг";
        }

        private void OkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (numUpDown1.Value == 0 || String.IsNullOrWhiteSpace(nameTextBox.Text)
                    || String.IsNullOrWhiteSpace(QnTextBox.Text) || String.IsNullOrWhiteSpace(UnitTextBox.Text))
                    throw new Exception("Все поля должны быть заполнены!");
                if (!edit)
                {
                    Table.id = (int)numUpDown1.Value;
                }
                fuelTable = new DataTables.FuelsTable
                {
                    id = Table.id,
                    fuel_id = Table.fuel_id,
                    Qn = Int32.Parse(this.QnTextBox.Text),
                    name = this.nameTextBox.Text,
                    unit = this.UnitTextBox.Text,
                    B_y = Int32.Parse(this.warninglbl.Text)
                };
                if (edit)
                {
                    this.DialogResult = DialogResult.OK;
                    //dbOps.UpdatePersonList(fuelTable);
                }
                else
                {
                    //dbOps.AddNewPerson(fuelTable);
                }
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
            g.DrawRectangle(p, new Rectangle(QnTextBox.Location.X - variance, QnTextBox.Location.Y - variance, QnTextBox.Width + variance, QnTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(nameTextBox.Location.X - variance, nameTextBox.Location.Y - variance, nameTextBox.Width + variance, nameTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(UnitTextBox.Location.X - variance, UnitTextBox.Location.Y - variance, UnitTextBox.Width + variance, UnitTextBox.Height + variance));
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Table.Id_org = Int32.Parse(typeComboBox.SelectedValue.ToString());
        }
        #endregion

        private void FuelAddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
