<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="JCS" Src="~/_controltemplates/15/MMHE.MO/JCS/Manage.ascx" %>
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
					<mo:projectname runat="server" id="projectName"></mo:projectname>
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
		<mo:jcs runat="server" id="jcs"></mo:jcs>
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
		$(document).ready(function ()
		{
			$("#datatable-buttons").DataTable({
				lengthChange: !1, buttons: ["excel"], search: false
			}).buttons().container().appendTo("#datatable-buttons_wrapper .col-md-6:eq(0)");

			$('.auto-resize').on('input', function ()
			{
				autoResize(this, "input");
			});

			$.each($('.auto-resize'), function (i, v) { autoResize(v, "each"); });
		});

		function autoResize(ctrl, event)
		{
			var h = ctrl.scrollHeight;
			var td = $(ctrl).closest("td");
			var tr = td.closest("tr");
			var h2 = td.height();
			if (h2 > h)
				h = h2;

			if (event == "each")
			{
				var fields = tr.find('.auto-resize');
				$.each(fields, function (i, v)
				{
					h2 = v.scrollHeight;
					if (h < h2)
						h = h2;
				});
				fields.height(h);
			} else
			{
				ctrl.style.height = "auto";
				ctrl.style.height = (h) + "px";

				var field;
				var fields = tr.find('.auto-resize');
				$.each(fields, function (i, v)
				{
					if (v.id.toString() != ctrl.id.toString())
					{
						field = v;
					}
				});
				field.setAttribute("style", "height:" + (h) + "px;");
			}
		}

		function removeRow(index)
		{
			$('.row-' + index).remove();
		}
	</script>
</asp:Content>
