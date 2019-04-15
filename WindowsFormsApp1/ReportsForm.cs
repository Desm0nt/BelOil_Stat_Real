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
        public ReportsForm()
        {
            InitializeComponent();
            year = dateTimePicker1.Value.Year;
            MakeTable1per();
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
            worksheet1["F3"] = String.Format("Данные за {0} месяц {1} года", dateTimePicker1.Value.ToString("MMMM"), year);
            worksheet1["P5"] = CurrentData.UserData.Post;
            worksheet1["S6"] = CurrentData.UserData.Name;
            worksheet1["P7"] = DateTime.Now.ToString("dd.MM.yyyy");

            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var NormList = dbOps.GetNormList(CurrentData.UserData.Id_org, report_id);
            var NormListSum = MakeListSum(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            var OldNormListSum = MakeListSum(dateTimePicker1.Value.Year-1, dateTimePicker1.Value.Month);
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
                    worksheet1[fuelrow-1 + tmp, 7] = String.Format("=ROUND({0}, 3)", a.val_plan);                    
                    worksheet1[fuelrow-1 + tmp, 9] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    worksheet1[fuelrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1[fuelrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1["E" + (fuelrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
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
                    worksheet1["P" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_plan);
                    worksheet1["R" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Fuel = dbOps.GetFuelData(a.fuel);
                    worksheet1["Q" + (fuelrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1["S" + (fuelrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    worksheet1["E" + (fuelrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet1["T" + (fuelrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", fuelrow + tmp);
                    worksheet1["U" + (fuelrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", fuelrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (fuelrow + tmp)] = Normo !=null? String.Format("=ROUND({0}, 3)", Normo.val_fact) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (fuelrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (fuelrow + tmp)] = String.Format("=ROUND({0}, 3)", Norm.val_fact);
                    worksheet1["G" + (fuelrow + tmp)] = String.Format("=ROUND(F{0} * {1}, 3)", fuelrow + tmp, Fuel.B_y);
                    tmp++;
                }
            }
            worksheet1["G" + (fuelrow -1)] = String.Format("=ROUND(SUM(G{0}:G{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["F" + (fuelrow - 1)] = String.Format("=ROUND(SUM(F{0}:F{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["H" + (fuelrow - 1)] = String.Format("=ROUND(SUM(H{0}:H{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["I" + (fuelrow - 1)] = String.Format("=ROUND(SUM(I{0}:I{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["J" + (fuelrow - 1)] = String.Format("=ROUND(SUM(J{0}:J{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["K" + (fuelrow - 1)] = String.Format("=ROUND(SUM(K{0}:K{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["L" + (fuelrow - 1)] = String.Format("=ROUND(IF(I{0}>0, K{0}/I{0}, 0), 3)", fuelrow - 1);
            worksheet1["M" + (fuelrow - 1)] = String.Format("=ROUND(IF(G{0}>0, K{0}/G{0}, 0), 3)", fuelrow - 1);
            worksheet1["N" + (fuelrow - 1)] = String.Format("=ROUND(SUM(N{0}:N{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["O" + (fuelrow - 1)] = String.Format("=ROUND(SUM(O{0}:O{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["P" + (fuelrow - 1)] = String.Format("=ROUND(SUM(P{0}:P{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["Q" + (fuelrow - 1)] = String.Format("=ROUND(SUM(Q{0}:Q{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["R" + (fuelrow - 1)] = String.Format("=ROUND(SUM(R{0}:R{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["S" + (fuelrow - 1)] = String.Format("=ROUND(SUM(S{0}:S{1}), 3)", fuelrow, fuelrow + counter - 1);
            worksheet1["T" + (fuelrow - 1)] = String.Format("=ROUND(IF(Q{0}>0, S{0}/Q{0}, 0), 3)", fuelrow-1);
            worksheet1["U" + (fuelrow - 1)] = String.Format("=ROUND(IF(O{0}>0, S{0}/O{0}, 0), 3)", fuelrow - 1);

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
                    worksheet1[heatrow - 1 + tmp, 7] = String.Format("=ROUND({0}, 3)", a.val_plan);
                    worksheet1[heatrow - 1 + tmp, 9] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Factor = dbOps.GetFactorData(a.type);
                    worksheet1[heatrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1[heatrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1["E" + (heatrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
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
                    worksheet1["P" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_plan);
                    worksheet1["R" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Factor = dbOps.GetFactorData(a.type);
                    worksheet1["Q" + (heatrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1["S" + (heatrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    worksheet1["E" + (heatrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet1["T" + (heatrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", heatrow + tmp);
                    worksheet1["U" + (heatrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", heatrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (heatrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Normo.val_fact) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (heatrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", heatrow + tmp, Factor.value);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (heatrow + tmp)] = String.Format("=ROUND({0}, 3)", Norm.val_fact);
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
                    worksheet1[elrow - 1 + tmp, 7] = String.Format("=ROUND({0}, 3)", a.val_plan);
                    worksheet1[elrow - 1 + tmp, 9] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Factor = dbOps.GetFactorData(a.type);
                    worksheet1[elrow - 1 + tmp, 8] = String.Format("=ROUND(H{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1[elrow - 1 + tmp, 10] = String.Format("=ROUND(J{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1["E" + (elrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
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
                    worksheet1["P" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_plan);
                    worksheet1["R" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", a.val_fact);
                    var Factor = dbOps.GetFactorData(a.type);
                    worksheet1["Q" + (elrow + tmp)] = String.Format("=ROUND(P{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1["S" + (elrow + tmp)] = String.Format("=ROUND(R{0} * {1}, 3)", elrow + tmp, Factor.value);
                    worksheet1["E" + (elrow + tmp)] = dbOps.GetProdUnit(a.Id_prod);
                    worksheet1["T" + (elrow + tmp)] = String.Format("=ROUND(IF(P{0}>0, R{0}/P{0}, 0), 3)", elrow + tmp);
                    worksheet1["U" + (elrow + tmp)] = String.Format("=ROUND(IF(N{0}>0, R{0}/N{0}, 0), 3)", elrow + tmp);
                    var Normo = OldNormListSum.FirstOrDefault(x => x.Id == a.Id);
                    worksheet1["N" + (elrow + tmp)] = Normo != null ? String.Format("=ROUND({0}, 3)", Normo.val_fact) : String.Format("=ROUND({0}, 3)", 0);
                    worksheet1["O" + (elrow + tmp)] = String.Format("=ROUND(N{0} * {1}, 3)", elrow + tmp, Factor.value);
                    int oldreport = dbOps.GetReportId(CurrentData.UserData.Id_org, dateTimePicker1.Value.Year - 1, dateTimePicker1.Value.Month);
                    var Norm = dbOps.GetOneNorm(CurrentData.UserData.Id_org, oldreport, a.Id);
                    worksheet1["F" + (elrow + tmp)] = String.Format("=ROUND({0}, 3)", Norm.val_fact);
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
            for (int i = 0; i <= 36; i++)
            {
                worksheet1.AutoFitRowHeight(i, true);
            }
            #endregion
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MakeTable1per();
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
    }
}
