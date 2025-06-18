using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentSeatRequestForm : System.Web.UI.Page
    {
        protected string dtdepart, dtdepart1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtgroupleadername.Attributes.Add("onkeydown", "javascript:return CheckOnlyCharacter();");
                txtadults.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                txtchild.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                txtcity.Attributes.Add("onkeydown", "javascript:return CheckOnlyCharacter();");
                txtaddress.Attributes.Add("onkeydown", "javascript:return chk();");
                txtPhone.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                txtMobile.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                txtZip.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                if ((dtdepart == "") || (dtdepart == null))
                {
                    dtdepart1 = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(-0).ToShortDateString();
                    departure.Value = ClsCommon.mmddyy2ddmmyy(dtdepart1);
                }
                else
                    dtdepart1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(dtdepart1)).ToShortDateString();
            }
            txttourname.Value = Request.QueryString["TourName"].ToString();
            Submit.Attributes.Add("onclick", "return validate();");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string valid = "false";
            if ((this.Session["CaptchaImageText"] != null) && (this.txtCaptchImage.Text.ToString() == this.Session["CaptchaImageText"].ToString()))
                valid = "true";
            if (valid == "true")
            {
                DateTime strJourneyDate = DateTime.Parse(departure.Value.Split('/').GetValue(1) + "/" + departure.Value.Split('/').GetValue(0) + "/" + departure.Value.Split('/').GetValue(2));
                string refno = DataLib.Code("SeatReq");
                int Val = clsBLL.EnquiryTable_Entry("Request for Seats:" + DataLib.funClear(txttourname.Value), DataLib.funClear(txtgroupleadername.Value),
                    txtMail.Value.Replace("'", "''").Replace("--", "").Replace("<script>", ""), DataLib.funClear(txtMobile.Value), "0", DataLib.funClear(txtaddress.Value),
                     DataLib.funClear(txtcity.Value), "0", "", Convert.ToInt32(DataLib.funClear(txtadults.Value)), Convert.ToInt32(DataLib.funClear(txtchild.Value)),
                     strJourneyDate, strJourneyDate, "SeatReq", refno, DataLib.funClear(txtCaptchImage.Text.ToString()));

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
                    ClsCommon.SeatRequest(refno, DataLib.funClear(txttourname.Value), DataLib.funClear(txtgroupleadername.Value), strJourneyDate,
                                        DataLib.funClear(txtadults.Value), DataLib.funClear(txtchild.Value), Rdbbustype.SelectedValue, DataLib.funClear(txtaddress.Value),
                                        DataLib.funClear(txtcity.Value), ddlState.SelectedValue, DataLib.funClear(txtPhone.Value), DataLib.funClear(txtMobile.Value),
                                        txtMail.Value.Replace("'", "''").Replace("--", "").Replace("<script>", ""));
                    clear();
                    this.MessageLabel.Text = "";
                    this.txtCaptchImage.Text = "";
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
        public void clear()
        {
            Globals.ClearControls(this);
            Random r = new Random();
            imgCaptcha.ImageUrl = "JpegImage.aspx?refresh=" + r.Next(1, 2000).ToString();
        }
    }
}