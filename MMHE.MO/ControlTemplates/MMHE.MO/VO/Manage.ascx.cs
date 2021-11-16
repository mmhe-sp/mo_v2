using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;

using System;
using System.Web.UI;

namespace MMHE.MO.Controls.VO
{
	public partial class Manage : UserControl
	{
		public VODetails Details { get; set; }
		public bool CanApprove { get; set; }
		public bool CanPrint { get; set; }
		public bool CanChange { get; set; }
		public int LastRowIndex = 0;
		public string JCSId = null;
		public string voId = null;
		protected void Page_Load(object sender, EventArgs e)
		{
			var user = (Page as BasePage).LoggedInUser;
			bool isNew = false;
			JCSId = Request.QueryString["id"];
			voId = Request.QueryString["jcs"];
			if (string.IsNullOrWhiteSpace(JCSId))
			{
				isNew = true;
			}

			Details = new VORepository().GetVODetails(JCSId, voId, user.ProjectId, user.Id);
			Details.Type = Type == "A" ? "Additional Work Order" : "Variation Order";
			jcsRepeater.DataSource = Details.Activities;
			jcsRepeater.DataBind();
			LastRowIndex = Details.Activities.Count + 1;
			if (Details.CanApprove && !isNew)
				CanApprove = true;

			if (Details.CanPrint && !isNew)
				CanPrint = true;
			if (Details.CanPrint && !isNew)
				CanPrint = true;
			
            CanChange = Type == "V" && isNew;
		}

		public string Type
		{
			get
			{
				string type = Request.QueryString["type"];
				if (string.IsNullOrWhiteSpace(type) || string.Equals(type, "v", StringComparison.InvariantCultureIgnoreCase))
					return "V";
				else
					return "A";
			}
		}
	}
}
