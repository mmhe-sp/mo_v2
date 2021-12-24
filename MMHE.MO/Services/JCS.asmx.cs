
using ClosedXML.Excel;
using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
    [WebService]
    [ScriptService]
    public class JCS : BaseWebService
    {
        



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Save(JCSDetails jcs)
        {
            new JCSRepository().Save(jcs, 0, LoggedInUser.Id);
            return new { Succeeded = true };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Submit(JCSDetails jcs)
        {
            new JCSRepository().Save(jcs, 1, LoggedInUser.Id);
            return new { Succeeded = true };
        }
    }
}
