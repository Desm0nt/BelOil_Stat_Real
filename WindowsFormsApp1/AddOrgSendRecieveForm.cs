using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBO;
using ComponentFactory.Krypton.Toolkit;
using WindowsFormsApp1.DataTables;

namespace WindowsFormsApp1
{
    public partial class AddOrgSendRecieveForm : KryptonForm
    {
        public ProfSendRecTable SendRecTable = new ProfSendRecTable();
        public AddOrgSendRecieveForm(int index)
        {
            InitializeComponent();
            typeComboBox.DisplayMember = "Text";
            typeComboBox.ValueMember = "Value";

            var items = new[] { new { Text = "Тепловая энергия", Value = "2" }, new { Text = "Электрическая энергия", Value = "3" } };
            typeComboBox.DataSource = items;
            typeComboBox.SelectedIndex = 0;
            typeComboBox.Visible = true;
            LoadOrgTreeObjects();
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
            PO.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
            PO.Tag = headlist[0].Id;
            PO.ImageIndex = 0;
            treeView1.Nodes.Add(PO);

            TreeNode Other = new TreeNode();
            Other.Text = headlist[1].Name;
            Other.Tag = headlist[1].Id;
            Other.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
            Other.ImageIndex = 1;
            Other.SelectedImageIndex = 1;
            treeView1.Nodes.Add(Other);

            TreeNode RUP = new TreeNode();
            RUP.Text = subheadlist[0].Name;
            RUP.Tag = subheadlist[0].Id;
            RUP.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
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
                org.ToolTipText = list100[i].Pid.ToString();
                org.ImageIndex = 0;
                RUP.Nodes.Add(org);
            }

            for (int i = 0; i < list200.Count; i++)
            {
                TreeNode org = new TreeNode();
                org.Text = list200[i].Name;
                org.Tag = list200[i].Id;
                org.ToolTipText = list200[i].Pid.ToString();
                org.ImageIndex = 0;
                Child.Nodes.Add(org);
            }

            for (int i = 0; i < list1001.Count; i++)
            {
                TreeNode org = new TreeNode();
                org.Text = list1001[i].Name;
                org.Tag = list1001[i].Id;
                org.ToolTipText = "0";
                org.ImageIndex = 1;
                org.SelectedImageIndex = 1;
                Other.Nodes.Add(org);
            }
            treeView1.ExpandAll();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn == null)
                MessageBox.Show("Элемент не выбран.");
            else               
            {
                SendRecTable.Id = 0;
                SendRecTable.Id_org = Int32.Parse(tn.Tag.ToString());
                SendRecTable.Org_name = tn.Text;
                SendRecTable.Type = Int32.Parse(typeComboBox.SelectedValue.ToString());
                SendRecTable.Head = Int32.Parse(tn.ToolTipText);
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }
    }
}
