using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls
{
	public partial class DWC : UserControl
	{
		public string Today { get; set; }
		public string Tomorrow { get; set; }
		IEnumerable<IGrouping<string,DWCDetails>> Details { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			Today = DateTime.Now.ToString("dd-MMM-yy");
			Tomorrow = DateTime.Now.AddDays(1).ToString("dd-MMM-yy");
			var user = (Page as BasePage).LoggedInUser;
			var details = new DWCRepository().GetAll(user.ProjectId, user.Id);
			var progress = new List<DWCDetails>();
			DWCActivity activity;
			foreach (var jcs in details.GroupBy(d => d.JCSID))
			{

				if (jcs.Any(d => d.ActivityID.HasValue))
				{
					activity = jcs.First();
					progress.Add(new DWCDetails
					{
						Work_Title = activity.Work_Title,
						CompletionPer = activity.CompletionPer,
						EndDate= activity.EndDate,
						JCSID = activity.JCSID,
						JSLStatus = activity.JSLStatus,
						lType = activity.lType,
						MyGroup = activity.MyGroup,
						OwnerNo = activity.OwnerNo,
						StartDate = activity.StartDate,
						Type = activity.Type,
						WBS = activity.WBS,
						ActivityProgress = jcs.Where(d=>d.ActivityID.HasValue).Select(d=> new DWCProgress
						{
							ActivityID = d.ActivityID.Value,
							ActivityTitle = d.ActivityTitle,
							ActivityType = d.ActivityType,
							Subscontractor = d.Subscontractor,
							SubscontractorId = d.Subscontractor
						}).ToList()
					});
				}
			}

			Details = progress.GroupBy(d=>d.MyGroup).OrderBy(d => d.Key).ToList();

		}
	}
}
