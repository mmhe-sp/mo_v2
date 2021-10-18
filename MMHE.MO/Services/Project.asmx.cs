using MMHE.MO.Models;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
    public class Project : BaseWebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void FindProjects(string project)
        {
            var serializer = new JavaScriptSerializer();


            JavaScriptSerializer js = new JavaScriptSerializer();
            var projects = new List<string> { "1234-Project1", "1234-Project1", "1234-Project1", "1234-Project1", "1234-Project1" };
            Context.Response.Write(js.Serialize(projects));
        }
    }
}
