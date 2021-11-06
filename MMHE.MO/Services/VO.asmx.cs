
using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
	[WebService]
    [ScriptService]
	public class VO : BaseWebService
	{
		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public object Save(VODetails vo)
		{
            new VORepository().Save(vo, LoggedInUser.Id);
			return new { Succeeded = true };
		}
	}
}
