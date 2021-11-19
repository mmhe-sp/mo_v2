<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="MMHE.MO.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>
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
                            <th style="column-width: 150px;">Owner No
                                <div>
                                    <small>Type</small>
                                </div>
                            </th>
                            <th class="no-sort text-center">Work Title</th>
                            <th class="no-sort text-center" style="column-width: 150px;"><%=Today %></th>
                            <th class="no-sort text-center" style="column-width: 150px;"><%=Tomorrow %></th>
                            <th class="no-sort text-center" style="column-width: 70px;">Completion %</th>
                            <th class="no-sort text-center" style="column-width: 150px;">Remarks</th>
                            <th class="no-sort text-center" style="column-width: 150px;">SubContractor <br />Remarks</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%foreach (var details in Details)
                          { %>
                        <tr>
                            <td colspan="7" class="group bg-light">
                                <%=details.Key %>
                            </td>
                        </tr>
                        <%foreach (MMHE.MO.Models.DWCDetails item in details)
                          { %>

                        <tr>

                            <td style="width: 40px;">
                                <div class="float-end"><span class="badge badge-pill badge-soft-primary font-size-11 jsl-status"><%= item.JSLStatus %></span></div>
                                <%= item.OwnerNo %>
                                <div>
                                    <small><%= item.lType %></small>
                                </div>

                            </td>
                            <td>
                                <div class="text-dark">
                                    <%= item.Work_Title %>
                                </div>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td>
                                <input type="text" value="<%= item.CompletionPer %>" class="form-control" />
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                        </tr>
                        <%foreach(var activity in item.ActivityProgress){ %>
                        <tr>
                            <td></td>
                            <td>
                                <%= activity.Subscontractor %>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td>
                                <input type="text"  class="form-control" />
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                            <td style="width: 130px; white-space: inherit; word-wrap: break-word !important;">
                                <textarea class="form-control"></textarea>
                            </td>
                        </tr>
                        <%} %>
                        <%} %>

                        <%} %>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <!-- end col -->
</div>
<!-- end row -->
