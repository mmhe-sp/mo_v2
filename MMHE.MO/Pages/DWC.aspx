<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="DWC" Src="~/_controltemplates/15/MMHE.MO/DWC.ascx" %>
<%@ Register TagPrefix="mo" TagName="ProjectName" Src="~/_controltemplates/15/MMHE.MO/ProjectName.ascx" %>

<%@ Page Language="C#" MasterPageFile="../_catalogs/masterpage/MO.master" Inherits="MMHE.MO.UI.BasePage,MMHE.MO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container-fluid">
        <!-- start page title -->
        <div class="row">
            <div class="col-12">
                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                    <mo:ProjectName runat="server" id="projectName"></mo:ProjectName>

                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="javascript: void(0);">Marine Operation</a></li>
                            <li class="breadcrumb-item active">Daily Work Progress</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:DWC runat="server" id="jcs"></mo:DWC>
    </div>
    <!-- container-fluid -->
    <div id="stacked-column-chart" style="display: none"></div>
    <div id="radialBar-chart" style="display: none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Daily Work Progress
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Daily Work Progress
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script>
        var __key = 'dwc';
        var jscDataTable;
        $(document).ready(function ()
        {

            var groupColumn = 0;
            var value = '';
            if (!value)
                value = '';
            try
            {
                var table = $('#jcsTable').DataTable({
                    search: { search: value },
                    ordering: false,
                    order: [[groupColumn, 'asc']],
                    autoWidth: false,
                    displayLength: 25,
                    "aLengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
                    "iDisplayLength": 10,
                    "pageLength": -1,
                    "dom": '<"pull-left"f><"pull-right"l>tip',
                    scrollY: "300px",
                    scrollX: true,
                });

            }
            catch (e) { }
            delaySearch("#jcsTable", __key);

            jscDataTable = $("#jcsTable");

            var records = jscDataTable.find('tr.jcs');
            $.each(records, function (i, v)
            {
                calculateJCSCompletion($(v).data('id'));
            });
            getJSLStatusBackground();

            $('.auto-resize').on('input', function ()
            {
                autoResize(this, "input");
            });

            $.each($('.auto-resize'), function (i, v)
            {
                autoResize(v, "each");
            });
            filterRowsByResource($('#subContractor')[0]);
        });

        function calculateJCSCompletion(jcsId)
        {
            var activities = jscDataTable.find('tr.activity[data-jcs-id="' + jcsId + '"]');
            var percentage = 0;
            var total = 0;
            var ctrl;
            var current;
            var count = 0;
            $.each(activities, function (i, v)
            {
                count++;
                ctrl = $(v).find('input.percentage')
                current = ctrl.data('current');
                if (isNaN(current))
                    current = 0;
                else current = Number(current);
                if (current > 0)
                    current = 0;
                else if (current > 100)
                    current = 100;
                percentage = ctrl.val();
                if (isNaN(percentage))
                    percentage = current;
                else
                    percentage = Number(percentage);
                if (percentage < current)
                    percentage = current;
                else if (100 < percentage)
                    percentage = 100;
                total += percentage
                ctrl.val(percentage);
            });
            if (!count)
                count = 1;
            jscDataTable.find('tr.jcs[data-id="' + jcsId + '"] span.percentage').text((total / count).toFixed(2) + ' %');
        }
        function saveProgress()
        {

            var vo = extractModel();
            $.ajax({
                url: "dwc.asmx/SaveProgress",
                data: JSON.stringify({ dwc: vo }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                d = d.d;
                showMessage('The progress have been saved successfully.', 'success', reloadPage);
            }).fail(function ()
            {
                showMessage('Unable to save the progress.', 'error', reloadPage);
            });
        }

        function extractModel()
        {
            var rows = jscDataTable.find('tbody tr');
            var row;
            var jcsId;
            var activities = [];
            var activity;
            var dwc = { Today: $('span.today').text(), Tomorrow: $('span.tomorrow').text(), JCS: [] };
            var jcs;

            for (var index = 0; index < rows.length; index++)
            {
                row = $(rows[index]);
                if (row.hasClass('discipline'))
                    continue;
                else if (row.hasClass('jcs'))
                {

                    jcsId = row.data('id');
                    activities = [];
                    jcs = {
                        JCSID: jcsId,
                        Activities: activities,
                        Today: row.find('textarea.today').val(),
                        Tomorrow: row.find('textarea.tomorrow').val(),
                        Remarks: row.find('textarea.remarks').val()
                    };
                    dwc.JCS.push(jcs);
                    continue;
                }
                else if (row.hasClass('activity'))
                {
                    activity = {
                        JCSID: jcsId,
                        ActivityID: row.data('id'),
                        Today: row.find('textarea.today').val(),
                        Tomorrow: row.find('textarea.tomorrow').val(),
                        Completion: row.find('input.percentage').val(),
                        Remarks: row.find('textarea.remarks').val(),
                        SubContractorRemarks: row.find('textarea.s-remarks').val()
                    };
                    activities.push(activity);
                }
            }

            return dwc;
        }
        function autoResize(ctrl, event)
        {
            var h = ctrl.scrollHeight;
            ctrl.style.height = (h) + "px";
        }

        function filterRowsByResource(ctrl)
        {
            $('#jcsTable tbody tr').removeClass('d-none');
            if (ctrl.value == '0')
            {
                $('td.s-contractator,th.s-contractator').addClass('d-none');
            }
            else
            {
                $('tr.activity[data-subcontractor!="' + ctrl.value + '"]').addClass('d-none');
                $('td.s-contractator,th.s-contractator').removeClass('d-none');
                hideEmptyJCS();
                hideEmptyDisciplines();
            }
        }

        function verify()
        {
            var vo = extractModel();
            vo.Subcontractor = $("#subContractor").val();
            $('.subcontractor-req-msg').addClass('d-none');
            if (vo.Subcontractor == "0")
            {
                $('.subcontractor-req-msg').removeClass('d-none');
                return;
            }
            $.ajax({
                url: "dwc.asmx/Verify",
                data: JSON.stringify({ dwc: vo }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                d = d.d;
                showMessage('The subcon verification is done.', 'success', reloadPage);
            }).fail(function ()
            {
                showMessage('Unable to do subcon verification.', 'error', reloadPage);
            });
        }

        function reloadPage()
        {
            window.location.reload(true);
        }

        function hideEmptyJCS()
        {
            var trs = $('#jcsTable tbody tr:visible');
            var jscTr;
            var tr;
            for (var index = 0; index < trs.length; index++)
            {
                tr = $(trs[index]);
                if (tr.hasClass('jcs'))
                {
                    if (jscTr)
                        jscTr.addClass('d-none');
                    jscTr = tr;
                }
                else if (tr.hasClass('activity'))
                    jscTr = null;
            }
            if (jscTr)
                jscTr.addClass('d-none');
        }

        function hideEmptyDisciplines()
        {
            var trs = $('#jcsTable tbody tr:visible');
            var jscTr;
            var tr;
            for (var index = 0; index < trs.length; index++)
            {
                tr = $(trs[index]);
                if (tr.hasClass('discipline'))
                {
                    if (jscTr)
                        jscTr.addClass('d-none');
                    jscTr = tr;
                }
                else if (tr.hasClass('jcs'))
                    jscTr = null;
            }
            if (jscTr)
                jscTr.addClass('d-none');
        }
    </script>
</asp:Content>
