using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class ReportsCreateForm : KryptonForm
    {
        bool checkreport;
        int curRepid, curProfNum, year, month, currentTubIndex;
        

        public ReportsCreateForm(int year1, int month1)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 0;
            currentTubIndex = 0;
            label14.Text = "Данные за " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month1) + " " + year1;
            treeView1.ExpandAll();
            tabControl1.Appearance = TabAppearance.Buttons;
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
            curProfNum = 0;
            year = year1;
            month = month1;       
            checkreport = dbOps.ExistReportCheck(CurrentData.UserData.Id_org, year, month);

            if (checkreport == false)
            {
                int prof_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, month);
                dbOps.AddNewReport(CurrentData.UserData.Id_org, year, month, CurrentData.UserData.Id, prof_num);
            }
            
            var curRep = dbOps.GetReportData(CurrentData.UserData.Id_org, year, month);
            curRepid = curRep.id;
            curProfNum = curRep.num;
            var TradeFuelList = dbOps.GetFuelsTradeId(CurrentData.UserData.Id_org, 1);
            var NormIdTypeList = dbOps.GetNormIdTypeList(CurrentData.UserData.Id_org, curRep.num);
            var SourceIdList = dbOps.GetSoucreIdList(CurrentData.UserData.Id_org, curRep.num);
            var RecievedIdList = dbOps.GetRecievedIdList(CurrentData.UserData.Id_org, curRep.num);
            var SendedIdList = dbOps.GetSendedIdList(CurrentData.UserData.Id_org, curRep.num);
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
                row.Cells[7].Value = (float)row.Cells[9].Value + (float)row.Cells[5].Value;
            }
            row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value * (float)row.Cells[4].Value), 1);
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            var row = dataGridView.Rows[e.RowIndex];
            if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                row.Cells[11].Value = (float)row.Cells[15].Value + (float)row.Cells[6].Value;
            if (!String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                row.Cells[12].Value = (float)row.Cells[16].Value + (float)row.Cells[7].Value;
            if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()) && !String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
            {
                row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[6].Value) * 100, 1);
                row.Cells[9].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[4].Value) * 100, 1);
                row.Cells[13].Value = Math.Round(((float)row.Cells[12].Value / (float)row.Cells[11].Value) * 100, 1);
                row.Cells[14].Value = Math.Round(((float)row.Cells[12].Value / (float)row.Cells[10].Value) * 100, 1);
            }
        }

        private List<NormTable> MakeListSum(int yearr, int monthh, int type)
        {
            var rep = dbOps.GetReportData(CurrentData.UserData.Id_org, yearr, 1);
            int report_id = rep.id;
            var NormList = dbOps.GetNormInputList(CurrentData.UserData.Id_org, report_id, rep.num, type, yearr, monthh);
            foreach (var a in NormList)
            {
                a.val_fact = 0;
                a.val_plan = 0;
            }

            if (monthh > 1)
            {
                for (int i = 1; i < monthh; i++)
                {
                    rep = dbOps.GetReportData(CurrentData.UserData.Id_org, yearr, i);
                    report_id = rep.id;
                    var tmplist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, report_id, rep.num, type, yearr, i);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < NormList.Count; j++)
                        {
                            NormList[j].val_plan += Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1));
                            NormList[j].val_fact += Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1));
                        }
                    }
                }
            }
            return NormList;
        }

        private List<NormTable> MakeListSumPR(int yearr, int monthh, int type)
        {
            List<NormTable> NormListPR = new List<NormTable>();
            for (int i = 1; i <= month; i++)
            {
                var rep = dbOps.GetReportData(CurrentData.UserData.Id_org, year, i);
                int report_id = rep.id;
                int profile_num = rep.num;
                var tempList = dbOps.GetNormInputList(CurrentData.UserData.Id_org, report_id, rep.num, type, yearr, monthh);
                foreach (var t in tempList)
                {
                    if (NormListPR.Any(norm => norm.Id_local == t.Id_local))
                    {
                        var index = NormListPR.FindIndex(norm => norm.Id_local == t.Id_local);
                        NormListPR[index].val_fact += t.val_fact;
                        NormListPR[index].val_plan += t.val_plan;
                        NormListPR[index].val_fact_ut += t.val_fact_ut;
                        NormListPR[index].val_plan_ut += t.val_plan_ut;

                    }
                    else
                    {
                        NormListPR.Add(t);
                    }
                }
            }

            return NormListPR;
        }

        //private List<NormTable> MakeListOldSum(int yearr, int monthh, int type)
        //{
        //    int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, yearr, 1);
        //    var NormList = dbOps.GetNormInputList(CurrentData.UserData.Id_org, report_id, type, yearr, monthh);

        //    if (monthh > 1)
        //    {
        //        for (int i = 2; i <= monthh; i++)
        //        {
        //            report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, yearr, i);
        //            var tmplist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, report_id, type, yearr, i);
        //            if (tmplist.Count != 0)
        //            {
        //                for (int j = 0; j < NormList.Count; j++)
        //                {
        //                    NormList[j].val_plan += Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1));
        //                    NormList[j].val_fact += Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1));
        //                }
        //            }
        //        }
        //    }
        //    return NormList;
        //}

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
                    button10.Enabled = false;
                    button10.Visible = false;
                    button6.Enabled = true;
                    button6.Visible = true;
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
                    dataGridView1.Columns[2].Width = 200;
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
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[9].ReadOnly = true;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
                        {
                            row.Cells[6].Value = Math.Round(((float)row.Cells[5].Value * (float)row.Cells[4].Value), 1);
                            row.Cells[7].Value = (float)row.Cells[9].Value + (float)row.Cells[5].Value;
                        }
                        row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value * (float)row.Cells[4].Value), 1);
                    }
                    break;
                case 2:
                    List<NormTable> NormToplist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, curProfNum, 1, year, month);
                    List<NormTable> NormToplistSum = MakeListSumPR(year, month, 1);
                    var repp = dbOps.GetReportData(CurrentData.UserData.Id_org, (year - 1), month);
                    List<NormTable> NormToplistOld = dbOps.GetNormInputList(CurrentData.UserData.Id_org, repp.id, repp.num, 1, (year-1), month);
                    List<NormTable> NormToplistOldSum = MakeListSumPR((year-1), month, 1);
                    List<NormInputTable> NormToplist1 = new List<NormInputTable>();
                    for (int i = 0; i < NormToplist.Count; i++)
                    {
                        var Fuel = dbOps.GetFuelData(NormToplist[i].fuel, year, month);
                            NormToplist1.Add(new NormInputTable
                            {
                                Id = NormToplist[i].Id,
                                Code = NormToplist[i].Code,
                                name = NormToplist[i].name + NormToplist[i].fuel_name,
                                Ed_izm = Fuel.unit,
                                val_fact_old = 0,
                                By = (float)Math.Round(Fuel.B_y, 3),
                                val_plan = (float)Math.Round(NormToplist[i].val_plan, 1),
                                val_fact = (float)Math.Round(NormToplist[i].val_fact, 1),
                                val_fact_plan = 0,
                                val_fact_factold = 0,
                                val_fact_old_sum = 0,
                                val_plan_sum = (float)Math.Round(NormToplistSum[i].val_plan, 1) + (float)Math.Round(NormToplist[i].val_plan, 1),
                                val_fact_sum = (float)Math.Round(NormToplistSum[i].val_fact, 1) + (float)Math.Round(NormToplist[i].val_fact, 1),
                                val_fact_plan_sum = (float)Math.Round((((float)Math.Round(NormToplistSum[i].val_fact, 1) + (float)Math.Round(NormToplist[i].val_fact, 1)) / ((float)Math.Round(NormToplistSum[i].val_plan, 1) + (float)Math.Round(NormToplist[i].val_plan, 1))) * 100, 1),
                                val_fact_factold_sum = 0,
                                val_plan_sum_back = (float)Math.Round(NormToplistSum[i].val_plan, 1),
                                val_fact_sum_back = (float)Math.Round(NormToplistSum[i].val_fact, 1)
                            });
                    }
                    foreach (var a in NormToplist1)
                    {
                        int index = NormToplistOld.FindIndex(b => b.name == a.name);
                        if (!String.IsNullOrWhiteSpace(index.ToString()) && index >= 0)
                        {
                            a.val_fact_old = (float)Math.Round(NormToplistOld[index].val_fact, 1);
                            a.val_fact_old_sum = (float)Math.Round(NormToplistOldSum[index].val_fact, 1);
                            a.val_fact_factold_sum = (float)Math.Round(((a.val_fact_sum + a.val_fact) / (float)Math.Round(NormToplistOldSum[index].val_fact, 1)) * 100, 1);
                        }
                    }
                    dataGridView2.DataSource = NormToplist1;
                    dataGridView2.Columns[0].ReadOnly = true;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].HeaderText = " # ";
                    dataGridView2.Columns[1].ReadOnly = true;
                    dataGridView2.Columns[2].HeaderText = "Наименование";
                    dataGridView2.Columns[2].ReadOnly = true;
                    dataGridView2.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridView2.Columns[2].Width = 200;
                    dataGridView2.Columns[3].HeaderText = "Ед. изм.";
                    dataGridView2.Columns[3].ReadOnly = true;
                    dataGridView2.Columns[4].HeaderText = "Ф, " + (year-1);
                    dataGridView2.Columns[4].ReadOnly = true;
                    dataGridView2.Columns[5].HeaderText = "Ву";
                    dataGridView2.Columns[5].ReadOnly = true;
                    dataGridView2.Columns[6].HeaderText = "План";
                    dataGridView2.Columns[7].HeaderText = "Факт";
                    dataGridView2.Columns[8].HeaderText = "Ф/П, %";
                    dataGridView2.Columns[8].ReadOnly = true;
                    dataGridView2.Columns[9].HeaderText = "Ф/Ф("+ (year - 1) + "), %";
                    dataGridView2.Columns[9].ReadOnly = true;
                    dataGridView2.Columns[10].HeaderText = "С начала года Ф, " + (year - 1);
                    dataGridView2.Columns[10].ReadOnly = true;
                    dataGridView2.Columns[11].HeaderText = "С начала года, План";
                    dataGridView2.Columns[11].ReadOnly = true;
                    dataGridView2.Columns[12].HeaderText = "С начала года, Факт";
                    dataGridView2.Columns[12].ReadOnly = true;
                    dataGridView2.Columns[13].HeaderText = "С начала года Ф/П, %";
                    dataGridView2.Columns[13].ReadOnly = true;
                    dataGridView2.Columns[14].HeaderText = "С начала года Ф/Ф(" + (year - 1) + "), %";
                    dataGridView2.Columns[14].ReadOnly = true;
                    dataGridView2.Columns[15].Visible = false;
                    dataGridView2.Columns[16].Visible = false;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                            row.Cells[11].Value = (float)row.Cells[15].Value + (float)row.Cells[6].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                            row.Cells[12].Value = (float)row.Cells[16].Value + (float)row.Cells[7].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()) && !String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                        {
                            row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[6].Value) * 100, 1);
                            row.Cells[9].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[4].Value) * 100, 1);
                        }
                    }
                    break;
                case 3:
                    List<SourceInputTable> SourceInputList = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView3.DataSource = SourceInputList;
                    dataGridView3.Columns[0].ReadOnly = true;
                    dataGridView3.Columns[1].HeaderText = "Наименование";
                    dataGridView3.Columns[1].Width = 250;
                    dataGridView3.Columns[1].ReadOnly = true;
                    dataGridView3.Columns[2].HeaderText = "Значение";
                    break;
                case 4:
                    List<RecievedInputTable> RecievedInputList = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView4.DataSource = RecievedInputList;
                    dataGridView4.Columns[0].ReadOnly = true;
                    dataGridView4.Columns[1].HeaderText = "Наименование организации";
                    dataGridView4.Columns[1].Width = 250;
                    dataGridView4.Columns[1].ReadOnly = true;
                    dataGridView4.Columns[2].HeaderText = "Значение";
                    break;
                case 5:
                    List<SendedInputTable> SendedInputList = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 2);
                    dataGridView5.DataSource = SendedInputList;
                    dataGridView5.Columns[0].ReadOnly = true;
                    dataGridView5.Columns[1].HeaderText = "Наименование организации";
                    dataGridView5.Columns[1].Width = 250;
                    dataGridView5.Columns[1].ReadOnly = true;
                    dataGridView5.Columns[2].HeaderText = "Значение";
                    break;
                case 6:
                    List<NormTable> NormHeatlist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, curProfNum, 2, year, month);
                    List<NormTable> NormHeatlistSum = MakeListSumPR(year, month, 2);
                    repp = dbOps.GetReportData(CurrentData.UserData.Id_org, (year - 1), month);
                    List<NormTable> NormHeatlistOld = dbOps.GetNormInputList(CurrentData.UserData.Id_org, repp.id, repp.num, 2, (year - 1), month);
                    List<NormTable> NormHeatlistOldSum = MakeListSumPR((year - 1), month, 2);
                    List<NormInputTable> NormHeatlist1 = new List<NormInputTable>();
                    for (int i = 0; i < NormHeatlist.Count; i++)
                    {
                        var Factor = dbOps.GetFactorData(2, month, year);
                        var Fuel = dbOps.GetFuelData(NormHeatlist[i].fuel, year, month);
                        NormHeatlist1.Add(new NormInputTable
                        {
                            Id = NormHeatlist[i].Id,
                            Code = NormHeatlist[i].Code,
                            name = NormHeatlist[i].name,
                            Ed_izm = Fuel.unit,
                            val_fact_old = 0,
                            By = (float)Math.Round(Factor.value, 3),
                            val_plan = (float)Math.Round(NormHeatlist[i].val_plan, 1),
                            val_fact = (float)Math.Round(NormHeatlist[i].val_fact, 1),
                            val_fact_plan = 0,
                            val_fact_factold = 0,
                            val_fact_old_sum = 0,
                            val_plan_sum = (float)Math.Round(NormHeatlistSum[i].val_plan, 1) + (float)Math.Round(NormHeatlist[i].val_plan, 1),
                            val_fact_sum = (float)Math.Round(NormHeatlistSum[i].val_fact, 1) + (float)Math.Round(NormHeatlist[i].val_fact, 1),
                            val_fact_plan_sum = (float)Math.Round((((float)Math.Round(NormHeatlistSum[i].val_fact, 1) + (float)Math.Round(NormHeatlist[i].val_fact, 1)) / ((float)Math.Round(NormHeatlistSum[i].val_plan, 1) + (float)Math.Round(NormHeatlist[i].val_plan, 1))) * 100, 1),
                            val_fact_factold_sum = 0,
                            val_plan_sum_back = (float)Math.Round(NormHeatlistSum[i].val_plan, 1),
                            val_fact_sum_back = (float)Math.Round(NormHeatlistSum[i].val_fact, 1)
                        });
                    }
                    foreach (var a in NormHeatlist1)
                    {
                        int index = NormHeatlistOld.FindIndex(b => b.name == a.name);
                        if (!String.IsNullOrWhiteSpace(index.ToString()) && index >=0)
                        {
                            a.val_fact_old = (float)Math.Round(NormHeatlistOld[index].val_fact, 1);
                            a.val_fact_old_sum = (float)Math.Round(NormHeatlistOldSum[index].val_fact, 1);
                            a.val_fact_factold_sum = (float)Math.Round(((a.val_fact_sum + a.val_fact) / (float)Math.Round(NormHeatlistOldSum[index].val_fact, 1)) * 100, 1);
                        }
                    }
                    dataGridView6.DataSource = NormHeatlist1;
                    dataGridView6.Columns[0].ReadOnly = true;
                    dataGridView6.Columns[0].Visible = false;
                    dataGridView6.Columns[1].HeaderText = " # ";
                    dataGridView6.Columns[1].ReadOnly = true;
                    dataGridView6.Columns[2].HeaderText = "Наименование";
                    dataGridView6.Columns[2].ReadOnly = true;
                    dataGridView6.Columns[2].Width = 250;
                    dataGridView6.Columns[3].HeaderText = "Ед. изм.";
                    dataGridView6.Columns[3].ReadOnly = true;
                    dataGridView6.Columns[4].HeaderText = "Ф, " + (year - 1);
                    dataGridView6.Columns[4].ReadOnly = true;
                    dataGridView6.Columns[5].HeaderText = "Ву";
                    dataGridView6.Columns[5].ReadOnly = true;
                    dataGridView6.Columns[6].HeaderText = "План";
                    dataGridView6.Columns[7].HeaderText = "Факт";
                    dataGridView6.Columns[8].HeaderText = "Ф/П, %";
                    dataGridView6.Columns[8].ReadOnly = true;
                    dataGridView6.Columns[9].HeaderText = "Ф/Ф(" + (year - 1) + "), %";
                    dataGridView6.Columns[9].ReadOnly = true;
                    dataGridView6.Columns[10].HeaderText = "С начала года Ф, " + (year - 1);
                    dataGridView6.Columns[10].ReadOnly = true;
                    dataGridView6.Columns[11].HeaderText = "С начала года, План";
                    dataGridView6.Columns[11].ReadOnly = true;
                    dataGridView6.Columns[12].HeaderText = "С начала года, Факт";
                    dataGridView6.Columns[12].ReadOnly = true;
                    dataGridView6.Columns[13].HeaderText = "С начала года Ф/П, %";
                    dataGridView6.Columns[13].ReadOnly = true;
                    dataGridView6.Columns[14].HeaderText = "С начала года Ф/Ф(" + (year - 1) + "), %";
                    dataGridView6.Columns[14].ReadOnly = true;
                    dataGridView6.Columns[15].Visible = false;
                    dataGridView6.Columns[16].Visible = false;
                    foreach (DataGridViewRow row in dataGridView6.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                            row.Cells[11].Value = (float)row.Cells[15].Value + (float)row.Cells[6].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                            row.Cells[12].Value = (float)row.Cells[16].Value + (float)row.Cells[7].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()) && !String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                        {
                            row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[6].Value) * 100, 1);
                            row.Cells[9].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[4].Value) * 100, 1);
                        }
                    }
                    break;
                case 7:
                    List<SourceInputTable> SourceInputList1 = dbOps.GetSourceInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView7.DataSource = SourceInputList1;
                    dataGridView7.Columns[0].ReadOnly = true;
                    dataGridView7.Columns[1].HeaderText = "Наименование";
                    dataGridView7.Columns[1].ReadOnly = true;
                    dataGridView7.Columns[1].Width = 250;
                    dataGridView7.Columns[2].HeaderText = "Значение";
                    break;
                case 8:
                    List<RecievedInputTable> RecievedInputList1 = dbOps.GetRecievedInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView8.DataSource = RecievedInputList1;
                    dataGridView8.Columns[0].ReadOnly = true;
                    dataGridView8.Columns[1].HeaderText = "Наименование организации";
                    dataGridView8.Columns[1].ReadOnly = true;
                    dataGridView8.Columns[1].Width = 250;
                    dataGridView8.Columns[2].HeaderText = "Значение";
                    break;
                case 9:
                    List<SendedInputTable> SendedInputList1 = dbOps.GetSendedInputList(CurrentData.UserData.Id_org, curRepid, 3);
                    dataGridView9.DataSource = SendedInputList1;
                    dataGridView9.Columns[0].ReadOnly = true;
                    dataGridView9.Columns[1].HeaderText = "Наименование организации";
                    dataGridView9.Columns[1].ReadOnly = true;
                    dataGridView9.Columns[1].Width = 250;
                    dataGridView9.Columns[2].HeaderText = "Значение";
                    break;
                case 10:
                    button10.Enabled = true;
                    button10.Visible = true;
                    button6.Enabled = false;
                    button6.Visible = false;
                    List<NormTable> NormEllist = dbOps.GetNormInputList(CurrentData.UserData.Id_org, curRepid, curProfNum, 3, year, month);
                    List<NormTable> NormEllistSum = MakeListSumPR(year, month, 3);
                    repp = dbOps.GetReportData(CurrentData.UserData.Id_org, (year - 1), month);
                    List<NormTable> NormEllistOld = dbOps.GetNormInputList(CurrentData.UserData.Id_org, repp.id, repp.num, 3, (year - 1), month);
                    List<NormTable> NormEllistOldSum = MakeListSumPR((year - 1), month, 3);
                    List<NormInputTable> NormEllist1 = new List<NormInputTable>();
                    for (int i = 0; i < NormEllist.Count; i++)
                    {
                        var Factor = dbOps.GetFactorData(3, month, year);
                        var Fuel = dbOps.GetFuelData(NormEllist[i].fuel, year, month);
                        NormEllist1.Add(new NormInputTable
                        {
                            Id = NormEllist[i].Id,
                            Code = NormEllist[i].Code,
                            name = NormEllist[i].name,
                            Ed_izm = Fuel.unit,
                            val_fact_old = 0,
                            By = (float)Math.Round(Factor.value, 3),
                            val_plan = (float)Math.Round(NormEllist[i].val_plan, 1),
                            val_fact = (float)Math.Round(NormEllist[i].val_fact, 1),
                            val_fact_plan = 0,
                            val_fact_factold = 0,
                            val_fact_old_sum = 0,
                            val_plan_sum = (float)Math.Round(NormEllistSum[i].val_plan, 1) + (float)Math.Round(NormEllist[i].val_plan, 1),
                            val_fact_sum = (float)Math.Round(NormEllistSum[i].val_fact, 1) + (float)Math.Round(NormEllist[i].val_fact, 1),
                            val_fact_plan_sum = (float)Math.Round((((float)Math.Round(NormEllistSum[i].val_fact, 1) + (float)Math.Round(NormEllist[i].val_fact, 1)) / ((float)Math.Round(NormEllistSum[i].val_plan, 1) + (float)Math.Round(NormEllist[i].val_plan, 1))) * 100, 1),
                            val_fact_factold_sum = 0,
                            val_plan_sum_back = (float)Math.Round(NormEllistSum[i].val_plan, 1),
                            val_fact_sum_back = (float)Math.Round(NormEllistSum[i].val_fact, 1)
                        });
                    }
                    foreach (var a in NormEllist1)
                    {
                        int index = NormEllistOld.FindIndex(b => b.name == a.name);
                        if (!String.IsNullOrWhiteSpace(index.ToString()) && index >= 0)
                        {
                            a.val_fact_old = (float)Math.Round(NormEllistOld[index].val_fact, 1);
                            a.val_fact_old_sum = (float)Math.Round(NormEllistOldSum[index].val_fact, 1);
                            a.val_fact_factold_sum = (float)Math.Round(((a.val_fact_sum + a.val_fact) / (float)Math.Round(NormEllistOldSum[index].val_fact, 1)) * 100, 1);
                        }
                    }
                    dataGridView10.DataSource = NormEllist1;
                    dataGridView10.Columns[0].ReadOnly = true;
                    dataGridView10.Columns[0].Visible = false;
                    dataGridView10.Columns[1].HeaderText = " # ";
                    dataGridView10.Columns[1].ReadOnly = true;
                    dataGridView10.Columns[2].HeaderText = "Наименование";
                    dataGridView10.Columns[2].Width = 250;
                    dataGridView10.Columns[2].ReadOnly = true;
                    dataGridView10.Columns[3].HeaderText = "Ед. изм.";
                    dataGridView10.Columns[3].ReadOnly = true;
                    dataGridView10.Columns[4].HeaderText = "Ф, " + (year - 1);
                    dataGridView10.Columns[4].ReadOnly = true;
                    dataGridView10.Columns[5].HeaderText = "Ву";
                    dataGridView10.Columns[5].ReadOnly = true;
                    dataGridView10.Columns[6].HeaderText = "План";
                    dataGridView10.Columns[7].HeaderText = "Факт";
                    dataGridView10.Columns[8].HeaderText = "Ф/П, %";
                    dataGridView10.Columns[8].ReadOnly = true;
                    dataGridView10.Columns[9].HeaderText = "Ф/Ф(" + (year - 1) + "), %";
                    dataGridView10.Columns[9].ReadOnly = true;
                    dataGridView10.Columns[10].HeaderText = "С начала года Ф, " + (year - 1);
                    dataGridView10.Columns[10].ReadOnly = true;
                    dataGridView10.Columns[11].HeaderText = "С начала года, План";
                    dataGridView10.Columns[11].ReadOnly = true;
                    dataGridView10.Columns[12].HeaderText = "С начала года, Факт";
                    dataGridView10.Columns[12].ReadOnly = true;
                    dataGridView10.Columns[13].HeaderText = "С начала года Ф/П, %";
                    dataGridView10.Columns[13].ReadOnly = true;
                    dataGridView10.Columns[14].HeaderText = "С начала года Ф/Ф(" + (year - 1) + "), %";
                    dataGridView10.Columns[14].ReadOnly = true;
                    dataGridView10.Columns[15].Visible = false;
                    dataGridView10.Columns[16].Visible = false;
                    foreach (DataGridViewRow row in dataGridView10.Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                            row.Cells[11].Value = (float)row.Cells[15].Value + (float)row.Cells[6].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                            row.Cells[12].Value = (float)row.Cells[16].Value + (float)row.Cells[7].Value;
                        if (!String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()) && !String.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                        {
                            row.Cells[8].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[6].Value) * 100, 1);
                            row.Cells[9].Value = Math.Round(((float)row.Cells[7].Value / (float)row.Cells[4].Value) * 100, 1);
                        }
                    }
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

        private void ReportsCreateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Int32.Parse(e.Node.Tag.ToString()) != -1)
                tabControl1.SelectedIndex = Int32.Parse(e.Node.Tag.ToString());
        }

        void EditingControlShowing1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.CurrentCell.ColumnIndex == 6 || dataGridView.CurrentCell.ColumnIndex == 7)
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


        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel pan = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, pan.ClientRectangle, System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(203)))), ((int)(((byte)(227))))), ButtonBorderStyle.Solid);
        }
    }
}
