using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MMHE.MO.Models
{
    public class DashboardStatistics
    {
        public DataTable TotalJSL { get; set; }
        public DataTable Statistics { get; set; }
        public DataTable TotalPerson { get; set; }
        public DataTable TotalTask { get; set; }
        public DataTable GraphDetails { get; set; }
    }
}
