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
    public partial class AgentCarbooking : System.Web.UI.Page
    {

        #region "Member Variable(s)"
        ClsAdo clsObj = null;
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                //Response.Redirect("sessionexpire.aspx");
                Response.Redirect("agentlogin.aspx");
            }
            if (!IsPostBack)
                bindPage();

        }
        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            string citycode = ddlcity.SelectedValue;
            string transferid = hidT.Value;
            string ac = "0";
            // if (radAC1.Checked)
            ac = "1";
            //if (Radio1.Checked) 


            string stranscity = hidTcity2.Value;
            string stransfer = hidT.Value;
            string stranssub = hidTsub1.Value;
            if (rdolocal.Checked == true)
            {

                Response.Redirect("agentCarMultiple.aspx?cityid=" + stranscity + "&tid=" + stransfer + "&subtransfer=" + stranssub);
            }
            if ((rdoout.Checked == true) && (rdofixed.Checked == true))
            {
                Response.Redirect("AgentCarFixedTours.aspx?cityid=" + citycode + "&tid=" + transferid + "&ac=" + ac);
            }
            if ((rdoout.Checked == true) && (rdotailer.Checked == true) && (rdokm.Checked == true))
            {
                Response.Redirect("Agent_CarPerKMBooking.aspx?cityid=" + citycode);
            }
            if ((rdoout.Checked == true) && (rdotailer.Checked == true) && (rdolumpsum.Checked == true))
            {
                Response.Redirect("Agent_CarLumpsumBooking.aspx?cityid=" + citycode + "");
            }
        }
        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
        }
        protected void ddlcity_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        #endregion
        #region "Method(s)"
        private void bindPage()
        {
            fillCities();
        }
        private void fillCities()
        {
            /*DataTable dtCities = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select cityid,cityname from City_tbl where lower(activated)='y' ");*/
            clsObj = new ClsAdo();
            DataTable dtCities = clsObj.fnGetActive_Cat_City();
            try
            {
                if (dtCities.Rows.Count > 0)
                {
                    ddlcity.DataSource = dtCities;
                    ddlcity.DataTextField = "cityname";
                    ddlcity.DataValueField = "cityid";
                    ddlcity.DataBind();
                }
                ddlcity.Items.Insert(0, new ListItem("Select", "0", true));
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtCities != null)
                {
                    dtCities.Dispose();
                    dtCities = null;

                }
            }
        }
        #endregion
    }
}