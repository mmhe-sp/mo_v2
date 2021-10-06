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
                <table id="mydata" class="stripe row-border order-column" cellspacing="0" width="100%">
                    <thead>
                        <tr>
                            <th align="left" style="column-width: 40px;">Type</th>
                            <th align="left" style="column-width: 40px;">Disc</th>
                            <th style="column-width: 40px;">Owner No</th>
                            <th style="column-width: 40px;">AWO/VO No</th>
                            <th style="column-width: 40px;">WBS</th>
                            <th style="column-width: 160px;">Work Title</th>
                            <th style="column-width: 160px;">Remarks</th>
                            <th style="column-width: 40px;">Status</th>
                            <th style="column-width: 30px;">% (DWC)</th>
                            <th>Action</th>
                            <th>DWC Completion Date</th>
                            <th>WDR Completion Date</th>
                            <th>WCR Completion Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="jcsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 40px; white-space: inherit; word-wrap: break-word !important;"><%# DataBinder.Eval(Container.DataItem, "lType") %></td>
                                    <td style="width: 40px; white-space: inherit; word-wrap: break-word !important;"><%# DataBinder.Eval(Container.DataItem, "lDiscipline") %></td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "OwnerNo") %></td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "AWO_VONO") %></td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "WBS") %></td>
                                    <td style="width: 160px; white-space: inherit; word-wrap: break-word !important;"><%# DataBinder.Eval(Container.DataItem, "Work_Title") %></td>
                                    <td style="width: 160px; white-space: inherit; word-wrap: break-word !important;"><%# DataBinder.Eval(Container.DataItem, "JSLRemarks") %></td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "JSLStatus") %></td>
                                    <td style="width: 30px; text-align: center;"><%# DataBinder.Eval(Container.DataItem, "CompletionPer") %></td>
                                    <td>
                                        <a href="../jcs/edit.aspx">Edit</a>
                                    </td>
                                    <td style="text-align: center"><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %></td>
                                    <td style="text-align: center"><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %></td>
                                    <td style="text-align: center"><%# DataBinder.Eval(Container.DataItem, "DtWCRAct",  "{0: dd/MM/yyyy}") %></td>
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
