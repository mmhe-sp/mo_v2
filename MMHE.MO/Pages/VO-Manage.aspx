﻿<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="VO" Src="~/_controltemplates/15/MMHE.MO/VO/Manage.ascx" %>
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
                            <li class="breadcrumb-item active">Manage VO</li>
                        </ol>
                    </div>

                </div>
            </div>
        </div>
        <!-- end page title -->
        <mo:VO runat="server" id="jcs"></mo:VO>
    </div>
    <!-- container-fluid -->

    <div id="stacked-column-chart" style="display: none"></div>
    <div id="radialBar-chart" style="display: none"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    MMHE::Manage VO
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Manage VO
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">

        var lastSequence = 0;
        $(document).ready(function ()
        {
            try
            {
                $("#activities").DataTable({
                    lengthChange: !1, search: false
                });
            } catch (e) { }

            $('.auto-resize').on('input', function ()
            {
                autoResize(this, "input");
            });

            $.each($('.auto-resize'), function (i, v)
            {
                autoResize(v, "each");
            });
            addNewRow(1);
            resetSequenceNumber();
            initializeResources();
        });

        function autoResize(ctrl, event)
        {
            var h = ctrl.scrollHeight;

            ctrl.style.height = (h) + "px";
        }

        function removeRow(ctrl)
        {
            $($(ctrl).closest('tr')).remove();
            resetSequenceNumber();
        }
        function saveJCS()
        {
            var jcs = { Activities: [] };
            jcs.JCSID = $('.jcs-id').text();
            jcs.StartDate = $('#startDate').val();
            if (!jcs.StartDate)
                jcs.StartDate = null;
            jcs.EndDate = $('#endDate').val();
            if (!jcs.EndDate)
                jcs.EndDate = null;

            //get activities
            var rows = $('tr.activity');
            var row, activityId, remarks, resource, sequenceNo;
            for (var index = 0; index < rows.length; index++)
            {
                row = $(rows[index]);
                activityId = row.find('.activityId').text();
                remarks = row.find('.remarks').val();
                resource = row.find('.resource').val();
                sequenceNo = row.find('.seqNo').text();
                if (!activityId && !remarks)
                    continue;
                jcs.Activities.push({ Remarks: remarks, Sequence: sequenceNo, Resource: resource });
                if (activityId)
                    jcs.Activities[jcs.Activities.length - 1].ActivityID = activityId;
            }

            $.ajax({
                url: "jcs.asmx/Save",
                data: 'jcs=' + JSON.stringify(jcs),
                dataType: "json",
                type: "POST"
            }).done(function (d)
            {
                alert('VO Details have been saved successfully.');
                window.location.reload(true);
            }).fail(function () { window.location.reload(true); });
        }

        function addNewRows()
        {
            var value = $('#rows').val();
            if (isNaN(value)) return;
            addNewRow(Number(value));
            resetSequenceNumber();
        }

        function addNewRow(count)
        {
            //find new row
            var tr = $('tr.row-new');

            for (; count > 0; count--)
            {
                var clonedTr = tr.clone(true)
                $('#activities tbody').append(clonedTr);
                clonedTr.addClass('activity').removeClass('row-new').removeClass('d-none');
            }
        }

        function resetSequenceNumber()
        {
            var sequenceNos = $('tr.activity .seqNo');
            for (var index = 1; index <= sequenceNos.length; index++)
            {
                $(sequenceNos[index - 1]).text(index);
            }

        }

		function addEmptyRow(ctrl)
		{
			var tr = $('tr.row-new').clone(true);
			$(ctrl).closest('tr').insertAfter(tr)
			tr.addClass('activity').removeClass('row-new').removeClass('d-none');
			resetSequenceNumber();
		}

        function initializeResources()
        {
            var lists = $('tr.activity .resource');
            var value = '';
            var list;
            for (var index = 0; index < lists.length; index++)
            {
                list = $(lists[index]);
                value = list.data('value');
                if (value)
                    list.val(value);
            }
        }

        function confirmDeletion(ctrl)
        {
            Swal.fire({ title: "Marine Operaion", text: "Are you sure you want to delete?", icon: "warning", showCancelButton: !0, confirmButtonColor: "#34c38f", cancelButtonColor: "#f46a6a", confirmButtonText: "Yes, delete it!" })
                .then(function (t) { if (t.value) removeRow(ctrl); });
		}
	</script>
</asp:Content>
