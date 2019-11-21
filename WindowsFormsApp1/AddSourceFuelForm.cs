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
    public partial class AddSourceFuelForm : KryptonForm
    {
        public int fuel_id;
        public string fuel_name;
        public AddSourceFuelForm()
        {
            InitializeComponent();
            LoadFuels();
            kryptonOutlookGrid7.Rows[0].Selected = true;
        }

        private void LoadFuels()
        {
            ProfileTable profData = dbOps.GetProfileData(CurrentData.UserData.Id_org, DateTime.Now.Month, DateTime.Now.Year);
            var fueltList = dbOps.GetProfFuelsList(CurrentData.UserData.Id_org, profData.Num, profData.Month, profData.Year);

            kryptonOutlookGrid7.ClearInternalRows();
            kryptonOutlookGrid7.ClearGroups();
            kryptonOutlookGrid7.RowHeadersWidth = 10;


            kryptonOutlookGrid7.GroupBox = kryptonOutlookGridGroupBox1;
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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonOutlookGrid7.SelectedRows.Count > 0)
            {
                fuel_id = Int32.Parse(kryptonOutlookGrid7.SelectedRows[0].Cells[0].Value.ToString());
                fuel_name = kryptonOutlookGrid7.SelectedRows[0].Cells[2].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
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

    }
}
