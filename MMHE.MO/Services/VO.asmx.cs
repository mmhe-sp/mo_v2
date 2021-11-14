
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
            new VORepository().Save(vo, LoggedInUser.ProjectId, LoggedInUser.Id);
            return new { Succeeded = true };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Print(VODetails vo)
        {
            new VORepository().Print(vo, LoggedInUser.ProjectId, LoggedInUser.Id);
            return new { Succeeded = true };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object FinalApprove(VODetails vo)
        {
            new VORepository().FinalApprove(vo, LoggedInUser.ProjectId, LoggedInUser.Id);
            return new { Succeeded = true };
        }
    }
}
