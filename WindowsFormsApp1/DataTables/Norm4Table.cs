using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class Norm4Table
    {
        public int Id_org { get; set; }
        public int Id_prod { get; set; }
        public long Id_local { get; set; }
        public string Norm_name { get; set; }
        public int Norm_code { get; set; }
        public int Norm_type { get; set; }
        public string Fuel_name { get; set; }
        public float Volume { get; set; }
        public float Norm { get; set; }
        public float Value { get; set; }
    }
}
