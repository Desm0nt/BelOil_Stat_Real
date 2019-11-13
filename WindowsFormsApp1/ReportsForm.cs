
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class ReportsForm : KryptonForm
    {
        public int year;
        public List<NormTable> actualList = new List<NormTable>();
        public List<NormTable> oldList = new List<NormTable>();
        public List<CompanyListTable> CompanyList = new List<CompanyListTable>();
        public ReportsForm()
        {
            InitializeComponent();
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            year = dateTimePicker1.Value.Year;
            MakeTable1per();
            MakeTable12tek();
            MakeTable12tekHidden();
            MakeTable12TekPril();
            CompanyList = dbOps.GetCompanyList();
            if (CurrentData.UserData.Id == 1)
            {
                CompanyBox.DataSource = CompanyList;
                CompanyBox.DisplayMember = "Name";
                CompanyBox.ValueMember = "Id";
                toolStrip3.Enabled = true;
            }
            LoadOrgTreeObjects();
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            toolStrip2.Size = new Size(70, 25);
            toolStrip1.Items.Insert(1, new ToolStripControlHost(this.dateTimePicker1));
            toolStrip1.Size = new Size(420, 25);
            toolStrip1.Location = new Point(71, 24);
            toolStrip3.Items.Insert(0, new ToolStripControlHost(this.CompanyBox));
            toolStrip3.Location = new Point(495, 24);
            yearButton.Text = DateTime.Now.Year.ToString();
            year1Button.Text = DateTime.Now.Year.ToString();
            year2Button.Text = (DateTime.Now.Year - 1).ToString();
            year3Button.Text = (DateTime.Now.Year - 2).ToString();
            toolStripDropDownButton1.Text = MakeQuaterText(DateTime.Now.Month);
            MonthBut.Text = "&" + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"] || tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                RUPButton.Enabled = true;
                POButton.Enabled = true;
            }
            else
            {
                RUPButton.Enabled = false;
                POButton.Enabled = false;
            }
        }

        void MakeTable1per()
        {
            reoGridControl1.Load("1per.xlsx");
            var worksheet1 = reoGridControl1.CurrentWorksheet;
            worksheet1.SetScale(0.94f);
            reoGridControl1.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet1.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet1.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);

            worksheet1["B2"] = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            worksheet1["F3"] = String.Format("Данные за {0} месяц {1} года", dateTimePicker1.Value.ToString("MMMM"), dateTimePicker1.Value.ToString("yyyy"));
            worksheet1["P5"] = CurrentData.UserData.Post;
            worksheet1["S6"] = CurrentData.UserData.Name;
            worksheet1["P7"] = DateTime.Now.ToString("dd.MM.yyyy");

            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            //var NormList = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
            //var NormListSum = MakeListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var NormList2 = dbOps.GetNormListPR(CurrentData.UserData.Id_org, report_id, profile_num, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
            var NormListSum = MakeListSumPR(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            List<NormTable> NormList = MakeListOnePR(NormList2, NormListSum);
            actualList = NormListSum;
            var OldNormListSum = MakeListSumPR(dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
            oldList = OldNormListSum;

            #region топливо
            int fuelrow = 15;
            int counter = 0;
            foreach (var a in NormList)
            {
                if (a.type == 1)
                    counter++;
            }
            if (counter >1)
                worksheet1.InsertRows(fuelrow, counter- 1);
            else
            {
                counter = 0;
                foreach (var a in NormListSum)
                {
                    if (a.type == 1)
                        counter++;
                }
                if (counter > 1)
                    worksheet1.InsertRows(fuelrow, counter - 1);
            }
            int tmp = 0;
            foreach (var a in NormList)
            {
                if (a.type == 1)
                {
                    worksheet1["C" + (fuelrow + tmp)] = a.name;
                    worksheet1["D" + (fuelrow + tmp)] = a.Code;
                    worksheet1["H" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));                    
                    worksheet1["J" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var zz1 = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan, 1));
                    var zz2 = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact, 1));
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    var z1 = worksheet1.Cells["H" + (fuelrow + tmp)].Data;
                    var z2 = worksheet1.Cells["J" + (fuelrow + tmp)].Data;
                    worksheet1[fuelrow - 1 + tmp, 8] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["H" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    //worksheet1[fuelrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    //worksheet1[fuelrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1["K" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["J" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    worksheet1["E" + (fuelrow + tmp)] = String.Format("=E{0}", fuelrow - 1); //dbOps.GetProdUnit(a.Id_prod); 
                    worksheet1["L" + (fuelrow + tmp)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", fuelrow + tmp);
                    worksheet1["M" + (fuelrow + tmp)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", fuelrow + tmp);
                    tmp++;
                }                 
            }
            tmp = 0;
            foreach (var a in NormListSum)
            {
                if (a.type == 1)
                {
                    worksheet1[fuelrow - 1 + tmp, 2] = a.name;
                    worksheet1[fuelrow - 1 + tmp, 3] = a.Code;
                    worksheet1["P" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));
                    worksheet1["R" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    //worksheet1["Q" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    worksheet1["Q" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan_ut,1));
                    worksheet1["S" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact_ut,1));
                    //worksheet1["S" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    worksheet1["E" + (fuelrow + tmp)] = String.Format("=E{0}", fuelrow - 1);
                    worksheet1["T" + (fuelrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", fuelrow + tmp);
                    worksheet1["U" + (fuelrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", fuelrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (fuelrow + tmp)] = Normo !=null? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact,1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (fuelrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact_ut, 1)) : String.Format("=ROUND({0}, 3)", 0);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(Math.Round(Norm.val_fact, 1) * Fuel.B_y, 1));
                    tmp++;
                }
            }
            var cell = worksheet1.Cells["G" + fuelrow];
            worksheet1["F" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(F{0}:F{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["G" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(G{0}:G{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["H" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(H{0}:H{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["I" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(I{0}:I{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["J" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(J{0}:J{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["K" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(K{0}:K{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["L" + (fuelrow - 1)] = String.Format("=ROUND(IF(I{0}>0, K{0}/I{0}, 0), 3)", fuelrow - 1);
            worksheet1["M" + (fuelrow - 1)] = String.Format("=ROUND(IF(G{0}>0, K{0}/G{0}, 0), 3)", fuelrow - 1);
            worksheet1["N" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(N{0}:N{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["O" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(O{0}:O{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["P" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(P{0}:P{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["Q" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(Q{0}:Q{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["R" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(R{0}:R{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["S" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(S{0}:S{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
            worksheet1["T" + (fuelrow - 1)] = String.Format("=ROUND(IF(Q{0}>0, S{0}/Q{0}, 0), 3)", fuelrow - 1);
            worksheet1["U" + (fuelrow - 1)] = String.Format("=ROUND(IF(O{0}>0, S{0}/O{0}, 0), 3)", fuelrow - 1);
#endregion

            #region тепло
            int heatrow = counter>0?fuelrow + counter + 1: fuelrow + counter + 2;
            counter = 0;
            foreach (var a in NormList)
            {
                if (a.type == 2)
                    counter++;
            }
            if (counter > 1)
                worksheet1.InsertRows(heatrow, counter - 1);
            else
            {
                counter = 0;
                foreach (var a in NormListSum)
                {
                    if (a.type == 2)
                        counter++;
                }
                if (counter > 1)
                    worksheet1.InsertRows(heatrow, counter - 1);
            }
            tmp = 0;
            foreach (var a in NormList)
            {
                if (a.type == 2)
                {
                    worksheet1["C" + (heatrow + tmp)] = a.name;
                    worksheet1["D" + (heatrow + tmp)] = a.Code;
                    worksheet1["H" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan, 1));
                    worksheet1["J" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact, 1));
                    var Factor = dbOps.GetFactorData(a.type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    worksheet1[heatrow - 1 + tmp, 8] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["H" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1[heatrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    //worksheet1[heatrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1[heatrow - 1 + tmp, 10] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["J" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["E" + (heatrow + tmp)] = String.Format("=E{0}", heatrow - 1);
                    worksheet1["L" + (heatrow + tmp)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", heatrow + tmp);
                    worksheet1["M" + (heatrow + tmp)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", heatrow + tmp);
                    tmp++;
                }
            }
            tmp = 0;
            foreach (var a in NormListSum)
            {
                if (a.type == 2)
                {
                    worksheet1[heatrow - 1 + tmp, 2] = a.name;
                    worksheet1[heatrow - 1 + tmp, 3] = a.Code;
                    worksheet1["P" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));
                    worksheet1["R" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Factor = dbOps.GetFactorData(a.type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    //worksheet1["Q" + (heatrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    //worksheet1["Q" + (heatrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1["S" + (heatrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    //worksheet1["S" + (heatrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["Q" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan_ut, 1));
                    worksheet1["S" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact_ut, 1));
                    worksheet1["E" + (heatrow + tmp)] = String.Format("=E{0}", heatrow - 1);
                    worksheet1["T" + (heatrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", heatrow + tmp);
                    worksheet1["U" + (heatrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", heatrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (heatrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact, 1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (heatrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact_ut, 1)) : String.Format("=ROUND({0}, 3)", 0);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (heatrow + tmp)] = String.Format("{0}", Math.Round(Math.Round(Norm.val_fact, 1) * Factor.value, 1));
                    tmp++;
                }
            }
            worksheet1["G" + (heatrow - 1)] = String.Format("=ROUND(SUM(G{0}:G{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["F" + (heatrow - 1)] = String.Format("=ROUND(SUM(F{0}:F{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["H" + (heatrow - 1)] = String.Format("=ROUND(SUM(H{0}:H{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["I" + (heatrow - 1)] = String.Format("=ROUND(SUM(I{0}:I{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["J" + (heatrow - 1)] = String.Format("=ROUND(SUM(J{0}:J{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["K" + (heatrow - 1)] = String.Format("=ROUND(SUM(K{0}:K{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["L" + (heatrow - 1)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", heatrow - 1);
            worksheet1["M" + (heatrow - 1)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", heatrow - 1);
            worksheet1["N" + (heatrow - 1)] = String.Format("=ROUND(SUM(N{0}:N{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["O" + (heatrow - 1)] = String.Format("=ROUND(SUM(O{0}:O{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["P" + (heatrow - 1)] = String.Format("=ROUND(SUM(P{0}:P{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["Q" + (heatrow - 1)] = String.Format("=ROUND(SUM(Q{0}:Q{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["R" + (heatrow - 1)] = String.Format("=ROUND(SUM(R{0}:R{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["S" + (heatrow - 1)] = String.Format("=ROUND(SUM(S{0}:S{1}), 3)", heatrow, heatrow + counter - 1);
            worksheet1["T" + (heatrow - 1)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", heatrow - 1);
            worksheet1["U" + (heatrow - 1)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", heatrow - 1);
            #endregion

            #region электроэнергия
            int elrow = counter > 0 ? heatrow + counter + 1 : heatrow + counter + 2;
            counter = 0;
            foreach (var a in NormList)
            {
                if (a.type == 3)
                    counter++;
            }
            if (counter > 1)
                worksheet1.InsertRows(elrow, counter - 1);
            else
            {
                counter = 0;
                foreach (var a in NormListSum)
                {
                    if (a.type == 3)
                        counter++;
                }
                if (counter > 1)
                    worksheet1.InsertRows(elrow, counter - 1);
            }
            tmp = 0;
            foreach (var a in NormList)
            {
                if (a.type == 3)
                {
                    worksheet1["C" + (elrow + tmp)] = a.name;
                    worksheet1["D" + (elrow + tmp)] = a.Code;
                    worksheet1["H" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan, 1));
                    worksheet1["J" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact, 1));
                    var Factor = dbOps.GetFactorData(a.type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    worksheet1[elrow - 1 + tmp, 8] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["H" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1[elrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", elrow + tmp, Factor.value);
                    //worksheet1[elrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1[elrow - 1 + tmp, 10] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["J" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["E" + (elrow + tmp)] = String.Format("=E{0}", elrow - 1);
                    worksheet1["L" + (elrow + tmp)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", elrow + tmp);
                    worksheet1["M" + (elrow + tmp)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", elrow + tmp);
                    tmp++;
                }
            }
            tmp = 0;
            foreach (var a in NormListSum)
            {
                if (a.type == 3)
                {
                    worksheet1[elrow - 1 + tmp, 2] = a.name;
                    worksheet1[elrow - 1 + tmp, 3] = a.Code;
                    worksheet1["P" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));
                    worksheet1["R" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Factor = dbOps.GetFactorData(a.type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    //worksheet1["Q" + (elrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", elrow + tmp, Factor.value);
                    //worksheet1["Q" + (elrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1["S" + (elrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", elrow + tmp, Factor.value);
                    //worksheet1["S" + (elrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["Q" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan_ut, 1));
                    worksheet1["S" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact_ut, 1));
                    worksheet1["E" + (elrow + tmp)] = String.Format("=E{0}", elrow - 1);
                    worksheet1["T" + (elrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", elrow + tmp);
                    worksheet1["U" + (elrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", elrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (elrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact,1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (elrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact_ut, 1)) : String.Format("=ROUND({0}, 3)", 0);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (elrow + tmp)] = String.Format("{0}", Math.Round(Math.Round(Norm.val_fact, 1) * Factor.value, 1));
                    tmp++;
                }
            }

            worksheet1["G" + (elrow - 1)] = String.Format("=ROUND(SUM(G{0}:G{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["F" + (elrow - 1)] = String.Format("=ROUND(SUM(F{0}:F{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["H" + (elrow - 1)] = String.Format("=ROUND(SUM(H{0}:H{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["I" + (elrow - 1)] = String.Format("=ROUND(SUM(I{0}:I{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["J" + (elrow - 1)] = String.Format("=ROUND(SUM(J{0}:J{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["K" + (elrow - 1)] = String.Format("=ROUND(SUM(K{0}:K{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["L" + (elrow - 1)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", elrow - 1);
            worksheet1["M" + (elrow - 1)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", elrow - 1);
            worksheet1["N" + (elrow - 1)] = String.Format("=ROUND(SUM(N{0}:N{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["O" + (elrow - 1)] = String.Format("=ROUND(SUM(O{0}:O{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["P" + (elrow - 1)] = String.Format("=ROUND(SUM(P{0}:P{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["Q" + (elrow - 1)] = String.Format("=ROUND(SUM(Q{0}:Q{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["R" + (elrow - 1)] = String.Format("=ROUND(SUM(R{0}:R{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["S" + (elrow - 1)] = String.Format("=ROUND(SUM(S{0}:S{1}), 3)", elrow, elrow + counter - 1);
            worksheet1["T" + (elrow - 1)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", elrow - 1);
            worksheet1["U" + (elrow - 1)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", elrow - 1);
            #endregion

            #region суммы
            worksheet1["F" + (elrow + counter)] = String.Format("=ROUND(SUM(F{0},F{1},F{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["F" + (elrow + counter + 1)] = String.Format("=F{0}", fuelrow - 1);
            worksheet1["F" + (elrow + counter + 2)] = String.Format("=F{0}", heatrow - 1);
            worksheet1["F" + (elrow + counter + 3)] = String.Format("=F{0}", elrow - 1);
            worksheet1["G" + (elrow + counter)] = String.Format("=ROUND(SUM(G{0},G{1},G{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["G" + (elrow + counter + 1)] = String.Format("=G{0}", fuelrow - 1);
            worksheet1["G" + (elrow + counter + 2)] = String.Format("=G{0}", heatrow - 1);
            worksheet1["G" + (elrow + counter + 3)] = String.Format("=G{0}", elrow - 1);
            worksheet1["H" + (elrow + counter)] = String.Format("=ROUND(SUM(H{0},H{1},H{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["H" + (elrow + counter + 1)] = String.Format("=H{0}", fuelrow - 1);
            worksheet1["H" + (elrow + counter + 2)] = String.Format("=H{0}", heatrow - 1);
            worksheet1["H" + (elrow + counter + 3)] = String.Format("=H{0}", elrow - 1);
            worksheet1["I" + (elrow + counter)] = String.Format("=ROUND(SUM(I{0},I{1},I{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["I" + (elrow + counter + 1)] = String.Format("=I{0}", fuelrow - 1);
            worksheet1["I" + (elrow + counter + 2)] = String.Format("=I{0}", heatrow - 1);
            worksheet1["I" + (elrow + counter + 3)] = String.Format("=I{0}", elrow - 1);
            worksheet1["J" + (elrow + counter)] = String.Format("=ROUND(SUM(J{0},J{1},J{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["J" + (elrow + counter + 1)] = String.Format("=J{0}", fuelrow - 1);
            worksheet1["J" + (elrow + counter + 2)] = String.Format("=J{0}", heatrow - 1);
            worksheet1["J" + (elrow + counter + 3)] = String.Format("=J{0}", elrow - 1);
            worksheet1["K" + (elrow + counter)] = String.Format("=ROUND(SUM(K{0},K{1},K{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["K" + (elrow + counter + 1)] = String.Format("=K{0}", fuelrow - 1);
            worksheet1["K" + (elrow + counter + 2)] = String.Format("=K{0}", heatrow - 1);
            worksheet1["K" + (elrow + counter + 3)] = String.Format("=K{0}", elrow - 1);
            worksheet1["N" + (elrow + counter)] = String.Format("=ROUND(SUM(N{0},N{1},N{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["N" + (elrow + counter + 1)] = String.Format("=N{0}", fuelrow - 1);
            worksheet1["N" + (elrow + counter + 2)] = String.Format("=N{0}", heatrow - 1);
            worksheet1["N" + (elrow + counter + 3)] = String.Format("=N{0}", elrow - 1);
            worksheet1["O" + (elrow + counter)] = String.Format("=ROUND(SUM(O{0},O{1},O{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["O" + (elrow + counter + 1)] = String.Format("=O{0}", fuelrow - 1);
            worksheet1["O" + (elrow + counter + 2)] = String.Format("=O{0}", heatrow - 1);
            worksheet1["O" + (elrow + counter + 3)] = String.Format("=O{0}", elrow - 1);
            worksheet1["P" + (elrow + counter)] = String.Format("=ROUND(SUM(P{0},P{1},P{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["P" + (elrow + counter + 1)] = String.Format("=P{0}", fuelrow - 1);
            worksheet1["P" + (elrow + counter + 2)] = String.Format("=P{0}", heatrow - 1);
            worksheet1["P" + (elrow + counter + 3)] = String.Format("=P{0}", elrow - 1);
            worksheet1["Q" + (elrow + counter)] = String.Format("=ROUND(SUM(Q{0},Q{1},Q{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["Q" + (elrow + counter + 1)] = String.Format("=Q{0}", fuelrow - 1);
            worksheet1["Q" + (elrow + counter + 2)] = String.Format("=Q{0}", heatrow - 1);
            worksheet1["Q" + (elrow + counter + 3)] = String.Format("=Q{0}", elrow - 1);
            worksheet1["R" + (elrow + counter)] = String.Format("=ROUND(SUM(R{0},R{1},R{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["R" + (elrow + counter + 1)] = String.Format("=R{0}", fuelrow - 1);
            worksheet1["R" + (elrow + counter + 2)] = String.Format("=R{0}", heatrow - 1);
            worksheet1["R" + (elrow + counter + 3)] = String.Format("=R{0}", elrow - 1);
            worksheet1["S" + (elrow + counter)] = String.Format("=ROUND(SUM(S{0},S{1},S{2}), 3)", fuelrow - 1, heatrow - 1, elrow - 1);
            worksheet1["S" + (elrow + counter + 1)] = String.Format("=S{0}", fuelrow - 1);
            worksheet1["S" + (elrow + counter + 2)] = String.Format("=S{0}", heatrow - 1);
            worksheet1["S" + (elrow + counter + 3)] = String.Format("=S{0}", elrow - 1);
            worksheet1["L" + (elrow + counter)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", (elrow + counter));
            worksheet1["L" + (elrow + counter + 1)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", (elrow + counter + 1));
            worksheet1["L" + (elrow + counter + 2)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", (elrow + counter + 2));
            worksheet1["L" + (elrow + counter + 3)] = String.Format("=ROUND(IF(H{0}>0, J{0}/H{0}, 0), 3)", (elrow + counter + 3));
            worksheet1["M" + (elrow + counter)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", (elrow + counter));
            worksheet1["M" + (elrow + counter + 1)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", (elrow + counter + 1));
            worksheet1["M" + (elrow + counter + 2)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", (elrow + counter + 2));
            worksheet1["M" + (elrow + counter + 3)] = String.Format("=ROUND(IF(F{0}>0, J{0}/F{0}, 0), 3)", (elrow + counter + 3));
            worksheet1["T" + (elrow + counter)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", (elrow + counter));
            worksheet1["T" + (elrow + counter + 1)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", (elrow + counter + 1));
            worksheet1["T" + (elrow + counter + 2)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", (elrow + counter + 2));
            worksheet1["T" + (elrow + counter + 3)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", (elrow + counter + 3));
            worksheet1["U" + (elrow + counter)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", (elrow + counter));
            worksheet1["U" + (elrow + counter + 1)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", (elrow + counter + 1));
            worksheet1["U" + (elrow + counter + 2)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", (elrow + counter + 2));
            worksheet1["U" + (elrow + counter + 3)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", (elrow + counter + 3));
            #endregion

            var a1 = worksheet1.Cells["U" + (elrow + counter + 3)].Data;

            #region style
            var grid = this.reoGridControl1;
            var sheet = grid.CurrentWorksheet;
            sheet.SetRangeStyles("A" + fuelrow + ":U" + (elrow + counter), new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                FontSize = 8,
                FontName = "Times New Roman",                
            });
            sheet.SetRangeDataFormat("L" + (fuelrow - 1) + ":L" + (elrow + counter + 4), CellDataFormatFlag.Percent);
            sheet.SetRangeDataFormat("M" + (fuelrow - 1) + ":M" + (elrow + counter + 4), CellDataFormatFlag.Percent);
            sheet.SetRangeDataFormat("T" + (fuelrow - 1) + ":T" + (elrow + counter + 4), CellDataFormatFlag.Percent);
            sheet.SetRangeDataFormat("U" + (fuelrow - 1) + ":U" + (elrow + counter + 4), CellDataFormatFlag.Percent);
            sheet.SetRangeStyles("E" + (fuelrow - 1) + ":E" + (elrow + counter + 4), new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Center,
            });

            worksheet1.SetRangeBorders(new RangePosition(fuelrow, 1, (elrow + counter - fuelrow -1), 20), BorderPositions.All, new RangeBorderStyle
            {
                Color = System.Drawing.ColorTranslator.FromHtml("#000000"),
                Style = BorderLineStyle.Solid,
            });
            worksheet1.SetRangeBorders(new RangePosition(fuelrow, 1, (elrow + counter - fuelrow - 1), 0), BorderPositions.Left, new RangeBorderStyle
            {
                Color = System.Drawing.ColorTranslator.FromHtml("#000000"),
                Style = BorderLineStyle.BoldSolid,
            });
            worksheet1.SetRangeBorders(new RangePosition(fuelrow, 21, (elrow + counter - fuelrow - 1), 0), BorderPositions.Left, new RangeBorderStyle
            {
                Color = System.Drawing.ColorTranslator.FromHtml("#000000"),
                Style = BorderLineStyle.BoldSolid,
            });
            worksheet1.SetRangeBorders(new RangePosition((elrow + counter - 1), 1, 0, 20), BorderPositions.Bottom, new RangeBorderStyle
            {
                Color = System.Drawing.ColorTranslator.FromHtml("#000000"),
                Style = BorderLineStyle.BoldSolid,
            });
            for (int i = 0; i <= 66; i++)
            {
                worksheet1.AutoFitRowHeight(i, true);
            }
            #endregion
        }

        void MakeTable12tek()
        {
            reoGridControl2.Load("12tek_s.xlsx");
            var worksheet2 = reoGridControl2.CurrentWorksheet;
            worksheet2.SetScale(0.92f);
            reoGridControl2.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet2.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet2.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);

            #region топливо
            float actSum = 0;
            float actSum_ut = 0;
            float actSum_111 = 0;
            float actSum_112 = 0;
            float oldSum = 0;
            float oldSum_111 = 0;
            float oldSum_112 = 0;
            float oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1)
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1)
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
                    oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet2["C12"] = actSum_ut;
            worksheet2["H12"] = oldSum_ut;
            worksheet2["C13"] = actSum_111;
            worksheet2["H13"] = oldSum_111;
            worksheet2["C14"] = actSum_112;
            worksheet2["H14"] = oldSum_112;
            #endregion

            var TFuelListSum = MakeTFuelSumPR(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldTFuelListSum = MakeTFuelSumPR(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло продано всего
            float TFuelSum = 0;
            float OldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                    TFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
            }
            foreach (var a in OldTFuelListSum)
            {
                OldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
            }
            worksheet2["C17"] = TFuelSum;
            worksheet2["H17"] = OldTFuelSum;
            #endregion

            #region тепло продано местное
            float mTFuelSum = 0;
            float mOldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                {
                    mTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldTFuelListSum)
            {
                if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                {
                    mOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["D17"] = mTFuelSum;
            worksheet2["I17"] = mOldTFuelSum;
            #endregion

            #region тепло продано местное
            float vTFuelSum = 0;
            float vOldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                {
                    vTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));

                }
            }
            foreach (var a in OldTFuelListSum)
            {
                if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                {
                    vOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["E17"] = vTFuelSum;
            worksheet2["J17"] = vOldTFuelSum;
            #endregion

            #region топливо местное
            float mestn_actSum = 0;
            float mestn_actSum_ut = 0;
            float mestn_actSum_111 = 0;
            float mestn_actSum_112 = 0;
            float mestn_oldSum = 0;
            float mestn_oldSum_111 = 0;
            float mestn_oldSum_112 = 0;
            float mestn_oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1 && (a.fuel > 2100 && a.fuel < 4000))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    mestn_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    mestn_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    if (a.row_options.Count() == 2)
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));

                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1 && (a.fuel > 2100 && a.fuel < 4000))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
                    mestn_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    mestn_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    if (a.row_options.Count() == 2)
                    {
                        mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet2["D12"] = mestn_actSum_ut;
            worksheet2["I12"] = mestn_oldSum_ut;
            worksheet2["D13"] = mestn_actSum_111;
            worksheet2["I13"] = mestn_oldSum_111;
            worksheet2["D14"] = mestn_actSum_112;
            worksheet2["I14"] = mestn_oldSum_112;
            #endregion

            #region топливо отходы
            float oth_actSum = 0;
            float oth_actSum_ut = 0;
            float oth_actSum_111 = 0;
            float oth_actSum_112 = 0;
            float oth_oldSum = 0;
            float oth_oldSum_111 = 0;
            float oth_oldSum_112 = 0;
            float oth_oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1 && ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000)))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    oth_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oth_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1 && ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000)))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
                    oth_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oth_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet2["E12"] = oth_actSum_ut;
            worksheet2["J12"] = oth_oldSum_ut;
            worksheet2["E13"] = oth_actSum_111;
            worksheet2["J13"] = oth_oldSum_111;
            worksheet2["E14"] = oth_actSum_112;
            worksheet2["J14"] = oth_oldSum_112;
            #endregion

            #region тепло
            float actSum2 = 0;
            float actSum2_111 = 0;
            oldSum = 0;
            oldSum_111 = 0;
            foreach (var a in actualList)
            {
                if (a.type == 2)
                {
                    actSum2 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 2)
                {
                    oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            worksheet2["F12"] = actSum2;
            worksheet2["K12"] = oldSum;
            worksheet2["F13"] = actSum2_111;
            worksheet2["K13"] = oldSum_111;
            #endregion

            #region электричество
            float actSum3 = 0;
            float actSum3_111 = 0;
            float actSum3_112 = 0;
            float oldSum3 = 0;
            float oldSum3_111 = 0;
            float oldSum3_112 = 0;
            foreach (var a in actualList)
            {
                if (a.type == 3)
                {
                    actSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 3)
                {
                    oldSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            worksheet2["G12"] = actSum3;
            worksheet2["L12"] = oldSum3;
            worksheet2["G13"] = actSum3_111;
            worksheet2["L13"] = oldSum3_111;
            worksheet2["G14"] = actSum3_112;
            worksheet2["L14"] = oldSum3_112;
            #endregion

            var RecievedListSum = MakeRecListSumPR(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldRecievedListSum = MakeRecListSumPR(dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
                
            #region тепло получено
            float tRecSum = 0;
            float tOldRecSum = 0;
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 2)
                {
                    tRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldRecievedListSum)
            {
                if (a.res_type == 2)
                {
                    tOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet2["F21"] = tRecSum;
            worksheet2["K21"] = tOldRecSum;
            #endregion

            #region электричество получено
            float eRecSum = 0;
            float eOldRecSum = 0;
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 3)
                {
                    eRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldRecievedListSum)
            {
                if (a.res_type == 3)
                {
                    eOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet2["G21"] = eRecSum;
            worksheet2["L21"] = eOldRecSum;
            #endregion

            var SendedListSum = MakeSendListSumPR(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldSendedListSum = MakeSendListSumPR(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло отдано
            float tSendSum = 0;
            float tOldSendSum = 0;
            foreach (var a in SendedListSum)
            {
                if (a.res_type == 2)
                {
                    tSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldSendedListSum)
            {
                if (a.res_type == 2)
                {
                    tOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet2["F16"] = tSendSum;
            worksheet2["K16"] = tOldSendSum;
            #endregion

            #region электричество отдано
            float eSendSum = 0;
            float eOldSendSum = 0;
            foreach (var a in SendedListSum)
            {
                if (a.res_type == 3)
                {
                    eSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldSendedListSum)
            {
                if (a.res_type == 3)
                {
                    eOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }

            worksheet2["G16"] = eSendSum;
            worksheet2["L16"] = eOldSendSum;
            #endregion

            var SourceSum = MakeSourceSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldSourceSum = MakeSourceSumPR(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            var a1 = SourceSum;
            var a2 = OldSourceSum;

            #region получено собственного тепла
            float tSourceSum = 0;
            float tOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2)
                {
                    tSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2)
                {
                    tOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["F18"] = tSourceSum;
            worksheet2["K18"] = tOldSourceSum;
            #endregion

            #region получено собственного тепла от ВЭР
            float tvSourceSum = 0;
            float tvOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 4000)
                {
                    tvSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 4000)
                {
                    tvOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["F19"] = tvSourceSum;
            worksheet2["K19"] = tvOldSourceSum;
            #endregion

            #region получено собственного тепла от солнца
            float tsSourceSum = 0;
            float tsOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group==5000)
                {
                    tsSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 5000)
                {
                    tsOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["F20"] = tsSourceSum;
            worksheet2["K20"] = tsOldSourceSum;
            #endregion

            #region получено собственного электричества
            float eSourceSum = 0;
            float eOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3)
                {
                    eSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 3)
                {
                    eOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["G18"] = eSourceSum;
            worksheet2["L18"] = eOldSourceSum;
            #endregion

            #region получено собственного электричества от ВЭР
            float evSourceSum = 0;
            float evOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 4000)
                {
                    evSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 4000)
                {
                    evOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["G19"] = evSourceSum;
            worksheet2["L19"] = evOldSourceSum;
            #endregion

            #region получено собственного электричества от солнца
            float esSourceSum = 0;
            float esOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 5000)
                {
                    esSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 5000)
                {
                    esOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet2["G20"] = esSourceSum;
            worksheet2["L20"] = esOldSourceSum;
            #endregion

            var TradesSum = MakeTradeSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            foreach (var a in TradesSum)
            {
                if (a.type == 2)
                    worksheet2["F17"] = Convert.ToSingle(Math.Round(a.value, 1));
                else if (a.type == 3)
                    worksheet2["G17"] = Convert.ToSingle(Math.Round(a.value, 1));
            }
            var OldTradesSum = MakeTradeSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
            foreach (var a in OldTradesSum)
            {
                if (a.type == 2)
                    worksheet2["K17"] = Convert.ToSingle(Math.Round(a.value, 1));
                else if (a.type == 3)
                    worksheet2["L17"] = Convert.ToSingle(Math.Round(a.value, 1));
            }

            worksheet2["F30"] = "=SUM(ROUND(C12, 0), ROUND(((F12-F18)*0,143), 0), ROUND(((G12-G18)*0,123), 0))";
            worksheet2["H30"] = "=SUM(ROUND(H12, 0), ROUND(((K12-K18)*0,143), 0), ROUND(((L12-L18)*0,123), 0))";

            worksheet2["F15"] = 0;
            worksheet2["G15"] = 0;
            worksheet2["K15"] = 0;
            worksheet2["L15"] = 0;

            for (int i = 0; i <= 66; i++)
            {
                worksheet2.AutoFitRowHeight(i, true);
            }
        }

        void MakeTable12teSUM(bool flag)
        {
            reoGridControl2.Load("12tek_s.xlsx");
            var worksheet2 = reoGridControl2.CurrentWorksheet;
            worksheet2.SetScale(0.92f);
            reoGridControl2.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet2.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet2.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);
            List<int> CompanyIdList = new List<int>();
            if (flag == false)
                CompanyIdList = dbOps.GetCompanyIdList(100);
            else if (flag == true)
            {
                CompanyIdList = dbOps.GetCompanyIdList(100);
                var list2 = dbOps.GetCompanyIdList(200);
                foreach (var c in list2)
                    CompanyIdList.Add(c);
            }
            var CurrentID = CurrentData.UserData.Id_org;

            #region топливо, тепло и электричество
            float actSum = 0;
            float actSum_ut = 0;
            float actSum_111 = 0;
            float actSum_112 = 0;
            float oldSum = 0;
            float oldSum_111 = 0;
            float oldSum_112 = 0;
            float oldSum_ut = 0;

            float actSum3 = 0;
            float actSum3_111 = 0;
            float actSum3_112 = 0;
            float oldSum3 = 0;
            float oldSum3_111 = 0;
            float oldSum3_112 = 0;

            float actSum2 = 0;
            float actSum2_111 = 0;
            float oldSum2 = 0;
            float oldSum2_111 = 0;

            float mestn_actSum = 0;
            float mestn_actSum_ut = 0;
            float mestn_actSum_111 = 0;
            float mestn_actSum_112 = 0;
            float mestn_oldSum = 0;
            float mestn_oldSum_111 = 0;
            float mestn_oldSum_112 = 0;
            float mestn_oldSum_ut = 0;

            float oth_actSum = 0;
            float oth_actSum_ut = 0;
            float oth_actSum_111 = 0;
            float oth_actSum_112 = 0;
            float oth_oldSum = 0;
            float oth_oldSum_111 = 0;
            float oth_oldSum_112 = 0;
            float oth_oldSum_ut = 0;

            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var NormListSum = MakeListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                var actualList1 = NormListSum;
                var OldNormListSum = MakeListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                var oldList1 = OldNormListSum;

                foreach (var a in actualList1)
                {
                    if (a.type == 1)
                    {
                        var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                        actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        if (a.row_options.Count() == 2)
                        {
                            actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }

                        if (a.fuel > 2100 && a.fuel < 4000)
                        {
                            mestn_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            mestn_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            if (a.row_options.Count() == 2)
                            {
                                mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                                mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));

                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                            {
                                mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                            {
                                mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                        }
                        if ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000))
                        {
                            oth_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            oth_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                            if (a.row_options.Count() == 2)
                            {
                                oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                                oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                            {
                                oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                            {
                                oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                        }
                    }
                    else if (a.type == 2)
                    {
                        actSum2 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        if (a.row_options.Count() == 2)
                        {
                            actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                    }
                    else if (a.type == 3)
                    {
                        actSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        if (a.row_options.Count() == 2)
                        {
                            actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                    }

                }
                foreach (var a in oldList1)
                {
                    if (a.type == 1)
                    {
                        var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                        oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        if (a.row_options.Count() == 2)
                        {
                            oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        }
                        if (a.fuel > 2100 && a.fuel < 4000)
                        {
                            mestn_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            mestn_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            if (a.row_options.Count() == 2)
                            {
                                mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                                mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                            {
                                mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                            {
                                mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                        }
                        if ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000))
                        {
                            oth_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            oth_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                            if (a.row_options.Count() == 2)
                            {
                                oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                                oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                            {
                                oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                            else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                            {
                                oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                            }
                        }
                    }
                    else if (a.type == 2)
                    {
                        oldSum2 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        if (a.row_options.Count() == 2)
                        {
                            oldSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            oldSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            oldSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                    }
                    else if (a.type == 3)
                    {
                        oldSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        if (a.row_options.Count() == 2)
                        {
                            oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                            oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                        {
                            oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                        else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                        {
                            oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        }
                    }
                }
            }
            worksheet2["C12"] = actSum_ut;
            worksheet2["H12"] = oldSum_ut;
            worksheet2["C13"] = actSum_111;
            worksheet2["H13"] = oldSum_111;
            worksheet2["C14"] = actSum_112;
            worksheet2["H14"] = oldSum_112;

            worksheet2["G12"] = actSum3;
            worksheet2["L12"] = oldSum3;
            worksheet2["G13"] = actSum3_111;
            worksheet2["L13"] = oldSum3_111;
            worksheet2["G14"] = actSum3_112;
            worksheet2["L14"] = oldSum3_112;

            worksheet2["F12"] = actSum2;
            worksheet2["K12"] = oldSum2;
            worksheet2["F13"] = actSum2_111;
            worksheet2["K13"] = oldSum2_111;

            worksheet2["D12"] = mestn_actSum_ut;
            worksheet2["I12"] = mestn_oldSum_ut;
            worksheet2["D13"] = mestn_actSum_111;
            worksheet2["I13"] = mestn_oldSum_111;
            worksheet2["D14"] = mestn_actSum_112;
            worksheet2["I14"] = mestn_oldSum_112;

            worksheet2["E12"] = oth_actSum_ut;
            worksheet2["J12"] = oth_oldSum_ut;
            worksheet2["E13"] = oth_actSum_111;
            worksheet2["J13"] = oth_oldSum_111;
            worksheet2["E14"] = oth_actSum_112;
            worksheet2["J14"] = oth_oldSum_112;
            #endregion

            #region тепло продано

            float TFuelSum = 0;
            float OldTFuelSum = 0;
            float mTFuelSum = 0;
            float mOldTFuelSum = 0;
            float vTFuelSum = 0;
            float vOldTFuelSum = 0;
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var TFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                var OldTFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                foreach (var a in TFuelListSum)
                {
                    TFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                    {
                        mTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                    {
                        vTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));

                    }
                }
                foreach (var a in OldTFuelListSum)
                {
                    OldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                    {
                        mOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                    {
                        vOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                }

            }        

            worksheet2["C17"] = TFuelSum;
            worksheet2["H17"] = OldTFuelSum;
            worksheet2["D17"] = mTFuelSum;
            worksheet2["I17"] = mOldTFuelSum;
            worksheet2["E17"] = vTFuelSum;
            worksheet2["J17"] = vOldTFuelSum;
            #endregion

            #region тепло и электричество получено

            float tRecSum = 0;
            float tOldRecSum = 0;
            float eRecSum = 0;
            float eOldRecSum = 0;
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var RecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                var OldRecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

                foreach (var a in RecievedListSum)
                {
                    if (a.res_type == 2)
                    {
                        tRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                    if (a.res_type == 3)
                    {
                        eRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                }
                foreach (var a in OldRecievedListSum)
                {
                    if (a.res_type == 2)
                    {
                        tOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                    if (a.res_type == 3)
                    {
                        eOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                }
            }
            worksheet2["F21"] = tRecSum;
            worksheet2["K21"] = tOldRecSum;
            worksheet2["G21"] = eRecSum;
            worksheet2["L21"] = eOldRecSum;

            #endregion

            #region тепло и электричество отдано
            float tSendSum = 0;
            float tOldSendSum = 0;
            float eSendSum = 0;
            float eOldSendSum = 0;
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var SendedListSum = MakeSendListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                var OldSendedListSum = MakeSendListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

                foreach (var a in SendedListSum)
                {
                    if (a.res_type == 2)
                    {
                        tSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                    if (a.res_type == 3)
                    {
                        eSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                }
                foreach (var a in OldSendedListSum)
                {
                    if (a.res_type == 2)
                    {
                        tOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                    if (a.res_type == 3)
                    {
                        eOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                    }
                }
            }
            worksheet2["F16"] = tSendSum;
            worksheet2["K16"] = tOldSendSum;
            worksheet2["G16"] = eSendSum;
            worksheet2["L16"] = eOldSendSum;
            #endregion

            #region получено собственного тепла и электричества
            float tSourceSum = 0;
            float tOldSourceSum = 0;
            float tvSourceSum = 0;
            float tvOldSourceSum = 0;
            float tsSourceSum = 0;
            float tsOldSourceSum = 0;
            float eSourceSum = 0;
            float eOldSourceSum = 0;
            float evSourceSum = 0;
            float evOldSourceSum = 0;
            float esSourceSum = 0;
            float esOldSourceSum = 0;
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var SourceSum = MakeSourceSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                var OldSourceSum = MakeSourceSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                foreach (var a in SourceSum)
                {
                    if (a.Res_type == 2)
                    {
                        tSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 2 && a.Fuel_group == 4000)
                    {
                        tvSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 2 && a.Fuel_group == 5000)
                    {
                        tsSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3)
                    {
                        eSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3 && a.Fuel_group == 4000)
                    {
                        evSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3 && a.Fuel_group == 5000)
                    {
                        esSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                }
                foreach (var a in OldSourceSum)
                {
                    if (a.Res_type == 2)
                    {
                        tOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 2 && a.Fuel_group == 4000)
                    {
                        tvOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 2 && a.Fuel_group == 5000)
                    {
                        tsOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3)
                    {
                        eOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3 && a.Fuel_group == 4000)
                    {
                        evOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                    if (a.Res_type == 3 && a.Fuel_group == 5000)
                    {
                        esOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    }
                }
            }
            worksheet2["F18"] = tSourceSum;
            worksheet2["K18"] = tOldSourceSum;
            worksheet2["F19"] = tvSourceSum;
            worksheet2["K19"] = tvOldSourceSum;
            worksheet2["F20"] = tsSourceSum;
            worksheet2["K20"] = tsOldSourceSum;
            worksheet2["G18"] = eSourceSum;
            worksheet2["L18"] = eOldSourceSum;
            worksheet2["G19"] = evSourceSum;
            worksheet2["L19"] = evOldSourceSum;
            worksheet2["G20"] = esSourceSum;
            worksheet2["L20"] = esOldSourceSum;
            #endregion

            #region продано населению
            float actTradeSumH = 0;
            float oldTradeSumH = 0;
            float actTradeSumE = 0;
            float oldTradeSumE = 0;
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var TradesSum = MakeTradeSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                foreach (var a in TradesSum)
                {
                    if (a.type == 2)
                        actTradeSumH += Convert.ToSingle(Math.Round(a.value, 1));
                    else if (a.type == 3)
                        actTradeSumE += Convert.ToSingle(Math.Round(a.value, 1));
                }
                var OldTradesSum = MakeTradeSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                foreach (var a in OldTradesSum)
                {
                    if (a.type == 2)
                        oldTradeSumH += Convert.ToSingle(Math.Round(a.value, 1));
                    else if (a.type == 3)
                        oldTradeSumE += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet2["F17"] = actTradeSumH;
            worksheet2["G17"] = oldTradeSumH;
            worksheet2["K17"] = actTradeSumE;
            worksheet2["L17"] = oldTradeSumE;

            worksheet2["F30"] = "=SUM(ROUND(C12, 0), ROUND(((F12-F18)*0,143), 0), ROUND(((G12-G18)*0,123), 0))";
            worksheet2["H30"] = "=SUM(ROUND(H12, 0), ROUND(((K12-K18)*0,143), 0), ROUND(((L12-L18)*0,123), 0))";

            worksheet2["F15"] = 0;
            worksheet2["G15"] = 0;
            worksheet2["K15"] = 0;
            worksheet2["L15"] = 0;
            #endregion

            for (int i = 0; i <= 66; i++)
            {
                worksheet2.AutoFitRowHeight(i, true);
            }
            CurrentData.UserData.Id_org = CurrentID;
        }

        void MakeTable12tekHidden()
        {
            reoGridControl3.Load("12tek_full.xlsx");
            var worksheet3 = reoGridControl3.Worksheets[0];
            worksheet3.SetScale(0.92f);
            reoGridControl3.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet3.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet3.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);
            var dt = DateTime.Now.ToString("dd MMMM yyyy");
            worksheet3["Y37"] = dt;


            #region топливо
            float actSum = 0;
            float actSum_ut = 0;
            float actSum_111 = 0;
            float actSum_112 = 0;
            float oldSum = 0;
            float oldSum_111 = 0;
            float oldSum_112 = 0;
            float oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1)
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1)
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet3["T12"] = actSum_ut;
            worksheet3["Y12"] = oldSum_ut;
            worksheet3["T13"] = actSum_111;
            worksheet3["Y13"] = oldSum_111;
            worksheet3["T14"] = actSum_112;
            worksheet3["Y14"] = oldSum_112;
            #endregion

            var TFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldTFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло продано всего
            float TFuelSum = 0;
            float OldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                TFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
            }
            foreach (var a in OldTFuelListSum)
            {
                OldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
            }
            worksheet3["T17"] = TFuelSum;
            worksheet3["Y17"] = OldTFuelSum;
            #endregion

            #region тепло продано местное
            float mTFuelSum = 0;
            float mOldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                {
                    mTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldTFuelListSum)
            {
                if (a.Fuel_group >= 2100 && a.Fuel_group <= 4000)
                {
                    mOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["U17"] = mTFuelSum;
            worksheet3["Z17"] = mOldTFuelSum;
            #endregion

            #region тепло продано местное
            float vTFuelSum = 0;
            float vOldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                {
                    vTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));

                }
            }
            foreach (var a in OldTFuelListSum)
            {
                if ((a.Fuel_group >= 2200 && a.Fuel_group <= 3100) || (a.Fuel_group >= 3100 && a.Fuel_group <= 4000))
                {
                    vOldTFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["V17"] = vTFuelSum;
            worksheet3["AA17"] = vOldTFuelSum;
            #endregion

            #region топливо местное
            float mestn_actSum = 0;
            float mestn_actSum_ut = 0;
            float mestn_actSum_111 = 0;
            float mestn_actSum_112 = 0;
            float mestn_oldSum = 0;
            float mestn_oldSum_111 = 0;
            float mestn_oldSum_112 = 0;
            float mestn_oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1 && (a.fuel > 2100 && a.fuel < 4000))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    mestn_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    mestn_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));

                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1 && (a.fuel > 2100 && a.fuel < 4000))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    mestn_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    mestn_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        mestn_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        mestn_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet3["U12"] = mestn_actSum_ut;
            worksheet3["Z12"] = mestn_oldSum_ut;
            worksheet3["U13"] = mestn_actSum_111;
            worksheet3["Z13"] = mestn_oldSum_111;
            worksheet3["U14"] = mestn_actSum_112;
            worksheet3["Z14"] = mestn_oldSum_112;
            #endregion

            #region топливо отходы
            float oth_actSum = 0;
            float oth_actSum_ut = 0;
            float oth_actSum_111 = 0;
            float oth_actSum_112 = 0;
            float oth_oldSum = 0;
            float oth_oldSum_111 = 0;
            float oth_oldSum_112 = 0;
            float oth_oldSum_ut = 0;
            foreach (var a in actualList)
            {
                if (a.type == 1 && ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000)))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
                    oth_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oth_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 1 && ((a.fuel > 2200 && a.fuel < 3100) || (a.fuel > 3100 && a.fuel < 4000)))
                {
                    var Fuel = dbOps.GetFuelData(a.fuel, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    oth_oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oth_oldSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact_ut, 1))), 1));
                    if (a.row_options.Count() == 2)
                    {
                        oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oth_oldSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oth_oldSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    }
                }
            }
            worksheet3["V12"] = oth_actSum_ut;
            worksheet3["AA12"] = oth_oldSum_ut;
            worksheet3["V13"] = oth_actSum_111;
            worksheet3["AA13"] = oth_oldSum_111;
            worksheet3["V14"] = oth_actSum_112;
            worksheet3["AA14"] = oth_oldSum_112;
            #endregion

            #region тепло
            float actSum2 = 0;
            float actSum2_111 = 0;
            oldSum = 0;
            oldSum_111 = 0;
            foreach (var a in actualList)
            {
                if (a.type == 2)
                {
                    actSum2 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 2)
                {
                    oldSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            worksheet3["W12"] = actSum2;
            worksheet3["AB12"] = oldSum;
            worksheet3["W13"] = actSum2_111;
            worksheet3["AB13"] = oldSum_111;
            #endregion

            #region электричество
            float actSum3 = 0;
            float actSum3_111 = 0;
            float actSum3_112 = 0;
            float oldSum3 = 0;
            float oldSum3_111 = 0;
            float oldSum3_112 = 0;
            foreach (var a in actualList)
            {
                if (a.type == 3)
                {
                    actSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            foreach (var a in oldList)
            {
                if (a.type == 3)
                {
                    oldSum3 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    if (a.row_options.Count() == 2)
                    {
                        oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oldSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oldSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    }
                }
            }
            worksheet3["X12"] = actSum3;
            worksheet3["AC12"] = oldSum3;
            worksheet3["X13"] = actSum3_111;
            worksheet3["AC13"] = oldSum3_111;
            worksheet3["X14"] = actSum3_112;
            worksheet3["AC14"] = oldSum3_112;
            #endregion

            var RecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldRecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло получено
            float tRecSum = 0;
            float tOldRecSum = 0;
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 2)
                {
                    tRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldRecievedListSum)
            {
                if (a.res_type == 2)
                {
                    tOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet3["W21"] = tRecSum;
            worksheet3["AB21"] = tOldRecSum;
            #endregion

            #region электричество получено
            float eRecSum = 0;
            float eOldRecSum = 0;
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 3)
                {
                    eRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldRecievedListSum)
            {
                if (a.res_type == 3)
                {
                    eOldRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet3["X21"] = eRecSum;
            worksheet3["AC21"] = eOldRecSum;
            #endregion

            var SendedListSum = MakeSendListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldSendedListSum = MakeSendListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло отдано
            float tSendSum = 0;
            float tOldSendSum = 0;
            foreach (var a in SendedListSum)
            {
                if (a.res_type == 2)
                {
                    tSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldSendedListSum)
            {
                if (a.res_type == 2)
                {
                    tOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            worksheet3["W16"] = tSendSum;
            worksheet3["AB16"] = tOldSendSum;
            #endregion

            #region электричество отдано
            float eSendSum = 0;
            float eOldSendSum = 0;
            foreach (var a in SendedListSum)
            {
                if (a.res_type == 3)
                {
                    eSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }
            foreach (var a in OldSendedListSum)
            {
                if (a.res_type == 3)
                {
                    eOldSendSum += Convert.ToSingle(Math.Round(a.value, 1));
                }
            }

            worksheet3["X16"] = eSendSum;
            worksheet3["AC16"] = eOldSendSum;
            #endregion

            var SourceSum = MakeSourceSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldSourceSum = MakeSourceSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region получено собственного тепла
            float tSourceSum = 0;
            float tOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2)
                {
                    tSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2)
                {
                    tOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["W18"] = tSourceSum;
            worksheet3["AB18"] = tOldSourceSum;
            #endregion

            #region получено собственного тепла от ВЭР
            float tvSourceSum = 0;
            float tvOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 4000)
                {
                    tvSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 4000)
                {
                    tvOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["W19"] = tvSourceSum;
            worksheet3["AB19"] = tvOldSourceSum;
            #endregion

            #region получено собственного тепла от солнца
            float tsSourceSum = 0;
            float tsOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 5000)
                {
                    tsSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 2 && a.Fuel_group == 5000)
                {
                    tsOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["W20"] = tsSourceSum;
            worksheet3["AB20"] = tsOldSourceSum;
            #endregion

            #region получено собственного электричества
            float eSourceSum = 0;
            float eOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3)
                {
                    eSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 3)
                {
                    eOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["X18"] = eSourceSum;
            worksheet3["AC18"] = eOldSourceSum;
            #endregion

            #region получено собственного электричества от ВЭР
            float evSourceSum = 0;
            float evOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 4000)
                {
                    evSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 4000)
                {
                    evOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["X19"] = evSourceSum;
            worksheet3["AC19"] = evOldSourceSum;
            #endregion

            #region получено собственного электричества от солнца
            float esSourceSum = 0;
            float esOldSourceSum = 0;
            foreach (var a in SourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 5000)
                {
                    esSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            foreach (var a in OldSourceSum)
            {
                if (a.Res_type == 3 && a.Fuel_group == 5000)
                {
                    esOldSourceSum += Convert.ToSingle(Math.Round(a.Value, 1));
                }
            }
            worksheet3["X20"] = esSourceSum;
            worksheet3["AC20"] = esOldSourceSum;
            #endregion

            var TradesSum = MakeTradeSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            foreach (var a in TradesSum)
            {
                if (a.type == 2)
                    worksheet3["W17"] = Convert.ToSingle(Math.Round(a.value, 1));
                else if (a.type == 3)
                    worksheet3["X17"] = Convert.ToSingle(Math.Round(a.value, 1));
            }
            var OldTradesSum = MakeTradeSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
            foreach (var a in OldTradesSum)
            {
                if (a.type == 2)
                    worksheet3["AB17"] = Convert.ToSingle(Math.Round(a.value, 1));
                else if (a.type == 3)
                    worksheet3["AC17"] = Convert.ToSingle(Math.Round(a.value, 1));
            }

            worksheet3["W30"] = "=SUM(ROUND(T12, 0), ROUND(((W12-W18)*0,143), 0), ROUND(((X12-X18)*0,123), 0))";
            worksheet3["Y30"] = "=SUM(ROUND(Y12, 0), ROUND(((AB12-AB18)*0,143), 0), ROUND(((AC12-AC18)*0,123), 0))";

            worksheet3["W15"] = 0;
            worksheet3["X15"] = 0;
            worksheet3["AB15"] = 0;
            worksheet3["AC15"] = 0;
            worksheet3["H26"] = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            worksheet3["G16"] = String.Format("за январь -  {0} {1} г", dateTimePicker1.Value.ToString("MMMM"), dateTimePicker1.Value.ToString("yyyy"));

            for (int i = 0; i <= 66; i++)
            {
                worksheet3.AutoFitRowHeight(i, true);
            }
            worksheet3.AutoFitRowHeight(5, false);
            worksheet3.AutoFitRowHeight(6, false);
            worksheet3.AutoFitRowHeight(8, false);
            worksheet3.AutoFitRowHeight(13, false);
            worksheet3.AutoFitRowHeight(18, false);
            worksheet3.AutoFitRowHeight(19, false);
            worksheet3.AutoFitRowHeight(20, false);
            worksheet3.AutoFitRowHeight(21, false);
            worksheet3.AutoFitRowHeight(22, false);
            worksheet3.AutoFitRowHeight(23, false);
            worksheet3.RowHeaders[18].Height = 49;
            worksheet3.RowHeaders[19].Height = 41;
            worksheet3.RowHeaders[20].Height = 33;
            worksheet3.RowHeaders[21].Height = 26;
            worksheet3.RowHeaders[22].Height = 26;
            worksheet3.RowHeaders[23].Height = 26;
            worksheet3.RowHeaders[5].Height = 19;
            worksheet3.RowHeaders[6].Height = 19;
            worksheet3.RowHeaders[8].Height = 19;
            worksheet3.RowHeaders[13].Height = 34;
        }

        void MakeTable12TekPril()
        {
            reoGrid4.Load("12tek_pril.xlsx");
            var worksheet4 = reoGrid4.CurrentWorksheet;
            worksheet4.SetScale(0.92f);
            reoGrid4.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet4.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet4.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);

            worksheet4["B1"] = String.Format("Приложение к отчету 12-тэк за январь - {0} {1} г", dateTimePicker1.Value.ToString("MMMM"), dateTimePicker1.Value.ToString("yyyy"));
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);

            #region строка 120
            var SendedList = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id, profile_num);
            var SendedListSum = MakeSendListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldSendedListSum = MakeSendListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
            List<WholeTable> WholeList = new List<WholeTable>();
            List<WholeTable> WholeListSum = new List<WholeTable>();
            List<WholeTable> WholeListSumOld = new List<WholeTable>();
            foreach (var a in SendedList)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeList.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeList.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = WholeList.Find(i => i.org_name == name);
                        int idx = WholeList.IndexOf(aa);
                        WholeList[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeList.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeList.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = WholeList.Find(i => i.org_name == name);
                        int idx = WholeList.IndexOf(aa);
                        WholeList[idx].evalue = a.value;
                    }                   
                }
            }
            foreach (var a in SendedListSum)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeListSum.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeListSum.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = WholeListSum.Find(i => i.org_name == name);
                        int idx = WholeListSum.IndexOf(aa);
                        WholeListSum[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeListSum.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeListSum.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = WholeListSum.Find(i => i.org_name == name);
                        int idx = WholeListSum.IndexOf(aa);
                        WholeListSum[idx].evalue = a.value;
                    }
                }
            }
            foreach (var a in OldSendedListSum)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeListSumOld.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeListSumOld.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = WholeListSumOld.Find(i => i.org_name == name);
                        int idx = WholeListSumOld.IndexOf(aa);
                        WholeListSumOld[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = WholeListSumOld.Any(item => item.org_name == name);
                    if (!containsItem)
                        WholeListSumOld.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = WholeListSumOld.Find(i => i.org_name == name);
                        int idx = WholeListSumOld.IndexOf(aa);
                        WholeListSumOld[idx].evalue = a.value;
                    }
                }
            }

            int row120 = 7;
            int counter = WholeList.Count;
            if (counter > 1)
                worksheet4.InsertRows(row120, counter - 1);
            else if (counter == 0)
            {
                counter = WholeListSumOld.Count;
                if (counter > 1)
                    worksheet4.InsertRows(row120, counter - 1);
            }
            int row130 = 7 + counter -1 + 10;
            int row150 = 7 + counter -1+ 10 + 8;

            var step = row120;
            foreach (var a in WholeList)
            {
                worksheet4["E" + (step + 1)] = Math.Round(a.tvalue,3);
                worksheet4["C" + (step + 1)] = Math.Round(a.evalue,3);
                worksheet4["B" + (step + 1)] = a.org_name;
                step++;
            }

            step = row120;
            foreach (var a in WholeListSum)
            {
                worksheet4["F" + (step + 1)] = Math.Round(a.tvalue, 3);
                worksheet4["D" + (step + 1)] = Math.Round(a.evalue, 3);
                step++;
            }

            step = row120;
            foreach (var a in WholeListSumOld)
            {
                worksheet4["H" + (step + 1)] = Math.Round(a.tvalue, 3);
                worksheet4["G" + (step + 1)] = Math.Round(a.evalue, 3);
                worksheet4["B" + (step + 1)] = a.org_name;
                step++;
            }
            if (counter > 1)
            {
                worksheet4["C" + (7 + counter + 1)] = String.Format("=ROUND(SUM(C{0}:C{1}), 3)", row120 + 1, row120 + counter);
                worksheet4["D" + (7 + counter + 1)] = String.Format("=ROUND(SUM(D{0}:D{1}), 3)", row120 + 1, row120 + counter);
                worksheet4["E" + (7 + counter + 1)] = String.Format("=ROUND(SUM(E{0}:E{1}), 3)", row120 + 1, row120 + counter);
                worksheet4["F" + (7 + counter + 1)] = String.Format("=ROUND(SUM(F{0}:F{1}), 3)", row120 + 1, row120 + counter);
                worksheet4["G" + (7 + counter + 1)] = String.Format("=ROUND(SUM(G{0}:G{1}), 3)", row120 + 1, row120 + counter);
                worksheet4["H" + (7 + counter + 1)] = String.Format("=ROUND(SUM(H{0}:H{1}), 3)", row120 + 1, row120 + counter);
            }
            else
            {
                worksheet4["C" + 9] = String.Format("=ROUND(C{0}, 3)", row120 + 1);
                worksheet4["D" + 9] = String.Format("=ROUND(D{0}, 3)", row120 + 1);
                worksheet4["E" + 9] = String.Format("=ROUND(E{0}, 3)", row120 + 1);
                worksheet4["F" + 9] = String.Format("=ROUND(F{0}, 3)", row120 + 1);
                worksheet4["G" + 9] = String.Format("=ROUND(G{0}, 3)", row120 + 1);
                worksheet4["H" + 9] = String.Format("=ROUND(H{0}, 3)", row120 + 1);
            }
            #endregion

            #region строка 130
            var TradesSum = MakeTradeSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            foreach (var a in TradesSum)
            {
                if (a.type == 2)
                    worksheet4["F" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
                else if (a.type == 3)
                    worksheet4["D" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
            }
            var OldTradesSum = MakeTradeSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
            foreach (var a in OldTradesSum)
            {
                if (a.type == 2)
                    worksheet4["H" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
                else if (a.type == 3)
                    worksheet4["G" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
            }
            var TradeList = dbOps.GetTrades(report_id);
            foreach (var a in TradeList)
            {
                if (a.type == 2)
                    worksheet4["E" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
                else if (a.type == 3)
                    worksheet4["C" + (row130 + 1)] = Convert.ToSingle(Math.Round(a.value, 2));
            }
            #endregion

            var RecievedList = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id, profile_num);
            var RecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldRecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
            List<WholeTable> RWholeList = new List<WholeTable>();
            List<WholeTable> RWholeListSum = new List<WholeTable>();
            List<WholeTable> RWholeListSumOld = new List<WholeTable>();
            foreach (var a in RecievedList)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeList.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeList.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = RWholeList.Find(i => i.org_name == name);
                        int idx = RWholeList.IndexOf(aa);
                        RWholeList[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeList.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeList.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = RWholeList.Find(i => i.org_name == name);
                        int idx = RWholeList.IndexOf(aa);
                        RWholeList[idx].evalue = a.value;
                    }
                }
            }
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeListSum.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeListSum.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = RWholeListSum.Find(i => i.org_name == name);
                        int idx = RWholeListSum.IndexOf(aa);
                        RWholeListSum[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeListSum.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeListSum.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = RWholeListSum.Find(i => i.org_name == name);
                        int idx = RWholeListSum.IndexOf(aa);
                        RWholeListSum[idx].evalue = a.value;
                    }
                }
            }
            foreach (var a in OldRecievedListSum)
            {
                if (a.res_type == 2)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeListSumOld.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeListSumOld.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = a.value, evalue = 0 });
                    else if (containsItem)
                    {
                        var aa = RWholeListSumOld.Find(i => i.org_name == name);
                        int idx = RWholeListSumOld.IndexOf(aa);
                        RWholeListSumOld[idx].tvalue = a.value;
                    }
                }
                if (a.res_type == 3)
                {
                    string name = dbOps.GetCompanyName(a.Id_org);
                    bool containsItem = RWholeListSumOld.Any(item => item.org_name == name);
                    if (!containsItem)
                        RWholeListSumOld.Add(new WholeTable { org_name = dbOps.GetCompanyName(a.Id_org), tvalue = 0, evalue = a.value });
                    else if (containsItem)
                    {
                        var aa = RWholeListSumOld.Find(i => i.org_name == name);
                        int idx = RWholeListSumOld.IndexOf(aa);
                        RWholeListSumOld[idx].evalue = a.value;
                    }
                }
            }

            int counter2 = RWholeList.Count;
            if (counter2 > 1)
                worksheet4.InsertRows(row150, counter2 - 1);
            else if (counter2 == 0)
            {
                counter2 = RWholeListSumOld.Count;
                if (counter2 > 1)
                    worksheet4.InsertRows(row150, counter2 - 1);
            }

            var rstep = row150;
            foreach (var a in RWholeList)
            {
                worksheet4["E" + (rstep + 1)] = Math.Round(a.tvalue,3);
                worksheet4["C" + (rstep + 1)] = Math.Round(a.evalue,4);
                worksheet4["B" + (rstep + 1)] = a.org_name;
                rstep++;
            }

            rstep = row150;
            foreach (var a in RWholeListSum)
            {
                worksheet4["F" + (rstep + 1)] = Math.Round(a.tvalue,3);
                worksheet4["D" + (rstep + 1)] = Math.Round(a.evalue,3);
                rstep++;
            }

            rstep = row150;
            foreach (var a in RWholeListSumOld)
            {
                worksheet4["H" + (rstep + 1)] = Math.Round(a.tvalue, 3);
                worksheet4["G" + (rstep + 1)] = Math.Round(a.evalue,3);
                worksheet4["B" + (rstep + 1)] = a.org_name;
                rstep++;
            }
            if (counter2 > 1)
            {
                worksheet4["C" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(C{0}:C{1}), 3)", row150 + 1, row150 + counter2);
                worksheet4["D" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(D{0}:D{1}), 3)", row150 + 1, row150 + counter2);
                worksheet4["E" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(E{0}:E{1}), 3)", row150 + 1, row150 + counter2);
                worksheet4["F" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(F{0}:F{1}), 3)", row150 + 1, row150 + counter2);
                worksheet4["G" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(G{0}:G{1}), 3)", row150 + 1, row150 + counter2);
                worksheet4["H" + (row150 + counter2 + 1)] = String.Format("=ROUND(SUM(H{0}:H{1}), 3)", row150 + 1, row150 + counter2);
            }
            else
            {
                worksheet4["C" + (row150 + 2)] = String.Format("=ROUND(C{0}, 3)", row150 + 1);
                worksheet4["D" + (row150 + 2)] = String.Format("=ROUND(D{0}, 3)", row150 + 1);
                worksheet4["E" + (row150 + 2)] = String.Format("=ROUND(E{0}, 3)", row150 + 1);
                worksheet4["F" + (row150 + 2)] = String.Format("=ROUND(F{0}, 3)", row150 + 1);
                worksheet4["G" + (row150 + 2)] = String.Format("=ROUND(G{0}, 3)", row150 + 1);
                worksheet4["H" + (row150 + 2)] = String.Format("=ROUND(H{0}, 3)", row150 + 1);
            }
            for (int i = 0; i <= 66; i++)
            {
                worksheet4.AutoFitRowHeight(i, true);
            }
            worksheet4.ColumnHeaders[1].Width = 319;
        }

        void MakeTable4norm()
        {
            reoGridControl4.Load("4norm.xlsx");
            var worksheet4 = reoGridControl4.CurrentWorksheet;
            worksheet4.SetScale(0.92f);
            reoGridControl4.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet4.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet4.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);
            int Norm4Year = dateTimePicker1.Value.Year;
            int Norm4Quater = MakeQuater(dateTimePicker1.Value.Month);
            var Norm4list = dbOps.Get4Norm(CurrentData.UserData.Id_org, Norm4Year, Norm4Quater);

            #region топливо
            int fuelrow = 25;
            int counter = 0;
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 1)
                    counter++;
            }
            if (counter > 1)
                worksheet4.InsertRows(fuelrow, counter - 1);
            int tmp = 0;
            double n9001sum = 0;
            double f9001sum = 0;
            double f9010sum = 0;
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 1)
                {
                    worksheet4["A" + (fuelrow + tmp)] = a.Norm_name + " (" + a.Fuel_name + ")";
                    worksheet4["B" + (fuelrow + tmp)] = a.Norm_code;
                    worksheet4["C" + (fuelrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet4["D" + (fuelrow + tmp)] = Math.Round(a.Volume, 2);
                    worksheet4["E" + (fuelrow + tmp)] = Math.Round(a.Norm, 2);
                    worksheet4["H" + (fuelrow + tmp)] = Math.Round(a.Value, 2);
                    worksheet4["F" + (fuelrow + tmp)] = Math.Round(Math.Round(a.Value, 2)/ Math.Round(a.Volume, 2) * 1000,1);
                    if (Math.Round(a.Volume, 2) > 0)
                        worksheet4["G" + (fuelrow + tmp)] = Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000,1);
                    else
                        worksheet4["G" + (fuelrow + tmp)] = Math.Round(Math.Round(a.Norm, 2), 1);
                    if (a.Norm_code != 9010)
                    {
                        if (Math.Round(a.Volume, 2) > 0)
                            n9001sum += Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000, 1);
                        else
                            n9001sum += Math.Round(Math.Round(a.Norm, 2), 1);
                        f9001sum += Math.Round(a.Value, 2);
                    }
                    else if (a.Norm_code == 9010)
                    {
                        f9010sum += Math.Round(a.Value, 2);
                    }
                    tmp++;
                }
            }
            worksheet4["G" + (fuelrow + tmp)] = n9001sum;
            worksheet4["H" + (fuelrow + tmp)] = f9001sum;
            worksheet4["H" + (fuelrow + tmp+1)] = f9010sum;
            worksheet4["H" + (fuelrow + tmp + 2)] = f9010sum + f9001sum;
            var cnt1 = counter;
            #endregion

            #region тепло
            tmp = 0;
            n9001sum = 0;
            f9001sum = 0;
            f9010sum = 0;
            int heatrow = fuelrow + counter + 9;
            counter = 0;
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 2)
                    counter++;
            }
            if (counter > 1)
                worksheet4.InsertRows(heatrow, counter - 1);
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 2)
                {
                    worksheet4["A" + (heatrow + tmp)] = a.Norm_name;
                    worksheet4["B" + (heatrow + tmp)] = a.Norm_code;
                    worksheet4["C" + (heatrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet4["D" + (heatrow + tmp)] = Math.Round(a.Volume, 2);
                    worksheet4["E" + (heatrow + tmp)] = Math.Round(a.Norm, 2);
                    worksheet4["H" + (heatrow + tmp)] = Math.Round(a.Value, 2);
                    worksheet4["F" + (heatrow + tmp)] = Math.Round(Math.Round(a.Value, 2) / Math.Round(a.Volume, 2) * 1000, 1);
                    if (Math.Round(a.Volume, 2) > 0)
                        worksheet4["G" + (heatrow + tmp)] = Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000, 1);
                    else
                        worksheet4["G" + (heatrow + tmp)] = Math.Round(Math.Round(a.Norm, 2), 1);
                    if (a.Norm_code != 9010)
                    {
                        if (Math.Round(a.Volume, 2) > 0)
                            n9001sum += Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000, 1);
                        else
                            n9001sum += Math.Round(Math.Round(a.Norm, 2), 1);
                        f9001sum += Math.Round(a.Value, 2);
                    }
                    else if (a.Norm_code == 9010)
                    {
                        f9010sum += Math.Round(a.Value, 2);
                    }
                    tmp++;
                }
            }
            worksheet4["G" + (heatrow + tmp)] = n9001sum;
            worksheet4["H" + (heatrow + tmp)] = f9001sum;
            worksheet4["H" + (heatrow + tmp + 1)] = f9010sum;
            worksheet4["H" + (heatrow + tmp + 2)] = f9010sum + f9001sum;
            var cnt2 = counter;
            #endregion

            #region Электричество
            tmp = 0;
            n9001sum = 0;
            f9001sum = 0;
            f9010sum = 0;
            int elrow = heatrow + counter + 9;
            counter = 0;
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 3)
                    counter++;
            }
            if (counter > 1)
                worksheet4.InsertRows(elrow, counter - 1);
            foreach (var a in Norm4list)
            {
                if (a.Norm_type == 3)
                {
                    worksheet4["A" + (elrow + tmp)] = a.Norm_name;
                    worksheet4["B" + (elrow + tmp)] = a.Norm_code;
                    worksheet4["C" + (elrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet4["D" + (elrow + tmp)] = Math.Round(a.Volume, 2);
                    worksheet4["E" + (elrow + tmp)] = Math.Round(a.Norm, 2);
                    worksheet4["H" + (elrow + tmp)] = Math.Round(a.Value, 2);
                    worksheet4["F" + (elrow + tmp)] = Math.Round(Math.Round(a.Value, 2) / Math.Round(a.Volume, 2) * 1000, 1);
                    if (Math.Round(a.Volume, 2) > 0)
                        worksheet4["G" + (elrow + tmp)] = Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000, 1);
                    else
                        worksheet4["G" + (elrow + tmp)] = Math.Round(Math.Round(a.Norm, 2), 1);
                    if (a.Norm_code != 9010)
                    {
                        if (Math.Round(a.Volume, 2) > 0)
                            n9001sum += Math.Round(Math.Round(a.Norm, 2) * Math.Round(a.Volume, 2) / 1000, 1);
                        else
                            n9001sum += Math.Round(Math.Round(a.Norm, 2), 1);
                        f9001sum += Math.Round(a.Value, 2);
                    }
                    else if (a.Norm_code == 9010)
                    {
                        f9010sum += Math.Round(a.Value, 2);
                    }
                    tmp++;
                }
            }
            worksheet4["G" + (elrow + tmp)] = n9001sum;
            worksheet4["H" + (elrow + tmp)] = f9001sum;
            worksheet4["H" + (elrow + tmp + 1)] = f9010sum;
            worksheet4["H" + (elrow + tmp + 2)] = f9010sum + f9001sum;
            var cnt3 = counter;
            #endregion

            #region style
            worksheet4.SetRangeStyles("A22:H24", new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 11,
            });
            worksheet4.SetRangeStyles("A" + (heatrow - 3) + ":H" + (heatrow - 1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 11,
            });
            worksheet4.SetRangeStyles("A" + (elrow - 3) + ":H" + (elrow - 1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 11,
            });
            worksheet4.SetRangeStyles("A25:H" + (fuelrow + cnt1-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 12,
            });
            worksheet4.SetRangeStyles("A" + heatrow + ":H" + (heatrow + cnt2-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 12,
            });
            worksheet4.SetRangeStyles("A" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                TextWrapMode = TextWrapMode.WordBreak,
                FontName = "Times New Roman",
                FontSize = 12,
            });
            worksheet4.SetRangeStyles("D25:H" + (fuelrow + cnt1-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Right,
            });
            worksheet4.SetRangeStyles("D" + heatrow + ":H" + (heatrow + cnt2-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Right,
            });
            worksheet4.SetRangeStyles("D" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Right,
            });
            worksheet4.SetRangeStyles("B25:C" + (fuelrow + cnt1-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Center,
            });
            worksheet4.SetRangeStyles("B" + heatrow + ":C" + (heatrow + cnt2-1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Center,
            });
            worksheet4.SetRangeStyles("B" + elrow + ":C" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Center,
            });
            for (int i = 0; i <= 166; i++)
            {
                worksheet4.AutoFitRowHeight(i, true);
            }
            #endregion
        }

        void MakeTable1tek()
        {
            reoGridControl5.Load("1tek.xlsx");
            var worksheet4 = reoGridControl5.CurrentWorksheet;
            worksheet4.SetScale(0.92f);
            reoGridControl5.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet4.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet4.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);
            int Norm4Year = dateTimePicker1.Value.Year;
            int Norm4Quater = MakeQuater(dateTimePicker1.Value.Month);

            var Norm4list = MakeNorm4List(Norm4Year);
            List<Norm4Table> OilList = new List<Norm4Table>();
            List<Norm4Table> GasList = new List<Norm4Table>();

            #region нефть 1 раздел
            foreach (var a in Norm4list)
            {
                if (a.Norm_code == 4120)
                    OilList.Add(a);
            }
            var tmp = 37;
            float cnt = 0;
            if (OilList.Count > 0)
            {
                worksheet4["B" + tmp] = "Транспортировка нефти";
                worksheet4["J" + tmp] = "120";
                worksheet4["K" + tmp] = dbOps.GetProdUnit(OilList[0].Id_prod);
                float topl = 0;
                float heat = 0;
                float el = 0;
                foreach (var a in OilList)
                {
                    if (a.Norm_type == 1)
                    {
                        topl += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 2)
                    {
                        heat += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 3)
                    {
                        el += a.Value;
                        cnt += a.Volume;
                    }
                }
                worksheet4["O" + tmp] = topl;
                worksheet4["R" + tmp] = heat;
                worksheet4["U" + tmp] = el;
                worksheet4["L" + tmp] = cnt;                
             
                tmp++;
            }
            #endregion

            #region газ 1 раздел
            foreach (var a in Norm4list)
            {
                if (a.Norm_code == 4130)
                    GasList.Add(a);
            }

            cnt = 0;
            if (GasList.Count > 0)
            {
                worksheet4["B" + tmp] = "Транспортировка газа";
                worksheet4["J" + tmp] = "130";
                worksheet4["K" + tmp] = dbOps.GetProdUnit(GasList[0].Id_prod);
                float topl = 0;
                float heat = 0;
                float el = 0;
                foreach (var a in GasList)
                {
                    if (a.Norm_type == 1)
                    {
                        topl += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 2)
                    {
                        heat += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 3)
                    {
                        el += a.Value;
                        cnt += a.Volume;
                    }
                }
                worksheet4["O" + tmp] = topl;
                worksheet4["R" + tmp] = heat;
                worksheet4["U" + tmp] = el;
                worksheet4["L" + tmp] = cnt/1000;
                tmp++;
            }
            #endregion

            var list1 = dbOps.GetSourcesFor1tek(CurrentData.UserData.Id_org);
            List<SourceTable1tek> list2 = new List<SourceTable1tek>();
            foreach (var a in list1)
            {
                if (a.Fuel_group != 5000 && a.Fuel_group != 4000 && a.Id_fuel != 1201 && a.Id_object != 42 && a.Id_object != 53 && a.Id_object != 60)
                    list2.Add(a);
            }

            List<Table1tekPart2> Table1Tek = new List<Table1tekPart2>();
            foreach (var a in list2)
            {
                float fv = 0;
                float hv = 0;
                float ev = 0;
                if (a.Name_object.Contains("Котельная"))
                {
                    for (int i = 1; i<= 12; i++)
                    {
                        int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                        if (a.Res_type == 2)
                            hv += dbOps.GetSrcValue(report_id, a.Id);
                        else if (a.Res_type == 3)
                            ev += dbOps.GetSrcValue(report_id, a.Id);
                        fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                    }
                    Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 5 });
                }
                else if (a.Name_object.Contains("ТЭЦ"))
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                        if (a.Res_type == 2)
                            hv += dbOps.GetSrcValue(report_id, a.Id);
                        else if (a.Res_type == 3)
                            ev += dbOps.GetSrcValue(report_id, a.Id);
                        fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                    }
                    Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 3 });
                }
                else if (a.Name_object.Contains("КГУ") || a.Name_object.Contains("КГТУ") || a.Name_object.Contains("МГТУ"))
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                        if (a.Res_type == 2)
                            hv += dbOps.GetSrcValue(report_id, a.Id);
                        else if (a.Res_type == 3)
                            ev += dbOps.GetSrcValue(report_id, a.Id);
                        fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                    }
                    Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 4 });
                }
            }

            float drova = 0;
            float toppech = 0;
            float neftprod = 0;
            float gasprir = 0;
            float gaspoput = 0;
            float drova_h = 0;
            float toppech_h = 0;
            float neftprod_h = 0;
            float gasprir_h = 0;
            float gaspoput_h = 0;
            float drova_e = 0;
            float toppech_e = 0;
            float neftprod_e = 0;
            float gasprir_e = 0;
            float gaspoput_e = 0;
            float sum = 0;
            float sum_h = 0;
            float sum_e = 0;

            foreach (var a in Table1Tek)
            {
                if (a.Group == 5)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M80"] = drova;
                worksheet4["M81"] = drova_h;
            }
            if (toppech != 0)
            {
                worksheet4["P80"] = toppech;
                worksheet4["P81"] = toppech_h;
            }
            if (neftprod != 0)
            {
                worksheet4["Q80"] = neftprod;
                worksheet4["Q81"] = neftprod_h;
            }
            if (gasprir != 0)
            {
                worksheet4["R80"] = gasprir;
                worksheet4["R81"] = gasprir_h;
            }
            if (gaspoput != 0)
            {
                worksheet4["S80"] = gaspoput;
                worksheet4["S81"] = gaspoput_h;
            }
            worksheet4["I80"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I81"] = sum_h != 0 ? sum_h.ToString() : "x";

            drova = 0;
            toppech = 0;
            neftprod = 0;
            gasprir = 0;
            gaspoput = 0;
            drova_h = 0;
            toppech_h = 0;
            neftprod_h = 0;
            gasprir_h = 0;
            gaspoput_h = 0;
            drova_e = 0;
            toppech_e = 0;
            neftprod_e = 0;
            gasprir_e = 0;
            gaspoput_e = 0;
            sum = 0;
            sum_h = 0;
            sum_e = 0;
            foreach (var a in Table1Tek)
            {
                if (a.Group == 3)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M73"] = drova;
                worksheet4["M74"] = drova_e;
                worksheet4["M75"] = drova_h;
            }
            if (toppech != 0)
            {
                worksheet4["P73"] = toppech;
                worksheet4["P74"] = toppech_e;
                worksheet4["P75"] = toppech_h;
            }
            if (neftprod != 0)
            {
                worksheet4["Q73"] = neftprod;
                worksheet4["Q74"] = neftprod_e;
                worksheet4["Q75"] = neftprod_h;
            }
            if (gasprir != 0)
            {
                worksheet4["R73"] = gasprir;
                worksheet4["R74"] = gasprir_e;
                worksheet4["R75"] = gasprir_h;
            }
            if (gaspoput != 0)
            {
                worksheet4["S73"] = gaspoput;
                worksheet4["S74"] = gaspoput_e;
                worksheet4["S75"] = gaspoput_h;
            }
            worksheet4["I73"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I74"] = sum_e != 0 ? sum_e.ToString() : "x";
            worksheet4["I75"] = sum_h != 0 ? sum_h.ToString() : "x";

            drova = 0;
            toppech = 0;
            neftprod = 0;
            gasprir = 0;
            gaspoput = 0;
            drova_h = 0;
            toppech_h = 0;
            neftprod_h = 0;
            gasprir_h = 0;
            gaspoput_h = 0;
            drova_e = 0;
            toppech_e = 0;
            neftprod_e = 0;
            gasprir_e = 0;
            gaspoput_e = 0;
            sum = 0;
            sum_h = 0;
            sum_e = 0;
            foreach (var a in Table1Tek)
            {
                if (a.Group == 4)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M77"] = drova;
                worksheet4["M78"] = drova_e;
            }
            if (toppech != 0)
            {
                worksheet4["P77"] = toppech;
                worksheet4["P78"] = toppech_e;
            }
            if (neftprod != 0)
            {
                worksheet4["Q77"] = neftprod;
                worksheet4["Q78"] = neftprod_e;
            }
            if (gasprir != 0)
            {
                worksheet4["R77"] = gasprir;
                worksheet4["R78"] = gasprir_e;
            }
            if (gaspoput != 0)
            {
                worksheet4["S77"] = gaspoput;
                worksheet4["S78"] = gaspoput_e;
            }
            
            worksheet4["I77"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I78"] = sum_e != 0 ? sum_e.ToString() : "x";

            //var a2 = 1;
            #region style
            //worksheet4.SetRangeStyles("A22:H24", new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A" + (heatrow - 3) + ":H" + (heatrow - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A" + (elrow - 3) + ":H" + (elrow - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A25:H" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("A" + heatrow + ":H" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("A" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("D25:H" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("D" + heatrow + ":H" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("D" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("B25:C" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            //worksheet4.SetRangeStyles("B" + heatrow + ":C" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            //worksheet4.SetRangeStyles("B" + elrow + ":C" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            for (int i = 0; i <= 166; i++)
            {
                worksheet4.AutoFitRowHeight(i, true);
            }
            #endregion
    }

        void MakeTable1tekSUM(bool flag)
        {
            reoGridControl5.Load("1tek.xlsx");
            var worksheet4 = reoGridControl5.CurrentWorksheet;
            worksheet4.SetScale(0.92f);
            reoGridControl5.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            worksheet4.SelectionStyle = WorksheetSelectionStyle.None;
            worksheet4.SetSettings(WorksheetSettings.Behavior_MouseWheelToZoom, false);
            int Norm4Year = dateTimePicker1.Value.Year;
            int Norm4Quater = MakeQuater(dateTimePicker1.Value.Month);
            List<int> CompanyIdList = new List<int>();
            if (flag == false)
                CompanyIdList = dbOps.GetCompanyIdList(100);
            else if (flag == true)
            {
                CompanyIdList = dbOps.GetCompanyIdList(100);
                var secondList = dbOps.GetCompanyIdList(200);
                foreach (var c in secondList)
                    CompanyIdList.Add(c);
            }
            var CurrentID = CurrentData.UserData.Id_org;

            List<Norm4Table> Norm4list = new List<Norm4Table>();
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                var templist = MakeNorm4List(Norm4Year);
                foreach (var c in templist)
                    Norm4list.Add(c);
            }           
            List<Norm4Table> OilList = new List<Norm4Table>();
            List<Norm4Table> GasList = new List<Norm4Table>();

            #region нефть 1 раздел
            foreach (var a in Norm4list)
            {
                if (a.Norm_code == 4120)
                    OilList.Add(a);
            }
            var tmp = 37;
            float cnt = 0;
            if (OilList.Count > 0)
            {
                worksheet4["B" + tmp] = "Транспортировка нефти";
                worksheet4["J" + tmp] = "120";
                worksheet4["K" + tmp] = dbOps.GetProdUnit(OilList[0].Id_prod);
                float topl = 0;
                float heat = 0;
                float el = 0;
                foreach (var a in OilList)
                {
                    if (a.Norm_type == 1)
                    {
                        topl += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 2)
                    {
                        heat += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 3)
                    {
                        el += a.Value;
                        cnt += a.Volume;
                    }
                }
                worksheet4["O" + tmp] = topl;
                worksheet4["R" + tmp] = heat;
                worksheet4["U" + tmp] = el;
                worksheet4["L" + tmp] = cnt;

                tmp++;
            }
            #endregion

            #region газ 1 раздел
            foreach (var a in Norm4list)
            {
                if (a.Norm_code == 4130)
                    GasList.Add(a);
            }

            cnt = 0;
            if (GasList.Count > 0)
            {
                worksheet4["B" + tmp] = "Транспортировка газа";
                worksheet4["J" + tmp] = "130";
                worksheet4["K" + tmp] = dbOps.GetProdUnit(GasList[0].Id_prod);
                float topl = 0;
                float heat = 0;
                float el = 0;
                foreach (var a in GasList)
                {
                    if (a.Norm_type == 1)
                    {
                        topl += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 2)
                    {
                        heat += a.Value;
                        cnt += a.Volume;
                    }
                    else if (a.Norm_type == 3)
                    {
                        el += a.Value;
                        cnt += a.Volume;
                    }
                }
                worksheet4["O" + tmp] = topl;
                worksheet4["R" + tmp] = heat;
                worksheet4["U" + tmp] = el;
                worksheet4["L" + tmp] = cnt / 1000;
                tmp++;
            }
            #endregion

            List<SourceTable1tek> list1 = new List<SourceTable1tek>();
            List<SourceTable1tek> list2 = new List<SourceTable1tek>();
            List<Table1tekPart2> Table1Tek = new List<Table1tekPart2>();
            foreach (var b in CompanyIdList)
            {
                CurrentData.UserData.Id_org = b;
                list1 = dbOps.GetSourcesFor1tek(b);
                foreach (var a in list1)
                {
                    if (a.Fuel_group != 5000 && a.Fuel_group != 4000 && a.Id_fuel != 1201 && a.Id_object != 42 && a.Id_object != 53 && a.Id_object != 60)
                        list2.Add(a);
                }
                foreach (var a in list2)
                {
                    float fv = 0;
                    float hv = 0;
                    float ev = 0;
                    if (a.Name_object.Contains("Котельная"))
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                            if (a.Res_type == 2)
                                hv += dbOps.GetSrcValue(report_id, a.Id);
                            else if (a.Res_type == 3)
                                ev += dbOps.GetSrcValue(report_id, a.Id);
                            fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                        }
                        Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 5 });
                    }
                    else if (a.Name_object.Contains("ТЭЦ"))
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                            if (a.Res_type == 2)
                                hv += dbOps.GetSrcValue(report_id, a.Id);
                            else if (a.Res_type == 3)
                                ev += dbOps.GetSrcValue(report_id, a.Id);
                            fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                        }
                        Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 3 });
                    }
                    else if (a.Name_object.Contains("КГУ") || a.Name_object.Contains("КГТУ") || a.Name_object.Contains("МГТУ"))
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, i);
                            if (a.Res_type == 2)
                                hv += dbOps.GetSrcValue(report_id, a.Id);
                            else if (a.Res_type == 3)
                                ev += dbOps.GetSrcValue(report_id, a.Id);
                            fv += dbOps.GetFuelFactValue(CurrentData.UserData.Id_org, a.Id_object, 1, a.Id_fuel, report_id);
                        }
                        Table1Tek.Add(new Table1tekPart2 { Id_obj = a.Id_object, Name_obj = a.Name_object, Id_fuel = a.Id_fuel, Fgroup = a.Fuel_group, Fvalue = fv, Hvalue = hv, Evalue = ev, Group = 3 });
                    }
                }
            }
                                

            float drova = 0;
            float toppech = 0;
            float neftprod = 0;
            float gasprir = 0;
            float gaspoput = 0;
            float drova_h = 0;
            float toppech_h = 0;
            float neftprod_h = 0;
            float gasprir_h = 0;
            float gaspoput_h = 0;
            float drova_e = 0;
            float toppech_e = 0;
            float neftprod_e = 0;
            float gasprir_e = 0;
            float gaspoput_e = 0;
            float sum = 0;
            float sum_h = 0;
            float sum_e = 0;

            foreach (var a in Table1Tek)
            {
                if (a.Group == 5)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M80"] = drova;
                worksheet4["M81"] = drova_h;
            }
            if (toppech != 0)
            {
                worksheet4["P80"] = toppech;
                worksheet4["P81"] = toppech_h;
            }
            if (neftprod != 0)
            {
                worksheet4["Q80"] = neftprod;
                worksheet4["Q81"] = neftprod_h;
            }
            if (gasprir != 0)
            {
                worksheet4["R80"] = gasprir;
                worksheet4["R81"] = gasprir_h;
            }
            if (gaspoput != 0)
            {
                worksheet4["S80"] = gaspoput;
                worksheet4["S81"] = gaspoput_h;
            }
            worksheet4["I80"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I81"] = sum_h != 0 ? sum_h.ToString() : "x";

            drova = 0;
            toppech = 0;
            neftprod = 0;
            gasprir = 0;
            gaspoput = 0;
            drova_h = 0;
            toppech_h = 0;
            neftprod_h = 0;
            gasprir_h = 0;
            gaspoput_h = 0;
            drova_e = 0;
            toppech_e = 0;
            neftprod_e = 0;
            gasprir_e = 0;
            gaspoput_e = 0;
            sum = 0;
            sum_h = 0;
            sum_e = 0;
            foreach (var a in Table1Tek)
            {
                if (a.Group == 3)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M73"] = drova;
                worksheet4["M74"] = drova_e;
                worksheet4["M75"] = drova_h;
            }
            if (toppech != 0)
            {
                worksheet4["P73"] = toppech;
                worksheet4["P74"] = toppech_e;
                worksheet4["P75"] = toppech_h;
            }
            if (neftprod != 0)
            {
                worksheet4["Q73"] = neftprod;
                worksheet4["Q74"] = neftprod_e;
                worksheet4["Q75"] = neftprod_h;
            }
            if (gasprir != 0)
            {
                worksheet4["R73"] = gasprir;
                worksheet4["R74"] = gasprir_e;
                worksheet4["R75"] = gasprir_h;
            }
            if (gaspoput != 0)
            {
                worksheet4["S73"] = gaspoput;
                worksheet4["S74"] = gaspoput_e;
                worksheet4["S75"] = gaspoput_h;
            }
            worksheet4["I73"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I74"] = sum_e != 0 ? sum_e.ToString() : "x";
            worksheet4["I75"] = sum_h != 0 ? sum_h.ToString() : "x";

            drova = 0;
            toppech = 0;
            neftprod = 0;
            gasprir = 0;
            gaspoput = 0;
            drova_h = 0;
            toppech_h = 0;
            neftprod_h = 0;
            gasprir_h = 0;
            gaspoput_h = 0;
            drova_e = 0;
            toppech_e = 0;
            neftprod_e = 0;
            gasprir_e = 0;
            gaspoput_e = 0;
            sum = 0;
            sum_h = 0;
            sum_e = 0;
            foreach (var a in Table1Tek)
            {
                if (a.Group == 4)
                {
                    if (a.Id_fuel == 2201)
                    {
                        drova += a.Fvalue;
                        drova_h += a.Hvalue;
                        drova_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1202)
                    {
                        toppech += a.Fvalue;
                        toppech_h += a.Hvalue;
                        toppech_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Id_fuel == 1203)
                    {
                        neftprod += a.Fvalue;
                        neftprod_h += a.Hvalue;
                        neftprod_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 1100)
                    {
                        gasprir += a.Fvalue;
                        gasprir_h += a.Hvalue;
                        gasprir_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                    else if (a.Fgroup == 2100)
                    {
                        gaspoput += a.Fvalue;
                        gaspoput_h += a.Hvalue;
                        gaspoput_e += a.Evalue;
                        sum += a.Fvalue;
                        sum_h += a.Hvalue;
                        sum_e += a.Evalue;
                    }
                }
            }
            if (drova != 0)
            {
                worksheet4["M77"] = drova;
                worksheet4["M78"] = drova_e;
            }
            if (toppech != 0)
            {
                worksheet4["P77"] = toppech;
                worksheet4["P78"] = toppech_e;
            }
            if (neftprod != 0)
            {
                worksheet4["Q77"] = neftprod;
                worksheet4["Q78"] = neftprod_e;
            }
            if (gasprir != 0)
            {
                worksheet4["R77"] = gasprir;
                worksheet4["R78"] = gasprir_e;
            }
            if (gaspoput != 0)
            {
                worksheet4["S77"] = gaspoput;
                worksheet4["S78"] = gaspoput_e;
            }

            worksheet4["I77"] = sum != 0 ? sum.ToString() : "x";
            worksheet4["I78"] = sum_e != 0 ? sum_e.ToString() : "x";

            //var a2 = 1;

            CurrentData.UserData.Id_org = CurrentID;
            #region style
            //worksheet4.SetRangeStyles("A22:H24", new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A" + (heatrow - 3) + ":H" + (heatrow - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A" + (elrow - 3) + ":H" + (elrow - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 11,
            //});
            //worksheet4.SetRangeStyles("A25:H" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("A" + heatrow + ":H" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("A" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.TextWrap | PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
            //    TextWrapMode = TextWrapMode.WordBreak,
            //    FontName = "Times New Roman",
            //    FontSize = 12,
            //});
            //worksheet4.SetRangeStyles("D25:H" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("D" + heatrow + ":H" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("D" + elrow + ":H" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Right,
            //});
            //worksheet4.SetRangeStyles("B25:C" + (fuelrow + cnt1 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            //worksheet4.SetRangeStyles("B" + heatrow + ":C" + (heatrow + cnt2 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            //worksheet4.SetRangeStyles("B" + elrow + ":C" + (elrow + cnt3 - 1), new WorksheetRangeStyle()
            //{
            //    Flag = PlainStyleFlag.HorizontalAlign,
            //    HAlign = ReoGridHorAlign.Center,
            //});
            for (int i = 0; i <= 166; i++)
            {
                worksheet4.AutoFitRowHeight(i, true);
            }
            #endregion
        }

        private int MakeQuater(int month)
        {
            int quater = 1;
            if (month >= 1 && month <= 3)
                quater = 1;
            if (month >= 4 && month <= 6)
                quater = 2;
            if (month >= 7 && month <= 9)
                quater = 3;
            if (month >= 10 && month <= 12)
                quater = 4;
            return quater;
        }
        private string MakeQuaterText(int month)
        {
            string quater = "I квартал";
            if (month >= 1 && month <= 3)
                quater = "I квартал";
            if (month >= 4 && month <= 6)
                quater = "II квартал";
            if (month >= 7 && month <= 9)
                quater = "III квартал";
            if (month >= 10 && month <= 12)
                quater = "IV квартал";
            return quater;
        }

        private List<Norm4Table> MakeNorm4List(int year)
        {
            var NormList = dbOps.Get4Norm(CurrentData.UserData.Id_org, year, 1);
            for (int i = 2; i <= 4; i++)
            {
                var tmpList = dbOps.Get4Norm(CurrentData.UserData.Id_org, year, i);
                foreach (var a in tmpList)
                {
                    NormList.Add(a);
                }
            }
            return NormList;
        }

        private List<NormTable> MakeListSumPR(int year, int month)
        {
            List<NormTable> NormListPR = new List<NormTable>();
            for (int i = 1; i <= month; i++)
            {
                int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                var tempList = dbOps.GetNormListPR(CurrentData.UserData.Id_org, report_id, profile_num, month, year);
                foreach (var t in tempList)
                {
                    if (NormListPR.Any(norm => norm.norm_code == t.norm_code))
                    {
                        var index = NormListPR.FindIndex(norm => norm.norm_code == t.norm_code);
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
        private List<NormTable> MakeListOnePR(List<NormTable> list, List<NormTable> listSum)
        {
            List<NormTable> ListOnePR = listSum.DeepClone();
            foreach (var t in ListOnePR)
            {
                if (list.Any(norm => norm.norm_code == t.norm_code))
                {
                    var sumIndex = ListOnePR.FindIndex(norm => norm.norm_code == t.norm_code);
                    var index = list.FindIndex(norm => norm.norm_code == t.norm_code);
                    ListOnePR[sumIndex].val_fact = list[index].val_fact;
                    ListOnePR[sumIndex].val_plan = list[index].val_plan;
                    ListOnePR[sumIndex].val_fact_ut = list[index].val_fact_ut;
                    ListOnePR[sumIndex].val_plan_ut = list[index].val_plan_ut;
                    ListOnePR[sumIndex].editable = true;

                }
                else
                {
                    var sumIndex = ListOnePR.FindIndex(norm => norm.norm_code == t.norm_code);
                    ListOnePR[sumIndex].val_fact = 0;
                    ListOnePR[sumIndex].val_plan = 0;
                    ListOnePR[sumIndex].val_fact_ut = 0;
                    ListOnePR[sumIndex].val_plan_ut = 0;
                }
            }
            return ListOnePR;
        }
        private List<NormTable> MakeListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var NormList = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
            for (int i =0; i < NormList.Count; i++)
            {
                if (NormList[i].type == 1)
                {
                    var Fuel = dbOps.GetFuelData(NormList[i].fuel, year, 1);
                    NormList[i].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_fact, 1)) * Fuel.B_y), 1));
                    NormList[i].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_plan, 1)) * Fuel.B_y), 1));
                }
                if (NormList[i].type == 2)
                {
                    var Factor = dbOps.GetFactorData(NormList[i].type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    NormList[i].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_fact, 1)) * Factor.value), 1));
                    NormList[i].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_plan, 1)) * Factor.value), 1));
                }
                if (NormList[i].type == 3)
                {
                    var Factor = dbOps.GetFactorData(NormList[i].type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                    NormList[i].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_fact, 1)) * Factor.value), 1));
                    NormList[i].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(NormList[i].val_plan, 1)) * Factor.value), 1));
                }
            }

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
                    for (int j = 0; j < tmplist.Count; j++)
                    {
                        if (tmplist[j].type == 1)
                        {
                            var Fuel = dbOps.GetFuelData(tmplist[j].fuel, year, i);
                            tmplist[j].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1)) * Fuel.B_y), 1));
                            tmplist[j].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1)) * Fuel.B_y), 1));
                        }
                        if (tmplist[j].type == 2)
                        {
                            var Factor = dbOps.GetFactorData(tmplist[j].type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                            tmplist[j].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1)) * Factor.value), 1));
                            tmplist[j].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1)) * Factor.value), 1));
                        }
                        if (tmplist[j].type == 3)
                        {
                            var Factor = dbOps.GetFactorData(tmplist[j].type, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
                            tmplist[j].val_fact_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1)) * Factor.value), 1));
                            tmplist[j].val_plan_ut = Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1)) * Factor.value), 1));
                        }
                    }
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < NormList.Count; j++)
                        {
                            NormList[j].val_plan += Convert.ToSingle(Math.Round(tmplist[j].val_plan, 1));
                            NormList[j].val_fact += Convert.ToSingle(Math.Round(tmplist[j].val_fact, 1));
                            NormList[j].val_plan_ut += Convert.ToSingle(Math.Round(tmplist[j].val_plan_ut, 1));
                            NormList[j].val_fact_ut += Convert.ToSingle(Math.Round(tmplist[j].val_fact_ut, 1));
                        }
                    }
                }
            }
            return NormList;
        }

        private List<RecievedTable> MakeRecListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, 1);
            var RecievedList = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id, profile_num);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id, profile_num);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < RecievedList.Count; j++)
                        {
                            int index = tmplist.FindIndex(a => a.Id == RecievedList[j].Id);
                            if (index > 0)
                                RecievedList[j].value += Convert.ToSingle(Math.Round(tmplist[index].value, 1));
                        }
                    }
                }
            }
            return RecievedList;
        }
        private List<RecievedTable> MakeRecListSumPR(int year, int month)
        {
            List<RecievedTable> RecievedList = new List<RecievedTable>();
            for (int i = 1; i <= month; i++)
            {
                int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                var RecievedListTemp = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id, profile_num);
                foreach (var t in RecievedListTemp)
                {
                    if (RecievedList.Any(rec => rec.Id_org == t.Id_org && rec.res_type == t.res_type))
                    {
                        var index = RecievedList.FindIndex(rec => rec.Id_org == t.Id_org && rec.res_type == t.res_type);
                        if (index >= 0)
                            RecievedList[index].value += Convert.ToSingle(Math.Round(t.value, 1));
                    }
                    else
                    {
                        RecievedList.Add(t);
                    }
                }
            }
            return RecievedList;
        }

        private List<SendedTable> MakeSendListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, 1);
            var SendedList = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id, profile_num);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id, profile_num);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < SendedList.Count; j++)
                        {
                            int index = tmplist.FindIndex(a => a.Id == SendedList[j].Id);
                            if (index > 0)
                                SendedList[j].value += Convert.ToSingle(Math.Round(tmplist[index].value, 1));
                        }
                    }
                }
            }
            return SendedList;
        }
        private List<SendedTable> MakeSendListSumPR(int year, int month)
        {
            List<SendedTable> SendedList = new List<SendedTable>();
            for (int i = 1; i <= month; i++)
            {
                int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                var SendedListTemp = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id, profile_num);
                foreach (var t in SendedListTemp)
                {
                    if (SendedList.Any(send => send.Id_org == t.Id_org && send.res_type == t.res_type))
                    {
                        var index = SendedList.FindIndex(send => send.Id_org == t.Id_org && send.res_type == t.res_type);
                        if (index >= 0)
                            SendedList[index].value += Convert.ToSingle(Math.Round(t.value, 1));
                    }
                    else
                    {
                        SendedList.Add(t);
                    }
                }
            }
            return SendedList;
        }

        private List<SourceTable> MakeSourceSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, 1);
            var SourceList = dbOps.GetSourceList(CurrentData.UserData.Id_org, report_id, profile_num);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetSourceList(CurrentData.UserData.Id_org, report_id, profile_num);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < SourceList.Count; j++)
                        {
                            SourceList[j].Value += Convert.ToSingle(Math.Round(tmplist[j].Value, 1));
                        }
                    }
                }
            }
            return SourceList;
        }
        private List<SourceTable> MakeSourceSumPR(int year, int month)
        {
            List<SourceTable> SourceList = new List<SourceTable>();
            for (int i = 1; i <= month; i++)
            {
                int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                var SourceListTemp = dbOps.GetSourceList(CurrentData.UserData.Id_org, report_id, profile_num);
                foreach (var t in SourceListTemp)
                {
                    if (SourceList.Any(sorc => sorc.Id_object == t.Id_object && sorc.Id_fuel == t.Id_fuel && sorc.Res_type == t.Res_type))
                    {
                        var index = SourceList.FindIndex(sorc => sorc.Id_object == t.Id_object && sorc.Id_fuel == t.Id_fuel && sorc.Res_type == t.Res_type);
                        if (index >= 0)
                            SourceList[index].Value += Convert.ToSingle(Math.Round(t.Value, 1));
                    }
                    else
                    {
                        SourceList.Add(t);
                    }
                }
            }
            return SourceList;
        }


        private List<FTradeTable> MakeTFuelSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, 1);
            var TFuelList = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id, profile_num);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id, profile_num);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < TFuelList.Count; j++)
                        {
                            TFuelList[j].Value += Convert.ToSingle(Math.Round(tmplist[j].Value, 1));
                        }
                    }
                }
            }
            return TFuelList;
        }
        private List<FTradeTable> MakeTFuelSumPR(int year, int month)
        {
            List<FTradeTable> TFuelList = new List<FTradeTable>();
            for (int i = 1; i <= month; i++)
            {
                int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                var TFuelListTemp = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id, profile_num);
                foreach (var t in TFuelListTemp)
                {
                    if (TFuelList.Any(fuel => fuel.fuel_code == t.fuel_code))
                    {
                        var index = TFuelList.FindIndex(fuel => fuel.fuel_code == t.fuel_code);
                        TFuelList[index].Value += Convert.ToSingle(Math.Round(t.Value, 1));
                    }
                    else
                    {
                        TFuelList.Add(t);
                    }
                }
            }
            return TFuelList;
        }

        private List<TradeTable> MakeTradeSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var TradeList = dbOps.GetTrades(report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetTrades(report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < TradeList.Count; j++)
                        {
                            TradeList[j].value += Convert.ToSingle(Math.Round(tmplist[j].value, 1));
                        }
                    }
                }
            }
            return TradeList;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MakeTable1per();
            MakeTable12tek();
            MakeTable12tekHidden();
            MakeTable12TekPril();
        }

        private void dateTimePicker1_DropDown(object sender, DateTimePickerDropArgs e)
        {
            dateTimePicker1.ValueChanged -= dateTimePicker1_ValueChanged;
        }

        private void dateTimePicker1_CloseUp(object sender, DateTimePickerCloseArgs e)
        {
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker1_ValueChanged(sender, e);
        }

        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.relog = true;
            this.Close();
        }

        private void сохранитьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var workbook = reoGridControl3;
            workbook.Worksheets[1] = reoGrid4.CurrentWorksheet;
            workbook.Worksheets[1].Name = "Приложение";
            var name = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            workbook.Save(Directory.GetCurrentDirectory() + "\\12_TEK_" + name.Replace("\"", " ") + "_" + dateTimePicker1.Value.ToString("yyyy") + "_" + dateTimePicker1.Value.ToString("MMMM") + ".xlsx", unvell.ReoGrid.IO.FileFormat.Excel2007);
            //worksheet4.Workbook.Save(Directory.GetCurrentDirectory() + "\\12_TEK_Prilozhenie_" + name.Replace("\"", " ") + "_" + dateTimePicker1.Value.ToString("yyyy") + "_" + dateTimePicker1.Value.ToString("MMMM") + ".xlsx", unvell.ReoGrid.IO.FileFormat.Excel2007);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MakeTable4norm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MakeTable1tek();
        }

        private void сохранитьОтчет1ТЭКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var workbook = reoGridControl5;
            var name = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            workbook.Save(Directory.GetCurrentDirectory() + "\\1_TEK_" + name.Replace("\"", " ") + "_" + dateTimePicker1.Value.ToString("yyyy") + ".xlsx", unvell.ReoGrid.IO.FileFormat.Excel2007);
            ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("Файл " + "1_TEK_" + name.Replace("\"", " ") + "_" + dateTimePicker1.Value.ToString("yyyy") + ".xlsx" + " успешно сохранен в папку программы");

        }

        private void RUPButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                MakeTable12teSUM(false);
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
                MakeTable1tekSUM(false);
        }

        private void POButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                MakeTable12teSUM(true);
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
                MakeTable1tekSUM(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentData.UserData.Id_org = Int32.Parse(CompanyBox.SelectedValue.ToString());
            MakeTable1per();
            MakeTable12tek();
            MakeTable12tekHidden();
            MakeTable12TekPril();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // dbOps.GetCompanyData(CurrentData.UserData.Id_org);
        }

        private void сформировать1ПЭРToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm = new ReportsCreateForm(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }
        private void myForm_FormClosed(object sender, EventArgs e)
        {
            MakeTable1per();
            MakeTable12tek();
            MakeTable12tekHidden();
            MakeTable12TekPril();
        }

        private void мастерВвода4Нормы1ТЭКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm = new NormReportCreateForm(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, MakeQuater(dateTimePicker1.Value.Month));
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var myForm = new AddUserForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void списокПродуктовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm = new ProductsListForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void списокСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var myForm = new PersonsListForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void year1Button_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem but = (ToolStripMenuItem)sender;
            yearButton.Text = but.Text;

            this.dateTimePicker1.Value = new DateTime(Int32.Parse(but.Text), dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
        }

        private void qButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem quart = (ToolStripMenuItem)sender;
            switch (quart.Text)
            {
                case "I квартал":
                    this.dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, 3, 1);
                    break;
                case "II квартал":
                    this.dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, 6,1);
                    break;
                case "III квартал":
                    this.dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, 9, 1);
                    break;
                case "IV квартал":
                    this.dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, 12, 1);
                    break;
                default:
                    break;
            }
            toolStripDropDownButton1.Text = quart.Text;
        }

        private void monthSelectBut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mont = (ToolStripMenuItem)sender;
            int month = DateTime.ParseExact(mont.Text.TrimStart('&'), "MMMM", new System.Globalization.CultureInfo("ru-Ru")).Month;
            this.dateTimePicker1.Value = new DateTime(dateTimePicker1.Value.Year, month, 1);
            MonthBut.Text = mont.Text;
            MonthBut.Image = mont.Image;
        }

        private void списокТопливныхРеусурсовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm = new FuelListForm();
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void профильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var myForm = new ProfileForm(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.Show();
        }

        private void LoadOrgTreeObjects()
        {
            var headlist = dbOps.GetStructCompanyList(-1);
            var subheadlist = dbOps.GetStructCompanyList(1);
            var list100 = dbOps.GetStructCompanyList(100);
            var list200 = dbOps.GetStructCompanyList(200);
            var list1001 = dbOps.GetStructCompanyList(1001);
            treeView1.Nodes.Clear();
            TreeNode PO = new TreeNode();
            PO.Text = headlist[0].Name;
            PO.Tag = headlist[0].Id;
            PO.ImageIndex = 0;
            treeView1.Nodes.Add(PO);

            TreeNode Other = new TreeNode();
            Other.Text = headlist[1].Name;
            Other.Tag = headlist[1].Id;
            Other.ImageIndex = 1;
            Other.SelectedImageIndex = 1;
            treeView1.Nodes.Add(Other);

            TreeNode RUP = new TreeNode();
            RUP.Text = subheadlist[0].Name;
            RUP.Tag = subheadlist[0].Id;
            RUP.ImageIndex = 0;
            PO.Nodes.Add(RUP);

            TreeNode Child = new TreeNode();
            Child.Text = subheadlist[1].Name;
            Child.Tag = subheadlist[1].Id;
            Child.ImageIndex = 0;
            PO.Nodes.Add(Child);

            for (int i = 0; i < list100.Count; i++)
            {
                TreeNode org = new TreeNode();
                org.Text = list100[i].Name;
                org.Tag = list100[i].Id;
                org.ImageIndex = 0;
                RUP.Nodes.Add(org);
            }

            for (int i = 0; i < list200.Count; i++)
            {
                TreeNode org = new TreeNode();
                org.Text = list200[i].Name;
                org.Tag = list200[i].Id;
                org.ImageIndex = 0;
                Child.Nodes.Add(org);
            }

            for (int i = 0; i < list1001.Count; i++)
            {
                TreeNode org = new TreeNode();
                org.Text = list1001[i].Name;
                org.Tag = list1001[i].Id;
                org.ImageIndex = 1;
                org.SelectedImageIndex = 1;
                Other.Nodes.Add(org);
            }
            treeView1.ExpandAll();

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Int32.Parse(e.Node.Tag.ToString()) != 1 && Int32.Parse(e.Node.Tag.ToString()) != 100 && Int32.Parse(e.Node.Tag.ToString()) != 200 && Int32.Parse(e.Node.Tag.ToString()) < 1001)
            {
                label9.Text = ">>" + e.Node.Text;
                CurrentData.UserData.Id_org = Int32.Parse(e.Node.Tag.ToString());
                MakeTable1per();
                MakeTable12tek();
                MakeTable12tekHidden();
                MakeTable12TekPril();

            }
        }
    }
}
public static class ExtensionMethods
{
    // Deep clone
    public static T DeepClone<T>(this T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }
}