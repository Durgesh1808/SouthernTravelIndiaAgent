using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentCarCancellation : System.Web.UI.Page
    {

        #region "Member variables"
        protected ArrayList branchlist, BusSerial;
        protected string TicketNo, sqlQuery = "", BranchCode, TourName, UserName;
        protected int TourNo, NAdult, NAdultsTwin, NadultsTriple, NSingleAdult, NChild, NChildwithoutBed, Age, agentID;
        protected DateTime ReportTime, DepartTime;
        protected System.DateTime JourneyDate;
        protected string Name, BusNo, SeatNumbers;
        protected decimal Amount, TotalAmount, Total, AgentCredit, AgentDebit, TDS, STaxValue, cctaxvalue;
        protected string PaymentMode, BankName, TransNumber, TelNo, Sex;
        protected int AdultFare, Adultstwinfare, adultstriplefare, singleadultfare, ChildFare, childwithoutbedfare;
        protected StringBuilder Ticketstr;
        protected string CurrentBranchCode = System.Configuration.ConfigurationManager.AppSettings["BranchCode"];
        protected string DateJourney, DateJourneySQL, PickUppoint, orderid, OName, title, tblPrintBG;
        protected int busserialno, tourserialno, TourSerial;
        protected int tpax = 0;
        protected StringBuilder stbuild = new StringBuilder();
        ClsAdo clsObj = new ClsAdo();
        protected int preadult, prechild, preadulttwin, preadulttriple, presingleadult, prechilewithoutbed;
        #endregion
        #region "Event's"
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["AgentId"] == null) && (Session["BranchId"] == null))
            {
                Response.Redirect("Agentlogin.aspx");
            }
            else
            {
                string saveName = Server.MapPath("Logo\\" + Session["UserId"].ToString());
                string exist = "N";
                if (System.IO.File.Exists(saveName + ".jpg")) { saveName = saveName + ".jpg"; exist = "Y"; }
                if (System.IO.File.Exists(saveName + ".gif")) { saveName = saveName + ".gif"; exist = "Y"; }
                if (System.IO.File.Exists(saveName + ".png")) { saveName = saveName + ".png"; exist = "Y"; }

                if (exist == "Y")
                {
                    imglogo.Src = saveName;
                    imglogo.Alt = Session["UserId"].ToString();
                }
                else
                {
                    imglogo.Src = "../Assets/images/southern-travel.gif";
                    imglogo.Alt = "Southern Travels Pvt Ltd.";
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
                    if (DateArr[1].Length == 1)
                    {
                        DateArr[1] = "0" + DateArr[1];
                    }
                    if (DateArr[0].Length == 1)
                    {
                        DateArr[0] = "0" + DateArr[0];
                    }
                    DateJourneySQL = DateArr[1] + "/" + DateArr[0] + "/" + DateArr[2];
                    //DateJourneySQL = Convert.ToDateTime(DateJourneySQL).ToShortDateString();
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

            lblerrmsg.Text = "";
            #region Optimize Code
            /*string str = "select * from tbl_carinitialdutyslip where cabno='" + txtpNRo.Text + "'";
            DataTable dtinitlacheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
            #endregion
            DataTable dtinitlacheck = clsObj.fnChkInitialDutySlip(txtpNRo.Text);
            if (dtinitlacheck.Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "nochance", "<script>alert('Sorry!. Initial Duty Slip was Generated');</script>");
            }
            else
            {
                #region OPtimize Code
                /*SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@PNRNO", txtpNRo.Text);
                param[1] = new SqlParameter("@email", txtemail.Text);
                param[2] = new SqlParameter("@journeydate", DateJourneySQL);

                sqlQuery = "sp_AgentCarcancellation";
                DataSet dtTour = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlQuery, param);*/


                #region Commented

                //            string str3 = @"SELECT     a.TicketNo AS cabid, a.TourName AS transfername,a.NoOfPax, a.PickTupTime AS picktuptime, a.PickTupTime AS time, ISNULL(a.PickupAddress, 
                //                      'Not Decide') AS pickupaddress, ISNULL(a.DropOffAddress, 'Not Decide') AS dropoffaddress, a.EmailId AS email,  
                //                       a.STax, b.paymentmode as PayMode, a.Advance, a.Discount, a.Cancelled AS cancelled, 
                //                      a.paySucceeded AS paysucceeded, b.AgentId, b.AgentCredit, b.AgentDebit, b.Credit, b.Debit, b.TDS, b.BranchCode, 
                //                      OnlineCustomer.FirstName AS firstname, OnlineCustomer.Mobile , OnlineCustomer.Addr1 AS addr1 , a.Fare AS fare,a.CarName
                //                       FROM         tbl_CarBookings_Log a INNER JOIN
                //                      OnlineTransactionTable b ON a.TicketNo = b.RefNo INNER JOIN
                //                      OnlineCustomer ON a.EmailId = OnlineCustomer.email OR a.EmailId = OnlineCustomer.Mobile where a.ticketno='" + txtpNRo.Text + "' and (OnlineCustomer.email='" + txtemail.Text + "' or OnlineCustomer.mobile='" + txtemail.Text + "') and convert(varchar(10),a.picktuptime,101)='" + DateJourneySQL + "' and a.ticketno=b.refno";

                //DataTable dtTour;
                //dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);
                #endregion
                #endregion
                DataSet dtTour = new DataSet();
                DataTable ldtRecSet = clsObj.fnAgentCarcancellation(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(DateJourneySQL));
                if (ldtRecSet != null)
                {
                    dtTour.Tables.Add(ldtRecSet);
                }
                if (dtTour.Tables[0].Rows.Count > 0)
                {
                    if ((Convert.ToString(dtTour.Tables[0].Rows[0]["agentid"]) == Convert.ToString(Session["AgentId"])) || Session["BranchId"] != null)
                    {
                        TotalAmount = Convert.ToDecimal(dtTour.Tables[0].Rows[0]["fare"]) + Convert.ToDecimal(dtTour.Tables[0].Rows[0]["stax"]);
                        ViewState["TotalAmount"] = Convert.ToString(TotalAmount);
                        ViewState["Amount"] = dtTour.Tables[0].Rows[0]["fare"].ToString();
                        ViewState["STaxValue"] = dtTour.Tables[0].Rows[0]["stax"].ToString();
                        ViewState["NoOfpax"] = dtTour.Tables[0].Rows[0]["noofpax"].ToString();
                        ViewState["TicketNo"] = dtTour.Tables[0].Rows[0]["cabid"].ToString();
                        string paymode = dtTour.Tables[0].Rows[0]["PayMode"].ToString();
                        string ss = "select Paymentmode from tbl_paymodes where rowid='1'";
                        string pm = DataLib.GetStringData(DataLib.Connection.ConnectionString, ss);

                        ViewState["PaymentMode"] = pm.ToString();
                        ViewState["AgentCredit"] = dtTour.Tables[0].Rows[0]["agentcredit"].ToString();
                        ViewState["AgentDebit"] = dtTour.Tables[0].Rows[0]["agentdebit"].ToString();
                        ViewState["TDS"] = dtTour.Tables[0].Rows[0]["tds"].ToString();
                        ViewState["AgentId"] = dtTour.Tables[0].Rows[0]["agentid"].ToString();
                        ViewState["Advance"] = dtTour.Tables[0].Rows[0]["credit"].ToString();
                        ViewState["Cancelled"] = dtTour.Tables[0].Rows[0]["Cancelled"].ToString();



                        lblTicketNo.Text = dtTour.Tables[0].Rows[0]["cabid"].ToString();
                        lblJourneyDate.Text = Convert.ToDateTime(dtTour.Tables[0].Rows[0]["picktuptime"].ToString()).ToShortDateString();
                        lblTourName.Text = dtTour.Tables[0].Rows[0]["transfername"].ToString();
                        string strCarName = dtTour.Tables[0].Rows[0]["CarName"].ToString();
                        lblVehicle.Text = strCarName;
                        lblPickupFrom.Text = dtTour.Tables[0].Rows[0]["pickupaddress"].ToString();
                        lblPickupTime.Text = dtTour.Tables[0].Rows[0]["time"].ToString();
                        lblDropAt.Text = dtTour.Tables[0].Rows[0]["dropoffaddress"].ToString();

                        lblName.Text = dtTour.Tables[0].Rows[0]["firstname"].ToString();
                        lblAddress.Text = dtTour.Tables[0].Rows[0]["addr1"].ToString();
                        lblPax.Text = dtTour.Tables[0].Rows[0]["noofpax"].ToString();
                        lblAmount.Text = dtTour.Tables[0].Rows[0]["fare"].ToString();
                        lblServiceTax.Text = dtTour.Tables[0].Rows[0]["stax"].ToString();
                        lblTotalAmount.Text = TotalAmount.ToString();
                        lblPaymentMode.Text = pm.ToString();
                        lblBranchCode.Text = dtTour.Tables[0].Rows[0]["branchcode"].ToString();

                        if (Convert.ToString(dtTour.Tables[0].Rows[0]["cancelled"]) != "True")
                        {
                            showTables();
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
        }
        protected void btncantic_Click(object sender, EventArgs e)
        {
            btncantic.Enabled = false;
            btncantic.Visible = false;
            string returnstr = "";
            if (chkAccept.Checked == true)
            {
                lblerrmsg.Text = "";
                string sdfasdf = ViewState["Cancelled"].ToString();
                if (sdfasdf == "False")
                {
                    returnstr = cancelticket(Convert.ToString(ViewState["TicketNo"]));
                }
                else
                {
                    lblerrmsg.Text = "Ticket already Cancelled.";
                }
                if (returnstr == "Success")
                {
                    //////////////////////////Print ticket-----------------------
                    ViewCancelTicket();
                }
                else
                {
                    lblerrmsg.Text = "Please fill all mandatory fields.";
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
            chkAccept.Visible = true;
            tblRuleRegulation.Visible = true;
            trCancelView.Visible = false;
            trPrint.Visible = false;
            tblPrintBG = "N";

        }
        private void showCancelTable()
        {
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblRuleRegulation.Visible = true;
            trCancelView.Visible = true;
            trPrint.Visible = true;

            trTicketView.Visible = false;
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
            /*string str3 = @"SELECT     TOP 1 a.TicketNo AS cabid, a.TourName AS transfername, a.NoOfPax, a.PickTupTime AS pickuptime, a.PickTupTime AS time, ISNULL(a.PickupAddress, 
                          'Not Decide') AS pickupaddress, ISNULL(a.DropOffAddress, 'Not Decide') AS dropoffaddress, a.EmailId AS email, a.STax, b.PaymentMode AS PayMode, 
                          a.Advance, a.Discount, a.Cancelled AS cancelled, a.paySucceeded AS paysucceeded, b.AgentId, b.AgentCredit, b.AgentDebit, b.Credit, b.Debit, b.TDS, 
                          b.BranchCode, d.FirstName AS firstname, d.Mobile, d.Addr1 AS addr1, a.Fare AS fare, c.CanCharges AS cancellationamt, c.RefundAmt, 
                          d.email AS Expr1,a.CarName
                           FROM         tbl_CarBookings_Log a INNER JOIN
                          OnlineTransactionTable b ON a.TicketNo = b.RefNo INNER JOIN
                          Cancel c ON a.TicketNo = c.TicketNo INNER JOIN
                          OnlineCustomer d ON a.EmailId = d.Mobile OR a.EmailId = d.email
             * where a.ticketno='" + txtpNRo.Text + "' and (d.email='" + txtemail.Text + "' or d.mobile='" + txtemail.Text + "') 
             * and convert(varchar(10),a.picktuptime,101)='" + DateJourneySQL + "' and a.ticketno=b.refno";


            DataTable dtTour = new DataTable();
            dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
            #endregion
            DataTable dtTour = clsObj.fnAgentCancelTicket(txtpNRo.Text, txtemail.Text, Convert.ToDateTime(DateJourneySQL));
            if (dtTour.Rows.Count > 0)
            {
                TotalAmount = Convert.ToDecimal(dtTour.Rows[0]["fare"]) + Convert.ToDecimal(dtTour.Rows[0]["stax"]);
                lblTicketNo.Text = dtTour.Rows[0]["cabid"].ToString();
                lblJourneyDate.Text = Convert.ToDateTime(dtTour.Rows[0]["pickuptime"].ToString()).ToShortDateString();
                lblTourName.Text = dtTour.Rows[0]["transfername"].ToString();
                string strCarName = dtTour.Rows[0]["CarName"].ToString();
                lblVehicle.Text = strCarName;
                lblPickupFrom.Text = dtTour.Rows[0]["pickupaddress"].ToString();
                lblPickupTime.Text = dtTour.Rows[0]["time"].ToString();
                lblDropAt.Text = dtTour.Rows[0]["dropoffaddress"].ToString();
                //lblName.Text = dtTour.Rows[0]["firstname"].ToString() + ' ' + dtTour.Rows[0]["lastname"].ToString();
                lblName.Text = dtTour.Rows[0]["firstname"].ToString();
                lblAddress.Text = dtTour.Rows[0]["addr1"].ToString();
                lblPax.Text = dtTour.Rows[0]["noofpax"].ToString();
                lblAmount.Text = dtTour.Rows[0]["fare"].ToString();
                lblServiceTax.Text = dtTour.Rows[0]["stax"].ToString();
                lblTotalAmount.Text = TotalAmount.ToString();
                lblCancellationCharge.Text = dtTour.Rows[0]["cancellationamt"].ToString();
                lblRefundAmount.Text = dtTour.Rows[0]["refundamt"].ToString();

                try
                {
                    if (Session["BranchId"] != null)
                    {
                        clsObj = new ClsAdo();
                        string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        int Val = clsObj.fnSaveBranchToAgentBooking(Convert.ToString(txtpNRo.Text), Convert.ToInt32(Session["BranchUserId"]),
                            Convert.ToInt32(Session["AgentId"]), REMOTE_ADDR);
                    }
                }
                catch { }

                if ((Session["AgentId"] == null) && (Session["BranchId"] != null))
                {
                    ////----Need to extract the sum of advance amount from Onlinetransaction_Advance table-----

                    lblAdvance.Text = dtTour.Rows[0]["advance"].ToString();
                    if (dtTour.Rows[0]["advance"] == null || dtTour.Rows[0]["advance"].ToString() == "")
                    {
                        string s = Convert.ToString(Convert.ToDecimal(TotalAmount.ToString()));
                        lblBalance.Text = s.ToString();
                    }
                    else
                    {
                        string s = Convert.ToString(Convert.ToDecimal(TotalAmount.ToString()) - Convert.ToDecimal(dtTour.Rows[0]["advance"].ToString())).ToString();
                        lblBalance.Text = s.ToString();
                    }
                }
                else
                {
                    AdvanceHide.Visible = false;
                    BalanceHide.Visible = false;
                }
            }
        }

        //private string cancelticket1(string ticketid)
        //{
        //    string sqladd = "", CancelCode, SuccessSaving;
        //    int CancelNo = 0, CancelRowid = 0, Cancel1Rowid = 0, totalchars, i;


        //    decimal cancelcharges = 0, stvalue = 0, refundamount = 0;

        //    DateTime journeydate;
        //    journeydate = Convert.ToDateTime(txtjdate.Value.Split('/').GetValue(1) + "/" + txtjdate.Value.Split('/').GetValue(0) +
        //        "/" + txtjdate.Value.Split('/').GetValue(2));
        //    int NoofDaysleft;
        //    TimeSpan ts = new TimeSpan();
        //    ts = journeydate.Subtract(DateTime.Now.Date);
        //    NoofDaysleft = ts.Days;

        //    decimal cancelpercen;
        //    if (NoofDaysleft > 1)
        //    {
        //        cancelpercen = 10;
        //    }
        //    else
        //    {
        //        cancelpercen = 25;
        //    }

        //    cancelcharges = decimal.Round((Convert.ToDecimal(ViewState["Amount"]) + stvalue) * (Convert.ToDecimal(cancelpercen) / 100));
        //    stvalue = Convert.ToDecimal(ViewState["STaxValue"]);
        //    refundamount = (Convert.ToDecimal(ViewState["Advance"]) - cancelcharges);


        //    decimal AvailableBalance = 0;
        //    string balance = "";
        //    int agentid = Convert.ToInt32(Session["agentID"]);
        //    if (agentid > 0)
        //    {
        //        #region Optimize Code
        //        /*string qry, qry1;
        //        qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + agentid;
        //        string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
        //        if (strRowNo == "" || strRowNo == null)
        //        {
        //            balance = "";
        //        }
        //        else
        //        {
        //            qry1 = "select availablebalance from OnlineTransactionTable(nolock) where agentid=" + agentid + " and rowid=" + strRowNo;
        //            balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
        //        }*/
        //        #endregion

        //        DataTable ldtRecSet = clsObj.fnGetAgent_AvailableBalance(Convert.ToInt32(Session["AgentId"]));
        //        if (ldtRecSet.Rows.Count > 0)
        //        {
        //            balance = Convert.ToString(ldtRecSet.Rows[0][0]);
        //        }
        //        else
        //        {
        //            balance = "";
        //        }
        //        if (balance == "")
        //            Session["Balance"] = "0";
        //        else
        //            Session["Balance"] = balance;

        //    }
        //    if (Convert.ToString(balance) != "")
        //    {
        //        AvailableBalance = Convert.ToDecimal(balance);
        //    }
        //    try
        //    {
        //        //updating the TicketDetails table by setting the cancellation field as Y 
        //        #region Optimize Code
        //        /*string query = "UPDATE    tbl_CarBookings_Log SET   CancellationAmt ='" + cancelcharges + "', 
        //         * RefundAmt ='" + refundamount + "', Cancelled ='1' where ticketno='" + ticketid.Trim() + "'";
        //        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, query, false);

        //        query = "";
        //        query = "Select NumericValue,RespectiveRowid from NewKeyTable_online where keyType='Cancel'";
        //        DataTable dtcan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, query);*/
        //        #endregion
        //        DataTable dtcan = clsObj.fnNewKeyTableID("Cancel");
        //        if (dtcan.Rows.Count > 0)
        //        {
        //            CancelNo = Convert.ToInt32(dtcan.Rows[0]["NumericValue"]);
        //            CancelRowid = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
        //            Cancel1Rowid = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
        //        }
        //        //Got the Last Ticket and 1 is added to that for a new One 
        //        CancelNo = CancelNo + 1;
        //        CancelRowid = CancelRowid + 1;
        //        Cancel1Rowid = Cancel1Rowid + 1;
        //        //Generating the CancelCode(Cancellation Number) which is store in the Cancel table 

        //        string CancelString;
        //        CancelCode = "";
        //        CancelString = CancelNo.ToString();
        //        totalchars = CancelString.Length;
        //        for (i = 0; i <= (5 - totalchars); i++)
        //        {
        //            CancelCode = CancelCode + 0;
        //        }
        //        CancelCode = "CANCAB" + CancelCode + CancelString;

        //        string sBranchCode;
        //        if (Session["BranchCode"] != null)
        //            sBranchCode = Session["BranchCode"].ToString();
        //        else
        //            sBranchCode = "";



        //        //inserting all the fields in the cancel table. 

        //        #region Optimize Code
        //        /*sqladd = "insert into cancel(RowId,CanNo,TicketNo,CanNoTick,CanCharges,RefundAmt,UserName,BranchCode)
        //        values(" + CancelRowid + ",'" + CancelCode + "','" + ticketid.Trim() + "'," + Convert.ToInt32(ViewState["NoOfpax"]) + ",
        //        " + cancelcharges + "," + refundamount + ",'" + Session["BranchUserId"] + "','" + sBranchCode + "')";
        //        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);

        //        //Updating the NewKeyTable_Online with the new Cancel Number and Rowid 
        //        sqladd = "";
        //        sqladd = "update newkeytable_online set NumericValue = " + CancelNo + " , RespectiveRowid=" + CancelRowid + " where KeyType = 'Cancel' ";
        //        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);

        //        string strid = "select transactiontypeid from transactiontypemaster where transactionname='AgentCabCancellation'";
        //        string transtype = DataLib.GetStringData(DataLib.Connection.ConnectionString, strid);
        //        sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,Commission,
        //        PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,Cashier,TransState,
        //        Status)values(0,'" + CancelCode + "'," + Convert.ToInt16(transtype) + ",0,0,0," + refundamount + ",0,
        //            '" + Convert.ToString(ViewState["PaymentMode"]) + "','',''," + refundamount + "," + (cancelcharges) + ",'',
        //            '" + ViewState["UserName"] + "','" + ViewState["BranchCode"] + "','','','','P','S')";
        //        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);*/
        //        #endregion
        //        int val4 = clsObj.fnUpdateCarCancelInfo(Convert.ToDecimal(cancelcharges), Convert.ToDecimal(refundamount), ticketid.Trim(), CancelRowid, CancelNo,
        //            CancelCode, Convert.ToInt32(ViewState["NoOfpax"]), Convert.ToString(ViewState["UserName"]), Convert.ToString(ViewState["BranchCode"]),
        //            Convert.ToString(Session["BranchUserId"]), sBranchCode, Convert.ToString(ViewState["PaymentMode"]));
        //        SuccessSaving = "Success";
        //    }
        //    catch (Exception ex_trans)
        //    {
        //        SuccessSaving = "Failure";
        //    }
        //    return SuccessSaving;
        //}

        private string cancelticket(string ticketid)
        {
            string[] seatNotCancelled = new string[21];
            int totalchars, CanBusnoRowid = 0, CanBusno1Rowid, count;
            string[] stringsep;
            string CancelCode, SuccessSaving, sqladd;
            int SeatArrRowid = 0, i, BusNo = 0, CancelNo = 0, CancelRowid = 0, Cancel1Rowid = 0;
            int[] BusNoCancelled = new int[16];
            int CancelNameRowid, CancelName1Rowid;
            CancelNameRowid = 0;
            CancelName1Rowid = 0;
            decimal cancelcharges = 0, stvalue = 0, cc = 0, refundamount = 0;
            int tourno = Convert.ToInt32(ViewState["TourNo"]);
            decimal lCalTds = 0;
            string journeydate11;
            DateTime journeydate;
            //string[] dd = lblPickupTime.Text.Split('/');
            //if (dd[0].Length <= 1)
            //    dd[0] = "0" + dd[0];
            //if (dd[1].Length <= 1)
            //    dd[1] = "0" + dd[1];

            journeydate11 = lblPickupTime.Text;


            journeydate = Convert.ToDateTime(journeydate11);
            //journeydate = Convert.ToDateTime(txtjdate.Value.Split('/').GetValue(1) + "/" + txtjdate.Value.Split('/').GetValue(0) + "/" + txtjdate.Value.Split('/').GetValue(2));

            int NoofDaysleft;
            TimeSpan ts = new TimeSpan();
            ts = journeydate.Subtract(DateTime.Now);
            NoofDaysleft = ts.Days;
            //decimal cancelpercen = Convert.ToDecimal(canpercentage(tourno, NoofDaysleft));
            decimal cancelpercen;
            if (NoofDaysleft >= 1)
            {
                cancelpercen = 10;
            }
            else
            {
                cancelpercen = 25;
            }
            Session["CancelCharge"] = Convert.ToString(cancelpercen);
            /*cancelcharges = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(Session["CancelCharge"]) / 100)));



             stvalue = Convert.ToDecimal(ViewState["STaxValue"]);*/

            decimal sTaxValue = Convert.ToDecimal(clsObj.fnGetSTaxForCanTkt(ticketid));
            stvalue = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(sTaxValue) / 100)));

            cancelcharges = decimal.Round((Convert.ToDecimal(ViewState["Amount"]) + stvalue) * (Convert.ToDecimal(Session["CancelCharge"]) / 100));

            if (Convert.ToDecimal(ViewState["cctaxvalue"]) > 0)
            {
                cc = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(ViewState["cctaxvalue"]) / 100)));
            }

            refundamount = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - cancelcharges);

            decimal AvailableBalance = 0;

            //int agentid = Convert.ToInt32(ViewState["agentID"]);


            string balance = "";
            DataTable ldtRecSet = clsObj.fnGetAgent_AvailableBalance(Convert.ToInt32(Session["AgentId"]));
            if (ldtRecSet != null && ldtRecSet.Rows.Count > 0)
            {
                balance = Convert.ToString(ldtRecSet.Rows[0][0]);
            }
            else
            {
                balance = "";
            }
            if (balance == "")
                Session["Balance"] = "0";
            else
                Session["Balance"] = balance;

            /******** Cancellation Commission ********/

            DataTable dtComm = new DataTable();
            decimal agentCancellationCommission = default(decimal);
            //if (Session["AgentLevel"] != null)
            //{
            dtComm = clsObj.fnAgentCommission("CarCancel", Convert.ToInt32(Session["AgentId"]));
            if (dtComm != null && dtComm.Rows.Count > 0)
            {
                agentCancellationCommission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
            }
            //}
            decimal CancellationCommission = (cancelcharges * agentCancellationCommission) / 100;
            decimal lTdsPer = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));

            lCalTds = Convert.ToDecimal(CancellationCommission * (Convert.ToDecimal(lTdsPer) / 100));

            CancellationCommission = CancellationCommission - lCalTds;


            int AgentCredit = Convert.ToInt32(decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommission));
            /******** Cancellation Commission End ********/


            if (Convert.ToString(Session["Balance"]) != "")
            {
                AvailableBalance = Convert.ToDecimal(Session["Balance"]) + AgentCredit;
            }

            try
            {



                int? returnParam = 0;


                int AgentDebit = Convert.ToInt32(decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)));
                int ticketAmount = Convert.ToInt32(decimal.Round(Convert.ToDecimal(ViewState["Amount"])));
                int noOfPa = Convert.ToInt32(ViewState["NoOfpax"]);
                string Branch = Convert.ToString(Session["LocalBranch"]);

                int val1 = clsObj.fnUpdateCarCancelInfo(
                    decimal.Round(cancelcharges),
                    decimal.Round(refundamount),
                    ticketid,
                    Convert.ToInt32(ViewState["NoOfpax"]),
                    Convert.ToString(Session["LocalBranch"]),
                    Convert.ToString(Session["LocalBranch"]),
                    Convert.ToInt32(Session["AgentId"]),
                    AgentCredit,
                    AvailableBalance,
                    AgentDebit,
                    ticketAmount,
                    CancellationCommission,
                    "AgentCash",
                    AgentCredit,
                    AgentDebit,
                    Convert.ToString(Session["UserId"]),
                    Convert.ToString(Session["LocalBranch"]),
                    stvalue,
                    lCalTds,
                    ref returnParam);
                SuccessSaving = "Success";
            }
            catch (Exception ex_trans)
            {
                //Trans_On2line.Rollback();
                //Response.Write(ex_trans.ToString());
                SuccessSaving = "Failure";
            }
            finally
            {
            }
            return SuccessSaving;
        }

        #endregion
    }
}