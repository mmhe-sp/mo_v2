using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
    public class UpdatedDWCProgress
    {
        public DateTime Today { get; set; }
        public DateTime Tomorrow { get; set; }
        public List<JCSProgress> JCS { get; set; }
    }
}
