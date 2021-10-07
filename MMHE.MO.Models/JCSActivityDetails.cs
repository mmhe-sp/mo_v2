using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class JCSActivityDetails
    {
        public Guid ActivityID { get; set; }
        public string Remarks { get; set; }
        public string Sequence { get; set; }
		public DateTime UpdatedOn { get; set; }
		public string Details { get; set; }
		public string UpdatedBy { get; set; }
		public string Resource { get; set; }
	}
}
