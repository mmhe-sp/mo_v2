using Microsoft.Reporting.WebForms;
using MMHE.MO.Business;
using MMHE.MO.Business.Repositories;
using MMHE.MO.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls.Reports
{
    public partial class VO : UserControl
    {
        public string JCSId { get; set; }
        public string JCSType { get; set; }
        public string connStr = ConnectionStringHelper.MO;

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;
            JCSId = Request.QueryString["id"];
            JCSType = Request.QueryString["type"];
            new VORepository().Print(JCSId, user.ProjectId, user.Id);

            this.AWOVOReport.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            BindReport();
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string jcsid = JCSId;            
            
            DataSet WDRSReportDetailsAll = GetWDRSReportDetailsMain(jcsid);
            DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

            DataSet WDRSReportDetailsAll2 = GetWDRSReportDetailsMainScope(jcsid);
            DataTable dtReportDetailsAll2 = WDRSReportDetailsAll2.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll2));

        }

        private void BindReport()
        {

            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            AWOVOReport.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

            AWOVOReport.ProcessingMode = ProcessingMode.Local;

            DataSet WDRSReportDetailsAll = GetWDRSReportDetailsMain(JCSId);
            DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
            AWOVOReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

            AWOVOReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/AWOVOReport.rdlc";

            AWOVOReport.LocalReport.EnableHyperlinks = true;
            AWOVOReport.LocalReport.Refresh();
        }

        private DataSet GetWDRSReportDetailsMain(string jcsid)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JCSID", jcsid);
                command.CommandText = "Sp_FetchAWOVOReport";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        
        private DataSet GetWDRSReportDetailsMainScope(string jcsid)
        {
            DataSet ds = new DataSet();
            try
            {
                             
                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;                
                command.Parameters.AddWithValue("@JCSID", jcsid);
                command.CommandText = "Sp_FetchAWOVOScope";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

       

    }
}
