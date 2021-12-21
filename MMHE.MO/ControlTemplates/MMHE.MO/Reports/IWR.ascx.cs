using Microsoft.Reporting.WebForms;
using MMHE.MO.Business;
using MMHE.MO.Business.Repositories;
using MMHE.MO.Helpers;
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
    public partial class IWR : UserControl
    {
        public string JCSId { get; set; }
        public string connStr = ConnectionStringHelper.MO;

        protected void Page_Load(object sender, EventArgs e)
        {

            JCSId = Request.QueryString["id"];
            BindReport();
            //var user = (Page as BasePage).LoggedInUser;            
            //new VORepository().Print(JCSId, user.ProjectId, user.Id);
        }

        private void BindReport()
        {

            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            IWRReport.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

            IWRReport.ProcessingMode = ProcessingMode.Local;

            DataSet WDRSReportDetailsAll = GetIWRReporstDetailsAll(JCSId);
            DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
            IWRReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Header", dtReportDetailsAll));
            //IWRReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

            DataSet WDRSReportDetailsAll2 = GetWDRSReportDetailsMain(JCSId);
            DataTable dtReportDetailsAll2 = WDRSReportDetailsAll2.Tables[0];
            IWRReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll2));

            IWRReport.LocalReport.ReportPath = AppSettingsHelper.ReportPath + "IWRReport.rdlc";

            IWRReport.LocalReport.EnableHyperlinks = true;
            IWRReport.LocalReport.Refresh();
        }

        private DataSet GetIWRReporstDetailsAll(string jcsid)
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

                command.CommandText = "[Sp_FetchIWRMainReport]";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
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
                command.CommandText = "Sp_FetchIWRSubReport";
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
