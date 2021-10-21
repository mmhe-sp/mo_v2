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
			<div class="card-header p-0">
				<a data-bs-target=".details" data-bs-toggle="collapse" class="accordion-button fw-medium" aria-expanded="false">Details</a>
			</div>
			<div class="card-body">
				<div class="details collapse">
					<div class="row mb-3">
						<div class="col-sm-4">
							<label for="example-text-input" class="form-label">Type</label>
							<input class="form-control" type="text" placeholder="Type" readonly value="<%=Details.Type %>">
						</div>
						<div class="col-sm-4">
							<label for="example-text-input" class="form-label">Owner No.</label>
							<input class="form-control" type="text" placeholder="Owner No." readonly value="<%=Details.OwnerNo %>">
						</div>
						<div class="col-sm-4">
							<label for="example-text-input" class="form-label">Discipline</label>
							<input class="form-control" type="text" placeholder="Discipline" readonly value="<%=Details.Discipline %>">
						</div>
					</div>
					<div class="mb-3 row">
						<div class="col-sm-12">
							<label for="example-text-input" class="form-label">Work Title</label>
							<input class="form-control" type="text" placeholder="Work Title" readonly value="<%=Details.WorkTitle %>">
						</div>
					</div>
					<div class="mb-3 row">
						<div class="col-sm-6">
							<label for="example-text-input" class="form-label">WBS</label>
							<input class="form-control" type="text" placeholder="WBS" readonly value="<%=Details.WBS %>">
						</div>
						<div class="col-sm-6">
							<label for="startDate" class="form-label">Start/End Date</label>
							<div class="input-group mb-3">
								<input type="date" id="startDate" class="form-control" title="Start Date" aria-label="Start Date" value="<%=(Details.StartDate.HasValue?Details.StartDate.Value.ToString("yyyy-MM-dd"):string.Empty) %>">
								<span class="input-group-text">-</span>
								<input type="date" id="endDate" class="form-control" title="End Date" aria-label="End Date" value="<%=(Details.EndDate.HasValue?Details.EndDate.Value.ToString("yyyy-MM-dd"):string.Empty) %>">
								<span class="input-group-text">Duration <%=Details.Duration %> Days</span>
							</div>
						</div>
					</div>
				</div>
				<div class="row mb-3">
					<div class="col">
						<button type="button" class="btn btn-primary me-1"><i class="mdi mdi-floppy me-1"></i>Save</button>
						<div class="btn-group">
							<button type="button" class="btn btn-primary dropdown-toggle" id="add-rows"
								data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								<i class="mdi mdi-plus-circle me-1"></i>Add Row</button>
							<div class="dropdown-menu p-0"
								aria-labelledby="add-rows">
								<form class="p-3">
									<div class="form-group m-0">
										<div class="input-group">
											<input type="text" class="form-control" id="rows" placeholder="Rows" aria-label="Recipient's username">
											<div class="input-group-append">
												<button class="btn btn-primary" type="button" onclick="addNewRows()"><i class="mdi mdi-magnify"></i></button>
											</div>
										</div>
									</div>
								</form>
							</div>
						</div>
						<button type="button" class="btn btn-primary me-1"><i class="mdi mdi-plus-circle me-1"></i>New VO</button>
					</div>
				</div>
				<div class="row">
					<div class="col-sm-12">
						<table id="activities" class="table table-bordered dt-responsive w-100 font-size-12">
							<thead>
								<tr>
									<th style="width: 40px">Item</th>
									<th>Details Scope & Specification</th>
									<th style="width: 200px">Resources</th>
									<th style="width: 100px">Update By</th>
									<th style="width: 20px">&nbsp;</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td colspan="5">
										<a href="javascript:void(0)" data-bs-toggle="collapse" data-bs-target="#scope" role="button" aria-expanded="false" aria-controls="scope">Original Work Scope</a>
										<div class="collapse" id="scope">
											<%=Details.Description.Replace(Environment.NewLine,"<br/>") %>
										</div>
									</td>
								</tr>
								<tr class="row-new d-none">
									<td style="width: 40px">
										<span class="seqNo"></span>
									</td>
									<td>
										<textarea class="form-control auto-resize"></textarea>
									</td>
									<td>
										<select class="form-select">
											<%foreach (var item in Details.Resources)
												{ %>
											<option value="<%=item.Value %>"><%=item.Text %></option>
											<%} %>
										</select>
									</td>
									<td>
										<div class="dark-text">
											&nbsp;
										</div>
										<div class="text-muted">
											&nbsp;
										</div>
									</td>
									<td>
										<a href="javascript:void(0)" onclick="removeRow(this)"><i class="mdi mdi-trash-can-outline fx-2 text-danger"></i></a>
									</td>
								</tr>
								<asp:Repeater ID="jcsRepeater" runat="server">
									<ItemTemplate>
										<tr class="row-<%# Container.ItemIndex %> activity">
											<td style="width: 40px">
												<span class="seqNo"><%# DataBinder.Eval(Container.DataItem, "Sequence") %></span>
											</td>
											<td>
												<textarea class="form-control auto-resize remarks"><%# DataBinder.Eval(Container.DataItem, "Remarks") %></textarea>
											</td>
											<td>
												<select class="form-select resource" data-value="<%# DataBinder.Eval(Container.DataItem, "Resource") %>">
													<%foreach (var item in Details.Resources)
														{ %>
													<option value="<%=item.Value %>"><%=item.Text %></option>
													<%} %>
												</select>
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
											<td>
												<a href="javascript:void(0)" onclick="removeRow(this)"><i class="mdi mdi-trash-can-outline fx-2 text-danger"></i></a>
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
