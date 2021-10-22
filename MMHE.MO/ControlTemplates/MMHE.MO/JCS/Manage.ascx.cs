using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls.JCS
{
	public partial class Manage : UserControl
	{
		public JCSDetails Details { get; set; }
		public int LastRowIndex = 0;
		public string JCSId = string.Empty;
		protected void Page_Load(object sender, EventArgs e)
		{
			JCSId = Request.QueryString["id"];
			Details = new JCSRepository().GetDetails(JCSId);
			jcsRepeater.DataSource = Details.Activities;
			jcsRepeater.DataBind();
			LastRowIndex = Details.Activities.Count + 1;
		}
	}
}
