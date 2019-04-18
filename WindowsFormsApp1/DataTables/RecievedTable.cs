using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class RecievedTable
    {
        public int Id { get; set; }
        public int Id_owner { get; set; }
        public int Id_org { get; set; }
        public int res_type { get; set; }
        public string org_name { get; set; }
        public float value { get; set; }
    }
}
