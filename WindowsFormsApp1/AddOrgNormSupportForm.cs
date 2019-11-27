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
    public partial class AddOrgNormSupportForm : KryptonForm
    {
        public int fuel_id = 0;
        public string name;
        public string fuel_name = "";
        public AddOrgNormSupportForm(string norm_name, int type)
        {
            InitializeComponent();
            textBox1.Text = norm_name;
            this.FuelTextBox.AutoSize = false;
            this.FuelTextBox.Size = new Size(224, 18);
            if( type != 1)
            {
                checkBox1.Enabled = false;
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
                fuel_name = " (" + myForm.fuel_name + ")";
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
            g.DrawRectangle(p, new Rectangle(FuelTextBox.Location.X - variance, FuelTextBox.Location.Y - variance, FuelTextBox.Width + variance, FuelTextBox.Height + variance));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if ((!FuelTextBox.Enabled || (FuelTextBox.Enabled && !String.IsNullOrWhiteSpace(FuelTextBox.Text))) && !String.IsNullOrWhiteSpace(textBox1.Text))
            {
                name = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                FuelTextBox.Enabled = true;
            else
                FuelTextBox.Enabled = false;
        }
    }
}
