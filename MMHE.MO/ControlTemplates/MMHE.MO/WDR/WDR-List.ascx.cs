using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;
using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.ControlTemplates.MMHE.MO.WDR
{
    public partial class WDR_List : UserControl
    {
        public List<Option> Resources { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var user = (Page as BasePage).LoggedInUser;

                var sunconGrp = new WDRRepository().GetSubcontractor(user.Id.ToString());
                if (sunconGrp != null && sunconGrp.Rows.Count != 0)
                {
                    subContractor.Items.Clear();
                    subContractor.Items.Add(new ListItem("All", "All"));
                    foreach (DataRow row in sunconGrp.Rows)
                    {
                        //Resources.Add(new Option { Text = row.Field<string>("MyGroup"), Value = row.Field<string>("MyGroup") });
                        subContractor.Items.Add(new ListItem(row.Field<string>("MyGroup"), row.Field<string>("VCode")));
                    }
                }
                rWDRSubmission.DataSource = new WDRRepository().GetAll(user.Id.ToString());
                rWDRSubmission.DataBind();
            }            
        }

        protected void subContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;
            var listofSubcon = new WDRRepository().GetAll(user.Id.ToString(), subContractor.SelectedValue =="All"? string.Empty:subContractor.SelectedValue);

            rWDRSubmission.DataSource = listofSubcon;
            rWDRSubmission.DataBind();

        }
    }
}
