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
    public partial class NormReportCreateForm : Form
    {
        int year, month, quater, id_org;
        public NormReportCreateForm(int years, int months, int quaters)
        {
            InitializeComponent();
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing1);
            this.dataGridView2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing);
            this.dataGridView3.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing);
            this.dataGridView4.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EditingControlShowing);
            year = years;
            month = months;
            quater = quaters;
            id_org = CurrentData.UserData.Id_org;
            List<Norm4InputTable> List4NormInput = dbOps.Get4NormInput(id_org, year, month, quater);
            if (dbOps.Exist4NormQuaterCheck(id_org, year, 1) == false)
            {
                foreach (var a in List4NormInput)
                {
                    if (!String.IsNullOrWhiteSpace(a.Fuel_name))
                    {
                        dbOps.Add4Norm(year, 1, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Fuel, a.Fuel_name, a.Id_obj);
                    }
                    else
                    {
                        dbOps.Add4Norm(year, 1, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Id_obj);
                    }
                }
            }
            if (dbOps.Exist4NormQuaterCheck(id_org, year, 2) == false)
            {
                foreach (var a in List4NormInput)
                {
                    if (!String.IsNullOrWhiteSpace(a.Fuel_name))
                    {
                        dbOps.Add4Norm(year, 2, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Fuel, a.Fuel_name, a.Id_obj);
                    }
                    else
                    {
                        dbOps.Add4Norm(year, 2, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Id_obj);
                    }
                }
            }
            if (dbOps.Exist4NormQuaterCheck(id_org, year, 3) == false)
            {
                foreach (var a in List4NormInput)
                {
                    if (!String.IsNullOrWhiteSpace(a.Fuel_name))
                    {
                        dbOps.Add4Norm(year, 3, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Fuel, a.Fuel_name, a.Id_obj);
                    }
                    else
                    {
                        dbOps.Add4Norm(year, 3, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Id_obj);
                    }
                }
            }
            if (dbOps.Exist4NormQuaterCheck(id_org, year, 4) == false)
            {
                foreach (var a in List4NormInput)
                {
                    if (!String.IsNullOrWhiteSpace(a.Fuel_name))
                    {
                        dbOps.Add4Norm(year, 4, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Fuel, a.Fuel_name, a.Id_obj);
                    }
                    else
                    {
                        dbOps.Add4Norm(year, 4, a.Id_org, a.Id_prod, a.Id_local, a.Id_norm, 0, 0, 0, a.Id_obj);
                    }
                }
            }
            List<Norm4InputQuater> NormQuater1 = dbOps.Get4NormQuaterType(id_org, year, month, 1, 1);
            List<Norm4InputQuater> NormQuater2 = dbOps.Get4NormQuaterType(id_org, year, month, 2, 1);
            List<Norm4InputQuater> NormQuater3 = dbOps.Get4NormQuaterType(id_org, year, month, 3, 1);
            List<Norm4InputQuater> NormQuater4 = dbOps.Get4NormQuaterType(id_org, year, month, 4, 1);
            List<AllNormTable> allNormList = new List<AllNormTable>();
            foreach (var a in NormQuater1)
            {
                allNormList.Add(new AllNormTable { Norm_id = a.Id_norm, Norm_name = a.Norm_name, q1Id = a.Id, q1Norm = a.norm });
            }
            foreach (var a in NormQuater2)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q2Id = a.Id;
                allNormList[idx].q2Norm = a.norm;
            }
            foreach (var a in NormQuater3)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q3Id = a.Id;
                allNormList[idx].q3Norm = a.norm;
            }
            foreach (var a in NormQuater4)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q4Id = a.Id;
                allNormList[idx].q4Norm = a.norm;
            }
            dataGridView1.DataSource = allNormList;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "1-ый квартал";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "2-ой квартал";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "3-ий квартал";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].HeaderText = "4-ый квартал";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<AllNormTable> allNormListt = dataGridView1.DataSource as List<AllNormTable>;
            foreach (var a in allNormListt)
            {
                dbOps.Update4Norm_norm(a.q1Id, a.q1Norm);
                dbOps.Update4Norm_norm(a.q2Id, a.q2Norm);
                dbOps.Update4Norm_norm(a.q3Id, a.q3Norm);
                dbOps.Update4Norm_norm(a.q4Id, a.q4Norm);
            }
            List<Norm4InputQuater> NormQuater1 = dbOps.Get4NormQuaterType(id_org, year, month, 1, 2);
            List<Norm4InputQuater> NormQuater2 = dbOps.Get4NormQuaterType(id_org, year, month, 2, 2);
            List<Norm4InputQuater> NormQuater3 = dbOps.Get4NormQuaterType(id_org, year, month, 3, 2);
            List<Norm4InputQuater> NormQuater4 = dbOps.Get4NormQuaterType(id_org, year, month, 4, 2);
            List<AllNormTable> allNormList = new List<AllNormTable>();
            foreach (var a in NormQuater1)
            {
                allNormList.Add(new AllNormTable { Norm_id = a.Id_norm, Norm_name = a.Norm_name, q1Id = a.Id, q1Norm = a.norm });
            }
            foreach (var a in NormQuater2)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q2Id = a.Id;
                allNormList[idx].q2Norm = a.norm;
            }
            foreach (var a in NormQuater3)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q3Id = a.Id;
                allNormList[idx].q3Norm = a.norm;
            }
            foreach (var a in NormQuater4)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q4Id = a.Id;
                allNormList[idx].q4Norm = a.norm;
            }
            dataGridView1_1.DataSource = allNormList;
            dataGridView1_1.Columns[0].Visible = false;
            dataGridView1_1.Columns[1].ReadOnly = true;
            dataGridView1_1.Columns[1].HeaderText = "Наименование";
            dataGridView1_1.Columns[2].Visible = false;
            dataGridView1_1.Columns[3].HeaderText = "1-ый квартал";
            dataGridView1_1.Columns[4].Visible = false;
            dataGridView1_1.Columns[5].HeaderText = "2-ой квартал";
            dataGridView1_1.Columns[6].Visible = false;
            dataGridView1_1.Columns[7].HeaderText = "3-ий квартал";
            dataGridView1_1.Columns[8].Visible = false;
            dataGridView1_1.Columns[9].HeaderText = "4-ый квартал";
            panel1.Visible = false;
            panel1_1.Visible = true;
        }

        private void button1_1_Click(object sender, EventArgs e)
        {
            List<AllNormTable> allNormListt = dataGridView1_1.DataSource as List<AllNormTable>;
            foreach (var a in allNormListt)
            {
                dbOps.Update4Norm_norm(a.q1Id, a.q1Norm);
                dbOps.Update4Norm_norm(a.q2Id, a.q2Norm);
                dbOps.Update4Norm_norm(a.q3Id, a.q3Norm);
                dbOps.Update4Norm_norm(a.q4Id, a.q4Norm);
            }
            List<Norm4InputQuater> NormQuater1 = dbOps.Get4NormQuaterType(id_org, year, month, 1, 3);
            List<Norm4InputQuater> NormQuater2 = dbOps.Get4NormQuaterType(id_org, year, month, 2, 3);
            List<Norm4InputQuater> NormQuater3 = dbOps.Get4NormQuaterType(id_org, year, month, 3, 3);
            List<Norm4InputQuater> NormQuater4 = dbOps.Get4NormQuaterType(id_org, year, month, 4, 3);
            List<AllNormTable> allNormList = new List<AllNormTable>();
            foreach (var a in NormQuater1)
            {
                allNormList.Add(new AllNormTable { Norm_id = a.Id_norm, Norm_name = a.Norm_name, q1Id = a.Id, q1Norm = a.norm });
            }
            foreach (var a in NormQuater2)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q2Id = a.Id;
                allNormList[idx].q2Norm = a.norm;
            }
            foreach (var a in NormQuater3)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q3Id = a.Id;
                allNormList[idx].q3Norm = a.norm;
            }
            foreach (var a in NormQuater4)
            {
                var aa = allNormList.Find(i => i.Norm_id == a.Id_norm);
                int idx = allNormList.IndexOf(aa);
                allNormList[idx].q4Id = a.Id;
                allNormList[idx].q4Norm = a.norm;
            }
            dataGridView1_2.DataSource = allNormList;
            dataGridView1_2.Columns[0].Visible = false;
            dataGridView1_2.Columns[1].ReadOnly = true;
            dataGridView1_2.Columns[1].HeaderText = "Наименование";
            dataGridView1_2.Columns[2].Visible = false;
            dataGridView1_2.Columns[3].HeaderText = "1-ый квартал";
            dataGridView1_2.Columns[4].Visible = false;
            dataGridView1_2.Columns[5].HeaderText = "2-ой квартал";
            dataGridView1_2.Columns[6].Visible = false;
            dataGridView1_2.Columns[7].HeaderText = "3-ий квартал";
            dataGridView1_2.Columns[8].Visible = false;
            dataGridView1_2.Columns[9].HeaderText = "4-ый квартал";
            panel1_1.Visible = false;
            panel1_2.Visible = true;
        }

        private void button1_2_Click(object sender, EventArgs e)
        {
            List<AllNormTable> allNormList = dataGridView1_2.DataSource as List<AllNormTable>;
            foreach (var a in allNormList)
            {
                dbOps.Update4Norm_norm(a.q1Id, a.q1Norm);
                dbOps.Update4Norm_norm(a.q2Id, a.q2Norm);
                dbOps.Update4Norm_norm(a.q3Id, a.q3Norm);
                dbOps.Update4Norm_norm(a.q4Id, a.q4Norm);
            }
            List<Norm4InputQuater> NormQuater = dbOps.Get4NormQuaterType(id_org, year, month, quater, 1);
            dataGridView2.DataSource = NormQuater;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[5].HeaderText = "Количество продукции";
            panel1_2.Visible = false;
            panel2.Visible = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            List<Norm4InputQuater> NormQuate = dataGridView2.DataSource as List<Norm4InputQuater>;
            foreach (var a in NormQuate)
            {
                dbOps.Update4Norm_value_volume(a.Id, dbOps.GetNormSum(id_org, year, quater, a.Id_norm), a.volume);
            }
            List<Norm4InputQuater> NormQuater = dbOps.Get4NormQuaterType(id_org, year, month, quater, 2);
            dataGridView3.DataSource = NormQuater;
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[3].Visible = false;
            dataGridView3.Columns[4].Visible = false;
            dataGridView3.Columns[2].ReadOnly = true;
            dataGridView3.Columns[5].HeaderText = "Количество продукции";
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Norm4InputQuater> NormQuate = dataGridView3.DataSource as List<Norm4InputQuater>;
            foreach (var a in NormQuate)
            {
                dbOps.Update4Norm_value_volume(a.Id, dbOps.GetNormSum(id_org, year, quater, a.Id_norm), a.volume);
            }
            List<Norm4InputQuater> NormQuater = dbOps.Get4NormQuaterType(id_org, year, month, quater, 3);
            dataGridView4.DataSource = NormQuater;
            dataGridView4.Columns[0].Visible = false;
            dataGridView4.Columns[1].Visible = false;
            dataGridView4.Columns[3].Visible = false;
            dataGridView4.Columns[4].Visible = false;
            dataGridView4.Columns[2].ReadOnly = true;
            dataGridView4.Columns[5].HeaderText = "Количество продукции";
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Norm4InputQuater> NormQuate = dataGridView4.DataSource as List<Norm4InputQuater>;
            foreach (var a in NormQuate)
            {
                dbOps.Update4Norm_value_volume(a.Id, dbOps.GetNormSum(id_org, year, quater, a.Id_norm), a.volume);
            }
            this.Close();
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
            if (dataGridView.CurrentCell.ColumnIndex == 3 || dataGridView.CurrentCell.ColumnIndex == 5 || dataGridView.CurrentCell.ColumnIndex == 7 || dataGridView.CurrentCell.ColumnIndex == 9)
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
