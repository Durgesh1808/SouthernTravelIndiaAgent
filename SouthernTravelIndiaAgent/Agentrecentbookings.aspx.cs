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
    public partial class Agentrecentbookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtTour = null;
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                dtTour = clsObj.fnFixed_RecentBookings("Agent", Convert.ToString(Session["Agentid"]));
                if (dtTour != null && dtTour.Rows.Count > 0)
                {
                    dgrecentbookings.DataSource = dtTour;
                    dgrecentbookings.DataBind();
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtTour != null)
                {
                    dtTour.Dispose();
                    dtTour = null;
                }
            }

        }
    }
}