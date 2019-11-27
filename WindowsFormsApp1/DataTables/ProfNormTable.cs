using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DataTables
{
    public class ProfNormTable
    {
        public int Id { get; set; }
        public int Id_prod { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string nUnit { get; set; }
        public int id_obj { get; set; }
        public bool s111 { get; set; }
        public bool s112 { get; set; }
        public int type { get; set; }
        public string Id_local { get; set; }
        public string real_name { get; set; }
        public string name_with_fuel { get; set; }
        public int id_fuel { get; set; }
    }
}
