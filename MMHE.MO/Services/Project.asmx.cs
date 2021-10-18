using MMHE.MO.Business.Repositories;

using System.Linq;
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
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(new ProjectsRepository().FindProjects(project).Select(p => p.Value + "-" + p.Text)));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void UpdateProject(string project)
        {
            string projectNumber = project.Split('-')[0];
            new ProjectsRepository().UpdateUserProject(projectNumber, LoggedInUser.Id);
            Context.Response.Write(new JavaScriptSerializer().Serialize(new { Ok = true }));
        }
    }
}
