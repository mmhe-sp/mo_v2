using Microsoft.Reporting.WebForms;
using MMHE.MO.Business;
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
        string connStr = ConnectionStringHelper.MO;
        string txtEndDate = DateTime.Now.ToString("dd/MM/yyyy");
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WDRSReport.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            BindReport();
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            DataSet dsSubDetails = null;
            // DataTable dtEmployeeDetails =
            string ownerno = e.Parameters[0].Values[0].ToString();
            string jcsid = e.Parameters[1].Values[0].ToString();
            string dc = e.Parameters[2].Values[0].ToString();
            string date = string.Empty;
            if (e.Parameters[3].Values[0] != null)
                date = e.Parameters[3].Values[0].ToString();
            else
                date = "";
            string vendor = e.Parameters[4].Values[0].ToString();
            //Common objCommon = new Common();
            string ProjectID = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                ProjectID = Request.QueryString["ProjectID"].ToString();
            }
            DataSet WDRSReportDetailsHeader = GetWDRSReportDetailsHeader(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"], ProjectID);
            DataTable dtReportDetailsHeader = WDRSReportDetailsHeader.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Header", dtReportDetailsHeader));
            // Approvals
            DataSet WDRSReportDetailsApprovals = GetWDRSReportDetailsApprovals(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"], ProjectID);
            DataTable dtReportDetailsApprovals = WDRSReportDetailsApprovals.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Approvals", dtReportDetailsApprovals));


            DataSet WDRSReportDetailsAll = GetWDRSReportDetailsMain(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"]);
            DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

            DataSet dsIWRMainDetailsAll = IWRMainDetailsAll(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"]);
            DataTable dtReportIWRMainDetailsAll = dsIWRMainDetailsAll.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("IWRMAIN", dtReportIWRMainDetailsAll));

            DataSet dsIWRAdditionalDetailsAll = IWRAdditionalDetailsAll(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"]);
            DataTable dtReportIWRAdditionalDetailsAll = dsIWRAdditionalDetailsAll.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("IWRAdditional", dtReportIWRAdditionalDetailsAll));

            DataSet WDRSReportDetailsAllAdditional = GetWDRSReportDetailsMainAdditional(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"]);
            DataTable dtReportDetailsAllAdditional = WDRSReportDetailsAllAdditional.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AdditionalScope", dtReportDetailsAllAdditional));


            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dtReportDetailsAll));
            //   WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dtReportDetailsAll));
            DataSet WDRSReportAdditionalworks = GetWDRSReportAdditionalworks(jcsid, "Additionalworks");
            DataTable dtReportAdditionalworks = WDRSReportAdditionalworks.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dtReportAdditionalworks));
            DataSet WDRSReportMaterialSuppliedbyYard = GetWDRSReportAdditionalworks(jcsid, "MaterialSuppliedbyYard");
            DataTable dtReportMaterialSuppliedbyYard = WDRSReportMaterialSuppliedbyYard.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet3", dtReportMaterialSuppliedbyYard));

            DataSet WDRSReportAccessories = GetWDRSReportAdditionalworks(jcsid, "Accessories");
            DataTable dtReportAccessories = WDRSReportAccessories.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4", dtReportAccessories));

        }

        private void BindReport()
        {
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            WDRSReport.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            if (Request.QueryString["type"] == "Subcon")
            {
                if (Request.QueryString["CANCELLED"] == "CANCELLED")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllCancelled.rdlc";
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllShipStaff.rdlc";
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllOwnerArrange.rdlc";
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;


                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/Report.rdlc";
                }

            }
            else
            {
                if (Request.QueryString["CANCELLED"] == "CANCELLED")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllCancelled.rdlc";
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllShipStaff.rdlc";
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/WCRReportClientAllOwnerArrange.rdlc";
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;

                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));
                    DataSet WDRSReportDetailsApproval = GetWCRSReportApprovals(Request.QueryString["jcsid"]);
                    DataTable dtReportDetailsApproval = WDRSReportDetailsApproval.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Approval", dtReportDetailsApproval));

                    WDRSReport.LocalReport.ReportPath = "E:/Simbiotik/Sumeet/repo/mo_v2/MMHE.MO/Layouts/MMHE.MO/ReportClientWCR.rdlc";
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

        private DataSet GetWDRSReportDetailsHeader(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType, string ProjectID)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    ////command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    //command.Parameters.AddWithValue("@JCSID", jcsid);
                    ////command.Parameters.AddWithValue("@DC", dccode);
                    ////command.Parameters.AddWithValue("@Date", adate);
                    //command.Parameters.AddWithValue("@Vendor", vendor);
                    ////command.Parameters.AddWithValue("@RType", reprotType);

                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRReport_Header_BySubcon";
                }
                else
                    command.CommandText = "Sp_FetchWCRReport_Header";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        private DataSet GetWDRSReportDetailsApprovals(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType, string ProjectID)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    // command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    //command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dc);
                    //command.Parameters.AddWithValue("@Date", adate);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRReport_Approval";
                }
                else
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

        private DataSet GetWDRSReportDetailsMain(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dccode);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSAllDetailsByVendor2";
                }
                else
                    command.CommandText = "Sp_FetchWDRSClientMainScope";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet IWRMainDetailsAll(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dccode);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSAllDetailsByVendor2";
                }
                else
                    command.CommandText = "Sp_FetchWDRSIWRMainDetailsAll";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet IWRAdditionalDetailsAll(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dccode);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSAllDetailsByVendor2";
                }
                else
                    command.CommandText = "Sp_FetchWDRSIWRAdditionalDetailsAll";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet GetWDRSReportDetailsMainAdditional(string ownerno, string jcsid, string dc, string date, string vendor, string reprotType)
        {
            DataSet ds = new DataSet();
            try
            {

                string cdate = date.Split(' ')[0];
                string day = string.Empty;
                string month = string.Empty;
                string dccode = string.Empty;
                switch (dc)
                {
                    case "Blasting, Painting & Cleaning":
                        dccode = "01";
                        break;
                    case "Cargo Containment System (CCS)":
                        dccode = "02";
                        break;
                    case "Electrical":
                        dccode = "03";
                        break;
                    case "Hull":
                        dccode = "04";
                        break;
                    case "Machinery":
                        dccode = "05";
                        break;
                    case "Piping":
                        dccode = "06";
                        break;
                    case "Piping & Valve":
                        dccode = "06";
                        break;
                    case "General Services":
                        dccode = "07";
                        break;
                    case "Scaffolding":
                        dccode = "08";
                        break;
                    default: break;
                }
                string adate = string.Empty;
                if (cdate.Contains("/"))
                {
                    if (Convert.ToInt32(cdate.Split('/')[0]) < 10)
                        month = "0" + cdate.Split('/')[0];
                    else
                        month = cdate.Split('/')[0];


                    if (Convert.ToInt32(cdate.Split('/')[1]) < 10)
                        day = "0" + cdate.Split('/')[1];
                    else
                        day = cdate.Split('/')[1];
                    adate = cdate.Split('/')[2] + "-" + month + "-" + day;
                }

                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();

                connection = new SqlConnection(connStr);
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                if (reprotType == "Subcon")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dccode);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSAllDetailsByVendor2";
                }
                else
                    command.CommandText = "Sp_FetchWDRSClientAdditionalScope";
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