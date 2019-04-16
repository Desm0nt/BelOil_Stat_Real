using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class NormTable
    {
        public int Id { get; set; }
        public int Id_org { get; set; }
        public int Id_prod { get; set; }
        public int Code { get; set; }
        public string name { get; set; }
        public int? fuel { get; set; }
        public int type { get; set; }
        public string[] row_options { get; set; }
        public float val_plan { get; set; }
        public float val_fact { get; set; }
    }
}
