<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="JCS" Src="~/_controltemplates/15/MMHE.MO/JCS/List.ascx" %>

<%@ Page Language="C#" MasterPageFile="../_catalogs/masterpage/MO.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container-fluid">
        <!-- start page title -->
        <div class="row">
            <div class="col-12">
                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                    <h4 class="mb-sm-0 font-size-18">1.21M0025 - Puteri Nilam Satu</h4>

                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="javascript: void(0);">Marine Operation</a></li>
                            <li class="breadcrumb-item active">Job Confirmation Scope</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:JCS runat="server" ID="jcs"></mo:JCS>
    </div>
    <!-- container-fluid -->

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Job Confirmation Scope
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Job Confirmation Scope
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        $(document).ready(function () {
            var groupColumn = 0;
            var value = '';
            if (!value)
                value = '';
            //$("#jcsTable").DataTable({ lengthChange: !1, buttons: ["excel"], search: false }).buttons().container().appendTo("#datatable-buttons_wrapper .col-md-6:eq(0)")
            //$('#jcsTable thead tr').clone(true).appendTo('#jcsTable thead');
            //$('#jcsTable thead tr:eq(1) th').each(function (i) {
            //    var title = $(this).text();
            //    if (title != "Action") {
            //        $(this).html('<input type="text" placeholder="Search ' + title + '" />');

            //        $('input', this).on('keyup change', function () {
            //            if (table.column(i).search() !== this.value) {
            //                table
            //                    .column(i)
            //                    .search(this.value)
            //                    .draw();
            //            }
            //        });
            //    }
            //});

            var table = $('#jcsTable').DataTable({
                search: { search: value },
                order: [[groupColumn, 'asc']],
                displayLength: 25,
                fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var count = 0;
                    api.column(groupColumn, { page: 'current' }).data().each(function (group, i) {
                        count++;
                        if (last !== group) {
                            $(rows).eq(i).before(
                                '<tr class="group"><td class="d-none"></td><td colspan="9">' + group + '</td></tr>'
                            );
                            last = group;
                            count = 0;
                        }
                    });
                },
                "aLengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
                "iDisplayLength": 10,
                "pageLength": -1,
                "dom": '<"pull-left"f><"pull-right"l>tip',
                scrollY: "300px",
                scrollX: true,
                // "lengthChange": false
            });
        });
    </script>
</asp:Content>
