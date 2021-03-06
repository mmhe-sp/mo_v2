using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls
{
    public partial class DWC : UserControl
    {
        public string Today { get; set; }
        public string Tomorrow { get; set; }
        public string _Today { get; set; }
        public string _Tomorrow { get; set; }
        public IEnumerable<IGrouping<string, DWCDetails>> Details { get; set; }
        public List<Option> Resources { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Today = DateTime.Now.ToString("dd-MMM-yy");
            _Today = DateTime.Now.ToString("yyyy-MM-dd");
            Tomorrow = DateTime.Now.AddDays(1).ToString("dd-MMM-yy");
            _Tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var user = (Page as BasePage).LoggedInUser;
            var details = new DWCRepository().GetAll(user.ProjectId, user.Id);
            var progress = new List<DWCDetails>();
            DWCActivity activity;
            foreach (var jcs in details.GroupBy(d => d.JCSID))
            {

                if (jcs.Any(d => d.ActivityID.HasValue))
                {
                    activity = jcs.First();
                    progress.Add(new DWCDetails
                    {
                        Work_Title = activity.Work_Title,
                        EndDate = activity.EndDate,
                        JCSID = activity.JCSID,
                        JSLStatus = activity.JSLStatus,
                        lType = activity.lType,
                        MyGroup = activity.MyGroup,
                        OwnerNo = activity.OwnerNo,
                        StartDate = activity.StartDate,
                        Type = activity.Type,
                        WBS = activity.WBS,
                        Today = activity.Today,
                        Tomorrow = activity.Tomorrow,
                        Remarks = activity.Remarks,
                        Discipline = activity.Discipline,
                        ActivityProgress = jcs.Where(d => d.ActivityID.HasValue).Select(d => new DWCProgress
                        {
                            ActivityID = d.ActivityID.Value,
                            ActivityTitle = d.ActivityTitle,
                            ActivityType = d.ActivityType,
                            Subscontractor = d.Subscontractor,
                            SubscontractorId = d.SubContractorID,
                            Completion = d.CompletionPer,
                            IWRStatus = d.IWRStatus,
                            ActivityDiscipline = d.ActivityDiscipline,
                            Today = d.ActivityToday,
                            Tomorrow = d.ActivityTomorrow,
                            Remarks = d.ActivityRemarks,
                            SubContractorRemarks = d.SubContractorRemarks,
                            Status = d.Status
                        }).ToList()
                    });
                }
            }

            Details = progress.GroupBy(d => d.MyGroup).OrderBy(d => d.Key).ToList();

            Resources = details.GroupBy(d => new { d.Subscontractor, d.SubContractorID })
                .Select(g => new Option { Text = g.Key.Subscontractor, Value = g.Key.SubContractorID })
                .Where(d => string.IsNullOrWhiteSpace(d.Text) == false).OrderBy(d => d.Text).ToList();

        }
    }
}
