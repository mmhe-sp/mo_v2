using MMHE.MO.UI;

using System;
using System.Web.UI;

namespace MMHE.MO.Controls
{
	public partial class ProjectName : UserControl
	{
		public string Project { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
            var user = (Page as BasePage).LoggedInUser;
            Project = user.ProjectId + "-" + user.ProjectName;
		}
	}
}
