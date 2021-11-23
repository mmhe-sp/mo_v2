<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="mo" TagName="VO" Src="~/_controltemplates/15/MMHE.MO/VO/Manage.ascx" %>
<%@ Register TagPrefix="mo" TagName="ProjectName" Src="~/_controltemplates/15/MMHE.MO/ProjectName.ascx" %>

<%@ Page Language="C#" MasterPageFile="../_catalogs/masterpage/MO.master" Inherits="MMHE.MO.UI.ManageVO,MMHE.MO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=42907a3e9063eed0" %>

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
                            <li class="breadcrumb-item"><a href="dashboard.aspx">Marine Operation</a></li>
                            <li class="breadcrumb-item"><a href="jcs-list.aspx">Job Confirmation Scope</a></li>
                            <li class="breadcrumb-item active">Manage VO/AWO</li>
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
    MMHE::Manage AWO/VO
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Manage AWO/VO
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">

        var lastSequence = 0;
        $(document).ready(function ()
        {
            try
            {
                $("#activities").DataTable(
					{
					    lengthChange: !1, search: false, paging: false, info: false, sorting: false
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
            getDuration();
            enableDetails();
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
            var vo= extractModel();
            $.ajax({
                url: "vo.asmx/Save",
                data: JSON.stringify({ vo: vo }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                d = d.d;
                showMessage('The Details have been saved successfully.', 'success', function () { window.location.href = 'vo-manage.aspx?type=' + vo.Type + '&id=' + d.Id; });
            }).fail(function ()
            {
                showMessage('Unable to save the Details.', 'error', reloadGrid);
            });
        }
        function extractModel()
        {
            var jcs = { Activities: [] };
            jcs.JCSID = $('.jcs-id').text();
            jcs.Type = $('.jcs-type').text();
            jcs.StartDate = $('#startDate').val();
            if (!jcs.StartDate)
                jcs.StartDate = null;
            jcs.EndDate = $('#endDate').val();
            jcs.WBS = $('#wbs').val();
            jcs.Discipline = $('#discipline').val();
            jcs.WorkTitle = $('#WorkTitle').val();
            if (jcs.Type == "V")
                jcs.OwnerNo = $('#ownerNo').val();
            else
				jcs.OwnerNo = $('#txtOwnerNo').val();
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
            return jcs;
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

        function addEmptyRow(ctrl, top)
        {
            var tr = $('tr.row-new').clone(true);
            if (top == -1)
                tr.insertBefore($('tr.activity:nth(0)'));
            else
                tr.insertAfter($(ctrl).closest('tr'));
            tr.addClass('activity').removeClass('row-new').removeClass('d-none');
            resetSequenceNumber();
        }

        function initializeResources()
        {
            var lists = $('.resource');
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
            Swal.fire({ title: "", text: "Are you sure you want to delete?", icon: "warning", showCancelButton: !0, confirmButtonColor: "#34c38f", cancelButtonColor: "#f46a6a", confirmButtonText: "Yes, delete it!" })
				.then(function (t) { if (t.value) removeRow(ctrl); });
        }

        function newVO(url)
        {
            window.location.href = url;
        }

        function showMessage(message, type, cb)
        {
            Swal.fire({ title: "", text: message, icon: type, showCancelButton: 0, confirmButtonColor: "#556ee6" }).then(cb);
        }
        function reloadGrid()
        {
            window.location.reload(true);
        }

        function getDuration()
        {

            var startDate = $('#startDate').val();
            var endDate = $('#endDate').val();
            var days = 0;
            $('#duration').text('');
            if (startDate && endDate)
            {
                startDate = parseDate(startDate);
                endDate = parseDate(endDate);
                days = endDate.getTime() - startDate.getTime();
                // To calculate the no. of days between two dates
                days = days / (1000 * 3600 * 24);
                $('#duration').text(days + 1);
            }

        }

        function parseDate(date)
        {
            var parts = date.split('/');
            date = parts[2] + '/' + parts[1] + '/' + parts[0];
            return new Date(date)
        }

        function updateWorkTitle(owner)
        {
            var option = $(owner).find('option:selected');
            $('#WorkTitle').val(option.data('work-title'));
            $('#discipline').val(option.data('discipline'));
            $('#wbs').val(option.data('wbs'));
        }
        function updateWBS(discipline)
        {
            var d = $(discipline).val();
            var options = $('#wbs option').hide();
            $('#wbs option[data-discipline="' + d.trim() + '"]').show();
            $(options[0]).prop('selected', true).show();
        }
        function printVO()
        {
            $.ajax({
                url: "vo.asmx/Print",
                data: JSON.stringify({ vo: extractModel() }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                showMessage('The Details have been printed successfully.', 'success', reloadGrid);
            }).fail(function ()
            {
                showMessage('Unable to print the Details.', 'error', reloadGrid);
            });
        }
        function approveVO()
        {
            $.ajax({
                url: "vo.asmx/FinalApprove",
                data: JSON.stringify({ vo: extractModel() }),
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=UTF-8'
            }).done(function (d)
            {
                showMessage('The Details have been approved successfully.', 'success', reloadGrid);
            }).fail(function ()
            {
                showMessage('Unable to approve the Details.', 'error', reloadGrid);
            });
        }

		function submitJCS()
		{
			$.ajax({
				url: "jcs.asmx/Submit",
				data: JSON.stringify({ vo: extractModel() }),
				dataType: "json",
				type: "POST",
				contentType: 'application/json; charset=UTF-8'
			}).done(function (d)
			{
				showMessage('The Details have been submitted successfully.', 'success', reloadGrid);
			}).fail(function ()
			{
				showMessage('Unable to submit the Details.', 'error', reloadGrid);
			});
		}

        function enableDetails()
        {
            var type = $('.jcs-type').text();
            var canEdit = $('.jcs-can-edit').text();

            if (type == 'V')
            {
				$('#txtOwnerNo').hide();
                if (canEdit == 'false')
                    $('#ownerNo').prop('disabled', true);
                $('#WorkTitle').prop('disabled', true);
                $('#discipline').prop('disabled', true);
                $('#wbs').prop('disabled', true);
            }
            else if (type == "A")
            {
                $('#ownerNo').hide();
                if (canEdit == 'false')
                {
					$('#txtOwnerNo').prop('disabled', true);
					$('#WorkTitle').prop('disabled', true);
					$('#discipline').prop('disabled', true);
					$('#wbs').prop('disabled', true);
				}
			}
        }
	</script>
</asp:Content>
