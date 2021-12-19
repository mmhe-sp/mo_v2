using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class JSLDetails
    {
        public Guid JSLID { get; set; }
        public string SequenceNo { get; set; }
        public string ProjectNo { get; set; }
        public string Type { get; set; }
        public string Discipline { get; set; }
        public string OwnerNo { get; set; }
        public string WBS { get; set; }
        public string WBSDesc { get; set; }
        public string WorkTitle { get; set; }
        public string Remakrs { get; set; }
        public string Status { get; set; }
        public string AWOVoNo { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? RcvPMT { get; set; }
        public DateTime? SubmitTo { get; set; }
        public DateTime? RcvClient { get; set; }
        public string AWOVORemarks { get; set; }
        public DateTime? DWCCompleted { get; set; }
        public DateTime? WCRPlan { get; set; }
        public DateTime? WCRAct { get; set; }
        public DateTime? WCRSign { get; set; }
        public string WCRStatus { get; set; }
        public double Weightage { get; set; }
        public string User { get; set; }
    }
}
