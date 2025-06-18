using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentHotelBooking : System.Web.UI.Page
    {
        #region "Member variables"
        protected string sql1, sql2, sql3, lcustRowid, stremailonline, dtarr;
        protected string sdeluxe, ssuperDeluxe, sexecutive, sRoyal;
        protected string scheckin, scheckin1;
        ClsAdo pclsObj = null;
        STSPLOrOther pvclsOth = null;
        #endregion
        #region "Event's"
        protected void Page_Load(object sender, EventArgs e)
        {
            txtarr.Attributes.Add("readonly", "readonly");
            txtdep.Attributes.Add("readonly", "readonly");
            txtamt.Attributes.Add("readonly", "readonly");
            txttax.Attributes.Add("readonly", "readonly");
            txttotalamtwithtax.Attributes.Add("readonly", "readonly");

            TextBox txtEmail = (TextBox)ucManageCustomer1.FindControl("emailid");
            ucManageCustomer1.btnSearchClick += new btnSearch_Click(ucManageCustomer1_btnSearchClick);
            ucManageCustomer1.fldIsPanRequired = true;

            /*ddlarrivalhr.Attributes.Add("disabled", "disabled");
            ddlarrivalmin.Attributes.Add("disabled", "disabled");
            ddlarrivalam.Attributes.Add("disabled", "disabled");
            ddlDeparture.Attributes.Add("disabled", "disabled");
            ddlDeparturemin.Attributes.Add("disabled", "disabled");
            ddlDepartuream.Attributes.Add("disabled", "disabled");*/

            //txtpaydate.Attributes.Add("readonly", "readonly");

            //book_continue.Attributes.Add("onclick", "javascript:return validate();");
            // txtmobile.Attributes.Add("onKeyPress", "javascript:return checknumber();");
            // txtadult.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
            //txtchild.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
            // S_phone.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
            //  txtmobile.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
            //  txtAlternateMobileNo.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
            this.Submit.Attributes.Add("onclick", "javascript:return valid();");
            btnContinue.Attributes.Add("onclick", "javascript:return validate();");
            btnSearch.Attributes.Add("onclick", "javascript:return Emailvalid();");
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }


            if (!Page.IsPostBack)
            {
                //chkPromotions.Checked = true;
                Panel2.Style.Add("display", "none");
                pnlCustomer.Visible = false;
                //Panel2.Visible = false;

                Panel1.Style.Add("display", "block");
                BindCityName();
                paymodes();
                //Bindroomtype();
                SelectPax(3, 2);
                // for MD5 Password Purpose Start
                Random r = new Random();
                string password = r.Next().ToString();
                if (password.Length > 6)
                {
                    password = password.Remove(6);
                }
                //ViewState["strPass"] = "";
                ViewState["Spass"] = password.Trim().ToString();
                ClientScript.RegisterStartupScript(typeof(string), "stsrtupSend8", "<script>fnMd5('" + ViewState["Spass"].ToString() + "');</script>");
                password = tmpEnValue.Value;
            }
            else
            {
                ViewState["strPass"] = tmpEnValue.Value;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //pnl2.Visible = true;

            //DataTable dtOnlineCust = new DataTable();
            //string emailidt = txtSearch.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
            //ClsAdo pclsObj = null;
            //try
            //{
            //    pclsObj = new ClsAdo();
            //    if (txtSearch.Text.Trim() != "")
            //    {
            //        dtOnlineCust = pclsObj.fnGetCustomerDetail(emailidt);
            //        if (dtOnlineCust != null && dtOnlineCust.Rows.Count > 0)
            //        {
            //            S_name.Attributes.Add("readonly", "readonly");
            //            S_email.Attributes.Add("readonly", "readonly");
            //            txtmobile.Attributes.Add("readonly", "readonly");

            //            S_name.Text = dtOnlineCust.Rows[0]["FirstName"].ToString();
            //            txtComp.Text = dtOnlineCust.Rows[0]["company"].ToString();
            //            ddlState.SelectedValue = dtOnlineCust.Rows[0]["state"].ToString();
            //            txtmobile.Text = dtOnlineCust.Rows[0]["Mobile"].ToString();
            //            txtAlternateMobileNo.Text = dtOnlineCust.Rows[0]["AlternativeNo"].ToString();
            //            S_email.Text = dtOnlineCust.Rows[0]["email"].ToString();
            //            txtcity.Text = dtOnlineCust.Rows[0]["City"].ToString();
            //            if (dtOnlineCust.Rows[0]["PhoneNo"].ToString().IndexOf("-") != -1)
            //            {
            //                string[] tempPhone = dtOnlineCust.Rows[0]["PhoneNo"].ToString().Split('-');
            //                //txtPhoneCountryCode.Text = tempPhone[0];
            //                S_phone.Text = tempPhone[1];
            //            }
            //            else
            //            {
            //                S_phone.Text = dtOnlineCust.Rows[0]["PhoneNo"].ToString();
            //            }
            //            S_streetaddress.Text = dtOnlineCust.Rows[0]["Addr1"].ToString();
            //            chkPromotions.Checked = Convert.ToBoolean(dtOnlineCust.Rows[0]["CanSendPromotions"] != DBNull.Value ?
            //                Convert.ToInt16(dtOnlineCust.Rows[0]["CanSendPromotions"]) : 1);
            //        }
            //        else
            //        {
            //            S_name.Attributes.Remove("readonly");
            //            S_email.Attributes.Remove("readonly");
            //            txtmobile.Attributes.Remove("readonly");

            //            S_name.Text = "";
            //            txtComp.Text = "";
            //            ddlState.SelectedIndex = -1;
            //            if (txtSearch.Text.Contains("@"))
            //            {
            //                txtmobile.Text = "";
            //                S_email.Text = txtSearch.Text;
            //            }
            //            else
            //            {
            //                txtmobile.Text = txtSearch.Text;
            //                S_email.Text = "";
            //            }
            //            txtcity.Text = "";
            //            S_phone.Text = "";
            //            S_streetaddress.Text = "";
            //            chkPromotions.Checked = true;
            //        }
            //    }

            //}
            //finally
            //{
            //    if (pclsObj != null)
            //    {
            //        pclsObj = null;
            //    }
            //    if (dtOnlineCust != null)
            //    {
            //        dtOnlineCust.Dispose();
            //        dtOnlineCust = null;
            //    }
            //}
        }
        protected void book_continue_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            ControlReadOnly();
            Panel1.Style.Add("display", "none");
            //pnl2.Visible = true;

            // Panel2.Visible = true;
            Panel2.Style.Add("display", "block");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (!OccupationGiven())
            {
                return;
            }

            string lArrivalDate, lArrTime, lDepartureDate, lDepTime;
            string[] lSd = txtarr.Text.Split('/');
            lArrivalDate = lSd[1] + "/" + lSd[0] + "/" + lSd[2];
            lArrTime = ddlarrivalhr.SelectedValue + ":" + ddlarrivalmin.SelectedValue + ":00 ";// +ddlarrivalam.SelectedValue;
            lArrivalDate = lArrivalDate + " " + lArrTime;

            //For Agent Balance check.
            string lBalance = "";
            pclsObj = null;
            DataTable ldtRecSet = null;
            try
            {
                pclsObj = new ClsAdo();
                if (Convert.ToString(Session["AgentId"]) != "")
                {

                    ldtRecSet = pclsObj.fnGetAgent_AvailableBalance(Convert.ToInt32(Session["AgentId"]));
                    if (ldtRecSet.Rows.Count > 0)
                    {
                        lBalance = Convert.ToString(ldtRecSet.Rows[0][0]);
                    }
                    else
                    {
                        lBalance = "";
                    }
                    if (lBalance == "")
                        Session["Balance"] = "0";
                    else
                        Session["Balance"] = lBalance;

                }
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                }
            }
            if (Convert.ToDecimal(Convert.ToString(Session["Balance"])) <= 0)
            {
                ClsCommon.ShowAlert("Insufficient funds");
                //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Insufficient funds');</script>");
                return;
            }
            if (Convert.ToDecimal(txttotalamtwithtax.Text) > Convert.ToDecimal(Convert.ToString(Session["Balance"])))
            {
                ClsCommon.ShowAlert("Insufficient funds");

                //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Insufficient funds');</script>");

            }
            else if (DateTime.Now >= Convert.ToDateTime(lArrivalDate))
            {
                ClsCommon.ShowAlert("Checkin Date & Time Should be greaterthan Todays Date & Time");

            }
            else
            {

                string[] lSd1 = txtdep.Text.Split('/');
                lDepartureDate = lSd1[1] + "/" + lSd1[0] + "/" + lSd1[2];
                lDepTime = ddlDeparture.SelectedValue + ":" + ddlDeparturemin.SelectedValue + ":00 ";// +ddlDepartuream.SelectedValue;
                lDepartureDate = lDepartureDate + " " + lDepTime;

                /***************/

                decimal noofdays = 0;
                noofdays = Convert.ToDecimal(txtnodays1.Value);
                /***************/

                GroupLeader pManageCustomer = ucManageCustomer1.fldGroupLeader;

                if (pManageCustomer == null)
                {
                    return;
                }
                string sCustName = pManageCustomer.fldName, sAddress = pManageCustomer.fldAddress, pNationality = pManageCustomer.fldNationality.ToString(),
                   pCountry = pManageCustomer.fldCountryID.ToString(), pState = pManageCustomer.fldState, pCompany = pManageCustomer.fldCompanyName,
                   pCity = pManageCustomer.fldCity, pPhoneNo = pManageCustomer.fldSTDCode + "-" + pManageCustomer.fldPhoneNo,
                   pEmail = pManageCustomer.fldEmailID, pMobile = pManageCustomer.fldMobileNo,
                   pEmrgContNo = pManageCustomer.fldEmergencyNo, pPincode = pManageCustomer.fldPinCode,
                   lTitle = pManageCustomer.fldTitle, pPanNo = pManageCustomer.fldPanNo, pPanImg = pManageCustomer.fldPanImage,
                   pOccupation = pManageCustomer.fldOccupation,
                   pCustomerGSTIN = pManageCustomer.GSTIN, pGSTHolderName = pManageCustomer.GSTHolderName,
                   pAadharNo = pManageCustomer.fldAadharNo, pAadharNoImg = pManageCustomer.fldAadharImage;

                int poccpationId = pManageCustomer.fldOccupationId;
                string GuestDetailsStr = GetGuestDetailsstring();

                bool pSendIsPro = pManageCustomer.fldCanSendPromotions, pIsGSTIN = pManageCustomer.IsGSTIN;
                int pCoustID = pManageCustomer.fldCustomerID;
                int IsPanNoVerify = 0;

                //   string sCustName = DataLib.funClear(Convert.ToString(S_name.Text.Trim()));
                //  string sCompany = DataLib.funClear(Convert.ToString(txtComp.Text.Trim()));
                //  string sPhone = DataLib.funClear(Convert.ToString(S_phone.Text.Trim()));
                // string sMobile = DataLib.funClear(Convert.ToString(txtmobile.Text.Trim()));
                //  string sAddress = S_streetaddress.Text.Trim();
                //  string sCity = DataLib.funClear(Convert.ToString(txtcity.Text.Trim()));
                ////  string sState = DataLib.funClear(Convert.ToString(ddlState.SelectedValue));
                //  string sEmail = DataLib.funClear(Convert.ToString(S_email.Text.Trim()));
                //   string sPWD = Convert.ToString(ViewState["strPass"]);
                // string sAdults = DataLib.funClear(Convert.ToString(txtadult.Text.Trim()));
                // string sChild = DataLib.funClear(Convert.ToString(txtchild.Text.Trim()));

                int intRoomTo = 0;
                string hotelRoomtypes = "";
                int lDeluxe = 0;
                int lSuperDeluxe = 0;
                int lExecutive = 0;
                int lRoyal = 0;

                int lNoofAdults = 0;
                int lNoofChild = 0;
                int lNooRooms = 0;// txtrooms.Value.Trim() == "" ? 0 : Convert.ToInt32(DataLib.funClear(Convert.ToString(intRoomTo)));

                string sAmount = DataLib.funClear(Convert.ToString(txtamt.Text.Trim()));
                string sDiscount = "0"; //DataLib.funClear(Convert.ToString(txtdiscount.Value.Trim()));

                string sMasterDiscount = "0"; //DataLib.funClear(Convert.ToString(txtdiscount.Value.Trim()));

                string ssTax = DataLib.funClear(Convert.ToString(txttax.Text.Trim()));
                string sTotalWithTax = DataLib.funClear(Convert.ToString(txttotalamtwithtax.Text.Trim()));

                if (Convert.ToDecimal(sTotalWithTax) <= 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Alert", "<script>alert('Total Cost must be greater then 0');</script>");
                    return;
                }

                string sAdvance = "0";// DataLib.funClear(Convert.ToString(txtadvance.Text.Trim()));
                string sBalance = "0";// DataLib.funClear(Convert.ToString(txtbalance.Text.Trim()));
                string sRemarks = txtremarks.Text.Trim();
                string sPaymode = "AgentCash";// DataLib.funClear(Convert.ToString(DDLPaymode.SelectedValue));
                string sBankName = "";//DataLib.funClear(Convert.ToString(txtBankname.Text.Trim()));
                string sChequeNo = "";//DataLib.funClear(Convert.ToString(txtChequeNo.Text.Trim()));
                string sPaydate = "";//DataLib.funClear(Convert.ToString(txtpaydate.Text.Trim()));
                string susername = Session["UserId"] == null ? "Admin" : Session["UserId"].ToString();
                string sbranchname = Session["LocalBranch"] == null ? "Admin" : Session["LocalBranch"].ToString();

                string pickupveh = "", strFlight = "", strPktime = "", strFlightNo = "", strPKstation = "";
                string dropveh = "", strDFlight = "", strDPktime = "", strDFlightNo = "";

                if (RadFlight.Checked == true)
                {
                    strFlight = txtpickVehicleNo.SelectedItem.Text.ToString();
                    pickupveh = "Flight";
                    strPKstation = txtpickVehicleNo.SelectedItem.Text.ToString();
                    strPktime = ddlPkHrs.Value + ":" + ddlPkMints.Value;
                    strFlightNo = DataLib.funClear(txtFlightNo.Text);
                }
                else if (RadTrain.Checked == true)
                {
                    pickupveh = "Train";
                    strPKstation = DataLib.funClear(txtRlyStationName.Text);
                    strFlight = DataLib.funClear(txtRlyStationName.Text);
                    strPktime = ddlTrainPkHr.Value + ":" + ddlTrainPkMin.Value;
                    strFlightNo = DataLib.funClear(txtTrainNo.Text);
                }
                else if (RadBus.Checked == true)
                {
                    pickupveh = "Bus";
                    strPKstation = DataLib.funClear(txtAddr.Text);
                    strFlightNo = DataLib.funClear(txtStreet.Text);
                }
                if (RadFlight_d.Checked == true)
                {
                    dropveh = "Flight";
                    strDFlight = txtpickVehicleNo_d.SelectedItem.Text.ToString();
                    strDPktime = ddlPkHrs_d.Value + ":" + ddlPkMints_d.Value;
                    strDFlightNo = DataLib.funClear(txtFlightNo_d.Text);
                }
                else if (RadTrain_d.Checked == true)
                {
                    dropveh = "Train";
                    strDFlight = DataLib.funClear(txtRlyStationName_d.Text);
                    strDPktime = ddlTrainPkHr_d.Value + ":" + ddlTrainPkMin_d.Value;
                    strDFlightNo = DataLib.funClear(txtTrainNo_d.Text);
                }
                else if (RadBus_d.Checked == true)
                {
                    dropveh = "Bus";
                    strDFlight = DataLib.funClear(txtAddr_d.Text);
                    strDFlightNo = DataLib.funClear(txtStreet_d.Text);
                }

                //For Insert Accomadation Detail
                bool flag = false;

                intRoomTo = intRoomTo + Convert.ToInt32(ddlNoRoom.SelectedValue);
                lNooRooms = intRoomTo;
                int i = Convert.ToInt32(hdRowIndex.Value);
                i = i - 1;
                Label lblrtype = (Label)gvRoomType.Rows[i].Cells[2].FindControl("lblrtype");
                HiddenField hdFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdFare");
                Label lblroomtypeId = (Label)gvRoomType.Rows[i].Cells[2].FindControl("lblroomtypeId");
                HiddenField hdExtraFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdExFare");
                HiddenField hdfareid = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdfareid");
                lNoofAdults = lNoofAdults + Convert.ToInt32(ddlAdults.SelectedValue);
                lNoofChild = lNoofChild + Convert.ToInt32(ddlChilds.SelectedValue);

                decimal XetraFare = 0;
                int ExtraBed = 0;
                int MaxAllow = 2;
                string lOccupancy = "";
                string pOccupancy = "";
                for (int lp = 0; lp < rbtOccupants.Items.Count; lp++)
                {
                    if (rbtOccupants.Items[lp].Selected && rbtOccupants.Items[lp].Value == "SingleOccupancy")
                    {
                        MaxAllow = 1;
                        lOccupancy = "Single Occupancy -- ";
                        pOccupancy = "SingleOccupancy";
                    }
                    else if (rbtOccupants.Items[lp].Selected && rbtOccupants.Items[lp].Value == "DoubleOccupancy")
                    {
                        MaxAllow = 2;
                        lOccupancy = "Double Occupancy -- ";
                        pOccupancy = "DoubleOccupancy";
                    }
                    else if (rbtOccupants.Items[lp].Selected && rbtOccupants.Items[lp].Value == "FourOccupancy")
                    {
                        MaxAllow = 4;
                        lOccupancy = "Four Occupancy -- ";
                        pOccupancy = "FourOccupancy";
                    }
                }
                ExtraBed = Convert.ToInt32(ddlAdults.SelectedValue) - (MaxAllow * intRoomTo);
                decimal ChildWithourBedFaraFare = Convert.ToDecimal(lblCWBedFare.Text == "" ? "0" : lblCWBedFare.Text);
                int TotalCWB = GetTotalChildWihtouBed();
                if (ExtraBed > 0)
                {
                    //XetraFare = Convert.ToDecimal(hdExtraFare.Value) * ExtraBed; ;
                    XetraFare = Convert.ToDecimal(hdExtraFare.Value) * ExtraBed;// *intRoomTo * noofdays;
                }
                else
                {
                    ExtraBed = 0;
                }



                if (flag == false)
                {
                    hotelRoomtypes = ddlNoRoom.SelectedValue + "X" + lOccupancy + lblrtype.Text;
                    flag = true;
                }
                else
                {
                    hotelRoomtypes = hotelRoomtypes + "+" + ddlNoRoom.SelectedValue + "X" + lOccupancy + lblrtype.Text;
                }
                HiddenField hdDisType = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdDisType");

                decimal pDiscountFare = 0;
                HiddenField hdISAMT = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdISAMT");
                HiddenField hdAmountDis = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdAmountDis");
                Label lblFare1 = (Label)gvRoomType.Rows[i].Cells[3].FindControl("lblFare");
                if (hdISAMT.Value == "Y")
                {
                    pDiscountFare = decimal.Round(Convert.ToDecimal(hdAmountDis.Value));


                }
                else if (hdISAMT.Value == "N")
                {
                    pDiscountFare = decimal.Round((Convert.ToDecimal(lblFare1.Text) * Convert.ToDecimal(hdAmountDis.Value) / 100));

                }

                if (hdDisType.Value == "Flat")
                {
                    sMasterDiscount = Convert.ToString(Convert.ToDecimal(pDiscountFare) * Convert.ToInt32(lNooRooms) * Convert.ToInt32(noofdays));
                }
                else if (hdDisType.Value == "Booking")
                {
                    sMasterDiscount = Convert.ToString(Convert.ToDecimal(pDiscountFare) * Convert.ToInt32(lNooRooms) * Convert.ToInt32(noofdays));
                }
                else if (hdDisType.Value == "CheckIn")
                {
                    HiddenField hdDisFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdDisFare");
                    HiddenField hdDisDays = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdDisDays");
                    sMasterDiscount = Convert.ToString(Convert.ToDecimal(pDiscountFare) * Convert.ToInt32(lNooRooms) * Convert.ToInt32(hdDisDays.Value));
                }

                //For Agent Balance Maintenance and his Comission Maintenance Purpose

                decimal AvailableBalance = 0, AgentCommission = 0, lCalTds = 0, total = decimal.Round(Convert.ToDecimal(txttotalamtwithtax.Text), 0);


                if (Convert.ToString(Session["AgentId"]) != "")
                {

                    decimal lTds = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));

                    if (Convert.ToString(Session["Balance"]) != "")
                    {
                        AvailableBalance = Convert.ToDecimal(Convert.ToString(Session["Balance"]));
                    }
                    //int? AgentLevel = default(int);
                    //pclsObj = null;

                    //try
                    //{
                    //    pclsObj = new ClsAdo();
                    //    pclsObj.Agent_GetAgentLevelbyAgentId(Convert.ToInt32(Session["AgentId"]), ref AgentLevel);
                    //}
                    //finally
                    //{
                    //    if (pclsObj != null)
                    //    {
                    //        pclsObj = null;
                    //    }
                    //}
                    DataTable dtComm = new DataTable();

                    decimal lComission = 0;
                    bool? pIsPers = false;
                    //if (AgentLevel != null)
                    //{
                    //    pclsObj = null;

                    try
                    {
                        pclsObj = new ClsAdo();
                        dtComm = pclsObj.fnAgentCommission("Accommodation Booking", Convert.ToInt32(Session["AgentId"]));
                    }
                    finally
                    {
                        if (pclsObj != null)
                        {
                            pclsObj = null;
                        }
                    }
                    if (dtComm != null && dtComm.Rows.Count > 0)
                    {
                        lComission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
                        pIsPers = Convert.ToString(dtComm.Rows[0]["IsPercentage"].ToString()) == "Y" ? true : false;
                        Session["Comission"] = Convert.ToString(lComission);
                        Session["IsPercentComm"] = Convert.ToBoolean(pIsPers);
                    }
                    // }

                    if (Convert.ToString(Session["Comission"]) != "")
                    {
                        AgentCommission = Convert.ToBoolean(Session["IsPercentComm"]) ? Convert.ToDecimal(Convert.ToDecimal(sAmount) * (Convert.ToDecimal(Session["Comission"]) / 100))
                             : decimal.Round(Convert.ToDecimal(Session["Comission"]) * (lNoofAdults + lNoofChild));
                    }

                    lCalTds = Convert.ToDecimal(AgentCommission * (Convert.ToDecimal(lTds) / 100));
                }
                //pvclsOth = null;
                GST_Data objGST_data = null;
                BranchHotel_Tbl clsTbl = null;
                clsTransactionTable pclsTbl = null;
                string orderId = "", lTicketCode = "", lAccoID = "0";
                try
                {
                    objGST_data = new GST_Data();
                    //pvclsOth = new STSPLOrOther();
                    clsTbl = new BranchHotel_Tbl();
                    clsTbl.AgentId = Convert.ToInt32(Session["AgentId"]);
                    clsTbl.noofadults = lNoofAdults;
                    clsTbl.noofchildren = lNoofChild;
                    clsTbl.noofrooms = lNooRooms;
                    clsTbl.Deluxe = lDeluxe;
                    clsTbl.SuperDeluxe = lSuperDeluxe;
                    clsTbl.Executive = lExecutive;
                    clsTbl.Royal = lRoyal;
                    clsTbl.Totalamount = decimal.Round(Convert.ToDecimal(sAmount), 0);
                    clsTbl.STaxValue = decimal.Round(Convert.ToDecimal(ssTax), 0);
                    clsTbl.AmtWithTax = decimal.Round(Convert.ToDecimal(sTotalWithTax), 0);
                    clsTbl.Discount = decimal.Round(Convert.ToDecimal(sDiscount), 0);
                    clsTbl.Advance = decimal.Round(Convert.ToDecimal(sAdvance), 0);
                    clsTbl.Paymode = sPaymode;
                    clsTbl.CheckNo = sChequeNo;
                    clsTbl.bankname = sBankName;
                    clsTbl.depttime = Convert.ToDateTime(lDepartureDate);
                    clsTbl.arrtime = Convert.ToDateTime(lArrivalDate);
                    clsTbl.Username = susername;
                    clsTbl.Branchcode = sbranchname;
                    clsTbl.BookingRoomTypes = hotelRoomtypes;
                    clsTbl.Remarks = sRemarks;
                    clsTbl.noofdays = noofdays;

                    clsTbl.PickUpVeh = pickupveh;
                    clsTbl.PickVehNo = strFlightNo;
                    clsTbl.PickTime = strPktime;
                    if (chkDrop.Checked == true)
                    {
                        clsTbl.DropVeh = pickupveh;
                        clsTbl.DropVehNo = strFlightNo;
                        clsTbl.DropTime = strPktime;
                    }
                    else
                    {
                        clsTbl.DropVeh = dropveh;
                        clsTbl.DropVehNo = strDFlightNo;
                        clsTbl.DropTime = strDPktime;
                    }
                    clsTbl.Station = strDFlight;
                    clsTbl.PkStation = strPKstation;
                    clsTbl.MasterDiscount = Convert.ToDecimal(sMasterDiscount);
                    clsTbl.HotelID = Convert.ToInt32(ddlHotel.SelectedValue);
                    clsTbl.Occupancy = pOccupancy;

                    pclsTbl = new clsTransactionTable();
                    pclsTbl.fldTransType = "AgentAccTicket";
                    pclsTbl.fldAgentCredit = decimal.Round((AgentCommission - lCalTds), 0);
                    pclsTbl.fldAvailableBalance = decimal.Round((((AvailableBalance + AgentCommission) - (lCalTds)) - (total)), 0);
                    pclsTbl.fldAgentDebit = decimal.Round(total, 0);
                    pclsTbl.fldTicketAmount = Convert.ToDecimal(txtamt.Text);
                    pclsTbl.fldCommission = decimal.Round((AgentCommission - (lCalTds)), 0);
                    pclsTbl.fldPaymentMode = sPaymode;
                    pclsTbl.fldNumber = "";
                    pclsTbl.fldDebit = decimal.Round((AgentCommission - (lCalTds)), 0);
                    pclsTbl.fldCredit = decimal.Round(total, 0);
                    pclsTbl.fldServiceTax = Convert.ToDecimal(txttax.Text);
                    pclsTbl.fldTDS = decimal.Round(lCalTds, 0);
                    pclsTbl.fldChqDate = "";
                    pclsTbl.fldImpersonatingUserName = "";
                    pclsTbl.fldImpersonatingBranchCode = "";
                    pclsTbl.fldCashier = "";
                    pclsTbl.fldTransState = 'P';
                    pclsTbl.fldStatus = "S";

                    pclsTbl.fldDiscount = decimal.Round((Convert.ToDecimal(sDiscount) + Convert.ToDecimal(sMasterDiscount)), 0);

                    lAccoID = objGST_data.GST_fnInsertFinalAccBookingInfo(clsTbl, pclsTbl, sCustName, sAddress,
                    pState, pPhoneNo, pMobile, pMobile, pEmail, pCountry, pCity,
                    Convert.ToDecimal(lblroomtypeId.Text), Convert.ToDecimal(hdFare.Value), ExtraBed,
                    hdfareid.Value, XetraFare, pSendIsPro, poccpationId, pOccupation, TotalCWB, ChildWithourBedFaraFare, GuestDetailsStr, ref orderId,
                    ref lTicketCode, pIsGSTIN, pCustomerGSTIN, pGSTHolderName, pPincode, pNationality, pCountry, pAadharNo, pAadharNoImg);

                    if (lAccoID != "0")
                    {
                        Session["orderId"] = orderId;
                        Session["TicketNo"] = lTicketCode;
                        Session["AccID"] = lAccoID;

                        if (!string.IsNullOrEmpty(pAadharNoImg))
                        {
                            SaveAadharPhysicalImg(pAadharNoImg);
                        }


                    }
                    else
                    {
                        ClsCommon.ShowAlert("Please Provide all the Information");

                        //ClientScript.RegisterStartupScript(GetType(), "Alert", "<script>alert('Please Provide all the Information');</script>");
                        return;
                    }
                }
                finally
                {
                    if (objGST_data != null)
                    {
                        objGST_data = null;
                    }
                    if (pclsTbl != null)
                    {
                        pclsTbl = null;
                    }
                    if (clsTbl != null)
                    {
                        clsTbl = null;
                    }
                    if (lAccoID != "0")
                    {
                        Session["sendmail"] = "yes";
                        Response.Redirect("Hotel_Reciept.aspx?orderid=" + Session["orderId"].ToString());
                    }
                }
            }
        }


        private void SaveAadharPhysicalImg(string pFileName)
        {
            // Specify the path to save the uploaded file to.
            string lSavePath = Server.MapPath("..\\AadharImg\\");
            SaveResizedAadharImage(pFileName);
            //F1ImageName.PostedFile.SaveAs(lSavePath + pFileName);
        }
        private void SaveResizedAadharImage(string pFileName)
        {
            string newpath = Server.MapPath("..\\AadharImg\\" + pFileName + ".jpg");
            FileUpload fupAadhar = (FileUpload)ucManageCustomer1.FindControl("fupAadhar");
            if (fupAadhar.HasFile)
            {
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fupAadhar.PostedFile.InputStream);
                System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 850);
                objImage.Save(newpath, ImageFormat.Png);
            }
        }


        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            if (image.Height < maxHeight)
            {
                maxHeight = image.Height;
            }
            var ratio = (double)maxHeight / image.Height;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                if (image.Height <= maxHeight)
                {
                    g.DrawImage(image, 0, 0);
                }
                else
                {
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }
            }
            return newImage;
        }


        protected void btnclear_Click(object sender, EventArgs e)
        {
            ControlnotReadOnly();
        }
        protected void Btnreset_Click(object sender, EventArgs e)
        {
            //pnl2.Visible = true;
            //Panel1.Enabled = true;
            Response.Redirect("AgentHotelBooking.aspx", true);


        }
        protected void gvRoomType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            pvclsOth = null;
            DataTable dtDiscount = null;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    pvclsOth = new STSPLOrOther();
                    dtDiscount = new DataTable();
                    int RoomTypeID = Convert.ToInt32(gvRoomType.DataKeys[e.Row.RowIndex].Values[0].ToString());
                    HiddenField hdFare = (HiddenField)e.Row.FindControl("hdFare");
                    HiddenField hdDisFare = (HiddenField)e.Row.FindControl("hdDisFare");
                    HiddenField hdDisDays = (HiddenField)e.Row.FindControl("hdDisDays");
                    Label lblFare = (Label)e.Row.FindControl("lblFare");
                    Label lblDFare = (Label)e.Row.FindControl("lblDFare");
                    HiddenField hdDisType = (HiddenField)e.Row.FindControl("hdDisType");
                    HiddenField hdISAMT = (HiddenField)e.Row.FindControl("hdISAMT");
                    HiddenField hdAmountDis = (HiddenField)e.Row.FindControl("hdAmountDis");
                    dtDiscount = pvclsOth.fnGetAccDiscount(Convert.ToInt32(ddlHotel.SelectedValue), Convert.ToInt32(RoomTypeID));
                    if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                    {


                        decimal strDis = Convert.ToDecimal(dtDiscount.Rows[0]["Descount"].ToString());
                        hdAmountDis.Value = dtDiscount.Rows[0]["Descount"].ToString();

                        string strFlat = dtDiscount.Rows[0]["IsFlat"].ToString();

                        string strCheckIn = dtDiscount.Rows[0]["IsCheckIn"].ToString();

                        string strAgent = dtDiscount.Rows[0]["IsAgent"].ToString();


                        string strisAmount = dtDiscount.Rows[0]["isAmount"].ToString();
                        hdISAMT.Value = dtDiscount.Rows[0]["isAmount"].ToString();
                        string strBooking = dtDiscount.Rows[0]["IsBooking"].ToString();


                        if (strAgent == "Y")
                        {
                            decimal pDiscountFare = 0;
                            if (strisAmount == "Y")
                            {
                                pDiscountFare = decimal.Round(Convert.ToDecimal(lblFare.Text) - strDis);


                            }
                            else
                            {
                                pDiscountFare = decimal.Round(Convert.ToDecimal(lblFare.Text) - (Convert.ToDecimal(lblFare.Text) * strDis / 100));

                            }

                            if (strFlat == "Y")
                            {
                                hdDisType.Value = "Flat";
                                lblFare.Style.Add("text-decoration", "line-through");
                                lblDFare.Text = pDiscountFare.ToString();
                                hdFare.Value = pDiscountFare.ToString();
                            }
                            if (strBooking == "Y")
                            {

                                if (dtDiscount.Rows[0]["BookingFrom"] != null && dtDiscount.Rows[0]["BookingTo"] != null)
                                {
                                    DateTime dtBFrom = Convert.ToDateTime(dtDiscount.Rows[0]["BookingFrom"]);
                                    DateTime dtBTo = Convert.ToDateTime(dtDiscount.Rows[0]["BookingTo"]);
                                    DateTime dtNow = DateTime.Now.Date;
                                    if (dtNow >= dtBFrom.Date && dtNow <= dtBTo.Date)
                                    {
                                        hdDisType.Value = "Booking";
                                        lblFare.Style.Add("text-decoration", "line-through");
                                        lblDFare.Text = pDiscountFare.ToString();
                                        hdFare.Value = pDiscountFare.ToString();
                                    }
                                }
                            }

                            //   HiddenField hdChkOut = (HiddenField)e.Row.FindControl("hdChkOut");
                            if (strCheckIn == "Y")
                            {

                                if (dtDiscount.Rows[0]["CheckInFrom"] != null && dtDiscount.Rows[0]["CheckOutTo"] != null)
                                {
                                    DateTime dtCFrom = Convert.ToDateTime(dtDiscount.Rows[0]["CheckInFrom"]);
                                    DateTime dtCTo = Convert.ToDateTime(dtDiscount.Rows[0]["CheckOutTo"]);

                                    char[] splitter = { '/' };
                                    string[] DateArr = txtarr.Text.ToString().Trim().Split(splitter);
                                    DateTime dtCheckIn = new DateTime(Convert.ToInt32(DateArr[2]), Convert.ToInt32(DateArr[1]),
                                         Convert.ToInt32(DateArr[0]));
                                    string[] DateArr1 = txtdep.Text.ToString().Trim().Split(splitter);
                                    DateTime dtCheckOut = new DateTime(Convert.ToInt32(DateArr1[2]), Convert.ToInt32(DateArr1[1]),
                                        Convert.ToInt32(DateArr1[0]));


                                    // hdChkIn.Value = dtCheckIn.ToShortDateString();
                                    //hdChkOut.Value = dtCheckOut.ToShortDateString();

                                    int DayDis = 0;
                                    if (dtCheckIn <= dtCFrom.Date && dtCheckOut <= dtCTo.Date)
                                    {
                                        TimeSpan ts = dtCheckOut - dtCFrom;

                                        DayDis = ts.Days;
                                    }
                                    else if (dtCheckIn >= dtCFrom.Date && dtCheckOut >= dtCTo.Date)
                                    {

                                        TimeSpan ts = dtCTo - dtCheckIn;

                                        DayDis = ts.Days;
                                        if (dtCheckIn == dtCTo.Date)
                                        {
                                            ts = new TimeSpan();
                                            ts = dtCTo.AddDays(1) - dtCheckIn;
                                            DayDis = ts.Days;
                                        }
                                        else if (dtCheckOut > dtCTo.Date)
                                        {
                                            ts = new TimeSpan();
                                            ts = dtCTo.AddDays(1) - dtCheckIn;
                                            DayDis = ts.Days;
                                        }

                                    }

                                    else if (dtCheckIn <= dtCFrom.Date && dtCheckOut >= dtCTo.Date)
                                    {
                                        TimeSpan ts = dtCTo - dtCFrom;

                                        DayDis = ts.Days;

                                        if (dtCheckOut >= dtCTo.AddDays(1).Date)
                                        {
                                            ts = new TimeSpan();
                                            ts = dtCTo.AddDays(1) - dtCFrom;
                                            DayDis = ts.Days;
                                        }

                                    }
                                    else if (dtCheckIn >= dtCFrom.Date && dtCheckOut <= dtCTo.Date)
                                    {
                                        TimeSpan ts = dtCheckOut - dtCheckIn;
                                        DayDis = ts.Days;
                                    }
                                    hdDisDays.Value = DayDis.ToString();

                                    if ((dtCheckIn >= dtCFrom.Date && dtCheckIn <= dtCTo.Date))
                                    {
                                        hdDisType.Value = "CheckIn";
                                        lblFare.Style.Add("text-decoration", "line-through");
                                        lblDFare.Text = pDiscountFare.ToString();
                                        //hdFare.Value = pDiscountFare.ToString();
                                        hdDisFare.Value = pDiscountFare.ToString();
                                    }
                                    else if ((dtCheckOut > dtCFrom.Date && dtCheckOut <= dtCTo.Date))
                                    {
                                        hdDisType.Value = "CheckIn";
                                        lblFare.Style.Add("text-decoration", "line-through");
                                        lblDFare.Text = pDiscountFare.ToString();
                                        //hdFare.Value = pDiscountFare.ToString();
                                        hdDisFare.Value = pDiscountFare.ToString();
                                    }
                                    else if (dtCheckIn <= dtCFrom.Date && dtCheckOut >= dtCTo.Date)
                                    {
                                        hdDisType.Value = "CheckIn";
                                        lblFare.Style.Add("text-decoration", "line-through");
                                        lblDFare.Text = pDiscountFare.ToString();
                                        //hdFare.Value = pDiscountFare.ToString();
                                        hdDisFare.Value = pDiscountFare.ToString();
                                    }
                                }

                            }
                        }

                    }
                }
            }
            finally
            {
                if (pvclsOth != null)
                {
                    pvclsOth = null;
                }
                if (dtDiscount != null)
                {
                    dtDiscount.Dispose();
                    dtDiscount = null;
                }
            }

        }
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            PnlGV.Visible = true;
            PnlCh.Enabled = false;
            btnContinue.Visible = false;
            BindGuestGrid();
            SetIsCalculateCWBFare();

        }
        protected void btnRe_Click(object sender, EventArgs e)
        {
            /*PnlGV.Visible = false;
            PnlCh.Enabled = true;
            btnContinue.Visible = true;*/
            Response.Redirect("AgentHotelBooking.aspx", true);
        }
        protected void ddlRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool lFlag = false;
            for (int i = 0; i < rbtOccupants.Items.Count; i++)
            {
                if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "SingleOccupancy")
                {
                    SelectPax(1, 1);
                    lFlag = true;
                }
                else if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "DoubleOccupancy")
                {
                    SelectPax(3, 2);
                    lFlag = true;
                }
                else if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "FourOccupancy")
                {
                    SelectPax(5, 2);
                    lFlag = true;
                }
            }
            if (!lFlag)
            {
                ddlNoRoom.SelectedIndex = -1;
                ClsCommon.ShowAlert("Please select Occupants.");

                //ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Please select Occupants.');</script>");
            }
        }
        protected void btnbook_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string[] lSd = txtarr.Text.Split('/');
            string lArrivalDate = lSd[1] + "/" + lSd[0] + "/" + lSd[2];
            string lArrTime = ddlarrivalhr.SelectedValue + ":" + ddlarrivalmin.SelectedValue + ":00 ";// +ddlarrivalam.SelectedValue;
            lArrivalDate = lArrivalDate + " " + lArrTime;
            if (DateTime.Now >= Convert.ToDateTime(lArrivalDate))
            {
                ClsCommon.ShowAlert("Checkin Date & Time Should be greater than Todays Date & Time");
                return;
            }
            ControlReadOnly();

            //pnl2.Visible = true;

            hdRowIndex.Value = "";
            Button ibtn1 = sender as Button;
            int RowIndex = Convert.ToInt32(ibtn1.CommandName) + 1;
            hdRowIndex.Value = RowIndex.ToString();
            Panel1.Style.Add("display", "none");
            Panel2.Style.Add("display", "block");
            for (int i = 0; i < rbtOccupants.Items.Count; i++)
            {
                if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "SingleOccupancy")
                {
                    DisplayInfo(1);
                }
                else if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "DoubleOccupancy")
                {
                    DisplayInfo(2);
                }
                else if (rbtOccupants.Items[i].Selected && rbtOccupants.Items[i].Value == "FourOccupancy")
                {
                    DisplayInfo(4);
                }
            }
            pnlCustomer.Visible = true;
            btnSearch_Click(sender, e);
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindHotel();

        }
        protected void ddlHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            STSPLOrOther stclsObj = new STSPLOrOther();
            List<GetHotelNames_SPResult> lResult = null;
            List<GetHotelNames_SPResult> ltempResult = null;
            hdnTimeSlot.Value = "812hrs";
            lblNote.Text = "CheckIn/CheckOut Time : 8 AM/12 Noon.";// or 24 Hr.";
            try
            {

                lResult = stclsObj.fnGetHotelNames(0, Convert.ToInt32(ddlCity.SelectedValue));
                if (lResult != null && lResult.Count > 0)
                {

                    ltempResult = new List<GetHotelNames_SPResult>();
                    ltempResult = (from templilResult in lResult
                                   where templilResult.HotelID == Convert.ToInt32(ddlHotel.SelectedValue)
                                   select templilResult).ToList();
                    if (ltempResult != null && ltempResult.Count > 0)
                    {
                        if (Convert.ToBoolean(ltempResult[0].Is24Hrs))
                        {
                            hdnTimeSlot.Value = "24hrs"; // 812hrs
                            lblNote.Text = "CheckIn/CheckOut Time : 24 Hr.";
                        }
                        stax.Value = Convert.ToString(ltempResult[0].STax);
                        LTstax.Value = Convert.ToString(ltempResult[0].LTax);
                    }
                }
                BindOccupants();
            }
            finally
            {
                if (lResult != null)
                {
                    lResult = null;
                }
                if (ltempResult != null)
                {
                    ltempResult = null;
                }
                if (stclsObj != null)
                {
                    stclsObj = null;
                }
            }
        }
        protected void btnGuestDetails_Click(object sender, EventArgs e)
        {
            if (validateGuestDetails())
            {
                MakeReadonlyGuestGridItems();
                PnlGV.Visible = true;
                PnlCh.Enabled = false;
                btnContinue.Visible = false;
                hdnTotalCWB.Value = Convert.ToString(GetTotalChildWihtouBed());
                Bindroomtype();
            }
        }
        protected void grdGuestDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblGuestType = (Label)e.Row.FindControl("lblGuetType");
                TextBox txtTempname = (TextBox)e.Row.FindControl("txtName");
                TextBox txttempage = (TextBox)e.Row.FindControl("txtAge");
                DropDownList ddlGender = (DropDownList)e.Row.FindControl("ddlGender");

            }
        }
        protected void btnReSetGuest_Click(object sender, EventArgs e)
        {
            BindGuestGrid();
            PnlGV.Visible = false;
        }
        void ucManageCustomer1_btnSearchClick(object sender, ImageClickEventArgs e)
        {
            TextBox txtEmail = (TextBox)ucManageCustomer1.FindControl("emailid");
            pnl2.Visible = true;
        }
        protected void rbtOccupants_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRooms_SelectedIndexChanged(sender, e);
        }
        #endregion
        #region "Methods"
        private void ControlReadOnly()
        {
            //txtadult.ReadOnly = true;
            // txtchild.ReadOnly = true;
            ddlarrivalhr.Enabled = false;
            ddlarrivalmin.Enabled = false;
            ddlarrivalam.Enabled = false;
            ddlDeparture.Enabled = false;
            ddlDeparturemin.Enabled = false;
            ddlDepartuream.Enabled = false;
            ddlNoRoom.Enabled = false;
            ddlAdults.Enabled = false;
            ddlChilds.Enabled = false;



        }
        private void ControlnotReadOnly()
        {

            ddlarrivalhr.Enabled = true;
            ddlarrivalmin.Enabled = true;
            ddlarrivalam.Enabled = true;
            ddlDeparture.Enabled = true;
            ddlDeparturemin.Enabled = true;
            ddlDepartuream.Enabled = true;
            ddlNoRoom.Enabled = true;
            ddlAdults.Enabled = true;
            ddlChilds.Enabled = true;
            Panel1.Style.Add("display", "block");
            pnl2.Visible = false;
            //Panel2.Visible = false;
            Panel2.Style.Add("display", "none");

        }
        private void paymodes()
        {
            #region Optimize Code
            /* DataTable dtpaymodes = DataLib.GetDataTableWithoutParamWithSP(DataLib.Connection.ConnectionString, "sp_GetPaymentMode");*/
            #endregion
            pvclsOth = null;
            pclsObj = null;
            DataTable dtpaymodes = null;
            try
            {
                pvclsOth = new STSPLOrOther();
                dtpaymodes = pvclsOth.fnGetPaymentMode();
                if (dtpaymodes.Rows.Count > 0)
                {
                    //DDLPaymode.Items.Clear();
                    //DDLPaymode.DataTextField = "PaymentMode";
                    //DDLPaymode.DataValueField = "Rowid";
                    //DDLPaymode.DataSource = dtpaymodes;
                    //DDLPaymode.DataBind();
                }
                pclsObj = new ClsAdo();
                stax.Value = Convert.ToString(pclsObj.fnGetServiceTaxValue("Acc"));
                LTstax.Value = Convert.ToString(pclsObj.fnGetServiceTaxValue("LT"));
            }
            finally
            {
                if (pvclsOth != null)
                {
                    pvclsOth = null;
                }
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (dtpaymodes != null)
                {
                    dtpaymodes.Dispose();
                }
            }
        }
        private void Bindroomtype_old()
        {
            DataTable dtpickup = null;
            pvclsOth = null;
            try
            {
                pvclsOth = new STSPLOrOther();
                dtpickup = pvclsOth.fnHotelRoomTypes();
                if (dtpickup.Rows.Count > 0)
                {
                    gvRoomType.DataSource = dtpickup;
                    gvRoomType.DataBind();
                }
            }
            finally
            {
                if (pvclsOth != null)
                {
                    pvclsOth = null;
                }
                if (dtpickup != null)
                {
                    dtpickup = null;
                }
            }
        }
        private void Bindroomtype()
        {
            DataTable ldtFare = new DataTable();
            ldtFare.Columns.Add("RoomTypeID", typeof(int));
            ldtFare.Columns.Add("SeasonID", typeof(int));
            ldtFare.Columns.Add("FareID", typeof(int));
            ldtFare.Columns.Add("FromDate", typeof(DateTime));
            ldtFare.Columns.Add("ToDate", typeof(DateTime));
            ldtFare.Columns.Add("RoomType", typeof(string));
            ldtFare.Columns.Add("Fare", typeof(string));
            ldtFare.Columns.Add("ExtraBedFare", typeof(string));
            ldtFare.Columns.Add("ChildWithoutBedFare", typeof(string));

            List<GetHotelRooms_Fare_SPResult> lResult = null;

            STSPLOrOther lSTPLOther = null;
            int lTypeID = 0, lSeasonID = 0;
            string[] lSd = txtarr.Text.Split('/');
            DateTime lChekInDate = new DateTime(Convert.ToInt32(lSd[2]), Convert.ToInt32(lSd[1]), Convert.ToInt32(lSd[0]));
            try
            {
                lSTPLOther = new STSPLOrOther();
                lResult = lSTPLOther.fnGetHotelRooms_Fares(Convert.ToInt32(ddlHotel.SelectedValue), lChekInDate).OrderBy(x => x.TypeID).ToList();

                if (lResult != null && lResult.Count > 0)
                {

                    DataRow drNew = null;
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtFare.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].SingleOccupancy) && rbtOccupants.SelectedValue == "SingleOccupancy")
                        {
                            if (Convert.ToInt32(lResult[lCtr].TypeID) != lTypeID && Convert.ToInt32(lResult[lCtr].SeasonID) != lSeasonID)
                            {
                                drNew["RoomTypeID"] = Convert.ToString(lResult[lCtr].TypeID);
                                drNew["SeasonID"] = Convert.ToString(lResult[lCtr].SeasonID);
                                drNew["FromDate"] = Convert.ToString(lResult[lCtr].FromDate);
                                drNew["ToDate"] = Convert.ToString(lResult[lCtr].ToDate);
                                drNew["RoomType"] = Convert.ToString(lResult[lCtr].TypeName);
                                drNew["Fare"] = Convert.ToString(lResult[lCtr].SglOccSellingPrice);
                                drNew["FareID"] = Convert.ToString(lResult[lCtr].FareID);
                                drNew["ExtraBedFare"] = Convert.ToString(0);
                                drNew["ChildWithoutBedFare"] = Convert.ToString(lResult[lCtr].CWBSellingPrice);
                                ldtFare.Rows.Add(drNew);
                                lTypeID = lResult[lCtr].TypeID;
                                lSeasonID = lResult[lCtr].SeasonID;
                            }
                        }
                    }
                    drNew = null;
                    lTypeID = 0; lSeasonID = 0;
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtFare.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].DoubleOccupancy) && rbtOccupants.SelectedValue == "DoubleOccupancy")
                        {
                            if (Convert.ToInt32(lResult[lCtr].TypeID) != lTypeID && Convert.ToInt32(lResult[lCtr].SeasonID) != lSeasonID)
                            {
                                drNew["RoomTypeID"] = Convert.ToString(lResult[lCtr].TypeID);
                                drNew["SeasonID"] = Convert.ToString(lResult[lCtr].SeasonID);
                                drNew["FromDate"] = Convert.ToString(lResult[lCtr].FromDate);
                                drNew["ToDate"] = Convert.ToString(lResult[lCtr].ToDate);
                                drNew["RoomType"] = Convert.ToString(lResult[lCtr].TypeName);
                                drNew["Fare"] = Convert.ToString(lResult[lCtr].SellingPrice);
                                drNew["FareID"] = Convert.ToString(lResult[lCtr].FareID);
                                drNew["ExtraBedFare"] = Convert.ToString(lResult[lCtr].ExtraBedSellingPrice);


                                drNew["ChildWithoutBedFare"] = Convert.ToString(lResult[lCtr].CWBSellingPrice);
                                ldtFare.Rows.Add(drNew);
                                lTypeID = lResult[lCtr].TypeID;
                                lSeasonID = lResult[lCtr].SeasonID;
                            }
                        }
                    }
                    drNew = null;
                    lTypeID = 0; lSeasonID = 0;
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtFare.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].FourOccupancy) && rbtOccupants.SelectedValue == "FourOccupancy")
                        {
                            if (Convert.ToInt32(lResult[lCtr].TypeID) != lTypeID && Convert.ToInt32(lResult[lCtr].SeasonID) != lSeasonID)
                            {
                                drNew["RoomTypeID"] = Convert.ToString(lResult[lCtr].TypeID);
                                drNew["SeasonID"] = Convert.ToString(lResult[lCtr].SeasonID);
                                drNew["FromDate"] = Convert.ToString(lResult[lCtr].FromDate);
                                drNew["ToDate"] = Convert.ToString(lResult[lCtr].ToDate);
                                drNew["RoomType"] = Convert.ToString(lResult[lCtr].TypeName);
                                drNew["Fare"] = Convert.ToString(lResult[lCtr].FourOccSellingPrice);
                                drNew["FareID"] = Convert.ToString(lResult[lCtr].FareID);
                                drNew["ExtraBedFare"] = Convert.ToString(lResult[lCtr].FourOccExtraBedSell);
                                drNew["ChildWithoutBedFare"] = Convert.ToString(lResult[lCtr].CWBSellingPrice);// has to change later.
                                ldtFare.Rows.Add(drNew);
                                lTypeID = lResult[lCtr].TypeID;
                                lSeasonID = lResult[lCtr].SeasonID;
                            }
                        }
                    }
                    if (ldtFare != null && ldtFare.Rows.Count > 0)
                    {
                        lblGV.Text = "";
                        gvRoomType.DataSource = ldtFare;
                        gvRoomType.DataBind();
                    }
                    else
                    {
                        lblGV.Text = "No Fare Found.";
                    }
                }
                else
                {
                    lblGV.Text = "No Fare Found.";
                }
            }
            finally
            {
                if (lSTPLOther != null)
                {
                    lSTPLOther = null;
                }
                if (ldtFare != null)
                {
                    ldtFare.Dispose();
                    ldtFare = null;
                }
            }
        }
        private void BindCityName()
        {
            DataListResponse<GetCityName_SPResult> lGetCityName = null;
            clsContractModule clsHCobj = new clsContractModule();
            try
            {
                lGetCityName = new DataListResponse<GetCityName_SPResult>();

                clsHCobj.fldConnString = DataLib.getConnectionString();
                lGetCityName = clsHCobj.fnGetCityName(0);
                if (lGetCityName.Status.Status)
                {
                    ddlCity.DataSource = lGetCityName.ResultList;
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            finally
            {
                if (clsHCobj != null)
                {
                    clsHCobj = null;
                }
                if (lGetCityName != null)
                {
                    lGetCityName = null;
                }
            }
        }
        private void BindHotel()
        {
            STSPLOrOther stclsObj = new STSPLOrOther();
            List<GetHotelNames_SPResult> lResult = null;

            try
            {

                lResult = stclsObj.fnGetHotelNames(0, Convert.ToInt32(ddlCity.SelectedValue));
                if (lResult != null && lResult.Count > 0)
                {
                    ddlHotel.Items.Clear();
                    ddlHotel.DataSource = lResult;
                    ddlHotel.DataValueField = "HotelID";
                    ddlHotel.DataTextField = "HotelName";
                    ddlHotel.DataBind();
                    ddlHotel.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            finally
            {
                if (lResult != null)
                {
                    lResult = null;
                }
                if (stclsObj != null)
                {
                    stclsObj = null;
                }
            }
        }
        private void BindOccupants()
        {
            DataTable ldtOccupancy = new DataTable();
            ldtOccupancy.Columns.Add("Occupancy", typeof(string));
            ldtOccupancy.Columns.Add("ISOccupancy", typeof(string));

            STSPLOrOther stclsObj = new STSPLOrOther();
            List<GetRoomTypeOccupancy_SPResult> lResult = null;
            try
            {
                lResult = stclsObj.fnGetRoomTypeOccupancy(Convert.ToInt32(ddlHotel.SelectedValue));
                if (lResult != null && lResult.Count > 0)
                {
                    DataRow drNew = null;
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtOccupancy.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].SingleOccupancy))
                        {
                            drNew["Occupancy"] = Convert.ToString("Single Occupancy");
                            drNew["ISOccupancy"] = "SingleOccupancy";
                            ldtOccupancy.Rows.Add(drNew);
                            break;
                        }
                    }
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtOccupancy.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].DoubleOccupancy))
                        {
                            drNew["Occupancy"] = Convert.ToString("Double Occupancy");
                            drNew["ISOccupancy"] = "DoubleOccupancy";
                            ldtOccupancy.Rows.Add(drNew);
                            break;
                        }
                    }
                    for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                    {
                        drNew = ldtOccupancy.NewRow();
                        if (Convert.ToBoolean(lResult[lCtr].FourOccupancy))
                        {
                            drNew["Occupancy"] = Convert.ToString("Four Occupancy");
                            drNew["ISOccupancy"] = "FourOccupancy";
                            ldtOccupancy.Rows.Add(drNew);
                            break;
                        }
                    }
                    rbtOccupants.DataSource = ldtOccupancy;
                    rbtOccupants.DataTextField = "Occupancy";
                    rbtOccupants.DataValueField = "ISOccupancy";
                    rbtOccupants.DataBind();
                }
            }
            finally
            {
                if (lResult != null)
                {
                    lResult = null;
                }
                if (ldtOccupancy != null)
                {
                    ldtOccupancy = null;
                }
                if (stclsObj != null)
                {
                    stclsObj = null;
                }
            }
            //for (int i = 0; i < rbtOccupants.Items.Count; i++)
            //{
            //    if (rbtOccupants.Items[i].Value == "SingleOccupancy")
            //    {
            //        //if(Convert.ToBoolean(ltempResult[0])
            //        //rbtOccupants.Items[i].Selected = true;
            //    }
            //}
        }
        private void SelectPax(int lMaxA, int lMaxC)
        {
            bindAdultsChildrens(Convert.ToInt32(ddlNoRoom.SelectedValue), ddlAdults, ddlChilds, lMaxA, lMaxC);
        }
        void bindAdultsChildrens(int NoOfRooms, DropDownList ddlAdults, DropDownList ddlChildrens, int lMaxA, int lMaxC)
        {
            DataTable dtAdult = new DataTable();
            dtAdult.Columns.Add("Adult");
            dtAdult.Columns.Add("AdultValue");
            DataTable dtChild = new DataTable();
            dtChild.Columns.Add("Child");
            dtChild.Columns.Add("ChildValue");
            try
            {
                int TotalAdultsAllowed = NoOfRooms * lMaxA;// 3;
                int TotalChildsAllowed = NoOfRooms * lMaxC;// 2;
                int i = 0;
                for (i = NoOfRooms; i <= TotalAdultsAllowed; i++)
                {
                    dtAdult.Rows.Add(i, i.ToString());
                }
                for (i = 0; i <= TotalChildsAllowed; i++)
                {
                    dtChild.Rows.Add(i, i.ToString());
                }
                ddlAdults.DataSource = dtAdult;
                ddlAdults.DataTextField = "Adult";
                ddlAdults.DataValueField = "AdultValue";
                ddlAdults.DataBind();

                ddlChildrens.DataSource = dtChild;
                ddlChildrens.DataTextField = "Child";
                ddlChildrens.DataValueField = "ChildValue";
                ddlChildrens.DataBind();
            }
            finally
            {
                if (dtAdult != null)
                {
                    dtAdult.Dispose();
                    dtAdult = null;
                }
                if (dtChild != null)
                {
                    dtChild.Dispose();
                    dtChild = null;
                }
            }
        }
        private void DisplayInfo(int MaxAllow)
        {
            lvlChkI.Text = txtarr.Text;
            lvlChkO.Text = txtdep.Text;
            lblRom.Text = ddlNoRoom.SelectedValue;
            lblAdu.Text = ddlAdults.SelectedValue;
            lblChld.Text = ddlChilds.SelectedValue;
            lblChld.Text = ddlChilds.SelectedValue;
            int i = Convert.ToInt32(hdRowIndex.Value) - 1;
            Label lblrtype = (Label)gvRoomType.Rows[i].Cells[2].FindControl("lblrtype");
            HiddenField hdFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdFare");
            Label lblroomtypeId = (Label)gvRoomType.Rows[i].Cells[2].FindControl("lblroomtypeId");
            HiddenField hdExtraFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdExFare");
            HiddenField hdDisDays = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdDisDays");
            HiddenField hdDisFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdDisFare");
            HiddenField hdCWBedFare = (HiddenField)gvRoomType.Rows[i].Cells[2].FindControl("hdnCWBFare");
            if (Convert.ToInt32(hdDisDays.Value) > 0)
            {
                lblDiscountFare.Text = "Discount Fare: " + hdDisFare.Value;
            }
            decimal XetraFare = 0;
            int ExtraBed = 0/*, MaxAllow = 2*/;
            ExtraBed = Convert.ToInt32(ddlAdults.SelectedValue) - (MaxAllow * Convert.ToInt32(ddlNoRoom.SelectedValue));
            if (ExtraBed > 0)
            {
                XetraFare = Convert.ToDecimal(hdExtraFare.Value) * ExtraBed;
            }
            else
            {
                ExtraBed = 0;
            }
            int TotalChildWithoutBed = GetTotalChildWihtouBed();
            lblRomType.Text = lblrtype.Text;
            lblFare.Text = hdFare.Value;
            lblExtra.Text = XetraFare.ToString();
            lblDay.Text = txtnodays1.Value;
            //  lblCWBedFare.Text = (Convert.ToDecimal(hdCWBedFare.Value) * TotalChildWithoutBed).ToString();
            if (TotalChildWithoutBed > 0)
                lblCWBedFare.Text = Convert.ToDecimal(hdCWBedFare.Value).ToString();
            else
                lblCWBedFare.Text = "0";
        }

        private bool validateGuestDetails()
        {
            bool ret = true;
            for (int i = 0; i < grdGuestDetails.Rows.Count; i++)
            {
                TextBox txtTempGuestType = (TextBox)grdGuestDetails.Rows[i].FindControl("txtGuestType");
                TextBox txtTempname = (TextBox)grdGuestDetails.Rows[i].FindControl("txtName");
                TextBox txttempage = (TextBox)grdGuestDetails.Rows[i].FindControl("txtAge");
                DropDownList ddlGender = (DropDownList)grdGuestDetails.Rows[i].FindControl("ddlGender");

                if (txtTempGuestType.Text.ToLower().Contains("adult"))
                {
                    if (!string.IsNullOrEmpty(txttempage.Text))
                    {
                        if (Convert.ToInt32(txttempage.Text) < 11)
                        {
                            ClsCommon.ShowAlert("Adult Guest Age should be > 10");
                            ret = false;
                            break;
                        }
                    }
                }
                else if (txtTempGuestType.Text.ToLower().Contains("child"))
                {
                    if (!string.IsNullOrEmpty(txttempage.Text))
                    {
                        if (Convert.ToInt32(txttempage.Text) > 10)
                        {
                            ClsCommon.ShowAlert("Child Guest Age should be between 6-10");
                            ret = false;
                            break;
                        }
                    }

                }
            }
            return ret;

        }

        private int GetTotalChildWihtouBed()
        {
            int TotalchildwithoutBed = 0;
            try
            {
                for (int i = 0; i < grdGuestDetails.Rows.Count; i++)
                {
                    TextBox txtTempGuestType = (TextBox)grdGuestDetails.Rows[i].FindControl("txtGuestType");
                    TextBox txtTempname = (TextBox)grdGuestDetails.Rows[i].FindControl("txtName");
                    TextBox txttempage = (TextBox)grdGuestDetails.Rows[i].FindControl("txtAge");
                    DropDownList ddlGender = (DropDownList)grdGuestDetails.Rows[i].FindControl("ddlGender");

                    if (txtTempGuestType.Text.ToLower().Contains("child"))
                    {
                        if (!string.IsNullOrEmpty(txttempage.Text))
                        {
                            if (Convert.ToInt32(txttempage.Text) <= 10 && Convert.ToInt32(txttempage.Text) >= 6)
                            {
                                TotalchildwithoutBed = TotalchildwithoutBed + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TotalchildwithoutBed = 0;
            }
            return TotalchildwithoutBed;

        }

        private string GetGuestDetailsstring()
        {
            string gueststr = "";
            for (int i = 0; i < grdGuestDetails.Rows.Count; i++)
            {
                TextBox txtTempGuestType = (TextBox)grdGuestDetails.Rows[i].FindControl("txtGuestType");
                TextBox txtTempname = (TextBox)grdGuestDetails.Rows[i].FindControl("txtName");
                TextBox txttempage = (TextBox)grdGuestDetails.Rows[i].FindControl("txtAge");
                DropDownList ddlGender = (DropDownList)grdGuestDetails.Rows[i].FindControl("ddlGender");
                gueststr = gueststr + txtTempname.Text + "~" + txttempage.Text + "~" + ddlGender.SelectedValue + "#";
            }
            gueststr = gueststr.TrimEnd('#');
            return gueststr;
        }

        private void MakeReadonlyGuestGridItems()
        {
            for (int i = 0; i < grdGuestDetails.Rows.Count; i++)
            {
                TextBox txtTempGuestType = (TextBox)grdGuestDetails.Rows[i].FindControl("txtGuestType");
                TextBox txtTempname = (TextBox)grdGuestDetails.Rows[i].FindControl("txtName");
                TextBox txttempage = (TextBox)grdGuestDetails.Rows[i].FindControl("txtAge");
                DropDownList ddlGender = (DropDownList)grdGuestDetails.Rows[i].FindControl("ddlGender");

                txtTempname.Enabled = false;
                txttempage.Enabled = false;
                ddlGender.Enabled = false;
            }

        }

        private bool OccupationGiven()
        {
            bool ret = true;
            GroupLeader pManageCustomer = ucManageCustomer1.fldGroupLeader;

            try
            {
                if (pManageCustomer != null && pManageCustomer.fldOccupationId == 0)
                {
                    ClsCommon.ShowAlert("Pleaese provide occupation details");
                    ret = false;
                }
                else if (pManageCustomer != null && pManageCustomer.fldOccupationId == -1)
                {
                    if (string.IsNullOrEmpty(pManageCustomer.fldOccupation))
                    {
                        ClsCommon.ShowAlert("Pleaese provide occupation details");
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.ShowAlert("Pleaese provide occupation details");
                ret = false;
            }

            return ret;
        }

        private void SetIsCalculateCWBFare()
        {
            //Note : child fare will be calculated only for southern hotel Dellhi
            if (ddlCity.SelectedItem.Text.ToLower().Contains("new delhi") && ddlHotel.SelectedItem.Text.ToLower().Contains("southern"))
            {
                hdnIsCalCWBFare.Value = "1";
            }
            else
            {
                hdnIsCalCWBFare.Value = "0";
            }
        }
        private void BindGuestGrid()
        {
            int TotalAdult = Convert.ToInt32(ddlAdults.SelectedValue);
            int TotalChild = Convert.ToInt32(ddlChilds.SelectedValue);
            DataTable ldtGuest = new DataTable();
            try
            {

                ldtGuest.Columns.Add("GuestType", typeof(string));
                DataRow drNew = null;

                for (int i = 0; i < TotalAdult; i++)
                {
                    drNew = ldtGuest.NewRow();
                    drNew["GuestType"] = "Adult";
                    ldtGuest.Rows.Add(drNew);
                }

                for (int i = 0; i < TotalChild; i++)
                {
                    drNew = ldtGuest.NewRow();
                    drNew["GuestType"] = "Child";
                    ldtGuest.Rows.Add(drNew);
                }
                pnlGuestDetails.Visible = true;
                grdGuestDetails.DataSource = ldtGuest;
                grdGuestDetails.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (ldtGuest != null)
                {
                    ldtGuest.Dispose();
                    ldtGuest = null;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [WebMethod]
        public static string[] GetCity(string prefixText, int count, string contextKey)
        {
            string lStateID1 = "";
            if (contextKey != "0")
                lStateID1 = contextKey;

            GST_Data obj = new GST_Data();
            List<GST_GetCityListByStateNameAndSearchedCityTextResult> objCity = obj.GST_GetCityListByStateNameAndSearchedCityText(prefixText, lStateID1);
            List<string> txtItems = new List<string>();
            String dbValues;
            foreach (GST_GetCityListByStateNameAndSearchedCityTextResult item in objCity)
            {
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.CityName.ToString(), item.CityID.ToString());
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        #endregion
    }
}