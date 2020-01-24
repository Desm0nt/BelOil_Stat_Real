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
        public int obj_id = -1;
        public string name;
        public string fuel_name = "";
        public AddOrgNormSupportForm(string norm_name, int type, int id_obj)
        {
            InitializeComponent();
            textBox1.Text = norm_name;
            obj_id = id_obj;
            this.FuelTextBox.AutoSize = false;
            this.FuelTextBox.Size = new Size(224, 18);
            this.ObjTextBox.AutoSize = false;
            this.ObjTextBox.Size = new Size(224, 18);
            if ( type != 1)
            {
                checkBox1.Enabled = false;
            }
            if (obj_id != -1)
            {
                checkBox2.Checked = true;
                ObjTextBox.Text = dbOps.GetObjName(obj_id);
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

        private void btn3_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceObjectForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK)
            {
                obj_id = myForm.obj_id;
                //obj_name = myForm.obj_name;
                ObjTextBox.Text = myForm.obj_name;
            }

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnLoad(EventArgs e)
        {
            var btn = new Button();
            var btn2 = new Button();
            var btn3 = new Button();
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
            btn3.BackColor = Color.White;
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.FlatAppearance.BorderSize = 0;
            btn3.Size = new Size(25, ObjTextBox.ClientSize.Height + 2);
            btn3.Location = new Point(ObjTextBox.ClientSize.Width - btn3.Width, -1);
            btn3.Cursor = Cursors.Default;
            btn3.Text = "...";
            btn3.Click += btn3_Click;
            ObjTextBox.Controls.Add(btn3);
            SendMessage(ObjTextBox.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn3.Width << 16));
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
            g.DrawRectangle(p, new Rectangle(ObjTextBox.Location.X - variance, ObjTextBox.Location.Y - variance, ObjTextBox.Width + variance, ObjTextBox.Height + variance));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if ((!FuelTextBox.Enabled || (FuelTextBox.Enabled && !String.IsNullOrWhiteSpace(FuelTextBox.Text))) && !String.IsNullOrWhiteSpace(textBox1.Text) && (!ObjTextBox.Enabled || (ObjTextBox.Enabled && !String.IsNullOrWhiteSpace(ObjTextBox.Text))) && !String.IsNullOrWhiteSpace(textBox1.Text))
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                ObjTextBox.Enabled = true;
            else
                ObjTextBox.Enabled = false;
        }
    }
}
