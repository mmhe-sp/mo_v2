<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="MMHE.MO.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="System.Linq" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DWC.ascx.cs" Inherits="MMHE.MO.Controls.DWC" %>

<style>
    .percentage {
        width: 70px;
    }

    textarea.form-control-sm {
        min-height: calc(1.5em + .967rem + 4px);
    }
</style>
<% int activitySequence = 0; %>
<div class="row mt-3">
    <div class="col-12">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col text-start">
                        <div class="row g-3 align-items-center">
                            <div class="col-auto">
                                <label class="col-form-label">Subcontractor:</label>
                            </div>
                            <div class="col-auto">
                                <select class="form-select subcontractor" onchange="filterRowsByResource(this)" id="subContractor">
                                    <option value="0">All</option>
                                    <%foreach (var item in Resources)
                                      { %>
                                    <option value="<%=item.Value %>"><%=item.Text %></option>
                                    <%} %>
                                </select>
                                <span class="text-danger d-none subcontractor-req-msg">Please select Sub-Contractor.</span>
                            </div>
                        </div>
                    </div>
                    <div class="col text-end">
                        <button type="button" class="btn btn-primary btn-sm me-1" onclick="verify()"><i class="mdi mdi-check-circle-outline me-1"></i>Subcon Verify</button>
                        <button type="button" class="btn btn-primary btn-sm me-1" onclick="saveProgress()"><i class="mdi mdi-floppy me-1"></i>Save</button>
                        <button type="button" class="btn btn-primary btn-sm me-1" onclick="exportDWC()"><i class="mdi mdi-download-circle me-1"></i>Export</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">

                        <table id="jcsTable" class="table table-bordered table-striped nowrap w-100 font-size-12">
                            <thead class="bg-primary bg-gradient text-white text-center align-middle">
                                <tr>
                                    <th style="width: 125px;">Owner No
                                        <div>
                                            <small>Type</small>
                                        </div>
                                    </th>
                                    <th style="width: 150px;">Work Title</th>
                                    <th style="width: 150px;"><%=Today %> (Progress)</th>
                                    <th style="width: 150px;"><%=Tomorrow %> (Plan)</th>
                                    <th style="width: 70px;">Completion %</th>
                                    <th style="width: 150px;">Remarks</th>
                                    <th style="width: 150px;" class="s-contractator">Sub-Contractor
                                        <br />
                                        Remarks
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (var details in Details)
                                  {
                                %>
                                <tr class="discipline" data-discipline="<%=details.First().Discipline %>">
                                    <td colspan="4" class="group bg-light">
                                        <%=details.Key %>
                                    </td>
                                    <td class="bg-light text-center">
                                        <span class="percentage font-weight-bold" />
                                    </td>
                                    <td class="bg-light"></td>
                                    <td class="bg-light s-contractator"></td>
                                </tr>
                                <%foreach (MMHE.MO.Models.DWCDetails item in details)
                                  { %>

                                <tr class="jcs" data-id="<%=item.JCSID %>" data-discipline="<%=item.Discipline %>">

                                    <td>
                                        <div class="float-end"><span class="badge badge-pill badge-soft-primary font-size-10 jsl-status text-uppercase"><%= item.JSLStatus %></span></div>
                                        <%= item.OwnerNo %>
                                        <div>
                                            <small><%= item.lType %></small>
                                        </div>

                                    </td>
                                    <td style="width: 200px;">
                                        <div class="text-dark ">
                                            <%= item.Work_Title %>
                                            <div class="text-start text-muted">
                                                <small>
                                                    <%=(item.StartDate.HasValue?item.StartDate.Value.ToShortDateString():"") %>
                                                    <%=(item.StartDate.HasValue && item.EndDate.HasValue?"-":"") + (item.EndDate.HasValue?item.EndDate.Value.ToShortDateString():"")  %>
                                                </small>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <textarea class="form-control form-control-sm today auto-resize" rows="1"><%=item.Today %></textarea>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <textarea class="form-control form-control-sm tomorrow auto-resize" rows="1"><%=item.Tomorrow %></textarea>
                                    </td>
                                    <td class="text-center">
                                        <span class="percentage font-weight-bold" />
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <textarea class="form-control form-control-sm remarks auto-resize" rows="1"><%=item.Remarks %></textarea>
                                    </td>
                                    <td class="s-contractator"></td>
                                </tr>
                                <%foreach (var activity in item.ActivityProgress)
                                  {
                                      activitySequence++;
                                %>
                                <tr class="activity" data-id="<%=activity.ActivityID %>" data-jcs-id="<%=item.JCSID %>" data-subcontractor="<%=activity.SubscontractorId %>">
                                    <td>
                                        <span class="id d-none"><%=activity.ActivityID%></span>

                                        <%if (activity.ActivityType == "I")
                                          { %>
                                        <div class="text-end">
                                            IWR
                                            <div>
                                                <small><%= activity.ActivityDiscipline %></small>
                                            </div>
                                        </div>
                                        <%} %>
                                    </td>
                                    <td style="width: 200px;">
                                        <%if (activity.Status == 1)
                                          {%>
                                        <div class="float-end" title="Subcon Verified">
                                            <span class="badge rounded-pill bg-success text-uppercase">
                                                <i class="mdi mdi-check-circle-outline"></i>Subcon Verified
                                            </span>
                                        </div>
                                        <%} %>

                                        <%if (activity.ActivityType == "I")
                                          { %>
                                        <div class="float-end"><span class="badge badge-pill <%=activity.IWRStatus.Equals("Pending",StringComparison.InvariantCultureIgnoreCase)?"bg-warning":"bg-success" %>  font-size-10 text-uppercase"><%=activity.IWRStatus %></span></div>
                                        <%} %>
                                        <a href="javascript:void(0)" data-bs-toggle="collapse" data-bs-target="#__a<%=activitySequence%>" role="button" aria-expanded="false" aria-controls="__a<%=activitySequence%>" class="d-block">
                                            <%= activity.Subscontractor %>
                                        </a>
                                        <span class="collapse mt-2" id="__a<%=activitySequence%>" style="white-space: normal; word-wrap: break-word !important;">
                                            <%=(string.IsNullOrWhiteSpace(activity.ActivityTitle)?"":activity.ActivityTitle).Replace(Environment.NewLine,"<br/>").Replace("\n","<br/>") %>
                                        </span>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <%if (activity.ActivityType != "I")
                                          { %>
                                        <textarea class="form-control form-control-sm today auto-resize" rows="1" <%=activity.Status == 1?"readonly":"" %>><%=activity.Today %></textarea>
                                        <%} %>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <%if (activity.ActivityType != "I")
                                          { %>
                                        <textarea class="form-control form-control-sm tomorrow auto-resize" rows="1"><%=activity.Tomorrow %></textarea>
                                        <%} %>
                                    </td>
                                    <td>
                                        <%if (activity.ActivityType == "I")
                                          { %>
                                        <input type="hidden" class="form-control form-control-sm percentage" value="<%=activity.Completion %>" />
                                        <div class="text-center">
                                            <span class="percentage"><%=activity.Completion.ToString("N0") %>%</span>
                                        </div>
                                        <%}
                                          else
                                          { %>
                                        <div class="input-group">
                                            <input type="number" class="form-control form-control-sm percentage" min="0" max="100" step="1" maxlength="3" data-current="<%=activity.Completion %>" value="<%=activity.Completion %>" onchange="calculateJCSCompletion('<%=item.JCSID %>')" <%=activity.Status == 1?"readonly":"" %> />
                                            <span class="input-group-text">%</span>
                                        </div>
                                        <%} %>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;">
                                        <%if (activity.ActivityType != "I")
                                          { %>
                                        <textarea class="form-control form-control-sm remarks auto-resize" rows="1"><%=activity.Remarks %></textarea>
                                        <%} %>
                                    </td>
                                    <td style="white-space: inherit; word-wrap: break-word !important;" class="s-contractator">
                                        <textarea class="form-control form-control-sm auto-resize s-remarks" rows="1"><%=activity.SubContractorRemarks %></textarea>
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
        </div>
    </div>
    <!-- end col -->
</div>
<!-- end row -->

<span class="today d-none"><%=_Today %></span>
<span class="tomorrow d-none"><%=_Tomorrow %></span>