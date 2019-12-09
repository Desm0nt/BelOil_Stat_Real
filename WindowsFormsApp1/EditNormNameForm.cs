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
    public partial class EditNormNameForm : KryptonForm
    {
        int id;
        bool edit = false;
        int cur_org_id;
        public string name_of_norm;
        public EditNormNameForm(string name)
        {
            InitializeComponent();
            NameTextbox.Text = name;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NameTextbox.Text))
            {
                name_of_norm = NameTextbox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
