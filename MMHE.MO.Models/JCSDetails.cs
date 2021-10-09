using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class JCSDetails
    {
        public Guid JCSID { get; set; }
        public string Type { get; set; }
        public string OwnerNo { get; set; }
        public string Discipline { get; set; }
        public string WorkTitle { get; set; }
        public string WBS { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Duration { get; set; }
        public List<JCSActivityDetails> Activities { get; set; }
		public object Resources { get; set; }
	}
}
