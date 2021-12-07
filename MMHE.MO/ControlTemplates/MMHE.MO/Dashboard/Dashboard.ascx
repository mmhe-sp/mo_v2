<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="MMHE.MO.ControlTemplates.MMHE.MO.Dashboard.Dashboard" %>

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
                        <asp:Label ID="TotalJSLItems" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="card">
    <div class="card-body">
        <table id="jslStatTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
            <thead class="thead-dark">
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
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <%# DataBinder.Eval(Container.DataItem, "Discipline") %>
                            </td>
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
		
