<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DWC.ascx.cs" Inherits="MMHE.MO.Controls.DWC" %>


<div class="row mt-3">
	<div class="col-12">
		<div class="card">
			<div class="card-body">
				<table id="jcsTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
					<thead class="thead-dark">
						<tr>
							<th></th>
							<th>Owner No</th>
							<th class="no-sort text-center" style="column-width: 150px;">Type</th>
							<th class="no-sort text-center" style="column-width: 150px;"><%=Today %></th>
							<th class="no-sort text-center" style="column-width: 150px;"><%=Tomorrow %></th>
							<th class="no-sort text-center" style="column-width: 40px;">Work Title</th>
							<th class="no-sort text-center" style="column-width: 120px;">Completion %</th>
							<th class="no-sort text-center">Remarks</th>
							<th class="no-sort text-center">Subcontractor Remarks</th>
						</tr>
					</thead>
					<tbody>
						<asp:Repeater ID="jcsRepeater" runat="server">
							<ItemTemplate>
								<tr>
									<td><%# DataBinder.Eval(Container.DataItem, "MyGroup") %></td>
									<td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "OwnerNo") %></td>
									<td>
										<div class="float-end"><span class="badge badge-pill badge-soft-primary font-size-11 jsl-status"><%# DataBinder.Eval(Container.DataItem, "JSLStatus") %></span></div>
										<%# DataBinder.Eval(Container.DataItem, "lType") %>
									</td>
									<td>
										<div class="text-dark">
											<%# DataBinder.Eval(Container.DataItem, "Work_Title") %>
										</div>
									</td>
									<td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
										<textarea class="form-control"></textarea>
									</td>
									<td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
										<textarea class="form-control" ></textarea>
									</td>
									<td>
										<input type="text" value="<%# DataBinder.Eval(Container.DataItem, "CompletionPer") %>" />
									</td>
									<td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
										<textarea class="form-control"></textarea>
									</td>
									<td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
										<textarea class="form-control" ></textarea>
									</td>
								</tr>
							</ItemTemplate>
						</asp:Repeater>
					</tbody>
				</table>
			</div>
		</div>
	</div>
	<!-- end col -->
</div>
<!-- end row -->
