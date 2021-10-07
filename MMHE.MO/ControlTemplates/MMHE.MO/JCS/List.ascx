<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="MMHE.MO.Controls.JCS.List" %>
<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <table id="jcsTable" class="table table-bordered nowrap w-100">
                    <thead>
                        <tr>
                            <th style="display: none;"></th>
                            <th align="left" style="column-width: 100px;">Type
                                <div class="text-muted"><small>Discipline</small></div>
                            </th>
                            <th style="column-width: 40px;">Owner No</th>
                            <th style="column-width: 40px;">WBS</th>
                            <th>Details</th>
                            <th style="column-width: 40px;">Status</th>
                            <th style="column-width: 30px;">% (DWC)</th>
                            <th style="width:70px;">Completion Date</th>
                            <th style="width:20px"></th>
                        </tr>
                    </thead>
                    <tbody class="font-size-13">
                        <asp:Repeater ID="jcsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="display: none;"><%# DataBinder.Eval(Container.DataItem, "MyGroup") %></td>
                                    <td style="width: 100px; white-space: inherit; word-wrap: break-word !important;"><%# DataBinder.Eval(Container.DataItem, "lType") %>
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
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "JSLStatus") %></td>
                                    <td style="width: 30px; text-align: center;"><%# DataBinder.Eval(Container.DataItem, "CompletionPer") %></td>
                                    <td style="text-align: center;width:70px">
                                        <div>
                                            <b>DWC:</b><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %>
                                        </div>
                                        <div>
                                            <b>WDR:</b><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %>
                                        </div>
                                        <div>
                                            <b>WCR:</b><%# DataBinder.Eval(Container.DataItem, "DtWCRAct",  "{0: dd/MM/yyyy}") %>
                                        </div>
                                    </td>
                                    <td style="width:20px">
                                        <a href="../jcs/edit.aspx?id=<%#DataBinder.Eval(Container.DataItem, "JCSID") %>"><i class="mdi mdi-circle-edit-outline"></i></a>
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
