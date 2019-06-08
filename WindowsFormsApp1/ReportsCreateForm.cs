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
        int curRepid, year, month;
        

        public ReportsCreateForm(int year1, int month1)
        {
            InitializeComponent();
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
            {
                dbOps.AddNewReport(CurrentData.UserData.Id_org, year, month, CurrentData.UserData.Id);
                curRepid = dbOps.GetReportId(CurrentData.UserData.Id_org, year, month);
            }
            else if (checkreport == true)
            {
                curRepid = dbOps.GetReportId(CurrentData.UserData.Id_org, year, month);
            }
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
            List<FTradeInputTable> tradelist = dbOps.GetFTradeInputList(CurrentData.UserData.Id_org, curRepid, year, month);
            dataGridView1.DataSource = tradelist;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText  = "Код топлива";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].HeaderText  = "Наименование";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].HeaderText  = "Значение";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<FTradeInputTable> FTradeInputList = dataGridView1.DataSource as List<FTradeInputTable>;
            foreach(var a in FTradeInputList)
            {
                dbOps.UpdateFuelTrades(a.Id, a.Value);
            }
            List<NormInputTable> NormToplist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 1, year, month);
            dataGridView2.DataSource = NormToplist;
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].HeaderText  = "Наименование";
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].HeaderText  = "План.";
            dataGridView2.Columns[3].HeaderText  = "Факт.";
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<NormInputTable> NormToplist = dataGridView2.DataSource as List<NormInputTable>;
            foreach (var a in NormToplist)
            {
                dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
            }
            List<SourceInputTable> SourceInputList = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 2);
            dataGridView3.DataSource = SourceInputList;
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[1].HeaderText  = "Наименование";
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[2].HeaderText  = "Значение";
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<SourceInputTable> SourceInputList = dataGridView3.DataSource as List<SourceInputTable>;
            foreach (var a in SourceInputList)
            {
                dbOps.UpdateSource(a.Id, a.Value);
            }
            List<RecievedInputTable> RecievedInputList = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 2);
            dataGridView4.DataSource = RecievedInputList;
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView4.Columns[1].HeaderText  = "Наименование организации";
            dataGridView4.Columns[1].ReadOnly = true;
            dataGridView4.Columns[2].HeaderText  = "Значение";
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<RecievedInputTable> RecievedInputList = dataGridView4.DataSource as List<RecievedInputTable>;
            foreach (var a in RecievedInputList)
            {
                dbOps.UpdateRecieved(a.Id, a.value);
            }
            List<SendedInputTable> SendedInputList = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 2);
            dataGridView5.DataSource = SendedInputList;
            dataGridView5.Columns[0].ReadOnly = true;
            dataGridView5.Columns[1].HeaderText  = "Наименование организации";
            dataGridView5.Columns[1].ReadOnly = true;
            dataGridView5.Columns[2].HeaderText  = "Значение";
            panel4.Visible = false;
            panel5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<SendedInputTable> SendedInputList = dataGridView5.DataSource as List<SendedInputTable>;
            foreach (var a in SendedInputList)
            {
                dbOps.UpdateSended(a.Id, a.value);
            }
            List<NormInputTable> NormHeatlist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 2, year, month);
            dataGridView6.DataSource = NormHeatlist;
            dataGridView6.Columns[0].ReadOnly = true;
            dataGridView6.Columns[1].HeaderText  = "Наименование";
            dataGridView6.Columns[1].ReadOnly = true;
            dataGridView6.Columns[2].HeaderText  = "План.";
            dataGridView6.Columns[3].HeaderText  = "Факт.";
            panel5.Visible = false;
            panel6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<NormInputTable> NormHeatlist = dataGridView6.DataSource as List<NormInputTable>;
            foreach (var a in NormHeatlist)
            {
                dbOps.UpdateFuelNorm(a.Id, a.val_plan, a.val_fact);
            }
            List<SourceInputTable> SourceInputList = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 3);
            dataGridView7.DataSource = SourceInputList;
            dataGridView7.Columns[0].ReadOnly = true;
            dataGridView7.Columns[1].HeaderText  = "Наименование";
            dataGridView7.Columns[1].ReadOnly = true;
            dataGridView7.Columns[2].HeaderText  = "Значение";
            panel6.Visible = false;
            panel7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<SourceInputTable> SourceInputList = dataGridView7.DataSource as List<SourceInputTable>;
            foreach (var a in SourceInputList)
            {
                dbOps.UpdateSource(a.Id, a.Value);
            }
            List<RecievedInputTable> RecievedInputList = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 3);
            dataGridView8.DataSource = RecievedInputList;
            dataGridView8.Columns[0].ReadOnly = true;
            dataGridView8.Columns[1].HeaderText  = "Наименование организации";
            dataGridView8.Columns[1].ReadOnly = true;
            dataGridView8.Columns[2].HeaderText  = "Значение";
            panel7.Visible = false;
            panel8.Visible = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<RecievedInputTable> RecievedInputList = dataGridView8.DataSource as List<RecievedInputTable>;
            foreach (var a in RecievedInputList)
            {
                dbOps.UpdateRecieved(a.Id, a.value);
            }
            List<SendedInputTable> SendedInputList = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 3);
            dataGridView9.DataSource = SendedInputList;
            dataGridView9.Columns[0].ReadOnly = true;
            dataGridView9.Columns[1].HeaderText  = "Наименование организации";
            dataGridView9.Columns[1].ReadOnly = true;
            dataGridView9.Columns[2].HeaderText  = "Значение";
            panel8.Visible = false;
            panel9.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<SendedInputTable> SendedInputList = dataGridView9.DataSource as List<SendedInputTable>;
            foreach (var a in SendedInputList)
            {
                dbOps.UpdateSended(a.Id, a.value);
            }
            List<NormInputTable> NormEllist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, 3, year, month);
            dataGridView10.DataSource = NormEllist;
            dataGridView10.Columns[0].ReadOnly = true;
            dataGridView10.Columns[1].HeaderText  = "Наименование";
            dataGridView10.Columns[1].ReadOnly = true;
            dataGridView10.Columns[2].HeaderText  = "План.";
            dataGridView10.Columns[3].HeaderText  = "Факт.";
            panel9.Visible = false;
            panel10.Visible = true;
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

        void EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.CurrentCell.ColumnIndex == 3)
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
