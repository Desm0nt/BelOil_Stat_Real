using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class ReportsForm : Form
    {
        public int year;
        public List<NormTable> actualList = new List<NormTable>();
        public List<NormTable> oldList = new List<NormTable>();
        public ReportsForm()
        {
            InitializeComponent();
            year = dateTimePicker1.Value.Year;
            MakeTable1per();
            MakeTable12tek();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // dbOps.GetCompanyData(CurrentData.UserData.Id_org);
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
            var NormList = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
            var NormListSum = MakeListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            actualList = NormListSum;
            var OldNormListSum = MakeListSum(dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
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
                    worksheet1[fuelrow-1 + tmp, 2] = a.name;
                    worksheet1[fuelrow-1 + tmp, 3] = a.Code;
                    worksheet1[fuelrow-1 + tmp, 7] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));                    
                    worksheet1[fuelrow-1 + tmp, 9] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    var z1 = worksheet1.Cells["H" + (fuelrow + tmp)].Data;
                    worksheet1[fuelrow - 1 + tmp, 8] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["H" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    //worksheet1[fuelrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    //worksheet1[fuelrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1[fuelrow - 1 + tmp, 10] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["J" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
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
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    worksheet1["Q" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    //worksheet1["Q" + (fuelrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    //worksheet1["S" + (fuelrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1["S" + (fuelrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (fuelrow + tmp)].Data.ToString()) * Fuel.B_y, 1));
                    worksheet1["E" + (fuelrow + tmp)] = String.Format("=E{0}", fuelrow - 1);
                    worksheet1["T" + (fuelrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", fuelrow + tmp);
                    worksheet1["U" + (fuelrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", fuelrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (fuelrow + tmp)] = Normo !=null? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact,1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (fuelrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (fuelrow + tmp)] = String.Format("=ROUND(F{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    tmp++;
                }
            }
            var cell = worksheet1.Cells["G" + fuelrow];
            worksheet1["F" + (fuelrow - 1)] = cell.Data != null ? String.Format("=ROUND(SUM(F{0}:F{1}), 3)", fuelrow, fuelrow + counter - 1) : "0";
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
                    worksheet1[heatrow - 1 + tmp, 2] = a.name;
                    worksheet1[heatrow - 1 + tmp, 3] = a.Code;
                    worksheet1[heatrow - 1 + tmp, 7] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));
                    worksheet1[heatrow - 1 + tmp, 9] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Factor = dbOps.GetFactorData(a.type);
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
                    var Factor = dbOps.GetFactorData(a.type);
                    //worksheet1["Q" + (heatrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1["Q" + (heatrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1["S" + (heatrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1["S" + (heatrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (heatrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["E" + (heatrow + tmp)] = String.Format("=E{0}", heatrow - 1);
                    worksheet1["T" + (heatrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", heatrow + tmp);
                    worksheet1["U" + (heatrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", heatrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (heatrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact,1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (heatrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (heatrow + tmp)] = String.Format("=ROUND(F{0} * {1}, 3)", heatrow + tmp, Factor.value);
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
                    worksheet1[elrow - 1 + tmp, 2] = a.name;
                    worksheet1[elrow - 1 + tmp, 3] = a.Code;
                    worksheet1[elrow - 1 + tmp, 7] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_plan,1));
                    worksheet1[elrow - 1 + tmp, 9] = String.Format("=ROUND({0}, 3)", Math.Round(a.val_fact,1));
                    var Factor = dbOps.GetFactorData(a.type);
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
                    var Factor = dbOps.GetFactorData(a.type);
//                    worksheet1["Q" + (elrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1["Q" + (elrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["P" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    //worksheet1["S" + (elrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1["S" + (elrow + tmp)] = String.Format("{0}", Math.Round(float.Parse(worksheet1.Cells["R" + (elrow + tmp)].Data.ToString()) * Factor.value, 1));
                    worksheet1["E" + (elrow + tmp)] = String.Format("=E{0}", elrow - 1);
                    worksheet1["T" + (elrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", elrow + tmp);
                    worksheet1["U" + (elrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", elrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (elrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Math.Round(Normo.val_fact,1)) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (elrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", elrow + tmp, Factor.value);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Math.Round(Norm.val_fact,1));
                    worksheet1["G" + (elrow + tmp)] = String.Format("=ROUND(F{0} * {1}, 3)", elrow + tmp, Factor.value);
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
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y),1));
                    var Normo = oldList.FirstOrDefault(x => x.Id == a.Id);
                    oldSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    oldSum_ut += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact,1)) * Fuel.B_y),1)) : 0;
                    if (a.row_options.Count() == 2)
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                        oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                    }
                    else if (a.row_options.Count()==1 && a.row_options[0] == "111")
                    {
                        actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
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

            var TFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldTFuelListSum = MakeTFuelSum(dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);

            #region тепло продано всего
            float TFuelSum = 0;
            float OldTFuelSum = 0;
            foreach (var a in TFuelListSum)
            {
                    TFuelSum += Convert.ToSingle(Math.Round(a.Value, 1));
                    var Normo = OldTFuelListSum.FirstOrDefault(x => x.Id == a.Id);
                    OldTFuelSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldTFuelListSum.FirstOrDefault(x => x.Id == a.Id);
                    mOldTFuelSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldTFuelListSum.FirstOrDefault(x => x.Id == a.Id);
                    vOldTFuelSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    mestn_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    mestn_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    var Normo = oldList.FirstOrDefault(x => x.Id == a.Id);
                    mestn_oldSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    mestn_oldSum_ut += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0;
                    if (a.row_options.Count() == 2)
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                        mestn_oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        mestn_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        mestn_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        mestn_oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
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
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    oth_actSum += Convert.ToSingle(Math.Round(a.val_fact, 1));
                    oth_actSum_ut += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                    var Normo = oldList.FirstOrDefault(x => x.Id == a.Id);
                    oth_oldSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    oth_oldSum_ut += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0;
                    if (a.row_options.Count() == 2)
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                        oth_oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0; ;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        oth_actSum_111 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        oth_actSum_112 += Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(a.val_fact, 1)) * Fuel.B_y), 1));
                        oth_oldSum_112 += Normo != null ? Convert.ToSingle(Math.Round((Convert.ToSingle(Math.Round(Normo.val_fact, 1)) * Fuel.B_y), 1)) : 0;
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
                    var Normo = oldList.FirstOrDefault(x => x.Id == a.Id);
                    oldSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    if (a.row_options.Count() == 2)
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum2_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum_111 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
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
                    var Normo = oldList.FirstOrDefault(x => x.Id == a.Id);
                    oldSum3 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    if (a.row_options.Count() == 2)
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_111 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_112 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "111")
                    {
                        actSum3_111 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_111 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
                    }
                    else if (a.row_options.Count() == 1 && a.row_options[0] == "112")
                    {
                        actSum3_112 += Convert.ToSingle(Math.Round(a.val_fact, 1));
                        oldSum3_112 += Normo != null ? Convert.ToSingle(Math.Round(Normo.val_fact, 1)) : 0;
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

            var RecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldRecievedListSum = MakeRecListSum(dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);

            #region тепло получено
            float tRecSum = 0;
            float tOldRecSum = 0;
            foreach (var a in RecievedListSum)
            {
                if (a.res_type == 2)
                {
                    tRecSum += Convert.ToSingle(Math.Round(a.value, 1));
                    var Normo = OldRecievedListSum.FirstOrDefault(x => x.Id == a.Id);
                    tOldRecSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.value, 1)) : 0;
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
                    var Normo = OldRecievedListSum.FirstOrDefault(x => x.Id == a.Id);
                    eOldRecSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.value, 1)) : 0;
                }
            }
            worksheet2["G21"] = eRecSum;
            worksheet2["L21"] = eOldRecSum;
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
                    var Normo = OldSendedListSum.FirstOrDefault(x => x.Id == a.Id);
                    tOldSendSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.value, 1)) : 0;
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
                    var Normo = OldSendedListSum.FirstOrDefault(x => x.Id == a.Id);
                    eOldSendSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.value, 1)) : 0;
                }
            }
            worksheet2["G16"] = eSendSum;
            worksheet2["L16"] = eOldSendSum;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    tOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    tvOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    tsOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    eOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    evOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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
                    var Normo = OldSourceSum.FirstOrDefault(x => x.Id == a.Id);
                    esOldSourceSum += Normo != null ? Convert.ToSingle(Math.Round(Normo.Value, 1)) : 0;
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


        private List<NormTable> MakeListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var NormList = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < NormList.Count; j++)
                        {
                            NormList[j].val_plan += tmplist[j].val_plan;
                            NormList[j].val_fact += tmplist[j].val_fact;
                        }
                    }
                }
            }
            return NormList;
        }

        private List<RecievedTable> MakeRecListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var RecievedList = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetRecievedList(CurrentData.UserData.Id_org, report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < RecievedList.Count; j++)
                        {
                            RecievedList[j].value += tmplist[j].value;
                        }
                    }
                }
            }
            return RecievedList;
        }

        private List<SendedTable> MakeSendListSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var SendedList = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetSendedList(CurrentData.UserData.Id_org, report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < SendedList.Count; j++)
                        {
                            SendedList[j].value += tmplist[j].value;
                        }
                    }
                }
            }
            return SendedList;
        }

        private List<SourceTable> MakeSourceSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var SourceList = dbOps.GetSourceList(CurrentData.UserData.Id_org, report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetSourceList(CurrentData.UserData.Id_org, report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < SourceList.Count; j++)
                        {
                            SourceList[j].Value += tmplist[j].Value;
                        }
                    }
                }
            }
            return SourceList;
        }

        private List<FTradeTable> MakeTFuelSum(int year, int month)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            var TFuelList = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id);

            if (month > 1)
            {
                for (int i = 2; i <= month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id);
                    if (tmplist.Count != 0)
                    {
                        for (int j = 0; j < TFuelList.Count; j++)
                        {
                            TFuelList[j].Value += tmplist[j].Value;
                        }
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
                            TradeList[j].value += tmplist[j].value;
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
        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged -= dateTimePicker1_ValueChanged;
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
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
    }
}
