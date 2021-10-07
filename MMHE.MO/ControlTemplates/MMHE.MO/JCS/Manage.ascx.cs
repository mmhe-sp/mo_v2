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
		protected void Page_Load(object sender, EventArgs e)
		{
			Details = new JCSRepository().GetDetails(Request.QueryString["id"]);
			jcsRepeater.DataSource = Details.Activities;
			jcsRepeater.DataBind();
		}
	}
}
