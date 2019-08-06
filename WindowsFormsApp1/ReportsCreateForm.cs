using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class ReportsCreateForm : Form
    {
        bool checkreport;
        int curRepid, year, month, currentTubIndex;
        

        public ReportsCreateForm(int year1, int month1)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 0;
            currentTubIndex = 0;
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing);
            this.dataGridView2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing1);
            this.dataGridView6.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing1);
            this.dataGridView10.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing1);
            this.dataGridView3.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            this.dataGridView4.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            this.dataGridView5.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            this.dataGridView7.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            this.dataGridView8.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            this.dataGridView9.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing2);
            curRepid = 0;
            year = year1;
            month = month1;       
            checkreport = dbOps.ExistReportCheck(CurrentData.UserData.Id_org, year, month);

            if (checkreport == false)
                dbOps.AddNewReport(CurrentData.UserData.Id_org, year, month, CurrentData.UserData.Id);

            curRepid = dbOps.GetReportId(CurrentData.UserData.Id_org, year, month);
            var TradeFuelList = dbOps.GetFuelsTradeId(CurrentData.UserData.Id_org, 1);
            var NormIdTypeList = dbOps.GetNormIdTypeList(CurrentData.UserData.Id_org);
            var SourceIdList = dbOps.GetSoucreIdList(CurrentData.UserData.Id_org);
            var RecievedIdList = dbOps.GetRecievedIdList(CurrentData.UserData.Id_org);
            var SendedIdList = dbOps.GetSendedIdList(CurrentData.UserData.Id_org);
            if (checkreport == false)
            {
                foreach (var a in TradeFuelList)
                {
                    dbOps.AddFuelTrades(a, CurrentData.UserData.Id_org, curRepid, 0);
                }
                foreach (var a in NormIdTypeList)
                {
                    dbOps.AddNormData(a.Id, curRepid, 0, 0);
                }
                foreach (var a in SourceIdList)
                {
                    dbOps.AddSource(a.Id, CurrentData.UserData.Id_org, curRepid, 0);
                }
                foreach (var a in RecievedIdList)
                {
                    dbOps.AddRecieved(a.Id, CurrentData.UserData.Id_org, curRepid, 0);
                }
                foreach (var a in SendedIdList)
                {
                    dbOps.AddSended(a.Id, CurrentData.UserData.Id_org, curRepid, 0);
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            var row = dataGridView.Rows[e.RowIndex];
            if (!String.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
            {
                row.Cells[6].Value = Math.Round(((float)row.Cells[5].Value * (float)row.Cells[4].Value), 1);
                row.Cells[7].Value = (float)row.Cells[7].Value + (float)row.Cells[5].Value;
            }
            row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value * (float)row.Cells[4].Value), 1);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (currentTubIndex)
            {
                case 1:
                    List<FTradeInputTable> FTradeInputList = dataGridView1.DataSource as List<FTradeInputTable>;
                    foreach (var a in FTradeInputList)
                    {
                        dbOps.UpdateFuelTrades(a.Id, a.Value);
                    }
                    break;
                case 2:
                    List<NormInputTable> NormToplist = dataGridView2.DataSource as List<NormInputTable>;
                    foreach (var a in NormToplist)
                    {
                        dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
                    }
                    break;
                case 3:
                    List<SourceInputTable> SourceInputList = dataGridView3.DataSource as List<SourceInputTable>;
                    foreach (var a in SourceInputList)
                    {
                        dbOps.UpdateSource(a.Id, a.Value);
                    }
                    break;
                case 4:
                    List<RecievedInputTable> RecievedInputList = dataGridView4.DataSource as List<RecievedInputTable>;
                    foreach (var a in RecievedInputList)
                    {
                        dbOps.UpdateRecieved(a.Id, a.value);
                    }
                    break;
                case 5:
                    List<SendedInputTable> SendedInputList = dataGridView5.DataSource as List<SendedInputTable>;
                    foreach (var a in SendedInputList)
                    {
                        dbOps.UpdateSended(a.Id, a.value);
                    }
                    break;
                case 6:
                    List<NormInputTable> NormHeatlist = dataGridView6.DataSource as List<NormInputTable>;
                    foreach (var a in NormHeatlist)
                    {
                        dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
                    }
                    break;
                case 7:
                    List<SourceInputTable> SourceInputList1 = dataGridView7.DataSource as List<SourceInputTable>;
                    foreach (var a in SourceInputList1)
                    {
                        dbOps.UpdateSource(a.Id, a.Value);
                    }
                    break;
                case 8:
                    List<RecievedInputTable> RecievedInputList1 = dataGridView8.DataSource as List<RecievedInputTable>;
                    foreach (var a in RecievedInputList1)
                    {
                        dbOps.UpdateRecieved(a.Id, a.value);
                    };
                    break;
                case 9:
                    List<SendedInputTable> SendedInputList1 = dataGridView9.DataSource as List<SendedInputTable>;
                    foreach (var a in SendedInputList1)
                    {
                        dbOps.UpdateSended(a.Id, a.value);
                    };
                    break;
                case 10:
                    List<NormInputTable> NormEllist = dataGridView10.DataSource as List<NormInputTable>;
                    foreach (var a in NormEllist)
                    {
                        dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
                    }
                    break;
                default:
                    break;
            }
            currentTubIndex = tabControl1.SelectedIndex;
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    List<FTradeInputTable> tradelist = dbOps.GetFTradeInputList(CurrentData.UserData.Id_org, curRepid, year, month);
                    dataGridView1.DataSource = tradelist;
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].HeaderText = "Наименование";
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].HeaderText = "Ед. изм.";
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].HeaderText = "Ву";
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].HeaderText = "За месяц";
                    dataGridView1.Columns[6].HeaderText = "За месяц, т.у.т";
                    dataGridView1.Columns[6].ReadOnly = true;
                    dataGridView1.Columns[7].HeaderText = "С начала года";
                    dataGridView1.Columns[7].ReadOnly = true;
                    dataGridView1.Columns[8].HeaderText = "С начала года, т.у.т";
                    dataGridView1.Columns[8].ReadOnly = true;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
                        {
                            row.Cells[6].Value = Math.Round(((float)row.Cells[5].Value * (float)row.Cells[4].Value), 1);
                            row.Cells[7].Value = (float)row.Cells[7].Value + (float)row.Cells[5].Value;
                        }
                        row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value * (float)row.Cells[4].Value), 1);
                    }
                    break;
                case 2:
                    List<NormInputTable> NormToplist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 1, year, month);
                    dataGridView2.DataSource = NormToplist;
                    dataGridView2.Columns[0].ReadOnly = true;
                    dataGridView2.Columns[1].HeaderText = "Наименование";
                    dataGridView2.Columns[1].ReadOnly = true;
                    dataGridView2.Columns[2].HeaderText = "План.";
                    dataGridView2.Columns[3].HeaderText = "Факт.";
                    break;
                case 3:
                    List<SourceInputTable> SourceInputList = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView3.DataSource = SourceInputList;
                    dataGridView3.Columns[0].ReadOnly = true;
                    dataGridView3.Columns[1].HeaderText = "Наименование";
                    dataGridView3.Columns[1].ReadOnly = true;
                    dataGridView3.Columns[2].HeaderText = "Значение";
                    break;
                case 4:
                    List<RecievedInputTable> RecievedInputList = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView4.DataSource = RecievedInputList;
                    dataGridView4.Columns[0].ReadOnly = true;
                    dataGridView4.Columns[1].HeaderText = "Наименование организации";
                    dataGridView4.Columns[1].ReadOnly = true;
                    dataGridView4.Columns[2].HeaderText = "Значение";
                    break;
                case 5:
                    List<SendedInputTable> SendedInputList = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView5.DataSource = SendedInputList;
                    dataGridView5.Columns[0].ReadOnly = true;
                    dataGridView5.Columns[1].HeaderText = "Наименование организации";
                    dataGridView5.Columns[1].ReadOnly = true;
                    dataGridView5.Columns[2].HeaderText = "Значение";
                    break;
                case 6:
                    List<NormInputTable> NormHeatlist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 2, year, month);
                    dataGridView6.DataSource = NormHeatlist;
                    dataGridView6.Columns[0].ReadOnly = true;
                    dataGridView6.Columns[1].HeaderText = "Наименование";
                    dataGridView6.Columns[1].ReadOnly = true;
                    dataGridView6.Columns[2].HeaderText = "План.";
                    dataGridView6.Columns[3].HeaderText = "Факт.";
                    break;
                case 7:
                    List<SourceInputTable> SourceInputList1 = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView7.DataSource = SourceInputList1;
                    dataGridView7.Columns[0].ReadOnly = true;
                    dataGridView7.Columns[1].HeaderText = "Наименование";
                    dataGridView7.Columns[1].ReadOnly = true;
                    dataGridView7.Columns[2].HeaderText = "Значение";
                    break;
                case 8:
                    List<RecievedInputTable> RecievedInputList1 = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView8.DataSource = RecievedInputList1;
                    dataGridView8.Columns[0].ReadOnly = true;
                    dataGridView8.Columns[1].HeaderText = "Наименование организации";
                    dataGridView8.Columns[1].ReadOnly = true;
                    dataGridView8.Columns[2].HeaderText = "Значение";
                    break;
                case 9:
                    List<SendedInputTable> SendedInputList1 = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView9.DataSource = SendedInputList1;
                    dataGridView9.Columns[0].ReadOnly = true;
                    dataGridView9.Columns[1].HeaderText = "Наименование организации";
                    dataGridView9.Columns[1].ReadOnly = true;
                    dataGridView9.Columns[2].HeaderText = "Значение";
                    break;
                case 10:
                    List<NormInputTable> NormEllist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 3, year, month);
                    dataGridView10.DataSource = NormEllist;
                    dataGridView10.Columns[0].ReadOnly = true;
                    dataGridView10.Columns[1].HeaderText = "Наименование";
                    dataGridView10.Columns[1].ReadOnly = true;
                    dataGridView10.Columns[2].HeaderText = "План.";
                    dataGridView10.Columns[3].HeaderText = "Факт.";
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex += 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<NormInputTable> NormEllist = dataGridView10.DataSource as List<NormInputTable>;
            foreach (var a in NormEllist)
            {
                dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
            }
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex -= 1;
        }

        void EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.CurrentCell.ColumnIndex == 5)
            {
                TextBox tb = e.Control as TextBox;
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }
        }

        void EditingControlShowing1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.CurrentCell.ColumnIndex == 2 || dataGridView.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }
        }

        void EditingControlShowing2(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.CurrentCell.ColumnIndex == 2 || dataGridView.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Text = ((TextBox)sender).Text;
            // filter the decimal point
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.') e.KeyChar = ',';
            // only allow one decimal point
            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {                
                e.Handled = true;
            }
        }
    }
}
