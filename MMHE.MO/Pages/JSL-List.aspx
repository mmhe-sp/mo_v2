<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="JSL" Src="~/_controltemplates/15/MMHE.MO/JSL/List.ascx" %>
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
                            <li class="breadcrumb-item active">Job Summary List</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:JSL runat="server" ID="jcs"></mo:JSL>
    </div>
    <!-- container-fluid -->
    <div id="stacked-column-chart" style="display:none"></div>
    <div id="radialBar-chart" style="display:none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Job Summary List
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Job Summary List
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        var __key = 'jsl';
        $(document).ready(function ()
        {

            $('#jcsTable thead tr').clone(true).appendTo('#jcsTable thead');
            $('#jcsTable thead tr:eq(1) th').each(function (i)
            {
                var title = $(this).text();
                if (title != "")
                {
                    $(this).html('<input type="text" placeholder="Search" class="form-control form-control-sm" />');

                    $('input', this).on('keyup change', function ()
                    {
                        if (table.column(i).search() !== this.value)
                        {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }
            });

            var groupColumn = 0;
            var value = sessionStorage.getItem(__key);
            if (!value)
                value = '';
            var table = $('#jcsTable').DataTable({
                search: { search: value },
                ordering:  false,
                order: [[groupColumn, 'asc']],
                displayLength: 25,
                "aLengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
                "iDisplayLength": 10,
                "pageLength": -1,
                "dom": '<"pull-left"f><"pull-right"l>tip',
                scrollY: "300px",
                scrollX: true,
            });

            // Order by the grouping
            $('#jcsTable tbody').on('click', 'tr.group', function ()
            {
                var currentOrder = table.order()[0];
                if (currentOrder[0] === groupColumn && currentOrder[1] === 'asc')
                {
                    table.order([groupColumn, 'desc']).draw();
                }
                else
                {
                    table.order([groupColumn, 'asc']).draw();
                }
            });
            delaySearch("#jcsTable", __key);
            getJSLStatusBackground();
        });

        function showUploadModal()
        {
			$('#jcsModal').modal('show');
        }
        function download()
        {
            $('#export-excel').attr('action', "BulkUpload.asmx/ExportJSL").submit();
			//var response = $.ajax({
			//	method: "GET",
			//	url: "jsl.asmx/Export",
			//	dataType: "json",
			//}).done(function (d, status, headers)
            //{
            //    var bytes = atob(d.Content);
			//	saveAs(new Blob([bytes],
			//		{
			//		    type: 'application/vnd.ms-excel'
			//		}), d.FileName);
			//});
        }

        function uploadExcel()
        {
			var fileUpload = $("#jcsFile").get(0);
			var files = fileUpload.files;

			// Create  a FormData object
			var fileData = new FormData();

			// if there are multiple files , loop through each files
			for (var i = 0; i < files.length; i++)
			{
				fileData.append(files[i].name, files[i]);
			}

			$.ajax({
				url: 'BulkUpload.asmx/JSL', //URL to upload files 
				type: "POST", //as we will be posting files and other method POST is used
				processData: false, //remember to set processData and ContentType to false, otherwise you may get an error
				contentType: false,
				data: fileData
			}).done(function ()
			{
			    window.location.reload();
			});
        }
        function editJCS()
        {
            $('#jcsEditModal').modal('show');
        }

        $(document).ready(function ()
        {
            $("#jcsTable tbody tr td div span[id*='statusBadge']").each(function ()
            {
                var $srow = $(this).closest('tr')[0];
                var innerHTMLString = "";
                $.each($srow.cells, function (index, Value)
                {
                    if (Value.innerHTML.indexOf('fontcolor+') > -1)
                    {
                        innerHTMLString = $srow.cells[index].innerHTML;
                    }
                });

                var element = jQuery.parseHTML(innerHTMLString);
                var Id = element[1].id;

                var fontColor = element[1].innerText;
                var backgroundColor = element[3].innerText;
                Id = Id.split('+')[1];

                if (backgroundColor != "" && backgroundColor != null && backgroundColor != undefined && fontColor != null && fontColor != "" && fontColor != undefined)
                {
                    if (fontColor.toUpperCase() == backgroundColor.toUpperCase())
                    {

                        //$("#jcsTable tbody tr td div span[id = 'statusBadge+" + Id + "']").removeAttr('style');
                        //$("#statusBadge+" + Id).removeAttr('style')
                        if (fontColor.toUpperCase() == "WHITE")
                        {
                            $("#jcsTable tbody tr td div span[id = 'statusBadge+" + Id + "']").css({ 'color': 'black' });
                        }
                        else
                        {
                            $("#jcsTable tbody tr td div span[id = 'statusBadge+" + Id + "']").css({ 'color': 'white' });
                        }

                    }
                }
            })
        });
	</script>
</asp:Content>
