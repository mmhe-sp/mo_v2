using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
    [WebService]
    [ScriptService]
    public class DWC : BaseWebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object SaveProgress(UpdatedDWCProgress dwc)
        {
            new DWCRepository().SaveProgress(LoggedInUser.ProjectId, LoggedInUser.Id, dwc);
            return new { Succeeded = true };
        }
    }
}
