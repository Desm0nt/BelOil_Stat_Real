using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class ProfFuelsTable
    {
        public int id { get; set; }
        public int fuel_id { get; set; }
        public string name { get; set; }
        public int Qn { get; set; }
        public float B_y { get; set; }
        public string unit { get; set; }
        public bool trade { get; set; }
    }
}
