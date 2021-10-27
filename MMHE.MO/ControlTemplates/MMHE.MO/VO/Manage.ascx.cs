using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Web.UI;

namespace MMHE.MO.Controls.VO
{
	public partial class Manage : UserControl
	{
		public VODetails Details { get; set; }
		public int LastRowIndex = 0;
		public string JCSId = string.Empty;
		protected void Page_Load(object sender, EventArgs e)
		{
			JCSId = Request.QueryString["id"];
			Details = new JCSRepository().GetVODetails(JCSId);
			jcsRepeater.DataSource = Details.Activities;
			jcsRepeater.DataBind();
			LastRowIndex = Details.Activities.Count + 1;
		}
	}
}
