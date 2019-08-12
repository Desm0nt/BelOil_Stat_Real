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

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[7], new OutlookGridTypeGroup(null), SortOrder.Ascending, -1, -1);
            kryptonOutlookGrid1.Columns[0].Visible = false;
            kryptonOutlookGrid1.Columns[7].Visible = false;
            kryptonOutlookGrid1.Columns[7].SortMode = DataGridViewColumnSortMode.Programmatic;

            ConditionalFormatting cond = new ConditionalFormatting();
            cond.ColumnName = kryptonOutlookGrid1.Columns[7].ToString();
            cond.FormatType = EnumConditionalFormatType.TwoColorsRange;
            cond.FormatParams = new TwoColorsParams(Color.White, Color.FromArgb(255, 255, 58, 61));
            kryptonOutlookGrid1.ConditionalFormatting.Add(cond);

            kryptonOutlookGrid1.ShowLines = true;
            LoadData();
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
                    row.CreateCells(kryptonOutlookGrid1, new object[] {
                        product.Id,
                        product.Code,
                        product.Name,
                        product.Unit,
                        product.nUnit,
                        product.s111,
                        product.s112,
                        product.type
                    });
                    l.Add(row);
                    //((KryptonDataGridViewTreeTextCell)row.Cells[1]).UpdateStyle(); //Important : after added to the rows list
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
                kryptonOutlookGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

    }
}
