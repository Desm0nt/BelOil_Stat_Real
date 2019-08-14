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

namespace WindowsFormsApp1
{
    public partial class ProductAddForm : KryptonForm
    {
        public ProductAddForm()
        {
            InitializeComponent();
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(74, 21);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(74, 21);
        }

        public ProductAddForm(DataTables.ProductTable table)
        {
            InitializeComponent();
            var a = table;
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new System.Drawing.Size(74, 21);
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new System.Drawing.Size(74, 21);
            kryptonNumericUpDown1.Value = table.Id;
            kryptonNumericUpDown2.Value = table.Code;
            kryptonTextBox2.Text = table.Name;
            label6.Text = table.Unit;
            label7.Text = table.nUnit;
            kryptonCheckBox1.Checked = table.s111;
            kryptonCheckBox2.Checked = table.s112;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello world");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello world2");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(textBox1.Location.X - variance, textBox1.Location.Y - variance, textBox1.Width + variance, textBox1.Height + variance));
            g.DrawRectangle(p, new Rectangle(textBox2.Location.X - variance, textBox2.Location.Y - variance, textBox2.Width + variance, textBox2.Height + variance));

        }
    }
}
