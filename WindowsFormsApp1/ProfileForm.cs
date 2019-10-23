﻿using ComponentFactory.Krypton.Toolkit;
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
            LoadCoeff();
            LoadFuels();
            LoadGen();
            LoadRec();
            LoadSend();
            this.Width = 1516;
            kryptonHeaderGroup2.ValuesPrimary.Heading = kryptonNavigator1.SelectedPage.Text;
            kryptonHeaderGroup2.ValuesPrimary.Image = kryptonNavigator1.SelectedPage.ImageSmall;
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

        private void LoadFuels()
        {
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            var fueltList = dbOps.GetProfFuelsList(cur_org_id, profData.Num, profData.Month, profData.Year);

            kryptonOutlookGrid7.ClearInternalRows();
            kryptonOutlookGrid7.ClearGroups();
            kryptonOutlookGrid7.RowHeadersWidth = 10;


            kryptonOutlookGrid7.GroupBox = kryptonOutlookGridGroupBox2;
            kryptonOutlookGrid7.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[7];
            columnsToAdd[0] = kryptonOutlookGrid7.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid7.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid7.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid7.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid7.Columns[4];
            columnsToAdd[5] = kryptonOutlookGrid7.Columns[5];
            columnsToAdd[6] = kryptonOutlookGrid7.Columns[6];

            //kryptonOutlookGrid7.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid7.AddInternalColumn(kryptonOutlookGrid7.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);

            kryptonOutlookGrid7.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid7.SuspendLayout();
            //kryptonOutlookGrid7.ClearInternalRows();
            kryptonOutlookGrid7.FillMode = FillMode.GROUPSONLY;
            string group = "";
            foreach (var fuel in fueltList)
            {
                row = new OutlookGridRow();
                group = fuel.fuel_id.ToString();
                row.CreateCells(kryptonOutlookGrid7, new object[] { fuel.id, fuel.fuel_id, new TextAndImage(fuel.name.ToString(), GetFuelFlag(Int32.Parse(group[0].ToString()))), fuel.unit, fuel.Qn, fuel.B_y, fuel.trade});
                l.Add(row);
            }

            kryptonOutlookGrid7.ResumeLayout();
            kryptonOutlookGrid7.AssignRows(l);
            kryptonOutlookGrid7.ForceRefreshGroupBox();
            kryptonOutlookGrid7.Fill();
        }

        private void LoadCoeff()
        {
            var factorData = dbOps.GetProfFactorList();
            kryptonOutlookGrid8.ClearInternalRows();
            kryptonOutlookGrid8.ClearGroups();
            kryptonOutlookGrid8.RowHeadersVisible = false;


            kryptonOutlookGrid8.GroupBox = kryptonOutlookGridGroupBox3;
            kryptonOutlookGrid8.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[4];
            columnsToAdd[0] = kryptonOutlookGrid8.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid8.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid8.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid8.Columns[3];

            //kryptonOutlookGrid7.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid8.AddInternalColumn(kryptonOutlookGrid8.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid8.AddInternalColumn(kryptonOutlookGrid8.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid8.AddInternalColumn(kryptonOutlookGrid8.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid8.AddInternalColumn(kryptonOutlookGrid8.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);

            kryptonOutlookGrid8.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid8.SuspendLayout();
            //kryptonOutlookGrid9.ClearInternalRows();
            kryptonOutlookGrid8.FillMode = FillMode.GROUPSONLY;

            foreach (var factor in factorData)
            {
                string monthf = factor.month.ToString();
                if (monthf.Length < 2)
                    monthf = "0" + monthf;                   
                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid8, new object[] { factor.id, new TextAndImage("01." + monthf + "." + factor.year, Properties.Resources.fdate), factor.gkal, factor.kVt  });
                l.Add(row);
            }

            kryptonOutlookGrid8.ResumeLayout();
            kryptonOutlookGrid8.AssignRows(l);
            kryptonOutlookGrid8.ForceRefreshGroupBox();
            kryptonOutlookGrid8.Fill();
        }

        private void LoadGen()
        {
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            var genList = dbOps.GetProfGenList(cur_org_id, profData.Num, profData.Month, profData.Year);

            kryptonOutlookGrid10.ClearInternalRows();
            kryptonOutlookGrid10.ClearGroups();
            kryptonOutlookGrid10.RowHeadersWidth = 10;

            kryptonOutlookGrid10.GroupBox = kryptonOutlookGridGroupBox4;
            kryptonOutlookGrid10.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[4];
            columnsToAdd[0] = kryptonOutlookGrid10.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid10.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid10.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid10.Columns[3];
            //kryptonOutlookGrid10.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[3], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);

            kryptonOutlookGrid10.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid10.SuspendLayout();
            //kryptonOutlookGrid10.ClearInternalRows();
            kryptonOutlookGrid10.FillMode = FillMode.GROUPSONLY;

            string group = "";
            foreach (var gen in genList)
            {
                row = new OutlookGridRow();
                group = gen.Fuel_id.ToString();
                row.CreateCells(kryptonOutlookGrid10, new object[] { gen.Id, new TextAndImage(gen.Obj_name, Properties.Resources.box), new TextAndImage(gen.Fuel_name, GetFuelFlag(Int32.Parse(group[0].ToString()))), new TextAndImage(gen.Type.ToString(), GetFlag(gen.Type)) });
                l.Add(row);
            }

            kryptonOutlookGrid10.ResumeLayout();
            kryptonOutlookGrid10.AssignRows(l);
            kryptonOutlookGrid10.ForceRefreshGroupBox();
            kryptonOutlookGrid10.Fill();
        }
        private void LoadRec()
        {
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            var genList = dbOps.GetProfRecList(cur_org_id, profData.Num, profData.Month, profData.Year);

            kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.ClearGroups();
            kryptonOutlookGrid1.RowHeadersWidth = 10;

            kryptonOutlookGrid1.GroupBox = kryptonOutlookGridGroupBox4;
            kryptonOutlookGrid1.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[5];
            columnsToAdd[0] = kryptonOutlookGrid1.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid1.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid1.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[3];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[4];
            //kryptonOutlookGrid10.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[3], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.ShowLines = false;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            //kryptonOutlookGrid10.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GROUPSONLY;

            foreach (var gen in genList)
            {
                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid1, new object[] { gen.Id, gen.Id_org, new TextAndImage(gen.Org_name, GetRecSendFlag(gen.Head)), new TextAndImage(gen.Type.ToString(), GetFlag(gen.Type)), gen.Head });
                l.Add(row);
            }

            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();
        }
        private void LoadSend()
        {
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            var genList = dbOps.GetProfSendList(cur_org_id, profData.Num, profData.Month, profData.Year);

            kryptonOutlookGrid6.ClearInternalRows();
            kryptonOutlookGrid6.ClearGroups();
            kryptonOutlookGrid6.RowHeadersWidth = 10;

            kryptonOutlookGrid6.GroupBox = kryptonOutlookGridGroupBox4;
            kryptonOutlookGrid6.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[5];
            columnsToAdd[0] = kryptonOutlookGrid6.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid6.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid6.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid6.Columns[3];
            columnsToAdd[3] = kryptonOutlookGrid6.Columns[4];
            //kryptonOutlookGrid60.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid6.AddInternalColumn(kryptonOutlookGrid6.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid6.AddInternalColumn(kryptonOutlookGrid6.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid6.AddInternalColumn(kryptonOutlookGrid6.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid6.AddInternalColumn(kryptonOutlookGrid6.Columns[3], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid6.AddInternalColumn(kryptonOutlookGrid6.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid6.ShowLines = false;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid6.SuspendLayout();
            //kryptonOutlookGrid60.ClearInternalRows();
            kryptonOutlookGrid6.FillMode = FillMode.GROUPSONLY;

            foreach (var gen in genList)
            {
                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid6, new object[] { gen.Id, gen.Id_org, new TextAndImage(gen.Org_name, GetRecSendFlag(gen.Head)), new TextAndImage(gen.Type.ToString(), GetFlag(gen.Type)), gen.Head });
                l.Add(row);
            }

            kryptonOutlookGrid6.ResumeLayout();
            kryptonOutlookGrid6.AssignRows(l);
            kryptonOutlookGrid6.ForceRefreshGroupBox();
            kryptonOutlookGrid6.Fill();
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
        private Image GetRecSendFlag(int type)
        {
            switch (type)
            {
                case 0:
                    return Properties.Resources.org2;
                default:
                    return Properties.Resources.org1;
            }
        }
        private Image GetFuelFlag(int type)
        {
            switch (type)
            {
                case 1:
                    return Properties.Resources.t1;
                case 2:
                    return Properties.Resources.t2;
                case 3:
                    return Properties.Resources.t3;
                case 4:
                    return Properties.Resources.t4;
                case 5:
                    return Properties.Resources.t5;
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


        private void kryptonNavigator1_SelectedPageChanged(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = (ComponentFactory.Krypton.Navigator.KryptonNavigator)sender;
            kryptonHeaderGroup2.ValuesPrimary.Heading = nav.SelectedPage.Text;
            kryptonHeaderGroup2.ValuesPrimary.Image = nav.SelectedPage.ImageSmall;
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
