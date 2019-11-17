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
        public AddOrgObjectForm()
        {
            InitializeComponent();
            OrgLable.Text = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
            NameTextbox.Text = "Новый объект (" + OrgLable.Text + ")";
            FullnameTextBox.Text = "Новый объект (" + OrgLable.Text + ")";
            this.Text = "Данные нового объекта " + OrgLable.Text;
        }

        public AddOrgObjectForm(int obj_id, string name, string fullname)
        {
            InitializeComponent();
            NameTextbox.Text = name;
            FullnameTextBox.Text = fullname;
            OrgLable.Text = dbOps.GetCompanyName(CurrentData.UserData.Id_org);
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
                    dbOps.UpdateObject(id, CurrentData.UserData.Id_org, NameTextbox.Text, FullnameTextBox.Text);
                }
                else
                {
                    dbOps.AddNewObject(CurrentData.UserData.Id_org, NameTextbox.Text, FullnameTextBox.Text);
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
