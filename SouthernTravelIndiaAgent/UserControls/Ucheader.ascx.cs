using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.UserControls
{
    public partial class Ucheader : System.Web.UI.UserControl
    {
        #region "Member Variable(s)"
        Current_Section pvMainSection;
        private string pvYears = "0";
        #endregion
        #region "Property(s)"
        public Current_Section fldMainSection
        {
            get
            {
                return pvMainSection;
            }
            set
            {
                pvMainSection = value;
                SetMainSection();
            }
        }
        public string fldYears
        {
            get
            {
                pvYears = (DateTime.Now.Year - 1970).ToString();
                return pvYears;
            }
        }
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                System.Uri currentUrl = System.Web.HttpContext.Current.Request.Url;

                if (!currentUrl.AbsoluteUri.Contains("www.") && !currentUrl.AbsoluteUri.Contains("localhost"))
                {
                    string NewUrl = currentUrl.AbsoluteUri.ToString();
                    ////NewUrl = NewUrl.Replace("http://", "https://www.");
                    //NewUrl = NewUrl.Replace("http://", "https://");
                    currentUrl = new Uri(NewUrl);
                    System.UriBuilder secureUrlBuilder = new UriBuilder(currentUrl);
                    secureUrlBuilder.Scheme = Uri.UriSchemeHttps;
                    secureUrlBuilder.Port = -1;
                    System.Web.HttpContext.Current.Response.Redirect(secureUrlBuilder.Uri.ToString(), false);


                }
            }
            catch { }

            if (!IsPostBack)
            {
                SetMainSection();
            }
            btnFaceBookRet.Attributes["style"] = "visibility: hidden";
        }
        #endregion
        #region "Method(s)"
        private void SetMainSection()
        {
            MainMFD.Attributes.Add("class", "");
            MainMHP.Attributes.Add("class", "");
            MainMINT.Attributes.Add("class", "");
            MainMHTL.Attributes.Add("class", "");
            MainMCR.Attributes.Add("class", "");
            MainMFLIGHT.Attributes.Add("class", "");
            MainMLLTC.Attributes.Add("class", "");
            MainENQRY.Attributes.Add("class", "");
            //MainMCONTACTUS.Attributes.Add("class", "");
            switch (fldMainSection)
            {
                case Current_Section.FIXED_DEPARTURE:
                    MainMFD.Attributes.Add("class", "active");
                    break;
                case Current_Section.HOLIDAY_PACKAGE_CAR:
                    MainMHP.Attributes.Add("class", "active");
                    break;
                case Current_Section.HOLIDAY_PACKAGE_INTERNATIONAL:
                    MainMINT.Attributes.Add("class", "active");
                    break;
                case Current_Section.HOLIDAY_PACKAGE_CRUISE:
                    MainMHP.Attributes.Add("class", "active");
                    break;
                case Current_Section.CAR_COACH_RENTAL:
                    MainMCR.Attributes.Add("class", "active");
                    break;
                case Current_Section.HOTEL_DELHI:
                    MainMHTL.Attributes.Add("class", "active");
                    break;
                case Current_Section.HOTEL_IN_INDIA:
                    MainMHTL.Attributes.Add("class", "active");
                    break;
                case Current_Section.DOMESTIC_FLIGHT:
                    MainMFLIGHT.Attributes.Add("class", "active");
                    break;
                case Current_Section.CONTACT_US:
                    //MainMCONTACTUS.Attributes.Add("class", "active");
                    break;
                case Current_Section.LTC_LFC_TOUR:
                    MainMLLTC.Attributes.Add("class", "active");
                    break;
                case Current_Section.ENQUIRY:
                    MainENQRY.Attributes.Add("class", "active");
                    break;
            }
        }
        #endregion
        #region "FaceBook"
        protected void btnFaceBookRet_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Customerregistration.aspx?fb=y");
            //Response.Redirect("Customerregistration.aspx?fb=y");

            // ClsCommon.ShowAlert(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);

            ClsAdo pClsObj = null;
            List<GetFbDetail_spResult> lResult = null;
            try
            {
                string email = Convert.ToString(Session["fc_Email"]);
                string fbID = Convert.ToString(Session["fc_Id"]);

                lResult = new List<GetFbDetail_spResult>();
                pClsObj = new ClsAdo();
                lResult = pClsObj.fnGetFaceBookDetail(email, fbID);

                if (lResult.Count > 0)
                {
                    Session["custrowid"] = Convert.ToString(lResult[0].RowId);
                }

                string pURL = Request.Url.ToString().ToLower();


                if ((pURL.Contains("TourBooking.aspx".ToLower()))
                   && Session["Panel2Step"] == null)
                {
                    if (lResult.Count > 0)
                    {
                        Response.Redirect("Customerprofile.aspx?MM=1");
                    }
                    else
                    {
                        Response.Redirect("Customerregistration.aspx?fb=y");
                    }
                }
                else if ((pURL.Contains("TourBooking.aspx".ToLower()))
                   && Session["Panel2Step"] != null)
                {
                    //Response.Redirect(Request.RawUrl.ToString());

                    Page.RegisterStartupScript("FireOkOfPage", "<script language='javascript'>FireFacebookButtonOnFixed();</script>");
                }
                else if (pURL.Contains("BookSpecialTour.aspx".ToLower()))
                {
                    // Server.Transfer(Request.RawUrl.ToString() + "&fb=y",true);
                    //Server.Transfer(Request.RawUrl.ToString(),true);
                    //Response.Redirect(Request.RawUrl.ToString())     
                    // Session["FromFB"] = "Y";
                    //Page_Load(sender,e); 

                    // Form1.Action = Request.RawUrl;
                    //this.Page.GetType().InvokeMember("DisplayMessage", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] {btnFaceBookRet});

                    //Button  btnlbl = this.Page.FindControl("Submit") as Button;
                    //ClsCommon.ShowAlert(btnlbl.Text);
                    //btnlbl_Click(sender, e);
                    Page.RegisterStartupScript("FireOkOfPage", "<script language='javascript'>FireFacebookButtonOnSpecial();</script>");


                }
                else
                {
                    if (lResult.Count > 0)
                    {
                        Response.Redirect("Customerprofile.aspx?MM=1");
                    }
                    else
                    {
                        Response.Redirect("Customerregistration.aspx?fb=y");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (pClsObj != null)
                {
                    pClsObj = null;
                }
                if (lResult != null)
                {
                    lResult = null;
                }
            }

        }
        #endregion
    }
}