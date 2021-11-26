using Microsoft.Reporting.WebForms;
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
    public partial class WCR : UserControl
    {
        string connStr = "server=MHB01VSQLP02; database=MMHE.MO; User ID=apps_user;Password=P@ssw0rd;";

        string endDate = "22/11/2021";
        string jcsid = "bf46c07d-bb5b-49f3-9ed3-0c9387ae7500";
        string dc = "01";
        string ownerno = "271.05";
        string reprotType = "InHouse";
        protected void Page_Load(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            WDRSReport.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            if (reprotType == "Subcon")
            {
                if (Request.QueryString["CANCELLED"] == "CANCELLED")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllCancelled.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllShipStaff.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllOwnerArrange.rdlc");
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;


                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/Report.rdlc");
                }

            }
            else
            {
                if (Request.QueryString["CANCELLED"] == "CANCELLED")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllCancelled.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllShipStaff.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllOwnerArrange.rdlc");
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;

                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(ownerno, jcsid, dc, endDate, reprotType);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));
                    DataSet WDRSReportDetailsApproval = GetWCRSReportApprovals(jcsid);
                    DataTable dtReportDetailsApproval = WDRSReportDetailsApproval.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Approval", dtReportDetailsApproval));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/ReportClientWCR.rdlc");
                }
            }
            WDRSReport.LocalReport.EnableHyperlinks = true;
            WDRSReport.LocalReport.Refresh();
        }

        private DataSet GetWDRSReportDetailsAllCancelled(string ownerno, string jcsid, string dc, string date, string reprotType)
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
                command.Parameters.AddWithValue("@OwnerNo", ownerno);
                command.Parameters.AddWithValue("@JCSID", jcsid);
                command.Parameters.AddWithValue("@DC", dc);


                //  command.CommandText = "[Sp_FetchWDRSAllDetails]";
                command.CommandText = "[Sp_GetWDRSReportDetailsAllCancelled]";

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet GetWDRSReportAdditionalworks(string JCSID, string Type)
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
                command.Parameters.AddWithValue("@JCSID", JCSID);
                command.Parameters.AddWithValue("@Type", Type);


                command.CommandText = "[Sp_GetWDRSAdditionalWorksJCSID]";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet GetWDRSReportDetailsAll(string ownerno, string jcsid, string dc, string date, string reprotType)
        {
            DataSet ds = new DataSet();
            try
            {
                string adate = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OwnerNo", ownerno);
                command.Parameters.AddWithValue("@JCSID", jcsid);
                command.Parameters.AddWithValue("@DC", dc);
                command.Parameters.AddWithValue("@Date", adate);
                command.Parameters.AddWithValue("@RType", reprotType);

                command.CommandText = "[Sp_FetchWDRSAllDetails]";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }


        private DataSet GetWCRSReportApprovals(string jcsid)
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
                command.CommandText = "Sp_FetchWDRReport_Approval";
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