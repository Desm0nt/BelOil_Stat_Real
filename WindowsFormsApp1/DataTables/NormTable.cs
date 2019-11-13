using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    [Serializable]
    public class NormTable
    {
        public int Id { get; set; }
        public int Id_org { get; set; }
        public int Id_prod { get; set; }
        public Int64 Id_local { get; set; }
        public int Code { get; set; }
        public string name { get; set; }
        public int? fuel { get; set; }
        public int type { get; set; }
        public string[] row_options { get; set; }
        public float val_plan { get; set; }
        public float val_fact { get; set; }
        public float val_plan_ut { get; set; }
        public float val_fact_ut { get; set; }
        public int norm_code { get; set; }
        public bool editable { get; set; }
    }
}
