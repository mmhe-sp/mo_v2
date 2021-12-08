<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="MMHE.MO.ControlTemplates.MMHE.MO.Dashboard.Dashboard" %>

<div class="col-xl-4">
    <div class="card">
        <div class="card-body">
            <div class="d-flex">
                <div class="flex-grow-1">
                    <div class="d-flex">
                        <div class="flex-grow-1">
                            <div class="text-muted">
                                <h5 class="card-title mb-4">Job Summary List</h5>
                            </div>
                        </div>
                        <div class="flex-shrink-0 dropdown ms-2">
                            <asp:Label ID="TotalJSLItems" runat="server" Style="font-weight: bold; font-size: large;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-header p-0">
            <a data-bs-target=".details" data-bs-toggle="collapse" class="accordion-button fw-medium collapsed">Details</a>
        </div>
        <div class="card-body">
            <div class="details collapse hide">
                <table id="jslStatTable" class="table table-bordered table-striped nowrap w-100 font-size-10">
                    <thead class="thead-dark" style="background-color: cornflowerblue;">
                        <tr>
                            <th class="no-sort text-center">Discipline</th>
                            <th class="no-sort text-center">Base Scope</th>
                            <th class="no-sort text-center">Variation</th>
                            <th class="no-sort text-center">Additional</th>
                            <th class="no-sort text-center">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="tJSLStatistics" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Discipline") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "BaseScope") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Variation") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Additional") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Total") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <h4 class="card-title mb-4">Daily Work Progress</h4>
            <div class="table-responsive mt-4">
                <table class="table align-middle table-nowrap">
                    <tbody>
                        <tr>
                            <td style="width: 30%">
                                <p class="mb-0">Progress Update</p>
                            </td>
                            <td style="width: 25%">
                                <div class="flex-shrink-0 dropdown ms-2">
                                    <asp:Label ID="lTotalNumber" runat="server" Style="font-weight: bold; font-size: large;"></asp:Label>
                                </div>
                            </td>
                            <td style="color: cornflowerblue;">
                                <div class="flex-shrink-0 dropdown ms-2">
                                    <asp:Label ID="lTotalTask" runat="server" Style="font-weight: bold; font-size: large;"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <%--<tr>
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
							</tr>--%>
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
                <h4 class="card-title mb-4">Job Confirmation Scope</h4>
            </div>
            <div id="stacked-column-chart" class="apex-charts" dir="ltr"></div>
        </div>
    </div>
</div>





		
