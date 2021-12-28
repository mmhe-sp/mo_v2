using ClosedXML.Excel;
using MMHE.MO.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace MMHE.MO.Services
{
    public class BulkUpload : BaseWebService
    {

        [WebMethod]
        public void JCS()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            var file = files[0];
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                ImportJCS(workSheet.Rows().Skip(2));
            }
        }

        [WebMethod]
        public void ExportJCS()
        {
            DataTable dataTable = new JCSRepository().GetAll(LoggedInUser.ProjectId, LoggedInUser.Id);
            Export("JCS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx", "JCS", dataTable);

        }

        [WebMethod]
        public void ExportJSL()
        {
            DataTable dataTable = new JSLRepository().ExportJSL(LoggedInUser.ProjectId);
            Export("JSL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx","JSL",dataTable);
        }

        [WebMethod]
        public void ExportDWC()
        {
            DataTable dataTable = new DWCRepository().Export(LoggedInUser.ProjectId, LoggedInUser.Id);
            Export("DWC_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx", "DWC", dataTable);
        }

        private void Export(string fileName, string tableName, DataTable dataTable)
        {
            // Prepare the response
            HttpResponse httpResponse = Context.Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + fileName + "\"");

           dataTable.TableName = tableName;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;
                wb.Style.Font.Bold = false;
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.WriteTo(httpResponse.OutputStream);
                    stream.Close();
                }
            }

            httpResponse.End();
        }

        [WebMethod]
        public bool JSL()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            var file = files[0];
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                ImportJSL(workSheet.Rows().Skip(1));
            }
            return true;
        }



        private void ImportJCS(IEnumerable<IXLRow> rows)
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
                if (cell.IsEmpty() == false && findJSL)
                {
                    continue;
                }

                if (IsJCSRowEmpty(row) && findJSL)
                    continue;
                else
                    findJSL = false;
                if (cell.IsEmpty() == false)
                {
                    if (addNewRow)
                        AddJCSRow(dt, owner, contractItems, discipline, jsl);
                    owner = cell.GetString();
                    jsl = row.Cell(2).GetString();
                    contractItems = string.Empty;
                    addNewRow = true;
                    continue;
                }
                else if (IsJCSRowEmpty(row) == false)
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
                AddJCSRow(dt, owner, contractItems, discipline, jsl);

            //save data into database
            var jcsRepository = new JCSRepository();
            foreach (DataRow dataRow in dt.Rows)
                jcsRepository.Import(LoggedInUser.ProjectId, LoggedInUser.Id, dataRow);

        }

        private void AddJCSRow(DataTable dt, string owner, string contractItems, string discipline, string jsl)
        {
            DataRow dataRow = dt.NewRow();
            dataRow[0] = owner;
            dataRow[1] = jsl;
            dataRow[2] = discipline;
            dataRow[3] = contractItems;
            dt.Rows.Add(dataRow);
        }

        private bool IsJCSRowEmpty(IXLRow row)
        {
            return
                row.Cell(1).IsEmpty()
                && row.Cell(2).IsEmpty()
                && row.Cell(3).IsEmpty()
                && row.Cell(4).IsEmpty()
                && row.Cell(5).IsEmpty()
                && row.Cell(6).IsEmpty()
                && row.Cell(7).IsEmpty()
                && row.Cell(8).IsEmpty();
        }


        private void ImportJSL(IEnumerable<IXLRow> rows)
        {
            Int32 iRow = 0;
            JSLRepository jslRepository = new JSLRepository();
            foreach (IXLRow _dr in rows)
            {
                string dtRcvPMT = _dr.Cell(11).GetString();
                string dtSubmitTo = _dr.Cell(12).GetString();
                string dtRcvClient = _dr.Cell(13).GetString();


                string dtWorkCompleted = _dr.Cell(15).GetString();
                string dtWCRPlan = "";
                string dtWCRAct = _dr.Cell(16).GetString();
                string dtWCRSign = _dr.Cell(17).GetString();


                if (_dr.Cell(3).GetString() != "")
                {
                    jslRepository.Import("XCELINS", "", LoggedInUser.ProjectId, _dr.Cell(1).GetString(), _dr.Cell(2).GetString(),
                    _dr.Cell(4).GetString(), _dr.Cell(5).GetString(), "",
                    _dr.Cell(6).GetString(), _dr.Cell(8).GetString(), _dr.Cell(7).GetString(),
                    _dr.Cell(9).GetString(), _dr.Cell(10).GetString(), dtRcvPMT,
                    dtSubmitTo.ToString(), dtRcvClient, _dr.Cell(14).GetString(), dtWorkCompleted,
                    dtWCRPlan.ToString(), dtWCRAct.ToString(),
                    dtWCRSign.ToString(), "", _dr.Cell(18).GetString(), LoggedInUser.Id, _dr.Cell(3).GetString());
                }


                iRow = iRow + 1;
            }
        }
    }
}
