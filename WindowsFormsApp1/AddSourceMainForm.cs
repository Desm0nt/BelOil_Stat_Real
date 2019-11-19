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
        public AddSourceMainForm()
        {
            InitializeComponent();
            this.rukTextBox.AutoSize = false;
            this.rukTextBox.Size = new Size(224, 18);
            this.otvTextBox.AutoSize = false;
            this.otvTextBox.Size = new Size(224, 18);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceObjectForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceFuelsForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
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
            btn.Size = new Size(25, rukTextBox.ClientSize.Height + 2);
            btn.Location = new Point(rukTextBox.ClientSize.Width - btn.Width, -1);
            btn.Cursor = Cursors.Default;
            btn.Text = "...";
            btn2.BackColor = Color.White;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.FlatAppearance.BorderSize = 0;
            btn2.Size = new Size(25, otvTextBox.ClientSize.Height + 2);
            btn2.Location = new Point(otvTextBox.ClientSize.Width - btn2.Width, -1);
            btn2.Cursor = Cursors.Default;
            btn2.Text = "...";
            btn2.Click += btn2_Click;
            btn.Click += btn_Click;
            rukTextBox.Controls.Add(btn);
            SendMessage(rukTextBox.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
            otvTextBox.Controls.Add(btn2);
            SendMessage(otvTextBox.Handle, 0xd3, (IntPtr)2, (IntPtr)(btn2.Width << 16));
            base.OnLoad(e);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel pan = (Panel)sender;
            //ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207))))), ButtonBorderStyle.Solid);
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(rukTextBox.Location.X - variance, rukTextBox.Location.Y - variance, rukTextBox.Width + variance, rukTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(otvTextBox.Location.X - variance, otvTextBox.Location.Y - variance, otvTextBox.Width + variance, otvTextBox.Height + variance));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
