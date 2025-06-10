using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
                Response.Redirect("agentlogin.aspx");
            else
            {
                //DataTable dt = ClsAgentTransaction.Agent_Profile(Convert.ToString(Session["UserId"]));

                List<Agent_ProfileResult> lResult = new List<Agent_ProfileResult>();
                lResult = new ClsAgentTransaction().Agent_Profile(Convert.ToString(Session["UserId"]));

                try
                {

                    if (lResult != null && lResult.Count > 0)
                    {
                        //lblFName.Text = Convert.ToString(dt.Rows[0]["Fname"]);
                        //lblLName.Text = Convert.ToString(dt.Rows[0]["Lname"]);
                        //lblEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);

                        lblFName.Text = Convert.ToString(lResult[0].Fname);
                        lblLName.Text = Convert.ToString(lResult[0].Lname);
                        lblEmail.Text = Convert.ToString(lResult[0].Email);



                        //if (Convert.ToString(dt.Rows[0]["Gender"]) == "M")
                        //    lblGender.Text = "Male";
                        //if (Convert.ToString(dt.Rows[0]["Gender"]) == "F")
                        //    lblGender.Text = "Female";

                        if (Convert.ToString(lResult[0].Gender) == "M")
                            lblGender.Text = "Male";
                        if (Convert.ToString(lResult[0].Gender) == "F")
                            lblGender.Text = "Female";

                        lblDOB.Text = Convert.ToDateTime(lResult[0].DOB).ToShortDateString();
                        lbloffadd.Text = Convert.ToString(lResult[0].Address);
                        lblcity.Text = Convert.ToString(lResult[0].City);
                        lblmob.Text = Convert.ToString(lResult[0].Mobile);
                        lblpho.Text = Convert.ToString(lResult[0].LandLine);
                        lblfax.Text = Convert.ToString(lResult[0].Fax);
                        lblPanNO.Text = Convert.ToString(lResult[0].PanNo);
                    }
                }
                finally
                {
                    if (lResult != null)
                    {
                        lResult = null;
                    }
                }
            }
        }
    }
}