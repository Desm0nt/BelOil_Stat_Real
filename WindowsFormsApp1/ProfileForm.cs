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
            LoadAllNorms();
            LoadTypedNorms(1, kryptonOutlookGrid4);
            LoadTypedNorms(2, kryptonOutlookGrid3);
            LoadTypedNorms(3, kryptonOutlookGrid2);
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

        private void LoadAllNorms()
        {
            List<ProfNormTable> normList = new List<ProfNormTable>();
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            normList = dbOps.GetProfNormList(cur_org_id, profData.Num, profData.Month, profData.Year, toolStripTextBox2.Text, toolStripTextBox2.Text);

            kryptonOutlookGrid9.ClearInternalRows();
            kryptonOutlookGrid9.ClearGroups();
            kryptonOutlookGrid9.RowHeadersWidth = 10;

            kryptonOutlookGrid9.GroupBox = kryptonOutlookGridGroupBox1;
            kryptonOutlookGrid9.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[8];
            columnsToAdd[0] = kryptonOutlookGrid9.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid9.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid9.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid9.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid9.Columns[4];
            columnsToAdd[5] = kryptonOutlookGrid9.Columns[5];
            columnsToAdd[6] = kryptonOutlookGrid9.Columns[6];
            columnsToAdd[7] = kryptonOutlookGrid9.Columns[7];
            //kryptonOutlookGrid9.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid9.Columns[0].Visible = false;
            kryptonOutlookGrid9.Columns[7].Visible = false;

            kryptonOutlookGrid9.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid9.SuspendLayout();
            //kryptonOutlookGrid9.ClearInternalRows();
            kryptonOutlookGrid9.FillMode = FillMode.GROUPSONLY;

            foreach (var product in normList)
            {
                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid9, new object[] { product.Id, product.Code, product.Name, product.Unit, product.nUnit, product.s111, product.s112, new TextAndImage(product.type.ToString(), GetFlag(product.type)) });
                l.Add(row);
            }

            kryptonOutlookGrid9.ResumeLayout();
            kryptonOutlookGrid9.AssignRows(l);
            kryptonOutlookGrid9.ForceRefreshGroupBox();
            kryptonOutlookGrid9.Fill();
        }

        private void LoadTypedNorms(int type, KryptonOutlookGrid.Classes.KryptonOutlookGrid grid)
        {
            List<ProfNormTable> normList = new List<ProfNormTable>();
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            normList = dbOps.GetProfNormTypedList(cur_org_id, profData.Num, profData.Month, profData.Year, toolStripTextBox2.Text, toolStripTextBox2.Text, type);

            grid.ClearInternalRows();
            grid.ClearGroups();
            grid.RowHeadersWidth = 10;

            grid.GroupBox = kryptonOutlookGridGroupBox1;
            grid.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[8];
            columnsToAdd[0] = grid.Columns[0];
            columnsToAdd[1] = grid.Columns[1];
            columnsToAdd[2] = grid.Columns[2];
            columnsToAdd[3] = grid.Columns[3];
            columnsToAdd[4] = grid.Columns[4];
            columnsToAdd[5] = grid.Columns[5];
            columnsToAdd[6] = grid.Columns[6];
            columnsToAdd[7] = grid.Columns[7];
            //grid.Columns.AddRange(columnsToAdd);

            grid.AddInternalColumn(grid.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            grid.AddInternalColumn(grid.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            grid.Columns[0].Visible = false;
            grid.Columns[7].Visible = false;

            grid.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            grid.SuspendLayout();
            //grid.ClearInternalRows();
            grid.FillMode = FillMode.GROUPSONLY;

            foreach (var product in normList)
            {
                row = new OutlookGridRow();
                row.CreateCells(grid, new object[] { product.Id, product.Code, product.Name, product.Unit, product.nUnit, product.s111, product.s112, new TextAndImage(product.type.ToString(), GetFlag(product.type)) });
                l.Add(row);
            }

            grid.ResumeLayout();
            grid.AssignRows(l);
            grid.ForceRefreshGroupBox();
            grid.Fill();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Int32.Parse(e.Node.Tag.ToString()) != -1)
            {
                //pictureBox1.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + pics[Int32.Parse(e.Node.Tag.ToString())].path);
                //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private Image GetFlag(int type)
        {
            switch (type)
            {
                case 1:
                    return Properties.Resources._1;
                case 2:
                    return Properties.Resources._2;
                case 3:
                    return Properties.Resources._3;
                default:
                    return null;
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
            KryptonOutlookGrid.Classes.KryptonOutlookGrid grid = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            int PreferredTotalWidth = 0;
            //Calculate the total preferred width
            foreach (DataGridViewColumn c in grid.Columns)
            {
                PreferredTotalWidth += Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
            }

            if (grid.Width > PreferredTotalWidth)
            {
                grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            else
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                foreach (DataGridViewColumn c in grid.Columns)
                {
                    c.Width = Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
                }
            }
        }
        #endregion
    }
}
