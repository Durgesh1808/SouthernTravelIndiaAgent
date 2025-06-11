using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentTouratGlance : System.Web.UI.Page
    {
        protected string dtdate, dtdate1, strDate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((dtdate == "") || (dtdate == null))
                {
                    dtdate1 = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(1).ToShortDateString();
                    date.Value = ClsCommon.mmddyy2ddmmyy(dtdate1);
                }
                else
                    dtdate1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(dtdate)).ToShortDateString();
            }
            if (!IsPostBack)
            {
                DataTable dt = null;
                ClsAdo clsObj = null;
                try
                {
                    clsObj = new ClsAdo();
                    dt = clsObj.fnFixed_OriginBranches();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlBranchName.DataSource = dt;
                        ddlBranchName.DataBind();
                        ddlBranchName.Items.Insert(0, new ListItem("-----All-----", "0"));
                    }
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                }
                fillgrid();
            }
        }

        /// <summary>
        /// /// This method fills the grid with tours at a glance based on the selected date and branch.
        /// </summary>
        public void fillgrid()
        {
            string[] dat = date.Value.Split('/');
            if (dat[1].Trim().Length == 1) dat[1] = ("0" + dat[1]);
            if (dat[0].Trim().Length == 1) dat[0] = ("0" + dat[0]);
            strDate = dat[1] + "/" + dat[0] + "/" + dat[2];
            string bcode;
            if (ddlBranchName.SelectedIndex == 0)
                bcode = "";
            else
                bcode = ddlBranchName.SelectedValue;
            DataTable dtgd = ClsCommon.Fixed_ToursAtAGlance(dat[0], dat[1], dat[2], bcode);
            try
            {
                dgglance.DataSource = dtgd;
                Globals.CheckData(ref dgglance, dtgd, ref lblMsg);
            }
            finally
            {
                if (dtgd != null)
                {
                    dtgd.Dispose();
                    dtgd = null;
                }
            }
        }


        /// <summary>
        /// /// This method handles the page index change event for the DataGrid control.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dgglance_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgglance.CurrentPageIndex = e.NewPageIndex;
            fillgrid();
        }


        /// <summary>
        /// /// This method handles the click event of the "Go" button to refresh the grid with the selected date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btngo_Click(object sender, EventArgs e)
        {
            fillgrid();
        }
    }
}