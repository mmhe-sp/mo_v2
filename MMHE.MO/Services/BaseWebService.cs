using MMHE.MO.Helpers;
using MMHE.MO.Models;

using System.Web.Services;

namespace MMHE.MO.Services
{
	public class BaseWebService : WebService
	{
		public IdentityUser LoggedInUser { get; set; }

		public BaseWebService()
		{
			LoggedInUser = LoggedInUserHelper.InitializeUser();
		}


	}
}
