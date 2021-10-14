<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="MMHE.MO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>
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
							<li class="breadcrumb-item active">Dashboard</li>
						</ol>
					</div>
				</div>
			</div>
		</div>
		<!-- end page title -->

		<div class="row">
			<div class="col-xl-4">
				<div class="card">
					<div class="card-body">
						<div class="d-flex">
							<div class="flex-grow-1">
								<div class="d-flex">
									<div class="flex-grow-1">
										<div class="text-muted">
											<h5 class="mb-1">Job Summary List</h5>
										</div>
									</div>
									<div class="flex-shrink-0 dropdown ms-2">
										<h4>583</h4>
									</div>
								</div>
								<hr>
								<div class="text-end">
									<a href="javascript: void(0);" class="btn btn-primary waves-effect waves-light btn-sm">Add <i class="mdi mdi-plus-circle ms-1"></i></a>
									<a href="javascript: void(0);" class="btn btn-primary waves-effect waves-light btn-sm">Export <i class="mdi mdi-download-circle ms-1"></i></a>
									<a href="javascript: void(0);" class="btn btn-primary waves-effect waves-light btn-sm">Import <i class="mdi mdi-progress-upload ms-1"></i></a>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="card">
					<div class="card-body">
						<h4 class="card-title mb-4 text-center">Daily Work Update</h4>

						<div class="table-responsive mt-4">
							<table class="table align-middle table-nowrap">
								<tbody>
									<tr>
										<td style="width: 30%">
											<p class="mb-0">Progress Update</p>
										</td>
										<td style="width: 25%">
											<h5 class="mb-0">1,456</h5>
										</td>
										<td>
											<div class="progress bg-transparent progress-sm">
												<div class="progress-bar bg-primary rounded" role="progressbar" style="width: 94%" aria-valuenow="94" aria-valuemin="0" aria-valuemax="100"></div>
											</div>
										</td>
									</tr>
									<tr>
										<td>
											<p class="mb-0">Progress Verification</p>
										</td>
										<td>
											<h5 class="mb-0">1,123</h5>
										</td>
										<td>
											<div class="progress bg-transparent progress-sm">
												<div class="progress-bar bg-success rounded" role="progressbar" style="width: 82%" aria-valuenow="82" aria-valuemin="0" aria-valuemax="100"></div>
											</div>
										</td>
									</tr>
									<tr>
										<td>
											<p class="mb-0">Work Done Verification</p>
										</td>
										<td>
											<h5 class="mb-0">1,026</h5>
										</td>
										<td>
											<div class="progress bg-transparent progress-sm">
												<div class="progress-bar bg-warning rounded" role="progressbar" style="width: 70%" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100"></div>
											</div>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
			<div class="col-xl-8">

				<div class="card">
					<div class="card-body">
						<div class="d-sm-flex flex-wrap">
							<h4 class="card-title mb-4">Job Scope Summary</h4>
						</div>

						<div id="stacked-column-chart" class="apex-charts" dir="ltr"></div>
					</div>
				</div>
			</div>
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
