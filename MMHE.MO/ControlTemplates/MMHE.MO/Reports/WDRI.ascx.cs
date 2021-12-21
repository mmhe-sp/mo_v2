using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.SharePoint;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.SharePoint.Utilities;
using System.Security;
using Microsoft.Reporting.WebForms;
using MMHE.MO.Business;
using MMHE.MO.Helpers;

namespace MMHE.MO.ControlTemplates.MMHE.MO.Reports
{
    public partial class WDRI : UserControl
    {
        string connStr = ConnectionStringHelper.MO;
        string txtEndDate = DateTime.Now.ToString("dd/MM/yyyy");

        protected void Page_Load(object sender, EventArgs e)
        {
            this.WDRSReport.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            BindWDRReport();
        }

        private void BindWDRReport()
        {
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            WDRSReport.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            if (Request.QueryString["type"] == "Subcon")
            {
                WDRSReport.LocalReport.ReportPath = AppSettingsHelper.ReportPath + "Report.rdlc";
            }
            else if (Request.QueryString["type"] == "WDRSub")
            {
                WDRSReport.LocalReport.ReportPath = AppSettingsHelper.ReportPath + "ReportSubconMain.rdlc";
            }
            else
            {
                WDRSReport.LocalReport.ReportPath = AppSettingsHelper.ReportPath + "ReportClient.rdlc";
            }
            WDRSReport.ProcessingMode = ProcessingMode.Local;

            //Common objCommon = new Common();

            DataSet WDRSReportDetailsAll;
            DataTable dtReportDetailsAll;

            if (Request.QueryString["type"] == "Subcon")
            {
                WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"], Request.QueryString["ProjectID"]);
                dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

            }
            else if (Request.QueryString["type"] == "WDRSub")
            {
                WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"], Request.QueryString["ProjectID"]);

                string _vName = Request.QueryString["VN"];
                if (Request.QueryString[null] != null)
                {
                    string _vExName = string.Empty;
                    if (Request.QueryString[null].Contains(','))
                    {
                        _vExName = Request.QueryString[null].Split(',')[1].ToString();
                    }
                    else
                    {
                        _vExName = Request.QueryString[null];
                    }
                    _vName = _vName + "&" + _vExName;
                }

                DataTable tblFiltered = WDRSReportDetailsAll.Tables[0];
                if (WDRSReportDetailsAll.Tables[0].Rows.Count > 0)
                {
                    DataTable tblFilteredFinal = WDRSReportDetailsAll.Tables[0].AsEnumerable()
                        .Where(row => row.Field<String>("Vendor").ToLower().Trim() == _vName.ToLower().Trim())
                        .OrderByDescending(row => row.Field<String>("Vendor"))
                        .CopyToDataTable();
                    dtReportDetailsAll = tblFilteredFinal;
                }
                else
                {
                    dtReportDetailsAll = tblFiltered; // WDRSReportDetailsAll.Tables[0];
                }

                WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

            }
            else
            {
                WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate, Request.QueryString["type"], Request.QueryString["ProjectID"]);
                dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));
            }
            WDRSReport.LocalReport.EnableHyperlinks = true;
            WDRSReport.LocalReport.Refresh();
        }

        private DataSet GetWDRSReportDetailsAll(string ownerno, string jcsid, string dc, string date, string reprotType, string ProjectID)
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

                if (reprotType == "Subcon")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@RType", reprotType);
                    command.CommandText = "[Sp_FetchWDRSAllDetailsMain_Vendor_Subcon]";
                }
                else if (reprotType == "WDRSub")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@RType", reprotType);
                    command.CommandText = "[Sp_FetchWDRSAllDetailsMain_Vendor_Subcon]";
                }
                else
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@RType", reprotType);
                    command.CommandText = "[Sp_FetchWDRSAllDetailsMain_Vendor]";
                }


                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }




        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            DataSet dsSubDetails = null;
            string vendor = "";
            // DataTable dtEmployeeDetails =
            string ownerno = e.Parameters[0].Values[0].ToString();
            string jcsid = e.Parameters[1].Values[0].ToString();
            string dc = e.Parameters[2].Values[0].ToString();
            string date = string.Empty;
            if (e.Parameters[3].Values[0] != null)
                date = e.Parameters[3].Values[0].ToString();
            else
                date = "";
            if (e.Parameters[4].Values[0] != null)
                vendor = e.Parameters[4].Values[0].ToString();
            //Common objCommon = new Common();
            //Header
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

            DataSet WDRSReportMaterialSuppliedbyYard = GetWDRSReportAdditionalworks(jcsid, "MaterialSuppliedbyYard", vendor);
            DataTable dtReportMaterialSuppliedbyYard = WDRSReportMaterialSuppliedbyYard.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet3", dtReportMaterialSuppliedbyYard));

            DataSet WDRSReportAccessories = GetWDRSReportAdditionalworks(jcsid, "Accessories", vendor);
            DataTable dtReportAccessories = WDRSReportAccessories.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4", dtReportAccessories));
            DataSet WDRSReportDetailsAllAdditional = GetWDRSReportDetailsMainAdditional(ownerno, jcsid, dc, date, vendor, Request.QueryString["type"]);
            DataTable dtReportDetailsAllAdditional = WDRSReportDetailsAllAdditional.Tables[0];
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("AdditionalScope", dtReportDetailsAllAdditional));
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
                if (reprotType == "Subcon" || reprotType == "WDRSub")
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
                    command.Parameters.AddWithValue("@Vendor", vendor);
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

                if (reprotType == "Subcon" || reprotType == "WDRSub")
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
                if (reprotType == "Subcon" || reprotType == "WDRSub")
                {
                    command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@ProjectID", ProjectID);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    command.Parameters.AddWithValue("@DC", dc);
                    command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon" || reprotType == "WDRSub")
                {
                    command.CommandText = "Sp_FetchWDRReport_Approval_BySubcon";
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
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else if (reprotType == "WDRSub")
                {
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }

                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSClientMainScopeBySubcon";
                }
                else if (reprotType == "WDRSub")
                {
                    command.CommandText = "Sp_FetchWDRSClientMainScopeBySubconByAll";
                }
                else
                {
                    command.CommandText = "Sp_FetchWDRSClientMainScope";
                }
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
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else if (reprotType == "WDRSub")
                {
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }

                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSIWRMainDetailsBySubcon";
                }
                else if (reprotType == "WDRSub")
                {
                    command.CommandText = "Sp_FetchWDRSIWRMainDetailsBySubconByAll";
                }
                else
                {
                    command.CommandText = "Sp_FetchWDRSIWRMainDetailsAll";
                }
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
                    // command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else if (reprotType == "WDRSub")
                {
                    // command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSIWRAdditionalDetailsBySubcon";
                }
                else if (reprotType == "WDRSub")
                {
                    command.CommandText = "Sp_FetchWDRSIWRAdditionalDetailsBySubconByAll";
                }
                else
                {
                    command.CommandText = "Sp_FetchWDRSIWRAdditionalDetailsAll";
                }
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private DataSet GetWDRSReportAdditionalworks(string JCSID, string Type, string Vendor)
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
                if (Request.QueryString["type"] == "WDRSub")
                {
                    command.Parameters.AddWithValue("@Vendor", Vendor);
                    command.CommandText = "[Sp_GetWDRSAdditionalWorksJCSIDByAll]";
                }
                else
                {
                    command.CommandText = "[Sp_GetWDRSAdditionalWorksJCSID]";
                }
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
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else if (reprotType == "WDRSub")
                {
                    //command.Parameters.AddWithValue("@OwnerNo", ownerno);
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                    //command.Parameters.AddWithValue("@DC", dccode);
                    //command.Parameters.AddWithValue("@Date", adate);
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    //command.Parameters.AddWithValue("@RType", reprotType);
                }
                else
                {
                    command.Parameters.AddWithValue("@JCSID", jcsid);
                }
                if (reprotType == "Subcon")
                {
                    command.CommandText = "Sp_FetchWDRSClientAdditionalScopeBySubcon";
                }
                else if (reprotType == "WDRSub")
                {
                    command.CommandText = "Sp_FetchWDRSClientAdditionalScopeBySubconByAll";
                }
                else
                {
                    command.CommandText = "Sp_FetchWDRSClientAdditionalScope";
                }
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
