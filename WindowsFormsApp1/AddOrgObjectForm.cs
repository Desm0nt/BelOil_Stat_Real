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
using WindowsFormsApp1.DataTables;
using WindowsFormsApp1.DBO;

namespace WindowsFormsApp1
{
    public partial class AddOrgObjectForm : KryptonForm
    {
        int id;
        bool edit = false;
        int cur_org_id;
        public AddOrgObjectForm(int org_id)
        {
            InitializeComponent();
            cur_org_id = org_id;
            OrgLable.Text = dbOps.GetCompanyName(cur_org_id);
            NameTextbox.Text = "Новый объект (" + OrgLable.Text + ")";
            FullnameTextBox.Text = "Новый объект (" + OrgLable.Text + ")";
            this.Text = "Данные нового объекта " + OrgLable.Text;
        }

        public AddOrgObjectForm(int obj_id, string name, string fullname, int org_id)
        {
            InitializeComponent();
            cur_org_id = org_id;
            NameTextbox.Text = name;
            FullnameTextBox.Text = fullname;
            OrgLable.Text = dbOps.GetCompanyName(cur_org_id);
            id = obj_id;
            edit = true;
            this.Text = "Данные объекта " + fullname;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NameTextbox.Text) && !String.IsNullOrWhiteSpace(FullnameTextBox.Text))
            {          
                if (edit)
                {
                    dbOps.UpdateObject(id, cur_org_id, NameTextbox.Text, FullnameTextBox.Text);
                }
                else
                {
                    dbOps.AddNewObject(cur_org_id, NameTextbox.Text, FullnameTextBox.Text);
                }
                this.Close();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
