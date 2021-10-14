using MMHE.MO.Helpers;
using MMHE.MO.Models;

using System;

namespace MMHE.MO.UI
{
	public class BasePage : Microsoft.SharePoint.WebPartPages.WebPartPage
	{
		public IdentityUser LoggedInUser { get; set; }
		protected override void OnPreLoad(EventArgs e)
		{
			base.OnPreLoad(e);

			//Get Logged In User
			LoggedInUser = LoggedInUserHelper.InitializeUser();
		}
	}
}
