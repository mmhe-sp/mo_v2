<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="MMHE.MO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>
<%@ Register TagPrefix="mo" TagName="ProjectName" Src="~/_controltemplates/15/MMHE.MO/ProjectName.ascx" %>
<%@ Register TagPrefix="da" TagName="dashboard" Src="~/_controltemplates/15/MMHE.MO/Dashboard/Dashboard.ascx" %>

<%@ Page Language="C#" MasterPageFile="../_catalogs/masterpage/MO.master" Inherits="MMHE.MO.UI.BasePage,MMHE.MO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container-fluid">
        <!-- start page title -->
        <div class="row">
            <div class="col-12">
                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                    <mo:ProjectName runat="server" id="projectName"></mo:ProjectName>
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="javascript: void(0);">Marine Operation</a></li>
                            <li class="breadcrumb-item active">Dashboard</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <!-- end page title -->

        <div class="row">
            <da:dashboard runat="server" id="wdri"></da:dashboard>
        </div>
        <!-- end row -->

        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col text-center">
                                <div class="avatar-sm mx-auto mb-3 mt-1">
                                    <span class="avatar-title rounded-circle bg-primary bg-soft text-primary font-size-16">
                                        <i class="mdi mdi-format-list-checks ms-1"></i>
                                    </span>
                                </div>
                                <h6><small><a href="">Daily Work Progress Log</a></small></h6>
                            </div>

                            <div class="col text-center">
                                <div class="avatar-sm mx-auto mb-3 mt-1">
                                    <span class="avatar-title rounded-circle bg-success bg-soft text-primary font-size-16">
                                        <i class="mdi mdi-check-box-multiple-outline ms-1"></i>
                                    </span>
                                </div>
                                <h6><small><a href="">Work Completion Report</a></small></h6>
                            </div>
                            <div class="col text-center">
                                <div class="avatar-sm mx-auto mb-3 mt-1">
                                    <span class="avatar-title rounded-circle bg-success bg-soft text-primary font-size-16">
                                        <i class="mdi mdi-notebook-check ms-1"></i>
                                    </span>
                                </div>
                                <h6><small><a href="">Work Done Report</a></small></h6>
                            </div>
                            <div class="col text-center">
                                <div class="avatar-sm mx-auto mb-3 mt-1">
                                    <span class="avatar-title rounded-circle bg-primary bg-soft text-primary font-size-16">
                                        <i class="mdi mdi-chart-areaspline ms-1"></i>
                                    </span>
                                </div>
                                <h6><small><a href="">Work Completion Summary</a></small></h6>
                            </div>

                            <div class="col text-center">
                                <div class="avatar-sm mx-auto mb-3 mt-1">
                                    <span class="avatar-title rounded-circle bg-primary bg-soft text-primary font-size-16">
                                        <i class="mdi mdi-note-text-outline ms-1"></i>
                                    </span>
                                </div>
                                <h6><small><a href="">AWO/VO Summary</a></small></h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!-- container-fluid -->

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Dashboard
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="contentScript" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function ()
        {
            var options = {
                chart: {
                    height: 305,
                    type: "bar",
                    stacked: 0,
                    toolbar: {
                        show: !1
                    },
                    zoom: {
                        enabled: !0
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: "25%",
                        endingShape: "rounded"
                    }
                },
                dataLabels: {
                    enabled: 1,
                    position: 'top'
                },
                series: data,
                xaxis: {
                    categories: ["Original", "Variation", "Additional"]
                },
                colors: ["#556ee6", "#f1b44c", "#34c38f"],
                legend: {
                    position: "bottom"
                },
                fill: {
                    opacity: 1
                }
            },
         chart = new ApexCharts(document.querySelector("#stacked-column-chart-jcs"), options);
            chart.render();
        });
    </script>
</asp:Content>
