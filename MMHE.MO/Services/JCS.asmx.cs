
using MMHE.MO.Business.Repositories;

using System;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MMHE.MO.Services
{
	public class JCS : WebService
	{
		[WebMethod]
		[ScriptMethod]
		public void Export()
		{
			DataTable dataTable = new JCSRepository().GetAll("1.21M0025", "264636");

			string filename = "JCS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
			System.IO.StringWriter tw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
			DataGrid dgGrid = new DataGrid();
			dgGrid.DataSource = dataTable;
			dgGrid.DataBind();

			//var xLWorkbook = new XLWorkbook();
		}
	}
}
