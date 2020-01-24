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
using WindowsFormsApp1.DataTables;

namespace WindowsFormsApp1
{
    public partial class AddOrgNormForm : KryptonForm
    {
        public ProfNormTable NormTable = new ProfNormTable();
        int object_id = -1;
        int nType = 0;
        int cur_org_id;
        List<DataTables.ProductTable> productList;
        bool[] groustate = new bool[3];

        public AddOrgNormForm(int index, int org_id)
        {
            InitializeComponent();
            cur_org_id = org_id;
            if (index > 1)
                nType = index -1;
            LoadData();
            this.Width = 803;
        }

        public AddOrgNormForm(int index, int _ntype, int obj_id, int org_id)
        {
            InitializeComponent();
            cur_org_id = org_id;
            object_id = obj_id;
            nType = _ntype;
            LoadData();
            this.Width = 803;
        }

        private void LoadData()
        {
            List<ProductTable> productList = new List<ProductTable>();
            if (nType > 0)
                productList = dbOps.GetTypedProdList(toolStripTextBox2.Text, toolStripTextBox1.Text, nType);
            else
                productList = dbOps.GetProdList(toolStripTextBox2.Text, toolStripTextBox1.Text);
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
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[8];
            columnsToAdd[0] = kryptonOutlookGrid1.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid1.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid1.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid1.Columns[4];
            columnsToAdd[5] = kryptonOutlookGrid1.Columns[5];
            columnsToAdd[6] = kryptonOutlookGrid1.Columns[6];
            columnsToAdd[7] = kryptonOutlookGrid1.Columns[7];
            //kryptonOutlookGrid1.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.Ascending, -1, 1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid1.Columns[0].Visible = false;
            kryptonOutlookGrid1.Columns[7].Visible = false;

            kryptonOutlookGrid1.ShowLines = true;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            //kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GROUPSONLY;

            foreach (var product in productList)
            {
                row = new OutlookGridRow();               
                row.CreateCells(kryptonOutlookGrid1, new object[] { product.Id, product.Code, product.Name, product.Unit, product.nUnit, product.s111, product.s112, new TextAndImage(product.type.ToString(), GetFlag(product.type)) });
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
            //        DataTables.ProductTable table = new DataTables.ProductTable
            //        {
            //            Id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()),
            //            Code = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()),
            //            Name = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
            //            Unit = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
            //            nUnit = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
            //            s111 = Boolean.Parse(dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()),
            //            s112 = Boolean.Parse(dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString()),
            //            type = Int32.Parse(((TextAndImage)dataGridView.Rows[e.RowIndex].Cells[7].Value).Text)
            //        };
            //        var myForm = new ProductAddForm(table, this);
            //        //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            //        myForm.ShowDialog ();

            //        if (myForm.DialogResult == DialogResult.OK)
            //        {
            //            var productTable = myForm.productTable;
            //            dataGridView.Rows[e.RowIndex].Cells[0].Value = productTable.Id;
            //            dataGridView.Rows[e.RowIndex].Cells[1].Value = productTable.Code;
            //            dataGridView.Rows[e.RowIndex].Cells[2].Value = productTable.Name;
            //            dataGridView.Rows[e.RowIndex].Cells[3].Value = productTable.Unit;
            //            dataGridView.Rows[e.RowIndex].Cells[4].Value = productTable.nUnit;
            //            dataGridView.Rows[e.RowIndex].Cells[5].Value = productTable.s111;
            //            dataGridView.Rows[e.RowIndex].Cells[6].Value = productTable.s112;
            //        }
            //    }
            //}
        }


        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            var myForm = new ProductAddForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 1);
            var dataGridViewCellMouseEventArgs = new DataGridViewCellMouseEventArgs(kryptonOutlookGrid1.CurrentCell.ColumnIndex, kryptonOutlookGrid1.CurrentCell.RowIndex, 0, 0, mouseEventArgs);
            kryptonOutlookGrid1_CellMouseDoubleClick(kryptonOutlookGrid1, dataGridViewCellMouseEventArgs);
        }

        private void kryptonOutlookGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid dataGridView = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            if (dataGridView.Rows[e.RowIndex].Cells[1].Value != null)
            {
                label1.Text = "#" + dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() + " - " + dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            else
            {
                string typestr = "Раздел: ";
                switch (Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    case 1:
                        typestr += "топливо";
                        break;
                    case 2:
                        typestr += "тепловая энергия";
                        break;
                    case 3:
                        typestr += "электрическая энергия";
                        break;
                    default:
                        break;
                }
                label1.Text = typestr;
            }
        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {
            var ind = kryptonOutlookGrid1.CurrentCell.RowIndex;
            if (ind >= 0)
            {
                if (kryptonOutlookGrid1.Rows[ind].Cells[1].Value != null)
                    dbOps.DeleteFromProd(Int32.Parse(kryptonOutlookGrid1.Rows[ind].Cells[0].Value.ToString()));
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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonOutlookGrid1.SelectedRows.Count > 0)
            {
                if (kryptonOutlookGrid1.SelectedRows[0].Cells[1].Value != null)
                {
                    string name = kryptonOutlookGrid1.SelectedRows[0].Cells[2].Value.ToString();
                    int fuel = 0;
                    string fuel_name = "";
                    int type4form = 0;
                    if (nType > 0)
                        type4form = nType;
                    else
                        type4form = Int32.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[7].Value.ToString());
                    var myForm = new AddOrgNormSupportForm(kryptonOutlookGrid1.SelectedRows[0].Cells[2].Value.ToString(), type4form, object_id);
                    myForm.ShowDialog();
                    if (myForm.DialogResult == DialogResult.OK)
                    {
                        name = myForm.name;
                        fuel = myForm.fuel_id;
                        fuel_name = myForm.fuel_name;
                        NormTable.Id = 0;
                        NormTable.Id_prod = Int32.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[0].Value.ToString());
                        NormTable.Code = Int32.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[1].Value.ToString());
                        NormTable.Name = name;
                        NormTable.Unit = kryptonOutlookGrid1.SelectedRows[0].Cells[3].Value.ToString();
                        NormTable.nUnit = kryptonOutlookGrid1.SelectedRows[0].Cells[4].Value.ToString();
                        NormTable.id_obj = myForm.obj_id;
                        NormTable.s111 = bool.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[5].Value.ToString());
                        NormTable.s112 = bool.Parse(kryptonOutlookGrid1.SelectedRows[0].Cells[6].Value.ToString());
                        NormTable.id_fuel = fuel;
                        NormTable.name_with_fuel = name + fuel_name;
                        NormTable.real_name = kryptonOutlookGrid1.SelectedRows[0].Cells[2].Value.ToString();
                        NormTable.type = type4form;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите корректный элемент!");
                }
            }
            else
            {
                MessageBox.Show("Элемент не выбран!");
            }
        }
    }
}
