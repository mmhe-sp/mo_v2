<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DWC.ascx.cs" Inherits="MMHE.MO.Controls.DWC" %>


<div class="row mt-3">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <table id="jcsTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
                    <thead class="thead-dark">
                        <tr>
                            <th></th>
                            <th class="no-sort text-center" style="column-width: 150px;">Type
                                <div class="text-muted"><small>Discipline</small></div>
                            </th>
                            <th class="no-sort text-center" style="column-width: 40px;">Owner No</th>
                            <th class="no-sort text-center" style="column-width: 40px;">WBS</th>
                            <th class="no-sort text-center">Work Title
                                <div class="text-muted"><small>Remarks</small></div>
                            </th>
                            <th class="no-sort text-center" style="column-width: 120px;">Progress</th>
                            <th class="no-sort text-center" style="width: 20px"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="jcsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# DataBinder.Eval(Container.DataItem, "MyGroup") %></td>
                                    <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                        <div class="float-end"><span class="badge badge-pill badge-soft-primary font-size-11 jsl-status"><%# DataBinder.Eval(Container.DataItem, "JSLStatus") %></span></div>
                                        <%# DataBinder.Eval(Container.DataItem, "lType") %>
                                        <div class="text-muted"><small><%# DataBinder.Eval(Container.DataItem, "lDiscipline") %></small></div>
                                    </td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "OwnerNo") %></td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "WBS") %></td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <div class="text-dark">
                                            <%# DataBinder.Eval(Container.DataItem, "Work_Title") %>
                                        </div>
                                        <span class="text-muted">
                                            <small><%# DataBinder.Eval(Container.DataItem, "JSLRemarks") %></small>
                                        </span>
                                    </td>
                                    <td class="text-center" style="width: 40px;">
                                        <div class="row mb-1">
                                            <div class="col text-start">
                                                <div class="text-muted">
                                                    <small><%# DataBinder.Eval(Container.DataItem, "CompletionPer") %>%</small>
                                                </div>
                                            </div>
                                            <div class="col text-end">
                                                <div class="text-muted"><small><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %></small></div>
                                            </div>
                                        </div>
                                        
                                    </td>
                                    <td style="width: 20px">
                                        <a href="<%#(DataBinder.Eval(Container.DataItem, "ActivityType").ToString() == "O")?"jcs-edit.aspx":"vo-manage.aspx" %>?id=<%#DataBinder.Eval(Container.DataItem, "JCSID") %>&type=<%#DataBinder.Eval(Container.DataItem, "ActivityType") %>"><i class="mdi mdi-circle-edit-outline"></i></a>
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