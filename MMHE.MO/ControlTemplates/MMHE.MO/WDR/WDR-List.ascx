<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WDR-List.ascx.cs" Inherits="MMHE.MO.ControlTemplates.MMHE.MO.WDR.WDR_List" %>
<div class="row">
</div>
<div class="row mt-3">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <table id="wdrTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
                    <thead class="thead-dark">
                        <tr>
                            <th class="no-sort text-center">Type
                                <div class="text-muted"><small>Discipline</small></div>
                            </th>
                            <th class="no-sort text-center">Owner No</th>
                            <th class="no-sort text-center">Work Title
                                <div class="text-muted"><small>Remarks</small></div>
                            </th>
                            <th class="no-sort text-center">DWC Completion Date</th>
                            <th class="no-sort text-center">WDR Completion Date</th>
                            <%--<th class="no-sort text-center">Progress</th>--%>
                            <th class="no-sort text-center" style="width: 20px"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rWDRSubmission" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                        <div class="float-end"></div>
                                        <%# DataBinder.Eval(Container.DataItem, "lType") %>
                                        <div class="text-muted"><small><%# DataBinder.Eval(Container.DataItem, "Description") %></small></div>
                                    </td>
                                    <td style="width: 40px;"><%# DataBinder.Eval(Container.DataItem, "OwnerNo") %></td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <div class="text-dark">
                                            <%# DataBinder.Eval(Container.DataItem, "WorkTitle") %>
                                            <div class="text-end text-muted">
                                                <small>
                                                    <%# DataBinder.Eval(Container.DataItem, "StartDate",  "{0:dd/MM/yyyy -}") %>
                                                    <%# DataBinder.Eval(Container.DataItem, "EndDate",  "{0:dd/MM/yyyy}") %>                                                    
                                                </small>
                                            </div>
                                        </div>
                                    </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0:dd/MM/yyyy}") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "WDRCompletedDt",  "{0:dd/MM/yyyy}") %></td>
                                    <%--<td class="text-center">
                                        <div class="row mb-1">
                                            <div class="col text-start">
                                                <div class="text-muted">
                                                    <small><%# DataBinder.Eval(Container.DataItem, "CompletionPer") %>%</small>
                                                </div>
                                            </div>
                                            <%--<div class="col text-end">
                                                <div class="text-muted"><small><%# DataBinder.Eval(Container.DataItem, "DtDWC_Completed",  "{0: dd/MM/yyyy}") %></small></div>
                                            </div>
                                        </div>                                        
                                    </td>--%>
                                    <td style="width: 20px">
                                        <a href="WDRI.aspx?refno=<%# DataBinder.Eval(Container.DataItem, "OwnerNo") %>&jcsid=<%# DataBinder.Eval(Container.DataItem, "JCSID") %>&dc=<%# DataBinder.Eval(Container.DataItem, "DisciplineCode") %>&ProjectID=<%# DataBinder.Eval(Container.DataItem, "ProjectID") %>&type=Subcon">WDR Report</a>
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

