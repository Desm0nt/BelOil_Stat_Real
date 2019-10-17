using ComponentFactory.Krypton.Toolkit;
using KryptonOutlookGrid.Classes;
using KryptonOutlookGrid.CustomColumns;
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
    public partial class ProfileForm : KryptonForm
    {
        int cur_org_id;
        int cyear, cmonth;

        public ProfileForm(int curyear, int curmonth)
        {
            InitializeComponent();
            cur_org_id = CurrentData.UserData.Id_org;
            cyear = curyear;
            cmonth = curmonth;
            LoadObjects();
        }

        private void LoadObjects()
        {
            List<ObjectTable> objectList = new List<ObjectTable>();
            objectList = dbOps.GetObjList(cur_org_id);
            treeView1.Nodes.Clear();
            for (int i = 0; i < objectList.Count; i++)
            {
                TreeNode child = new TreeNode();
                child.Text = objectList[i].Name;
                child.Tag = objectList[i].Id;
                child.ImageIndex = 0;
                treeView1.Nodes.Add(child);
            }
            treeView1.ExpandAll();

        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Int32.Parse(e.Node.Tag.ToString()) != -1)
            {
                //pictureBox1.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + pics[Int32.Parse(e.Node.Tag.ToString())].path);
                //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



        /// <summary>
        /// Отрисовка элементов, их ресайз, рамки, сокрытия и прочее подобное
        /// </summary>
        #region Рисование различной фигни
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.unpTextBox.AutoSize = false;
            this.okpoTextBox.AutoSize = false;
            this.nameTextBox.AutoSize = false;
            this.fnameTextBox.AutoSize = false;
            this.adressTextbox.AutoSize = false;
            this.mailTextBox.AutoSize = false;
            this.unpTextBox.Height = 18;
            this.okpoTextBox.Height = 18;
            this.nameTextBox.Height = 18;
            this.fnameTextBox.Height = 18;
            this.adressTextbox.Height = 18;
            this.mailTextBox.Height = 18;
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(nameTextBox.Location.X - variance, nameTextBox.Location.Y - variance, nameTextBox.Width + variance, nameTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(fnameTextBox.Location.X - variance, fnameTextBox.Location.Y - variance, fnameTextBox.Width + variance, fnameTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(adressTextbox.Location.X - variance, adressTextbox.Location.Y - variance, adressTextbox.Width + variance, adressTextbox.Height + variance));
            g.DrawRectangle(p, new Rectangle(mailTextBox.Location.X - variance, mailTextBox.Location.Y - variance, mailTextBox.Width + variance, mailTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(okpoTextBox.Location.X - variance, okpoTextBox.Location.Y - variance, okpoTextBox.Width + variance, okpoTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(unpTextBox.Location.X - variance, unpTextBox.Location.Y - variance, unpTextBox.Width + variance, unpTextBox.Height + variance));
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            this.rukTextBox.AutoSize = false;
            this.rukTextBox.Size = new Size(224, 18);
            this.otvTextBox.AutoSize = false;
            this.otvTextBox.Size = new Size(224, 18);
            Panel pan = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207))))), ButtonBorderStyle.Solid);
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(rukTextBox.Location.X - variance, rukTextBox.Location.Y - variance, rukTextBox.Width + variance, rukTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(otvTextBox.Location.X - variance, otvTextBox.Location.Y - variance, otvTextBox.Width + variance, otvTextBox.Height + variance));
        }

        private void kryptonOutlookGrid1_Resize(object sender, EventArgs e)
        {
            int PreferredTotalWidth = 0;
            //Calculate the total preferred width
            foreach (DataGridViewColumn c in kryptonOutlookGrid9.Columns)
            {
                PreferredTotalWidth += Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
            }

            if (kryptonOutlookGrid9.Width > PreferredTotalWidth)
            {
                kryptonOutlookGrid9.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                kryptonOutlookGrid9.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            else
            {
                kryptonOutlookGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                foreach (DataGridViewColumn c in kryptonOutlookGrid9.Columns)
                {
                    c.Width = Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
                }
            }
        }
        #endregion
    }
}
