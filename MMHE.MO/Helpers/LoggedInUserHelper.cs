using Microsoft.SharePoint;

using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

namespace MMHE.MO.Helpers
{
	public static class LoggedInUserHelper
	{
		public static IdentityUser InitializeUser()
		{
			var web = SPContext.Current.Web;
			if (web.CurrentUser != null)
			{
				var userName = web.CurrentUser.LoginName;

				if (userName.Contains("i:0#.w|"))
				{
					userName = userName.Remove(0, 7);
				}
				return new UserRepository().GetUserDetails(userName);
			}
			return null;
		}
	}
}
