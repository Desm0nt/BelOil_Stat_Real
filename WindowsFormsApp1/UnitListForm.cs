using ComponentFactory.Krypton.Navigator;
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
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;
using WindowsFormsApp1.DBO;
using KryptonOutlookGrid.Classes;
using KryptonOutlookGrid.CustomColumns;

namespace WindowsFormsApp1
{
    public partial class UnitListForm : KryptonForm
    {
        List<DataTables.UnitsTable> unitList;
        bool[] groustate = new bool[3];

        public UnitListForm()
        {
            InitializeComponent();
        }

        private void FuelListForm_Load(object sender, EventArgs e)
        {      
            LoadData();
        }

        private void LoadData()
        {

            unitList = dbOps.GetUnitsList(toolStripTextBox2.Text, toolStripTextBox1.Text);
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
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[5];
            columnsToAdd[0] = kryptonOutlookGrid1.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid1.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid1.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid1.Columns[4];

            //kryptonOutlookGrid1.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[4], new OutlookGridUnitGroup(null), SortOrder.Ascending, 1, -1);

            kryptonOutlookGrid1.Columns[4].Visible = false;

            kryptonOutlookGrid1.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            //kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GROUPSONLY;
            string group = "";
            foreach (var units in unitList)
            {
                string units_code = units.id.ToString();
                int lenght = units_code.Length;
                for (int i = lenght; i < 4; i++)
                {
                    units_code = "0" + units_code;
                }

                row = new OutlookGridRow();
                row.CreateCells(kryptonOutlookGrid1, new object[] { units.id, units_code, units.full_name, units.name, units.pid});
                l.Add(row);
            }

            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();

            //kryptonOutlookGrid1.Collapse(kryptonOutlookGrid1.Columns[7].Name);
            //if (edit)
            //{
            //    for (int i = 0; i < kryptonOutlookGrid1.GroupCollection.Count; i++)
            //        kryptonOutlookGrid1.GroupCollection[i].Collapsed = groustate[i];
            //}

        }

        private void kryptonOutlookGrid1_Resize(object sender, EventArgs e)
        {
            int PreferredTotalWidth = 0;
            //Calculate the total preferred width
            foreach (DataGridViewColumn c in kryptonOutlookGrid1.Columns)
            {
                PreferredTotalWidth += Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
            }

            if (kryptonOutlookGrid1.Width > PreferredTotalWidth)
            {
                kryptonOutlookGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                kryptonOutlookGrid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            else
            {
                kryptonOutlookGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                foreach (DataGridViewColumn c in kryptonOutlookGrid1.Columns)
                {
                    c.Width = Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
                }
            }
        }

        public class TypeConverter
        {
            public static string ProcessType(string FullQualifiedName)
            {
                //Translate types here to accomodate code changes, namespaces and version
                //Select Case FullQualifiedName
                //    Case "JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridAlphabeticGroup, JDHSoftware.Krypton.Toolkit, Version=1.2.0.0, Culture=neutral, PublicKeyToken=e12f297423986ef5",
                //        "JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridAlphabeticGroup, JDHSoftware.Krypton.Toolkit, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null"
                //        'Change with new version or namespace or both !
                //        FullQualifiedName = "TestMe, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null"
                //        Exit Select
                //End Select
                return FullQualifiedName;
            }
        }

        private void myForm_FormClosed(object sender, EventArgs e)
        {
            LoadData();
        }

        private void kryptonOutlookGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //KryptonOutlookGrid.Classes.KryptonOutlookGrid dataGridView = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            //if (e.RowIndex >= 0)
            //{
            //    if (dataGridView.Rows[e.RowIndex].Cells[1].Value != null)
            //    {
            //        DataTables.FuelsTable table = new DataTables.FuelsTable
            //        {
            //            id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()),
            //            fuel_id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()),
            //            name = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
            //            Qn = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
            //            B_y = float.Parse(dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()),
            //            unit = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()
            //        };
            //        var myForm = new FuelAddForm(table, this);
            //        //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            //        myForm.ShowDialog();

            //        if (myForm.DialogResult == DialogResult.OK)
            //        {
            //            var productTable = myForm.fuelTable;
            //            dataGridView.Rows[e.RowIndex].Cells[0].Value = productTable.id;
            //            dataGridView.Rows[e.RowIndex].Cells[1].Value = productTable.fuel_id;
            //            dataGridView.Rows[e.RowIndex].Cells[2].Value = productTable.name;
            //            dataGridView.Rows[e.RowIndex].Cells[4].Value = productTable.Qn;
            //            dataGridView.Rows[e.RowIndex].Cells[5].Value = productTable.B_y;
            //            dataGridView.Rows[e.RowIndex].Cells[3].Value = productTable.unit;
            //        }
            //    }
            //}
        }


        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            var myForm = new FuelAddForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 1);
            var dataGridViewCellMouseEventArgs = new DataGridViewCellMouseEventArgs(kryptonOutlookGrid1.CurrentCell.ColumnIndex, kryptonOutlookGrid1.CurrentCell.RowIndex, 0, 0, mouseEventArgs);
            kryptonOutlookGrid1_CellMouseDoubleClick(kryptonOutlookGrid1, dataGridViewCellMouseEventArgs);
        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {
            var ind = kryptonOutlookGrid1.CurrentCell.RowIndex;
            if (ind >= 0)
            {
                if (kryptonOutlookGrid1.Rows[ind].Cells[1].Value != null)
                    dbOps.DeleteFromFuel(Int32.Parse(kryptonOutlookGrid1.Rows[ind].Cells[0].Value.ToString()));
            }
            LoadData();
        }

        private void searchToolStripButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void resetToolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            toolStripTextBox2.Text = "";
            LoadData();
        }
    }
}
