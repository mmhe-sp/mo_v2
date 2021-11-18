using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls
{
	public partial class DWC : UserControl
	{
		public string Today { get; set; }
		public string Tomorrow { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			Today = DateTime.Now.ToString("dd-MMM-yy");
			Tomorrow = DateTime.Now.AddDays(1).ToString("dd-MMM-yy");
		}
	}
}
