using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls.Reports
{
    public partial class WCR : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllCancelled.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllShipStaff.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllOwnerArrange.rdlc");
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;


                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/Report.rdlc");
                }

            }
            else
            {
                if (Request.QueryString["CANCELLED"] == "CANCELLED")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllCancelled.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "ShipStaff")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllShipStaff.rdlc");
                }
                else if (Request.QueryString["CANCELLED"] == "OwnerArrange")
                {
                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAllCancelled(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MainScope", dtReportDetailsAll));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/WCRReportClientAllOwnerArrange.rdlc");
                }
                else
                {
                    WDRSReport.ProcessingMode = ProcessingMode.Local;

                    DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
                    DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));
                    DataSet WDRSReportDetailsApproval = GetWCRSReportApprovals(Request.QueryString["jcsid"]);
                    DataTable dtReportDetailsApproval = WDRSReportDetailsApproval.Tables[0];
                    WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Approval", dtReportDetailsApproval));

                    WDRSReport.LocalReport.ReportPath = Server.MapPath("~/_LAYOUTS/15/MMHE.Work_MO/ReportClientWCR.rdlc");
                }
            }
            // WDRSReport.ProcessingMode = ProcessingMode.Local;

            // Common objCommon = new Common();
            // DataSet WDRSReportDetailsAll = GetWDRSReportDetailsAll(Request.QueryString["refno"], Request.QueryString["jcsid"], Request.QueryString["dc"], txtEndDate.Text, Request.QueryString["type"]);
            // DataTable dtReportDetailsAll = WDRSReportDetailsAll.Tables[0];
            // WDRSReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("ReportDataSet", dtReportDetailsAll));
            if (Request.QueryString["type"] == "Subcon")
            {

            }
            else
            {
                //DataSet WDRSReportAdditionalworks = GetWDRSReportAdditionalworks(Request.QueryString["refno"], "Additionalworks");
                //DataTable dtReportAdditionalworks = WDRSReportAdditionalworks.Tables[0];

                //DataSet WDRSReportMaterialSuppliedbyYard = GetWDRSReportAdditionalworks(Request.QueryString["refno"], "MaterialSuppliedbyYard");
                //DataTable dtReportMaterialSuppliedbyYard = WDRSReportMaterialSuppliedbyYard.Tables[0];

                //DataSet WDRSReportAccessories = GetWDRSReportAdditionalworks(Request.QueryString["refno"], "Accessories");
                //DataTable dtReportAccessories = WDRSReportAccessories.Tables[0];
            }
            WDRSReport.LocalReport.EnableHyperlinks = true;
            WDRSReport.LocalReport.Refresh();
        }
    }
}
