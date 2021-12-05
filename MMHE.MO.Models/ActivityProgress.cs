﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class ActivityProgress
    {
        public string ActivityID { get; set; }
        public string Remarks { get; set; }
        public string Today { get; set; }
        public string Tomorrow { get; set; }
        public decimal Completion { get; set; }
        public string SubContractorRemarks { get; set; }
    }
}
