using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class Fixedenquiryform : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    Form1.Action = Request.RawUrl;
        //    string pURL = Request.Url.ToString();
        //    divrecaptcha.Attributes.Add("data-sitekey", System.Configuration.ConfigurationManager.AppSettings["GooglereCaptcha_Sitekey"]);
        //    Submit1.Attributes.Add("onclick", "javascript:return fnValidation();");
        //    if (!IsPostBack)
        //    {
        //        if (Request.QueryString["id"] != null)
        //        {
        //            int pTourType = 1;
        //            if (Request.QueryString["TourType"] != null)
        //            {
        //                pTourType = Convert.ToInt32(Request.QueryString["TourType"]);
        //            }
        //            DataTable dtTourName = clsBLL.FixedTour_Code(Convert.ToInt32(DataLib.funClear(Convert.ToString(Request.QueryString["id"]))), pTourType);
        //            if (dtTourName != null && dtTourName.Rows.Count > 0)
        //            {
        //                lblTour.Text = Convert.ToString(dtTourName.Rows[0]["tourname"]);
        //                ViewState["TourName"] = Convert.ToString(dtTourName.Rows[0]["tourname"]);
        //            }
        //            else
        //                ClientScript.RegisterStartupScript(typeof(string), "Nodetails", "<script>alert('Sorry!.., No Details Found')</script>");
        //        }
        //    }
        //}
        //protected void ImageButton1_Click(object sender, EventArgs e)
        //{
        //    Clear();
        //}
        //public bool reCaptcha()
        //{
        //    bool lFlag = false;
        //    //start building recaptch api call
        //    var sb = new System.Text.StringBuilder();
        //    sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");

        //    //our secret key
        //    var secretKey = System.Configuration.ConfigurationManager.AppSettings["GooglereCaptcha_Secretkey"]; //"6LesfBwTAAAAAPKzkHq9ny59cb_BtZa1D6ZLLBGf";
        //    sb.Append(secretKey);

        //    //response from recaptch control
        //    sb.Append("&");
        //    sb.Append("response=");
        //    var reCaptchaResponse = Request["g-recaptcha-response"];
        //    sb.Append(reCaptchaResponse);

        //    //client ip address
        //    //---- This Ip address part is optional. If you donot want to send IP address you can
        //    //---- Skip(Remove below 4 lines)
        //    sb.Append("&");
        //    sb.Append("remoteip=");
        //    var clientIpAddress = GetUserIp();
        //    sb.Append(clientIpAddress);

        //    //make the api call and determine validity
        //    using (var client = new System.Net.WebClient())
        //    {
        //        var uri = sb.ToString();
        //        var json = client.DownloadString(uri);
        //        var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(RecaptchaApiResponse));
        //        var ms = new System.IO.MemoryStream(System.Text.Encoding.Unicode.GetBytes(json));
        //        var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

        //        //--- Check if we are able to call api or not.
        //        if (result == null)
        //        {
        //            MessageLabel.Text = "Captcha was unable to make the api call";
        //        }
        //        else // If Yes
        //        {
        //            //api call contains errors
        //            if (result.ErrorCodes != null)
        //            {
        //                if (result.ErrorCodes.Count > 0)
        //                {
        //                    foreach (var error in result.ErrorCodes)
        //                    {
        //                        MessageLabel.Text = "Captcha is required.";
        //                    }
        //                }
        //            }
        //            else //api does not contain errors
        //            {
        //                if (!result.Success) //captcha was unsuccessful for some reason
        //                {
        //                    MessageLabel.Text = "Captcha did not pass, please try again.";
        //                }
        //                else //---- If successfully verified. Do your rest of logic.
        //                {
        //                    MessageLabel.Text = "Captcha cleared ";
        //                    lFlag = true;
        //                }
        //            }

        //        }

        //    }
        //    return lFlag;
        //}
        //[System.Runtime.Serialization.DataContract]
        //public class RecaptchaApiResponse
        //{
        //    [System.Runtime.Serialization.DataMember(Name = "success")]
        //    public bool Success;

        //    [System.Runtime.Serialization.DataMember(Name = "error-codes")]
        //    public System.Collections.Generic.List<string> ErrorCodes;
        //}

        ////--- To get user IP(Optional)
        //private string GetUserIp()
        //{
        //    var visitorsIpAddr = string.Empty;

        //    if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        //    {
        //        visitorsIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    }
        //    else if (!string.IsNullOrEmpty(Request.UserHostAddress))
        //    {
        //        visitorsIpAddr = Request.UserHostAddress;
        //    }

        //    return visitorsIpAddr;
        //}
        //private void Clear()
        //{
        //    S_name.Value = "";
        //    S_email.Value = ""; ;
        //    S_phone.Value = "";
        //    S_fax.Value = "";
        //    S_streetaddress.Value = "";
        //    S_city.Value = "";
        //    S_pin.Value = "";
        //    S_country.Value = "";
        //    ddlAdults.SelectedIndex = -1;
        //    ddlChilds.SelectedIndex = -1;
        //}
        //protected void Submit1_Click(object sender, EventArgs e)
        //{
        //    bool lFlag = reCaptcha();
        //    if (lFlag)
        //    {
        //        if (Request.QueryString["id"] != null)
        //        {
        //            string refno = DataLib.Code("Fixed");
        //            string[] jdate = this.txtarrival.Value.ToString().Replace("'", "''").Split('/');
        //            DateTime sArrivalDate = DateTime.Today.Date;
        //            if (jdate.Length > 1)
        //            {
        //                //sArrivalDate = Convert.ToDateTime(jdate[1] + "/" + jdate[0] + "/" + jdate[2]);
        //                sArrivalDate = new DateTime(Convert.ToInt32(jdate[2]), Convert.ToInt32(jdate[1]),
        //                    Convert.ToInt32(jdate[0]));
        //            }
        //            string sName = DataLib.funClear(this.S_name.Value.ToString().Replace("'", "''"));
        //            string sEmail = this.S_email.Value.ToString().Replace("'", "''").Replace("--", "");
        //            string sPhone = DataLib.funClear(this.S_phone.Value.ToString().Replace("'", "''"));
        //            string sFax = DataLib.funClear(this.S_fax.Value.ToString().Replace("'", "''"));
        //            string sStreet = DataLib.funClear(this.S_streetaddress.Value.ToString().Replace("'", "''"));
        //            string sCity = DataLib.funClear(this.S_city.Value.ToString().Replace("'", "''"));
        //            string sZip = DataLib.funClear(this.S_pin.Value.ToString().Replace("'", "''"));
        //            string sCountry = DataLib.funClear(this.S_country.Value.ToString().Replace("'", "''"));
        //            int strAdults = Convert.ToInt32(ddlAdults.SelectedValue);
        //            int strChild = Convert.ToInt32(ddlChilds.SelectedValue);

        //            int Val = clsBLL.EnquiryTable_Entry(Convert.ToString(ViewState["TourName"]), sName, sEmail, sPhone, sFax, sStreet, sCity,
        //                sZip, sCountry, strAdults, strChild, sArrivalDate, sArrivalDate, "Fixed", refno,
        //                "");
        //            if ((Val == 1) || (Val == 2))
        //            {
        //                tblEnquiry.Visible = false;
        //                this.MessageLabel.Text = "";

        //            }
        //            else
        //            {
        //                // ================================= Mail Format Start Here ==============================
        //                string mailbody = "";
        //                mailbody = mailbody + "<br><table width='60%' border='1' cellspacing='1' cellpadding='2' align='center'>";
        //                mailbody = mailbody + "<tr><td bgcolor='#CCCCCC' colspan='2'><font face='Verdana' size='-1'><b>Enquiry Details : </b>" + refno + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Journey Date</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sArrivalDate + "  </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>TourName</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + Convert.ToString(ViewState["TourName"]) + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Name</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sName + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Email</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sEmail + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Phone / Mobile</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sPhone + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Fax</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sFax + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Street Address</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sStreet + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>City / State</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sCity + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Zip / Postal Code</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sZip + " </font></td>";
        //                mailbody = mailbody + "</tr>";
        //                mailbody = mailbody + "<tr bgcolor='#FFFFFF'>";
        //                mailbody = mailbody + "<td><font size='-1' face='Verdana'>Country</font></td>";
        //                mailbody = mailbody + "<td><font face='Verdana' size='-1'>" + sCountry + " </font></td>";
        //                mailbody = mailbody + "</tr>";

        //                mailbody = mailbody + "</table>";
        //                // ================================= Mail Format End Here ==============================

        //                ClsCommon.sendmail(ConfigurationSettings.AppSettings["Enquiry"].ToString(), "", "", sEmail,
        //                    refno + ": " + Convert.ToString(ViewState["TourName"]) + " Fixed Tour Enquiry details", mailbody, "");
        //                //tblEnquiry.Visible = false;
        //                lblMsg.Text = "Thanks for your interest. Our executive will get back you very soon.";
        //                ClientScript.RegisterStartupScript(GetType(), "response", "<script>alert('Thanks for your interest. Our executive will get back you very soon...');</script>");
        //                Clear();

        //            }
        //        }
        //        else
        //            Response.Redirect("index.aspx");
        //    }

        //}
    }
}