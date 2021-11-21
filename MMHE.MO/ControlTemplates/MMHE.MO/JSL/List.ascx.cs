using MMHE.MO.Business.Repositories;
using MMHE.MO.UI;

using System;
using System.Web.UI;

namespace MMHE.MO.Controls.JSL
{
	public partial class List : UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var user = (Page as BasePage).LoggedInUser;

			jcsRepeater.DataSource = new JSLRepository().GetAll(user.ProjectId);
			jcsRepeater.DataBind();
		}
	}
}
