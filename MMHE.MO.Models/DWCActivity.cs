using System;
using System.Collections.Generic;

namespace MMHE.MO.Models
{
	public class DWCActivity
	{
		public Guid JCSID { get; set; }
		public Guid? ActivityID { get; set; }
		public string ActivityType { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string ActivityTitle { get; set; }
		public string Work_Title { get; set; }
		public string OwnerNo { get; set; }
		public string MyGroup { get; set; }
		public decimal CompletionPer { get; set; }
		public string lType { get; set; }
		public string JSLStatus { get; set; }
		public string JSLRemarks { get; set; }
		public string Type { get; set; }
		public string Subscontractor { get; set; }
		public string WBS { get; set; }
	}
}
