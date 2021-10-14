using Microsoft.SharePoint;

using MMHE.MO.Business.Repositories;
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
			InitializeUser();
		}


		private void InitializeUser()
		{
			var web = SPContext.Current.Web;
			if (web.CurrentUser != null)
			{
				var userName = web.CurrentUser.LoginName;

				if (userName.Contains("i:0#.w|"))
				{
					userName = userName.Remove(0, 7);
				}
				LoggedInUser = new UserRepository().GetUserDetails(userName);
			}
		}
	}
}
