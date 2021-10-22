
using ClosedXML.Excel;

using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Collections.Generic;
using System.Data;
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

            Context.Response.Write(js.Serialize(new { Content = Convert.ToBase64String(excelContent), FileName = "JCS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx" }));
        }

        [WebMethod]
        public void Upload()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            var file = files[0];
            using(XLWorkbook workBook = new XLWorkbook(file.InputStream))
			{
                IXLWorksheet workSheet = workBook.Worksheet(1);
                Import(workSheet.Rows().Skip(2));
            }
        }

        public void Save(string jcs)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var jcsDetails = javaScriptSerializer.Deserialize<JCSDetails>(jcs);
        }

        private void Import(IEnumerable<IXLRow> rows)
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
            IXLCell cell;
            bool findJSL = true;
            string contractItems = string.Empty;
            foreach (IXLRow row in rows)
            {
                cell = row.Cell(1);
                if (cell.IsEmpty() == false && cell.GetString() == "DISC")
                {

                    discipline = row.Cell(2).GetString().Split('.')[0];
                    Console.WriteLine(discipline);
                    findJSL = true;
                    continue;
                }

                if (IsRowEmpty(row) && findJSL)
                    continue;
                else if (cell.IsEmpty() && findJSL)
                    continue;
                else
                    findJSL = false;
                if (cell.IsEmpty() == false)
                {
                    if (addNewRow)
                        AddRow(dt, owner, contractItems, discipline, jsl);
                    owner = cell.GetString();
                    jsl = row.Cell(2).GetString();
                    contractItems = string.Empty;
                    addNewRow = true;
                    continue;
                }
                else if (IsRowEmpty(row) == false)
                {
                    if (string.IsNullOrWhiteSpace(contractItems) == false)
                        contractItems += Environment.NewLine;
                    contractItems += row.Cell(2).GetString();
                    contractItems += " " + row.Cell(3).GetString();
                    contractItems += " " + row.Cell(4).GetString();
                    contractItems += " " + row.Cell(5).GetString();
                    contractItems += " " + row.Cell(6).GetString();
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
        private bool IsRowEmpty(IXLRow row)
        {
            return
                row.Cell(1).IsEmpty()
                && row.Cell(2).IsEmpty()
                && row.Cell(3).IsEmpty()
                && row.Cell(4).IsEmpty()
                && row.Cell(5).IsEmpty()
                && row.Cell(6).IsEmpty()
                && row.Cell(7).IsEmpty()
                && row.Cell(8).IsEmpty()
                && row.Cell(1).IsEmpty();
        }
    }
}
