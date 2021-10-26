<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="MMHE.MO.Controls.JSL.List" %>
<div class="row">
	<div class="col-12">
		<button type="button" class="btn btn-primary btn-sm me-1"><i class="mdi mdi-plus-circle me-1"></i>New AWO</button>
		<button type="button" class="btn btn-primary btn-sm me-1"><i class="mdi mdi-plus-circle me-1"></i>New VO</button>
		<button type="button" class="btn btn-primary btn-sm me-1" onclick="exportJCS()"><i class="mdi mdi-download-circle me-1"></i>Export</button>
		<button type="button" class="btn btn-primary btn-sm me-1" onclick="showJCSUploadModal()"><i class="mdi mdi-cloud-upload-outline me-1"></i>Upload</button>
	</div>
</div>
<div class="row mt-3">
	<div class="col-12">
		<div class="card">
			<div class="card-body">
			</div>
		</div>
	</div>
	<!-- end col -->
</div>
<!-- end row -->

<div class="modal" id="jslModal" tabindex="-1" role="dialog">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<h5 class="modal-title">JSL - Bulk Upload</h5>
				<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
			</div>
			<div class="modal-body">
				<div class="row m-3">
					<div class="col">
						<label class="form-label" for="jcsFile">Select File :</label>
						<input type="file" id="jcsFile" class="form-control form-control-file" title="Select Only Excel File" />
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-primary" onclick="uploadExcel()">Upload</button>
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
			</div>
		</div>
		<!-- /.modal-content -->
	</div>
	<!-- /.modal-dialog -->
</div>
<!-- /.modal -->
