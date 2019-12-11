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
    public struct ListObjects
    {
        public int Id;
        public string Name;
    }

    public partial class ProfileForm : KryptonForm
    {
        int cur_org_id;
        int cyear, cmonth;
        bool edited = false;
        List<int> objToDisactivate = new List<int>();
        public static List<ListObjects> elList;
        List<DataTables.ObjectTable> objectsList;

        public ProfileForm(int curyear, int curmonth, int id_org)
        {
            InitializeComponent();
            this.rukTextBox.AutoSize = false;
            this.rukTextBox.Size = new Size(224, 18);
            this.otvTextBox.AutoSize = false;
            this.otvTextBox.Size = new Size(224, 18);
            this.Text = "Профиль организации " + dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            cur_org_id = id_org;
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
            LoadCompData();
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
                child.ToolTipText = objectList[i].FullName;
                child.ImageIndex = 0;
                treeView1.Nodes.Add(child);
            }
            treeView1.ExpandAll();

        }

        private void LoadCompData()
        {
            var compData = dbOps.GetProfCompList(cur_org_id);
            nameTextBox.Text = compData.Name;
            fnameTextBox.Text = compData.Name;
            rukTextBox.Text = compData.Head;
            otvTextBox.Text = compData.Impl;
            kryptonCheckBox1.Checked = compData.doch;

        }

        private void LoadAllNorms()
        {
            objectsList = dbOps.GetObjList(cur_org_id);
            elList = new List<ListObjects>();
            foreach (var a in objectsList)
                elList.Add(new ListObjects { Id = a.Id, Name = a.Name});
            elList.Add(new ListObjects { Id = -1, Name = "Общие по организации" });

            List<ProfNormTable> normList = new List<ProfNormTable>();
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            normList = dbOps.GetProfNormList(cur_org_id, profData.Num, profData.Month, profData.Year, toolStripTextBox2.Text, toolStripTextBox2.Text);

            kryptonOutlookGrid9.ClearInternalRows();
            kryptonOutlookGrid9.ClearGroups();
            kryptonOutlookGrid9.RowHeadersWidth = 10;

            kryptonOutlookGrid9.GroupBox = kryptonOutlookGridGroupBox1;
            kryptonOutlookGrid9.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[14];
            columnsToAdd[0] = kryptonOutlookGrid9.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid9.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid9.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid9.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid9.Columns[4];
            columnsToAdd[5] = kryptonOutlookGrid9.Columns[5];
            columnsToAdd[6] = kryptonOutlookGrid9.Columns[6];
            columnsToAdd[7] = kryptonOutlookGrid9.Columns[7];
            columnsToAdd[8] = kryptonOutlookGrid9.Columns[8];
            columnsToAdd[9] = kryptonOutlookGrid9.Columns[9];
            columnsToAdd[10] = kryptonOutlookGrid9.Columns[10];
            columnsToAdd[11] = kryptonOutlookGrid9.Columns[11];
            columnsToAdd[12] = kryptonOutlookGrid9.Columns[12];
            columnsToAdd[13] = kryptonOutlookGrid9.Columns[13];

            //kryptonOutlookGrid9.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[8], new OutlookGridObjectGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[9], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[10], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[11], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[12], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid9.AddInternalColumn(kryptonOutlookGrid9.Columns[13], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
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
                row.CreateCells(kryptonOutlookGrid9, new object[] 
                {
                    product.Id,
                    product.Code,
                    product.name_with_fuel,
                    product.Unit,
                    product.nUnit,
                    product.s111,
                    product.s112,
                    new TextAndImage(product.type.ToString(), GetFlag(product.type)),
                    new TextAndImage(product.id_obj.ToString(), GetFlagObj(product.id_obj)),
                    product.Id_local,
                    product.real_name,
                    product.Name,
                    product.id_fuel,
                    product.Id_prod });
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
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[14];
            columnsToAdd[0] = grid.Columns[0];
            columnsToAdd[1] = grid.Columns[1];
            columnsToAdd[2] = grid.Columns[2];
            columnsToAdd[3] = grid.Columns[3];
            columnsToAdd[4] = grid.Columns[4];
            columnsToAdd[5] = grid.Columns[5];
            columnsToAdd[6] = grid.Columns[6];
            columnsToAdd[7] = grid.Columns[7];
            columnsToAdd[8] = grid.Columns[8];
            columnsToAdd[9] = grid.Columns[9];
            columnsToAdd[10] = grid.Columns[10];
            columnsToAdd[11] = grid.Columns[11];
            columnsToAdd[12] = grid.Columns[12];
            columnsToAdd[13] = grid.Columns[13];
            //grid.Columns.AddRange(columnsToAdd);

            grid.AddInternalColumn(grid.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            grid.AddInternalColumn(grid.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            grid.AddInternalColumn(grid.Columns[8], new OutlookGridObjectGroup(null), SortOrder.Ascending, 1, -1);
            grid.AddInternalColumn(grid.Columns[9], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[10], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[11], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[12], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(grid.Columns[13], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
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
                row.CreateCells(grid, new object[] { product.Id, product.Code, product.name_with_fuel, product.Unit, product.nUnit, product.s111, product.s112, new TextAndImage(product.type.ToString(), GetFlag(product.type)), new TextAndImage(product.id_obj.ToString(), GetFlagObj(product.id_obj)), product.Id_local, product.real_name, product.Name, product.id_fuel, product.Id_prod });
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
                row.CreateCells(kryptonOutlookGrid7, new object[] { fuel.id, fuel.fuel_id, new TextAndImage(fuel.name.ToString(), GetFuelFlag(Int32.Parse(group[0].ToString()))), fuel.unit, fuel.Qn, fuel.B_y, fuel.trade });
                l.Add(row);
            }

            kryptonOutlookGrid7.ResumeLayout();
            kryptonOutlookGrid7.AssignRows(l);
            kryptonOutlookGrid7.ForceRefreshGroupBox();
            kryptonOutlookGrid7.Fill();
        }

        private void LoadCoeff()
        {
            try
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
                    row.CreateCells(kryptonOutlookGrid8, new object[] { factor.id, new TextAndImage("01." + monthf + "." + factor.year, Properties.Resources.fdate), factor.gkal, factor.kVt });
                    l.Add(row);
                }

                kryptonOutlookGrid8.ResumeLayout();
                kryptonOutlookGrid8.AssignRows(l);
                kryptonOutlookGrid8.ForceRefreshGroupBox();
                kryptonOutlookGrid8.Fill();
            }
            catch (Exception ex) { }
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
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[6];
            columnsToAdd[0] = kryptonOutlookGrid10.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid10.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid10.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid10.Columns[3];
            columnsToAdd[3] = kryptonOutlookGrid10.Columns[4];
            columnsToAdd[3] = kryptonOutlookGrid10.Columns[5];
            //kryptonOutlookGrid10.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid10.AddInternalColumn(kryptonOutlookGrid10.Columns[5], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);

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
                row.CreateCells(kryptonOutlookGrid10, new object[] { gen.Id, gen.Obj_id, gen.Fuel_id, new TextAndImage(gen.Obj_name, Properties.Resources.box), new TextAndImage(gen.Fuel_name, GetFuelFlag(Int32.Parse(group[0].ToString()))), new TextAndImage(gen.Type.ToString(), GetFlag(gen.Type)) });
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
        private Image GetFlagObj(int type)
        {
            switch (type)
            {
                case -1:
                    return Properties.Resources.star;
                default:
                    return Properties.Resources.box;
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

        private void myForm_FormClosed(object sender, EventArgs e)
        {
            LoadCompData();
            LoadObjects();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var myForm = new SelectPersonForm(true);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var myForm = new SelectPersonForm(false);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
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
            Panel pan = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207))))), ButtonBorderStyle.Solid);
            Pen p = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222))))));
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(rukTextBox.Location.X - variance, rukTextBox.Location.Y - variance, rukTextBox.Width + variance, rukTextBox.Height + variance));
            g.DrawRectangle(p, new Rectangle(otvTextBox.Location.X - variance, otvTextBox.Location.Y - variance, otvTextBox.Width + variance, otvTextBox.Height + variance));
        }

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

        private void kryptonNavigator1_SelectedPageChanged(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = (ComponentFactory.Krypton.Navigator.KryptonNavigator)sender;
            kryptonHeaderGroup2.ValuesPrimary.Heading = nav.SelectedPage.Text;
            kryptonHeaderGroup2.ValuesPrimary.Image = nav.SelectedPage.ImageSmall;
        }

        private void kryptonOutlookGrid1_Resize2(object sender, EventArgs e)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid grid = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            int PreferredTotalWidth = 0;
            //Calculate the total preferred width
            foreach (DataGridViewColumn c in grid.Columns)
            {
                PreferredTotalWidth += Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
            }

            if (grid.Width < PreferredTotalWidth)
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var myForm = new AddOrgObjectForm(cur_org_id);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                Console.WriteLine("No tree node selected.");
            else
            {
                var myForm = new AddOrgObjectForm(Int32.Parse(treeView1.SelectedNode.Tag.ToString()), treeView1.SelectedNode.Text.ToString(), treeView1.SelectedNode.ToolTipText.ToString(), cur_org_id);
                myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
                myForm.ShowDialog();
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var myForm = new AddOrgObjectForm(Int32.Parse(e.Node.Tag.ToString()), e.Node.Text.ToString(), e.Node.ToolTipText.ToString(), cur_org_id);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void AddFactorButton_Click(object sender, EventArgs e)
        {
            dbOps.AddNewFactor(DateTime.Now.Month, DateTime.Now.Year, 0, 0);
            LoadCoeff();
        }

        private void kryptonOutlookGrid8_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            var row = dataGridView.Rows[e.RowIndex];
            int id = (int)row.Cells[0].Value;
            float gkal = float.Parse(row.Cells[2].Value.ToString());
            float kvch = float.Parse(row.Cells[3].Value.ToString());
            dbOps.UpdateFactor(gkal, kvch, id);
            LoadCoeff();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            var ind = kryptonOutlookGrid8.CurrentCell.RowIndex;
            if (ind >= 0)
            {
                if (kryptonOutlookGrid8.Rows[ind].Cells[1].Value != null)
                    dbOps.DeleteFactor(Int32.Parse(kryptonOutlookGrid8.Rows[ind].Cells[0].Value.ToString()));
            }
            LoadCoeff();
        }

        private void AddFuelButton_Click(object sender, EventArgs e)
        {
            var myForm = new AddOrgFuelForm();
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK)
            {
                OutlookGridRow row = new OutlookGridRow();
                List<OutlookGridRow> l = new List<OutlookGridRow>();
                foreach (OutlookGridRow a in kryptonOutlookGrid7.Rows)
                {
                    if (a.Cells[1].Value != null)
                        l.Add(a);
                }
                kryptonOutlookGrid7.SuspendLayout();
                kryptonOutlookGrid7.ClearInternalRows();
                kryptonOutlookGrid7.FillMode = FillMode.GROUPSONLY;

                string group = "";
                row = new OutlookGridRow();
                group = myForm.FuelRow.fuel_id.ToString();
                row.CreateCells(kryptonOutlookGrid7, new object[] { myForm.FuelRow.id, myForm.FuelRow.fuel_id, new TextAndImage(myForm.FuelRow.name.ToString(), GetFuelFlag(Int32.Parse(group[0].ToString()))), myForm.FuelRow.unit, myForm.FuelRow.Qn, myForm.FuelRow.B_y, false });
                l.Add(row);

                kryptonOutlookGrid7.ResumeLayout();
                kryptonOutlookGrid7.AssignRows(l);
                kryptonOutlookGrid7.ForceRefreshGroupBox();
                kryptonOutlookGrid7.Fill();
                edited = true;
            }
        }
        private void RemoveFuelButton_Click(object sender, EventArgs e)
        {
            if (kryptonOutlookGrid7.SelectedRows.Count > 0)
            {
                if (kryptonOutlookGrid7.SelectedRows[0].Cells[1].Value != null)
                {
                    kryptonOutlookGrid7.Rows.RemoveAt(kryptonOutlookGrid7.SelectedRows[0].Index);
                    edited = true;
                }
            }
        }

        private void SendRecievAddButton_Click(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = kryptonNavigator3;
            int index = Int32.Parse(nav.SelectedPage.Tag.ToString());
            if (index == 1 || index == 2)
            {
                var myForm = new AddOrgSendRecieveForm(index);
                myForm.ShowDialog();
                if (myForm.DialogResult == DialogResult.OK)
                {
                    OutlookGridRow row = new OutlookGridRow();
                    List<OutlookGridRow> l = new List<OutlookGridRow>();
                    KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                    if (index == 1)
                        newGrid = kryptonOutlookGrid1;
                    else if (index == 2)
                        newGrid = kryptonOutlookGrid6;
                    foreach (OutlookGridRow a in newGrid.Rows)
                    {
                        if (a.Cells[1].Value != null)
                            l.Add(a);
                    }
                    newGrid.SuspendLayout();
                    newGrid.ClearInternalRows();
                    newGrid.FillMode = FillMode.GROUPSONLY;

                    string group = "";
                    row = new OutlookGridRow();
                    row.CreateCells(newGrid, new object[] { myForm.SendRecTable.Id, myForm.SendRecTable.Id_org, new TextAndImage(myForm.SendRecTable.Org_name, GetRecSendFlag(myForm.SendRecTable.Head)), new TextAndImage(myForm.SendRecTable.Type.ToString(), GetFlag(myForm.SendRecTable.Type)), myForm.SendRecTable.Head });
                    l.Add(row);

                    newGrid.ResumeLayout();
                    newGrid.AssignRows(l);
                    newGrid.ForceRefreshGroupBox();
                    newGrid.Fill();
                    edited = true;
                }
            }

        }
        private void SendRecievRemoveButton_Click(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = kryptonNavigator3;
            int index = Int32.Parse(nav.SelectedPage.Tag.ToString());
            if (index == 1 || index == 2)
            {
                KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                if (index == 1)
                    newGrid = kryptonOutlookGrid1;
                else if (index == 2)
                    newGrid = kryptonOutlookGrid6;
                if (newGrid.SelectedRows.Count > 0)
                {
                    if (newGrid.SelectedRows[0].Cells[1].Value != null)
                    {
                        newGrid.Rows.RemoveAt(newGrid.SelectedRows[0].Index);
                        edited = true;
                    }
                }
            }
        }

        private KryptonOutlookGrid.Classes.KryptonOutlookGrid GetGridByType(int type)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid Grid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
            if (type == 0)
                Grid = kryptonOutlookGrid9;
            else if (type == 1)
                Grid = kryptonOutlookGrid4;
            else if (type == 2)
                Grid = kryptonOutlookGrid3;
            else if (type == 3)
                Grid = kryptonOutlookGrid2;
            return Grid;
        }

        private void AddNormButton_Click(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = kryptonNavigator2;
            int index = Int32.Parse(nav.SelectedPage.Tag.ToString());
            if (index == 1 || index == 2 || index == 3 || index == 4)
            {
                KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid2 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                newGrid = GetGridByType(index - 1);

                var myForm = new AddOrgNormForm(index, cur_org_id);
                if (newGrid.SelectedRows.Count > 0)
                {
                    if (newGrid.SelectedRows[0].Cells[1].Value != null)
                    {
                        if (index != 1)
                            myForm = new AddOrgNormForm(index, (index - 1), Int32.Parse(newGrid.SelectedRows[0].Cells[8].Value.ToString()), cur_org_id);
                        else
                            myForm = new AddOrgNormForm(index, Int32.Parse(newGrid.SelectedRows[0].Cells[7].Value.ToString()), Int32.Parse(newGrid.SelectedRows[0].Cells[8].Value.ToString()), cur_org_id);
                    }
                }
                myForm.ShowDialog();
                if (myForm.DialogResult == DialogResult.OK)
                {
                    OutlookGridRow row = new OutlookGridRow();
                    List<OutlookGridRow> l = new List<OutlookGridRow>();
                    List<OutlookGridRow> l2 = new List<OutlookGridRow>();
                    if (index != 1)
                        newGrid2 = kryptonOutlookGrid9;
                    else if (index == 1)
                        newGrid2 = GetGridByType(myForm.NormTable.type);

                    foreach (OutlookGridRow a in newGrid2.Rows)
                    {
                        if (a.Cells[1].Value != null)
                            l2.Add(a);
                    }
                    string tmp_id = myForm.NormTable.Id + DateTime.Now.ToString();
                    newGrid2.SuspendLayout();
                    newGrid2.ClearInternalRows();
                    newGrid2.FillMode = FillMode.GROUPSONLY;
                    row = new OutlookGridRow();
                    string obj_for_local ="00";
                    if (myForm.NormTable.id_obj != -1)
                    {
                        obj_for_local = myForm.NormTable.id_obj.ToString();
                    }            
                    string code_for_local = myForm.NormTable.Code.ToString();
                    int lenght = obj_for_local.Length;
                    for (int i=lenght; i<2; i++)
                    {
                        obj_for_local = "0" + obj_for_local;
                    }
                    lenght = code_for_local.Length;
                    for (int i = lenght; i < 4; i++)
                    {
                        code_for_local = "0" + code_for_local;
                    }
                    string id_local = myForm.NormTable.type.ToString() + "0000" + obj_for_local + "000000" + code_for_local + "00";
                    bool valid = dbOps.LocalIdValidChecking(id_local, cur_org_id, myForm.NormTable.id_fuel, myForm.NormTable.Id_prod, myForm.NormTable.real_name);
                    while (!valid)
                    {
                        Int64 tmpid = Int64.Parse(id_local);
                        tmpid += 1;
                        id_local = tmpid.ToString();
                        valid = dbOps.LocalIdValidChecking(id_local, cur_org_id, myForm.NormTable.id_fuel, myForm.NormTable.Id_prod, myForm.NormTable.real_name);
                    }
                    row.CreateCells(newGrid2, new object[] { tmp_id, myForm.NormTable.Code, myForm.NormTable.name_with_fuel, myForm.NormTable.Unit, myForm.NormTable.nUnit, myForm.NormTable.s111, myForm.NormTable.s112, new TextAndImage(myForm.NormTable.type.ToString(), GetFlag(myForm.NormTable.type)), new TextAndImage(myForm.NormTable.id_obj.ToString(), GetFlagObj(myForm.NormTable.id_obj)), id_local, myForm.NormTable.real_name, myForm.NormTable.Name, myForm.NormTable.id_fuel, myForm.NormTable.Id_prod });
                    l2.Add(row);

                    newGrid2.ResumeLayout();
                    newGrid2.AssignRows(l2);
                    newGrid2.ForceRefreshGroupBox();
                    newGrid2.Fill();

                    foreach (OutlookGridRow a in newGrid.Rows)
                    {
                        if (a.Cells[1].Value != null)
                            l.Add(a);
                    }
                    newGrid.SuspendLayout();
                    newGrid.ClearInternalRows();
                    newGrid.FillMode = FillMode.GROUPSONLY;

                    string group = "";
                    row = new OutlookGridRow();
                    row.CreateCells(newGrid, new object[] { tmp_id, myForm.NormTable.Code, myForm.NormTable.name_with_fuel, myForm.NormTable.Unit, myForm.NormTable.nUnit, myForm.NormTable.s111, myForm.NormTable.s112, new TextAndImage(myForm.NormTable.type.ToString(), GetFlag(myForm.NormTable.type)), new TextAndImage(myForm.NormTable.id_obj.ToString(), GetFlagObj(myForm.NormTable.id_obj)), id_local, myForm.NormTable.real_name, myForm.NormTable.Name, myForm.NormTable.id_fuel, myForm.NormTable.Id_prod });
                    l.Add(row);

                    newGrid.ResumeLayout();
                    newGrid.AssignRows(l);
                    newGrid.ForceRefreshGroupBox();
                    newGrid.Fill();
                    edited = true;
                }
            }
        }
        private void RemoveNormButton_Click(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Navigator.KryptonNavigator nav = kryptonNavigator2;
            int index = Int32.Parse(nav.SelectedPage.Tag.ToString());
            if (index == 1 || index == 2 || index == 3 || index == 4)
            {
                KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid2 = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                newGrid = GetGridByType(index - 1);
                if (newGrid.SelectedRows.Count > 0)
                {
                    if (newGrid.SelectedRows[0].Cells[1].Value != null)
                    {
                        int type = Int32.Parse(newGrid.SelectedRows[0].Cells[7].Value.ToString());
                        if (index != 1)
                            newGrid2 = kryptonOutlookGrid9;
                        else if (index == 1)
                            newGrid2 = GetGridByType(type);
                        int rowIndex = -1;

                        DataGridViewRow tmprow = newGrid2.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells[0].Value.ToString().Equals(newGrid.SelectedRows[0].Cells[0].Value.ToString()))
                            .First();

                        rowIndex = tmprow.Index;
                        newGrid.Rows.RemoveAt(newGrid.SelectedRows[0].Index);
                        newGrid2.Rows.RemoveAt(newGrid2.Rows[rowIndex].Index);
                        edited = true;
                    }
                }
            }
        }

        private void SourceAddButton_Click(object sender, EventArgs e)
        {
            var myForm = new AddSourceMainForm();
           // myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog(); 
            if (myForm.DialogResult == DialogResult.OK)
            {
                OutlookGridRow row = new OutlookGridRow();
                List<OutlookGridRow> l = new List<OutlookGridRow>();
                foreach (OutlookGridRow a in kryptonOutlookGrid10.Rows)
                {
                    if (a.Cells[1].Value != null)
                        l.Add(a);
                }
                kryptonOutlookGrid10.SuspendLayout();
                kryptonOutlookGrid10.ClearInternalRows();
                kryptonOutlookGrid10.FillMode = FillMode.GROUPSONLY;

                string group = "";
                row = new OutlookGridRow();
                group = myForm.fuel_id.ToString();
                row.CreateCells(kryptonOutlookGrid10, new object[] { 0, myForm.obj_id, myForm.fuel_id, new TextAndImage(myForm.obj_name, Properties.Resources.box), new TextAndImage(myForm.fuel_name, GetFuelFlag(Int32.Parse(group[0].ToString()))), new TextAndImage(myForm.type.ToString(), GetFlag(myForm.type)) });
                l.Add(row);

                kryptonOutlookGrid10.ResumeLayout();
                kryptonOutlookGrid10.AssignRows(l);
                kryptonOutlookGrid10.ForceRefreshGroupBox();
                kryptonOutlookGrid10.Fill();
                edited = true;
            }
        }

        private void SaveProfileButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что желаете сохранить внесенные в профиль изменения?", "Сохранение изменений", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SaveProfile();
                edited = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }           
        }

        private void SourceRemoveButton_Click(object sender, EventArgs e)
        {
            if (kryptonOutlookGrid10.SelectedRows.Count > 0)
            {
                if (kryptonOutlookGrid10.SelectedRows[0].Cells[1].Value != null)
                {
                    kryptonOutlookGrid10.Rows.RemoveAt(kryptonOutlookGrid10.SelectedRows[0].Index);
                    edited = true;
                }
            }
        }

        private void ProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edited)
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, что желаете сохранить внесенные в профиль изменения?", "Закрытие редактора профиля", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveProfile();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }

        private void kryptonOutlookGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void kryptonOutlookGrid9_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid dataGridView = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            var row = dataGridView.Rows[e.RowIndex];

            if (row.Cells[1].Value != null)
            {
                var myForm = new EditNormNameForm(row.Cells[11].Value.ToString());
                // myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
                myForm.ShowDialog();
                if (myForm.DialogResult == DialogResult.OK)
                {
                    ComponentFactory.Krypton.Navigator.KryptonNavigator nav = kryptonNavigator2;
                    KryptonOutlookGrid.Classes.KryptonOutlookGrid newGrid = new KryptonOutlookGrid.Classes.KryptonOutlookGrid();
                    int index = Int32.Parse(nav.SelectedPage.Tag.ToString());

                    if (index == 1)
                        newGrid = GetGridByType(Int32.Parse(row.Cells[7].Value.ToString()));
                    else
                        newGrid = kryptonOutlookGrid9;

                    int rowIndex = -1;

                    DataGridViewRow tmprow = newGrid.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.ToString().Equals(row.Cells[0].Value.ToString()))
                        .First();

                    rowIndex = tmprow.Index;

                    row.Cells[11].Value = myForm.name_of_norm;
                    newGrid.Rows[rowIndex].Cells[11].Value = myForm.name_of_norm;
                    if (Int32.Parse(row.Cells[12].Value.ToString()) != 0)
                    {
                        string fuel_name = " (" + dbOps.GetFuelNameById(Int32.Parse(row.Cells[12].Value.ToString()), DateTime.Now.Year, DateTime.Now.Month) + ")";
                        row.Cells[2].Value = myForm.name_of_norm + fuel_name;
                        newGrid.Rows[rowIndex].Cells[2].Value = myForm.name_of_norm + fuel_name;
                    }
                    else
                    {
                        row.Cells[2].Value = myForm.name_of_norm;
                        newGrid.Rows[rowIndex].Cells[2].Value = myForm.name_of_norm;
                    }
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                Console.WriteLine("No tree node selected.");
            else
            {
                var main_obj = kryptonOutlookGrid9.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells[8].Value != null && r.Cells[8].Value.ToString().Equals(treeView1.SelectedNode.Tag.ToString()));
                foreach (var a in main_obj.ToList())
                    kryptonOutlookGrid9.Rows.RemoveAt(kryptonOutlookGrid9.Rows[a.Index].Index);
                var fuel_obj = kryptonOutlookGrid4.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells[8].Value != null && r.Cells[8].Value.ToString().Equals(treeView1.SelectedNode.Tag.ToString()));
                foreach (var a in fuel_obj.ToList())
                    kryptonOutlookGrid4.Rows.RemoveAt(kryptonOutlookGrid4.Rows[a.Index].Index);
                var heat_obj = kryptonOutlookGrid3.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells[8].Value != null && r.Cells[8].Value.ToString().Equals(treeView1.SelectedNode.Tag.ToString()));
                foreach (var a in heat_obj.ToList())
                    kryptonOutlookGrid3.Rows.RemoveAt(kryptonOutlookGrid3.Rows[a.Index].Index);
                var el_obj = kryptonOutlookGrid2.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells[8].Value != null && r.Cells[8].Value.ToString().Equals(treeView1.SelectedNode.Tag.ToString()));
                foreach (var a in el_obj.ToList())
                    kryptonOutlookGrid2.Rows.RemoveAt(kryptonOutlookGrid2.Rows[a.Index].Index);
                objToDisactivate.Add(Int32.Parse(treeView1.SelectedNode.Tag.ToString()));
                treeView1.SelectedNode.Remove();

            }        
        }

        private void SaveProfile()
        {
            foreach (var a in objToDisactivate)
            {
                dbOps.DisactivateObject(a);
            }
            int profilenum = dbOps.GetProfileNumIfExist(cur_org_id, DateTime.Now.Year, DateTime.Now.Month);
            ProfileTable profData = dbOps.GetProfileData(cur_org_id, cmonth, cyear);
            if (profilenum != 0)
            {
                dbOps.DeleteOldProfileVariant(profilenum, cur_org_id);
                //тут будем вайпать кучу табличек по номеру
            }
            else
            {
                profilenum = profData.Num + 1;
                dbOps.AddProfile(DateTime.Now.Year, DateTime.Now.Month, cur_org_id, profilenum);
            }

            //добавление Fuel
            foreach (OutlookGridRow a in kryptonOutlookGrid7.Rows)
            {
                if (a.Cells[1].Value != null)
                    dbOps.AddNewOrgFuels(DateTime.Now.Year, DateTime.Now.Month, profilenum, cur_org_id, Int32.Parse(a.Cells[1].Value.ToString()), bool.Parse(a.Cells[6].Value.ToString()));
            }
            //Добавление Source 
            foreach (OutlookGridRow a in kryptonOutlookGrid10.Rows)
            {
                if (a.Cells[1].Value != null)
                    dbOps.AddNewOrgSource(DateTime.Now.Year, DateTime.Now.Month, profilenum, cur_org_id, Int32.Parse(a.Cells[1].Value.ToString()), Int32.Parse(a.Cells[2].Value.ToString()), Int32.Parse(a.Cells[5].Value.ToString()));
            }
            //Добавление Sended & Recieved
            foreach (OutlookGridRow a in kryptonOutlookGrid1.Rows)
            {
                if (a.Cells[1].Value != null)
                    dbOps.AddNewOrgRec(DateTime.Now.Year, DateTime.Now.Month, profilenum, Int32.Parse(a.Cells[1].Value.ToString()), cur_org_id, Int32.Parse(a.Cells[3].Value.ToString()));
            }
            foreach (OutlookGridRow a in kryptonOutlookGrid6.Rows)
            {
                if (a.Cells[1].Value != null)
                    dbOps.AddNewOrgSend(DateTime.Now.Year, DateTime.Now.Month, profilenum, Int32.Parse(a.Cells[1].Value.ToString()), cur_org_id, Int32.Parse(a.Cells[3].Value.ToString()));
            }
            foreach (OutlookGridRow a in kryptonOutlookGrid9.Rows)
            {
                if (a.Cells[1].Value != null)
                {
                    string row_options = "";
                    if (bool.Parse(a.Cells[5].Value.ToString()) && bool.Parse(a.Cells[6].Value.ToString()))
                        row_options = "111,112";
                    else if (bool.Parse(a.Cells[5].Value.ToString()))
                        row_options = "111";
                    else if (bool.Parse(a.Cells[6].Value.ToString()))
                        row_options = "112";
                    dbOps.AddNewOrgNorm(cur_org_id, Int32.Parse(a.Cells[13].Value.ToString()), a.Cells[9].Value.ToString(), Int32.Parse(a.Cells[1].Value.ToString()), a.Cells[11].Value.ToString(), Int32.Parse(a.Cells[12].Value.ToString()), row_options, Int32.Parse(a.Cells[7].Value.ToString()), Int32.Parse(a.Cells[8].Value.ToString()), profilenum, DateTime.Now.Month, DateTime.Now.Year, a.Cells[10].Value.ToString());
                }
            }
            MessageBox.Show("Данные сохранены.");
        }

    }
}
