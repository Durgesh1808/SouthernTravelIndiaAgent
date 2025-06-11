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
    public partial class agenttour : System.Web.UI.Page
    {
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBooking.Attributes.Add("onclick", "javascript:return validation();");
            if (!IsPostBack)
            {
                DataTable dt = null;
                ClsAdo clsObj = null;
                try
                {
                    clsObj = new ClsAdo();
                    dt = clsObj.fnGetFixedToursActive();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlTourName.DataSource = dt;
                        ddlTourName.DataBind();
                        ddlTourName.Items.Insert(0, "Select");
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
            }
        }

        /// <summary>
        /// /// This method is triggered when the booking button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBooking_Click(object sender, ImageClickEventArgs e)
        {
            string value = ddlTourName.SelectedValue;
            if ((value != "0") && (value != "") && (value != "Select"))
            {
                Session["Comission"] = ClsAgentTransaction.Agent_FixedTourCommission(Convert.ToInt32(value), Convert.ToInt32(Session["AgentLevel"]));
                Response.Redirect("AgentTourBooking.aspx?TourId=" + value);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "SelectTour", "<script>alert('Please selct the tour From the List');</script>");
                Response.Redirect("AgentHomepage.aspx");
            }
        }
        #endregion
    }
}