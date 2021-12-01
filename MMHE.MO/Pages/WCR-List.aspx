<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="WCRList" Src="~/_controltemplates/15/MMHE.MO/WCR/WCR-List.ascx" %>
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
                            <li class="breadcrumb-item active">Work Completion Report - Client</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:WCRList runat="server" ID="jcs"></mo:WCRList>
    </div>
    <!-- container-fluid -->
    <div id="stacked-column-chart" style="display:none"></div>
    <div id="radialBar-chart" style="display:none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Work Completion Report - Client
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Work Completion Report - Client
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        var __key = 'wcr-list';
        $(document).ready(function ()
        {

            $('#wcrTable thead tr').clone(true).appendTo('#wcrTable thead');
            $('#wcrTable thead tr:eq(1) th').each(function (i)
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
            var table = $('#wcrTable').DataTable({
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
            $('#wcrTable tbody').on('click', 'tr.group', function ()
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
            delaySearch("#wcrTable", __key);
			getStatusBackground();
        });

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
