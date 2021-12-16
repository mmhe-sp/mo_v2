<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="MMHE.MO.Controls.JSL.List" %>

<div class="row">
    <div class="col-12">
        <button type="button" class="btn btn-primary btn-sm me-1" onclick="download()"><i class="mdi mdi-download-circle me-1"></i>Export</button>
        <button type="button" class="btn btn-primary btn-sm me-1" onclick="showUploadModal()"><i class="mdi mdi-cloud-upload-outline me-1"></i>Upload</button>
    </div>
</div>
<div class="row mt-3">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <table id="jcsTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
                    <thead class="thead-dark">
                        <tr>
                            <th>Seq No</th>
                            <th class="no-sort text-center" style="column-width: 150px;">Type
                                <div class="text-muted"><small>Discipline</small></div>
                            </th>
                            <th class="no-sort text-center" style="column-width: 40px;">Owner No</th>
                            <th class="no-sort text-center" style="column-width: 40px;">WBS</th>
                            <th class="no-sort text-center">Work Title
                                <div class="text-muted"><small>Remarks</small></div>
                            </th>
                            <th class="no-sort text-center" style="column-width: 120px;">Weightage</th>
                            <th class="no-sort text-center" style="width: 20px"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="jcsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# DataBinder.Eval(Container.DataItem, "SequenceNo") %></td>
                                    <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                        <label hidden id="backgroundcolor+<%#DataBinder.Eval(Container.DataItem, "JSLID") %>"><%# DataBinder.Eval(Container.DataItem, "BackgroundColor") %></label>
                                        <label hidden id="fontcolor+<%#DataBinder.Eval(Container.DataItem, "JSLID") %>"><%# DataBinder.Eval(Container.DataItem, "FontColor") %></label>
                                        <div class="float-end"><span id="statusBadge+<%#DataBinder.Eval(Container.DataItem, "JSLID") %>" name="statusBadge+<%#DataBinder.Eval(Container.DataItem, "JSLID") %>" style="color: <%# DataBinder.Eval(Container.DataItem, "FontColor") %>; background-color: <%# DataBinder.Eval(Container.DataItem, "BackgroundColor") %>" class="badge badge-pill badge-soft-primary font-size-10  text-uppercase jsl-status"><%# DataBinder.Eval(Container.DataItem, "JSLStatus") %></span></div>
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
                                                    <small><%# DataBinder.Eval(Container.DataItem, "Weightage") %>%</small>
                                                </div>
                                            </div>
                                            <div class="col text-end">
                                                <div class="text-muted"><small><%# DataBinder.Eval(Container.DataItem, "WCRStatus") %></small></div>
                                            </div>
                                        </div>

                                    </td>
                                    <td style="width: 20px">
                                        <a href="javascript::void(0)"><i class="mdi mdi-circle-edit-outline"></i></a>
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

<div class="modal" id="jcsModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">JSL - Bulk Upload</h5>
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
</div>


<div class="modal" id="jcsEditModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Manage JSL Master</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="mb-3 row">
                    <div class="col-sm-12">
                        <label class="form-label">Sequence No</label>
                        <input class="form-control" type="text" placeholder="Sequence Number" value="">
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-sm-4">
                        <label class="form-label">Type</label>
                        <select class="form-select resource" data-value="" onchange="updateWorkTitle(this)" id="ownerNo">
                            <option disabled selected>Select Owner</option>
                            
                        </select>
                    </div>

                    <div class="col-sm-4">
                        <label class="form-label">Discipline</label>
                        <select class="form-select resource" data-value="" onchange="updateWBS(this)" id="discipline">
                            <option disabled selected>Select Discipline</option>
                           
                        </select>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-sm-4">
                        <label class="form-label">Owner No.</label>
                        <input class="form-control" type="text" placeholder="Owner No." readonly value="">
                    </div>
                    <div class="col-sm-4">
                        <label class="form-label">Status</label>
                        <select class="form-select resource" data-value="">
                            <option disabled selected>Select Status</option>
                           
                        </select>
                        
                    </div>
                </div>
                <div class="mb-3 row">
                    <div class="col-sm-12">
                        <label class="form-label">Work Title</label>
                        <input class="form-control" type="text" placeholder="Work Title" value="">
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" onclick="saveJSL()">Save</button>
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->
