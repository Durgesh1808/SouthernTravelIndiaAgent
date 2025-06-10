using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    using SouthernTravelIndiaAgent.BAL;
    using SouthernTravelIndiaAgent.Common;
    using SouthernTravelIndiaAgent.DAL;
    using System;
    using System.Configuration;

    public partial class AgentEnquiry : System.Web.UI.Page
    {
        protected string dtDOB;
        protected string dtDOB1;
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Attributes.Add("onsubmit", "return validate();");
            this.txtfax.Attributes.Add("onkeydown", "return isNumberKey(event);");
            this.txtmobile.Attributes.Add("onkeydown", "return isNumberKey(event);");
            this.txtphone.Attributes.Add("onkeydown", "return isNumberKey(event);");
            this.txtpin.Attributes.Add("onkeydown", "return isNumberKey(event);");
            this.txtlastname.Attributes.Add("onkeydown", "return CheckOnlyCharacter(event);");
            this.txtfirstname.Attributes.Add("onkeydown", "return CheckOnlyCharacter(event);");
            this.txtcity.Attributes.Add("onkeydown", "return CheckOnlyCharacter(event);");
            dtDOB = txtdob.Value.ToString();
            if (dtDOB == "")
            {
                dtDOB1 = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(-0).ToShortDateString();
                txtdob.Value = ClsCommon.mmddyy2ddmmyy(dtDOB1);
            }
            else
                dtDOB1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(dtDOB)).ToShortDateString();
        }
        private void SnedMail(string refno)
        {
            try
            {
                string mailbody = "";
                mailbody += "<font size='3' face='Arial, Helvetica, sans-serif'><strong>Agent Details <br></strong></font>";
                mailbody += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td width='25%'><font size='2' face='Arial, Helvetica, sans-serif'>Name</font></td>";
                mailbody += "<td width='75%'><font size='2' face='Arial, Helvetica, sans-serif'>" + txtfirstname.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>Lastname </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtlastname.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td>  <font size='2' face='Arial, Helvetica, sans-serif'>DOB</font> <br></td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtdob.Value + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td>  <font size='2' face='Arial, Helvetica, sans-serif'>Address </font> <br></td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtaddress.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td>  <font size='2' face='Arial, Helvetica, sans-serif'>City </font> <br></td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtcity.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td>  <font size='2' face='Arial, Helvetica, sans-serif'>Country </font> <br></td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + ddlcountry.Value + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>Phone No </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtphone.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>Mobile No </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtmobile.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>E-mail </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtemail.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>AuthorityMember </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + ddlauthority.Value + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFFFE1'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>Pan No </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + txtPanNo.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "<tr bgcolor='#FFDFFF'>";
                mailbody += "<td> <font size='2' face='Arial, Helvetica, sans-serif'>Description </font>  </td>";
                mailbody += "<td><font size='2' face='Arial, Helvetica, sans-serif'>" + TextBox1.Text + "</font></td>";
                mailbody += "</tr>";
                mailbody += "</table>";

                string Toemail = ConfigurationManager.AppSettings["agentRegistrationMails"].ToString();
                string enquireyEmail = ConfigurationManager.AppSettings["EnquiryEmailIn"].ToString();
                //ClsCommon.sendmail(Toemail, "", "", txtemail.Text, refno + ": Agent Registration Request", mailbody.ToString());
                ClsCommon.sendmail(Toemail, "", "", enquireyEmail, refno + ": Agent Registration Request", mailbody.ToString(), txtemail.Text);

            }
            catch (Exception ex)
            {
                Response.Write("<!-- " + ex.ToString() + " -->");
            }
        }
        private void clear()
        {
            this.RadioButtonList2.SelectedIndex = -1;
            this.ddlauthority.SelectedIndex = -1;
            this.ddlstate.SelectedIndex = -1;
            this.ddlcountry.SelectedIndex = -1;
            this.MessageLabel.Text = "";
            Globals.ClearControls(this);
            Random r = new Random();
            imgCaptcha.ImageUrl = "../JpegImage.aspx?refresh=" + r.Next(1, 2000).ToString();
        }
        protected void SendRequest_Click1(object sender, EventArgs e)
        {
            string valid = "false";
            if ((this.Session["CaptchaImageText"] != null) && (this.txtCaptchImage.Text.ToString() == this.Session["CaptchaImageText"].ToString()))
                valid = "true";
            if (valid == "true")
            {
                string refno = DataLib.Code("AGTENQ");
                int Val = clsBLL.EnquiryTable_Entry("Agent Details:" + refno, DataLib.funClear(txtfirstname.Text) + " " + DataLib.funClear(txtlastname.Text), txtemail.Text.Replace("'", "''").Replace("--", ""),
                    DataLib.funClear(txtmobile.Text), "", DataLib.funClear(txtaddress.Text), DataLib.funClear(txtcity.Text), "0", ddlcountry.Value.ToString(),
                    0, 0, DateTime.Today.Date, DateTime.Today.Date, "AGTENQ", refno, DataLib.funClear(txtCaptchImage.Text.ToString()), txtPanNo.Text);
                if ((Val == 1) || (Val == 2))
                {
                    clear();
                    this.MessageLabel.Text = "";
                    this.txtCaptchImage.Text = "";
                    Random r = new Random();
                    imgCaptcha.ImageUrl = "JpegImage.aspx?refresh=" + r.Next(1, 2000).ToString();
                }
                else
                {
                    SnedMail(refno);
                    ClientScript.RegisterStartupScript(typeof(string), "status", "<script>alert('Request send Successfully ');</script>");
                    clear();
                    Response.Redirect("agentlogin.aspx");
                }
            }
            else
            {
                this.MessageLabel.Text = "Incorrect Code, try again.";
                this.txtCaptchImage.Text = "";
                Random r = new Random();
                imgCaptcha.ImageUrl = "../JpegImage.aspx?refresh=" + r.Next(1, 2000).ToString();
            }
        }
    }

}