using SouthernTravelIndiaAgent.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class agenthomepage : System.Web.UI.Page
    {
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Attributes.Add("onclick", "javascript:return validation();");
            txtAmount.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
            if (Session["AgentId"] != null)
            {
                string balance = Convert.ToString(ClsAgentTransaction.Agent_Availablebalance(Convert.ToInt32(Session["AgentId"])).Rows[0][0]);
                if (balance == "0" || balance == null || balance == "")
                {
                    lblBalance.Text = "Wallet Empty";
                    Session["Balance"] = "0";
                }
                else
                {
                    Session["Balance"] = balance;
                    lblBalance.Text = "Rs." + balance;
                }
            }
            else
                Response.Redirect("agentlogin.aspx");
        }
        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            string strAmount = txtAmount.Text.Replace("'", "''").Trim();
            Response.Redirect("agentaddfunds.aspx?amt=" + strAmount);
        }
        #endregion
    }
}