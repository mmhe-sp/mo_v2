<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="DWC" Src="~/_controltemplates/15/MMHE.MO/DWC.ascx" %>
<%@ Register TagPrefix="mo" TagName="ProjectName" Src="~/_controltemplates/15/MMHE.MO/ProjectName.ascx" %>

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
                            <li class="breadcrumb-item active">Daily Work Progress</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:DWC runat="server" id="jcs"></mo:DWC>
    </div>
    <!-- container-fluid -->
    <div id="stacked-column-chart" style="display: none"></div>
    <div id="radialBar-chart" style="display: none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Daily Work Progress
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Daily Work Progress
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        var __key = 'dwc';
        $(document).ready(function ()
        {

            //$('#jcsTable thead tr').clone(true).appendTo('#jcsTable thead');
            //$('#jcsTable thead tr:eq(1) th').each(function (i)
            //{
            //    var title = $(this).text();
            //    if (title != "")
            //    {
            //        $(this).html('<input type="text" placeholder="Search" class="form-control form-control-sm" />');

            //        $('input', this).on('keyup change', function ()
            //        {
            //            if (table.column(i).search() !== this.value)
            //            {
            //                table
            //                    .column(i)
            //                    .search(this.value)
            //                    .draw();
            //            }
            //        });
            //    }
            //});

            var groupColumn = 0;
            var value = '';
            if (!value)
                value = '';
            var table = $('#jcsTable').DataTable({
                search: { search: value },
                ordering: false,
                order: [[groupColumn, 'asc']],
                autoWidth:false,
                displayLength: 25,
                "aLengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
                "iDisplayLength": 10,
                "pageLength": -1,
                "dom": '<"pull-left"f><"pull-right"l>tip',
                scrollY: "300px",
                scrollX: true,
            });


            delaySearch("#jcsTable", __key);

        });

        function saveProgress()
        {

            var vo = extractModel();
            $.ajax({
                url: "dwc.asmx/SaveProgress",
                data: JSON.stringify({ dwc: vo }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                d = d.d;
                showMessage('The progress have been saved successfully.', 'success', function () { window.location.reload(true); });
            }).fail(function ()
            {
                showMessage('Unable to save the progress.', 'error', reloadGrid);
            });
        }
        function extractModel()
        {
        }
    </script>
</asp:Content>
