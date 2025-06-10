using SouthernTravelIndiaAgent.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.UserControls
{
    public partial class UcAgentHeader : System.Web.UI.UserControl
    {
        #region "Member Variable(s)"
        protected string sBalance;
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                string balance = Convert.ToString(ClsAgentTransaction.Agent_Availablebalance(Convert.ToInt32(Session["AgentId"])).Rows[0][0]);
                if (balance == "0" || balance == null || balance == "")
                {
                    sBalance = "Wallet Empty";
                    Session["Balance"] = "0";
                }
                else
                {
                    Session["Balance"] = balance;
                    sBalance = "Rs." + balance;
                }
                if (!IsPostBack)
                {
                    if (Session["BranchId"] != null)
                    {
                        string lUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                        if (lUrl.ToLower().Contains("agenttourbooking.aspx")
                            || lUrl.ToLower().Contains("agentsplpackagetours.aspx")
                            || lUrl.ToLower().Contains("groupbookingrequestform.aspx")
                            || lUrl.ToLower().Contains("agentcarmultiple.aspx")
                            || lUrl.ToLower().Contains("agenthotelbooking.aspx")
                            || lUrl.ToLower().Contains("agentspecialseasontour.aspx"))
                        {
                            String lAgentName = "";
                            if (Session["AgentFname"] != null)
                            {
                                lAgentName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "if(confirm('This is " + lAgentName + " Login, are you sure you want to continue with Agent login ?')){}else{window.open('../Branch/BranchTempLogin.aspx?BranchUserID=" + Convert.ToString(Session["BranchId"]) + "','_self');}", true);
                        }
                    }
                }
            }

        }
        #endregion
        protected void lbtn_Click(object sender, EventArgs e)
        {
            if (Session["BranchId"] != null)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "if(confirm('Are you sure to go back into branch ?')){window.open('../Branch/BranchTempLogin.aspx?BranchUserID=" + Convert.ToString(Session["BranchId"]) + "','_self');}else{}", true);
                //Response.Redirect("~/Branch/BranchTempLogin.aspx?BranchUserID=" + Convert.ToString(Session["BranchId"]));
            }
        }
    }
}