using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentSpecialTourCancellation : System.Web.UI.Page
    {
        #region "Member variables"
        protected decimal TotalAmount;
        protected string DateJourney, DateJourneySQL, tblPrintBG;
        ClsAdo clsObj = null;
        #endregion
        #region "Event's"
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AgentId"] == null)
            {
                Response.Redirect("AgentLogin.aspx");
            }
            else
            {
                string saveName = "Logo\\" + Session["UserId"].ToString();
                string exist = "N";
                if (System.IO.File.Exists(Server.MapPath(saveName + ".jpg"))) { saveName = saveName + ".jpg"; exist = "Y"; }
                if (System.IO.File.Exists(Server.MapPath(saveName + ".gif"))) { saveName = saveName + ".gif"; exist = "Y"; }
                if (System.IO.File.Exists(Server.MapPath(saveName + ".png"))) { saveName = saveName + ".png"; exist = "Y"; }
                string address1 = Globals.AgentAddress;
                string ph = Globals.AgentPhone;
                if (exist == "Y")
                {
                    agentimg.Src = saveName;
                    agentimg.Alt = Session["UserId"].ToString();
                    agtaddress.Text = address1 + ", " + ph;
                    southernimg.Src = "../Assets/images/southerntravels_logo.gif";
                    southernimg.Alt = "Southern Travels Pvt Ltd.";
                }
                else
                {
                    string AgentName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                    agentimg.Visible = false;
                    agtaddress.Text = AgentName + "<br/>" + address1 + ", " + ph;
                    southernimg.Src = "../Assets/images/southerntravels_logo.gif";
                    southernimg.Alt = "Southern Travels Pvt Ltd.";
                }
                DateJourney = txtjdate.Value.ToString();
                if (DateJourney == "")
                {
                }
                else
                {
                    string[] DateArr = new string[3];
                    char[] splitter = { '/' };
                    DateArr = DateJourney.Split(splitter);
                    DateJourneySQL = DateArr[1] + "/" + DateArr[0] + "/" + DateArr[2];
                    DateJourneySQL = Convert.ToDateTime(DateJourneySQL).ToShortDateString();
                }
                if (!Page.IsPostBack)
                {
                    hideTables();
                }
                this.btnsubmit.Attributes.Add("OnClick", "return validation();");
                btncantic.Attributes.Add("OnClick", "return chkterms();");
                return;
            }
        }



        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            tblRuleRegulation.Visible = false;

            if (txtjdate.Value.Trim() == "")
            {
                lblerrmsg.Text = "Enter Journey date.";
                return;
            }
            TimeSpan hr;
            DateTime jdate = Convert.ToDateTime(mmddyy2ddmmyy(txtjdate.Value));
            DateTime tdate = System.DateTime.Now;
            hr = jdate.Subtract(tdate);
            double noofhours = hr.TotalHours;

            lblerrmsg.Text = "";
            #region Optimize Code
            /*string str3 = "select s.*,o.* from spl_tourEnquiry s inner join onlineTransactionTable o on s.TicketNo=o.RefNo where
             * s.TicketNo='" + txtpNRo.Text + "' and (s.Email='" + txtemail.Text + "' or s.Mobile='" + txtemail.Text + "') 
             * and s.JourneyDate='" + DateJourneySQL + "' and s.status<>'N'";

            DataTable dtTour;
            dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
            #endregion
            clsObj = new ClsAdo();
            DataTable dtTour = clsObj.fnAgentSplTourTicket(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(DateJourneySQL));
            DataTable dt = null, dtCar = null;
            try
            {
                if (dtTour.Rows.Count > 0)
                {
                    if ((Convert.ToString(dtTour.Rows[0]["AgentId"]) != "0") &&
                        (Convert.ToString(dtTour.Rows[0]["AgentId"]) == Convert.ToString(Session["AgentId"])))
                    {
                        TotalAmount = Convert.ToDecimal(dtTour.Rows[0]["TicketAmount"]) + Convert.ToDecimal(dtTour.Rows[0]["ServiceTax"]);
                        ViewState["tourid"] = Convert.ToString(dtTour.Rows[0]["tourid"]);
                        ViewState["TotalAmount"] = Convert.ToString(decimal.Round(TotalAmount));
                        ViewState["Amount"] = dtTour.Rows[0]["TicketAmount"].ToString();
                        ViewState["STaxValue"] = dtTour.Rows[0]["ServiceTax"].ToString();
                        ViewState["NoOfpax"] = dtTour.Rows[0]["TotalPax"].ToString();
                        ViewState["TicketNo"] = dtTour.Rows[0]["TicketNo"].ToString();
                        ViewState["PaymentMode"] = dtTour.Rows[0]["PaymentMode"].ToString();
                        ViewState["AgentCredit"] = dtTour.Rows[0]["AgentCredit"].ToString();
                        ViewState["AgentDebit"] = dtTour.Rows[0]["AgentDebit"].ToString();
                        ViewState["TDS"] = dtTour.Rows[0]["TDS"].ToString();
                        ViewState["AgentId"] = dtTour.Rows[0]["AgentId"].ToString();
                        if (Convert.ToInt32(ViewState["AgentId"]) > 0)
                            ViewState["Advance"] = dtTour.Rows[0]["Advancepaid"].ToString()/*Convert.ToString(TotalAmount)*/;
                        else
                            ViewState["Advance"] = dtTour.Rows[0]["Credit"].ToString();

                        ViewState["Discount"] = dtTour.Rows[0]["Discount"].ToString();

                        lblSplNo.Text = dtTour.Rows[0]["pnrno"].ToString();
                        lblTicketNo.Text = dtTour.Rows[0]["TicketNo"].ToString();
                        DateTime strDate = Convert.ToDateTime(dtTour.Rows[0]["JourneyDate"].ToString());
                        lblJourneyDate.Text = strDate.ToString("dd/MM/yyyy");
                        lblTourName.Text = Convert.ToString(dtTour.Rows[0]["TourName"].ToString());

                        #region Optimize Code
                        /* string strC = "select VehicleName from Spl_Vehiclemaster where vehicleid in (" + dtTour.Rows[0]["CarType"].ToString() + ")";
                    DataTable dtCar = new DataTable();
                    dtCar = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strC);*/
                        #endregion
                        dtCar = clsObj.fnAgentSplVehiclemaster(dtTour.Rows[0]["CarType"].ToString());

                        string strCarName = "";
                        if (dtCar.Rows.Count > 0)
                        {
                            for (int m = 0; m < dtCar.Rows.Count; m++)
                            {
                                strCarName += (dtCar.Rows[m][0].ToString() + ", ");
                            }
                        }
                        strCarName = strCarName.Trim().TrimEnd(',');

                        lblVehicle.Text = strCarName;
                        string strCategory = dtTour.Rows[0]["FareCategoryType"].ToString();
                        /* if (strCategory == "1")
                             lblCategory.Text = "Standard";
                         else
                             lblCategory.Text = "Deluxe";
                         */
                        lblCategory.Text = Convert.ToString(dtTour.Rows[0]["categoryname"]);
                        /*if (Convert.ToInt32(ViewState["AgentId"]) > 0)
                        {
                            lblDiscount.Text = "";
                            lbldiscount1.Text = "";
                            lblbookingbranch.Text = "Booking Agent:";
                            lblAdvance.Text = TotalAmount.ToString();
                            lblAdvance1.Text = TotalAmount.ToString();
                            lblBalance.Text = "Nil";
                        }
                        else
                        {*/
                        lblDiscount.Text = "";
                        lbldiscount1.Text = "";
                        lblbookingbranch.Text = "Booking Agent:";
                        lblAdvance.Text = dtTour.Rows[0]["Advancepaid"].ToString();
                        lblAdvance1.Text = dtTour.Rows[0]["Advancepaid"].ToString();

                        //}
                        lblPickupFrom.Text = dtTour.Rows[0]["PickUpVeh"].ToString();
                        lblPickupNo.Text = dtTour.Rows[0]["PickVehNo"].ToString();
                        lblPickupTime.Text = dtTour.Rows[0]["PickTime"].ToString();
                        lblDropAt.Text = dtTour.Rows[0]["DropVeh"].ToString();
                        lblDropNo.Text = dtTour.Rows[0]["DropVehNo"].ToString();
                        lblDropTime.Text = dtTour.Rows[0]["DropTime"].ToString();
                        lblName.Text = dtTour.Rows[0]["FirstName"].ToString() + ' ' + dtTour.Rows[0]["LastName"].ToString();
                        lblAddress.Text = dtTour.Rows[0]["Address"].ToString();
                        lblPax.Text = dtTour.Rows[0]["TotalPax"].ToString() + "/" + dtTour.Rows[0]["SingleSharing"].ToString();
                        lblAmount.Text = dtTour.Rows[0]["TicketAmount"].ToString();
                        lblServiceTax.Text = dtTour.Rows[0]["ServiceTax"].ToString();
                        lblTotalAmount.Text = TotalAmount.ToString();

                        DateTime journeydate = Convert.ToDateTime(txtjdate.Value.Split('/').GetValue(1) + "/" + txtjdate.Value.Split('/').GetValue(0) + "/" + txtjdate.Value.Split('/').GetValue(2));
                        TimeSpan ts = new TimeSpan();
                        ts = journeydate.Subtract(DateTime.Now.Date);
                        int NoofDaysleft = ts.Days;
                        clsObj = new ClsAdo();
                        dt = new DataTable();
                        dt = clsObj.fnspl_weekend_cancelpercentage(Convert.ToInt32(ViewState["tourid"]), NoofDaysleft);
                        DataSet dsper = new DataSet();
                        if (dt != null)
                            dsper.Tables.Add(dt);
                        decimal cancelpercen = 0;
                        if (dsper.Tables[0].Rows.Count > 0)
                            cancelpercen = Convert.ToDecimal(dsper.Tables[0].Rows[0]["percentage"]);
                        decimal cancelcharges = 0;
                        decimal refundamount = 0;
                        cancelcharges = decimal.Round(Convert.ToDecimal(TotalAmount) * (Convert.ToDecimal(cancelpercen) / 100));
                        refundamount = decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - cancelcharges);


                        lblCancellationCharge1.Text = cancelcharges.ToString();
                        if (Convert.ToDecimal(refundamount) > 0)
                        {
                            lblrefund1.Text = "Refund Amount:";
                            lblRefundAmount1.Text = refundamount.ToString();
                            lblBalance.Text = "Nil";
                        }
                        else
                        {
                            lblrefund1.Text = "Exceeding Amount:";
                            lblRefundAmount1.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(refundamount)));
                            lblBalance.Text = Convert.ToString(TotalAmount - Convert.ToDecimal(dtTour.Rows[0]["Advancepaid"]));
                        }

                        if (dtTour.Rows[0]["PaymentMode"].ToString().Trim().Length < 3)
                        {
                            #region Optimize Code
                            /*string ss = "select Paymentmode from tbl_paymodes where rowid='" + dtTour.Rows[0]["PaymentMode"] + "'";
                        string pm = DataLib.GetStringData(DataLib.Connection.ConnectionString, ss);*/
                            #endregion
                            dt = clsObj.fnPayMode(Convert.ToString(dtTour.Rows[0]["PaymentMode"]));
                            string pm = dt.Rows[0][0].ToString();
                            string details = "";
                            if (Convert.ToString(dtTour.Rows[0]["Number"]) != "")
                                details = details + "<br><span class=cgi>Trans No: </span>" + dtTour.Rows[0]["Number"].ToString();
                            if (Convert.ToString(dtTour.Rows[0]["BankName"]) != "")
                                details = details + "<br><span class=cgi>Bank: </span>" + dtTour.Rows[0]["BankName"].ToString();
                            if (Convert.ToString(dtTour.Rows[0]["chqdate"]) != "")
                                details = details + "<br><span class=cgi>Date: </span>" + dtTour.Rows[0]["chqdate"].ToString();

                            lblPaymentMode.Text = pm + details;
                        }
                        else
                            lblPaymentMode.Text = dtTour.Rows[0]["PaymentMode"].ToString();
                        lblBranchCode.Text = dtTour.Rows[0]["BranchCode"].ToString();
                        if (Convert.ToString(dtTour.Rows[0]["iscancel"]) != "Y")
                        {
                            if (noofhours > 24)
                                showTables();
                            else
                            {
                                if (tdate > jdate)
                                    lblerrmsg.Text = "Tour was Closed";
                                else
                                    lblerrmsg.Text = "Cancellation Not Allowed With in 24 Hours.";
                                hideTables();
                            }
                        }
                        else
                        {
                            ViewCancelTicket();
                        }
                    }
                    else
                    {
                        lblerrmsg.Text = "You Don't Have Permission to Cancel This Ticket";
                        hideTables();
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
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtTour != null)
                {
                    dtTour.Dispose();
                    dtTour = null;
                }
                if (dtCar != null)
                {
                    dtCar.Dispose();
                    dtCar = null;
                }

            }
        }
        protected void btncantic_Click(object sender, EventArgs e)
        {
            btncantic.Enabled = false;
            btncantic.Visible = false;
            string returnstr = "";
            if (chkAccept.Checked == true)
            {
                lblerrmsg.Text = "";
                clsObj = new ClsAdo();
                DataTable dtTour = clsObj.fnAgentSplTourTicket(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(DateJourneySQL));

                if (Convert.ToString(dtTour.Rows[0]["IsCancel"]) != "Y")
                    returnstr = cancelticket(Convert.ToString(ViewState["TicketNo"]));
                else
                {
                    lblerrmsg.Text = "This Ticket has been Cancelled.";
                    ViewCancelTicket();
                }

                if (returnstr == "Success")
                    ViewCancelTicket();
                else
                    lblerrmsg.Text = "Please fill all mandatory fields.";
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
        private void hideTables()
        {
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblRuleRegulation.Visible = false;
            trPrint.Visible = false;
        }
        private void showTables()
        {
            btncantic.Visible = true;
            btncantic.Enabled = true;
            chkAccept.Visible = true;
            tblRuleRegulation.Visible = true;
            trCancelView.Visible = false;
            trCancelView1.Visible = true;
            trPrint.Visible = false;
            tblPrintBG = "N";
            trTicketTerms.Visible = true;
            trTicketView1.Visible = true;
            trTicketView.Visible = true;
        }
        private void showCancelTable()
        {
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblRuleRegulation.Visible = true;
            trCancelView.Visible = true;
            trCancelView1.Visible = false;
            trPrint.Visible = true;
            trTicketView.Visible = false;
            trTicketView1.Visible = false;
            trTicketTerms.Visible = false;
            tblPrintBG = "Y";
        }
        public static string mmddyy2ddmmyy(string date1)
        {
            if (date1 != null || date1 != "")
            {
                string[] DateArr3 = new string[3];
                char[] splitter1 = { '/' };
                DateArr3 = date1.Split(splitter1);
                return DateArr3[1] + "/" + DateArr3[0] + "/" + DateArr3[2];
            }
            else
            {
                return "";
            }
        }
        private void ViewCancelTicket()
        {

            lblerrmsg.Text = "This Ticket has been Cancelled.";
            showCancelTable();
            #region Optimize Code
            /*string str = "select s.*,c.* from spl_tourEnquiry s inner join Cancel c on s.TicketNo=c.TicketNo 
             * where s.TicketNo='" + txtpNRo.Text + "' and (s.Email='" + txtemail.Text + "' or s.Mobile='" + txtemail.Text + "') and s.JourneyDate='" + DateJourneySQL + "' and s.status<>'N'";
            DataTable dtTour = new DataTable();
            dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
            #endregion
            clsObj = new ClsAdo();
            DataTable dtTour = clsObj.fnAgentSplTourCancelTicket(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(DateJourneySQL));
            DataTable dtCar = null;
            try
            {
                if (dtTour.Rows.Count > 0)
                {
                    TotalAmount = Convert.ToDecimal(dtTour.Rows[0]["Fare"]) + Convert.ToDecimal(dtTour.Rows[0]["ServiceTax"]);
                    lblSplNo.Text = dtTour.Rows[0]["pnrno"].ToString();
                    lblTicketNo.Text = dtTour.Rows[0]["TicketNo"].ToString();

                    DateTime strDate = Convert.ToDateTime(dtTour.Rows[0]["JourneyDate"].ToString());
                    lblJourneyDate.Text = strDate.ToString("dd/MM/yyyy");

                    // lblJourneyDate.Text = Convert.ToDateTime(dtTour.Rows[0]["JourneyDate"].ToString()).ToShortDateString();
                    lblTourName.Text = dtTour.Rows[0]["TourName"].ToString();
                    #region Optimize Code
                    /*string strC = "select VehicleName from Spl_Vehiclemaster where vehicleid in (" + dtTour.Rows[0]["CarType"].ToString() + ")";
                DataTable dtCar = new DataTable();
                dtCar = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strC);*/
                    #endregion
                    dtCar = clsObj.fnAgentSplVehiclemaster(dtTour.Rows[0]["CarType"].ToString());
                    string strCarName = "";
                    if (dtCar.Rows.Count > 0)
                    {
                        for (int m = 0; m < dtCar.Rows.Count; m++)
                        {
                            strCarName += (dtCar.Rows[m][0].ToString() + ", ");
                        }
                    }
                    strCarName = strCarName.Trim().TrimEnd(',');

                    lblVehicle.Text = strCarName;
                    /*string strCategory = dtTour.Rows[0]["FareCategoryType"].ToString();
                    if (strCategory == "1")
                    {
                        lblCategory.Text = "Standard";
                    }
                    else
                    {
                        lblCategory.Text = "Deluxe";
                    }*/
                    lblCategory.Text = dtTour.Rows[0]["categoryname"].ToString();
                    // lblCategory.Text = dtTour.Rows[0]["FareCategoryType"].ToString();
                    /* if (Convert.ToInt32(ViewState["AgentId"]) > 0)
                     {
                         lblDiscount.Text = "";
                         lbldiscount1.Text = "";
                         lblbookingbranch.Text = "Booking Agent:";
                         lblAdvance.Text = TotalAmount.ToString();
                         lblAdvance1.Text = TotalAmount.ToString();
                         lblBalance.Text = "Nil";

                     }
                     else
                     {*/
                    lblDiscount.Text = "";
                    lbldiscount1.Text = "";
                    lblbookingbranch.Text = "Booking Agent:";
                    lblAdvance.Text = dtTour.Rows[0]["Advancepaid"].ToString();
                    lblAdvance1.Text = dtTour.Rows[0]["Advancepaid"].ToString();

                    //}
                    lblPickupFrom.Text = dtTour.Rows[0]["PickUpVeh"].ToString();
                    lblPickupNo.Text = dtTour.Rows[0]["PickVehNo"].ToString();
                    lblPickupTime.Text = dtTour.Rows[0]["PickTime"].ToString();
                    lblDropAt.Text = dtTour.Rows[0]["DropVeh"].ToString();
                    lblDropNo.Text = dtTour.Rows[0]["DropVehNo"].ToString();
                    lblDropTime.Text = dtTour.Rows[0]["DropTime"].ToString();
                    lblName.Text = dtTour.Rows[0]["FirstName"].ToString() + ' ' + dtTour.Rows[0]["LastName"].ToString();
                    lblAddress.Text = dtTour.Rows[0]["Address"].ToString();
                    lblPax.Text = dtTour.Rows[0]["TotalPax"].ToString() + "/" + dtTour.Rows[0]["SingleSharing"].ToString();
                    lblAmount.Text = dtTour.Rows[0]["Fare"].ToString();
                    lblServiceTax.Text = dtTour.Rows[0]["ServiceTax"].ToString();
                    lblTotalAmount.Text = TotalAmount.ToString();

                    lblCancellationCharge.Text = dtTour.Rows[0]["CanCharges"].ToString();
                    if (Convert.ToDecimal(dtTour.Rows[0]["RefundAmt"]) > 0)
                    {
                        lblrefund.Text = "Refund Amount:";
                        lblRefundAmount.Text = dtTour.Rows[0]["RefundAmt"].ToString();
                        lblBalance.Text = "Nil";
                    }
                    else
                    {
                        lblrefund.Text = "Exceeding Amount:";
                        lblRefundAmount.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(dtTour.Rows[0]["RefundAmt"])));
                        lblBalance.Text = Convert.ToString(TotalAmount - Convert.ToDecimal(dtTour.Rows[0]["Advancepaid"]));
                    }
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtTour != null)
                {
                    dtTour.Dispose();
                    dtTour = null;
                }
                if (dtCar != null)
                {
                    dtCar.Dispose();
                    dtCar = null;
                }
            }
        }
        private string cancelticket(string ticketid)
        {
            string sqladd = "", CancelCode, SuccessSaving;
            int CancelNo = 0, CancelRowid = 0, Cancel1Rowid = 0, totalchars, i, NoofDaysleft;
            decimal cancelcharges = 0, stvalue = 0, refundamount = 0, cancelpercen = 0;
            DateTime journeydate = Convert.ToDateTime(txtjdate.Value.Split('/').GetValue(1) + "/" + txtjdate.Value.Split('/').GetValue(0) + "/" + txtjdate.Value.Split('/').GetValue(2));
            TimeSpan ts = new TimeSpan();
            ts = journeydate.Subtract(DateTime.Now.Date);
            NoofDaysleft = ts.Days;
            #region Optimize Code
            /*SqlParameter[] canper = new SqlParameter[2];
            canper[0] = new SqlParameter("@tourid", Convert.ToInt32(ViewState["tourid"]));
            canper[1] = new SqlParameter("@noofdays", NoofDaysleft);
            DataSet dsper = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "spl_weekend_cancelpercentage", canper);*/
            #endregion
            clsObj = null;
            DataTable dt = null;
            DataSet dsper = null;
            DataTable dtComm = null;
            try
            {
                clsObj = new ClsAdo();
                dt = clsObj.fnspl_weekend_cancelpercentage(Convert.ToInt32(ViewState["tourid"]), NoofDaysleft);
                dsper = new DataSet();
                if (dt != null)
                    dsper.Tables.Add(dt);

                if (dsper.Tables[0].Rows.Count > 0)
                    cancelpercen = Convert.ToDecimal(dsper.Tables[0].Rows[0]["percentage"]);
                else
                    cancelpercen = 0;
                //cancelcharges = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(cancelpercen) / 100)));
                //stvalue = Convert.ToDecimal(ViewState["STaxValue"]);

                decimal sTaxValue = Convert.ToDecimal(clsObj.fnGetSTaxForCanTkt(ticketid));
                stvalue = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(sTaxValue) / 100)));

                decimal pTotal = Convert.ToDecimal(ViewState["Amount"]) + stvalue;
                cancelcharges = decimal.Round(Convert.ToDecimal(pTotal) * (Convert.ToDecimal(cancelpercen) / 100));
                refundamount = decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - cancelcharges);

                /******** Cancellation Commission ********/
                decimal CancellationCommissionAmount = 0;
                decimal lCalTds = 0;
                int agentid = Convert.ToInt32(Session["agentID"]);
                if (agentid > 0)
                {
                    //int? AgentLevel = default(int);
                    //clsObj.Agent_GetAgentLevelbyAgentId(agentid, ref AgentLevel);

                    dtComm = new DataTable();

                    decimal agentCancellationCommission = default(decimal);
                    //if (AgentLevel != null)
                    //{
                    dtComm = clsObj.fnAgentCommission("SpecialTour Cancel", agentid, Convert.ToInt32(ViewState["tourid"]));
                    if (dtComm != null && dtComm.Rows.Count > 0)
                    {
                        agentCancellationCommission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
                    }
                    //}
                    CancellationCommissionAmount = (cancelcharges * agentCancellationCommission) / 100;
                    decimal lTdsPer = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));

                    lCalTds = Convert.ToDecimal(CancellationCommissionAmount * (Convert.ToDecimal(lTdsPer) / 100));

                    CancellationCommissionAmount = CancellationCommissionAmount - lCalTds;
                }
                /******** End ********/
                decimal AvailableBalance = 0;
                string balance = "";

                if (agentid > 0)
                    balance = Convert.ToString(ClsAgentTransaction.Agent_Availablebalance(agentid).Rows[0][0]);
                if (Convert.ToDecimal(ViewState["Advance"]) < pTotal)
                {
                    CancellationCommissionAmount = 0;
                }
                if (Convert.ToString(balance) != "")
                {
                    AvailableBalance = Convert.ToDecimal(balance) + CancellationCommissionAmount;
                }

                //string chk = "select * from spl_tourEnquiry where TicketNo='" + ticketid.Trim() + "' and iscancel='Y' ";
                //DataTable dtcheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, chk);
                //if (dtcheck.Rows.Count == 0)
                //{
                #region Optimize Code
                //updating the TicketDetails table by setting the cancellation field as Y 
                #region Optimize Code
                /* string query = "update spl_tourEnquiry set iscancel='Y' where TicketNo='" + ticketid.Trim() + "' and status='S'";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, query, false);

                     query = "";
                    query = "Select NumericValue,RespectiveRowid from NewKeyTable_online where keyType='Cancel'";
                    DataTable dtcan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, query);*/
                #endregion
                /*DataTable dtcan = clsObj.fnNewKeyTableID("Cancel");
                    if (dtcan.Rows.Count > 0)
                    {
                        CancelNo = Convert.ToInt32(dtcan.Rows[0]["NumericValue"]);
                        CancelRowid = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
                        Cancel1Rowid = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
                    }
                    //Got the Last Ticket and 1 is added to that for a new One 
                    CancelNo = CancelNo + 1;
                    CancelRowid = CancelRowid + 1;
                    Cancel1Rowid = Cancel1Rowid + 1;
                    //Generating the CancelCode(Cancellation Number) which is store in the Cancel table 
                    string CancelString;
                    CancelCode = "";
                    CancelString = CancelNo.ToString();
                    totalchars = CancelString.Length;
                    for (i = 0; i <= (5 - totalchars); i++)
                    {
                        CancelCode = CancelCode + 0;
                    }
                    CancelCode = "CANSPL" + CancelCode + CancelString;*/


                /*sqladd = "insert into Cancel(RowId,CanNo,TicketNo,CanNoTick,CanCharges,RefundAmt,UserName,BranchCode)values(
                 * " + CancelRowid + ",'" + CancelCode + "','" + ticketid.Trim() + "'," + Convert.ToInt32(ViewState["NoOfpax"]) + ",
                 * " + decimal.Round(cancelcharges) + "," + decimal.Round(refundamount) + ",'" + Session["AgentId"] + "','" + sBranchCode + "')";

                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);

                //Updating the NewKeyTable_Online with the new Cancel Number and Rowid 
                sqladd = "";
                sqladd = "update newkeytable_online set NumericValue = " + CancelNo + " , RespectiveRowid=" + CancelRowid + " where KeyType = 'Cancel' ";
                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);

                string strid = "select transactiontypeid from transactiontypemaster where transactionname='AgentSpecialTourCancellation' and IsAgentService='Y'";
                string transtype = DataLib.GetStringData(DataLib.Connection.ConnectionString, strid);

                sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,Commission,
                 * PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,Cashier,
                 * TransState,Status,Servicetax,TDS) values(" + agentid + ",'" + CancelCode + "'," + Convert.ToInt16(transtype) + ",
                 * " + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + ",
                 * " + decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges)) + ",
                 * " + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + ",
                 * " + decimal.Round(Convert.ToDecimal(ViewState["Amount"])) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"])) + ",
                 * '" + Convert.ToString(ViewState["PaymentMode"]) + "','','',
                 * " + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + ",
                 * " + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + ",'','" + Session["UserId"] + "',
                 * '" + sBranchCode + "','','','','P','S'," + stvalue + "," + decimal.Round(Convert.ToDecimal(ViewState["TDS"])) + ")";
                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);*/
                #endregion
                string sBranchCode = "AGENT";
                int val1 = clsObj.fnUpdateSPLTourCancelInfo(decimal.Round(cancelcharges), decimal.Round(refundamount), ticketid.Trim(), Convert.ToInt32(ViewState["NoOfpax"]), Convert.ToString(Session["AgentId"]), agentid,
                    decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommissionAmount),
                    decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges)),
                    decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)), decimal.Round(Convert.ToDecimal(ViewState["Amount"])),
                    decimal.Round(Convert.ToDecimal(CancellationCommissionAmount)), sBranchCode, Convert.ToString(ViewState["PaymentMode"]),
                    decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommissionAmount),
                    decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)), Convert.ToString(Session["UserId"]),
                    sBranchCode, stvalue,
                    decimal.Round(Convert.ToDecimal(lCalTds)));
                ViewState["CarCancel"] = "Success";
                SuccessSaving = "Success";
                //}
                //else
                //{
                //    SuccessSaving = "Success";
                //}
            }
            catch (Exception ex_trans)
            {
                SuccessSaving = "Failure";
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dsper != null)
                {
                    dsper.Dispose();
                    dsper = null;
                }
                if (dtComm != null)
                {
                    dtComm.Dispose();
                    dtComm = null;
                }
            }
            return SuccessSaving;
        }
        #endregion
    }
}