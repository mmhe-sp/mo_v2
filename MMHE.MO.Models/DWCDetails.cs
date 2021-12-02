using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
	public class DWCDetails
	{
		public Guid JCSID { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Work_Title { get; set; }
		public string OwnerNo { get; set; }
		public string MyGroup { get; set; }
		public decimal CompletionPer { get; set; }
		public string lType { get; set; }
		public string JSLStatus { get; set; }
		public string Type { get; set; }
		public string WBS { get; set; }

		public List<DWCProgress> ActivityProgress { get; set; }
        public string Remarks { get; set; }
        public string Today { get; set; }
        public string Tomorrow { get; set; }
	}
}
