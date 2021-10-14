using MMHE.MO.Models;

using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
	public class Project : BaseWebService
	{
		[WebMethod]
		[ScriptMethod]
		public List<Option> FindProjects()
		{
			return new List<Option>() { new Option { Text = "A", Value = "B" } };
		}
	}
}
