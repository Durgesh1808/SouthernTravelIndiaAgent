using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AccommodationCancellation : System.Web.UI.Page
    {
        #region "Member variables"
        protected string CheckInDate;
        protected DateTime CheckInDateSQL;
        ClsAdo pvclsObj = null;
        STSPLOrOther pvvClsSpl = null;
        protected string tblPrintBG = "";
        #endregion
        #region "Event's"
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckInDate = txtCheckindate.Value.ToString();
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }
            if (CheckInDate == "")
            {
            }
            else
            {
                string[] DateArr = new string[3];
                char[] splitter = { '/' };
                DateArr = CheckInDate.Split(splitter);
                CheckInDateSQL = new DateTime(Convert.ToInt32(DateArr[2]), Convert.ToInt32(DateArr[1]),
                    Convert.ToInt32(DateArr[0]));
            }
            if (!Page.IsPostBack)
            {
                hideTables();
                btnPartialTicket.Visible = false;
            }
            this.btnsubmit.Attributes.Add("onclick", "javascript:return validation();");
            //btnPartialTicket.Attributes.Add("onclick", "javascript:return checkradio();");
            btncantic.Attributes.Add("OnClick", "return chkterms();");
            Session["TktCode"] = DataLib.funClear(txtticketno.Text);
            Session["Mail"] = "No";
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            GetTktInfo();
        }
        protected void btncantic_Click(object sender, EventArgs e)
        {
            btncantic.Enabled = false;
            btncantic.Visible = false;
            string pReturnStatus = "";
            if (chkAccept.Checked == true)
            {
                lblerrmsg.Text = "";
                DateTime lCheckinDate;
                if (txtCheckindate.Value.Trim() == "")
                {
                    lblerrmsg.Text = "Enter Checkin date.";
                    return;
                }
                else
                {
                    string[] strDate = txtCheckindate.Value.Split('/');

                    lCheckinDate = new DateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                        Convert.ToInt32(strDate[0]));

                    lblerrmsg.Text = "";

                }
                pvvClsSpl = new STSPLOrOther();
                DataTable ldtAccDetail = pvvClsSpl.fnAcccancellationTKTInfo(txtpNRo.Text, txtticketno.Text, txtemail.Text, lCheckinDate);

                string isCanceled = ViewState["Cancelled"].ToString();
                if (ldtAccDetail != null && ldtAccDetail.Rows.Count > 0)
                {
                    isCanceled = Convert.ToString(ldtAccDetail.Rows[0]["cancelled"]);
                }

                if (isCanceled == "False")
                {
                    pReturnStatus = cancelticket(Convert.ToString(ViewState["TicketNo"]));
                    //chkOverrideCancel_Chk();
                    if (pReturnStatus == "Success")
                    //ViewCancelTicket();
                    {
                        Session["Mail"] = "Yes";
                        lblticket.Text = HotelCacReceiptticket(txtticketno.Text.Trim()).ToString(); ;
                    }
                    else
                    {
                        lblerrmsg.Text = "Please fill all mandatory fields.";
                    }
                }
                else
                {
                    lblerrmsg.Text = "Ticket already Cancelled.";
                    lblticket.Text = HotelCacReceiptticket(ldtAccDetail.Rows[0]["ticketno"].ToString()).ToString();
                }

            }
            else
            {
                btncantic.Enabled = true;
                btncantic.Visible = true;
                lblerrmsg.Text = "You are not agreed our terms and conditions.";
                ClientScript.RegisterStartupScript(GetType(), "Error", "<Script>alert('You Must Agree the Terms and Conditions Before Cancellation')</script>");
            }

        }
        #endregion
        #region "Methods"
        private void GetTktInfo()
        {
            tblRuleRegulation.Visible = false;

            DateTime lCheckinDate;
            if (txtCheckindate.Value.Trim() == "")
            {
                lblerrmsg.Text = "Enter Checkin date.";
                return;
            }
            else
            {
                string[] strDate = txtCheckindate.Value.Split('/');

                lCheckinDate = new DateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]),
                    Convert.ToInt32(strDate[0]));

                lblerrmsg.Text = "";

            }
            pvvClsSpl = null;
            pvclsObj = null;
            DataTable ldtAccDetail = null;
            DataTable dtHtNoroom = null;
            DataTable ldtRecSetPay = null;
            try
            {
                pvclsObj = new ClsAdo();
                pvvClsSpl = new STSPLOrOther();
                ldtAccDetail = pvvClsSpl.fnAcccancellationTKTInfo(txtpNRo.Text, txtticketno.Text, txtemail.Text, lCheckinDate);

                if (ldtAccDetail != null && ldtAccDetail.Rows.Count > 0)
                {
                    if (Convert.ToString(ldtAccDetail.Rows[0]["AgentId"]) == Convert.ToString(Session["AgentId"]))
                    {
                        // chkOverrideCancel_Chk();
                        lblTicketNo.Text = ldtAccDetail.Rows[0]["ticketno"].ToString();
                        lblPnr.Text = ldtAccDetail.Rows[0]["OrderID"].ToString();
                        DateTime dtChkInDate = Convert.ToDateTime(ldtAccDetail.Rows[0]["ArrTime"].ToString());
                        DateTime dtChkoutDate = Convert.ToDateTime(ldtAccDetail.Rows[0]["depttime"].ToString());
                        lblCheckinDate.Text = dtChkInDate.ToString("dd-MMM-yyyy HH:mm tt");
                        lblCheckoutDate.Text = dtChkoutDate.ToString("dd-MMM-yyyy HH:mm tt");


                        lblName.Text = ldtAccDetail.Rows[0]["firstname"].ToString();

                        lblAddress.Text = ldtAccDetail.Rows[0]["addr1"].ToString();
                        lblMbl.Text = ldtAccDetail.Rows[0]["Mobile"].ToString();
                        /*lblNoPax.Text = Convert.ToString(Convert.ToInt32(ldtAccDetail.Rows[0]["noofadults"]))+" Adults" +
                            Convert.ToString(Convert.ToInt32(ldtAccDetail.Rows[0]["noofchildren"]) + " Child's ");*/

                        lblNoPax.Text = Convert.ToString(Convert.ToInt32(ldtAccDetail.Rows[0]["noofadults"])) + " Adults " +
                            Convert.ToString(Convert.ToInt32(ldtAccDetail.Rows[0]["noofchildren"]) + " Child's ");

                        lblNoDays.Text = ldtAccDetail.Rows[0]["noofdays"].ToString();
                        lblAmount.Text = ldtAccDetail.Rows[0]["Totalamount"].ToString();
                        lblServiceTax.Text = ldtAccDetail.Rows[0]["STaxValue"].ToString();
                        lblTotalAmount.Text = ldtAccDetail.Rows[0]["AmtWithTax"].ToString();
                        string pm = ldtAccDetail.Rows[0]["PayMode"].ToString();
                        if (pm.Trim() != "AgentCash")
                        {
                            ldtRecSetPay = pvclsObj.fnPayMode(pm);
                            if (ldtRecSetPay != null)
                            {
                                pm = Convert.ToString(ldtRecSetPay.Rows[0][0]);
                            }
                        }

                        lblPaymentMode.Text = pm;
                        ViewState["PaymentMode"] = pm;
                        lblBranchCode.Text = ldtAccDetail.Rows[0]["Branchcode"].ToString();

                        //dtHtNoroom = pvclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(ldtAccDetail.Rows[0]["RowID"]));
                        ViewState["HotelID"] = "0";

                        if (ldtAccDetail.Rows[0]["HotelID"] != DBNull.Value && Convert.ToInt32(ldtAccDetail.Rows[0]["HotelID"]) > 0)
                        {
                            ViewState["HotelID"] = ldtAccDetail.Rows[0]["HotelID"].ToString();

                            dtHtNoroom = pvclsObj.fnGetRoomTypeOccupancyNew(Convert.ToInt32(ldtAccDetail.Rows[0]["RowID"]));
                            lblST.Text = Convert.ToString(ldtAccDetail.Rows[0]["STAX"].ToString());
                            lblLT.Text = Convert.ToString(ldtAccDetail.Rows[0]["LTAX"].ToString());
                        }
                        else
                        {

                            dtHtNoroom = pvclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(ldtAccDetail.Rows[0]["RowID"]));
                            lblST.Text = Convert.ToString(pvclsObj.fnGetServiceTaxValue("Acc")) + " % ";
                            lblLT.Text = Convert.ToString(pvclsObj.fnGetServiceTaxValue("LT")) + " %";
                        }

                        bool flag = false;
                        int noofroom = 0;
                        if (dtHtNoroom.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtHtNoroom.Rows.Count; k++)
                            {

                                noofroom = noofroom + Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"].ToString());
                            }
                        }
                        lblNoRoom.Text = noofroom.ToString();

                        DateTime CheckIndate = Convert.ToDateTime(ldtAccDetail.Rows[0]["ArrTime"].ToString());

                        TimeSpan tspan = new TimeSpan();
                        tspan = CheckIndate.Subtract(DateTime.Now);
                        int NoofDaysleft = Convert.ToInt32(tspan.TotalHours);

                        // decimal cancelpercen = Convert.ToDecimal(canpercentage(NoofDaysleft));
                        decimal cancelpercen = 0;
                        decimal stvalue = Convert.ToDecimal(ldtAccDetail.Rows[0]["staxValue"].ToString());
                        decimal cancelcharges = decimal.Round((Convert.ToDecimal(ldtAccDetail.Rows[0]["TotalAmount"]) + stvalue) *
                            (Convert.ToDecimal(cancelpercen) / 100));
                        lblCancellationCharge.Text = cancelcharges.ToString();

                        decimal RefundAmount = 0;
                        if (Convert.ToInt32(Session["AgentId"]) > 0)
                        {
                            lblAdvance.Text = ldtAccDetail.Rows[0]["AmtWithTax"].ToString();

                            RefundAmount = (Convert.ToDecimal(ldtAccDetail.Rows[0]["AmtWithTax"]) - cancelcharges);
                            lblRefundAmount.Text = RefundAmount.ToString();
                            ViewState["Advance"] = ldtAccDetail.Rows[0]["AmtWithTax"].ToString();
                        }
                        else
                        {
                            RefundAmount = (Convert.ToDecimal(ldtAccDetail.Rows[0]["Advance"]) - cancelcharges);
                            lblRefundAmount.Text = RefundAmount.ToString();

                            lblAdvance.Text = ldtAccDetail.Rows[0]["Advance"].ToString();
                            ViewState["Advance"] = ldtAccDetail.Rows[0]["Advance"].ToString();
                        }
                        //pvclsObj = new clsLinqtoDB();
                        //lblST.Text = Convert.ToString(pvclsObj.fnGetServiceTaxValue("Acc")) + " % ";
                        //lblLT.Text = Convert.ToString(pvclsObj.fnGetServiceTaxValue("LT")) + " %";



                        trCancelView.Visible = true;
                        BalanceHide.Visible = false;
                        ViewState["CheckinDate"] = ldtAccDetail.Rows[0]["ArrTime"].ToString();
                        ViewState["Cancelled"] = Convert.ToString(ldtAccDetail.Rows[0]["cancelled"]);
                        ViewState["TicketNo"] = ldtAccDetail.Rows[0]["ticketno"].ToString();
                        ViewState["STaxValue"] = ldtAccDetail.Rows[0]["staxValue"].ToString();
                        ViewState["Amount"] = ldtAccDetail.Rows[0]["TotalAmount"].ToString();

                        ViewState["AgentId"] = Session["AgentId"];
                        ViewState["AgentCredit"] = ldtAccDetail.Rows[0]["AgentCredit"].ToString();
                        ViewState["AgentDebit"] = ldtAccDetail.Rows[0]["AgentDebit"].ToString();
                        ViewState["TDS"] = ldtAccDetail.Rows[0]["TDS"].ToString();
                        ViewState["agentID"] = Convert.ToString(Session["AgentId"]);
                        ViewState["BranchCode"] = Convert.ToString(ldtAccDetail.Rows[0]["BranchCode"]);

                        ViewState["UserName"] = Convert.ToString(ldtAccDetail.Rows[0]["UserName"]);
                        ViewState["NoOfpax"] = Convert.ToString(Convert.ToInt32(ldtAccDetail.Rows[0]["noofadults"]) +
                        Convert.ToInt32(ldtAccDetail.Rows[0]["noofchildren"]));
                        if (Convert.ToString(ldtAccDetail.Rows[0]["cancelled"]) != "True")
                        {
                            showTables();
                        }
                        else
                        {
                            lblticket.Text = HotelCacReceiptticket(ldtAccDetail.Rows[0]["ticketno"].ToString()).ToString(); ;//ViewCancelTicket();
                        }
                    }
                    else
                    {
                        lblerrmsg.Text = "You Don't Have Permission to Cancel This Ticket";
                    }

                }
                else
                {
                    lblerrmsg.Text = "Invalid entry Please check it & try again.";
                    hideTables();
                }
            }
            finally
            {
                if (pvvClsSpl != null)
                {
                    pvvClsSpl = null;
                }
                if (pvclsObj != null)
                {
                    pvclsObj = null;
                }
                if (dtHtNoroom != null)
                {
                    dtHtNoroom.Dispose();
                    dtHtNoroom = null;
                }
                if (ldtAccDetail != null)
                {
                    ldtAccDetail.Dispose();
                    ldtAccDetail = null;
                }
                if (ldtRecSetPay != null)
                {
                    ldtRecSetPay.Dispose();
                    ldtRecSetPay = null;
                }
            }
        }
        private void hideTables()
        {
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblRuleRegulation.Visible = false;
            tblCan.Visible = false;
            //trPrint.Visible = false;
            tblChk.Visible = false;
            trTicketTerms.Visible = false;
        }
        private void showCancelTable()
        {
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblCan.Visible = true;
            tblChk.Visible = false;
            tblRuleRegulation.Visible = true;
            trCancelView.Visible = false;
            //trPrint.Visible = true;
            trTicketView.Visible = false;
            trTicketTerms.Visible = false;
            tblPrintBG = "Y";
            BalanceHide.Visible = false;
        }

        private StringBuilder HotelCacReceiptticket(string orderid)
        {

            string snoofpersons, TypeofRooms = "", rommrentperday = "";

            DateTime sBookingDate, sdepttime, sarrtime;
            DateTime srrivaldate, sdeparturedate;
            DataTable ldtRecSet = null;
            DataSet dtFare = null;
            DataTable dtterm = null;
            DataSet ldsRecSet = null;
            decimal sfareor;
            int totalCWB = 0;
            decimal CWBFare = 0;
            try
            {
                pvclsObj = new ClsAdo();
                ldtRecSet = pvclsObj.fnAccCancelReceipt(orderid);
                dtFare = new DataSet();
                if (ldtRecSet != null)
                {
                    dtFare.Tables.Add(ldtRecSet);
                }
                if (dtFare != null && dtFare.Tables[0].Rows.Count > 0)
                {
                    showCancelTable();
                    sBookingDate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["BookingDate"].ToString().Trim());
                    string sorderid = dtFare.Tables[0].Rows[0]["OrderId"].ToString().Trim();
                    sdepttime = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["depttime"].ToString());
                    sarrtime = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["arrtime"].ToString());

                    string snoofadults = Convert.ToString(dtFare.Tables[0].Rows[0]["noofadults"]);
                    decimal snoofchildren = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["noofchildren"].ToString().Trim());

                    snoofpersons = snoofadults + " Adults";
                    if (snoofchildren > 0)
                    {
                        snoofpersons = snoofadults + " Adults " + "+" + Convert.ToString(snoofchildren) + " Child's ";

                    }




                    string snoofrooms = dtFare.Tables[0].Rows[0]["noofrooms"].ToString().Trim();
                    string sticketno = dtFare.Tables[0].Rows[0]["ticketno"].ToString().Trim();

                    decimal Fare = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Totalamount"].ToString().Trim());
                    decimal stax = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["STaxValue"].ToString().Trim());
                    decimal stotalwithtax = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["AmtWithTax"].ToString().Trim());

                    decimal advance = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Advance"].ToString().Trim());
                    decimal sbalance = Convert.ToDecimal(stotalwithtax - advance);

                    DateTime Bookingdate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["Bookingdate"].ToString().Trim());

                    DateTime Canceldate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["CancelledOn"].ToString().Trim());
                    DataTable dtHtNoroom = null;
                    string lHotelID = Convert.ToString(ldtRecSet.Rows[0]["HotelID"]);
                    if (Convert.ToInt32(lHotelID) > 0)
                    {
                        dtHtNoroom = pvclsObj.fnGetRoomTypeOccupancyNew(Convert.ToInt32(ldtRecSet.Rows[0]["RowID"]));
                    }
                    else
                    {

                        dtHtNoroom = pvclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(ldtRecSet.Rows[0]["RowID"]));
                    }
                    bool flag = false;
                    int noofroom = 0;
                    int ExtraBed = 0;
                    decimal ExtraFare = 0;
                    string pickveh, pickvehno, pickuptime, dropveh, dropvehno, droptime, PickupFrom = "", DropAt = "";
                    string strPkStation = "", strDPStation = "";

                    pickveh = Convert.ToString(dtFare.Tables[0].Rows[0]["PickUpVeh"]);
                    pickvehno = Convert.ToString(dtFare.Tables[0].Rows[0]["PickVehNo"]);
                    pickuptime = Convert.ToString(dtFare.Tables[0].Rows[0]["PickTime"]);
                    dropveh = Convert.ToString(dtFare.Tables[0].Rows[0]["DropVeh"]);
                    dropvehno = Convert.ToString(dtFare.Tables[0].Rows[0]["DropVehNo"]);
                    droptime = Convert.ToString(dtFare.Tables[0].Rows[0]["DropTime"]);
                    strPkStation = Convert.ToString(dtFare.Tables[0].Rows[0]["pkstation"]);
                    strDPStation = Convert.ToString(dtFare.Tables[0].Rows[0]["station"]);
                    string rmarks = Convert.ToString(dtFare.Tables[0].Rows[0]["Remarks"]);



                    if (pickveh == "Flight")
                    {
                        PickupFrom = strPkStation;
                        //pickvehno = "Flight No: " + pickvehno;
                    }
                    else if (pickveh == "Train")
                    {
                        PickupFrom = strPkStation; ;
                        // pickvehno = "Train No: " + pickvehno;
                    }
                    else if (pickveh == "Bus")
                    {
                        PickupFrom = strPkStation; ;
                        //PickupFrom = "Bus Stand";
                        // pickvehno = pickvehno;
                    }
                    if (dropveh == "Flight")
                    {
                        DropAt = strDPStation;
                        // dropvehno = "Flight No:" + dropvehno;
                    }
                    else if (dropveh == "Train")
                    {
                        DropAt = strDPStation;
                        // dropvehno = "Train No:" + dropvehno;
                    }
                    else if (dropveh == "Bus")
                    {
                        DropAt = strDPStation;
                        //  DropAt = "Location/Address";
                        // dropvehno =  dropvehno;
                    }


                    StringBuilder strPickupInfo = new StringBuilder();
                    if (PickupFrom != "Select")
                    {
                        if (pickveh == "Flight")
                        {
                            strPickupInfo.Append("<TR>");
                            strPickupInfo.Append("<TD><SPAN class=cgi>Pick Up From: </SPAN><SPAN class=hlinks>");
                            strPickupInfo.Append(PickupFrom);
                            strPickupInfo.Append("</SPAN></TD>");
                            strPickupInfo.Append("<TD>");
                            strPickupInfo.Append("<SPAN class=cgi>Flight No: </SPAN> <SPAN class=hlinks>");
                            strPickupInfo.Append(pickvehno);
                            strPickupInfo.Append("</SPAN>");
                            strPickupInfo.Append("</TD>");
                            strPickupInfo.Append("<TD><SPAN class=cgi>Pickup Time: </SPAN> <SPAN class=hlinks>");
                            strPickupInfo.Append(pickuptime + " " + "Hrs");
                            strPickupInfo.Append("</SPAN>");
                            strPickupInfo.Append("</TD>");
                            strPickupInfo.Append("</TR>");
                        }
                        else if (pickveh == "Train")
                        {

                            strPickupInfo.Append("<TR>");
                            strPickupInfo.Append("<TD><SPAN class=cgi>Railway Station Name: </SPAN><SPAN class=hlinks>");
                            strPickupInfo.Append(PickupFrom);
                            strPickupInfo.Append("</SPAN></TD>");
                            strPickupInfo.Append("<TD>");
                            strPickupInfo.Append("<SPAN class=cgi>Train No: </SPAN> <SPAN class=hlinks>");
                            strPickupInfo.Append(pickvehno);
                            strPickupInfo.Append("</SPAN>");
                            strPickupInfo.Append("</TD>");
                            strPickupInfo.Append("<TD><SPAN class=cgi>Pickup Time: </SPAN> <SPAN class=hlinks>");
                            strPickupInfo.Append(pickuptime + " " + "Hrs");
                            strPickupInfo.Append("</SPAN>");
                            strPickupInfo.Append("</TD>");
                            strPickupInfo.Append("</TR>");
                        }
                        else if (pickveh == "Bus")
                        {

                            strPickupInfo.Append("<TR>");
                            strPickupInfo.Append("<TD ><SPAN class=cgi>Pickup Address: </SPAN><SPAN class=hlinks>");
                            strPickupInfo.Append(PickupFrom);
                            strPickupInfo.Append("</SPAN></TD>");
                            strPickupInfo.Append("<TD colspan=2>");
                            strPickupInfo.Append("<SPAN class=cgi>Street: </SPAN> <SPAN class=hlinks>");
                            strPickupInfo.Append(pickvehno);
                            strPickupInfo.Append("</SPAN>");
                            strPickupInfo.Append("</TD>");
                            strPickupInfo.Append("</TR>");
                        }
                    }
                    if (dropveh == "Flight")
                    {
                        strPickupInfo.Append("<TR>");
                        strPickupInfo.Append("<TD><SPAN class=cgi>Drop Address: </SPAN><SPAN class=hlinks>");
                        strPickupInfo.Append(DropAt);
                        strPickupInfo.Append("</SPAN></TD>");
                        strPickupInfo.Append("<TD>");
                        strPickupInfo.Append("<SPAN class=cgi> Flight No: </SPAN> <SPAN class=hlinks>");
                        strPickupInfo.Append(dropvehno);
                        strPickupInfo.Append("</SPAN>");
                        strPickupInfo.Append("</TD>");
                        strPickupInfo.Append("<TD><SPAN class=cgi>Drop Time:</SPAN> <SPAN class=hlinks>");
                        strPickupInfo.Append(droptime + " " + "Hrs");
                        strPickupInfo.Append("</SPAN>");
                        strPickupInfo.Append("</TD>");
                        strPickupInfo.Append("</TR>");
                    }

                    else if (dropveh == "Train")
                    {
                        strPickupInfo.Append("<TR>");
                        strPickupInfo.Append("<TD><SPAN class=cgi>Railway Station Name: </SPAN><SPAN class=hlinks>");
                        strPickupInfo.Append(DropAt);
                        strPickupInfo.Append("</SPAN></TD>");
                        strPickupInfo.Append("<TD>");
                        strPickupInfo.Append("<SPAN class=cgi> Train No: </SPAN> <SPAN class=hlinks>");
                        strPickupInfo.Append(dropvehno);
                        strPickupInfo.Append("</SPAN>");
                        strPickupInfo.Append("</TD>");
                        strPickupInfo.Append("<TD><SPAN class=cgi>Drop Time:</SPAN> <SPAN class=hlinks>");
                        strPickupInfo.Append(droptime + " " + "Hrs");
                        strPickupInfo.Append("</SPAN>");
                        strPickupInfo.Append("</TD>");
                        strPickupInfo.Append("</TR>");
                    }
                    else if (dropveh == "Bus")
                    {
                        strPickupInfo.Append("<TR>");
                        strPickupInfo.Append("<TD><SPAN class=cgi>Drop Address: </SPAN><SPAN class=hlinks>");
                        strPickupInfo.Append(DropAt);
                        strPickupInfo.Append("</SPAN></TD>");
                        strPickupInfo.Append("<TD colspan=2>");
                        strPickupInfo.Append("<SPAN class=cgi> Street: </SPAN> <SPAN class=hlinks>");
                        strPickupInfo.Append(dropvehno);
                        strPickupInfo.Append("</SPAN>");
                        strPickupInfo.Append("</TD>");

                        strPickupInfo.Append("</TR>");
                    }

                    StringBuilder lRoomType = new StringBuilder();
                    lRoomType.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");

                    //totalCWB = Convert.ToInt32(dtHtNoroom.Rows[0]["TotalChildWB"]);
                    //CWBFare = Convert.ToInt32(dtHtNoroom.Rows[0]["ChildWbFare"]);
                    totalCWB = Convert.ToInt32(Convert.ToString(dtHtNoroom.Rows[0]["TotalChildWB"]) == "" ? 0 : dtHtNoroom.Rows[0]["TotalChildWB"]);
                    CWBFare = Convert.ToInt32(Convert.ToString(dtHtNoroom.Rows[0]["ChildWbFare"]) == "" ? 0 : dtHtNoroom.Rows[0]["ChildWbFare"]);


                    if (dtHtNoroom.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtHtNoroom.Rows.Count; k++)
                        {
                            ExtraBed = ExtraBed + Convert.ToInt32(dtHtNoroom.Rows[k]["ExtraRoom"]);

                            int ExtraBed1 = Convert.ToInt32(dtHtNoroom.Rows[k]["ExtraRoom"]);

                            ExtraFare = Convert.ToDecimal(dtHtNoroom.Rows[k]["ExtraRoomFare"]);
                            if (flag == false)
                            {
                                lRoomType.Append("<TR><TD  width =15% align=\"left\"><span class=\"cgi\">Room Rent Per Day:</span></TD>");
                                if (dtHtNoroom.Rows[k]["noofroom"] != null && Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"]) > 0)
                                {
                                    TypeofRooms = dtHtNoroom.Rows[k]["RoomType"].ToString() + "<br/>";
                                    rommrentperday = " " + dtHtNoroom.Rows[k]["RoomType"].ToString() + " " + dtHtNoroom.Rows[k]["noofroom"].ToString() + "x"
                                        + dtHtNoroom.Rows[k]["Fare"].ToString() + " + " + ExtraFare + " (Extra Bed " + ExtraBed1 + ")" + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                                    flag = true;
                                }
                            }
                            else
                            {
                                lRoomType.Append("<TR><TD c align=\"left\"><span class=\"cgi\">&nbsp;</span></TD>");
                                if (dtHtNoroom.Rows[k]["noofroom"] != null && Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"]) > 0)
                                {
                                    TypeofRooms = TypeofRooms + dtHtNoroom.Rows[k]["RoomType"].ToString() + "<br/>";
                                    rommrentperday = rommrentperday + dtHtNoroom.Rows[k]["RoomType"].ToString() + " "
                                        + dtHtNoroom.Rows[k]["noofroom"].ToString() + "x" + dtHtNoroom.Rows[k]["Fare"].ToString() + " + " + ExtraFare + " (Extra Bed " + ExtraBed1 + ")" + "<br/>";

                                }
                            }
                            noofroom = noofroom + Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"].ToString());
                            if (ExtraBed > 0)
                            {
                                lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + dtHtNoroom.Rows[k]["RoomType"].ToString() + " " +
                                    dtHtNoroom.Rows[k]["noofroom"].ToString() + " <b>x</b> " + dtHtNoroom.Rows[k]["Fare"].ToString() + " + " +
                                    ExtraFare + " (Extra Bed " + ExtraBed1 + ")" + "</span></TD>");
                            }
                            else
                            {
                                lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + dtHtNoroom.Rows[k]["RoomType"].ToString() + " " +
                                    dtHtNoroom.Rows[k]["noofroom"].ToString() + " <b>x</b> " + dtHtNoroom.Rows[k]["Fare"].ToString() + "</span></TD>");
                            }
                        }
                        //---Adding Cwb Fare details
                        if (totalCWB > 0)
                        {
                            lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + "+" + Convert.ToString(CWBFare * totalCWB) + " (" + "Child WB&nbsp;&nbsp;" + Convert.ToString(totalCWB) + ")" + "</span></TD>");
                        }
                        lRoomType.Append("</TR>");

                    }
                    lRoomType.Append("</TABLE>");
                    srrivaldate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["arrtime"].ToString().Trim());
                    sdeparturedate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["depttime"].ToString().Trim());




                    int i = 1;


                    StringBuilder strTable = new StringBuilder();
                    strTable.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() +
                        "\\AccCancelTKT.htm")));
                    strTable = strTable.Replace("#canceldate", Convert.ToString(Canceldate.ToString("dd-MMM-yyyy hh:mmtt")));
                    strTable = strTable.Replace("#bookDate", Convert.ToString(Bookingdate.ToString("dd-MMM-yyyy hh:mmtt")));
                    strTable = strTable.Replace("#cancelno", Convert.ToString(dtFare.Tables[0].Rows[0]["CanNo"].ToString()));
                    strTable = strTable.Replace("#pnr", sorderid);
                    strTable = strTable.Replace("#arrival", srrivaldate.ToString("dd-MMM-yyyy"));
                    strTable = strTable.Replace("#arrival", srrivaldate.ToString("dd-MMM-yyyy"));
                    strTable = strTable.Replace("#arrtime", srrivaldate.ToShortTimeString());
                    strTable = strTable.Replace("#departure", sdeparturedate.ToString("dd-MMM-yyyy"));
                    strTable = strTable.Replace("#departime", sdeparturedate.ToShortTimeString());


                    strTable = strTable.Replace("#vocher", dtFare.Tables[0].Rows[0]["ticketno"].ToString());
                    strTable = strTable.Replace("#name", dtFare.Tables[0].Rows[0]["FirstName"].ToString());
                    strTable = strTable.Replace("#mob", dtFare.Tables[0].Rows[0]["Mobile"].ToString());
                    strTable = strTable.Replace("#add", dtFare.Tables[0].Rows[0]["Addr1"].ToString());
                    strTable = strTable.Replace("#cdate", sarrtime.ToString("dd-MMM-yyyy HH:mm tt"));
                    strTable = strTable.Replace("#codate", sdepttime.ToString("dd-MMM-yyyy HH:mm tt"));

                    try
                    {
                        if (Session["BranchId"] != null)
                        {
                            pvclsObj = new ClsAdo();
                            string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            int Val = pvclsObj.fnSaveBranchToAgentBooking(Convert.ToString(dtFare.Tables[0].Rows[0]["CanNo"].ToString()), Convert.ToInt32(Session["BranchUserId"]),
                                Convert.ToInt32(Session["AgentId"]), REMOTE_ADDR);
                        }
                    }
                    catch { }

                    //strTable = strTable.Replace("#City", "New Delhi");
                    //strTable = strTable.Replace("#HotelAddress", "Hotel Southern 18/2,Arya Samaj Road,Karol Bagh,New Delhi-110005 ");
                    string lNotes = "We will try to provide accommodation in our #HotelName and Annexure Buildings.Incase accommodation is not available in Our #HotelName, similar accommodation will be arranged in a nearby Hotel.";
                    if (ldtRecSet.Rows[0]["HotelName"] != null && Convert.ToString(ldtRecSet.Rows[0]["HotelName"]) != "")
                    {
                        strTable = strTable.Replace("#Notes", lNotes.ToString().Replace("#HotelName", Convert.ToString(ldtRecSet.Rows[0]["HotelName"].ToString())));
                    }
                    else
                    {
                        strTable = strTable.Replace("#Notes", lNotes.ToString().Replace("#HotelName", "Hotel Southern"));
                    }
                    strTable = strTable.Replace("#Notes", lNotes.ToString());

                    strTable = strTable.Replace("#noofper", snoofpersons.ToString());
                    //strTable = strTable.Replace("#rooms", noofroom.ToString() + " + Extra Bed: " + ExtraBed.ToString());//dtFare.Tables[0].Rows[0]["noofrooms"].ToString());
                    if (ExtraBed > 0)
                    {
                        strTable = strTable.Replace("#rooms", noofroom.ToString() + " + " + ExtraBed.ToString() + "(Extra Bed) ");//dtFare.Tables[0].Rows[0]["noofrooms"].ToString());
                    }
                    else
                    {
                        strTable = strTable.Replace("#rooms", noofroom.ToString());//dtFare.Tables[0].Rows[0]["noofrooms"].ToString());
                    }
                    strTable = strTable.Replace("#rentperday", lRoomType.ToString());
                    strTable = strTable.Replace("#amount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Totalamount"].ToString()))));

                    strTable = strTable.Replace("#stax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STaxValue"].ToString()))));
                    strTable = strTable.Replace("#BookingOffice", "BookingOffice");
                    //string STAcc = Convert.ToString(pvclsObj.fnGetServiceTaxValue("Acc"));
                    //string STLT = Convert.ToString(pvclsObj.fnGetServiceTaxValue("LT"));
                    string STAcc = "", STLT = "";
                    //strTable = strTable.Replace("#City", "New Delhi");
                    //strTable = strTable.Replace("#HotelAddress", "Hotel Southern 18/2,Arya Samaj Road,Karol Bagh,New Delhi-110005 ");
                    if (Convert.ToInt32(lHotelID) > 0)
                    {

                        STAcc = Convert.ToString(ldtRecSet.Rows[0]["STAX"].ToString());
                        STLT = Convert.ToString(ldtRecSet.Rows[0]["LTAX"].ToString());
                        strTable = strTable.Replace("#City", ldtRecSet.Rows[0]["CityName"].ToString());
                        strTable = strTable.Replace("#HotelAddress", Convert.ToString(ldtRecSet.Rows[0]["HotelAddress"].ToString()));
                    }
                    else
                    {
                        STAcc = Convert.ToString(pvclsObj.fnGetServiceTaxValue("Acc"));
                        STLT = Convert.ToString(pvclsObj.fnGetServiceTaxValue("LT"));
                        strTable = strTable.Replace("#City", "New Delhi");
                        strTable = strTable.Replace("#HotelAddress", "Hotel Southern 18/2,Arya Samaj Road,Karol Bagh,New Delhi-110005 ");
                    }

                    strTable = strTable.Replace("#STLT", STLT);
                    strTable = strTable.Replace("#STAcc", STAcc);

                    if (strPickupInfo != null && strPickupInfo.ToString() != "")
                    {
                        strTable = strTable.Replace("#Pickup", strPickupInfo.ToString());
                    }
                    else
                    {
                        strTable = strTable.Replace("#Pickup", "");
                    }

                    strTable = strTable.Replace("#CancellationCharges", Convert.ToString(dtFare.Tables[0].Rows[0]["CanCharges"].ToString()));
                    strTable = strTable.Replace("#refundamount", Convert.ToString(dtFare.Tables[0].Rows[0]["RefundAmt"].ToString()));

                    strTable = strTable.Replace("#Totawithtax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["AmtWithTax"].ToString()))));

                    if ((Convert.ToInt32(dtFare.Tables[0].Rows[0]["AgentId"]) > 0))
                    {
                        strTable = strTable.Replace("#BookingOffice", "Booking Agent :");
                        strTable = strTable.Replace("#branchCode", Convert.ToString(dtFare.Tables[0].Rows[0]["Branchcode1"]));

                        strTable = strTable.Replace("#cancelbranchCode", Convert.ToString(dtFare.Tables[0].Rows[0]["BranchCode"]));
                        strTable = strTable.Replace("#cancellingExecutive", "");
                        strTable = strTable.Replace("#cancelbranchUser", "");
                    }
                    else if ((Convert.ToString(dtFare.Tables[0].Rows[0]["BranchCode"]) != "EBK0001") && (Convert.ToInt32(dtFare.Tables[0].Rows[0]["AgentId"]) == 0))
                    {
                        strTable = strTable.Replace("#BookingOffice", "Booking Branch :");
                        strTable = strTable.Replace("#branchCode", Convert.ToString(dtFare.Tables[0].Rows[0]["Branchcode1"]));
                        strTable = strTable.Replace("#cancelbranchCode", Convert.ToString(dtFare.Tables[0].Rows[0]["BranchCode"]));
                        strTable = strTable.Replace("#cancellingExecutive", "Cancelled Executive: ");
                        strTable = strTable.Replace("#cancelbranchUser", Convert.ToString(dtFare.Tables[0].Rows[0]["Username"]));
                    }


                    if (Convert.ToInt32(dtFare.Tables[0].Rows[0]["AgentID"]) > 0)
                    {
                        advance = Math.Round(stotalwithtax);
                        strTable = strTable.Replace("#advance", Convert.ToString(Math.Round(advance)));
                    }
                    else
                    {
                        strTable = strTable.Replace("#advance", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Advance"].ToString()))));
                    }


                    if (rmarks != "")
                    {
                        strTable.Replace("#Remarks", rmarks);
                        strTable.Replace("#ShowRemarks", "block");

                    }
                    else
                    {
                        strTable.Replace("#ShowRemarks", "none");
                    }
                    ViewState["branchcode"] = dtFare.Tables[0].Rows[0]["Branchcode"].ToString();
                    ViewState["agentid"] = Convert.ToString(dtFare.Tables[0].Rows[0]["agentid"]);
                    ViewState["username"] = dtFare.Tables[0].Rows[0]["Username"].ToString();
                    strTable = strTable.Replace("#branchCode", dtFare.Tables[0].Rows[0]["Branchcode"].ToString());
                    strTable = strTable.Replace("#branchUser", dtFare.Tables[0].Rows[0]["Username"].ToString());

                    decimal dis = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Discount"]);

                    string PaymentMode = Convert.ToString(dtFare.Tables[0].Rows[0]["Paymode"]);
                    string chknumber = Convert.ToString(dtFare.Tables[0].Rows[0]["CheckNo"]);
                    string bank = Convert.ToString(dtFare.Tables[0].Rows[0]["bankname"]);
                    string chqdate = Convert.ToString(dtFare.Tables[0].Rows[0]["TransactionDate"]);
                    string bookMode = "";
                    if ((Convert.ToString(dtFare.Tables[0].Rows[0]["Branchcode"]) != "EBK0001") && (Convert.ToString(dtFare.Tables[0].Rows[0]["agentid"]) == "0"))
                    {
                        bookMode = "Branch";
                    }


                    sfareor = Fare;


                    string sfaredyna = Convert.ToString(Math.Round(float.Parse(sfareor.ToString())));
                    string staxdyna = Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STaxValue"].ToString())));
                    StringBuilder strTable1 = new StringBuilder();

                    if ((dis) > 0)
                    {
                        //strTable1.Append("<tr>");
                        //strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                        //strTable1.Append(" <span class=cgi>Fare&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + sfaredyna + "</span>");
                        //strTable1.Append("</td>");
                        //strTable1.Append("<td style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                        //strTable1.Append("<span class=cgi>Discount&nbsp;&nbsp;Rs&nbsp;:</span><span class=hlinks>" + dis + "</span>");
                        //strTable1.Append("</td>");
                        //strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                        //strTable1.Append(" <span class=cgi>GST&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + staxdyna + "</span>");
                        //strTable1.Append("</td>");
                        //strTable1.Append("</tr");

                        // strTable = strTable.Replace("#amount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Totalamount"].ToString()))));
                        strTable = strTable.Replace("#lblDiscount", " Special Discount: ");
                        strTable = strTable.Replace("#discount", Convert.ToString(decimal.Round(dis)));
                    }
                    else
                    {
                        strTable = strTable.Replace("#lblDiscount", "");
                        strTable = strTable.Replace("#discount", "");
                        //strTable1.Append("<tr>");
                        //strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                        //strTable1.Append(" <span class=cgi>Fare &nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + sfaredyna + "</span>");
                        //strTable1.Append("</td>");

                        //strTable1.Append("<td colspan=2 style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                        //strTable1.Append(" <span class=cgi>GST&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + staxdyna + "</span>");
                        //strTable1.Append("</td>");
                        //strTable1.Append("</tr");

                    }

                    strTable = strTable.Replace("#lblServiceTax", "GST : ");
                    strTable = strTable.Replace("#tax", Convert.ToString(dtFare.Tables[0].Rows[0]["STaxValue"]));
                    strTable = strTable.Replace("#BookingExecutive", "Booking Executive: ");
                    strTable = strTable.Replace("#branchUser", dtFare.Tables[0].Rows[0]["Username"].ToString());
                    strTable = strTable.Replace("#totalamount", Convert.ToString(decimal.Round(stotalwithtax)));

                    strTable = strTable.Replace("#noofdays", Convert.ToString(dtFare.Tables[0].Rows[0]["noofdays"].ToString()));
                    strTable.Replace("#GenerationTime", DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                    /*if (decimal.Round((stotalwithtax - advance)) > 0)
                    {
                        strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(stotalwithtax - advance)));
                    }
                    else
                    {
                        strTable = strTable.Replace("#balance", "Nil");
                    }*/
                    if (Convert.ToDecimal(ldtRecSet.Rows[0]["CanCharges"]) > advance)//(decimal.Round((stotalwithtax - advance)) > 0)
                    {
                        //strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(stotalwithtax - advance)));
                        strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(Convert.ToDecimal(ldtRecSet.Rows[0]["CanCharges"]) - advance)));
                    }
                    else
                    {
                        strTable = strTable.Replace("#balance", "Nil");
                    }
                    ldsRecSet = pvclsObj.fnGetPayModeOrTerm(sticketno);
                    if (PaymentMode.Trim().Length < 3)
                    {

                        string pm = ldsRecSet.Tables[0].Rows[0][0].ToString();
                        strTable = strTable.Replace("#PaymentMode", "PaymentMode :");
                        string details = "";
                        if (chknumber != "")
                            details = details + "<br><span class=cgi>Trans No: </span>" + chknumber;
                        if (bank != "")
                            details = details + "<br><span class=cgi>Bank: </span>" + bank;
                        if (chqdate != "")
                            details = details + "<br><span class=cgi>Date: </span>" + chqdate;

                        strTable = strTable.Replace("#Mode", pm + details);
                    }
                    else
                    {
                        strTable = strTable.Replace("#PaymentMode", "PaymentMode :");
                        strTable = strTable.Replace("#Mode", PaymentMode);
                    }

                    dtterm = ldsRecSet.Tables[1];
                    if (dtterm != null && dtterm.Rows.Count > 0)
                    {

                        //strTable.Append(dtterm.Rows[0]["HTLAccTermsCondition"].ToString());
                        strTable.Append(dtterm.Rows[0]["HTLAccTermsCondition"].ToString().Replace("#STAcc", STAcc.ToString()).Replace("#STLT", STLT.ToString()));


                    }


                    StringBuilder TicketStr = new StringBuilder();
                    TicketStr.Append("<Table id=Table2 cellSpacing=0 cellPadding=0 width=95% border=0>");
                    TicketStr.Append("<TR>");
                    TicketStr.Append("<TD>");
                    TicketStr.Append("<table width=100%>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td width=40% align=right>");
                    TicketStr.Append("<INPUT class=intdtxth id=Button1 onclick=framePrint('print_area" + i + "');  type=button value=Print name=Button1>");
                    TicketStr.Append("</TD>");
                    TicketStr.Append("</TR>");
                    TicketStr.Append("</Table>");
                    TicketStr.Append("</TR>");
                    TicketStr.Append("</TD>");
                    TicketStr.Append("<TR>");
                    TicketStr.Append("<TD align=left>");
                    TicketStr.Append("<DIV id=print_area" + i + ">");

                    //--adding guest detailstota
                    strTable = AppendGuestDetails(strTable);

                    TicketStr.Append(strTable);
                    TicketStr.Append("</DIV></TD></TR></Table>");
                    //string htdup = Request.QueryString["duplicate"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
                    if (Convert.ToString(Session["Mail"]) != "Yes")
                    {
                        SendMail(dtFare.Tables[0].Rows[0]["email"].ToString(), ConfigurationSettings.AppSettings["eTicketEmail"].ToString(), "", "CANCELLED RECEIPT FOR ACCOMMODATION", strTable.ToString(), "", orderid, Convert.ToString(ldtRecSet.Rows[0]["CityName"]));
                    }
                    return TicketStr;
                }
                else
                {
                    StringBuilder TicketStr = new StringBuilder();
                    TicketStr.Append("");
                    return TicketStr;
                }
            }
            finally
            {
                if (pvclsObj != null)
                {
                    pvclsObj = null;
                }
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
                if (dtFare != null)
                {
                    dtFare.Dispose();
                    dtFare = null;
                }
                if (dtterm != null)
                {
                    dtterm.Dispose();
                    dtterm = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
            }
        }
        private void SendMail(string To, string From, string Bcc, string subject, string body, string cc, string orderid, string lCityName)
        {
            string strRetMail = "";

            if (Convert.ToString(ViewState["agentid"]) != "")
            {
                try
                {
                    pvclsObj = new ClsAdo();

                    strRetMail = pvclsObj.fnGetEmailIDAgent(Convert.ToInt32(ViewState["agentid"]));
                }
                finally
                {
                    if (pvclsObj != null)
                    {
                        pvclsObj = null;
                    }
                }
            }
            if (Convert.ToString(ViewState["branchcode"]) != "")
            {
                try
                {
                    pvclsObj = new ClsAdo();

                    strRetMail = strRetMail + "," + pvclsObj.fnEmailSecurity(Convert.ToString(ViewState["branchcode"]), Convert.ToString(ViewState["username"]));
                }
                finally
                {
                    if (pvclsObj != null)
                    {
                        pvclsObj = null;
                    }
                }
            }


            if (To == orderid + "@temp.com")
            {
                To = "";
            }
            else if (To.Length > 23)
            {
                string k = To.Substring(19, 4);
                if (k == "temp")
                    To = "";
            }
            try
            {


                string ticketmailBCC = string.Empty;
                if (To != "")
                {
                    if (lCityName.ToUpper().Trim() == "VIJAYAWADA")
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketmailBCC = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["iticketemailAccVijaywadaCan"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketmailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                    else
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketmailBCC = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["ticketemailAccCan"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketmailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                }
                else
                {
                    To = ConfigurationSettings.AppSettings["SupportEmail"].ToString();
                    
                    if (lCityName.ToUpper().Trim() == "VIJAYAWADA")
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketmailBCC = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["iticketemailAccVijaywadaCan"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketmailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                    else
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketmailBCC = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["ticketemailAccCan"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketmailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                }
                ClsCommon.sendmail(To, ticketmailBCC, "", ConfigurationSettings.AppSettings["eTicketEmail"].ToString(), subject, body, "");
            }
            catch (Exception)
            {

            }
            finally
            {

            }


        }
        private void ViewCancelTicket()
        {
            lblerrmsg.Text = "This Ticket has been Cancelled.";
            showCancelTable();
            BalanceHide.Visible = true;
            DataTable dtHACCanInfo = null;
            DataTable dtHtNoroom = null;
            try
            {
                pvclsObj = new ClsAdo();
                dtHACCanInfo = pvclsObj.fnBranchHACCancelInfo(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(CheckInDateSQL));
                if (dtHACCanInfo != null && dtHACCanInfo.Rows.Count > 0)
                {
                    lblTicketNo.Text = dtHACCanInfo.Rows[0]["ticketno"].ToString();
                    lblPnr.Text = dtHACCanInfo.Rows[0]["OrderID"].ToString();
                    DateTime dtChkInDate = Convert.ToDateTime(dtHACCanInfo.Rows[0]["ArrTime"].ToString());
                    DateTime dtChkoutDate = Convert.ToDateTime(dtHACCanInfo.Rows[0]["depttime"].ToString());
                    lblCheckinDate.Text = dtChkInDate.ToString("dd-MMM-yyyy HH:mm tt");
                    lblCheckoutDate.Text = dtChkoutDate.ToString("dd-MMM-yyyy HH:mm tt");


                    lblName.Text = dtHACCanInfo.Rows[0]["firstname"].ToString();

                    lblAddress.Text = dtHACCanInfo.Rows[0]["addr1"].ToString();
                    lblMbl.Text = dtHACCanInfo.Rows[0]["Mobile"].ToString();
                    /*lblNoPax.Text = Convert.ToString(Convert.ToInt32(dtHACCanInfo.Rows[0]["noofadults"]) +
                        Convert.ToInt32(dtHACCanInfo.Rows[0]["noofchildren"]));*/
                    lblNoPax.Text = Convert.ToString(Convert.ToInt32(dtHACCanInfo.Rows[0]["noofadults"])) + " Adults " +
                            Convert.ToString(Convert.ToInt32(dtHACCanInfo.Rows[0]["noofchildren"]) + " Child's ");

                    lblNoDays.Text = dtHACCanInfo.Rows[0]["noofdays"].ToString();
                    lblAmount.Text = dtHACCanInfo.Rows[0]["Totalamount"].ToString();
                    lblServiceTax.Text = dtHACCanInfo.Rows[0]["STaxValue"].ToString();
                    lblTotalAmount.Text = dtHACCanInfo.Rows[0]["AmtWithTax"].ToString();
                    string pm = dtHACCanInfo.Rows[0]["PayMode"].ToString();
                    if (pm.Trim() != "AgentCash")
                    {
                        DataTable ldtRecSetPay = pvclsObj.fnPayMode(pm);
                        pm = Convert.ToString(ldtRecSetPay.Rows[0][0]);

                    }

                    lblPaymentMode.Text = pm;
                    ViewState["PaymentMode"] = pm;
                    lblBranchCode.Text = dtHACCanInfo.Rows[0]["Branchcode"].ToString();

                    dtHtNoroom = pvclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(dtHACCanInfo.Rows[0]["RowID"]));
                    bool flag = false;
                    int noofroom = 0;
                    if (dtHtNoroom != null && dtHtNoroom.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtHtNoroom.Rows.Count; k++)
                        {

                            noofroom = noofroom + Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"].ToString());
                        }
                    }
                    lblNoRoom.Text = noofroom.ToString();

                    lblCancellationCharge.Text = dtHACCanInfo.Rows[0]["cancellationamt"].ToString();
                    lblRefundAmount.Text = dtHACCanInfo.Rows[0]["refundamt"].ToString();

                    decimal RefundAmount = 0;
                    if (Convert.ToInt32(Session["AgentId"]) > 0)
                    {
                        lblAdvance.Text = dtHACCanInfo.Rows[0]["AmtWithTax"].ToString();

                        RefundAmount = (Convert.ToDecimal(dtHACCanInfo.Rows[0]["AmtWithTax"]) - Convert.ToDecimal(dtHACCanInfo.Rows[0]["cancellationamt"]));
                        lblRefundAmount.Text = RefundAmount.ToString();

                        lblBalance.Text = "Nill";
                    }
                    else
                    {
                        RefundAmount = (Convert.ToDecimal(dtHACCanInfo.Rows[0]["Advance"]) - (Convert.ToDecimal(dtHACCanInfo.Rows[0]["cancellationamt"])));
                        lblRefundAmount.Text = RefundAmount.ToString();

                        lblAdvance.Text = dtHACCanInfo.Rows[0]["Advance"].ToString();
                        lblBalance.Text = Convert.ToString(Convert.ToDecimal(dtHACCanInfo.Rows[0]["AmtWithTax"]) -
                            Convert.ToDecimal(dtHACCanInfo.Rows[0]["cancellationamt"]));
                    }
                    //}
                }
            }
            finally
            {

                if (pvclsObj != null)
                {
                    pvclsObj = null;
                }
                if (dtHACCanInfo != null)
                {
                    dtHtNoroom.Dispose();
                    dtHtNoroom = null;
                }
                if (dtHACCanInfo != null)
                {
                    dtHACCanInfo.Dispose();
                    dtHACCanInfo = null;
                }
            }
        }
        private void showTables()
        {
            tblCan.Visible = false;
            btncantic.Visible = true;
            btncantic.Enabled = true;
            chkAccept.Visible = true;
            tblRuleRegulation.Visible = true;
            //trCancelView.Visible = false;
            //trPrint.Visible = false;
            tblPrintBG = "N";
            tblChk.Visible = true;
            trTicketTerms.Visible = true;
        }
        private string cancelticket(string ticketid)
        {
            pvclsObj = null;
            DataTable dtComm = null; DataTable ldtRecSet = null;
            try
            {
                pvclsObj = new ClsAdo();
                string CancelCode, SuccessSaving, CancelString = "";
                int CancelNo = 0, CancelRowid = 0, totalchars, NoofDaysleft;
                decimal cancelcharges = 0, stvalue = 0, refundamount = 0, cancelpercen;
                DateTime CheckIndate = Convert.ToDateTime(ViewState["CheckinDate"]);

                TimeSpan tspan = new TimeSpan();
                tspan = CheckIndate.Subtract(DateTime.Now);
                NoofDaysleft = Convert.ToInt32(tspan.TotalHours);


                cancelpercen = 0;
                // cancelpercen = Convert.ToDecimal(canpercentage(NoofDaysleft));

                //stvalue = Convert.ToDecimal(ViewState["STaxValue"]);
                pvclsObj = new ClsAdo();
                //decimal sTaxValue = Convert.ToDecimal(pvclsObj.fnGetSTaxForCanTkt(ticketid));
                decimal sTaxValue = 0;
                pvclsObj = new ClsAdo();
                string lSTax = "", lLTax = "";
                if (Convert.ToInt32(ViewState["HotelID"]) > 0)
                {
                    STSPLOrOther clsObj1 = new STSPLOrOther();
                    sTaxValue = Convert.ToDecimal(clsObj1.fnGetSTaxForHACTkt(ticketid, ref lSTax, ref lLTax));
                }
                else
                {
                    sTaxValue = Convert.ToDecimal(pvclsObj.fnGetSTaxForCanTkt(ticketid));
                }
                stvalue = decimal.Round(Convert.ToDecimal(ViewState["STaxValue"]/*sTaxValue*/));
                cancelcharges = decimal.Round((Convert.ToDecimal(ViewState["Amount"]) + stvalue) * (Convert.ToDecimal(cancelpercen) / 100));

                if (Convert.ToDecimal(ViewState["Advance"]) >= Convert.ToDecimal(cancelcharges))
                {
                    refundamount = (Convert.ToDecimal(ViewState["Advance"]) - cancelcharges);
                }

                try
                {


                    decimal debit = default(decimal);
                    decimal credit = default(decimal);
                    debit = 0;
                    credit = 0;

                    decimal AvailableBalance = 0;
                    string transactionname = "BranchAccCancel";
                    int? agentId = 0;
                    decimal agentCredit = 0;
                    decimal agentDebit = 0;
                    decimal commission = 0;
                    decimal lCalTds = 0;

                    agentId = Convert.ToInt32(Session["AgentId"]);
                    if (agentId > 0)
                    {
                        /******** Cancellation Commission ********/
                        //int? AgentLevel = default(int);
                        //pvclsObj.Agent_GetAgentLevelbyAgentId(agentId, ref AgentLevel);

                        dtComm = new DataTable();
                        decimal agentCancellationCommission = default(decimal);
                        //if (AgentLevel != null)
                        //{
                        dtComm = pvclsObj.fnAgentCommission("Accommodation Cancel", agentId);
                        if (dtComm != null && dtComm.Rows.Count > 0)
                        {
                            agentCancellationCommission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
                        }
                        // }
                        decimal CancellationCommissionAmount = (cancelcharges * agentCancellationCommission) / 100;

                        decimal lTdsPer = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));

                        lCalTds = Convert.ToDecimal(CancellationCommissionAmount * (Convert.ToDecimal(lTdsPer) / 100));

                        CancellationCommissionAmount = CancellationCommissionAmount - lCalTds;


                        /******** Cancellation Commission End ********/

                        ldtRecSet = pvclsObj.fnGetAgent_AvailableBalance(agentId);
                        if (ldtRecSet != null && ldtRecSet.Rows.Count > 0)
                        {
                            AvailableBalance = Convert.ToDecimal(ldtRecSet.Rows[0][0]);
                        }
                        else
                        {
                            AvailableBalance = 0;
                        }


                        //AvailableBalance = decimal.Round(AvailableBalance - (Convert.ToDecimal(ViewState["AgentCredit"]))) + CancellationCommissionAmount + refundamount;
                        //AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges);

                        if (Convert.ToDecimal(ViewState["Advance"]) >= Convert.ToDecimal(cancelcharges))
                        {
                            AvailableBalance = decimal.Round(AvailableBalance + CancellationCommissionAmount + Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges);
                        }
                        else
                        {
                            AvailableBalance = decimal.Round(AvailableBalance + CancellationCommissionAmount - Convert.ToDecimal(ViewState["AgentCredit"]));
                        }
                        transactionname = "AgentAccCancel";
                        //agentCredit = CancellationCommissionAmount + refundamount;

                        if (Convert.ToDecimal(decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) -
                       cancelcharges)) >= 0)
                        {
                            agentCredit = Convert.ToDecimal(decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) -
                                cancelcharges + CancellationCommissionAmount));
                        }
                        else
                        {
                            agentCredit = decimal.Round(CancellationCommissionAmount - Convert.ToDecimal(ViewState["AgentCredit"]));
                        }


                        agentDebit = decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + cancelcharges);

                        commission = CancellationCommissionAmount;

                        //debit = CancellationCommissionAmount + refundamount;
                        debit = agentCredit;

                        credit = cancelcharges + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]));

                    }

                    char? lOver = '0';
                    decimal? lOverPers = 0;


                    int val = pvclsObj.fnSaveHACCancelInfo(ticketid.Trim(), cancelcharges, refundamount,
                        Convert.ToInt32(ViewState["NoOfpax"]), Convert.ToString(ViewState["UserName"]),
                        Convert.ToString(ViewState["BranchCode"]), Convert.ToString(ViewState["PaymentMode"]),
                        Convert.ToDecimal(ViewState["Amount"]), Convert.ToString(""), transactionname,
                        agentId, agentCredit, AvailableBalance, agentDebit, commission, debit,
                        credit, decimal.Round(Convert.ToDecimal(stvalue)),
                        decimal.Round(Convert.ToDecimal(lCalTds)), lOver, lOverPers, null, null);

                    if (val == 1)
                    {


                        SuccessSaving = "Success";
                    }
                    else
                        SuccessSaving = "Failure";
                }
                catch (Exception ex_trans)
                {
                    SuccessSaving = "Failure";
                }
                return SuccessSaving;
            }
            finally
            {
                if (pvclsObj != null)
                {
                    pvclsObj = null;
                }
                if (dtComm != null)
                {
                    dtComm.Dispose();
                    dtComm = null;
                }
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }
        public string canpercentage(int NoofDaysleft)
        {
            pvclsObj = null;
            try
            {
                pvclsObj = new ClsAdo();
                decimal cancelpercen = 0;
                if (ViewState["HotelID"] != null && Convert.ToInt32(ViewState["HotelID"]) > 0)
                {
                    cancelpercen = pvclsObj.fnGetHotelCancelPerc(NoofDaysleft, Convert.ToInt32(ViewState["HotelID"]));
                }
                else
                {
                    cancelpercen = pvclsObj.fnGetHAccCancelPerc(NoofDaysleft);
                }
                return Convert.ToString(cancelpercen);
            }
            finally
            {
                if (pvclsObj != null)
                {
                    pvclsObj = null;
                }
            }
        }

        private StringBuilder AppendGuestDetails(StringBuilder hactckstr)
        {

            StringBuilder gueststr = new StringBuilder();
            gueststr.Append("<tr style='height: 25px'>");
            gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px' colspan=3><span class='cgi'>Guest Details</span>&nbsp;&nbsp;</td></tr>");

            gueststr.Append("<tr>");
            gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px' ><span class='cgi'>Name</span>&nbsp;&nbsp;</td>");
            gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px' ><span class='cgi'>Age</span>&nbsp;&nbsp;</td>");
            gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px' ><span class='cgi'>Sex</span>&nbsp;&nbsp;</td></tr>");

            string GetTickeDetailsForExtraServicetax = StoredProcedures.GetGuestDetails_sp;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@I_OrderNo", txtpNRo.Text.Trim());
            DataTable dt = new DataTable();

            DataSet dsHac = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetTickeDetailsForExtraServicetax, param);
            dt = dsHac.Tables[0];

            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gueststr.Append("<tr> <td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px'>");
                        gueststr.Append("<span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span>&nbsp;&nbsp;</td>");


                        gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px'>");
                        gueststr.Append("<span>" + Convert.ToString(dt.Rows[i]["age"]) + "</span>&nbsp;&nbsp;</td>");

                        gueststr.Append("<td align='center' valign='middle' style='font-family: Verdana, Arial, Helvetica; line-height: 1.4;color: #000000; font-size: 12px'>");
                        gueststr.Append("<span>" + Convert.ToString(dt.Rows[i]["sex"]) + "</span>&nbsp;&nbsp;</td>");
                        gueststr.Append("<tr>");
                    }
                    hactckstr.Replace("#Guestdeatils", gueststr.ToString());
                }
                else
                {
                    hactckstr.Replace("#Guestdeatils", "");
                }
                return hactckstr;
            }
            catch (Exception ex)
            {
                hactckstr.Replace("#Guestdeatils", "");
                return hactckstr;
            }
            finally
            {
                if (dsHac != null)
                {
                    dsHac = null;
                }
                if (dt != null)
                {
                    dt = null;
                }
            }
        }

        #endregion
    }
}