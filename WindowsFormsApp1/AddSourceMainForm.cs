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
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class AddSourceMainForm : KryptonForm
    {
        int obj_id, fuel_id;
        public AddSourceMainForm()
        {
            InitializeComponent();
            this.SourceTextBox.AutoSize = false;
            this.SourceTextBox.Size = new Size(224, 18);
            this.FuelTextBox.AutoSize = false;
            this.FuelTextBox.Size = new Size(224, 18);
            typeComboBox.DisplayMember = "Text";
            typeComboBox.ValueMember = "Value";

            var items = new[] { new { Text = "Тепловая энергия", Value = "2" }, new { Text = "Электрическая энергия", Value = "3" } };
            typeComboBox.DataSource = items;
            typeComboBox.SelectedIndex = 0;
            typeComboBox.Visible = true;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceObjectForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK)
            {
                obj_id = myForm.obj_id;
                SourceTextBox.Text = myForm.obj_name;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceFuelForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK)
            {
                fuel_id = myForm.fuel_id;
                FuelTextBox.Text = myForm.fuel_name;
            }

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnLoad(EventArgs e)
        {
            var btn = new Button();
            var btn2 = new Button();
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new Size(25, SourceTextBox.ClientSize.Height + 2);
            btn.Location = new Point(SourceTextBox.ClientSize.Width - btn.Width, -1);
            btn.Cursor = Cursors.Default;
            btn.Text = "...";
            btn2.BackColor = Color.White;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.FlatAppearance.BorderSize = 0;
            btn2.Size = new Size(25, FuelTextBox.ClientSize.Height + 2);
            btn2.Location = new Point(FuelTextBox.ClientSize.Width - btn2.Width, -1);
            btn2.Cursor = Cursors.Default;
            btn2.Text = "...";
            btn2.Click += btn2_Click;
            btn.Click += btn_Click;
            SourceTextBox.Controls.Add(btn);
            SendMessage(SourceTextBox.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            FuelTextBox.Controls.Add(btn2);
            SendMessage(FuelTextBox.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn2.Width << 16));
            base.OnLoad(e);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel pan = (Panel)sender;
            //ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207))))), ButtonBorderStyle.Solid);
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(SourceTextBox.Location.X - variance, SourceTextBox.Location.Y - variance, SourceTextBox.Width + variance, SourceTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(FuelTextBox.Location.X - variance, FuelTextBox.Location.Y - variance, FuelTextBox.Width + variance, FuelTextBox.Height + variance));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
