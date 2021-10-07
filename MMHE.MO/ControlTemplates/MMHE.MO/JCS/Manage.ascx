<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="MMHE.MO.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manage.ascx.cs" Inherits="MMHE.MO.Controls.JCS.Manage" %>

<div class="row">
	<div class="col-12">
		<div class="card">
			<div class="card-body">
				<div class="row mb-3">
					<div class="col-sm-4">
						<label for="example-text-input" class="form-label">Type</label>
						<input class="form-control" type="text" placeholder="Type" readonly value="<%=Details.Type %>">
					</div>
					<div class="col-sm-4">
						<label for="example-text-input" class="form-label">Owner No.</label>
						<input class="form-control" type="text" placeholder="Owner No." readonly value="<%=Details.Type %>">
					</div>
					<div class="col-sm-4">
						<label for="example-text-input" class="form-label">Discipline</label>
						<input class="form-control" type="text" placeholder="Discipline" readonly value="<%=Details.Type %>">
					</div>
				</div>
				<div class="mb-3 row">
					<div class="col-sm-12">
						<label for="example-text-input" class="form-label">Work Title</label>
						<input class="form-control" type="text" placeholder="Work Title" readonly value="<%=Details.Type %>">
					</div>
				</div>
				<div class="mb-3 row">
					<div class="col-sm-6">
						<label for="example-text-input" class="form-label">WBS</label>
						<input class="form-control" type="text" placeholder="WBS" readonly value="<%=Details.WBS %>">
					</div>

					<div class="col-sm-6">
						<label for="example-text-input" class="form-label">Start/End Date</label>
						<div class="input-group mb-3">
							<input type="text" class="form-control" placeholder="Start Date" aria-label="Start Date" readonly value="<%=Details.StartDate %>">
							<span class="input-group-text">-</span>
							<input type="text" class="form-control" placeholder="End Date" aria-label="End Date" readonly value="<%=Details.EndDate %>">
							<span class="input-group-text">Duration <%=Details.Duration %> Days</span>
						</div>
					</div>
				</div>
				<div class="row mb-3">
					<div class="col">
						<button type="button" class="btn btn-primary me-1"><i class="mdi mdi-floppy me-1"></i>Save</button>
						<button type="button" class="btn btn-primary me-1"><i class="mdi mdi-plus-circle me-1"></i>Add Row</button>
						<button type="button" class="btn btn-primary me-1"><i class="mdi mdi-plus-circle me-1"></i>New VO</button>
					</div>
				</div>
				<div class="row">
					<div class="col-sm-12">
						<table id="datatable-buttons" class="table table-bordered dt-responsive nowrap w-100">
							<thead>
								<tr>
									<th style="width: 40px">Item</th>
									<th>Details Scope & Specification</th>
									<th style="width: 100px">Resources</th>
									<th style="width: 100px">Update By</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td colspan="4">
										<a href="javascript:void(0)" data-bs-toggle="collapse" data-arget="#scope" role="button" aria-expanded="false" aria-controls="scope">Original Work Scope</a>
										<div class="collapse" id="scope">
											<div class="card card-body">
												Some placeholder content for the collapse component. This panel is hidden by default but revealed when the user activates the relevant trigger.
											</div>
										</div>
									</td>
								</tr>
								<asp:Repeater ID="jcsRepeater" runat="server">
									<ItemTemplate>
										<tr>
											<td style="width: 40px">
												<%# DataBinder.Eval(Container.DataItem, "Sequence") %>
											</td>
											<td>
												<%# DataBinder.Eval(Container.DataItem, "Remarks") %>
											</td>
											<td>
												<%# DataBinder.Eval(Container.DataItem, "Resource") %>
											</td>
											<td>
												<div class="dark-text">
													<%# DataBinder.Eval(Container.DataItem, "UpdatedBy") %>
												</div>
												<div class="text-muted">
													<span class="small">
														<%# DataBinder.Eval(Container.DataItem, "UpdatedOn",  "{0: dd/MM/yyyy}") %>
													</span>
												</div>
											</td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</tbody>
						</table>
					</div>
				</div>

			</div>
		</div>
	</div>
	<!-- end col -->
</div>
<!-- end row -->
