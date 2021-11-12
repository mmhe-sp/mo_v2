using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.UI
{
    public class ManageVO : BasePage
    {
        public string Type { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string type = Request.QueryString["Type"];
            if (string.IsNullOrWhiteSpace(type) || string.Equals(type, "vo", StringComparison.InvariantCultureIgnoreCase))
                Type = "VO";
            else
                Type = "AWO";

        }
       
    }
}
