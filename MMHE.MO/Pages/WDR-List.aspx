<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="WDR" Src="~/_controltemplates/15/MMHE.MO/WDR/WDR-List.ascx" %>
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
                            <li class="breadcrumb-item active">Work Done Submission</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:WDR runat="server" ID="wdr"></mo:WDR>
    </div>
    <!-- container-fluid -->
    <div id="stacked-column-chart" style="display:none"></div>
    <div id="radialBar-chart" style="display:none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Work Done Submission
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Work Done Submission
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        var __key = 'wdr';
        $(document).ready(function ()
        {

            $('#wdrTable thead tr').clone(true).appendTo('#wdrTable thead');
            $('#wdrTable thead tr:eq(1) th').each(function (i)
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
            var table = $('#wdrTable').DataTable({
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
            $('#wdrTable tbody').on('click', 'tr.group', function ()
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
            delaySearch("#wdrTable", __key);
			getStatusBackground();
        });

        //function showUploadModal()
        //{
		//	$('#jcsModal').modal('show');
        //}
        //function download()
        //{
		//	var response = $.ajax({
		//		method: "GET",
		//		url: "jsl.asmx/Export",
		//		dataType: "json",
		//	}).done(function (d, status, headers)
        //    {
        //        var bytes = atob(d.Content);
		//		saveAs(new Blob([bytes],
		//			{
		//			    type: 'application/vnd.ms-excel'
		//			}), d.FileName);
		//	});
        //}

        //function uploadExcel()
        //{
		//	var fileUpload = $("#jcsFile").get(0);
		//	var files = fileUpload.files;

		//	// Create  a FormData object
		//	var fileData = new FormData();

		//	// if there are multiple files , loop through each files
		//	for (var i = 0; i < files.length; i++)
		//	{
		//		fileData.append(files[i].name, files[i]);
		//	}

		//	$.ajax({
		//		url: 'BulkUpload.asmx/JSL', //URL to upload files 
		//		type: "POST", //as we will be posting files and other method POST is used
		//		processData: false, //remember to set processData and ContentType to false, otherwise you may get an error
		//		contentType: false,
		//		data: fileData
		//	}).done(function ()
		//	{
		//	    window.location.reload();
		//	});
        //}
		function getStatusBackground()
		{
			var statuses = $('.jsl-status');
			$.each(statuses, function (i, v)
			{
				var s = $(v);
                var bgColor = 'inherit';
                switch (s.text().toLocaleLowerCase())
				{
					case 'approved':
						bgColor = 'magento';
						break;
					case 'pending':
						bgColor = 'orange';
						break;
					case 'final approved':
						bgColor = 'green';
						break;
				}
				s.css('background-color', bgColor);
			});
		}
	</script>
</asp:Content>
