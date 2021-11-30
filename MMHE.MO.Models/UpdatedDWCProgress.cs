using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class UpdatedDWCProgress
    {
        public List<ActivityProgress> Activities { get; set; }
        public string Today { get; set; }
        public string Tomorrow { get; set; }
    }
}
