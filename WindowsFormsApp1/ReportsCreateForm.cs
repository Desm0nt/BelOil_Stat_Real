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
    public partial class ReportsCreateForm : Form
    {
        bool checkreport;
        int curRepid, year, month;

        public ReportsCreateForm(int year1, int month1)
        {
            InitializeComponent();
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
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
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
            panel2.Visible = false;
            panel3.Visible = true;
        }
    }
}
