
using ClosedXML.Excel;

using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
	public class JCS : BaseWebService
	{
		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
		public void Export()
		{
			DataTable dataTable = new JCSRepository().GetAll(LoggedInUser.ProjectId, LoggedInUser.Id);
			dataTable.TableName = "JCS";
			byte[] excelContent;
			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dataTable);
				wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;
				wb.Style.Font.Bold = false;
				using (MemoryStream stream = new MemoryStream())
				{
					wb.SaveAs(stream);
					excelContent = stream.ToArray();
				}
			}

			JavaScriptSerializer js = new JavaScriptSerializer();

			Context.Response.Write(js.Serialize(new { Content = excelContent, FileName = "JCS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls" }));
		}


		[WebMethod]
		public void Upload()
		{
			HttpFileCollection files = HttpContext.Current.Request.Files;
			var file = files[0];
			var ext = Path.GetExtension(file.FileName);

			//save file temporariely
			string tempFolder = "~/_CONTROLTEMPLATES/15/MMHE.Work_MO/Temp/";
			string path = Server.MapPath(tempFolder + Path.GetFileName(file.FileName));
			file.SaveAs(path);

			string _connString = "";
			if (ext == ".xls")
			{   //For Excel 97-03
				_connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
			}
			else if (ext == ".xlsx")
			{    //For Excel 07 and greater
				_connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
			}
			_connString = String.Format(_connString, path, "Yes");
			using (OleDbConnection _Oldbconn = new OleDbConnection(_connString))
			using (OleDbCommand _oldbcmd = new OleDbCommand())
			{
				OleDbDataAdapter _oldda = new OleDbDataAdapter();
				DataTable _dt = new DataTable();
				_oldbcmd.Connection = _Oldbconn;

				_Oldbconn.Open();
				DataTable _dtSchema;
				_dtSchema = _Oldbconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				string _xcelSName = _dtSchema.Rows[0]["TABLE_NAME"].ToString();
				_oldbcmd.CommandText = "SELECT * From [" + _xcelSName + "]";
				_oldda.SelectCommand = _oldbcmd;
				_oldda.Fill(_dt);
				_Oldbconn.Close();
			}
		}

		public void Save(string jcs)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			var jcsDetails = javaScriptSerializer.Deserialize<JCSDetails>(jcs);
		}

		private void Import(DataTable dataTable)
		{
			//Create a new DataTable.
			DataTable dt = new DataTable();
			dt.Columns.Add("OwnerNo", typeof(string));
			dt.Columns.Add("JSL", typeof(string));
			dt.Columns.Add("Discipline", typeof(string));
			dt.Columns.Add("Description", typeof(string));
			bool addNewRow = false;
			string discipline = string.Empty;
			string owner = string.Empty;
			string jsl = string.Empty;
			string value = string.Empty;
			string cell;
			bool findJSL = true;
			string contractItems = string.Empty;
			foreach (DataRow row in dataTable.Rows)
			{
				cell = row[0].ToString();
				if (string.IsNullOrWhiteSpace(cell) == false && cell == "DISC")
				{

					discipline = row[1].ToString().Split('.')[0];
					Console.WriteLine(discipline);
					findJSL = true;
					continue;
				}

				cell = row[0].ToString();

				if (IsRowEmpty(row) && findJSL)
					continue;
				else if (string.IsNullOrWhiteSpace(cell) && findJSL)
					continue;
				else
					findJSL = false;
				if (string.IsNullOrWhiteSpace(cell) == false)
				{
					if (addNewRow)
						AddRow(dt, owner, contractItems, discipline, jsl);
					owner = cell;
					jsl = row[1].ToString();
					contractItems = string.Empty;
					addNewRow = true;
					continue;
				}
				else if (IsRowEmpty(row) == false)
				{
					if (string.IsNullOrWhiteSpace(contractItems) == false)
						contractItems += Environment.NewLine;
					contractItems += row[1].ToString();
					contractItems += " " + row[2].ToString();
					contractItems += " " + row[3].ToString();
					contractItems += " " + row[4].ToString();
					contractItems += " " + row[5].ToString();
				}
			}

			//Add last Row.
			if (addNewRow)
				AddRow(dt, owner, contractItems, discipline, jsl);

			//save data into database
			var jcsRepository = new JCSRepository();
			foreach (DataRow dataRow in dt.Rows)
				jcsRepository.Import(LoggedInUser.ProjectId, LoggedInUser.Id, dataRow);

		}

		private void AddRow(DataTable dt, string owner, string contractItems, string discipline, string jsl)
		{
			DataRow dataRow = dt.NewRow();
			dataRow[0] = owner;
			dataRow[1] = jsl;
			dataRow[2] = discipline;
			dataRow[3] = contractItems;
			dt.Rows.Add(dataRow);
		}
		private bool IsRowEmpty(DataRow row)
		{
			return string.IsNullOrWhiteSpace(row[0].ToString())
				&& string.IsNullOrWhiteSpace(row[1].ToString())
				&& string.IsNullOrWhiteSpace(row[2].ToString())
				&& string.IsNullOrWhiteSpace(row[3].ToString())
				&& string.IsNullOrWhiteSpace(row[4].ToString())
				&& string.IsNullOrWhiteSpace(row[5].ToString())
				&& string.IsNullOrWhiteSpace(row[6].ToString())
				&& string.IsNullOrWhiteSpace(row[7].ToString())
				&& string.IsNullOrWhiteSpace(row[8].ToString());
		}
	}
}
