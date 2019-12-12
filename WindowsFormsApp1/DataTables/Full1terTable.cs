using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class Full1terTable
    {
        public int Id { get; set; }
        public int Id_org { get; set; }
        public int Id_prod { get; set; }
        public Int64 Id_local { get; set; }
        public int Code { get; set; }
        public string name { get; set; }
        public string Unit { get; set; }
        public string nUnit { get; set; }
        public int? fuel { get; set; }
        public string fuel_name { get; set; }
        public int type { get; set; }
        public int id_rep { get; set; }
        public float koeff { get; set; }
        public string[] row_options { get; set; }
        public float val_plan { get; set; }
        public float val_fact { get; set; }
        public float val_fact_old { get; set; }
        public float val_plan_ut { get; set; }
        public float val_fact_ut { get; set; }
        public float sum_val_plan { get; set; }
        public float sum_val_fact { get; set; }
        public float sum_val_fact_old { get; set; }
        public float sum_val_plan_ut { get; set; }
        public float sum_val_fact_ut { get; set; }
        public int norm_code { get; set; }
        public bool editable { get; set; }
        public int id_obj { get; set; }
    }
}
