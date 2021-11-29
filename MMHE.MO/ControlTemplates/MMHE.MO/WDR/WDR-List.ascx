﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WDR-List.ascx.cs" Inherits="MMHE.MO.ControlTemplates.MMHE.MO.WDR.WDR_List" %>
<div class="row">
    <%--<div class="col-12">
        <a href="vo-manage.aspx?type=A" class="btn btn-primary btn-sm me-1"><i class="mdi mdi-plus-circle me-1"></i>New AWO</a>
        <a href="vo-manage.aspx?type=V" class="btn btn-primary btn-sm me-1"><i class="mdi mdi-plus-circle me-1"></i>New VO</a>
        <button type="button" class="btn btn-primary btn-sm me-1" onclick="exportJCS()"><i class="mdi mdi-download-circle me-1"></i>Export</button>
        <button type="button" class="btn btn-primary btn-sm me-1" onclick="showJCSUploadModal()"><i class="mdi mdi-cloud-upload-outline me-1"></i>Upload</button>
    </div>--%>
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
                            <th class="no-sort text-center">Start Date</th>
                            <th class="no-sort text-center">End Date</th>
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
                                        </div>
                                    </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "StartDate",  "{0:dd/MM/yyyy}") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "EndDate",  "{0:dd/MM/yyyy}") %></td>
                                    <td style="width: 20px">
                                        <a href="WDRI.aspx?refno=<%# DataBinder.Eval(Container.DataItem, "OwnerNo") %>&jcsid=<%# DataBinder.Eval(Container.DataItem, "JCSID") %>&dc=<%# DataBinder.Eval(Container.DataItem, "DisciplineCode") %>&ProjectID=<%# DataBinder.Eval(Container.DataItem, "ProjectID") %>&type=Subcon"><i class="mdi mdi-circle-edit-outline"></i></a>
                                        <td></td>
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

<%--<div class="modal" id="jcsModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">JCS - Bulk Upload</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="row m-3">
                    <div class="col">
                        <label class="form-label" for="jcsFile">Select File :</label>
                        <input type="file" id="jcsFile" class="form-control form-control-file" title="Select Only Excel File" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" onclick="uploadExcel()">Upload</button>
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>--%>