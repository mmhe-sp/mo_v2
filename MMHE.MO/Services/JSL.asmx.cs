
using ClosedXML.Excel;

using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;

using System;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MMHE.MO.Services
{
    [WebService]
    [ScriptService]
    public class JSL : BaseWebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void Export()
        {
            DataTable dataTable = new JCSRepository().GetAll(LoggedInUser.ProjectId, LoggedInUser.Id);
            dataTable.TableName = "JSL";
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Save(JSLDetails jsl)
        {
            jsl.ProjectNo = LoggedInUser.ProjectId;
            //jsl.User = LoggedInUser.Id;
            new JSLRepository().Save(jsl);
            return new { Succeeded = true };
        }
    }
}
