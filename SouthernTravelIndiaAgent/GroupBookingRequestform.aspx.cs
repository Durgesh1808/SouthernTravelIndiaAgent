using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class GroupBookingRequestform : System.Web.UI.Page
    {
        protected string dtdepart, dtdepart1, dtarrive, dtarrive1;
        ClsAdo pclsObj = null;
        DataListResponse<GetCountryName_SPResult> lGetCountryName = null;
        clsContractModule clsHCobj = new clsContractModule();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                this.txtcity.Attributes.Add("onkeydown", "return chkCharacter();");
                this.txtPhone.Attributes.Add("onKeyPress", "return isNumberKey(event);");
                this.txtMobile.Attributes.Add("onKeyPress", "return isNumberKey(event);");

                this.txtZip.Attributes.Add("onKeyPress", "return isNumberKey();");
                this.txtadults.Attributes.Add("onKeyPress", "return isNumberKey(event);");
                this.txtchild.Attributes.Add("onKeyPress", "return isNumberKey(event);");
                this.txtgroupleadername.Attributes.Add("onkeydown", "return chkCharacter();");

                if ((dtdepart == "") || (dtdepart == null))
                    dtdepart1 = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(-0).ToShortDateString();
                else
                    dtdepart1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(dtdepart1)).ToShortDateString();
                if ((dtarrive == "") || (dtarrive == null))
                    dtarrive1 = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(-0).ToShortDateString();
                else
                    dtarrive1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(dtarrive1)).ToShortDateString();
                if (!IsPostBack)
                {
                    chkPromotions.Checked = true;
                    DataTable dtTour = null;
                    lGetCountryName = new DataListResponse<GetCountryName_SPResult>();
                    lGetCountryName = clsHCobj.fnGetCountryName(0);
                    try
                    {
                        //ddlNationality.DataSource = lGetCountryName.ResultList;
                        //ddlNationality.DataTextField = "CountryName";
                        //ddlNationality.DataValueField = "CountryID";
                        //ddlNationality.DataBind();
                        //ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
                        //ddlNationality.SelectedValue = "1";

                        pclsObj = new ClsAdo();
                        ddlState.DataSource = pclsObj.fnGetState();
                        ddlState.DataTextField = "State";
                        ddlState.DataValueField = "State";
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, new ListItem("--------Select--------", ""));

                        dtTour = pclsObj.fnGetFixedToursActive();

                        ddlTour.Items.Clear();
                        ddlTour.DataSource = dtTour;
                        ddlTour.DataValueField = "TourNo";
                        ddlTour.DataTextField = "TourName";
                        ddlTour.DataBind();
                        ddlTour.Items.Insert(0, new ListItem("<--Select Tour-->", "0"));
                        ddlTour.Items.Insert(dtTour.Rows.Count + 1, new ListItem("Others..", "00"));
                    }
                    finally
                    {
                        if (pclsObj != null)
                        {
                            pclsObj = null;
                        }
                        if (dtTour != null)
                        {
                            dtTour.Dispose();
                            dtTour = null;
                        }
                    }
                }
                Submit.Attributes.Add("onclick", "return validate()");
            }
            else
                Response.Redirect("agentlogin.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string accm, food, tourname;
            if (txtadults.Value != "")
            {
                if ((chkaccommodation.Checked) == true)
                    accm = "Y";
                else
                    accm = "N";
                if ((ChkFood.Checked) == true)
                    food = "Y";
                else
                    food = "N";
                if (Convert.ToInt32(ddlTour.SelectedValue) != 0)
                    tourname = ddlTour.SelectedItem.Text;
                else
                {
                    if (DataLib.funClear(othertour.Value) != "")
                        tourname = DataLib.funClear(othertour.Value);
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "Notourname", "<script>alert('Please enter the tourname');</script>");
                        return;
                    }
                }
                string depar = departure.Value.Split('/').GetValue(1) + "/" + departure.Value.Split('/').GetValue(0) + "/" + departure.Value.Split('/').GetValue(2);
                string arri = arrival.Value.Split('/').GetValue(1) + "/" + arrival.Value.Split('/').GetValue(0) + "/" + arrival.Value.Split('/').GetValue(2);
                Group_GroupBookingRequest TblObj = null;
                try
                {
                    #region Optimize Code
                    /* SqlParameter[] lparam = new SqlParameter[19];
                     lparam[0] = new SqlParameter("@TourName", tourname);
                     lparam[1] = new SqlParameter("@TourId", Convert.ToInt32(ddlTour.SelectedValue));
                     lparam[2] = new SqlParameter("@GroupLeaderName", DataLib.funClear(txtgroupleadername.Value));
                     lparam[3] = new SqlParameter("@NoofAdults", Convert.ToInt32(DataLib.funClear(txtadults.Value)));
                     lparam[4] = new SqlParameter("@NoofChilds", Convert.ToInt32(DataLib.funClear(txtchild.Value)));
                     lparam[5] = new SqlParameter("@BusType", ddlbustype.SelectedValue);
                     lparam[6] = new SqlParameter("@DepartureDate", Convert.ToDateTime(depar));
                     lparam[7] = new SqlParameter("@ArrivalDate", Convert.ToDateTime(arri));
                     lparam[8] = new SqlParameter("@Accommodation", Convert.ToChar(accm));
                     lparam[9] = new SqlParameter("@Food", Convert.ToChar(food));
                     lparam[10] = new SqlParameter("@Address", DataLib.funClear(txtaddress.Value));
                     lparam[11] = new SqlParameter("@City", DataLib.funClear(txtcity.Value));
                     lparam[12] = new SqlParameter("@State", ddlState.SelectedValue);
                     lparam[13] = new SqlParameter("@PinCode", DataLib.funClear(txtZip.Value));
                     lparam[14] = new SqlParameter("@PhoneNo", DataLib.funClear(txtPhone.Value));
                     lparam[15] = new SqlParameter("@MobileNo", DataLib.funClear(txtMobile.Value));
                     lparam[16] = new SqlParameter("@Emailid", DataLib.funClear(txtMail.Value));
                     lparam[17] = new SqlParameter("@AgentId", Convert.ToString(Session["UserId"]));
                     lparam[18] = new SqlParameter("@O_Id", Convert.ToInt32(0));
                     lparam[18].Direction = ParameterDirection.Output;
                     string strQueryId = Convert.ToString(DataLib.InsStoredProcData("insert_Group_GroupBookingRequest", lparam));*/
                    #endregion

                    pclsObj = new ClsAdo();
                    TblObj = new Group_GroupBookingRequest();
                    TblObj.TourName = tourname;
                    TblObj.TourId = Convert.ToInt32(ddlTour.SelectedValue);
                    TblObj.GroupLeaderName = DataLib.funClear(txtgroupleadername.Value);
                    TblObj.NoofAdults = Convert.ToInt32(DataLib.funClear(txtadults.Value));
                    TblObj.NoofChilds = Convert.ToInt32(DataLib.funClear(txtchild.Value));
                    TblObj.BusType = ddlbustype.SelectedValue;
                    TblObj.DepartureDate = Convert.ToDateTime(depar);
                    TblObj.ArrivalDate = Convert.ToDateTime(arri);
                    TblObj.Accommodation = Convert.ToChar(accm);
                    //TblObj.Nationality = ddlNationality.SelectedValue;
                    TblObj.Food = Convert.ToChar(food);
                    TblObj.Address = DataLib.funClear(txtaddress.Value);
                    TblObj.City = DataLib.funClear(txtcity.Value);
                    TblObj.State = ddlState.SelectedValue;
                    TblObj.PinCode = DataLib.funClear(txtZip.Value);
                    TblObj.PhoneNo = DataLib.funClear(txtPhone.Value);
                    TblObj.MobileNo = DataLib.funClear(txtMobile.Value);
                    TblObj.Emailid = DataLib.funClear(txtMail.Value);
                    TblObj.AgentId = Convert.ToString(Session["UserId"]);
                    string strQueryId = Convert.ToString(pclsObj.fnInsertGroup_GroupBookingRequest(TblObj));


                    if ((strQueryId != "0") && (strQueryId != "2"))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "Success", "<script>alert('Thank you.your request id is RST" + strQueryId + ", keep this for future reference')</script>");

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("Dear Manager,<br>");
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Southern Travels<br>");
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Subject - Inquiry for Group Booking - " + tourname + "<br>");
                        sb.Append("&nbsp;&nbsp;&nbsp;Sir I am " + txtgroupleadername.Value + ",<br>");
                        sb.Append("I want to enquire about this tour. my personal details are as follows:<br>");
                        sb.Append("Departure date :" + depar + "<br>");
                        sb.Append("Arrival date :" + arri + "<br>");
                        sb.Append("Vehicle Type :" + ddlbustype.SelectedValue + "<br>");
                        sb.Append("Accommodation :" + accm + "<br>");
                        sb.Append("Food :" + food + "<br>");
                        sb.Append("No. of Adults :" + txtadults.Value + "<br>");
                        sb.Append("No. of Childs :" + txtchild.Value + "<br>");
                        sb.Append("Address :" + txtaddress.Value + "<br>");
                        sb.Append("City :" + txtcity.Value + "<br>");
                        sb.Append("Mobile no :" + txtMobile.Value + "<br>");
                        sb.Append("Phone no :" + txtPhone.Value + "<br>");
                        ClsCommon.SendMail(ConfigurationSettings.AppSettings["splTourEnquiryMailId"].ToString(), "", "", txtMail.Value,
                            "Inquiry for Groupbooking - " + tourname, sb.ToString());
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (pclsObj != null)
                    {
                        pclsObj = null;
                    }
                    if (TblObj != null)
                    {
                        TblObj = null;
                    }
                    Globals.ClearControls(this);
                }
            }
        }
    }
}