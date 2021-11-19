using System;

namespace MMHE.MO.Models
{
	public class DWCProgress
	{
		public Guid ActivityID { get; set; }
		public string ActivityType { get; set; }
		public string ActivityTitle { get; set; }
		public string Subscontractor { get; set; }
		public string SubscontractorId { get; set; }
	}
}
