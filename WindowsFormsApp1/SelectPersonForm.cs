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
    public partial class SelectPersonForm : KryptonForm
    {
        bool person_type;
        public SelectPersonForm(bool type)
        {
            InitializeComponent();
            person_type = type;
            LoadData();
            kryptonOutlookGrid1.Rows[0].Selected = true;
        }

        private void LoadData()
        {

            var personList = dbOps.GetPersonList(CurrentData.UserData.Id_org);
            var elList = new List<ListElement>();
            foreach (var a in personList)
                elList.Add(new ListElement { Id = a.Id_org, Name = a.Orgs, Group = a.Subhead });
            //if (edit)
            //{
            //    for (int i = 0; i < kryptonOutlookGrid1.GroupCollection.Count; i++)
            //        groustate[i] = kryptonOutlookGrid1.GroupCollection[i].Collapsed;
            //}
            kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.ClearGroups();
            kryptonOutlookGrid1.RowHeadersWidth = 10;


            kryptonOutlookGrid1.GroupBox = kryptonOutlookGridGroupBox1;
            kryptonOutlookGrid1.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[4];
            columnsToAdd[0] = kryptonOutlookGrid1.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid1.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid1.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[3];

            //kryptonOutlookGrid1.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);

            kryptonOutlookGrid1.ShowLines = false;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            //kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GROUPSONLY;

            foreach (var person in personList)
            {
                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid1, new object[] {
                    person.Id,
                    new TextAndImage(GetPText(person.Type), GetPFlag(person.Type)),
                    person.Name,                
                    person.Post,
                });
                l.Add(row);
            }

            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();
        }

        private Image GetPFlag(int type)
        {
            switch (type)
            {
                case 0:
                    return Properties.Resources.empty;
                case 1:
                    return Properties.Resources.rf;
                case 2:
                    return Properties.Resources.gf;
                default:
                    return null;
            }
        }
        private string GetPText(int type)
        {
            switch (type)
            {
                case 0:
                    return " - ";
                case 1:
                    return "рук.";
                case 2:
                    return "отв.";
                default:
                    return "";
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            int id_value = 0;
            if (kryptonOutlookGrid1.SelectedRows.Count > 0)
            {
                id_value = Int32.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[0].Value.ToString());
                dbOps.UpdateOrgPerson(CurrentData.UserData.Id_org, id_value, person_type);
            }
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
