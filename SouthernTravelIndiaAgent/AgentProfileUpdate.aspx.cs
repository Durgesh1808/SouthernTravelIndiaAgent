using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentProfileUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtphone.Attributes.Add("onkeydown", "javascript:return checknumber();");
            txtmob.Attributes.Add("onkeydown", "javascript:return checknumber();");
            txtFax.Attributes.Add("onkeydown", "javascript:return checknumber();");
            if (Session["AgentId"] == null)
                Response.Redirect("AgentLogin.aspx");
            btnUpdate.Attributes.Add("OnClick", "javascript:return CheckValidation();");
            if (!Page.IsPostBack)
            {
                fillDOB();
                List<Agent_ProfileResult> lResult = new List<Agent_ProfileResult>();
                // DataTable dt = ClsAgentTransaction.Agent_Profile(Convert.ToString(Session["UserId"]));
                lResult = new ClsAgentTransaction().Agent_Profile(Convert.ToString(Session["UserId"]));

                try
                {
                    if (lResult.Count != null && lResult.Count > 0)
                    {
                        //txtFname.Value = Convert.ToString(dt.Rows[0]["FName"]);
                        //txtLname.Value = Convert.ToString(dt.Rows[0]["LName"]);
                        //txtEmail.Value = Convert.ToString(dt.Rows[0]["Email"]);

                        txtFname.Value = Convert.ToString(lResult[0].Fname);
                        txtLname.Value = Convert.ToString(lResult[0].Lname);
                        txtEmail.Value = Convert.ToString(lResult[0].Email);

                        //if (Convert.ToString(dt.Rows[0]["Gender"]) == "M")
                        //    rdoMale.Checked = true;
                        //if (Convert.ToString(dt.Rows[0]["Gender"]) == "F")
                        //    rdoFemale.Checked = true;

                        if (Convert.ToString(lResult[0].Gender) == "M")
                            rdoMale.Checked = true;
                        if (Convert.ToString(lResult[0].Gender) == "F")
                            rdoFemale.Checked = true;


                        //string[] strDOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString().Trim()).ToShortDateString().Split('/');
                        string[] strDOB = Convert.ToDateTime(lResult[0].DOB.ToString().Trim()).ToShortDateString().Split('/');

                        ddlYear.Items.FindByText(strDOB[2]).Selected = true;
                        //ddlMonth.Items.FindByValue(strDOB[0]).Selected = true;

                        ddlMonth.Items.FindByValue(Convert.ToInt32(strDOB[0]).ToString()).Selected = true;

                        //ddlDay.Items.FindByText(strDOB[1]).Selected = true;

                        ddlDay.Items.FindByText(Convert.ToInt32(strDOB[1]).ToString()).Selected = true;


                        //txtadd.Value = Convert.ToString(dt.Rows[0]["Address"]);
                        //txtcity.Value = Convert.ToString(dt.Rows[0]["City"]);
                        //txtmob.Value = Convert.ToString(dt.Rows[0]["Mobile"]);
                        //txtphone.Value = Convert.ToString(dt.Rows[0]["LandLine"]);
                        //txtFax.Value = Convert.ToString(dt.Rows[0]["Fax"]);

                        txtadd.Value = Convert.ToString(lResult[0].Address);
                        txtcity.Value = Convert.ToString(lResult[0].City);
                        txtmob.Value = Convert.ToString(lResult[0].Mobile);
                        txtphone.Value = Convert.ToString(lResult[0].LandLine);
                        txtFax.Value = Convert.ToString(lResult[0].Fax);
                        txtPanNo.Text = Convert.ToString(lResult[0].PanNo);
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


        /// <summary>
        /// /// This method fills the date of birth dropdowns with day, month, and year options.
        /// </summary>
        private void fillDOB()
        {
            Globals.LoadDropDownDay(ref ddlDay, 1, true);
            Globals.LoadDropDownMonth(ref ddlMonth, 1, true);
            FillddlYear();
        }


        /// <summary>
        /// /// This method fills the year dropdown with the last 120 years, starting from the current year.
        /// </summary>
        private void FillddlYear()
        {
            int j = Convert.ToInt32(System.DateTime.Now.Year.ToString());
            for (int i = 0; i < 120; i++)
            {
                if (i == 0)
                    ddlYear.Items.Insert(i, new ListItem("YYYY", "0"));
                else
                {
                    ddlYear.Items.Insert(i, new ListItem(j.ToString(), j.ToString()));
                    j = j - 1;
                }
            }
        }
        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string sMemberFname = DataLib.funClear(txtFname.Value.Trim());
            string sMemberLname = DataLib.funClear(txtLname.Value.Trim());
            string sDay = ddlDay.SelectedValue;
            string sMonth = ddlMonth.SelectedValue;
            string sYear = ddlYear.SelectedValue;
            string sMemberDOB = sMonth + '/' + sDay + '/' + sYear;
            string sMemberEmail = txtEmail.Value.Trim().Replace("'", "''").Replace("</script>", "").Replace("--", "");
            char sMemberGender = rdoMale.Checked ? 'M' : 'F';
            string smembeoffadd = DataLib.funClear(txtadd.Value.Trim());
            string smembercity = DataLib.funClear(txtcity.Value.Trim());
            string stxtmob = DataLib.funClear(txtmob.Value.Trim());
            string stxtphone = DataLib.funClear(txtphone.Value.Trim());
            string stxtFax = DataLib.funClear(txtFax.Value.Trim());

            int lResult = ClsAgentTransaction.Agent_UpdateProfile(Convert.ToString(Session["UserId"]), sMemberEmail, sMemberFname, sMemberLname,
                Convert.ToDateTime(sMemberDOB), sMemberGender, smembeoffadd, smembercity, stxtmob, stxtphone, stxtFax, txtPanNo.Text);
            if (lResult == 2)
                ClientScript.RegisterStartupScript(typeof(string), "onerror", "<script>alert('This Email id Already exists for another user.');</script>");
            else if (lResult == 1)
                ClientScript.RegisterStartupScript(typeof(string), "onstartup1", "<script>alert('Your Profile Updated Successfully.');window.location.href='agenthomepage.aspx'</script>");
        }
    }
}