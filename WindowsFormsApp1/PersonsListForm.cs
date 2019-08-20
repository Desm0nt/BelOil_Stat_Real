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
using WindowsFormsApp1.DBO;
using KryptonOutlookGrid.Classes;
using KryptonOutlookGrid.CustomColumns;

namespace WindowsFormsApp1
{
    public struct ListElement
    {
        public int Id;
        public string Name;
        public int Group;
    }

    public partial class PersonsListForm : KryptonForm
    {
        public static List<ListElement> elList;
        List<DataTables.PersonTable> personList;
        bool[] groustate = new bool[3];

        public PersonsListForm()
        {
            InitializeComponent();
        }

        private void PersonsListForm_Load(object sender, EventArgs e)
        {      
            LoadData(false);       
        }

        private void LoadData(bool edit)
        {

            personList = dbOps.GetPersonList(toolStripTextBox2.Text);
            elList = new List<ListElement>();
            foreach (var a in personList)
                elList.Add(new ListElement { Id = a.Id_org, Name = a.Orgs, Group = a.Subhead });
            //if (edit)
            //{
            //    for (int i = 0; i < kryptonOutlookGrid1.GroupCollection.Count; i++)
            //        groustate[i] = kryptonOutlookGrid1.GroupCollection[i].Collapsed;
            //}
            kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.ClearGroups();
            
            

            kryptonOutlookGrid1.GroupBox = kryptonOutlookGridGroupBox1;
            kryptonOutlookGrid1.RegisterGroupBoxEvents();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[12];
            columnsToAdd[0] = kryptonOutlookGrid1.Columns[0];
            columnsToAdd[1] = kryptonOutlookGrid1.Columns[1];
            columnsToAdd[2] = kryptonOutlookGrid1.Columns[2];
            columnsToAdd[3] = kryptonOutlookGrid1.Columns[3];
            columnsToAdd[4] = kryptonOutlookGrid1.Columns[4];
            columnsToAdd[5] = kryptonOutlookGrid1.Columns[5];
            columnsToAdd[6] = kryptonOutlookGrid1.Columns[6];
            columnsToAdd[7] = kryptonOutlookGrid1.Columns[7];
            columnsToAdd[8] = kryptonOutlookGrid1.Columns[8];
            columnsToAdd[9] = kryptonOutlookGrid1.Columns[9];
            columnsToAdd[10] = kryptonOutlookGrid1.Columns[10];
            columnsToAdd[11] = kryptonOutlookGrid1.Columns[11];

            //kryptonOutlookGrid1.Columns.AddRange(columnsToAdd);

            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[1], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[5], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[6], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[7], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[8], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[9], new OutlookGridDefaultGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[10], new OutlookGridPersonGroup(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid1.AddInternalColumn(kryptonOutlookGrid1.Columns[11], new OutlookGridPersonGroup2(null), SortOrder.Ascending, 1, -1);
            kryptonOutlookGrid1.Columns[0].Visible = false;
           // kryptonOutlookGrid1.Columns[9].Visible = false;
            kryptonOutlookGrid1.Columns[10].Visible = false;
            kryptonOutlookGrid1.Columns[11].Visible = false;

            kryptonOutlookGrid1.ShowLines = false;

            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            //kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = FillMode.GROUPSONLY;

            foreach (var person in personList)
            {
                row = new OutlookGridRow();               
                row.CreateCells(kryptonOutlookGrid1, new object[] {
                    person.Id,
                    new TextAndImage(person.Surname, Properties.Resources.cbx2p_h0eqv),
                    person.Name,
                    person.Otchestvo,
                    new TextAndImage(GetPText(person.Type), GetPFlag(person.Type)),
                    person.Post,
                    person.Phone,
                    person.WPhone,
                    person.Email,
                    new TextAndImage(person.Head, Properties.Resources.predpr),
                    new TextAndImage(person.Subhead.ToString(), Properties.Resources.predpr),
                    new TextAndImage(person.Id_org.ToString(), Properties.Resources.predpr),
                }); 
                l.Add(row);
            }

            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();

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

        private Image GetPFlag(int type)
        {
            switch (type)
            {
                case 0:
                    return Properties.Resources.empty;
                case 1:
                    return Properties.Resources.rf;
                case 2:
                    return Properties.Resources.gf;
                default:
                    return null;
            }
        }
        private string GetPText(int type)
        {
            switch (type)
            {
                case 0:
                    return " - ";
                case 1:
                    return "рук.";
                case 2:
                    return "отв.";
                default:
                    return "";
            }
        }

        private void myForm_FormClosed(object sender, EventArgs e)
        {
            LoadData(true);
        }

        private void kryptonOutlookGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid dataGridView = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            if (e.RowIndex >= 0)
            {
                if (dataGridView.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    DataTables.PersonTable table = new DataTables.PersonTable
                    {
                        Id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()),
                        Name = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                        Surname = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(),
                        Otchestvo = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
                        Post = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(),
                        Email = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString(),
                        WPhone = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString(),
                        Phone = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(),
                        Id_org = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString())
                    };
                    var myForm = new PersonAddForm(table, this, elList);
                    //myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
                    myForm.ShowDialog();

                    if (myForm.DialogResult == DialogResult.OK)
                    {
                        var personTable = myForm.personTable;
                        dataGridView.Rows[e.RowIndex].Cells[0].Value = personTable.Id;
                        dataGridView.Rows[e.RowIndex].Cells[1].Value = new TextAndImage(personTable.Surname, Properties.Resources.cbx2p_h0eqv);
                        dataGridView.Rows[e.RowIndex].Cells[2].Value = personTable.Name;
                        dataGridView.Rows[e.RowIndex].Cells[3].Value = personTable.Otchestvo;
                        dataGridView.Rows[e.RowIndex].Cells[5].Value = personTable.Post;
                        dataGridView.Rows[e.RowIndex].Cells[6].Value = personTable.WPhone;
                        dataGridView.Rows[e.RowIndex].Cells[7].Value = personTable.Phone;
                        dataGridView.Rows[e.RowIndex].Cells[8].Value = personTable.Email;
                        dataGridView.Rows[e.RowIndex].Cells[11].Value = new TextAndImage(personTable.Id_org.ToString(), Properties.Resources.predpr);
                    }
                }
            }
        }


        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            var myForm = new PersonAddForm(elList);
            myForm.FormClosed += new FormClosedEventHandler(myForm_FormClosed);
            myForm.ShowDialog();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 1);
            var dataGridViewCellMouseEventArgs = new DataGridViewCellMouseEventArgs(kryptonOutlookGrid1.CurrentCell.ColumnIndex, kryptonOutlookGrid1.CurrentCell.RowIndex, 0, 0, mouseEventArgs);
            kryptonOutlookGrid1_CellMouseDoubleClick(kryptonOutlookGrid1, dataGridViewCellMouseEventArgs);
        }

        private void kryptonOutlookGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            KryptonOutlookGrid.Classes.KryptonOutlookGrid dataGridView = (KryptonOutlookGrid.Classes.KryptonOutlookGrid)sender;
            //if (dataGridView.Rows[e.RowIndex].Cells[1].Value != null)
            //{
            //    label1.Text = "#" + dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() + " - " + dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            //}
            //else
            //{
            //    string typestr = "Раздел: ";
            //    switch (Int32.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            //    {
            //        case 1:
            //            typestr += "топливо";
            //            break;
            //        case 2:
            //            typestr += "тепловая энергия";
            //            break;
            //        case 3:
            //            typestr += "электрическая энергия";
            //            break;
            //        default:
            //            break;
            //    }
            //    label1.Text = typestr;
            //}
        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {
            var ind = kryptonOutlookGrid1.CurrentCell.RowIndex;
            if (ind >= 0)
            {
                if (kryptonOutlookGrid1.Rows[ind].Cells[1].Value != null)
                    dbOps.DeleteFromPerson(Int32.Parse(kryptonOutlookGrid1.Rows[ind].Cells[0].Value.ToString()));
            }
            LoadData(true);
        }

        private void searchToolStripButton1_Click(object sender, EventArgs e)
        {
            LoadData(false);
        }

        private void resetToolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";
            LoadData(false);
        }
    }
}
