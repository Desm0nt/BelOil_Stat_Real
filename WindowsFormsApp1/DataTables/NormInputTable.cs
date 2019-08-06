using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class NormInputTable
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string name { get; set; }
        public string Ed_izm { get; set; }
        public float val_fact_old { get; set; }
        public float By { get; set; }
        public float val_plan { get; set; }
        public float val_fact { get; set; }
        public float val_fact_plan { get; set; }
        public float val_fact_factold { get; set; }
        public float val_fact_old_sum { get; set; }
        public float val_plan_sum { get; set; }
        public float val_fact_sum { get; set; }
        public float val_fact_plan_sum { get; set; }
        public float val_fact_factold_sum { get; set; }
        public float val_plan_sum_back { get; set; }
        public float val_fact_sum_back { get; set; }
    }
}
