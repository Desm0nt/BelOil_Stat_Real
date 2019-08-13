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
using JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid;
using WindowsFormsApp1.DBO;
using JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.CustomColumns;
using JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.Formatting;

namespace WindowsFormsApp1
{
    public partial class ProductsListForm : KryptonForm
    {
        List<DataTables.ProductTable> productList;

        public ProductsListForm()
        {
            InitializeComponent();
        }

        private void ProductsListForm_Load(object sender, EventArgs e)
        {
            productList = dbOps.GetProdList();
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
            LoadData();

            kryptonOutlookGrid1.Collapse(kryptonOutlookGrid1.Columns[7].Name);


        }

        private void LoadData()
        {
            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GroupsOnly;

            foreach (var product in productList) //TODO for instead foreach for perfs...
            {
                row = new OutlookGridRow();               
                row.CreateCells(kryptonOutlookGrid1, new object[] { product.Id, product.Code, product.Name, product.Unit, product.nUnit, product.s111, product.s112, product.type });
                l.Add(row);
                //((KryptonDataGridViewTreeTextCell)row.Cells[1]).UpdateStyle();
            }

            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();
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

        private void kryptonOutlookGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var myForm = new ProductAddForm();
            //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }
    }
}
