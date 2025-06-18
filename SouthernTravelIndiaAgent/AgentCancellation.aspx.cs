using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentCancellation : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        protected StringBuilder BoookChart;
        protected StringBuilder VacantChart;
        protected ArrayList branchlist, BusSerial;
        protected string TicketNo, BranchCode, TourName, UserName, orderid, OName, title, busserialno;
        protected int NAdult, NAdultsTwin, NadultsTriple, NSingleAdult, NChild, NChildwithoutBed, Ndormitory, Age, agentID;
        protected DateTime ReportTime, JourneyDate, DepartTime;
        protected string Name, BusNo, SeatNumbers, DateJourney, DateJourneySQL, PickUppoint;
        protected decimal Amount, TotalAmount, Total, AgentCredit, AgentDebit, TDS, STaxValue, cctaxvalue, AdultServiceCharges = 0, ChildServiceCharges = 0,
            ServiceChargesTotal = 0, ServiceChargesTax = 0, ServiceChargesTaxVal = 0;
        protected string PaymentMode, BankName, TransNumber, TelNo, Sex;
        protected int AdultFare, Adultstwinfare, adultstriplefare, singleadultfare, ChildFare, childwithoutbedfare, dormitoryfare, AWFoodFare, CWFoodFare;
        protected StringBuilder Ticketstr;
        protected string CurrentBranchCode = System.Configuration.ConfigurationManager.AppSettings["BranchCode"];
        protected int tourserialno, tpax = 0, i, TourNo;
        protected int? TourSerial;
        protected StringBuilder stbuild = new StringBuilder();
        protected int preadult, prechild, preadulttwin, preadulttriple, presingleadult, prechilewithoutbed, predormitory, AWFood, CWFood;
        ClsAdo pclsObj = null;
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }
            this.btnsubmit.Attributes.Add("onclick", "javascript:return validation();");
            DateJourney = txtjdate.Value.ToString();
            if (DateJourney == "")
            {
                DateJourneySQL = Convert.ToDateTime(System.DateTime.Now.Date).AddDays(-0).ToShortDateString();
                //txtjdate.Value = mmddyy2ddmmyy(DateJourneySQL);
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
                btnPartialTicket.Visible = false;
                btncantic.Visible = false;
            }
            this.btnPartialTicket.Attributes.Add("onclick", "javascript:return validateruntime();");
            btnPartialTicket.Attributes.Add("onclick", "javascript:return checkradio();");
            btncantic.Attributes.Add("onclick", "javascript:return chkterms();");
            return;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            lblticket.Text = "";
            tblRuleRegulation.Visible = false;

            if (txtjdate.Value.Trim() == "")
            {
                lblerrmsg.Text = "Enter Journey date.";
                return;
            }
            if (txtpNRo.Text.Trim().ToUpper().IndexOf("CAB") != -1)
            {

            }
            TimeSpan hr;
            DateTime jdate = Convert.ToDateTime(mmddyy2ddmmyy(txtjdate.Value));
            DateTime tdate = System.DateTime.Now;
            hr = jdate.Subtract(tdate);
            double noofhours = hr.TotalHours;
            int child = 0, childwithoutbed = 0;
            //if (noofhours > 24)
            //{
            lblerrmsg.Text = "";
            String Email;
            Email = txtemail.Text;
            #region Optimize Code
            /*string str3 = "select ticketcode,BusSerialNo,TourSerial,
             * (NoofAdults+NoofChild+NoofAdultsTwin+NoofAdultsTriple+ChildWithoutbed+SingleAdult+dormitory) as totalpax,
             * NoofAdults,NoofChild,NoofAdultsTwin,NoofAdultsTriple,ChildWithoutbed,SingleAdult,dormitory from OnlineToursBooking 
             * inner join OnlineCustByOrder on OnlineCustByOrder.OrderId = OnlineToursBooking.OrderId 
             * inner join onlinecustomer on onlinecustomer.RowId =OnlineCustByOrder.OcustRowid where OnlineToursBooking.orderid='" + txtpNRo.Text + "'
             * and OnlineToursBooking.TicketCode='" + txtticketno.Text + "'  and (onlinecustomer.email='" + Email + "'
             * or onlinecustomer.mobile='" + Email + "') and doj='" + DateJourneySQL + "'";
            string tick = "NA";
            DataTable dtTour = new DataTable();
            dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
            #endregion
            string tick = "NA";

            pclsObj = new ClsAdo();
            DataSet ldsRecSet = pclsObj.fnAgentFixedTourInfo(txtpNRo.Text, Email, txtticketno.Text, Convert.ToDateTime(DateJourneySQL));
            DataTable dtTour = ldsRecSet.Tables[0];
            DataTable dtboarding = null, dtcheck = null, dtnames = null;
            try
            {
                if (dtTour.Rows.Count > 0)
                {
                    tpax = Convert.ToInt32(dtTour.Rows[0]["totalpax"]);
                    ViewState["tpax"] = tpax;
                    tick = dtTour.Rows[0]["ticketcode"].ToString();
                    busserialno = Convert.ToString(dtTour.Rows[0]["BusSerialNo"]);
                    tourserialno = Convert.ToInt32(dtTour.Rows[0]["TourSerial"]);
                    child = Convert.ToInt32(dtTour.Rows[0]["NoofChild"]);

                    preadult = Convert.ToInt32(dtTour.Rows[0]["NoofAdults"]);
                    preadulttwin = Convert.ToInt32(dtTour.Rows[0]["NoofAdultsTwin"]);
                    preadulttriple = Convert.ToInt32(dtTour.Rows[0]["NoofAdultsTriple"]);
                    presingleadult = Convert.ToInt32(dtTour.Rows[0]["SingleAdult"]);
                    prechild = Convert.ToInt32(dtTour.Rows[0]["NoofChild"]);
                    prechilewithoutbed = Convert.ToInt32(dtTour.Rows[0]["ChildWithoutbed"]);
                    predormitory = Convert.ToInt32(dtTour.Rows[0]["dormitory"]);

                    AWFood = Convert.ToInt32(dtTour.Rows[0]["noAdultWithFood"]);
                    CWFood = Convert.ToInt32(dtTour.Rows[0]["noChildWithFood"]);

                    childwithoutbed = Convert.ToInt32(dtTour.Rows[0]["ChildWithoutbed"]);
                    totadu.Value = Convert.ToString(preadult + preadulttwin + preadulttriple + presingleadult + predormitory + AWFood);
                    totchi.Value = Convert.ToString(prechild + childwithoutbed + CWFood);
                    ViewState["tourserialno"] = Convert.ToString(tourserialno);
                    ViewState["busserialno"] = Convert.ToString(busserialno);
                    ViewState["IsPartialPaymentByAgent"] = false;
                    if (dtTour.Rows[0]["IsPartialPaymentByAgent"] != null)
                    {
                        ViewState["IsPartialPaymentByAgent"] = Convert.ToBoolean(dtTour.Rows[0]["IsPartialPaymentByAgent"]);
                    }
                }
                if (!(tick == "NA"))
                {
                    #region Optimize Code
                    /*str3 = "select * from boardingpass where ticketno='" + tick + "'";
                DataTable dtboarding = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
                    #endregion
                    dtboarding = ldsRecSet.Tables[1];

                    if (dtboarding.Rows.Count == 0)
                    {
                        #region Optimize Code
                        /*str3 = "select AgentId,BranchCode from ticketdetails where ticketno='" + tick + "'";
                    DataTable dtcheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/

                        //if ((Convert.ToString(dtcheck.Rows[0]["AgentId"]) == Convert.ToString(Session["AgentId"])) && (Convert.ToString(dtcheck.Rows[0]["BranchCode"]) == "EBK0001"))
                        #endregion
                        dtcheck = ldsRecSet.Tables[2];

                        if (Convert.ToString(dtcheck.Rows[0]["AgentId"]) == Convert.ToString(Session["AgentId"]))
                        {
                            if (PartialCancellation.Checked == true)
                            {
                                lblticket.Text = TicketString(tick, 1, 'N').ToString();
                                canrowid.Value = "";
                                canc.Value = "0";
                                maxSeatCancel.Value = "";
                                optedSeatNosBook.Value = "";
                                GetSeatDetails();//Seatting Chart
                                #region Optimize Code
                                /*str3 = "Select rowid,name,age,sex from ticketnames where ticketno='" + tick + "' order by age desc";
                            DataTable dtnames = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
                                #endregion
                                dtnames = ldsRecSet.Tables[3];

                                tpx.Value = Convert.ToString(tpax);
                                if (child == 0)
                                {
                                    trcf.Visible = false;
                                    tr8.Visible = false;
                                }
                                if (CWFood == 0)
                                {

                                    trCWF.Visible = false;
                                    trCWFColor.Visible = false;
                                }
                                if (childwithoutbed == 0)
                                {
                                    trcbf.Visible = false;
                                    tr11.Visible = false;
                                }
                                for (int j = 0; j <= Convert.ToInt32(dtnames.Rows.Count - 1); j++)
                                {
                                    int i = Convert.ToInt32(dtnames.Rows[j]["rowid"]);
                                    string fullname = dtnames.Rows[j]["name"].ToString();
                                    string age = dtnames.Rows[j]["age"].ToString();
                                    char sex = Convert.ToChar(dtnames.Rows[j]["sex"]);
                                    string ss = "";
                                    string[] nn = fullname.Split('.');
                                    string name = nn[1];
                                    string title = nn[0];
                                    if (sex == 'M')
                                    {
                                        ss = "Male";
                                    }
                                    else
                                    {
                                        ss = "Female";
                                    }
                                    if (((preadult != 0) || (prechild != 0)) || ((AWFood != 0) || (CWFood != 0)))
                                    {
                                        stbuild.Append("<table width=100% border=0>");
                                        if (j == 0)
                                        {
                                            stbuild.Append("<TR>");
                                            stbuild.Append("<TD class=hlinks colspan=4 align=left valign=middle width=100% height=15>Select the Person to whom you want to Cancel</TD>");
                                            stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc height=1 ></td></tr>");
                                            stbuild.Append("</TR>");
                                        }
                                        stbuild.Append("<TR>");
                                        //if (NameCtr < preadult)
                                        if ((preadult != 0) && (j < preadult))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkadul" + i + " value=" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Adult</TD>");
                                        }
                                        //if (NameCtr < AWFood)
                                        else if ((AWFood != 0) && (j < AWFood + preadult))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkaduF" + i + " value=" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Adult With South Veg Food</TD>");
                                        }
                                        else if ((prechild != 0) && (j < AWFood + preadult + prechild))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkchil" + i + " value=" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Child</TD>");
                                        }
                                        else
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkchiF" + i + " value=" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Child With South Veg Food</TD>");
                                        }
                                        stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc height=1 ></td></tr>");
                                        stbuild.Append("</table>");
                                    }
                                    else
                                    {

                                        stbuild.Append("<table width=100% border=0>");
                                        if (j == 0)
                                        {
                                            stbuild.Append("<TR>");
                                            stbuild.Append("<TD class=hlinks colspan=5 align=left valign=middle width=100% height=15>Select the Person to whom you want to Cancel</TD>");
                                            stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc height=1 ></td></tr>");
                                            stbuild.Append("</TR>");
                                        }
                                        stbuild.Append("<TR>");
                                        if ((preadulttwin != 0) && (j < preadulttwin))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkTwin" + i + " value=" + i + " Name=chkTwin" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Twin Share</TD>");
                                        }
                                        else if ((preadulttriple != 0) && (j < preadulttwin + preadulttriple))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox  ID=chkTriple" + i + " value=" + i + " Name=chkTrip" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Triple Share</TD>");
                                        }
                                        else if ((presingleadult != 0) && (j < preadulttwin + preadulttriple + presingleadult))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkSingle" + i + " value=" + i + " Name=chkSing" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Single Share</TD>");
                                        }
                                        else if ((predormitory != 0) && (j < preadulttwin + preadulttriple + presingleadult + predormitory))
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkDormit" + i + " value=" + i + " Name=chkDorm" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Dormitory Accommodation</TD>");
                                        }
                                        else
                                        {
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=10% height=15><input type=checkbox ID=chkwith" + i + " value=" + i + " Name=chkwith" + i + " onclick=\"cancelpax(this);\"  runat=server class=cgi1 /></td>");
                                            stbuild.Append("<TD class=hlinks align=left valign=middle width=30% height=15>" + name + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Age: " + age + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>" + ss + "</TD>");
                                            stbuild.Append("<TD class=hlinks  valign=left align=center width=20% height=15>Child Without Bed</TD>");
                                        }
                                        stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc height=1 ></td></tr>");
                                        stbuild.Append("</table>");
                                    }
                                }
                                if (lblticket.Text == "")
                                {
                                    lblerrmsg.Text = "Invalid entry Please check it & try again.";
                                    tblRuleRegulation.Visible = false;
                                    hideTables();
                                    btnPartialTicket.Visible = false;
                                    btncantic.Visible = false;
                                    Remainpax.Visible = false;
                                    listpax.Visible = false;
                                    trSeat.Visible = false;
                                }
                                else if (lblticket.Text == "Y")
                                {
                                    hideTables();
                                    btnPartialTicket.Visible = false;
                                    btncantic.Visible = false;
                                    lblticket.Text = CancelTicketString(tick, 1).ToString();
                                    lblerrmsg.Text = "This Ticket Was Already Cancelled.";
                                    tblRuleRegulation.Visible = false;
                                    Remainpax.Visible = false;
                                    listpax.Visible = false;
                                    trSeat.Visible = false;
                                }
                                tblRuleRegulation.Visible = true;
                            }
                            else
                            {
                                TransNumber = txtpNRo.Text;
                                ViewState["TransNumber"] = TransNumber;
                                lblticket.Text = TicketString(tick, 1, 'Y').ToString();
                                if (lblticket.Text == "")
                                {
                                    lblerrmsg.Text = "Invalid entry Please check it & try again.";
                                    tblRuleRegulation.Visible = false;
                                    hideTables();
                                    btnPartialTicket.Visible = false;
                                    btncantic.Visible = false;
                                    Remainpax.Visible = false;
                                    listpax.Visible = false;
                                    trSeat.Visible = false;
                                }
                                else if (lblticket.Text == "Y")
                                {
                                    lblticket.Text = CancelTicketString(tick, 1).ToString();
                                    lblerrmsg.Text = "This Ticket Was Already Cancelled.";
                                    tblRuleRegulation.Visible = false;
                                    hideTables();
                                    btnPartialTicket.Visible = false;
                                    btncantic.Visible = false;
                                    Remainpax.Visible = false;
                                    listpax.Visible = false;
                                    trSeat.Visible = false;
                                }
                                tblRuleRegulation.Visible = true;
                            }
                        }
                        else
                        {
                            lblerrmsg.Text = "You Don't Have Permission to Cancel This Ticket";
                        }
                    }
                    else
                    {
                        lblerrmsg.Text = "Boarding Pass Issued. Can not Cancel the ticket";
                        tblRuleRegulation.Visible = false;
                    }
                }
                else
                {
                    lblerrmsg.Text = "Invalid entry Please check it & try again.";
                }
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtTour != null)
                {
                    dtTour.Dispose();
                    dtTour = null;
                }
                if (dtboarding != null)
                {
                    dtboarding.Dispose();
                    dtboarding = null;
                }
                if (dtcheck != null)
                {
                    dtcheck.Dispose();
                    dtcheck = null;
                }
                if (dtnames != null)
                {
                    dtnames.Dispose();
                    dtnames = null;
                }
            }
        }
        protected void btncantic_Click(object sender, EventArgs e)
        {
            btncantic.Enabled = false;
            string returnstr = "";
            if (chkAccept.Checked == true)
            {
                lblerrmsg.Text = "";
                if (ViewState["CarCancel"] == null)
                {
                    pclsObj = new ClsAdo();
                    DataTable dtTour = pclsObj.fnbr_can_ticketstring(Convert.ToString(ViewState["TicketNo"]));
                    if (Convert.ToString(dtTour.Rows[0]["Cancelled"]) == "Y")
                    {
                        String Email;
                        if ((Convert.ToString(txtemail.Text) == "") || (Convert.ToString(txtemail.Text) == null))
                        {
                            Email = txtpNRo.Text.ToString().Substring(3) + "temp.com";
                        }
                        else
                        {
                            Email = txtemail.Text;
                        }
                        string tick = "";
                        pclsObj = new ClsAdo();
                        DataTable dtTour2 = pclsObj.fnAgentFixedTourDetail(txtpNRo.Text, Email, txtticketno.Text, Convert.ToDateTime(DateJourneySQL));
                        try
                        {
                            if (dtTour2.Rows.Count > 0)
                            {
                                tick = dtTour2.Rows[0]["ticketcode"].ToString();
                            }
                            lblticket.Text = CancelTicketString(tick, 1).ToString();
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
                        lblerrmsg.Text = "This Ticket Was Already Cancelled.";
                        return;
                    }
                    else
                    {
                        returnstr = cancelticket(Convert.ToString(ViewState["TicketNo"]));
                    }
                }
                if (returnstr == "Success")
                {
                    String Email;
                    if ((Convert.ToString(txtemail.Text) == "") || (Convert.ToString(txtemail.Text) == null))
                    {
                        Email = txtpNRo.Text.ToString().Substring(3) + "temp.com";
                    }
                    else
                    {
                        Email = txtemail.Text;
                    }
                    #region Optimize Code
                    /*string str3 = "select ticketcode,BusSerialNo,TourSerial from OnlineToursBooking inner join OnlineCustByOrder on 
                     * OnlineCustByOrder.OrderId = OnlineToursBooking.OrderId inner join onlinecustomer on onlinecustomer.RowId =OnlineCustByOrder.OcustRowid
                     * where OnlineToursBooking.orderid='" + txtpNRo.Text + "'and OnlineToursBooking.TicketCode='" + txtticketno.Text + "' 
                     * and (onlinecustomer.email='" + Email + "' or onlinecustomer.mobile='" + Email + "')  and doj='" + DateJourneySQL + "'";

                    DataTable dtTour = new DataTable();
                    dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str3);*/
                    #endregion
                    string tick = "";
                    pclsObj = new ClsAdo();
                    DataTable dtTour = pclsObj.fnAgentFixedTourDetail(txtpNRo.Text, Email, txtticketno.Text, Convert.ToDateTime(DateJourneySQL));
                    try
                    {
                        if (dtTour.Rows.Count > 0)
                        {
                            tick = dtTour.Rows[0]["ticketcode"].ToString();
                        }
                        lblticket.Text = CancelTicketString(tick, 1).ToString();
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
                else
                {
                    lblerrmsg.Text = "Please fill all mandatory fields.";
                }
            }
            else
            {
                btncantic.Enabled = true;
                lblerrmsg.Text = "You are not agreed our terms and conditions.";
                RegisterStartupScript("Error", "<Script>alert('You Must Agree the Terms and Conditions Before Cancellation')</script>");
            }
            btncantic.Enabled = true;
        }
        protected void btnPartialTicket_Click(object sender, EventArgs e)
        {
            this.btnPartialTicket.Attributes.Add("OnClick", "validateruntime();");
            btnPartialTicket.Attributes.Add("OnClick", "checkradio();");
            string returnstr = "";
            lblerrmsg.Text = "";
            if (Convert.ToString(canc.Value) != "" && Convert.ToInt32(canc.Value) > 0)
            {
                if ((Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(canc.Value)) > 0)
                {
                    ViewState["cpax"] = canc.Value;
                    returnstr = cancelpartialticket(Convert.ToString(ViewState["TicketNo"]), Convert.ToInt32(ViewState["tpax"]), Convert.ToInt32(canc.Value));
                    string lSeatRelease = optedSeatNosBook.Value.TrimStart(',').TrimEnd(',');
                    if (returnstr == "Success")
                    {
                        Response.Redirect("AgentReprocess.aspx?PaymentBy=AgentCash&refund=" + ViewState["refund"] + "&agentid=" +
                            ViewState["agentID"] + "&username=" + ViewState["UserName"] + "&branchcode=" + ViewState["BranchCode"] + "&pt=" +
                            ViewState["TotalAmount"] + "&can=" + ViewState["cancelcharges"] + "&ot=" + ViewState["TicketNo"] +
                            "&cp=" + ViewState["cpax"] + "&SeatRelease=" + Server.UrlEncode(ClsCommon.Encrypt(lSeatRelease)) + "");
                    }
                    else
                    {
                        lblerrmsg.Text = "Please fill all mandatory fields.";
                    }
                }
                else
                {
                    lblerrmsg.Text = "Please fill all mandatory fields.";
                }
            }
            else
            {
                lblerrmsg.Text = "Please select The Cancelled Passenger";
            }
        }
        #endregion
        #region "Method(s)"
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
        protected int CalcAdult()
        {
            Int64 result = NAdult * AdultFare;
            return (int)result;
        }
        protected int CalcChild()
        {
            Int64 result = NChild * ChildFare;
            return (int)result;
        }
        protected int CalcAdulttwin()
        {
            Int64 result = NAdultsTwin * Adultstwinfare;
            return (int)result;
        }
        protected int CalcAdulttriple()
        {
            Int64 result = NadultsTriple * adultstriplefare;
            return (int)result;
        }
        protected int CalcSingleadult()
        {
            Int64 result = NSingleAdult * singleadultfare;
            return (int)result;
        }
        protected int CalcChildWithOutBed()
        {
            Int64 result = NChildwithoutBed * childwithoutbedfare;
            return (int)result;
        }
        protected int CalcDormiroty()
        {
            Int64 result = Ndormitory * dormitoryfare;
            return (int)result;
        }
        protected int CalcAdultWFood()
        {
            Int64 result;
            result = AWFood * AWFoodFare;
            return (int)result;
        }
        protected int CalcChildWFood()
        {
            Int64 result;
            result = CWFood * CWFoodFare;
            return (int)result;
        }
        protected StringBuilder CancelTicketString(string TicketCode, int i)
        {
            //<!-- from here to added on mar2709-->

            #region Optimize Code
            /*DataTable dtTicketDetails1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, 
             * "select ticketcode,tourid,rowid,orderid,busserialno from OnlineToursBooking where ticketcode='" + TicketCode + "'");*/
            #endregion
            StringBuilder MailSubject = new StringBuilder();
            StringBuilder s = new StringBuilder();
            s.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() +
                "\\cancel.html")));
            string chknumber = "", bank = "", chqdate = "";
            decimal discount = 0;
            #region Optimize Code
            /*string sql = "select Td.ticketno,ba.busno,otb.calctaxvalue,otb.seatno,otb.amount,otb.AdvancePaid,td.advance,dbo.Pickuppoint_address_fn(Td.ticketno) as pickuppoint,td.reporttime,td.departtime,otb.noofadults,otb.noofadultstwin,otb.noofadultstriple,otb.singleadult,otb.noofchild,otb.childwithoutbed,otb.dormitory,tn.name,tn.age,tn.sex,dbo.initcap(tm.tourname) as tourname,tm.tourno,otb.doj,otb.dob,tt.paymentmode,tt.number,tt.bankname,isnull(tt.chqdate,'') as chequedate,td.branchcode,otb.busenvtype,otb.adultfare,otb.childfare,otb.adultstwinfare,otb.adultstriplefare,otb.childwithoutbedfare,otb.singleadultfare,otb.dormitoryfare,td.agentid,td.username,td.telno,td.reltelno,td.manual_ticketno,otb.totalamount,otb.tax,otb.CreditCardFee,otb.discount,otb.Remarks from ticketdetails td,busallot ba,onlinetoursbooking otb,ticketnames tn,tourmaster tm,Onlinetransactiontable tt where otb.orderid='" + dtTicketDetails1.Rows[0]["orderid"].ToString() + "' and otb.rowid=" + Convert.ToInt32(dtTicketDetails1.Rows[0]["rowid"]) + " and otb.ticketcode=td.ticketno and tn.ticketno=otb.ticketcode and tm.tourno=otb.tourid and tt.refno=otb.ticketcode and ba.rowid in(" + Convert.ToString(dtTicketDetails1.Rows[0]["busserialno"]) + ")";
            DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sql);*/
            #endregion
            pclsObj = new ClsAdo();
            DataSet ldsRecSet = pclsObj.fnAgentCancelTktPrint(TicketCode);
            DataTable dtTicketDetails = ldsRecSet.Tables[0];
            DataTable dtterms = null, dtCan = null, dt = null, ldtHierarchyCanChr = null, dtcan = null, dtseats = null;
            try
            {
                if (dtTicketDetails.Rows.Count > 0)
                {
                    orderid = dtTicketDetails.Rows[0]["orderid"].ToString();
                    TourNo = Convert.ToInt32(dtTicketDetails.Rows[0]["tourno"].ToString());
                    TicketNo = dtTicketDetails.Rows[0]["ticketno"].ToString();
                    BranchCode = dtTicketDetails.Rows[0]["branchcode"].ToString();
                    TourName = dtTicketDetails.Rows[0]["tourname"].ToString();
                    NAdult = Convert.ToInt32(dtTicketDetails.Rows[0]["noofadults"].ToString());
                    NAdultsTwin = Convert.ToInt32(dtTicketDetails.Rows[0]["noofadultstwin"].ToString());
                    NadultsTriple = Convert.ToInt32(dtTicketDetails.Rows[0]["noofadultstriple"].ToString());
                    NSingleAdult = Convert.ToInt32(dtTicketDetails.Rows[0]["singleadult"].ToString());
                    Ndormitory = Convert.ToInt32(dtTicketDetails.Rows[0]["dormitory"].ToString());
                    ReportTime = Convert.ToDateTime(dtTicketDetails.Rows[0]["reporttime"].ToString());
                    PickUppoint = dtTicketDetails.Rows[0]["pickuppoint"].ToString();
                    JourneyDate = Convert.ToDateTime(dtTicketDetails.Rows[0]["doj"].ToString());
                    JourneyDate = Convert.ToDateTime((JourneyDate).ToShortDateString().ToString());
                    DateTime bookingdate = Convert.ToDateTime(dtTicketDetails.Rows[0]["dob"].ToString());
                    bookingdate = Convert.ToDateTime((bookingdate).ToShortDateString().ToString());
                    DepartTime = Convert.ToDateTime(dtTicketDetails.Rows[0]["departtime"].ToString());
                    NChild = Convert.ToInt32(dtTicketDetails.Rows[0]["noofchild"].ToString());
                    NChildwithoutBed = Convert.ToInt32(dtTicketDetails.Rows[0]["childwithoutbed"].ToString());
                    Amount = Convert.ToDecimal(dtTicketDetails.Rows[0]["amount"].ToString());
                    decimal calTax = Convert.ToDecimal(dtTicketDetails.Rows[0]["Calctaxvalue"].ToString());
                    STaxValue = Convert.ToDecimal(dtTicketDetails.Rows[0]["tax"].ToString());
                    TotalAmount = Convert.ToDecimal(dtTicketDetails.Rows[0]["totalamount"].ToString());
                    decimal cctax = Convert.ToDecimal(dtTicketDetails.Rows[0]["CreditCardFee"].ToString());
                    PaymentMode = dtTicketDetails.Rows[0]["paymentmode"].ToString();
                    chknumber = Convert.ToString(dtTicketDetails.Rows[0]["number"]);
                    bank = Convert.ToString(dtTicketDetails.Rows[0]["bankname"]);
                    chqdate = Convert.ToString(dtTicketDetails.Rows[0]["chequedate"]);
                    TelNo = dtTicketDetails.Rows[0]["telno"].ToString();
                    BusNo = dtTicketDetails.Rows[0]["busno"].ToString();
                    SeatNumbers = dtTicketDetails.Rows[0]["seatno"].ToString();
                    AdultFare = Convert.ToInt32(dtTicketDetails.Rows[0]["AdultFare"]);
                    Adultstwinfare = Convert.ToInt32(dtTicketDetails.Rows[0]["adultstwinfare"]);
                    adultstriplefare = Convert.ToInt32(dtTicketDetails.Rows[0]["adultstriplefare"]);
                    singleadultfare = Convert.ToInt32(dtTicketDetails.Rows[0]["singleadultfare"]);
                    dormitoryfare = Convert.ToInt32(dtTicketDetails.Rows[0]["dormitoryfare"]);
                    ChildFare = Convert.ToInt32(dtTicketDetails.Rows[0]["ChildFare"]);
                    childwithoutbedfare = Convert.ToInt32(dtTicketDetails.Rows[0]["childwithoutbedfare"]);
                    AWFoodFare = Convert.ToInt32(dtTicketDetails.Rows[0]["AdultWithFoodFare"]);
                    CWFoodFare = Convert.ToInt32(dtTicketDetails.Rows[0]["ChildWithFoodFare"]);
                    AWFood = Convert.ToInt32(dtTicketDetails.Rows[0]["noAdultWithFood"]);
                    CWFood = Convert.ToInt32(dtTicketDetails.Rows[0]["noChildWithFood"]);

                    AdultServiceCharges = Convert.ToDecimal(dtTicketDetails.Rows[0]["AdultServiceCharges"].ToString());
                    ChildServiceCharges = decimal.Round(Convert.ToDecimal(dtTicketDetails.Rows[0]["ChildServiceCharges"]));
                    ServiceChargesTotal = decimal.Round(Convert.ToDecimal(dtTicketDetails.Rows[0]["ServiceChargesTotal"]));
                    ServiceChargesTax = Convert.ToDecimal(dtTicketDetails.Rows[0]["ServiceChargesTax"]);
                    ServiceChargesTaxVal = decimal.Round(Convert.ToDecimal(dtTicketDetails.Rows[0]["ServiceChargesTaxVal"]));

                    string agentid = dtTicketDetails.Rows[0]["agentid"].ToString();
                    string agentname = dtTicketDetails.Rows[0]["username"].ToString();
                    string rmarks = Convert.ToString(dtTicketDetails.Rows[0]["Remarks"]);
                    decimal advancePaid = 0;
                    if ((Convert.ToString(dtTicketDetails.Rows[0]["discount"]) != "") && (Convert.ToString(dtTicketDetails.Rows[0]["discount"]) != null))
                    {
                        discount = Convert.ToDecimal(dtTicketDetails.Rows[0]["discount"]);
                    }
                    else
                    {
                        discount = 0;
                    }
                    if ((Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != null)
                        && (Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != "") && (Convert.ToDecimal(dtTicketDetails.Rows[0]["AdvancePaid"]) != 0))
                    {
                        advancePaid = Convert.ToDecimal(dtTicketDetails.Rows[0]["AdvancePaid"].ToString());
                    }
                    else
                    {
                        if ((BranchCode == "EBK0001") || (Convert.ToInt32(agentid) > 0)) advancePaid = TotalAmount; else advancePaid = 0;
                    }

                    StringBuilder TicketStr = new StringBuilder();
                    TicketStr.Append("<Table id=Table2 cellSpacing=0 cellPadding=0 width=100% border=0>");
                    TicketStr.Append("<TR>");
                    TicketStr.Append("<TD>");

                    StringBuilder TicketStrAgent = new StringBuilder();
                    TicketStrAgent.Append("<TABLE id=Table2 cellSpacing=0 cellPadding=2 width=100% border=0>");

                    TicketStrAgent.Append("<TR><td colspan=2><table width=100% border=1 cellpadding=0 cellspacing=0 bgcolor=#CCCCCC> <tr> <td><table width=100% border=0 cellpadding=10 cellspacing=0 bgcolor=#FFFFFF><tr>");
                    string ImgUrl = ConfigurationSettings.AppSettings["LogoUrl"].ToString();
                    string saveName = "Logo\\" + Session["UserId"].ToString();
                    string exist = "N";
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".jpg"))) { saveName = saveName + ".jpg"; exist = "Y"; }
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".gif"))) { saveName = saveName + ".gif"; exist = "Y"; }
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".png"))) { saveName = saveName + ".png"; exist = "Y"; }
                    string address1 = Globals.AgentAddress;
                    string ph = Globals.AgentPhone;
                    if (exist == "Y")
                    {
                        TicketStrAgent.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><img src='" + saveName + "' width=\"157\" height=\"69\" /><br />" + address1 + "," + ph + "</font></td>");
                    }
                    else
                    {
                        string AgentName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                        TicketStrAgent.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><string>'" + AgentName + "'</string><br/>" + address1 + "," + ph + "</font></td>");
                    }
                    TicketStrAgent.Append("<td width=\"31%\" valign=\"top\"><img src=\"" + ImgUrl + "/Assets/images/STPL_Preferred_Sales_Partner.gif\" width=\"231\" height=\"64\" /></td>");
                    TicketStrAgent.Append("</TR></table></td></tr></table></td></tr>");
                    TicketStrAgent.Append("<TR>");
                    TicketStrAgent.Append("<TD class=style5 align=center height=7></TD>");
                    TicketStrAgent.Append("<TD class=style5 align=center height=7></TD>");
                    TicketStrAgent.Append("</TR>");



                    StringBuilder lCostingHTML = new StringBuilder();
                    int pATotalPax = 0, pCTotalPax = 0;
                    if (NAdult > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Adults", NAdult, AdultFare, CalcAdult()));
                        pATotalPax = pATotalPax + NAdult;
                    }
                    if (NChild > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Children", NChild, ChildFare, CalcChild()));
                        pCTotalPax = pCTotalPax + NChild;
                    }
                    if (Ndormitory > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Dormitory Accommodation", Ndormitory, dormitoryfare, CalcDormiroty()));
                        pATotalPax = pATotalPax + NAdult;
                    }
                    if (NAdultsTwin > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Twin Sharing", NAdultsTwin, Adultstwinfare, CalcAdulttwin()));
                        pATotalPax = pATotalPax + NAdultsTwin;
                    }
                    if (NadultsTriple > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Triple Sharing", NadultsTriple,
                            adultstriplefare, CalcAdulttriple()));
                        pATotalPax = pATotalPax + NadultsTriple;
                    }
                    if (NSingleAdult > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Single Sharing", NSingleAdult,
                            singleadultfare, CalcSingleadult()));
                        pATotalPax = pATotalPax + NSingleAdult;
                    }
                    if (AWFood > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Adult With South Veg Food", AWFood, AWFoodFare, CalcAdultWFood()));
                        pATotalPax = pATotalPax + AWFood;
                    }
                    if (CWFood > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Child With South Veg Food", CWFood, CWFoodFare, CalcChildWFood()));
                        pCTotalPax = pCTotalPax + CWFood;
                    }
                    if (NChildwithoutBed > 0)
                    {
                        lCostingHTML.Append(ClsCommon.GetCostingRow("Child Without Bed", NChildwithoutBed,
                            childwithoutbedfare, CalcChildWithOutBed()));
                        pCTotalPax = pCTotalPax + NChildwithoutBed;
                    }
                    #region Optimize Code
                    //if (lCostingHTML.ToString().Trim() != string.Empty)
                    //{
                    //    string lCosting = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" + lCostingHTML.ToString() +
                    //        "</table>";
                    //    lCostingHTML = new StringBuilder();
                    //    lCostingHTML.Append(lCosting);
                    //}
                    /* StringBuilder Strname = new StringBuilder();
                     Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100% border=1>");
                     Strname.Append("<TR><TD class=cgi align=center>Passenger Names</TD><TD class=cgi align=center width=50px>Age</TD><TD class=cgi align=center width=50px>Sex</TD><TD class=cgi align=center width=50px>Bus No</TD><TD class=cgi align=center>Seat</TD></TR>");

                      dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select * from canname where canno=(select top 1 canno from cancel where ticketno='" + TicketNo + "')");            
                     string ticketbusno = "select busno,seatnumbers from ticketbusno where ticketno='" + TicketNo + "' order by busno";

                     DataTable dtseats = DataLib.GetDataTable(DataLib.Connection.ConnectionString, ticketbusno);*/





                    /*StringBuilder lPassenger = new StringBuilder();
                    lPassenger.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"1\">");
                    lPassenger.Append("<TR><TD class=\"tdMailerBorderRB\" align=\"left\"><span class=\"cgi\">&nbsp; Passenger Names</span></TD>");
                    lPassenger.Append("<TD class=\"tdMailerBorderRB\" align=\"center\" width=\"50px\"><span class=\"cgi\">Age</span></TD>");
                    lPassenger.Append("<TD class=\"tdMailerBorderRB\" align=\"center\" width=\"50px\"><span class=\"cgi\">Sex</span></TD>");
                    lPassenger.Append("<TD class=\"tdMailerBorderRB\" align=\"center\" width=\"50px\"><span class=\"cgi\">Bus No</span></TD>");
                    lPassenger.Append("<TD class=\"tdMailerBorderB\" align=\"center\"><span class=\"cgi\">Seat</span></TD></TR>");*/
                    #endregion
                    StringBuilder Strname = new StringBuilder();
                    Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100%>");
                    Strname.Append("<TR style=\"border-style:solid; border-width:5px; border-color:Black;\">");
                    Strname.Append("<TD class=cgi align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-top-style:none;\">Passenger Names</TD>");
                    Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-top-style:none;\">Age</TD>");
                    Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-top-style:none;\">Sex</TD>");
                    Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-top-style:none;\">Bus No</TD>");
                    Strname.Append("<TD class=cgi align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-right-style:none; border-top-style:none;\">Seat</TD>");
                    Strname.Append("</TR>");

                    dtTicketDetails = new DataTable();
                    dtTicketDetails = ldsRecSet.Tables[1];
                    dtseats = ldsRecSet.Tables[2];

                    string seat11 = "", seat1 = "", busno11 = "";
                    for (int j = 0; j < dtseats.Rows.Count; j++)
                    {
                        seat11 = Convert.ToString(dtseats.Rows[j]["seatnumbers"]);
                        busno11 = Convert.ToString(dtseats.Rows[j]["busno"]);
                        string[] aa = seat11.Split(',');
                        for (int k = 0; k < aa.Length; k++)
                        {
                            if (seat1 == "")
                            {
                                seat1 = busno11 + "-" + aa[k];
                            }
                            else
                            {
                                seat1 = seat1 + "@" + busno11 + "-" + aa[k];
                            }
                        }
                    }
                    string[] busseat = seat1.Split('@');
                    for (int m = 0; m < dtTicketDetails.Rows.Count; m++)
                    {
                        Name = dtTicketDetails.Rows[m]["Name"].ToString();
                        Age = Convert.ToInt32(dtTicketDetails.Rows[m]["Age"].ToString());
                        Sex = dtTicketDetails.Rows[m]["Sex"].ToString();
                        string[] bno = busseat[m].Split('-');
                        string aa = bno[0];
                        string bb = bno[1], lTopBottomBorder = "";
                        lTopBottomBorder = " border-top-style:none;";
                        if (m == (dtTicketDetails.Rows.Count - 1))
                        {
                            lTopBottomBorder += " border-bottom-style:none;";
                        }
                        Strname.Append("<TR style=\"border-style:solid; border-width:5px; border-color:Black;\">");
                        Strname.Append("<TD class=hlinks style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\">&nbsp; " +
                            dtTicketDetails.Rows[m]["Name"].ToString() + " </TD>");
                        Strname.Append("<TD class=hlinks width=15px align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " +
                            Convert.ToInt32(dtTicketDetails.Rows[m]["Age"].ToString()) + " </TD>");
                        Strname.Append("<TD class=hlinks width=15px align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " +
                            dtTicketDetails.Rows[m]["Sex"].ToString() + " </TD>");
                        Strname.Append("<TD class=hlinks width=50px align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " + aa + " </TD>");
                        Strname.Append("<TD class=hlinks align=center style=\"border-style:solid; border-width:1px; " +
                        "border-color:Black; border-left-style:none; border-right-style:none;" + lTopBottomBorder + "\"> " +
                        bb.Replace("s", "") + " </TD>");
                        Strname.Append("</TR>");
                    }
                    Strname.Append("</TABLE>");
                    decimal amt1 = (Amount * (STaxValue / 100));
                    decimal amt2 = ((amt1 + Amount) * (cctax / 100));
                    decimal amt3 = amt1 + amt2;
                    #region Optimize Code
                    /*string can = "select a.cancharges,a.refundamt,a.username,a.BranchCode,a.canno,b.transactiondate from cancel a,onlinetransactiontable b where a.ticketno='" + TicketNo + "' and a.canno=b.refno";
                DataTable dtcan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, can);*/
                    #endregion
                    dtcan = ldsRecSet.Tables[3];

                    s = s.Replace("#pnr", orderid);
                    s = s.Replace("#bookDate", Convert.ToString(bookingdate.ToString("dd-MMM-yyyy")));
                    s = s.Replace("#pickName", PickUppoint);
                    s = s.Replace("#ticket", TicketNo);
                    s = s.Replace("#jdate", Convert.ToString(JourneyDate.ToString("dd-MMM-yyyy")));
                    s = s.Replace("#tourName", TourName);
                    //s = s.Replace("#reprtingTime", Convert.ToString(ReportTime.ToShortTimeString()));
                    s = s.Replace("#reprtingTime", Convert.ToString(DepartTime.AddMinutes(-15).ToShortTimeString()));
                    s = s.Replace("#departTime", Convert.ToString(DepartTime.ToShortTimeString()));
                    s = s.Replace("#Costing", Convert.ToString(lCostingHTML));
                    s = s.Replace("#passengerslist", Convert.ToString(Strname));
                    s = s.Replace("#Agent", Convert.ToString(TicketStrAgent));
                    s = s.Replace("#Amount", Convert.ToString(decimal.Round(Amount + decimal.Round(discount))));

                    DateTime canceldate = Convert.ToDateTime(dtcan.Rows[0]["transactiondate"]);
                    s = s.Replace("#cancelno", Convert.ToString(dtcan.Rows[0]["canno"]));
                    s = s.Replace("#canceldate", Convert.ToString(canceldate.ToString("dd-MMM-yyyy hh:mmtt")));
                    try
                    {
                        if (Session["BranchId"] != null)
                        {
                            pclsObj = new ClsAdo();
                            string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            int Val = pclsObj.fnSaveBranchToAgentBooking(Convert.ToString(dtcan.Rows[0]["canno"]), Convert.ToInt32(Session["BranchUserId"]),
                                Convert.ToInt32(Session["AgentId"]), REMOTE_ADDR);
                        }
                    }
                    catch { }
                    if (BranchCode == "EBK0001")
                    {
                        s = s.Replace("#lblDiscount", "");
                        s = s.Replace("#discount", "");
                    }
                    else
                    {
                        if ((discount) > 0)
                        {
                            s = s.Replace("#lblDiscount", "Discount: ");
                            s = s.Replace("#discount", Convert.ToString(decimal.Round(discount)));
                        }
                        else
                        {
                            s = s.Replace("#lblDiscount", "");
                            s = s.Replace("#discount", "");
                            //s = s.Replace("#discount", "Nil");
                        }
                    }
                    if (Convert.ToDecimal(amt3) <= 0)
                    {
                        s = s.Replace("#lblServiceTax", "");
                        s = s.Replace("#st", "");
                    }
                    if ((agentid == "0") && (BranchCode == "EBK0001"))
                    {
                        s = s.Replace("#lblServiceTax", "Tax + Service fee: ");
                        s = s.Replace("#st", Convert.ToString(decimal.Round(amt3)));
                        s = s.Replace("#BookingExecutive", "");
                        s = s.Replace("#branchUser", "");
                        s = s.Replace("#cancellingExecutive", "");
                        s = s.Replace("#cancelbranchUser", "");
                    }
                    else
                    {
                        s = s.Replace("#lblServiceTax", "GST : ");
                        s = s.Replace("#st", Convert.ToString(decimal.Round(amt3)));
                        s = s.Replace("#BookingExecutive", "Booking Executive: ");
                        s = s.Replace("#branchUser", agentname);
                        s = s.Replace("#cancellingExecutive", "Cancelled Executive: ");
                        s = s.Replace("#cancelbranchUser", Convert.ToString(dtcan.Rows[0]["Username"]));
                    }

                    StringBuilder pServiceCharges = new StringBuilder();

                    pServiceCharges.Append("<tr>");
                    pServiceCharges.Append("<td colspan=\"3\">");
                    pServiceCharges.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                    pServiceCharges.Append("<tr>");
                    pServiceCharges.Append("<td width=\"25%\" ><b>Service Charge</b></br>(Pickup Point) :</td>");
                    //pServiceCharges.Append("</tr>");
                    //pServiceCharges.Append("<tr>");
                    //pServiceCharges.Append("<td width=\"10%\">&nbsp;");
                    //pServiceCharges.Append("</td>");
                    pServiceCharges.Append("<td width=\"30%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                    pServiceCharges.Append("#ServiceChargesAmount");
                    pServiceCharges.Append("</td>");
                    pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                    pServiceCharges.Append("<span class=\"cgi\">&nbsp;Amount :</span><span class=\"hlinks\"> #SAmount </span>");
                    pServiceCharges.Append("</td>");
                    pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                    pServiceCharges.Append("<span class=\"cgi\">&nbsp;GST :</span><span class=\"hlinks\"> #ServiceChargesTax </span>");
                    pServiceCharges.Append("</td>");
                    pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                    pServiceCharges.Append("<span class=\"cgi\">&nbsp;Total  :</span><span class=\"hlinks\"> #ServiceChargesTotal </span>");
                    pServiceCharges.Append("</td>");
                    pServiceCharges.Append("</tr>");
                    pServiceCharges.Append("</table>");
                    pServiceCharges.Append("</td>");
                    pServiceCharges.Append("</tr>");


                    if (AdultServiceCharges > 0)
                    {
                        StringBuilder strCal = new StringBuilder();
                        strCal.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");

                        strCal.Append("<tr>");
                        strCal.Append("<td>");
                        strCal.Append("<span class=\"cgi\">Adult</span>");
                        strCal.Append("</td>");
                        strCal.Append("<td>");
                        strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(AdultServiceCharges)));
                        strCal.Append("</td>");
                        strCal.Append("<td>");
                        strCal.Append("<span class=\"cgi\">" + "X" + "</span>");
                        strCal.Append("</td>");
                        strCal.Append("<td>");
                        strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(pATotalPax)) + "</span><span class=\"cgi\"> =</span>");
                        strCal.Append("</td>");
                        strCal.Append("<td>");
                        strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(AdultServiceCharges) * decimal.Round(pATotalPax)) + "</span>");
                        strCal.Append("</td>");
                        strCal.Append("</tr>");
                        if (pCTotalPax > 0)
                        {
                            strCal.Append("<tr>");
                            strCal.Append("<td>");
                            strCal.Append("<span class=\"cgi\">Child</span>");
                            strCal.Append("</td>");
                            strCal.Append("<td>");
                            strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(ChildServiceCharges)));
                            strCal.Append("</td>");
                            strCal.Append("<td>");
                            strCal.Append("<span class=\"cgi\">" + "X" + "</span>");
                            strCal.Append("</td>");
                            strCal.Append("<td>");
                            strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(pCTotalPax)) + "</span><span class=\"cgi\"> =</span>");
                            strCal.Append("</td>");
                            strCal.Append("<td>");
                            strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(ChildServiceCharges) * decimal.Round(pCTotalPax)) + "</span>");
                            strCal.Append("</td>");
                            strCal.Append("</tr>");
                        }
                        strCal.Append("</table>");

                        pServiceCharges = pServiceCharges.Replace("#ServiceChargesAmount", strCal.ToString());
                        pServiceCharges = pServiceCharges.Replace("#SAmount", Convert.ToString(decimal.Round(ServiceChargesTotal) - decimal.Round(ServiceChargesTaxVal)));

                        pServiceCharges = pServiceCharges.Replace("#ServiceChargesTax", Convert.ToString(decimal.Round(ServiceChargesTaxVal)));
                        pServiceCharges = pServiceCharges.Replace("#ServiceChargesTotal", Convert.ToString(decimal.Round(ServiceChargesTotal)));

                        s = s.Replace("#ServiceCharges", pServiceCharges.ToString());

                    }
                    else
                    {
                        s = s.Replace("#ServiceCharges", "");
                    }

                    decimal st = decimal.Round((Convert.ToDecimal(Amount) * (Convert.ToDecimal(STaxValue) / 100)));
                    ViewState["TotalAmount"] = Convert.ToString(Amount + st);
                    s = s.Replace("#totalAmt", Convert.ToString(decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) + ServiceChargesTotal)));
                    //s = s.Replace("#totalAmt", Convert.ToString(decimal.Round(TotalAmount)));
                    if ((advancePaid) > 0)
                    {


                        s = s.Replace("#advPaid", Convert.ToString(advancePaid));
                        //s = s.Replace("#advPaid", Convert.ToString(decimal.Round(advancePaid)));

                    }
                    else
                    {
                        s = s.Replace("#advPaid", "Nil");
                    }
                    string strCanPrev = "";
                    decimal pTotCanCharges = 0;
                    if (dtcan.Rows.Count > 0)
                    {
                        int? lHierarchyCounter = 0;
                        ldtHierarchyCanChr = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(TicketNo),
                              ref lHierarchyCounter);

                        if (ldtHierarchyCanChr.Rows.Count > 0)
                        {
                            /*if (decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - Convert.ToDecimal(ViewState["TotalAmount"])) >= 0)
                            {
                                pTotCanCharges = Convert.ToDecimal(ldtHierarchyCanChr.Rows[0]["TktCanCharge"]);
                                s.Replace("#PCanCh", pTotCanCharges.ToString());
                            }
                            else
                            {*/
                            pTotCanCharges = decimal.Round(Convert.ToDecimal(ldtHierarchyCanChr.Compute("Sum(TktCanCharge)", "RowID >=0").ToString()));
                            bool flag = false;
                            for (int jk = 0; jk < ldtHierarchyCanChr.Rows.Count; jk++)
                            {
                                if (flag == false)
                                {
                                    flag = true;
                                    strCanPrev = ldtHierarchyCanChr.Rows[jk]["TktCanCharge"].ToString();
                                }
                                else
                                {
                                    strCanPrev = strCanPrev + "+" + ldtHierarchyCanChr.Rows[jk]["TktCanCharge"].ToString();
                                }
                            }
                            if (strCanPrev.Contains("+"))
                            {
                                s.Replace("#PCanCh", strCanPrev + " = " + pTotCanCharges.ToString());
                            }
                            else
                            {
                                s.Replace("#PCanCh", pTotCanCharges.ToString());
                            }
                            //}

                        }
                        else
                        {
                            s.Replace("#PCanCh", "Nill");
                        }
                    }
                    else
                    {
                        s.Replace("#PCanCh", "Nill");
                    }


                    if (PaymentMode.Trim().Length < 3)
                    {
                        #region Optimize Code
                        /*string ss = "select Paymentmode from tbl_paymodes where rowid='" + PaymentMode + "'";
                    string pm = DataLib.GetStringData(DataLib.Connection.ConnectionString, ss);*/
                        #endregion
                        string pm = ldsRecSet.Tables[4].Rows[0][0].ToString();
                        s = s.Replace("#PaymentMode", "PaymentMode :");
                        string details = "";
                        if (chknumber != "")
                            details = details + "<br><span class=cgi>Trans No: </span>" + chknumber;
                        if (bank != "")
                            details = details + "<br><span class=cgi>Bank: </span>" + bank;
                        if (chqdate != "")
                            details = details + "<br><span class=cgi>Date: </span>" + chqdate;

                        s = s.Replace("#Mode", pm + details);
                    }
                    else
                    {
                        s = s.Replace("#PaymentMode", "PaymentMode :");
                        s = s.Replace("#Mode", PaymentMode);
                    }
                    #region Optimize Code
                    //string gpldr = groupleader(orderid);
                    #endregion
                    dt = ldsRecSet.Tables[5];

                    string gpldr = dt.Rows[0]["Name1"].ToString();
                    s = s.Replace("#custContact", gpldr + " / " + TelNo);

                    if ((Convert.ToInt32(agentid) > 0))
                    {
                        s = s.Replace("#BookingOffice", "Booking Agent :");
                        s = s.Replace("#branchCode", BranchCode);
                        //s = s.Replace("#BookingOffice", "Booking Agent :");
                        s = s.Replace("#cancelbranchCode", Convert.ToString(dtcan.Rows[0]["BranchCode"]));
                    }
                    else if ((BranchCode != "EBK0001") && (agentid == "0"))
                    {
                        s = s.Replace("#BookingOffice", "Booking Branch :");
                        s = s.Replace("#branchCode", BranchCode);
                        s = s.Replace("#cancelbranchCode", Convert.ToString(dtcan.Rows[0]["BranchCode"]));
                    }
                    else
                    {
                        s = s.Replace("#BookingOffice", "Booking from :");
                        s = s.Replace("#branchCode", "Online Booking");
                        s = s.Replace("#cancelbranchCode", Convert.ToString(dtcan.Rows[0]["BranchCode"]));
                    }
                    decimal refundamt = 0;
                    if (dtcan.Rows.Count > 0)
                    {
                        s = s.Replace("#CancellationCharges", Convert.ToString(dtcan.Rows[0]["cancharges"]));
                        /*refundamt = Convert.ToDecimal(dtcan.Rows[0]["refundamt"]);
                        int? lHierarchyCount = 0;
                        DataTable ldtHierarchy = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(TicketNo),
                            ref lHierarchyCount);
                        if (lHierarchyCount > 1)
                        {
                            refundamt -= Convert.ToDecimal(ldtHierarchy.Rows[0]["TktCanCharge"]);
                            if (refundamt > 0)
                            {
                                s = s.Replace("#refundamount", Convert.ToString(decimal.Round(refundamt)));
                            }
                            else
                            {
                                s = s.Replace("#refundamount", "Nill");
                            }
                        }
                        else
                        {
                            s = s.Replace("#refund", Convert.ToString(decimal.Round(refundamt)));
                        }*/
                        refundamt = Convert.ToDecimal(dtcan.Rows[0]["refundamt"]);
                        s = s.Replace("#refundamount", Convert.ToString(dtcan.Rows[0]["refundamt"]));
                    }
                    if (refundamt > 0)
                    {

                        s = s.Replace("#balDue", "Nil");
                    }
                    else
                    {
                        s = s.Replace("#balDue", Convert.ToString(decimal.Round(TotalAmount - advancePaid /*- pTotCanCharges*/)));
                    }
                    //s = s.Replace("#branchUser", agentname);
                    s.Replace("#GenerationTime", DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                    MailSubject.Append(s);
                    if (rmarks != "")
                    {
                        //MailSubject.Append("<table width=100% border=0>");
                        //MailSubject.Append("<TR>");
                        //MailSubject.Append("<TD><span class=cgi>Remarks :</span><span class=hlinks>" + rmarks + "</span></TD>");
                        //MailSubject.Append("</TR></table>");
                        StringBuilder RemarksText = new StringBuilder();
                        RemarksText.Append("<span class=hlinks><span class=cgi>Remarks: </span><span>" + rmarks.ToString() + "</span>");

                        MailSubject.Replace("#ShowRemarksText", "<tr><td colspan=\"3\">" + RemarksText.ToString() + "</td></tr>");
                    }
                    else
                    {
                        MailSubject.Replace("#ShowRemarksText", "");

                    }



                    #region Optimize Code
                    /*string strterms = "SELECT FixedTermsCondition FROM CompanyDetailsForRpt";
                DataTable dtterms = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strterms);*/

                    /* DataTable dtterms = ldsRecSet.Tables[6];
                     if (dtterms.Rows.Count > 0)
                     {
                         MailSubject = MailSubject.Replace("#TermsNConditions", dtterms.Rows[0]["FixedTermsCondition"].ToString());
                         //MailSubject.Append(dtterms.Rows[0][0].ToString());
                     }*/

                    /*string strCancel = "select distinct tourno, noofdays,cast(percentage as int) as percentage from  CancelMaster(nolock) where tourno=" + TourNo + " order by noofdays desc";
                    DataTable dtCan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strCancel);*/
                    #endregion
                    dtCan = ldsRecSet.Tables[7];

                    //if (dtCan.Rows.Count > 0)
                    //{
                    //    MailSubject.Append("<p class=\"hlinks\">&nbsp;");
                    //    MailSubject.Append("<font face=\"Arial\" color=\"red\"><b>Cancellation Charges For " + TourName + " :</b></font><br />");
                    //    for (int jj = 0; jj < dtCan.Rows.Count; jj++)
                    //    {
                    //        MailSubject.Append("<li></li>Before " + dtCan.Rows[jj]["noofdays"] + " Hours charges are " + dtCan.Rows[jj]["percentage"] + "% ");
                    //    }
                    //}
                    StringBuilder BannerText = new StringBuilder();


                    BannerText.Append(ClsCommon.GetTourTicketBanner(TourNo, "FT", "Agent"));
                    MailSubject = MailSubject.Replace("#ShowBanner", BannerText.ToString());

                    //Term Condition
                    dtterms = ldsRecSet.Tables[6];
                    StringBuilder TermConditionText = new StringBuilder();

                    TermConditionText.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() +
                   "\\TermCondition.htm")));
                    if (dtterms.Rows.Count > 0)
                    {
                        TermConditionText = TermConditionText.Replace("#Terms&Condition", dtterms.Rows[0]["FixedTermsCondition"].ToString());
                    }
                    else
                    {
                        TermConditionText = TermConditionText.Replace("#Terms&Condition", string.Empty);
                    }
                    if (dtCan.Rows.Count > 0)
                    {
                        StringBuilder lCanChargeNote = new StringBuilder();
                        lCanChargeNote.Append("<p class=\"hlinks\">&nbsp;");
                        lCanChargeNote.Append("<span style=\"font-size: 7.5pt\"><font face=\"Arial\" color=\"red\"><b>Cancellation Charges For " +
                            TourName + " :</b></font>");
                        for (int jj = 0; jj < dtCan.Rows.Count; jj++)
                        {
                            lCanChargeNote.Append("<li style=\"margin: 0in 0in 0pt 0.5in;\">Before " + dtCan.Rows[jj]["noofdays"] + " Hours charges are " +
                                dtCan.Rows[jj]["percentage"] + "% </li>");
                        }
                        lCanChargeNote.Append("</span></p>");
                        TermConditionText = TermConditionText.Replace("#CancelChargeNote", lCanChargeNote.ToString());
                    }
                    else
                    {
                        TermConditionText = TermConditionText.Replace("#CancelChargeNote", "");
                    }
                    MailSubject = MailSubject.Replace("#TermsNConditions", TermConditionText.ToString());
                    MailSubject.Append("	</td>");
                    MailSubject.Append("	</tr>");
                    MailSubject.Append("	</table>");

                    StringBuilder TKTDetail = new StringBuilder();
                    TKTDetail.Append("<table width=100%>");
                    TKTDetail.Append("<tr>");
                    TKTDetail.Append("<td width=40% align=right>");
                    TKTDetail.Append("<INPUT class=intdtxth id=Button1 onclick=\"framePrint('print_area" + i + "');\"  type=button value=Print name=Button1>");


                    TKTDetail.Append("</TD>");
                    TKTDetail.Append("</TR>");
                    TKTDetail.Append("</Table>");

                    //===============================================//
                    TKTDetail.Append("<DIV id=print_area" + i + ">");
                    TicketStr.Append(TKTDetail);
                    TicketStr.Append(MailSubject);
                    TicketStr.Append("</TD>");
                    TicketStr.Append("</TR>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td height=5 colspan=3>");
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td colspan=3 class=hlinks>");
                    TicketStr.Append("<View our Terms and conditions");
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");
                    TicketStr.Append("</Table>");

                    #region Optimize Code
                    /*string Toemail = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select email from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid='" + Convert.ToString(ViewState["TransNumber"]) + "'");*/
                    #endregion
                    string Toemail = ldsRecSet.Tables[8].Rows[0][0].ToString();
                    if (Toemail == Convert.ToString(ViewState["TransNumber"]) + "@temp.com")
                    {

                        Toemail = "";
                    }
                    else if (Toemail.Length > 23)
                    {
                        string k = Toemail.Substring(19, 4);
                        if (k == "temp")
                            Toemail = "";

                    }
                    try
                    {
                        //MailMessage sendMail = new MailMessage();
                        //sendMail.To = Toemail;
                        //string ticketmail = ConfigurationSettings.AppSettings["cancelticketemail"].ToString();
                        //sendMail.Bcc = ticketmail;
                        //sendMail.From = "etickets@southerntravels.in";
                        //sendMail.Body = MailSubject.ToString();
                        //sendMail.BodyFormat = MailFormat.Html;
                        //sendMail.Subject = "Cancelled Ticket - Southern Travels";
                        //SmtpMail.Send(sendMail);

                        ClsCommon.sendmail(Toemail, ConfigurationSettings.AppSettings["cancelticketemail"].ToString(), "", ConfigurationSettings.AppSettings["eTicketEmail"].ToString(), "Cancelled Ticket - Southern Travels", MailSubject.ToString(), "");



                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {

                    }
                    //End-For Send the Mail



                    if (PartialCancellation.Checked == true)
                    {
                        hideTables();
                        Remainpax.Visible = false;
                        listpax.Visible = false;
                        trSeat.Visible = false;
                        btnPartialTicket.Visible = false;
                    }
                    else
                    {
                        hideTables();
                        Remainpax.Visible = false;
                        listpax.Visible = false;
                        trSeat.Visible = false;
                        btnPartialTicket.Visible = false;
                        tblRuleRegulation.Visible = true;
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
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtTicketDetails != null)
                {
                    dtTicketDetails.Dispose();
                    dtTicketDetails = null;
                }
                if (dtterms != null)
                {
                    dtterms.Dispose();
                    dtterms = null;
                }
                if (dtCan != null)
                {
                    dtCan.Dispose();
                    dtCan = null;
                }
                if (ldtHierarchyCanChr != null)
                {
                    ldtHierarchyCanChr.Dispose();
                    ldtHierarchyCanChr = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (dtcan != null)
                {
                    dtcan.Dispose();
                    dtcan = null;
                }
                if (dtseats != null)
                {
                    dtseats.Dispose();
                    dtseats = null;
                }
            }
        }
        /// <summary>
        /// Not use
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        protected string groupleader(string orderid)
        {
            string grp = "";
            try
            {
                grp = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select FirstName + ' ' + isnull(LastName,'') as Name1 from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid='" + orderid + "'");
            }
            catch
            {
            }
            finally
            {
                if (grp == "")
                    grp = "";
            }
            return grp;
        }
        protected StringBuilder TicketString(string TicketCode, int i, char n)
        {
            #region Optimize Code
            /*string str1 = "br_can_ticketstring";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ticketno", TicketCode);
            DataSet dts = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, str1, param);*/
            #endregion
            pclsObj = new ClsAdo();
            DataSet ldsRecSet = pclsObj.fnAgent_can_ticketstring(TicketCode);
            DataTable dtTour = pclsObj.fnbr_can_ticketstring(TicketCode);
            DataTable ldtHierarchyCanChr = null, dtchkcancel = null, dtseats = null;
            string ac = "";
            try
            {
                if (dtTour.Rows.Count > 0)
                {
                    if (Convert.ToString(dtTour.Rows[0]["Cancelled"]) != "Y")
                    {
                        TourNo = Convert.ToInt32(dtTour.Rows[0]["TourNo"]);
                        TicketNo = Convert.ToString(dtTour.Rows[0]["TicketNo"]);
                        BranchCode = Convert.ToString(dtTour.Rows[0]["BranchCode"]);
                        UserName = Convert.ToString(dtTour.Rows[0]["UserName"]);
                        TourName = Convert.ToString(dtTour.Rows[0]["TourName"]);
                        NAdult = Convert.ToInt32(dtTour.Rows[0]["NAdult"]);
                        NAdultsTwin = Convert.ToInt32(dtTour.Rows[0]["noofadultstwin"]);
                        NadultsTriple = Convert.ToInt32(dtTour.Rows[0]["noofadultstriple"]);
                        NSingleAdult = Convert.ToInt32(dtTour.Rows[0]["singleadult"]);
                        Ndormitory = Convert.ToInt32(dtTour.Rows[0]["dormitory"]);

                        ReportTime = Convert.ToDateTime(dtTour.Rows[0]["ReportTime"]);
                        PickUppoint = Convert.ToString(dtTour.Rows[0]["PickUppoint"]);
                        JourneyDate = Convert.ToDateTime(dtTour.Rows[0]["JourneyDate"]);
                        DepartTime = Convert.ToDateTime(dtTour.Rows[0]["DepartTime"]);
                        NChild = Convert.ToInt32(dtTour.Rows[0]["NChild"]);
                        NChildwithoutBed = Convert.ToInt32(dtTour.Rows[0]["childwithoutbed"]);
                        Amount = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["Amount"]));
                        STaxValue = Convert.ToDecimal(dtTour.Rows[0]["STaxValue"]);
                        TotalAmount = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["TotalAmount"]));
                        PaymentMode = Convert.ToString(dtTour.Rows[0]["PaymentMode"]);
                        TelNo = Convert.ToString(dtTour.Rows[0]["TelNo"]).ToString();
                        BusNo = Convert.ToInt32(dtTour.Rows[0]["BusNo"]).ToString();
                        SeatNumbers = Convert.ToString(dtTour.Rows[0]["SeatNumbers"]).ToString();
                        AdultFare = Convert.ToInt32(dtTour.Rows[0]["AdultFare"]);
                        Adultstwinfare = Convert.ToInt32(dtTour.Rows[0]["adultstwinfare"]);
                        adultstriplefare = Convert.ToInt32(dtTour.Rows[0]["adultstriplefare"]);
                        singleadultfare = Convert.ToInt32(dtTour.Rows[0]["singleadultfare"]);
                        ChildFare = Convert.ToInt32(dtTour.Rows[0]["ChildFare"]);
                        childwithoutbedfare = Convert.ToInt32(dtTour.Rows[0]["childwithoutbedfare"]);
                        dormitoryfare = Convert.ToInt32(dtTour.Rows[0]["dormitoryfare"]);

                        AdultServiceCharges = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["AdultServiceCharges"]));
                        ChildServiceCharges = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["ChildServiceCharges"]));
                        ServiceChargesTotal = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["ServiceChargesTotal"]));
                        ServiceChargesTax = Convert.ToDecimal(dtTour.Rows[0]["ServiceChargesTax"]);
                        ServiceChargesTaxVal = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["ServiceChargesTaxVal"]));

                        ViewState["AdultServiceCharges"] = AdultServiceCharges;
                        ViewState["ChildServiceCharges"] = ChildServiceCharges;
                        ViewState["ServiceChargesTotal"] = ServiceChargesTotal;
                        ViewState["ServiceChargesTax"] = ServiceChargesTax;
                        ViewState["ServiceChargesTaxVal"] = ServiceChargesTaxVal;


                        agentID = Convert.ToInt32(dtTour.Rows[0]["AgentId"]);
                        AgentCredit = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["AgentCredit"]));
                        AgentDebit = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["AgentDebit"]));
                        cctaxvalue = Convert.ToDecimal(dtTour.Rows[0]["creditcardfee"]);
                        TDS = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["TDS"]));
                        ac = Convert.ToString(dtTour.Rows[0]["AC"]);
                        ViewState["Amount"] = Convert.ToString(Amount);
                        ViewState["cctaxvalue"] = "0";
                        ViewState["agentID"] = Convert.ToString(agentID);
                        ViewState["BranchCode"] = Convert.ToString(BranchCode);
                        AWFoodFare = Convert.ToInt32(dtTour.Rows[0]["AdultWithFoodFare"]);
                        CWFoodFare = Convert.ToInt32(dtTour.Rows[0]["ChildWithFoodFare"]);
                        AWFood = Convert.ToInt32(dtTour.Rows[0]["noAdultWithFood"]);
                        CWFood = Convert.ToInt32(dtTour.Rows[0]["noChildWithFood"]);
                        decimal Advance = decimal.Round(Convert.ToDecimal(dtTour.Rows[0]["Advance"]));
                        //string newUserName="select UserId from agent_agentdetails(nolock) where agentid='"+agentID+"' ";
                        ViewState["UserName"] = Convert.ToString(UserName);
                        //if (agentID == 0)
                        //{
                        //    ViewState["TotalAmount"] = Convert.ToString(TotalAmount);
                        //}
                        //else
                        //{
                        decimal st = decimal.Round((Convert.ToDecimal(Amount) * (Convert.ToDecimal(STaxValue) / 100)));
                        ViewState["TotalAmount"] = Convert.ToString(Amount + st + ServiceChargesTotal);
                        //}

                        ViewState["Advance"] = Convert.ToDecimal(Advance);//ViewState["TotalAmount"]);
                        ViewState["STaxValue"] = Convert.ToString(STaxValue);
                        ViewState["NoOfpax"] = Convert.ToString(Convert.ToInt32(NAdult + NChild + NAdultsTwin + NadultsTriple + NSingleAdult + NChildwithoutBed + Ndormitory + AWFood + CWFood));
                        ViewState["TicketNo"] = Convert.ToString(TicketNo);
                        ViewState["PaymentMode"] = Convert.ToString(PaymentMode.Trim());
                        ViewState["AgentCredit"] = Convert.ToString(AgentCredit);
                        ViewState["AgentDebit"] = Convert.ToString(AgentDebit);
                        ViewState["SeatNumbers"] = Convert.ToString(SeatNumbers);
                        ViewState["TourNo"] = Convert.ToString(TourNo);
                        ViewState["TDS"] = Convert.ToString(TDS);
                        ViewState["TourName"] = Convert.ToString(TourName);
                        ViewState["AdultFare"] = Convert.ToString(AdultFare);
                        ViewState["ChildFare"] = Convert.ToString(ChildFare);
                        ViewState["Adultstwinfare"] = Convert.ToString(Adultstwinfare);
                        ViewState["adultstriplefare"] = Convert.ToString(adultstriplefare);
                        ViewState["childwithoutbedfare"] = Convert.ToString(childwithoutbedfare);
                        ViewState["singleadultfare"] = Convert.ToString(singleadultfare);
                        ViewState["dormitoryfare"] = Convert.ToString(dormitoryfare);
                        ViewState["AWFoodFare"] = Convert.ToString(AWFoodFare);
                        ViewState["CWFoodFare"] = Convert.ToString(CWFoodFare);
                        StringBuilder TicketStr = new StringBuilder();
                        if (n == 'Y')
                        {
                            TicketStr.Append("<Table id=Table2 cellSpacing=0 cellPadding=0 width=100% border=0 bordercolor=#fff0ca align=center  >");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD>");
                            TicketStr.Append("<DIV id=print_area" + i + ">");
                            TicketStr.Append("<TABLE id=Table21 cellSpacing=0 cellPadding=6 width=100% border=1  bordercolor=#fff0ca >");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD class=style5 style=HEIGHT:15px align=center></TD>");
                            TicketStr.Append("<TD class=style5 style=HEIGHT:15px align=center></TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR><td colspan=2><table width=100% border=1 cellpadding=0 cellspacing=0 bgcolor=#CCCCCC> <tr> <td><table width=100% border=0 cellpadding=10 cellspacing=0 bgcolor=#FFFFFF><tr>");

                            string saveName = "Logo\\" + Session["UserId"].ToString();
                            string exist = "N";
                            if (System.IO.File.Exists(Server.MapPath(saveName + ".jpg"))) { saveName = saveName + ".jpg"; exist = "Y"; }
                            if (System.IO.File.Exists(Server.MapPath(saveName + ".gif"))) { saveName = saveName + ".gif"; exist = "Y"; }
                            if (System.IO.File.Exists(Server.MapPath(saveName + ".png"))) { saveName = saveName + ".png"; exist = "Y"; }
                            string address1 = Globals.AgentAddress;
                            string ph = Globals.AgentPhone;
                            if (exist == "Y")
                            {
                                TicketStr.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><img src='" + saveName + "' width=\"157\" height=\"69\" /><br />" + address1 + "," + ph + "</font></td>");
                            }
                            else
                            {
                                string AgentName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                                TicketStr.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><string>'" + AgentName + "'</string><br/>" + address1 + "," + ph + "</font></td>");
                            }
                            TicketStr.Append("<td width=\"31%\" valign=\"top\"><img src=\"../Assets/images/southerntravels_logo.gif\" width=\"231\" height=\"64\" /></td>");
                            TicketStr.Append("</TR></table></td></tr></table></td></tr>");
                            //TicketStr.Append("<TR>");
                            //TicketStr.Append("<TD class=style5 align=center height=7></TD>");
                            //TicketStr.Append("<TD class=style5 align=center height=7></TD>");
                            //TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD class=cgi style=HEIGHT:15px align=center colspan=2>");//code added
                            TicketStr.Append("<table width=100%>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD class=cgi width=60% align=right >");
                            TicketStr.Append("CANCELLED TOUR TICKET");
                            TicketStr.Append("</td>");
                            TicketStr.Append("<td width=40% align=right>");
                            //TicketStr.Append("<INPUT class=intdtxth id=Button1 onclick=getPrint('print_area" + i + "'); type=button value=Print name=Button1>");
                            //TicketStr.Append("<INPUT class=intdtxth id=btnPrint  onclick=window.open('Itinerary.aspx?ID=" + TourNo + "','','height=700,width=800,scrollbars=yes'); type=button value=\"View/Print Itinerary\" size=25>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("</Table>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD colspan=2>");
                            TicketStr.Append("<TABLE id=Table1 cellSpacing=0 cellPadding=1 width=100% border=1 bordercolor=#fff0ca >");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Ticket No: </SPAN><SPAN class=hlinks>");
                            TicketStr.Append("" + TicketNo + "");
                            TicketStr.Append("</SPAN></TD>");

                            TicketStr.Append("<TD ><SPAN class=cgi>PNR&nbsp;No:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(Convert.ToString(ViewState["TransNumber"]));
                            TicketStr.Append("</SPAN>");

                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi>Journey Date:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(JourneyDate.ToString("dd/MM/yyyy"));
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Tour Name:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(TourName);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD ><SPAN class=cgi>Adults:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(NAdult + NAdultsTwin + NadultsTriple + NSingleAdult + Ndormitory + AWFood);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi>Children:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(NChild + NChildwithoutBed + CWFood);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Pickup Point:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(PickUppoint);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi>Reporting Time:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(ReportTime.ToShortTimeString());
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi>Departure Time:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(DepartTime.ToShortTimeString());
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD colspan=3>");
                            TicketStr.Append("<Table width=\"100%\">");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD  width=15%><SPAN class=cgi>&nbsp;Costing: </SPAN>");
                            TicketStr.Append("</TD>");
                            /* if (NAdult > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Adults &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NAdult + " * " + AdultFare + "&nbsp;=&nbsp;" + CalcAdult() + "/-</SPAN>");
                             }
                             if (NChild > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Children &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NChild + "*" + ChildFare + "&nbsp;=&nbsp;" + CalcChild() + "/-</SPAN>");
                             }
                             if (Ndormitory > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Dormitory Accommodation &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + Ndormitory + "*" + dormitoryfare + "&nbsp;=&nbsp;" + CalcDormiroty() + "/-</SPAN>");
                             }
                             if (NAdultsTwin > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Adults On Twin Sharing &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NAdultsTwin + "*" + Adultstwinfare + "&nbsp;=&nbsp;" + CalcAdulttwin() + "/-</SPAN>");
                             }
                             if (NadultsTriple > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Adults On Triple Sharing &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NadultsTriple + "*" + adultstriplefare + "&nbsp;=&nbsp;" + CalcAdulttriple() + "/-</SPAN>");
                             }
                             if (NSingleAdult > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Adults On Single Sharing &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NSingleAdult + "*" + singleadultfare + "&nbsp;=&nbsp;" + CalcSingleadult() + "/-</SPAN>");
                             }
                             if (NChildwithoutBed > 0)
                             {
                                 TicketStr.Append(" <br/>&nbsp;&nbsp;&nbsp;&nbsp; <SPAN class=cgi>Child Without Bed &nbsp;</SPAN>");
                                 TicketStr.Append("<SPAN class=hlinks>" + NChildwithoutBed + "*" + childwithoutbedfare + "&nbsp;=&nbsp;" + CalcChildWithOutBed() + "/-</SPAN>");
                             }
                            */
                            StringBuilder lCostingHTML = new StringBuilder();
                            #region "Costing HTML"
                            if (NAdult > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Adults", NAdult, AdultFare, CalcAdult()));
                            }
                            if (NChild > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Children", NChild, ChildFare, CalcChild()));
                            }
                            if (Ndormitory > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Dormitory Accommodation", Ndormitory, dormitoryfare, CalcDormiroty()));
                            }
                            if (NAdultsTwin > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Twin Sharing", NAdultsTwin, Adultstwinfare, CalcAdulttwin()));
                            }
                            if (NadultsTriple > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Triple Sharing", NadultsTriple,
                                    adultstriplefare, CalcAdulttriple()));
                            }
                            if (NSingleAdult > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Single Sharing", NSingleAdult,
                                    singleadultfare, CalcSingleadult()));
                            }
                            if (NChildwithoutBed > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Child Without Bed", NChildwithoutBed,
                                    childwithoutbedfare, CalcChildWithOutBed()));
                            }
                            if (AWFood > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Adult With South Veg Food", AWFood, AWFoodFare, CalcAdultWFood()));
                            }
                            if (CWFood > 0)
                            {
                                lCostingHTML.Append(ClsCommon.GetCostingRow("Child With South Veg Food", CWFood, CWFoodFare, CalcChildWFood()));
                            }
                            if (lCostingHTML.ToString().Trim() != string.Empty)
                            {
                                string lCosting = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" + lCostingHTML.ToString() +
                                    "</table>";
                                lCostingHTML = new StringBuilder();
                                lCostingHTML.Append(lCosting);
                            }
                            #endregion

                            TicketStr.Append("<TD>");
                            TicketStr.Append(lCostingHTML);
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("</table>");

                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD colSpan=10>");
                            StringBuilder Strname = new StringBuilder();
                            Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100%>");
                            Strname.Append("<TR style=\"border-style:solid; border-width:5px; border-color:Black;\">");
                            Strname.Append("<TD class=cgi align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-top-style:none;\">Passenger Names</TD>");
                            Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-top-style:none;\">Age</TD>");
                            Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-top-style:none;\">Sex</TD>");
                            Strname.Append("<TD class=cgi align=center width=50px style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-top-style:none;\">Bus No</TD>");
                            Strname.Append("<TD class=cgi align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-right-style:none; border-top-style:none;\">Seat</TD>");
                            Strname.Append("</TR>");

                            #region Optimize Code
                            /*string str2 = "select * from ticketnames where ticketno='" + TicketCode + "'";
                        dtTour = new DataTable();
                        dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str2);*/

                            /*string ticketbusno = "select busno,seatnumbers from ticketbusno where ticketno='" + TicketCode + "' order by busno";
                            DataTable dtseats = DataLib.GetDataTable(DataLib.Connection.ConnectionString, ticketbusno);*/
                            #endregion
                            dtTour = ldsRecSet.Tables[0];
                            string[] seat;
                            seat = SeatNumbers.Split(',');
                            dtseats = ldsRecSet.Tables[1];

                            string seat11 = "";
                            string seat1 = "";
                            string busno11 = "";
                            for (int j = 0; j < dtseats.Rows.Count; j++)
                            {
                                seat11 = Convert.ToString(dtseats.Rows[j]["seatnumbers"]);
                                busno11 = Convert.ToString(dtseats.Rows[j]["busno"]);
                                string[] aa = seat11.Split(',');
                                for (int k = 0; k < aa.Length; k++)
                                {
                                    if (seat1 == "")
                                    {
                                        seat1 = busno11 + "-" + aa[k];
                                    }
                                    else
                                    {
                                        seat1 = seat1 + "@" + busno11 + "-" + aa[k];
                                    }
                                }
                            }
                            string[] busseat = seat1.Split('@');
                            for (int m = 0; m < dtTour.Rows.Count; m++)
                            {
                                Name = Convert.ToString(dtTour.Rows[m]["Name"]);
                                Age = Convert.ToInt32(dtTour.Rows[m]["Age"]);
                                Sex = Convert.ToString(dtTour.Rows[m]["Sex"]);
                                //Strname.Append("<TR><TD class=hlinks> " + Name + " </TD><TD class=hlinks width=15px align=center> " + Age + " </TD><TD class=hlinks width=15px align=center> " + Sex + " </TD><TD class=hlinks width=50px align=center> " + BusNo + " </TD><TD class=hlinks align=center> " + seat[m].Replace("s", "") + " </TD></TR>");
                                string[] bno = busseat[m].Split('-');
                                string aa = bno[0], lTopBottomBorder = ""; ;
                                string bb = bno[1];

                                lTopBottomBorder = " border-top-style:none;";
                                if (m == (dtTour.Rows.Count - 1))
                                {
                                    lTopBottomBorder += " border-bottom-style:none;";
                                }
                                Strname.Append("<TR style=\"border-style:solid; border-width:5px; border-color:Black;\">");
                                Strname.Append("<TD class=hlinks style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\">&nbsp; " +
                                    dtTour.Rows[m]["Name"].ToString() + " </TD>");
                                Strname.Append("<TD class=hlinks width=15px align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " +
                                    Convert.ToInt32(dtTour.Rows[m]["Age"].ToString()) + " </TD>");
                                Strname.Append("<TD class=hlinks width=15px align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " +
                                    dtTour.Rows[m]["Sex"].ToString() + " </TD>");
                                Strname.Append("<TD class=hlinks width=50px align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none;" + lTopBottomBorder + "\"> " + aa + " </TD>");
                                Strname.Append("<TD class=hlinks align=center style=\"border-style:solid; border-width:1px; " +
                                "border-color:Black; border-left-style:none; border-right-style:none;" + lTopBottomBorder + "\"> " +
                                bb.Replace("s", "") + " </TD>");
                                Strname.Append("</TR>");
                            }
                            Strname.Append("</TABLE>");
                            TicketStr.Append(Strname);
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Amount Rs.:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(Amount);
                            TicketStr.Append("/-</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi></SPAN> <SPAN class=hlinks>");

                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            decimal amt3 = 0, amt1 = 0, amt = 0;
                            if (STaxValue <= 0)
                            {
                                TicketStr.Append("<TD> </TD>");
                            }
                            else
                            {
                                if ((Session["AgentId"] == null) && (Session["BranchUserId"] == null))
                                {
                                    TicketStr.Append("<TD><SPAN class=cgi>GST (" + STaxValue + "%)+Convenience Charges(" + cctaxvalue + "%):</SPAN>");
                                }
                                else
                                {
                                    TicketStr.Append("<TD><SPAN class=cgi>GST (" + STaxValue + "%):</SPAN>");
                                }

                                if ((Session["AgentId"] == null) && (Session["BranchUserId"] == null))
                                {
                                    amt1 = (Amount * (STaxValue / 100));
                                    decimal amt2 = ((amt1 + Amount) * (cctaxvalue / 100));
                                    amt3 = amt1 + amt2;
                                    TicketStr.Append(" <SPAN class=hlinks>" + decimal.Round(amt3) + "/- </SPAN></TD>");
                                }
                                else
                                {
                                    amt = Amount * (STaxValue / 100);
                                    TicketStr.Append(" <SPAN class=hlinks>" + decimal.Round(amt) + "/- </SPAN></TD>");
                                }
                            }
                            TicketStr.Append("</tr>");
                            int pATotalPax = NAdult + NAdultsTwin + NadultsTriple + NSingleAdult + Ndormitory + AWFood;
                            int pCTotalPax = NChild + NChildwithoutBed + CWFood;
                            StringBuilder pServiceCharges = new StringBuilder();

                            pServiceCharges.Append("<tr>");
                            pServiceCharges.Append("<td colspan=\"3\">");
                            pServiceCharges.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                            pServiceCharges.Append("<tr>");
                            pServiceCharges.Append("<td width=\"25%\" ><b>Service Charge</b></br>(Pickup Point) :</td>");
                            //pServiceCharges.Append("</tr>");
                            //pServiceCharges.Append("<tr>");
                            //pServiceCharges.Append("<td width=\"10%\">&nbsp;");
                            //pServiceCharges.Append("</td>");
                            pServiceCharges.Append("<td width=\"30%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                            pServiceCharges.Append("#ServiceChargesAmount");
                            pServiceCharges.Append("</td>");
                            pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                            pServiceCharges.Append("<span class=\"cgi\">&nbsp;Amount :</span><span class=\"hlinks\"> #SAmount </span>");
                            pServiceCharges.Append("</td>");
                            pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                            pServiceCharges.Append("<span class=\"cgi\">&nbsp;GST :</span><span class=\"hlinks\"> #ServiceChargesTax </span>");
                            pServiceCharges.Append("</td>");
                            pServiceCharges.Append("<td width=\"15%\" style=\"border-left-style:solid; border-width:1px; border-color:Black;border-bottom-style:none;\">");
                            pServiceCharges.Append("<span class=\"cgi\">&nbsp;Total  :</span><span class=\"hlinks\"> #ServiceChargesTotal </span>");
                            pServiceCharges.Append("</td>");
                            pServiceCharges.Append("</tr>");
                            pServiceCharges.Append("</table>");
                            pServiceCharges.Append("</td>");
                            pServiceCharges.Append("</tr>");


                            if (AdultServiceCharges > 0)
                            {
                                StringBuilder strCal = new StringBuilder();
                                strCal.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");

                                strCal.Append("<tr>");
                                strCal.Append("<td>");
                                strCal.Append("<span class=\"cgi\">Adult</span>");
                                strCal.Append("</td>");
                                strCal.Append("<td>");
                                strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(AdultServiceCharges)));
                                strCal.Append("</td>");
                                strCal.Append("<td>");
                                strCal.Append("<span class=\"cgi\">" + "X" + "</span>");
                                strCal.Append("</td>");
                                strCal.Append("<td>");
                                strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(pATotalPax)) + "</span><span class=\"cgi\"> =</span>");
                                strCal.Append("</td>");
                                strCal.Append("<td>");
                                strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(AdultServiceCharges) * decimal.Round(pATotalPax)) + "</span>");
                                strCal.Append("</td>");
                                strCal.Append("</tr>");
                                if (pCTotalPax > 0)
                                {
                                    strCal.Append("<tr>");
                                    strCal.Append("<td>");
                                    strCal.Append("<span class=\"cgi\">Child</span>");
                                    strCal.Append("</td>");
                                    strCal.Append("<td>");
                                    strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(ChildServiceCharges)));
                                    strCal.Append("</td>");
                                    strCal.Append("<td>");
                                    strCal.Append("<span class=\"cgi\">" + "X" + "</span>");
                                    strCal.Append("</td>");
                                    strCal.Append("<td>");
                                    strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(pCTotalPax)) + "</span><span class=\"cgi\"> =</span>");
                                    strCal.Append("</td>");
                                    strCal.Append("<td>");
                                    strCal.Append("<span class=\"hlinks\">" + Convert.ToString(decimal.Round(ChildServiceCharges) * decimal.Round(pCTotalPax)) + "</span>");
                                    strCal.Append("</td>");
                                    strCal.Append("</tr>");
                                }
                                strCal.Append("</table>");

                                pServiceCharges = pServiceCharges.Replace("#ServiceChargesAmount", strCal.ToString());
                                pServiceCharges = pServiceCharges.Replace("#SAmount", Convert.ToString(decimal.Round(ServiceChargesTotal) - decimal.Round(ServiceChargesTaxVal)));

                                pServiceCharges = pServiceCharges.Replace("#ServiceChargesTax", Convert.ToString(decimal.Round(ServiceChargesTaxVal)));
                                pServiceCharges = pServiceCharges.Replace("#ServiceChargesTotal", Convert.ToString(decimal.Round(ServiceChargesTotal)));
                                TicketStr = TicketStr.Append(pServiceCharges.ToString());
                            }
                            TicketStr.Append("<TD colspan=3><SPAN class=cgi >Total Amount: </SPAN>");
                            TicketStr.Append("<SPAN class=hlinks>" + decimal.Round(Amount + amt + ServiceChargesTotal) + "/-</SPAN>");

                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");


                            //<!-- For Displaying the Cancellation Charges -->                        
                            string[] dd = txtjdate.Value.Split('/');
                            if (dd[0].Length <= 1)
                                dd[0] = "0" + dd[0];
                            if (dd[1].Length <= 1)
                                dd[1] = "0" + dd[1];

                            string journeydate11 = (dd[1] + "/" + dd[0] + "/" + dd[2]);
                            #region Optimize Code
                            /*string jdate = "select journeydate from tours where tourno='" + TourNo + "' and convert(varchar(10),journeydate,101)='" + journeydate11 + "'";
                        DateTime journeydate = Convert.ToDateTime(DataLib.GetStringData(DataLib.Connection.ConnectionString, jdate));*/
                            #endregion
                            DateTime journeydate = Convert.ToDateTime(ldsRecSet.Tables[2].Rows[0][0]);
                            TimeSpan ts = new TimeSpan();
                            ts = journeydate.Subtract(DateTime.Now);
                            int NoofDaysleft = ts.Days * 24 + ts.Hours;

                            decimal cancelcharges = 0, stvalue = 0, cc = 0, refundamount = 0;

                            decimal cancelpercen = Convert.ToDecimal(canpercentage(TourNo, NoofDaysleft, false));
                            stvalue = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(ViewState["STaxValue"]) / 100)));
                            cancelcharges = decimal.Round((Convert.ToDecimal(ViewState["Amount"]) + stvalue) * (Convert.ToDecimal(cancelpercen) / 100));

                            decimal pServiceCancelPrc = Convert.ToDecimal(canpercentage(TourNo, NoofDaysleft, true));
                            decimal pServiceCancelCharge = 0;
                            if (ServiceChargesTotal > 0)
                            {
                                pServiceCancelCharge = decimal.Round(ServiceChargesTotal * (Convert.ToDecimal(pServiceCancelPrc) / 100));

                            }
                            cancelcharges = cancelcharges + pServiceCancelCharge;

                            if (Convert.ToDecimal(ViewState["cctaxvalue"]) > 0)
                            {
                                cc = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(ViewState["cctaxvalue"]) / 100)));
                            }
                            #region Optimize Code
                            /* string chkcancel = "select * from cancel where newticket='" + TourNo + "'";
                        DataTable dtchkcancel = DataLib.GetDataTable(DataLib.Connection.ConnectionString, chkcancel);*/
                            #endregion
                            dtchkcancel = ldsRecSet.Tables[3];

                            decimal pTotCanCharges = 0; string strCanPrev = "";
                            if (dtchkcancel.Rows.Count > 0)
                            {
                                int? lHierarchyCounter = 0; pclsObj = new ClsAdo();

                                ldtHierarchyCanChr = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(TicketNo),
                                      ref lHierarchyCounter);

                                if (ldtHierarchyCanChr.Rows.Count > 0)
                                {


                                    if (decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - Convert.ToDecimal(ViewState["TotalAmount"])) >= 0)
                                    {
                                        pTotCanCharges = Convert.ToDecimal(ldtHierarchyCanChr.Rows[0]["TktCanCharge"]);
                                        strCanPrev = pTotCanCharges.ToString();
                                    }
                                    else
                                    {
                                        pTotCanCharges = decimal.Round(Convert.ToDecimal(ldtHierarchyCanChr.Compute("Sum(TktCanCharge)", "RowID >=0").ToString()));
                                        bool flag = false;
                                        for (int jk = 0; jk < ldtHierarchyCanChr.Rows.Count; jk++)
                                        {
                                            if (flag == false)
                                            {
                                                flag = true;
                                                strCanPrev = ldtHierarchyCanChr.Rows[jk]["TktCanCharge"].ToString();
                                            }
                                            else
                                            {
                                                strCanPrev = strCanPrev + "+" + ldtHierarchyCanChr.Rows[jk]["TktCanCharge"].ToString();
                                            }
                                        }

                                    }

                                }


                            }
                            else
                            {
                                TicketStr.Replace("#PCanCh", "Nill");
                            }
                            if (dtchkcancel.Rows.Count > 0)
                            {

                                if (Convert.ToDecimal(dtchkcancel.Rows[0]["RefundAmt"]) > 0)
                                {
                                    #region Optimize Code
                                    /*chkcancel = "select totalamount from onlinetoursbooking where ticketcode='" + TourNo + "'";
                                string tamt = DataLib.GetStringData(DataLib.Connection.ConnectionString, chkcancel);*/
                                    #endregion
                                    string tamt = ldsRecSet.Tables[4].Rows[0][0].ToString();
                                    decimal pretotal = Convert.ToDecimal(tamt);
                                    decimal tt = decimal.Round(Convert.ToDecimal(dtchkcancel.Rows[0]["RefundAmt"]) - cancelcharges);
                                    decimal yy = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - pretotal));
                                    if (yy > 0)
                                    {
                                        refundamount = tt - yy;
                                    }
                                    else
                                    {
                                        if (Advance >= Convert.ToDecimal(ViewState["TotalAmount"]))
                                        {
                                            refundamount = decimal.Round((Convert.ToDecimal(ViewState["TotalAmount"]) - cancelcharges));
                                        }
                                        else
                                        {
                                            refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges));
                                        }
                                    }
                                }
                                else
                                {
                                    refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges));
                                }

                            }
                            else
                            {
                                refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges));
                            }

                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Advance:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(Math.Round(Convert.ToDecimal(ViewState["Advance"]), 0));
                            TicketStr.Append("/-</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD><SPAN class=cgi>Cancellation Charges:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(cancelcharges);
                            TicketStr.Append("/-</SPAN>");
                            if (decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - Convert.ToDecimal(TotalAmount)) >= 0)
                            {
                                TicketStr.Append("</TD>");
                                TicketStr.Append("<TD><SPAN class=cgi>Refund Amount:</SPAN> <SPAN class=hlinks>");
                                TicketStr.Append(refundamount/* - pTotCanCharges*/);
                                TicketStr.Append("/-</SPAN>");
                                TicketStr.Append("</TD>");

                            }
                            else if (decimal.Round(Convert.ToDecimal(ViewState["Advance"]) - cancelcharges - pTotCanCharges) > 0)
                            {
                                TicketStr.Append("</TD>");
                                TicketStr.Append("<TD><SPAN class=cgi>Refund Amount:</SPAN> <SPAN class=hlinks>");
                                TicketStr.Append(refundamount - pTotCanCharges);
                                TicketStr.Append("/-</SPAN>");
                                TicketStr.Append("</TD>");

                            }
                            else
                            {
                                TicketStr.Append("</TD>");
                                TicketStr.Append("<TD><SPAN class=cgi>Balance Due</SPAN> <SPAN class=hlinks>");
                                TicketStr.Append(pTotCanCharges + cancelcharges - Convert.ToDecimal(ViewState["Advance"]));
                                TicketStr.Append("/-</SPAN>");
                                TicketStr.Append("</TD>");

                            }

                            /* if (refundamount - pTotCanCharges < 0)
                             {
                                 TicketStr.Append("</TD>");
                                 TicketStr.Append("<TD><SPAN class=cgi>Refund Amount:</SPAN> <SPAN class=hlinks>");
                                 TicketStr.Append(refundamount - pTotCanCharges);
                                 TicketStr.Append("/-</SPAN>");
                                 TicketStr.Append("</TD>");
                             }
                             else
                             {
                                 TicketStr.Append("</TD>");
                                 TicketStr.Append("<TD><SPAN class=cgi>Balance</SPAN> <SPAN class=hlinks>");
                                 TicketStr.Append(pTotCanCharges - refundamount);
                                 TicketStr.Append("/-</SPAN>");
                                 TicketStr.Append("</TD>");
                             }*/
                            TicketStr.Append("</TR>");
                            //
                            if (strCanPrev.Contains("+"))
                            {
                                //TicketStr.Replace("#PCanCh", strCanPrev + " = " + pTotCanCharges.ToString());
                                TicketStr.Append("<TR>");
                                TicketStr.Append("<TD colspan=3><SPAN class=cgi>Previous Cancellation Charges :</SPAN> <SPAN class=hlinks>");
                                TicketStr.Append(strCanPrev + " = " + pTotCanCharges.ToString());
                                TicketStr.Append("/-</SPAN>");
                                TicketStr.Append("</TD>");
                                TicketStr.Append("</TR>");
                            }
                            else
                            {
                                TicketStr.Append("<TR>");
                                TicketStr.Append("<TD colspan=3><SPAN class=cgi>Previous Cancellation Charges :</SPAN> <SPAN class=hlinks>");
                                TicketStr.Append(pTotCanCharges.ToString());
                                TicketStr.Append("/-</SPAN>");
                                TicketStr.Append("</TD>");
                                //TicketStr.Replace("#PCanCh", pTotCanCharges.ToString());
                                TicketStr.Append("</TR>");
                            }



                            //<!-- For Displaying the Cancellation Charges-->
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD><SPAN class=cgi>Payment Mode:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(PaymentMode);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("<TD colSpan=2>");
                            TicketStr.Append("");

                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD colSpan=3><SPAN class=cgi>Phone No:</SPAN> <SPAN class=hlinks>");
                            TicketStr.Append(TelNo);
                            TicketStr.Append("</SPAN>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("<TR>");
                            TicketStr.Append("<TD colSpan=3><SPAN class=cgi>Note*: Cancellation charges may vary depending on the time left for departure.</SPAN></TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("</TABLE>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            //TicketStr.Append("<TR>");
                            //TicketStr.Append("<TD align=center height=20></TD>");
                            //TicketStr.Append("<TD align=center height=20></TD>");
                            //TicketStr.Append("</TR>");
                            //TicketStr.Append("<TR>");
                            //TicketStr.Append("<TD align=center></TD>");
                            //TicketStr.Append("<TD align=center></TD>");
                            //TicketStr.Append("</TR>");
                            TicketStr.Append("</TABLE>");
                            TicketStr.Append("</DIV>");
                            TicketStr.Append("</TD>");
                            TicketStr.Append("</TR>");
                            TicketStr.Append("</Table>");
                            showTables();
                            return TicketStr;
                        }
                        else
                        {
                            hideTables();
                            TicketStr.Append(".");
                            //getfaregrid(JourneyDate, TourNo, ac);
                            GetTourFare(JourneyDate, TourNo, ac);
                            return TicketStr;
                        }
                    }
                    else
                    {
                        StringBuilder TicketStr = new StringBuilder();
                        TicketStr.Append("Y");
                        return TicketStr;
                    }
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
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtTour != null)
                {
                    dtTour.Dispose();
                    dtTour = null;
                }
                if (ldtHierarchyCanChr != null)
                {
                    ldtHierarchyCanChr.Dispose();
                    ldtHierarchyCanChr = null;
                }
                if (dtchkcancel != null)
                {
                    dtchkcancel.Dispose();
                    dtchkcancel = null;
                }
                if (dtseats != null)
                {
                    dtseats.Dispose();
                    dtseats = null;
                }

            }
        }
        public string canpercentage(int tourno, int NoofDaysleft, bool lType)
        {
            #region Optimize Code
            /*decimal cancelpercen = 0;
            string str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays < " + NoofDaysleft + " ORDER BY NoofDays DESC";
            DataTable dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            if (dtper.Rows.Count > 0)
            {
            }
            else
            {
                str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays >= " + NoofDaysleft + " ORDER BY NoofDays";
                dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            }
            if (dtper.Rows.Count > 0)
            {
                cancelpercen = Convert.ToDecimal(dtper.Rows[0]["percentage"]);
            }
            else
            {
                cancelpercen = 0;
            }*/
            #endregion
            int? lRetrun = 0;
            decimal cancelpercen = 0;
            try
            {
                pclsObj = new ClsAdo();
                cancelpercen = pclsObj.fnGetCancelPercentage(tourno, NoofDaysleft, lType, ref lRetrun);
                return Convert.ToString(cancelpercen);
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
            }
        }
        private void BranchStorage()
        {
            int i;
            branchlist = new ArrayList();
            #region Optimize Code
            /*string str5 = "select BranchCode from BranchMaster";
            DataTable dtTour = new DataTable();
            dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str5);*/
            #endregion
            pclsObj = new ClsAdo();
            DataTable dtTour = pclsObj.fnAgentBranchCode();
            try
            {
                for (i = 0; i < dtTour.Rows.Count - 1; i++)
                {
                    branchlist.Add(dtTour.Rows[i][0].ToString());
                }
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
        /// <summary>
        /// Not Use
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="rowid"></param>
        /// <param name="status"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        private string ChangeTableInsert(string TableName, int rowid, char status, char done)
        {
            int i;
            string String1 = "", String2 = "", String3 = "", ChangeInsertString = "";
            BranchStorage();
            String1 = "Insert into ChangeTable(TableName,RowId,Status";
            for (i = 0; i <= branchlist.Count - 1; i++)
            {
                String2 = String2 + "," + branchlist[i].ToString();
            }
            String2 = String2 + ") values ('" + TableName + "'," + rowid + ",'" + status + "'";
            for (i = 0; i <= branchlist.Count - 1; i++)
            {
                if (branchlist[i].ToString() == CurrentBranchCode)
                {
                    String3 = String3 + ",'d'";
                }
                else
                {
                    String3 = String3 + ",'" + done + "'";
                }
            }
            ChangeInsertString = String1 + String2 + String3 + ")";
            return ChangeInsertString;
        }
        /// <summary>
        /// Not Use
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string rowidretrieval(string s)
        {
            string sqlstring;
            sqlstring = "Select RespectiveRowId from NewKeyTable_Online with (Tablockx) where KeyType = '" + s + "'";
            return sqlstring;
        }
        private void hideTables()
        {
            Remainpax.Visible = true;
            listpax.Visible = true;
            trSeat.Visible = true;
            btnPartialTicket.Visible = true;
            btncantic.Visible = false;
            chkAccept.Visible = false;
            tblRuleRegulation.Visible = false;
        }
        private void showTables()
        {
            Remainpax.Visible = false;
            listpax.Visible = false;
            trSeat.Visible = false;
            btnPartialTicket.Visible = false;
            btncantic.Visible = true;
            chkAccept.Visible = true;
            tblRuleRegulation.Visible = true;
        }
        private string cancelticket(string ticketid)
        {
            DataTable ldtRecSet = null, dtComm = null;

            string SuccessSaving;
            try
            {
                pclsObj = new ClsAdo();

                string[] seatNotCancelled = new string[21];



                int[] BusNoCancelled = new int[16];


                decimal cancelcharges = 0, stvalue = 0, cc = 0, refundamount = 0;
                int tourno = Convert.ToInt32(ViewState["TourNo"]);

                string journeydate11;
                DateTime journeydate;
                string[] dd = txtjdate.Value.Split('/');
                if (dd[0].Length <= 1)
                    dd[0] = "0" + dd[0];
                if (dd[1].Length <= 1)
                    dd[1] = "0" + dd[1];

                journeydate11 = (dd[1] + "/" + dd[0] + "/" + dd[2]);
                #region Optimize Code
                /*string jdate = "select journeydate from tours where tourno='" + tourno + "' and convert(varchar(10),journeydate,101)='" + journeydate11 + "'";
            journeydate = Convert.ToDateTime(DataLib.GetStringData(DataLib.Connection.ConnectionString, jdate));*/
                #endregion
                //journeydate = Convert.ToDateTime(journeydate11);
                journeydate = Convert.ToDateTime(pclsObj.fnJourneyDate(Convert.ToDateTime(journeydate11), tourno));
                int NoofDaysleft;
                TimeSpan ts = new TimeSpan();
                ts = journeydate.Subtract(DateTime.Now);
                NoofDaysleft = ts.Days * 24 + ts.Hours;
                decimal cancelpercen = Convert.ToDecimal(canpercentage(tourno, NoofDaysleft, false));
                #region Optimize Code
                /*string str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays < " + NoofDaysleft + " ORDER BY NoofDays DESC";
            DataTable dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            decimal cancelpercen = 0;

            if (dtper.Rows.Count > 0)
            {
            }
            else
            {
                str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays >= " + NoofDaysleft + " ORDER BY NoofDays";
                dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            }

            if (dtper.Rows.Count > 0)
            {
                cancelpercen=Convert.ToDecimal(dtper.Rows[0]["percentage"]);
            }
            else
            {
                cancelpercen = 0;
            }*/
                #endregion
                pclsObj = new ClsAdo();
                Session["CancelCharge"] = Convert.ToString(cancelpercen);
                //cancelcharges = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(Session["CancelCharge"]) / 100)));
                // Commented by Santosh 17-May-2012 For Update Tax Detacted in Cancel Ticket
                /*cancelcharges = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(Session["CancelCharge"]) / 100)));

                stvalue = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(ViewState["STaxValue"]) / 100)));*/

                decimal sTaxValue = Convert.ToDecimal(pclsObj.fnGetSTaxForCanTkt(ticketid));
                stvalue = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["Amount"]) * (Convert.ToDecimal(sTaxValue) / 100)));

                decimal pTotal = Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(stvalue);
                cancelcharges = decimal.Round(pTotal * (Convert.ToDecimal(Session["CancelCharge"]) / 100));

                decimal pServiceCancelPrc = Convert.ToDecimal(canpercentage(tourno, NoofDaysleft, true));

                decimal pADServiceCharges = 0, pCHServiceCharges = 0;
                decimal pServiceChargesTotal = 0;
                decimal pServiceChargesAmount = 0;
                decimal pServiceChargesTax = 0;
                decimal pServiceChargesTaxVal = 0;
                decimal pServiceCancelCharge = 0;
                decimal pServiceChargeRefund = 0;
                if (ViewState["ServiceChargesTotal"] != null)
                {
                    pADServiceCharges = Convert.ToDecimal(ViewState["AdultServiceCharges"]);
                    pCHServiceCharges = Convert.ToDecimal(ViewState["ChildServiceCharges"]);
                    pServiceChargesTotal = Convert.ToDecimal(ViewState["ServiceChargesTotal"]);
                    pServiceChargesTax = Convert.ToDecimal(ViewState["ServiceChargesTax"]);
                    pServiceChargesTaxVal = Convert.ToDecimal(ViewState["ServiceChargesTaxVal"]);

                    pServiceChargesAmount = pServiceChargesTotal - pServiceChargesTaxVal;
                    pServiceCancelCharge = decimal.Round(pServiceChargesTotal * (pServiceCancelPrc / 100));

                }
                cancelcharges = cancelcharges + pServiceCancelCharge;

                if (Convert.ToDecimal(ViewState["cctaxvalue"]) > 0)
                {
                    cc = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(ViewState["TotalAmount"]) * (Convert.ToDecimal(ViewState["cctaxvalue"]) / 100)));
                }
                refundamount = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - cancelcharges - cc);

                if (Convert.ToDecimal(ViewState["Advance"]) >= Convert.ToDecimal(ViewState["TotalAmount"]))
                {
                    refundamount = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - cancelcharges - cc);
                }
                else if (decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges)) > 0)
                    refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges));/**/
                decimal AvailableBalance = 0;


                int agentid = Convert.ToInt32(ViewState["agentID"]);
                decimal CancellationCommissionAmount = 0;
                decimal lCalTds = 0;
                pclsObj = new ClsAdo();
                if (agentid > 0)
                {
                    #region Optimize Code
                    /* string qry, qry1;
                qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + agentid;
                string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                if (strRowNo == "" || strRowNo == null)
                {
                }
                else
                {
                    qry1 = "select isnull(availablebalance,0) as availablebalance from OnlineTransactionTable(nolock) where agentid=" + agentid + " and rowid=" + strRowNo;
                    string balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
                    if (balance == "")
                        Session["Balance"] = "0";
                    else
                        Session["Balance"] = balance;
                }*/
                    #endregion

                    /******** Cancellation Commission ********/
                    /* int? AgentLevel = default(int);
                     pclsObj.Agent_GetAgentLevelbyAgentId(agentid, ref AgentLevel);
                     */
                    dtComm = new DataTable();

                    decimal agentCancellationCommission = default(decimal);
                    //if (AgentLevel != null)
                    //{

                    dtComm = pclsObj.fnAgentCommission("FixedTour Cancel", agentid, tourno);
                    if (dtComm != null && dtComm.Rows.Count > 0)
                    {
                        agentCancellationCommission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
                    }
                    //}


                    decimal lTdsPer = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));


                    /*if (Convert.ToDecimal(ViewState["TotalAmount"]) <= Convert.ToDecimal(ViewState["Advance"]))
                    {*/
                    CancellationCommissionAmount = (cancelcharges * agentCancellationCommission) / 100;
                    lCalTds = Convert.ToDecimal(CancellationCommissionAmount * (Convert.ToDecimal(lTdsPer) / 100));
                    CancellationCommissionAmount = CancellationCommissionAmount - lCalTds;
                    /*}*/
                    /******** End ********/
                    string balance = "";
                    ldtRecSet = pclsObj.fnGetAgent_AvailableBalance(agentid);
                    if (ldtRecSet.Rows.Count > 0)
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
                }

                if (Convert.ToString(Session["Balance"]) != "")
                {
                    AvailableBalance = Convert.ToDecimal(Session["Balance"]) + CancellationCommissionAmount;
                }
                #region Optimize Code
                /*SqlCommand sqlcom = new SqlCommand();
            sqlcom.CommandType = CommandType.Text;
            SqlDataReader sqlread=null;
            SqlConnection sqlcon = DataLib.GetConnection();
            SqlTransaction Trans_On2line = sqlcon.BeginTransaction(IsolationLevel.RepeatableRead);
            sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.CommandType = CommandType.Text;
            sqlcom.Transaction = Trans_On2line;*/
                #endregion
                try
                {

                    //updating the TicketDetails table by setting the cancellation field as Y 
                    #region Optimize Code
                    #region Optimize Code
                    /*sqlcom.CommandText = "update TicketDetails set Cancelled='Y' where TicketNo='" + ticketid.Trim() + "';SELECT     RowId FROM         TicketDetails WHERE     (TicketNo = '" + ticketid.Trim() + "')";
                string rowId = Convert.ToString(sqlcom.ExecuteScalar());           
                sqlcom.CommandText = "Select NumericValue,RespectiveRowid from NewKeyTable_online where keyType='Cancel'";
                sqlread = sqlcom.ExecuteReader();

                while ((sqlread.Read()) == true)
                {
                    CancelNo = Convert.ToInt32(sqlread["NumericValue"]);
                    CancelRowid = Convert.ToInt32(sqlread["RespectiveRowid"]);
                    Cancel1Rowid = Convert.ToInt32(sqlread["RespectiveRowid"]);
                }
                sqlread.Close();*/
                    #endregion
                    /*DataTable dtcan = pclsObj.fnNewKeyTableID("Cancel");
                if (dtcan.Rows.Count > 0)
                {
                    CancelNo = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
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
                CancelCode = "CAN" + CancelCode + CancelString;*/

                    /*sqladd = "insert into Cancel(RowId,CanNo,TicketNo,CanNoTick,CanCharges,RefundAmt,UserName,BranchCode)
                     * values(" + CancelRowid + ",'" + CancelCode + "','" + ticketid.Trim() + "'," + Convert.ToInt32(ViewState["NoOfpax"]) + ",
                     * " + cancelcharges + "," + refundamount + ",'" + Session["AgentId"] + "','" + Convert.ToString(Session["LocalBranch"]) + "')";
                    sqlcom.CommandText = sqladd;
                    sqlcom.ExecuteNonQuery();

                    //Updating the NewKeyTable_Online with the new Cancel Number and Rowid 
                    sqladd = "update newkeytable_online set NumericValue = " + CancelNo + " , RespectiveRowid=" + CancelRowid + " where KeyType = 'Cancel' ";
                    sqlcom.CommandText = sqladd;
                    sqlcom.ExecuteNonQuery();           
                    //Retrieving the CancelName Table Rowid 
                    sqlcom.CommandText = rowidretrieval("CanName");
                    sqlcom.CommandTimeout = 500000;
                    sqlread = sqlcom.ExecuteReader();
                    while ((sqlread.Read()) == true)
                    {
                        CancelNameRowid = Convert.ToInt32(sqlread[0]);
                        CancelName1Rowid = Convert.ToInt32(sqlread[0]);
                    }
                    sqlread.Close();*/
                    #endregion

                    decimal lAgentCredit = 0, lAgentDebit = 0, lCredit = 0, lDebit = 0, lAvailableBalance = 0;

                    lAvailableBalance = decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["TotalAmount"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges));

                    lAgentCredit = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) -
                        cancelcharges + CancellationCommissionAmount);
                    if (lAgentCredit < 0)
                    {
                        lAgentCredit = CancellationCommissionAmount;
                    }
                    lAgentDebit = decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges));

                    lDebit = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommissionAmount);
                    if (lDebit < 0)
                    {
                        lDebit = CancellationCommissionAmount;
                    }
                    lCredit = decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges));

                    decimal pTotCanCharges = 0;
                    if (Convert.ToBoolean(ViewState["IsPartialPaymentByAgent"]))
                    {
                        int? lHierarchyCounter = 0;
                        DataTable ldtHierarchyCanChr = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(ticketid.Trim()),
                           ref lHierarchyCounter);

                        if (ldtHierarchyCanChr.Rows.Count > 0)
                        {
                            pTotCanCharges = decimal.Round(Convert.ToDecimal(ldtHierarchyCanChr.Compute("Sum(TktCanCharge)", "RowID >=0").ToString()));
                        }

                        lAvailableBalance = decimal.Round((AvailableBalance + Convert.ToDecimal(ViewState["Advance"])) - (Convert.ToDecimal(ViewState["AgentCredit"])) - (cancelcharges) /*+ pTotCanCharges*/);
                        lAgentCredit = CancellationCommissionAmount;
                        lAgentDebit = (cancelcharges/* + pTotCanCharges*/);
                        lDebit = CancellationCommissionAmount;
                        lCredit = (cancelcharges/* + pTotCanCharges*/);
                        decimal lto = Convert.ToDecimal(ViewState["TotalAmount"]);
                        decimal ltoA = Convert.ToDecimal(ViewState["Advance"]);
                        if (Convert.ToDecimal(ViewState["TotalAmount"]) >= Convert.ToDecimal(ViewState["Advance"]))
                        {
                            lAvailableBalance = decimal.Round((AvailableBalance + Convert.ToDecimal(ViewState["Advance"])) - (cancelcharges + pTotCanCharges)/**/);
                            lAgentCredit = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (cancelcharges + pTotCanCharges)) + CancellationCommissionAmount);
                            lAgentDebit = (cancelcharges + pTotCanCharges);
                            lDebit = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (cancelcharges + pTotCanCharges)) + CancellationCommissionAmount);
                            lCredit = (cancelcharges + pTotCanCharges);
                            refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (cancelcharges + pTotCanCharges)));
                        }

                    }


                    int val1 = pclsObj.fnUpdateFixedTourCancelInfo(decimal.Round(cancelcharges), decimal.Round(refundamount), ticketid.Trim(), Convert.ToInt32(ViewState["NoOfpax"]),
                        Convert.ToString(Session["LocalBranch"]), agentid, lAgentCredit, lAvailableBalance, lAgentDebit, decimal.Round(Convert.ToDecimal(ViewState["Amount"])),
                            decimal.Round(Convert.ToDecimal(CancellationCommissionAmount)), Convert.ToString(ViewState["BranchCode"]), Convert.ToString(ViewState["PaymentMode"]),
                            lDebit, lCredit, Convert.ToString(ViewState["UserName"]), Convert.ToString(ViewState["BranchCode"]), stvalue, decimal.Round(Convert.ToDecimal(lCalTds)), false,
                             "", 0, '0', pServiceCancelCharge, "", 0, false, null, null);

                    #region Optimize code
                    //DataTable dtcan1 = pclsObj.fnNewKeyTableID("CanName");
                    //if (dtcan1.Rows.Count > 0)
                    //{
                    //    CancelNameRowid = Convert.ToInt32(dtcan1.Rows[0]["RespectiveRowid"]);
                    //    CancelName1Rowid = Convert.ToInt32(dtcan1.Rows[0]["RespectiveRowid"]);
                    //}
                    //int val12 = pclsObj.fnInsertCancelPasanInfo(ticketid, CancelNameRowid, CancelCode);


                    /*string str2 = "select * from ticketnames where ticketno='" + ticketid + "'";
                   DataTable dtTour = new DataTable();
                   dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str2);
                   count = 0;
                   for (i = 0; i <= dtTour.Rows.Count - 1; i++)
                   {
                       count = count + 1;
                       CancelNameRowid = CancelNameRowid + 1;
                       sqlcom.CommandText = "insert into CanName(Rowid,Canno,Name,Age,Sex)
                   * values(" + CancelNameRowid + ",'" + CancelCode + "','" + Convert.ToString(dtTour.Rows[i]["Name"]) + "',
                   * '" + Convert.ToInt32(dtTour.Rows[i]["Age"]) + "','" + Convert.ToString(dtTour.Rows[i]["Sex"]) + "')";
                       sqlcom.ExecuteNonQuery();
                       count = count + 3;
                   }

                   //Updating the NewKeyTable_Online with the new Ticket Number and Row id 
                   sqladd = "update newkeytable_online set RespectiveRowid=" + CancelNameRowid + " where KeyType = 'CanName' ";
                   sqlcom.CommandText = sqladd;
                   sqlcom.ExecuteNonQuery();

                       string strid = "select transactiontypeid from transactiontypemaster where transactionname='BusTicketCancellation' and IsAgentService='Y'";
                       string transtype = DataLib.GetStringData(DataLib.Connection.ConnectionString, strid);

                       if (Convert.ToString(ViewState["PaymentMode"]) == "AgentCash")
                       {
                           sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,
                     * TicketAmount,Commission,PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,
                     * ImpersonatingBranchCode,Cashier,TransState,Status,Servicetax,TDS)values(" + agentid + ",'" + CancelCode + "',
                     * " + Convert.ToInt32(transtype) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + "," + decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges)) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + "," + decimal.Round(Convert.ToDecimal(ViewState["Amount"])) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"])) + ",'" + Convert.ToString(ViewState["PaymentMode"]) + "','',''," + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + ",'','" + ViewState["UserName"] + "','" + ViewState["BranchCode"] + "','','','','P','S'," + stvalue + "," + decimal.Round(Convert.ToDecimal(ViewState["TDS"])) + ")";
                       }
                       else
                       {
                           sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,
                     * Commission,PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,
                     * Cashier,TransState,Status,servicetax,TDS)values(" + agentid + ",'" + CancelCode + "'," + Convert.ToInt32(transtype) + ",
                     * " + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + ",
                     * " + decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - 
                     * cancelcharges)) + "," + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + ",
                     * " + decimal.Round(Convert.ToDecimal(ViewState["Amount"])) + ",
                     * " + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"])) + ",'" + Convert.ToString(ViewState["PaymentMode"]) + "','','',
                     * " + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + ",
                     * " + decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges)) + ",'','" + ViewState["UserName"] + "',
                     * '" + ViewState["BranchCode"] + "','','','','P','S'," + stvalue + "," + decimal.Round(Convert.ToDecimal(ViewState["TDS"])) + ")";
                       }          

                   sqlcom.CommandText = sqladd;
                   sqlcom.ExecuteNonQuery();*/

                    /////// Transaction Table -Change Table,New KeyTable_Online,Transaction Table Rows are inserted 

                    //int val2 = pclsObj.fnAgentUblockSeatCancelTKT(ticketid, Convert.ToString(ViewState["busserialno"]), CancelCode); ;

                    #region Optimize Code
                    /*//===========Start========================

               string multiple = "select seatno from onlinetoursbooking where ticketcode='" + ticketid + "'";
                string sssss = DataLib.GetStringData(DataLib.Connection.ConnectionString, multiple);
                string[] str1 = sssss.Split(',');
                busserialno = Convert.ToString(ViewState["busserialno"]);
                string[] ser = busserialno.Split(',');
                string seat = "";
                int Bbusno = 0;
                if (ser.Length > 1)
                {
                    for (int k = 0; k < ser.Length; k++)
                    {
                        seat = "";
                        for (int ii = 0; ii < str1.Length; ii++)
                        {
                            if (str1[ii].Length > 3)
                            {
                                if (str1[ii].Substring(1, 1) == Convert.ToString(k + 1))
                                {
                                    //tblseatfld[ii] = str1[ii].Substring(2, 2);
                                    if (seat == "")
                                    {
                                        seat = str1[ii].Substring(2, 2);
                                    }
                                    else
                                    {
                                        seat = seat + "," + str1[ii].Substring(2, 2);
                                    }
                                }
                            }
                            else
                            {
                                if (str1[ii].Substring(1, 1) == Convert.ToString(k + 1))
                                {
                                    //tblseatfld[ii] = str1[ii].Substring(2, 1);
                                    if (seat == "")
                                    {
                                        seat = str1[ii].Substring(2, 1);
                                    }
                                    else
                                    {
                                        seat = seat + "," + str1[ii].Substring(2, 1);
                                    }
                                }
                            }
                        }
                        seat = seat.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                        string[] sd = seat.Split(',');
                        string tbnseat = "";
                        for (i = 0; i < sd.Length; i++)
                        {
                            string updatestring = "UPDATE SeatArrangement set s" + sd[i] + " = Null where TourSerial = " + ser[k] + " ";
                            sqlcom.CommandText = updatestring;
                            sqlcom.ExecuteNonQuery();
                            if (tbnseat == "")
                            {
                                tbnseat = "s" + sd[i];
                            }
                            else
                            {
                                tbnseat = tbnseat + "," + "s" + sd[i];
                            }
                        }
                        int seatArrangeRowid = 0;
                        sqlcom.CommandText = "Select * from SeatArrangement where TourSerial = " + ser[k] + "";
                        sqlread = sqlcom.ExecuteReader();
                        while (sqlread.Read())
                        {
                            Bbusno = Convert.ToInt32(sqlread["BusNo"]);
                            seatArrangeRowid = Convert.ToInt32(sqlread[0].ToString());
                        }
                        sqlread.Close();

                        sqlcom.CommandText = rowidretrieval("CancelBusNo");
                        sqlread = sqlcom.ExecuteReader();
                        while ((sqlread.Read()) == true)
                        {
                            CanBusnoRowid = Convert.ToInt32(sqlread[0]);
                            CanBusno1Rowid = Convert.ToInt32(sqlread[0]);
                        }
                        sqlread.Close();

                        CanBusnoRowid = CanBusnoRowid + 1;
                        sqlcom.CommandText = "insert into CancelBusNo(Rowid,CanNo,BusNo,SeatNo) values(" + CanBusnoRowid + ",'" + CancelCode + "'," + Bbusno + ",'" + tbnseat + "')";
                        sqlcom.ExecuteNonQuery();
                        sqladd = "update newkeytable_online set RespectiveRowid=" + CanBusnoRowid + " where KeyType = 'CancelBusno' ";
                        sqlcom.CommandText = sqladd;
                        sqlcom.ExecuteNonQuery();
                    }
                }
                else
                {
                    stringsep = Convert.ToString(ViewState["SeatNumbers"]).ToString().Split(',');
                    for (int k = 0; k < stringsep.Length; k++)
                    {
                        sqladd = "update seatArrangement set ";
                        sqladd = sqladd + Convert.ToString(stringsep[k]) + " = Null where TourSerial = " + Convert.ToInt32(ViewState["busserialno"]) + " ";
                        sqlcom.CommandText = sqladd;
                        sqlcom.ExecuteNonQuery();

                        sqlcom.CommandText = "select rowid,BusNo from seatarrangement where TourSerial = " + Convert.ToInt32(ViewState["busserialno"]) + " ";
                        sqlread = sqlcom.ExecuteReader();
                        while ((sqlread.Read()) == true)
                        {
                            SeatArrRowid = Convert.ToInt32(sqlread["Rowid"]);
                            BusNo = Convert.ToInt32(sqlread["BusNo"]);
                        }
                        sqlread.Close();
                    }
                    sqlcom.CommandText = rowidretrieval("CancelBusNo");
                    sqlread = sqlcom.ExecuteReader();
                    while ((sqlread.Read()) == true)
                    {
                        CanBusnoRowid = Convert.ToInt32(sqlread[0]);
                        CanBusno1Rowid = Convert.ToInt32(sqlread[0]);
                    }
                    sqlread.Close();

                    CanBusnoRowid = CanBusnoRowid + 1;
                    sqlcom.CommandText = "insert into CancelBusNo(Rowid,CanNo,BusNo,SeatNo) values(" + CanBusnoRowid + ",'" + CancelCode + "'," + BusNo + ",'" + Convert.ToString(ViewState["SeatNumbers"]) + "')";
                    sqlcom.ExecuteNonQuery();
                    sqladd = "update newkeytable_online set RespectiveRowid=" + CanBusnoRowid + " where KeyType = 'CancelBusno' ";
                    sqlcom.CommandText = sqladd;
                    sqlcom.ExecuteNonQuery();
                }
                //============End=========================   

                Trans_On2line.Commit();*/
                    #endregion
                    #endregion
                    if (val1 == 1)
                    {
                        SuccessSaving = "Success";
                    }
                    else
                    {
                        SuccessSaving = "Failure";
                    }
                }
                catch (Exception ex_trans)
                {
                    //Trans_On2line.Rollback();
                    //Response.Write(ex_trans.ToString());
                    SuccessSaving = "Failure";
                }
                finally
                {
                    /*if (!sqlread.IsClosed)
                    {
                        sqlread.Close();
                    }
                    sqlcon.Close();*/
                }
                return SuccessSaving;
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
                    ldtRecSet = null;
                }
                if (dtComm != null)
                {
                    dtComm.Dispose();
                    dtComm = null;
                }
            }
        }
        private string cancelpartialticket(string ticketid, int tpax, int cpax)
        {
            int lCanAdult = 0, lCanChild = 0;
            string SuccessSaving;

            string[] seatNotCancelled = new string[21];


            int[] BusNoCancelled = new int[16];

            decimal cancelcharges = 0, stvalue = 0, cc = 0, refundamount = 0, canamt = 0;

            if (Convert.ToInt32(canadul.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["AdultFare"]) * Convert.ToDecimal(canadul.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(canadul.Value);
            }
            if (Convert.ToInt32(canchil.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["ChildFare"]) * Convert.ToDecimal(canchil.Value);
                lCanChild = lCanChild + Convert.ToInt32(canchil.Value);
            }
            if (Convert.ToInt32(cantwin.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["Adultstwinfare"]) * Convert.ToDecimal(cantwin.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(cantwin.Value);
            }
            if (Convert.ToInt32(cantrip.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["adultstriplefare"]) * Convert.ToDecimal(cantrip.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(cantrip.Value);
            }
            if (Convert.ToInt32(canwith.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["childwithoutbedfare"]) * Convert.ToDecimal(canwith.Value);
                lCanChild = lCanChild + Convert.ToInt32(canwith.Value);
            }
            if (Convert.ToInt32(cansing.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["singleadultfare"]) * Convert.ToDecimal(cansing.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(cansing.Value);
            }
            if (Convert.ToInt32(candorm.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["dormitoryfare"]) * Convert.ToDecimal(candorm.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(candorm.Value);
            }
            if (Convert.ToInt32(canaduF.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["AWFoodFare"]) * Convert.ToDecimal(canaduF.Value);
                lCanAdult = lCanAdult + Convert.ToInt32(canaduF.Value);
            }
            if (Convert.ToInt32(canchiF.Value) > 0)
            {
                canamt = canamt + Convert.ToDecimal(ViewState["CWFoodFare"]) * Convert.ToDecimal(canchiF.Value);
                lCanChild = lCanChild + Convert.ToInt32(canchiF.Value);
            }
            int tourno = Convert.ToInt32(ViewState["TourNo"]);


            string journeydate11;
            DateTime journeydate;
            string[] dd = txtjdate.Value.Split('/');
            if (dd[0].Length <= 1)
                dd[0] = "0" + dd[0];
            if (dd[1].Length <= 1)
                dd[1] = "0" + dd[1];

            journeydate11 = (dd[1] + "/" + dd[0] + "/" + dd[2]);
            #region Optimize Code
            /* string jdate = "select journeydate from tours where tourno='" + tourno + "' and convert(varchar(10),journeydate,101)='" + journeydate11 + "'";
            journeydate = Convert.ToDateTime(DataLib.GetStringData(DataLib.Connection.ConnectionString, jdate));*/
            #endregion
            //journeydate = Convert.ToDateTime(journeydate11);
            DataTable dtComm = null, ldtRecSet = null;
            try
            {
                pclsObj = new ClsAdo();
                journeydate = Convert.ToDateTime(pclsObj.fnJourneyDate(Convert.ToDateTime(journeydate11), tourno));
                int NoofDaysleft;
                TimeSpan ts = new TimeSpan();
                ts = journeydate.Subtract(DateTime.Now);
                NoofDaysleft = ts.Days * 24 + ts.Hours;
                #region Optimize Code
                /*  string str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays < " + NoofDaysleft + " ORDER BY NoofDays DESC";
            //decimal cancelpercen = Convert.ToDecimal(Convert.ToString(DataLib.GetStringData(DataLib.Connection.ConnectionString, str)));
            DataTable dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            decimal cancelpercen = 0;

            if (dtper.Rows.Count > 0)
            {
            }
            else
            {
                str = "SELECT TOP 1 percentage FROM CancelMaster WHERE tourno = " + tourno + " AND noofdays >= " + NoofDaysleft + " ORDER BY NoofDays";
                dtper = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            }

            if (dtper.Rows.Count > 0)
            {
                cancelpercen = Convert.ToDecimal(dtper.Rows[0]["percentage"]);
            }
            else
            {
                cancelpercen = 0;
            }*/
                #endregion
                decimal cancelpercen = Convert.ToDecimal(canpercentage(tourno, NoofDaysleft, false));
                pclsObj = new ClsAdo();
                Session["CancelCharge"] = Convert.ToString(cancelpercen);
                decimal amm = decimal.Round(canamt);

                // Commented by Santosh 17-May-2012 For Update Tax Detacted in Cancel Ticket
                //stvalue = decimal.Round(Convert.ToDecimal(amm * (Convert.ToDecimal(ViewState["STaxValue"]) / 100)));

                decimal sTaxValue = Convert.ToDecimal(pclsObj.fnGetSTaxForCanTkt(ticketid));
                stvalue = decimal.Round(Convert.ToDecimal(amm * (Convert.ToDecimal(sTaxValue) / 100)));


                cancelcharges = decimal.Round(Convert.ToDecimal((amm + stvalue) * (Convert.ToDecimal(Session["CancelCharge"]) / 100)));

                decimal pServiceCancelPrc = Convert.ToDecimal(canpercentage(tourno, NoofDaysleft, true));
                decimal pADServiceCharges = 0, pCHServiceCharges = 0;
                decimal pServiceChargesTotal = 0;
                decimal pServiceChargesAmount = 0;
                decimal pServiceChargesTax = 0;
                decimal pServiceChargesTaxVal = 0;
                decimal pServiceCancelCharge = 0;
                decimal pServiceChargeRefund = 0;
                if (ViewState["ServiceChargesTotal"] != null)
                {
                    pADServiceCharges = Convert.ToDecimal(ViewState["AdultServiceCharges"]);
                    pCHServiceCharges = Convert.ToDecimal(ViewState["ChildServiceCharges"]);

                    pServiceChargesTotal = Convert.ToDecimal(ViewState["ServiceChargesTotal"]);
                    pServiceChargesTax = Convert.ToDecimal(ViewState["ServiceChargesTax"]);
                    pServiceChargesTaxVal = Convert.ToDecimal(ViewState["ServiceChargesTaxVal"]);
                    pServiceChargesAmount = pServiceChargesTotal - pServiceChargesTaxVal;

                    decimal lAmount = (pADServiceCharges * lCanAdult) + (pCHServiceCharges * lCanChild);

                    decimal lStax = (lAmount * pServiceChargesTax) / 100;

                    pServiceCancelCharge = decimal.Round((lAmount + lStax) * (pServiceCancelPrc / 100));

                }
                cancelcharges = cancelcharges + pServiceCancelCharge;

                if (Convert.ToDecimal(ViewState["cctaxvalue"]) > 0)
                {
                    cc = decimal.Round(Convert.ToDecimal(canamt * (Convert.ToDecimal(ViewState["cctaxvalue"]) / 100)));
                }
                decimal st = stvalue;
                decimal c = 0;
                if (Convert.ToDecimal(ViewState["cctaxvalue"]) > 0)
                {
                    c = cc;
                }
                if (Convert.ToDecimal(ViewState["Advance"]) >= Convert.ToDecimal(ViewState["TotalAmount"]))
                {
                    refundamount = decimal.Round(Convert.ToDecimal(ViewState["TotalAmount"]) - cancelcharges - cc);
                }
                else if (decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges)) > 0)
                    refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - cancelcharges));
                //refundamount = decimal.Round(((Convert.ToDecimal(ViewState["TotalAmount"])) - cancelcharges));

                decimal AvailableBalance = 0;

                int agentid = Convert.ToInt32(ViewState["agentID"]);

                /******** Cancellation Commission ********/
                decimal CancellationCommissionAmount = 0;
                decimal lCalTds = 0;
                /* int? AgentLevel = default(int);
                 pclsObj.Agent_GetAgentLevelbyAgentId(agentid, ref AgentLevel);*/

                dtComm = new DataTable();
                decimal agentCancellationCommission = default(decimal);
                //if (AgentLevel != null)
                //{
                pclsObj = new ClsAdo();
                dtComm = pclsObj.fnAgentCommission("FixedTour Cancel", agentid, tourno);
                if (dtComm != null && dtComm.Rows.Count > 0)
                {
                    agentCancellationCommission = Convert.ToDecimal(dtComm.Rows[0]["Commission"].ToString());
                }
                //}


                decimal lTdsPer = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));


                if (Convert.ToDecimal(ViewState["TotalAmount"]) <= Convert.ToDecimal(ViewState["Advance"]))
                {
                    ViewState["IsPartialPaymentByAgent"] = false;

                    /* CancellationCommissionAmount = (cancelcharges * agentCancellationCommission) / 100;
                     lCalTds = Convert.ToDecimal(CancellationCommissionAmount * (Convert.ToDecimal(lTdsPer) / 100));
                     CancellationCommissionAmount = CancellationCommissionAmount - lCalTds; */
                }
                /******** End ********/

                string balance = "";

                ldtRecSet = pclsObj.fnGetAgent_AvailableBalance(agentid);

                if (ldtRecSet.Rows.Count > 0)
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
                #region Optimize Code
                /*if ((agentid != 0) && (agentid > 0))
            {
                string qry, qry1;
                qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + agentid;
                string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                if (strRowNo == "" || strRowNo == null)
                {
                }
                else
                {
                    qry1 = "select isnull(availablebalance,0) as availablebalance from OnlineTransactionTable(nolock) where agentid=" + agentid + " and rowid=" + strRowNo;
                    string balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
                    if (balance == "")
                        Session["Balance"] = "0";
                    else
                        Session["Balance"] = balance;
                }
            }*/
                #endregion
                if (Convert.ToString(Session["Balance"]) != "")
                {
                    AvailableBalance = Convert.ToDecimal(Session["Balance"]) + CancellationCommissionAmount;
                }
                #region Optimize Code
                /*SqlCommand sqlcom = new SqlCommand();
            sqlcom.CommandType = CommandType.Text;
            SqlDataReader sqlread=null;
            SqlConnection sqlcon = DataLib.GetConnection();
            SqlTransaction Trans_On2line = sqlcon.BeginTransaction(IsolationLevel.RepeatableRead);
            sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.CommandType = CommandType.Text;
            sqlcom.Transaction = Trans_On2line;*/
                #endregion
                try
                {
                    //updating the TicketDetails table by setting the cancellation field as Y 
                    #region Optimize Code
                    /*sqlcom.CommandText = "update TicketDetails set Cancelled='Y' where TicketNo='" + ticketid.Trim() + "';SELECT     RowId FROM         TicketDetails WHERE     (TicketNo = '" + ticketid.Trim() + "')";
                string rowId = Convert.ToString(sqlcom.ExecuteScalar());
                sqlcom.CommandText = "Select NumericValue,RespectiveRowid from NewKeyTable_online where keyType='Cancel'";
                sqlread = sqlcom.ExecuteReader();
                while ((sqlread.Read()) == true)
                {
                    CancelNo = Convert.ToInt32(sqlread["NumericValue"]);
                    CancelRowid = Convert.ToInt32(sqlread["RespectiveRowid"]);
                    Cancel1Rowid = Convert.ToInt32(sqlread["RespectiveRowid"]);
                }
                sqlread.Close();*/

                    /*DataTable dtcan = pclsObj.fnNewKeyTableID("Cancel");
                    if (dtcan.Rows.Count > 0)
                    {
                        CancelNo = Convert.ToInt32(dtcan.Rows[0]["RespectiveRowid"]);
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
                    CancelCode = "CAN" + CancelCode + CancelString;*/
                    #endregion
                    //inserting all the fields in the cancel table.  
                    decimal lAgentCredit = 0, lAgentDebit = 0, lCredit = 0, lDebit = 0, lAvailableBalance = 0;
                    lAvailableBalance = decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["TotalAmount"]/*ViewState["AgentDebit"]*/) - (Convert.ToDecimal(ViewState["AgentCredit"])) /*- cancelcharges*/));
                    lAgentCredit = decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommissionAmount);
                    lAgentDebit = decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges));
                    lCredit = decimal.Round(Convert.ToDecimal(ViewState["AgentCredit"]) + (cancelcharges));
                    lDebit = decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges + CancellationCommissionAmount);
                    decimal pTotCanCharges = 0;
                    if (Convert.ToBoolean(ViewState["IsPartialPaymentByAgent"]))
                    {
                        int? lHierarchyCounter = 0;
                        DataTable ldtHierarchyCanChr = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(ticketid.Trim()),
                           ref lHierarchyCounter);

                        if (ldtHierarchyCanChr.Rows.Count > 0)
                        {
                            pTotCanCharges = decimal.Round(Convert.ToDecimal(ldtHierarchyCanChr.Compute("Sum(TktCanCharge)", "RowID >=0").ToString()));
                        }
                        lAvailableBalance = decimal.Round((AvailableBalance + Convert.ToDecimal(ViewState["Advance"])) /*- (cancelcharges) + pTotCanCharges*/);
                        lAgentCredit = Convert.ToDecimal(ViewState["Advance"]) + CancellationCommissionAmount; ;
                        lAgentDebit = (0/*cancelcharges + pTotCanCharges*/);
                        lDebit = Convert.ToDecimal(ViewState["Advance"]) + CancellationCommissionAmount;
                        lCredit = (0/*cancelcharges + pTotCanCharges*/);

                        decimal lNewTotalAmt = 0, lNewAdvanceAmt = 0;

                        CheckTotalWithAdvace(tourno, journeydate, Convert.ToBoolean(ViewState["IsPartialPaymentByAgent"]), refundamount, ref lNewTotalAmt, ref lNewAdvanceAmt);

                        if (Convert.ToDecimal(lNewTotalAmt) <= Convert.ToDecimal(lNewAdvanceAmt))
                        {
                            lAvailableBalance = decimal.Round((AvailableBalance + Convert.ToDecimal(ViewState["Advance"])) - (/*cancelcharges +*/ pTotCanCharges)/**/);
                            lAgentCredit = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (/*cancelcharges +*/ pTotCanCharges)) + CancellationCommissionAmount);
                            lAgentDebit = (/*cancelcharges+*/ pTotCanCharges);
                            lDebit = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (/*cancelcharges +*/ pTotCanCharges)) + CancellationCommissionAmount);
                            lCredit = (/*cancelcharges +*/ pTotCanCharges);
                            refundamount = decimal.Round((Convert.ToDecimal(ViewState["Advance"]) - (/*cancelcharges +*/ pTotCanCharges)));
                        }
                    }
                    int val1 = pclsObj.fnUpdateFixedTourCancelInfo(decimal.Round(cancelcharges), decimal.Round(refundamount), ticketid.Trim(), Convert.ToInt32(cpax), Convert.ToString(Session["LocalBranch"]), agentid,
                           lAgentCredit, lAvailableBalance, lAgentDebit, decimal.Round(Convert.ToDecimal(ViewState["Amount"])),
                            decimal.Round(Convert.ToDecimal(CancellationCommissionAmount)), Convert.ToString(ViewState["BranchCode"]), Convert.ToString(ViewState["PaymentMode"]),
                           lDebit, lCredit, Convert.ToString(ViewState["UserName"]), Convert.ToString(ViewState["BranchCode"]), stvalue, decimal.Round(Convert.ToDecimal(lCalTds)), true,
                           "", 0, '0', pServiceCancelCharge, "", 0, false, null, null);

                    #region Optimize Code
                    /*sqladd = "insert into Cancel(RowId,CanNo,TicketNo,CanNoTick,CanCharges,RefundAmt,UserName,BranchCode)values(" + CancelRowid + ",'" + CancelCode + "','" + ticketid.Trim() + "'," + cpax + "," + cancelcharges + "," + refundamount + ",'" + Session["AgentId"] + "','" + Convert.ToString(Session["LocalBranch"]) + "')";            
                sqlcom.CommandText = sqladd;
                sqlcom.ExecuteNonQuery();

                //Updating the NewKeyTable_Online with the new Cancel Number and Rowid 
                sqladd = "update newkeytable_online set NumericValue = " + CancelNo + " , RespectiveRowid=" + CancelRowid + " where KeyType = 'Cancel' ";
                sqlcom.CommandText = sqladd;
                sqlcom.ExecuteNonQuery();
                sqlcom.CommandText = rowidretrieval("CanName");
                sqlcom.CommandTimeout = 500000;
                sqlread = sqlcom.ExecuteReader();
                while ((sqlread.Read()) == true)
                {
                    CancelNameRowid = Convert.ToInt32(sqlread[0]);
                    CancelName1Rowid = Convert.ToInt32(sqlread[0]);
                }
                sqlread.Close();
                string str2 = "select * from ticketnames where ticketno='" + ticketid + "'";
                DataTable dtTour = new DataTable();
                dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str2);
                count = 0;
                for (i = 0; i <= dtTour.Rows.Count - 1; i++)
                {
                    count = count + 1;
                    CancelNameRowid = CancelNameRowid + 1;
                    sqlcom.CommandText = "insert into CanName(Rowid,Canno,Name,Age,Sex)values(" + CancelNameRowid + ",'" + CancelCode + "','" + Convert.ToString(dtTour.Rows[i]["Name"]) + "','" + Convert.ToInt32(dtTour.Rows[i]["Age"]) + "','" + Convert.ToString(dtTour.Rows[i]["Sex"]) + "')";
                    sqlcom.ExecuteNonQuery();
                    count = count + 3;
                }
                sqladd = "update newkeytable_online set RespectiveRowid=" + CancelNameRowid + " where KeyType = 'CanName' ";
                sqlcom.CommandText = sqladd;
                sqlcom.ExecuteNonQuery();

                if ((agentid != 0) && (agentid > 0))
                {
                    string strid = "select transactiontypeid from transactiontypemaster where transactionname='BusTicketCancellation' and IsAgentService='Y'";
                    string transtype = DataLib.GetStringData(DataLib.Connection.ConnectionString, strid);
                    decimal acre = (Convert.ToDecimal(ViewState["AgentCredit"]) / Convert.ToDecimal(tpax));
                    if (Convert.ToString(ViewState["PaymentMode"]) == "AgentCash")
                    {
                        sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType," + 
                            "AgentCredit," + 
                            "AvailableBalance,AgentDebit,TicketAmount,Commission,PaymentMode,Number,BankName," + 
                            "Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode," + 
                            "Cashier,TransState,Status,servicetax,TDS)values(" + 
                            agentid + ",'" + CancelCode + "'," + Convert.ToInt32(transtype) + "," + 
                            decimal.Round(refundamount - (Convert.ToDecimal(ViewState["AgentCredit"]))) + "," + 
                            decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"])) - 
                            (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges) + "," + 
                            decimal.Round(cancelcharges) + "," + 
                            decimal.Round(Convert.ToDecimal(ViewState["Amount"])) + "," + 
                            decimal.Round((Convert.ToDecimal(ViewState["AgentCredit"]) / Convert.ToDecimal(tpax)) * Convert.ToDecimal(cpax)) + ",'" + 
                            Convert.ToString(ViewState["PaymentMode"]) + "','',''," + 
                            decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - 
                            cancelcharges - st) + "," + 
                            decimal.Round((Convert.ToDecimal(ViewState["AgentCredit"]) / Convert.ToDecimal(tpax) * Convert.ToDecimal(cpax)) + 
                            cancelcharges) + ",'','" + 
                            ViewState["UserName"] + "','" + ViewState["BranchCode"] + "','','','','P','S'," + st + "," + 
                            decimal.Round(Convert.ToDecimal((Convert.ToDecimal(ViewState["TDS"]) / Convert.ToDecimal(tpax)) * Convert.ToDecimal(cpax))) + ")";
                    }
                    else
                    {
                        sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,
                 * Commission,PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,
                 * Cashier,TransState,Status,servicetax,TDS)values(" + agentid + ",'" + CancelCode + "'," + Convert.ToInt32(transtype) + ",
                 * " + decimal.Round(refundamount - (Convert.ToDecimal(ViewState["AgentCredit"]))) + "
                 * ," + decimal.Round(AvailableBalance + (Convert.ToDecimal(ViewState["AgentDebit"])) - (Convert.ToDecimal(ViewState["AgentCredit"])) - 
                 * cancelcharges) + "," + decimal.Round(cancelcharges) + "," + decimal.Round(Convert.ToDecimal(ViewState["Amount"])) + ",
                 * " + decimal.Round((Convert.ToDecimal(ViewState["AgentCredit"]) / Convert.ToDecimal(tpax)) * Convert.ToDecimal(cpax)) + ",
                 * '" + Convert.ToString(ViewState["PaymentMode"]) + "','','',
                 * " + decimal.Round(Convert.ToDecimal(ViewState["AgentDebit"]) - (Convert.ToDecimal(ViewState["AgentCredit"])) - cancelcharges - st) + ",
                 * " + decimal.Round((Convert.ToDecimal(ViewState["AgentCredit"]) / Convert.ToDecimal(tpax) * Convert.ToDecimal(cpax)) + cancelcharges) + ",
                 * '','" + ViewState["UserName"] + "','" + ViewState["BranchCode"] + "','','','','P','S'," + st + ",
                 * " + decimal.Round(Convert.ToDecimal((Convert.ToDecimal(ViewState["TDS"]) / Convert.ToDecimal(tpax)) * Convert.ToDecimal(cpax))) + ")";
                    }
                }

                sqlcom.CommandText = sqladd;
                sqlcom.ExecuteNonQuery();*/
                    #endregion
                    //----------------------XXXXXXXXXXXXXXX-----------------------------------------//

                    //================Start=======================
                    #region Optimize Code
                    /* string multiple = "select seatno from onlinetoursbooking where ticketcode='" + ticketid + "'";
                string sssss = DataLib.GetStringData(DataLib.Connection.ConnectionString, multiple);
                string[] str1 = sssss.Split(',');
                busserialno = Convert.ToString(ViewState["busserialno"]);
                string[] ser = busserialno.Split(',');
                string seat = "";
                int Bbusno = 0;
                if (ser.Length > 1)
                {
                    for (int k = 0; k < ser.Length; k++)
                    {
                        seat = "";
                        for (int ii = 0; ii < str1.Length; ii++)
                        {
                            if (str1[ii].Length > 3)
                            {
                                if (str1[ii].Substring(1, 1) == Convert.ToString(k + 1))
                                {
                                    //tblseatfld[ii] = str1[ii].Substring(2, 2);
                                    if (seat == "")
                                    {
                                        seat = str1[ii].Substring(2, 2);
                                    }
                                    else
                                    {
                                        seat = seat + "," + str1[ii].Substring(2, 2);
                                    }
                                }
                            }
                            else
                            {
                                if (str1[ii].Substring(1, 1) == Convert.ToString(k + 1))
                                {
                                    //tblseatfld[ii] = str1[ii].Substring(2, 1);
                                    if (seat == "")
                                    {
                                        seat = str1[ii].Substring(2, 1);
                                    }
                                    else
                                    {
                                        seat = seat + "," + str1[ii].Substring(2, 1);
                                    }
                                }
                            }
                        }
                        seat = seat.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                        string[] sd = seat.Split(',');
                        string tbnseat = "";
                        for (i = 0; i < sd.Length; i++)
                        {
                            string updatestring = "UPDATE SeatArrangement set s" + sd[i] + " = Null where TourSerial = " + ser[k] + " ";
                            sqlcom.CommandText = updatestring;
                            sqlcom.ExecuteNonQuery();
                            if (tbnseat == "")
                            {
                                tbnseat = "s" + sd[i];
                            }
                            else
                            {
                                tbnseat = tbnseat + "," + "s" + sd[i];
                            }
                        }
                        int seatArrangeRowid = 0;
                        sqlcom.CommandText = "Select * from SeatArrangement where TourSerial = " + ser[k] + "";
                        sqlread = sqlcom.ExecuteReader();
                        while (sqlread.Read())
                        {
                            Bbusno = Convert.ToInt32(sqlread["BusNo"]);
                            seatArrangeRowid = Convert.ToInt32(sqlread[0].ToString());
                        }
                        sqlread.Close();

                        sqlcom.CommandText = rowidretrieval("CancelBusNo");
                        sqlread = sqlcom.ExecuteReader();
                        while ((sqlread.Read()) == true)
                        {
                            CanBusnoRowid = Convert.ToInt32(sqlread[0]);
                            CanBusno1Rowid = Convert.ToInt32(sqlread[0]);
                        }
                        sqlread.Close();

                        CanBusnoRowid = CanBusnoRowid + 1;
                        sqlcom.CommandText = "insert into CancelBusNo(Rowid,CanNo,BusNo,SeatNo) values(" + CanBusnoRowid + ",'" + CancelCode + "'," + Bbusno + ",'" + tbnseat + "')";
                        sqlcom.ExecuteNonQuery();
                        sqladd = "update newkeytable_online set RespectiveRowid=" + CanBusnoRowid + " where KeyType = 'CancelBusno' ";
                        sqlcom.CommandText = sqladd;
                        sqlcom.ExecuteNonQuery();
                    }
                }
                else
                {
                    stringsep = Convert.ToString(ViewState["SeatNumbers"]).ToString().Split(',');
                    for (int k = 0; k < stringsep.Length; k++)
                    {
                        sqladd = "update seatArrangement set ";
                        sqladd = sqladd + Convert.ToString(stringsep[k]) + " = Null where TourSerial = " + Convert.ToInt32(ViewState["busserialno"]) + " ";
                        sqlcom.CommandText = sqladd;
                        sqlcom.ExecuteNonQuery();

                        sqlcom.CommandText = "select rowid,BusNo from seatarrangement where TourSerial = " + Convert.ToInt32(ViewState["busserialno"]) + " ";
                        sqlread = sqlcom.ExecuteReader();
                        while ((sqlread.Read()) == true)
                        {
                            SeatArrRowid = Convert.ToInt32(sqlread["Rowid"]);
                            BusNo = Convert.ToInt32(sqlread["BusNo"]);
                        }
                        sqlread.Close();
                    }
                    sqlcom.CommandText = rowidretrieval("CancelBusNo");
                    sqlread = sqlcom.ExecuteReader();
                    while ((sqlread.Read()) == true)
                    {
                        CanBusnoRowid = Convert.ToInt32(sqlread[0]);
                        CanBusno1Rowid = Convert.ToInt32(sqlread[0]);
                    }
                    sqlread.Close();

                    CanBusnoRowid = CanBusnoRowid + 1;
                    sqlcom.CommandText = "insert into CancelBusNo(Rowid,CanNo,BusNo,SeatNo) values(" + CanBusnoRowid + ",'" + CancelCode + "'," + BusNo + ",'" + Convert.ToString(ViewState["SeatNumbers"]) + "')";
                    sqlcom.ExecuteNonQuery();
                    sqladd = "update newkeytable_online set RespectiveRowid=" + CanBusnoRowid + " where KeyType = 'CancelBusno' ";
                    sqlcom.CommandText = sqladd;
                    sqlcom.ExecuteNonQuery();
                }
                //=================End========================

                ////// CancelBusno----CancelBusNo,ChangeTableInsert,NewKeyTable_Online 
                Trans_On2line.Commit();*/
                    #endregion
                    // int val2 = pclsObj.fnAgentUblockSeatCancelTKT(ticketid, Convert.ToString(ViewState["busserialno"]), CancelCode); 

                    if (val1 == 1)
                    {
                        SuccessSaving = "Success";
                        // orderid = DataLib.pnr();

                        ViewState["cancelcharges"] = cancelcharges;
                        insertbook(tourno, (Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(canc.Value)), journeydate, ref orderid, refundamount,
                            Convert.ToBoolean(ViewState["IsPartialPaymentByAgent"]));
                        Session["orderid"] = orderid;

                    }
                    else
                    {
                        SuccessSaving = "Failure";
                    }
                    return SuccessSaving;
                }
                catch (Exception ex_trans)
                {

                    SuccessSaving = "Failure";
                    return SuccessSaving;
                }
                finally
                {

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
                    ldtRecSet = null;
                }
                if (dtComm != null)
                {
                    dtComm.Dispose();
                    dtComm = null;
                }
            }
            //------------------- Cancellation was Completed Now we have to Book the New Ticket For the Remaining Persons----//



        }
        private void SendMail(string To, string From, string Bcc, string subject, string body)
        {
            try
            {

                MailMessage mObj = new MailMessage();
                mObj.To = To;
                mObj.Bcc = Bcc;
                mObj.Cc = "info@southerntravels.in";
                mObj.From = From;
                mObj.Subject = subject;
                mObj.BodyFormat = MailFormat.Html;
                mObj.Body = body;
                SmtpMail.Send(mObj);
            }
            catch (Exception ex)
            {
                Response.Write("<!-- " + ex.ToString() + " -->");
            }
            finally
            {

            }
        }
        /// <summary>
        /// Not use
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="tourid"></param>
        /// <param name="bustype"></param>
        public void getfaregrid(DateTime jd, Int32 tourid, string bustype)
        {
            DataTable date;
            DataTable cfare;
            DataTable rfare;
            if ((bustype == "Y") || (bustype == "y"))
            {
                RadAC.Value = "1";
                RadNAC.Value = "0";
            }
            else
            {
                RadAC.Value = "0";
                RadNAC.Value = "1";
            }
            lblA2ACfare.Value = "0";
            lblA2NACfare.Value = "0";
            lblA3ACfare.Value = "0";
            lblA3NACfare.Value = "0";
            lblAACfare.Value = "0";
            lblANACfare.Value = "0";
            lblCACfare.Value = "0";
            lblCBACfare.Value = "0";
            lblCBNACfare.Value = "0";
            lblCNACfare.Value = "0";
            lblSACfare.Value = "0";
            lblSNACfare.Value = "0";
            lblDACfare.Value = "0";
            lblDNACfare.Value = "0";

            date = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select * from revisedtourfare where tourno=" + tourid + " and isaffected='Y'");
            if (date.Rows.Count > 0)
            {
                if (jd.Date != null)
                {
                    if (Convert.ToDateTime(date.Rows[0]["Affectedfrom"].ToString()) <= jd.Date)
                    {
                        rfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select a.tourid,a.acfare,a.nonacfare,b.Rowid,b.category,b.isaccommodation from tbl_revisedfaremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");
                        if (rfare.Rows.Count > 0)
                        {
                            for (int i = 0; i < rfare.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblAACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblANACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2ACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3ACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                                {
                                    this.lblDACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }

                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2NACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3NACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                                {
                                    this.lblDNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                            }
                            if (((this.lblAACfare.Value == "0") || (this.lblAACfare.Value == "")) && ((this.lblANACfare.Value == "0") || (this.lblANACfare.Value == "")))
                            {
                                trAC.Value = "0";
                                traf.Visible = false;
                                tr7.Visible = false;
                            }
                            if (((this.lblCACfare.Value == "0") || (this.lblCACfare.Value == "")) && ((this.lblCNACfare.Value == "0") || (this.lblCNACfare.Value == "")))
                            {
                                trNONAC.Value = "0";
                                trcf.Visible = false;
                                tr8.Visible = false;
                            }
                            if (((this.lblA2ACfare.Value == "0") || (this.lblA2ACfare.Value == "")) && ((this.lblA2NACfare.Value == "0") || (this.lblA2NACfare.Value == "")))
                            {
                                tr2AC.Value = "0";
                                tra2f.Visible = false;
                                tr9.Visible = false;
                            }
                            if (((this.lblA3ACfare.Value == "0") || (this.lblA3ACfare.Value == "")) && ((this.lblA3NACfare.Value == "0") || (this.lblA3NACfare.Value == "")))
                            {
                                tr3AC.Value = "0";
                                tra3f.Visible = false;
                                tr10.Visible = false;
                            }
                            if (((this.lblCBACfare.Value == "0") || (this.lblCBACfare.Value == "")) && ((this.lblCBNACfare.Value == "0") || (this.lblCBNACfare.Value == "")))
                            {
                                trcbAC.Value = "0";
                                trcbf.Visible = false;
                                tr11.Visible = false;
                            }
                            if (((this.lblSACfare.Value == "0") || (this.lblSACfare.Value == "")) && ((this.lblSNACfare.Value == "0") || (this.lblSNACfare.Value == "")))
                            {
                                trsAC.Value = "0";
                                trsf.Visible = false;
                            }
                            if (((this.lblDACfare.Value == "0") || (this.lblDACfare.Value == "")) && ((this.lblDNACfare.Value == "0") || (this.lblDNACfare.Value == "")))
                            {
                                trDAC.Value = "0";
                                tradf.Visible = false;
                                tr2.Visible = false;
                            }
                        }
                        else
                        {
                            traf.Visible = false;
                            tr7.Visible = false;
                            trcf.Visible = false;
                            tr8.Visible = false;
                            tra2f.Visible = false;
                            tr9.Visible = false;
                            tra3f.Visible = false;
                            tr10.Visible = false;
                            trcbf.Visible = false;
                            tr11.Visible = false;
                            trsf.Visible = false;
                            tradf.Visible = false;
                            tr2.Visible = false;
                        }
                    }
                    else
                    {
                        cfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select b.category,b.Rowid,a.acfare,a.nonacfare from tbl_faremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");
                        if (cfare.Rows.Count > 0)
                        {
                            for (int i = 0; i < cfare.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblAACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblANACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2ACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3ACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                {
                                    this.lblDACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2NACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3NACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                {
                                    this.lblDNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                }
                            }
                            if (((this.lblAACfare.Value == "0") || (this.lblAACfare.Value == "")) && ((this.lblANACfare.Value == "0") || (this.lblANACfare.Value == "")))
                            {
                                trAC.Value = "0";
                                traf.Visible = false;
                                tr7.Visible = false;
                            }
                            if (((this.lblCACfare.Value == "0") || (this.lblCACfare.Value == "")) && ((this.lblCNACfare.Value == "0") || (this.lblCNACfare.Value == "")))
                            {
                                trNONAC.Value = "0";
                                trcf.Visible = false;
                                tr8.Visible = false;
                            }
                            if (((this.lblA2ACfare.Value == "0") || (this.lblA2ACfare.Value == "")) && ((this.lblA2NACfare.Value == "0") || (this.lblA2NACfare.Value == "")))
                            {
                                tr2AC.Value = "0";
                                tra2f.Visible = false;
                                tr9.Visible = false;
                            }
                            if (((this.lblA3ACfare.Value == "0") || (this.lblA3ACfare.Value == "")) && ((this.lblA3NACfare.Value == "0") || (this.lblA3NACfare.Value == "")))
                            {
                                tr3AC.Value = "0";
                                tra3f.Visible = false;
                                tr10.Visible = false;
                            }
                            if (((this.lblCBACfare.Value == "0") || (this.lblCBACfare.Value == "")) && ((this.lblCBNACfare.Value == "0") || (this.lblCBNACfare.Value == "")))
                            {
                                trcbAC.Value = "0";
                                trcbf.Visible = false;
                                tr11.Visible = false;
                            }
                            if (((this.lblSACfare.Value == "0") || (this.lblSACfare.Value == "")) && ((this.lblSNACfare.Value == "0") || (this.lblSNACfare.Value == "")))
                            {
                                trsAC.Value = "0";
                                trsf.Visible = false;
                            }
                            if (((this.lblDACfare.Value == "0") || (this.lblDACfare.Value == "")) && ((this.lblDNACfare.Value == "0") || (this.lblDNACfare.Value == "")))
                            {
                                trDAC.Value = "0";
                                tradf.Visible = false;
                                tr2.Visible = false;
                            }
                        }
                        else
                        {
                            traf.Visible = false;
                            tr7.Visible = false;
                            trcf.Visible = false;
                            tr8.Visible = false;
                            tra2f.Visible = false;
                            tr9.Visible = false;
                            tra3f.Visible = false;
                            tr10.Visible = false;
                            trcbf.Visible = false;
                            tr11.Visible = false;
                            trsf.Visible = false;
                            tradf.Visible = false;
                            tr2.Visible = false;
                        }
                    }
                }
            }
            else
            {
                cfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select b.category,b.Rowid,a.acfare,a.nonacfare from tbl_faremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");
                if (cfare.Rows.Count > 0)
                {
                    for (int i = 0; i < cfare.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                        {
                            this.lblAACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                        {
                            this.lblCACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                        {
                            this.lblANACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                        {
                            this.lblCNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                        {
                            this.lblA2ACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                        {
                            this.lblA3ACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                        {
                            this.lblCBACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                        {
                            this.lblSACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                        {
                            this.lblDACfare.Value = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                        {
                            this.lblA2NACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                        {
                            this.lblA3NACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                        {
                            this.lblCBNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                        {
                            this.lblSNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                        {
                            this.lblDNACfare.Value = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                        }
                    }
                    if (((this.lblAACfare.Value == "0") || (this.lblAACfare.Value == "")) && ((this.lblANACfare.Value == "0") || (this.lblANACfare.Value == "")))
                    {

                        trAC.Value = "0";
                        traf.Visible = false;
                        tr7.Visible = false;
                    }
                    if (((this.lblCACfare.Value == "0") || (this.lblCACfare.Value == "")) && ((this.lblCNACfare.Value == "0") || (this.lblCNACfare.Value == "")))
                    {
                        trNONAC.Value = "0";
                        trcf.Visible = false;
                        tr8.Visible = false;
                    }
                    if (((this.lblA2ACfare.Value == "0") || (this.lblA2ACfare.Value == "")) && ((this.lblA2NACfare.Value == "0") || (this.lblA2NACfare.Value == "")))
                    {
                        tr2AC.Value = "0";
                        //tr4.Visible = false;
                        tra2f.Visible = false;
                        tr9.Visible = false;
                    }
                    if (((this.lblA3ACfare.Value == "0") || (this.lblA3ACfare.Value == "")) && ((this.lblA3NACfare.Value == "0") || (this.lblA3NACfare.Value == "")))
                    {
                        tr3AC.Value = "0";
                        //tr5.Visible = false;
                        tra3f.Visible = false;
                        tr10.Visible = false;
                    }
                    if (((this.lblCBACfare.Value == "0") || (this.lblCBACfare.Value == "")) && ((this.lblCBNACfare.Value == "0") || (this.lblCBNACfare.Value == "")))
                    {
                        trcbAC.Value = "0";
                        //tr6.Visible = false;
                        trcbf.Visible = false;
                        tr11.Visible = false;
                    }
                    if (((this.lblSACfare.Value == "0") || (this.lblSACfare.Value == "")) && ((this.lblSNACfare.Value == "0") || (this.lblSNACfare.Value == "")))
                    {
                        trsAC.Value = "0";
                        trsf.Visible = false;
                    }
                    if (((this.lblDACfare.Value == "0") || (this.lblDACfare.Value == "")) && ((this.lblDNACfare.Value == "0") || (this.lblDNACfare.Value == "")))
                    {
                        trDAC.Value = "0";
                        tradf.Visible = false;
                        tr2.Visible = false;
                    }
                }
                else
                {
                    traf.Visible = false;
                    tr7.Visible = false;
                    trcf.Visible = false;
                    tr8.Visible = false;
                    tra2f.Visible = false;
                    tr9.Visible = false;
                    tra3f.Visible = false;
                    tr10.Visible = false;
                    trcbf.Visible = false;
                    tr11.Visible = false;
                    trsf.Visible = false;
                    tradf.Visible = false;
                    tr2.Visible = false;
                }
            }
        }
        public void GetTourFare(DateTime jd, Int32 tourid, string bustype)
        {
            DataTable date = null;
            DataTable cfare = null;
            DataTable rfare = null;
            ClsAdo clsObj = null;
            try
            {
                char lIsLTC = 'N';
                if ((bustype == "Y") || (bustype == "y"))
                {
                    RadAC.Value = "1";
                    RadNAC.Value = "0";
                }
                else
                {
                    RadAC.Value = "0";
                    RadNAC.Value = "1";
                }
                lblA2ACfare.Value = "0";
                lblA2NACfare.Value = "0";
                lblA3ACfare.Value = "0";
                lblA3NACfare.Value = "0";
                lblAACfare.Value = "0";
                lblANACfare.Value = "0";
                lblCACfare.Value = "0";
                lblCBACfare.Value = "0";
                lblCBNACfare.Value = "0";
                lblCNACfare.Value = "0";
                lblSACfare.Value = "0";
                lblSNACfare.Value = "0";
                lblDACfare.Value = "0";
                lblDNACfare.Value = "0";
                lblAWFACfare.Value = "0";
                lblAWFNACfare.Value = "0";
                clsObj = new ClsAdo();
                rfare = clsObj.fnGetFixedTour_Fare(tourid, jd, lIsLTC, "Agent").Tables[0];
                if (rfare.Rows.Count > 0)
                {
                    for (int i = 0; i < rfare.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                        {
                            this.lblAACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                        {
                            this.lblCACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }

                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                        {
                            this.lblANACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                        {
                            this.lblCNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 8)
                        {
                            this.lblAWFACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 9)
                        {
                            this.lblCWFACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 8)
                        {
                            this.lblAWFNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 9)
                        {
                            this.lblCWFNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                        {
                            this.lblA2ACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                        {
                            this.lblA3ACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                        {
                            this.lblCBACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                        {
                            this.lblSACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                        {
                            this.lblDACfare.Value = Convert.ToString(rfare.Rows[i]["ACFare"]);
                        }

                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                        {
                            this.lblA2NACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                        {
                            this.lblA3NACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                        {
                            this.lblCBNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                        {
                            this.lblSNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                        if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                        {
                            this.lblDNACfare.Value = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                        }
                    }
                    if (((this.lblAACfare.Value == "0") || (this.lblAACfare.Value == "")) && ((this.lblANACfare.Value == "0") || (this.lblANACfare.Value == "")))
                    {
                        trAC.Value = "0";
                        traf.Visible = false;
                        tr7.Visible = false;
                    }
                    if (((this.lblCACfare.Value == "0") || (this.lblCACfare.Value == "")) && ((this.lblCNACfare.Value == "0") || (this.lblCNACfare.Value == "")))
                    {
                        trNONAC.Value = "0";
                        trcf.Visible = false;
                        tr8.Visible = false;
                    }
                    if (((this.lblAWFACfare.Value == "0") || (this.lblAWFACfare.Value == "")) &&
                           ((this.lblAWFNACfare.Value == "0") || (this.lblAWFNACfare.Value == "")))
                    {
                        //trAC.Value = "0";
                        trAWF.Visible = false;
                        trAWFColor.Visible = false;
                    }
                    if (((this.lblCWFACfare.Value == "0") || (this.lblCWFACfare.Value == "")) &&
                        ((this.lblCWFNACfare.Value == "0") || (this.lblCWFNACfare.Value == "")))
                    {
                        //trNONAC.Value = "0";
                        trCWF.Visible = false;
                        trCWFColor.Visible = false;
                    }
                    if (((this.lblA2ACfare.Value == "0") || (this.lblA2ACfare.Value == "")) && ((this.lblA2NACfare.Value == "0") || (this.lblA2NACfare.Value == "")))
                    {
                        tr2AC.Value = "0";
                        tra2f.Visible = false;
                        tr9.Visible = false;
                    }
                    if (((this.lblA3ACfare.Value == "0") || (this.lblA3ACfare.Value == "")) && ((this.lblA3NACfare.Value == "0") || (this.lblA3NACfare.Value == "")))
                    {
                        tr3AC.Value = "0";
                        tra3f.Visible = false;
                        tr10.Visible = false;
                    }
                    if (((this.lblCBACfare.Value == "0") || (this.lblCBACfare.Value == "")) && ((this.lblCBNACfare.Value == "0") || (this.lblCBNACfare.Value == "")))
                    {
                        trcbAC.Value = "0";
                        trcbf.Visible = false;
                        tr11.Visible = false;
                    }
                    if (((this.lblSACfare.Value == "0") || (this.lblSACfare.Value == "")) && ((this.lblSNACfare.Value == "0") || (this.lblSNACfare.Value == "")))
                    {
                        trsAC.Value = "0";
                        trsf.Visible = false;
                    }
                    if (((this.lblDACfare.Value == "0") || (this.lblDACfare.Value == "")) && ((this.lblDNACfare.Value == "0") || (this.lblDNACfare.Value == "")))
                    {
                        trDAC.Value = "0";
                        tradf.Visible = false;
                        tr2.Visible = false;
                    }
                    if (RadAC.Value == "1")
                    {
                        lblFareAdults.Text = lblAACfare.Value;
                        lblfareChild.Text = lblCACfare.Value;
                        lblFareAdultsTwin.Text = lblA2ACfare.Value;
                        lblFareAdultsTriple.Text = lblA3ACfare.Value;
                        lblFareChildBed.Text = lblCBACfare.Value;
                        lblFareSingles.Text = lblSACfare.Value;
                        lblFareAdultsDorm.Text = lblDACfare.Value;
                        lblAdulWFoodfare.Text = lblAWFACfare.Value;
                        lblChildWFoodfare.Text = lblCWFACfare.Value;
                    }
                    else
                    {
                        lblFareAdults.Text = lblANACfare.Value;
                        lblfareChild.Text = lblCNACfare.Value;
                        lblFareAdultsTwin.Text = lblA2NACfare.Value;
                        lblFareAdultsTriple.Text = lblA3NACfare.Value;
                        lblFareChildBed.Text = lblCBNACfare.Value;
                        lblFareSingles.Text = lblSNACfare.Value;
                        lblFareAdultsDorm.Text = lblDNACfare.Value;
                        lblAdulWFoodfare.Text = lblAWFNACfare.Value;
                        lblChildWFoodfare.Text = lblCWFNACfare.Value;
                    }
                }
                else
                {
                    traf.Visible = false;
                    tr7.Visible = false;
                    trcf.Visible = false;
                    tr8.Visible = false;
                    tra2f.Visible = false;
                    tr9.Visible = false;
                    tra3f.Visible = false;
                    tr10.Visible = false;
                    trcbf.Visible = false;
                    tr11.Visible = false;
                    trsf.Visible = false;
                    tradf.Visible = false;
                    tr2.Visible = false;
                    trCWF.Visible = false;
                    trCWFColor.Visible = false;
                    trAWF.Visible = false;
                    trAWFColor.Visible = false;
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (date != null)
                {
                    date.Dispose();
                    date = null;
                }
                if (cfare != null)
                {
                    cfare.Dispose();
                    cfare = null;
                }
                if (rfare != null)
                {
                    rfare.Dispose();
                    rfare = null;
                }
            }
        }

        private void CheckTotalWithAdvace(int tourno, System.DateTime jdate, bool lIsPartialPaymentByAgent, decimal refundamt, ref decimal lNewTotalAmt, ref decimal lNewAdvanceAmt)
        {
            int afare = 0, cfare = 0, a2fare = 0, a3fare = 0, cbfare = 0, safare = 0, dfare = 0;
            decimal AdWFoodfare = 0;
            decimal CWFoodfare = 0;
            string doj;
            doj = jdate.ToShortDateString();
            System.DateTime dob = System.DateTime.Now;
            string env = "";
            if ((RadAC.Value == "1") && (RadNAC.Value == "0"))
            {
                env = "Y";
            }
            else
            {
                env = "N";
            }

            int adults, child, adultstwin, adultstriple, childbed, singleadults, dormitory, NoAWFood, NoCWFood; ;
            adults = Convert.ToInt32(txtNoOfAdults.Text.ToString());
            child = Convert.ToInt32(txtNoOfChilds.Text.ToString());
            adultstwin = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString());
            adultstriple = Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString());
            childbed = Convert.ToInt32(txtNoOfChildBed.Text.ToString());
            singleadults = Convert.ToInt32(txtNoOfSingles.Text.ToString());
            dormitory = Convert.ToInt32(txtNoOfDormitory.Text.ToString());
            NoAWFood = Convert.ToInt32(txtNoOfAdultWF.Text.ToString());
            NoCWFood = Convert.ToInt32(txtNoOfChildWF.Text.ToString());

            int agentid = Convert.ToInt32(ViewState["agentID"]);
            string branchcode = Convert.ToString(ViewState["BranchCode"]);
            decimal amt;
            amt = CalCulateAmount(adults, child, adultstwin, adultstriple, childbed, singleadults, dormitory, NoAWFood, NoCWFood);
            decimal tax, ccfee;

            tax = Convert.ToDecimal(pclsObj.fnGetServiceTaxIsAcc(tourno));

            ccfee = pclsObj.fnGetServiceTaxValue("CC");
            decimal taxamt;
            taxamt = (amt * (tax / 100));

            decimal tot;
            tot = (amt) + (taxamt);
            lNewTotalAmt = tot;
            decimal ccamt = 0;
            ViewState["refund"] = decimal.Round(refundamt - tot);

            int? pkpID = 0;
            pclsObj = new ClsAdo();
            DataTable dtbusse = null;
            try
            {

                int pStatus = pclsObj.fnGetTourDetail(tourno, jdate, Convert.ToString(ViewState["TicketNo"]), ref TourSerial, ref pkpID, ref SeatNumbers);

                if (env == "Y")
                {
                    afare = Convert.ToInt32(lblAACfare.Value);
                    cfare = Convert.ToInt32(lblCACfare.Value);
                    a2fare = Convert.ToInt32(lblA2ACfare.Value);
                    a3fare = Convert.ToInt32(lblA3ACfare.Value);
                    cbfare = Convert.ToInt32(lblCBACfare.Value);
                    safare = Convert.ToInt32(lblSACfare.Value);
                    dfare = Convert.ToInt32(lblDACfare.Value);
                    AdWFoodfare = Convert.ToDecimal(lblAWFACfare.Value); ;
                    CWFoodfare = Convert.ToDecimal(lblCWFACfare.Value); ;
                }

                if (env == "N")
                {
                    afare = Convert.ToInt32(lblANACfare.Value);
                    cfare = Convert.ToInt32(lblCNACfare.Value);
                    a2fare = Convert.ToInt32(lblA2NACfare.Value);
                    a3fare = Convert.ToInt32(lblA3NACfare.Value);
                    cbfare = Convert.ToInt32(lblCBNACfare.Value);
                    safare = Convert.ToInt32(lblSNACfare.Value);
                    dfare = Convert.ToInt32(lblDNACfare.Value);
                    AdWFoodfare = Convert.ToDecimal(lblAWFNACfare.Value); ;
                    CWFoodfare = Convert.ToDecimal(lblCWFNACfare.Value); ;
                }


                string str1;
                string[] seat;

                double? advance1 = 0;
                int val = pclsObj.fnSaveONTranAdv(Convert.ToString(ViewState["TicketNo"]), Convert.ToDecimal(tot + Convert.ToDecimal(ViewState["cancelcharges"])),
                    ref advance1);

                decimal advance = Convert.ToDecimal(tot + Convert.ToDecimal(ViewState["cancelcharges"]));
                if (lIsPartialPaymentByAgent)
                {
                    advance = Convert.ToDecimal(advance1);
                }
                lNewAdvanceAmt = advance;
            }
            catch { }
        }
        private void insertbook(int tourno, int totpax, System.DateTime jdate, ref string orderid, decimal refundamt, bool lIsPartialPaymentByAgent)
        {
            ViewState["refund"] = refundamt;
            int afare = 0, cfare = 0, a2fare = 0, a3fare = 0, cbfare = 0, safare = 0, dfare = 0;
            decimal AdWFoodfare = 0;
            decimal CWFoodfare = 0;
            string doj;
            doj = jdate.ToShortDateString();
            /* Previuos Code
            System.DateTime dob = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
             */
            /* By Kunal */
            System.DateTime dob = System.DateTime.Now;
            /* By Kunal */

            string env = "";
            if ((RadAC.Value == "1") && (RadNAC.Value == "0"))
            {
                env = "Y";
            }
            else
            {
                env = "N";
            }

            int adults, child, adultstwin, adultstriple, childbed, singleadults, dormitory, NoAWFood, NoCWFood; ;
            adults = Convert.ToInt32(txtNoOfAdults.Text.ToString());
            child = Convert.ToInt32(txtNoOfChilds.Text.ToString());
            adultstwin = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString());
            adultstriple = Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString());
            childbed = Convert.ToInt32(txtNoOfChildBed.Text.ToString());
            singleadults = Convert.ToInt32(txtNoOfSingles.Text.ToString());
            dormitory = Convert.ToInt32(txtNoOfDormitory.Text.ToString());
            NoAWFood = Convert.ToInt32(txtNoOfAdultWF.Text.ToString());
            NoCWFood = Convert.ToInt32(txtNoOfChildWF.Text.ToString());

            int agentid = Convert.ToInt32(ViewState["agentID"]);
            string branchcode = Convert.ToString(ViewState["BranchCode"]);
            decimal amt;
            amt = CalCulateAmount(adults, child, adultstwin, adultstriple, childbed, singleadults, dormitory, NoAWFood, NoCWFood);
            decimal tax, ccfee;
            #region Optimize Code
            /*SqlCommand sqlcom;
            SqlConnection sqlcon = DataLib.GetConnection();
            SqlDataReader sqlread = null;

            string chktour = "select isaccomodation from tourmaster where tourno=" + tourno + "";
            string tourchk = DataLib.GetStringData(DataLib.Connection.ConnectionString, chktour);
            string tt = "";
            if (tourchk.ToUpper() == "Y")
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='ATP'";
            }
            else
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='NATP'";
            }



            sqlcom = new SqlCommand(tt, sqlcon);
            tax = Convert.ToDecimal(Convert.ToString(sqlcom.ExecuteScalar()));
            sqlcom = null;
            sqlcom = new SqlCommand("select TaxValue from ServiceTaxMaster where TaxType='CC'", sqlcon);
            ccfee = Convert.ToDecimal(Convert.ToString(sqlcom.ExecuteScalar()));
            sqlcom = null;*/

            #endregion
            tax = Convert.ToDecimal(pclsObj.fnGetServiceTaxIsAcc(tourno));

            ccfee = pclsObj.fnGetServiceTaxValue("CC");
            decimal taxamt;
            taxamt = (amt * (tax / 100));

            decimal tot;
            tot = (amt) + (taxamt);
            decimal ccamt = 0;
            ViewState["refund"] = decimal.Round(refundamt - tot);
            #region Optimize Code
            /*sqlcom = new SqlCommand("Select * from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and TourNo=" + tourno, sqlcon);
            sqlread = sqlcom.ExecuteReader();
            if (sqlread.Read())
            {
                TourSerial = Convert.ToInt32(sqlread["RowId"]);
            }
            sqlread.Close();
            sqlcom = null;

            int pkpID;
            sqlcom = new SqlCommand("select pickupmasterrowid from pickupaddress where tourno='" + tourno + "'", sqlcon);
            pkpID = Convert.ToInt32(sqlcom.ExecuteScalar());
            sqlcom = null;
            //SeatNumbers = Convert.ToString(ViewState["SeatNumbers"]);        
            string seatno = "select seatno from onlinetoursbooking where ticketcode='" + Convert.ToString(ViewState["TicketNo"]) + "'";
            SeatNumbers = DataLib.GetStringData(DataLib.Connection.ConnectionString, seatno);*/
            #endregion
            int? pkpID = 0;
            pclsObj = new ClsAdo();
            DataTable dtbusse = null;
            try
            {

                int pStatus = pclsObj.fnGetTourDetail(tourno, jdate, Convert.ToString(ViewState["TicketNo"]), ref TourSerial, ref pkpID, ref SeatNumbers);

                if (env == "Y")
                {
                    afare = Convert.ToInt32(lblAACfare.Value);
                    cfare = Convert.ToInt32(lblCACfare.Value);
                    a2fare = Convert.ToInt32(lblA2ACfare.Value);
                    a3fare = Convert.ToInt32(lblA3ACfare.Value);
                    cbfare = Convert.ToInt32(lblCBACfare.Value);
                    safare = Convert.ToInt32(lblSACfare.Value);
                    dfare = Convert.ToInt32(lblDACfare.Value);
                    AdWFoodfare = Convert.ToDecimal(lblAWFACfare.Value); ;
                    CWFoodfare = Convert.ToDecimal(lblCWFACfare.Value); ;
                }

                if (env == "N")
                {
                    afare = Convert.ToInt32(lblANACfare.Value);
                    cfare = Convert.ToInt32(lblCNACfare.Value);
                    a2fare = Convert.ToInt32(lblA2NACfare.Value);
                    a3fare = Convert.ToInt32(lblA3NACfare.Value);
                    cbfare = Convert.ToInt32(lblCBNACfare.Value);
                    safare = Convert.ToInt32(lblSNACfare.Value);
                    dfare = Convert.ToInt32(lblDNACfare.Value);
                    AdWFoodfare = Convert.ToDecimal(lblAWFNACfare.Value); ;
                    CWFoodfare = Convert.ToDecimal(lblCWFNACfare.Value); ;
                }


                string str1;
                string[] seat;

                double? advance1 = 0;
                int val = pclsObj.fnSaveONTranAdv(Convert.ToString(ViewState["TicketNo"]), Convert.ToDecimal(tot + Convert.ToDecimal(ViewState["cancelcharges"])),
                    ref advance1);

                decimal pAdultServiceCharges = 0, pChildServiceCharges = 0;
                decimal pServiceChargesTotal = 0;
                decimal pServiceChargesAmount = 0;
                decimal pServiceChargesTax = 0;
                decimal pServiceChargesTaxVal = 0;
                int pATotalPax = adults + adultstwin + adultstriple + singleadults + dormitory + NoAWFood;
                int pCTotalPax = child + childbed + NoCWFood;
                if (ViewState["ServiceChargesTotal"] != null)
                {
                    pAdultServiceCharges = Convert.ToDecimal(ViewState["AdultServiceCharges"]);
                    pChildServiceCharges = Convert.ToDecimal(ViewState["ChildServiceCharges"]);
                    pServiceChargesAmount = (pAdultServiceCharges * pATotalPax) + (pChildServiceCharges * pCTotalPax);
                    pServiceChargesTax = Convert.ToDecimal(ViewState["ServiceChargesTax"]);
                    pServiceChargesTaxVal = decimal.Round((pServiceChargesAmount * Convert.ToDecimal(pServiceChargesTax)) / 100);
                    pServiceChargesTotal = decimal.Round(pServiceChargesAmount + pServiceChargesTaxVal);
                }
                tot = tot + pServiceChargesTotal;

                decimal advance = Convert.ToDecimal(tot + Convert.ToDecimal(ViewState["cancelcharges"]));
                if (lIsPartialPaymentByAgent)
                {
                    advance = Convert.ToDecimal(advance1);
                }
                seat = SeatNumbers.Split(',');
                string[] ssno = SeatNumbers.Split(',');
                string tempstr = "";
                int n = 0;

                string[] pSeatNotBook = Convert.ToString(optedSeatNosBook.Value.TrimStart(',').TrimEnd(',') + ",").Split(',');
                string pSeatWithBus = "";
                string pSeatWithoutBus = "";
                for (int lCtr = 0; lCtr < pSeatNotBook.Length - 1; lCtr++)
                {
                    string[] pSeatStr = pSeatNotBook[lCtr].Split('-');
                    if (pSeatNotBook[lCtr] != "")
                    {
                        string pSeatNo = "s" + pSeatStr[3].Substring(2);

                        pSeatWithBus = pSeatWithBus + pSeatStr[3] + ",";
                        pSeatWithoutBus = pSeatWithoutBus + pSeatNo + ",";
                    }
                }

                //for (int m = 0; m < (Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(Cancelledpax.Text)); m++)
                /*for (int m = 0; m < (Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(canc.Value)); m++)
                {
                    n = n + 1;
                    //if (n == ((Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(Cancelledpax.Text))))
                    if (n == ((Convert.ToInt32(ViewState["tpax"]) - Convert.ToInt32(canc.Value))))
                    {
                        tempstr = tempstr + seat[m];
                    }
                    else
                    {
                        tempstr = tempstr + seat[m] + ",";
                    }
                }*/
                for (int m = 0; m < seat.Length; m++)
                {
                    //n = n + 1;
                    string pBusSerial = Convert.ToString(ViewState["busserialno"]);
                    string[] CHKMulBus = pBusSerial.Split(',');
                    if (CHKMulBus.Length > 1)
                    {
                        bool lFlag = pSeatWithBus.Split(',').Any(p => p.Trim() == seat[m].Trim());
                        //if (!pSeatWithBus.Split(',').Contains(seat[m]))
                        if (!lFlag)
                        {
                            bool lFlag_Dup = tempstr.Split(',').Any(p => p.Trim() == seat[m].Trim());
                            //if (!tempstr.Split(',').Contains(seat[m]))
                            if (!lFlag_Dup)
                            {
                                tempstr = tempstr + seat[m] + ",";
                            }
                        }
                    }
                    else
                    {
                        bool lFlag = pSeatWithoutBus.Split(',').Any(p => p.Trim() == seat[m].Trim());
                        //if (!pSeatWithoutBus.Split(',').Contains(seat[m]))
                        if (!lFlag)
                        {
                            bool lFlag_Dup = tempstr.Split(',').Any(p => p.Trim() == seat[m].Trim());
                            //if (!tempstr.Split(',').Contains(seat[m]))
                            if (!lFlag_Dup)
                            {
                                tempstr = tempstr + seat[m] + ",";
                            }
                        }
                    }
                }
                tempstr = tempstr.TrimEnd(',');
                string emailid = txtemail.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
                tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                string[] ttemp = tempstr.Split(',');
                string dd = Convert.ToString(ViewState["busserialno"]);
                string[] ddd = dd.Split(',');
                if (ddd.Length > 1)
                {
                    string bbno = "";
                    for (int i = 0; i <= ttemp.Length - 1; i++)
                    {
                        if (ttemp[i].Length > 3)
                        {
                            if (bbno == "")
                            {
                                bbno = ttemp[i].Substring(1, 1);
                            }
                            else
                            {
                                bbno = bbno + "," + ttemp[i].Substring(1, 1);
                            }
                        }
                        else
                        {
                            if (bbno == "")
                            {
                                bbno = ttemp[i].Substring(1, 1);
                            }
                            else
                            {
                                bbno = bbno + "," + ttemp[i].Substring(1, 1);
                            }
                        }
                    }
                    #region Optimize Code
                    /*string seatbusno = "select rowid from busallot where tourserial='" + TourSerial + "' and busno in(" + bbno + ")order by busno";
                DataTable dtbusse = DataLib.GetDataTable(DataLib.Connection.ConnectionString, seatbusno);*/
                    #endregion

                    dtbusse = pclsObj.fnAgentGetBusAllotRec(TourSerial, bbno);
                    string ww = "";
                    if (dtbusse.Rows.Count > 0)
                    {
                        for (int h = 0; h < dtbusse.Rows.Count; h++)
                        {
                            if (ww == "")
                            {
                                ww = Convert.ToString(dtbusse.Rows[h]["rowid"]);
                            }
                            else
                            {
                                ww = ww + "," + Convert.ToString(dtbusse.Rows[h]["rowid"]);
                            }
                        }
                    }
                    string[] SeatChk = ww.Split(',');
                    if (SeatChk.Length == 1)
                    {
                        string strSeats = pclsObj.fnGetMultiSeattingChartWithSeats(Convert.ToString(ViewState["TicketNo"]), tempstr);
                        if (strSeats != "0")
                        {
                            tempstr = strSeats;
                        }
                    }
                    int Val = pclsObj.fnInsertAgentFixTourInfo(ref orderid, Convert.ToString(tourno), Convert.ToDateTime(doj), dob, Convert.ToChar(env), adults,
                        child, adultstwin, adultstriple, childbed,
                        singleadults, Convert.ToString(ViewState["TourName"]), Math.Round(amt, 0, MidpointRounding.AwayFromZero), tax, Math.Round(taxamt, 0, MidpointRounding.AwayFromZero),
                        decimal.Round(tot + Convert.ToDecimal(ViewState["cancelcharges"])), tempstr, ww, Convert.ToString(TourSerial), pkpID, afare, cfare, a2fare,
                        a3fare, cbfare, safare, 0, 0, dormitory, dfare, emailid, Convert.ToString(ViewState["TicketNo"]),
                        canrowid.Value, NoAWFood, NoCWFood, Convert.ToDecimal(AdWFoodfare), Convert.ToDecimal(CWFoodfare), Convert.ToInt32(advance), lIsPartialPaymentByAgent,
                        pAdultServiceCharges, pChildServiceCharges, pServiceChargesTotal, pServiceChargesTax, pServiceChargesTaxVal);
                    #region Optimize COde
                    /*str1 = "insert into onlinetoursbooking (orderid,tourid,doj,dob,BusEnvType,NoOfAdults,Noofchild," + 
                    "NoofAdultsTwin,NoofAdultsTriple,ChildWithoutbed,SingleAdult,TourName,Amount,Tax," + 
                    "CalcTaxValue,TotalAmount,seatno,BusSerialNo,TourSerial,PickupPointID,Adultfare," + 
                    "Childfare,adultstwinfare,adultstriplefare,childwithoutbedfare,singleadultfare," + 
                    "CreditCardFee,CalcCreditCardFee,dormitory,dormitoryfare) values('" + orderid + "','" + 
                    tourno + "','" + doj + "','" + dob + "','" + env + "'," + adults + "," + child + "," + 
                    adultstwin + "," + adultstriple + "," + childbed + "," + singleadults + ",'" + 
                    ViewState["TourName"] + "'," + decimal.Round(amt) + "," + tax + "," + 
                    decimal.Round(taxamt) + "," + decimal.Round(tot + Convert.ToDecimal(ViewState["cancelcharges"])) + ",'" + 
                    tempstr + "','" + ww + "'," + TourSerial + "," + pkpID + "," + afare + "," + cfare + "," + a2fare + "," + 
                    a3fare + "," + cbfare + "," + safare + ",0,0," + dormitory + "," + dfare + ")";*/
                    #endregion
                }
                else
                {
                    string strSeats_S = "";
                    strSeats_S = pclsObj.fnGetMultiSeattingChartWithSeats(Convert.ToString(ViewState["TicketNo"]), tempstr);
                    if (strSeats_S != "0" && strSeats_S != "")
                    {
                        tempstr = strSeats_S;
                    }
                    int Val = pclsObj.fnInsertAgentFixTourInfo(ref orderid, Convert.ToString(tourno), Convert.ToDateTime(doj), dob, Convert.ToChar(env),
                        adults, child, adultstwin, adultstriple, childbed,
                       singleadults, Convert.ToString(ViewState["TourName"]), Math.Round(amt, 0, MidpointRounding.AwayFromZero), tax, Math.Round(taxamt, 0, MidpointRounding.AwayFromZero),
                       decimal.Round(tot + Convert.ToDecimal(ViewState["cancelcharges"])), tempstr, Convert.ToString(ViewState["busserialno"]),
                       Convert.ToString(TourSerial), pkpID, afare, cfare, a2fare,
                       a3fare, cbfare, safare, 0, 0, dormitory, dfare, emailid, Convert.ToString(ViewState["TicketNo"]), canrowid.Value
                       , NoAWFood, NoCWFood, Convert.ToDecimal(AdWFoodfare), Convert.ToDecimal(CWFoodfare), Convert.ToInt32(advance), lIsPartialPaymentByAgent,
                       pAdultServiceCharges, pChildServiceCharges, pServiceChargesTotal, pServiceChargesTax, pServiceChargesTaxVal);

                }
            }
            finally
            {
                if (dtbusse != null)
                {
                    dtbusse.Dispose();
                    dtbusse = null;
                }
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
            }
            #region Optimize Code
            /*str1 = "insert into onlinetoursbooking (orderid,tourid,doj,dob,BusEnvType,NoOfAdults,Noofchild,NoofAdultsTwin,NoofAdultsTriple,
                ChildWithoutbed,SingleAdult,TourName,Amount,Tax,CalcTaxValue,TotalAmount,seatno,BusSerialNo,TourSerial,PickupPointID,Adultfare,
                Childfare,adultstwinfare,adultstriplefare,childwithoutbedfare,singleadultfare,CreditCardFee,CalcCreditCardFee,dormitory,dormitoryfare)
                values('" + orderid + "','" + tourno + "','" + doj + "','" + dob + "','" + env + "'," + adults + "," + child + ",
                    " + adultstwin + "," + adultstriple + "," + childbed + "," + singleadults + ",'" + ViewState["TourName"] + "',
                    " + decimal.Round(amt) + "," + tax + "," + decimal.Round(taxamt) + ",
                    " + decimal.Round(tot + Convert.ToDecimal(ViewState["cancelcharges"])) + ",
                '" + tempstr + "','" + ViewState["busserialno"] + "'," + TourSerial + "," + pkpID + "," + afare + ",
                " + cfare + "," + a2fare + "," + a3fare + "," + cbfare + "," + safare + ",0,0," + dormitory + "," + dfare + ")";*/

            /*DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);

            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, "insert into blockstatus values('" + orderid + "','Blocked')", false); ;*/
            #endregion
            #region Optimize COde
            /*
            //DataTable dtOnlineCust = new DataTable();
            int rowidtemp;
            int OAge;
            string emailid = txtemail.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
            if (emailid.Trim() != "")
            {
                str1 = "select * from onlinecustomer where email='" + emailid + "' or mobile='" + emailid + "' ";
                dtOnlineCust = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);
                str1 = "select * from TicketNames where TicketNo='" + ViewState["TicketNo"] + "' and rowid not in(" + canrowid.Value + ")";
                DataTable dtnam = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);
                if (dtOnlineCust.Rows.Count > 0)
                {
                    rowidtemp = Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"].ToString());
                    OName = Request["TxtOName" + i.ToString()];
                    for (i = 0; i < dtnam.Rows.Count; i++)
                    {
                        OName = dtnam.Rows[i]["Name"].ToString();
                        OAge = Convert.ToInt32(dtnam.Rows[i]["Age"]);
                        char gender = Convert.ToChar(dtnam.Rows[i]["Sex"]);
                        string str21;
                        str21 = "insert into onlinepassenger(ocustrowid,orderid,name,age,sex) 
                 * values(" + rowidtemp.ToString() + ",'" + orderid + "'," + "'" + OName + "'," + OAge + ",'" + gender + "')";
                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str21, false);
                    }
                    str1 = "insert into onlinecustbyorder (orderid,ocustrowid) " + " values ('" + orderid + "'," + rowidtemp + ")";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);
                }
                string aa = canrowid.Value;

                int val = pclsObj.fnSaveAgentTourCanPansInfo(emailid, Convert.ToString(ViewState["TicketNo"]), canrowid.Value, orderid);
                 */
            #endregion

        }
        private decimal CalCulateAmount(int adults, int child, int adultstwin, int adultstriple, int childbed, int singleadults, int dormitory, int NoAWFood, int NoCWFood)
        {
            if (lblAACfare.Value == "")
                lblAACfare.Value = "0";
            if (lblCACfare.Value == "")
                lblCACfare.Value = "0";
            if (lblA2ACfare.Value == "")
                lblA2ACfare.Value = "0";
            if (lblA3ACfare.Value == "")
                lblA3ACfare.Value = "0";
            if (lblCBACfare.Value == "")
                lblCBACfare.Value = "0";
            if (lblSACfare.Value == "")
                lblSACfare.Value = "0";
            if (lblANACfare.Value == "")
                lblANACfare.Value = "0";
            if (lblCNACfare.Value == "")
                lblCNACfare.Value = "0";
            if (lblA2NACfare.Value == "")
                lblA2NACfare.Value = "0";
            if (lblA3NACfare.Value == "")
                lblA3NACfare.Value = "0";
            if (lblCBNACfare.Value == "")
                lblCBNACfare.Value = "0";
            if (lblSNACfare.Value == "")
                lblSNACfare.Value = "0";
            if (lblDACfare.Value == "")
                lblDACfare.Value = "0";
            if (lblDNACfare.Value == "")
                lblDNACfare.Value = "0";

            if (lblAWFACfare.Value == "")
                lblAWFACfare.Value = "0";
            if (lblAWFNACfare.Value == "")
                lblAWFNACfare.Value = "0";
            if (lblCWFNACfare.Value == "")
                lblCWFNACfare.Value = "0";
            if (lblCWFACfare.Value == "")
                lblCWFACfare.Value = "0";


            decimal amt = 0;
            if ((RadAC.Value == "1") && (RadNAC.Value == "0"))
            {
                amt = (Convert.ToDecimal(lblAACfare.Value) * Convert.ToDecimal(adults));
                amt = amt + (Convert.ToDecimal(lblCACfare.Value) * Convert.ToDecimal(child));
                amt = amt + (Convert.ToDecimal(lblA2ACfare.Value) * Convert.ToDecimal(adultstwin));
                amt = amt + (Convert.ToDecimal(lblA3ACfare.Value) * Convert.ToDecimal(adultstriple));
                amt = amt + (Convert.ToDecimal(lblCBACfare.Value) * Convert.ToDecimal(childbed));
                amt = amt + (Convert.ToDecimal(lblSACfare.Value) * Convert.ToDecimal(singleadults));
                amt = amt + (Convert.ToDecimal(lblDACfare.Value) * Convert.ToDecimal(dormitory));
                amt = amt + (Convert.ToDecimal(lblAWFACfare.Value) * Convert.ToDecimal(NoAWFood));
                amt = amt + (Convert.ToDecimal(lblCWFACfare.Value) * Convert.ToDecimal(NoCWFood));
            }
            if ((RadAC.Value == "0") && (RadNAC.Value == "1"))
            {
                amt = (Convert.ToDecimal(lblANACfare.Value) * Convert.ToDecimal(adults));
                amt = amt + (Convert.ToDecimal(lblCNACfare.Value) * Convert.ToDecimal(child));
                amt = amt + (Convert.ToDecimal(lblA2NACfare.Value) * Convert.ToDecimal(adultstwin));
                amt = amt + (Convert.ToDecimal(lblA3NACfare.Value) * Convert.ToDecimal(adultstriple));
                amt = amt + (Convert.ToDecimal(lblCBNACfare.Value) * Convert.ToDecimal(childbed));
                amt = amt + (Convert.ToDecimal(lblSNACfare.Value) * Convert.ToDecimal(singleadults));
                amt = amt + (Convert.ToDecimal(lblDACfare.Value) * Convert.ToDecimal(dormitory));
                amt = amt + (Convert.ToDecimal(lblAWFNACfare.Value) * Convert.ToDecimal(NoAWFood));
                amt = amt + (Convert.ToDecimal(lblCWFNACfare.Value) * Convert.ToDecimal(NoCWFood));
            }
            return decimal.Round(amt);
        }
        public StringBuilder seatarr(string pSeaterType, int pTourSerial, int pBusSerialNo, string pBusNo, string pSeatNo, bool IsBook, string pBusNoPrint)
        {
            pBusNo = pBusNo + "-" + pTourSerial + "-" + pBusSerialNo + "-s" + pBusNo;
            StringBuilder Chart = new StringBuilder();

            ClsAdo clsObj = null;
            DataSet ldtRecord = null;
            DataTable vacent = null;
            try
            {
                clsObj = new ClsAdo();
                int? lTourSrNo = pBusSerialNo;
                ldtRecord = clsObj.fnGetSeatArrangementDetail(lTourSrNo);

                if (ldtRecord != null && ldtRecord.Tables.Count > 0)
                {
                    vacent = ldtRecord.Tables[0];
                    int pBType = Convert.ToInt32(ldtRecord.Tables[1].Rows[0]["bustype"]);

                    string pSeatDes = ldtRecord.Tables[2].Rows[0]["seatdesign"].ToString();

                    string[] pSeatDesign;
                    pSeatDesign = pSeatDes.Split(',');

                    Chart.Append("<div>BusNo - " + pBusNoPrint + " </div>");
                    Chart.Append("<div class=\"selectiondiv\" > <div class=\"frontsection\"><div class=\"seat-d\">");

                    Chart.Append("<img src=\"images/seat-d.png\"></div>  ");
                    if (pBType < 14)
                    {
                        Chart.Append("</div> <div class=\"backsection\">");
                    }
                    else
                    {
                        Chart.Append("<div class=\"seat-c\"><img src=\"images/seat-c.png\"></div>");
                        Chart.Append("<div class=\"bus-entry\"><img src=\"images/bus-entry.jpg\"></div></div> <div class=\"backsection\">");
                        Chart.Append("<div class=\"bus-midspace\">&nbsp;</div>");
                    }
                    Chart.Append("<table id=chart" + pBusNo + "2 width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    int pCount = 0;

                    for (int i = 0; i < pSeatDesign.Length; i++)
                    {
                        string pColm = pSeatDesign[i];
                        if (i == 0)
                        {
                            Chart.Append("<tr>");
                        }

                        if (pColm == "b")
                        {
                            Chart.Append("<td width='70px' bgColor='#fff' ></td>");
                        }
                        else if (pColm == "z")
                        {
                            Chart.Append("<td width='70px' bgColor='#fff' colspan=\"3\" style='color:#ffffff;font-weight:normal'><h1>" + "Seater Type - " + pSeaterType + " Seater" + "</h1></td>");
                        }
                        else if ((pColm != "b") && (pColm != "n"))
                        {
                            pCount = pCount + 1;
                            if (pCount <= pBType)
                            {
                                if (IsBook)
                                {
                                    string pSeat = "s" + pColm.ToString();
                                    //if (pSeatNo.Split(',').Contains(pSeat)) // == DBNull.Value)
                                    bool lFlag = pSeatNo.Split(',').Any(p => p.Trim() == pSeat.Trim());
                                    if (lFlag)
                                    {
                                        Chart.Append("<td ID='td" + pBusNo + "" +
                                            pColm + "' width='70px' align='center' height='50px'> <input type='checkbox' ID='" +
                                            pBusNo + "chk" + pColm + "' value='" + pBusNo + "" + pColm +
                                            "' style='display:none;' /><div class='TB_selctd' style='cursor:pointer;' onclick=\"javascript:checkUnheckBook('" + pBusNo +
                                            "chk" + pColm + "',this);\"><span>" + pColm + "</span></div></td>");
                                    }
                                    else
                                    {
                                        Chart.Append("<td align='center' width='70px' height='50px' ><div class='TB_Bkd'><span>" + pColm + "</span></div></td>");
                                    }
                                }
                            }
                        }
                        if (pColm == "n")
                        {
                            Chart.Append("</tr>");
                            Chart.Append("<tr>");
                        }
                    }
                    Chart.Append("</tr>");
                    Chart.Append("</table></div></div>");
                }
                return Chart;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (vacent != null)
                {
                    vacent.Dispose();
                    vacent = null;
                }
                if (ldtRecord != null)
                {
                    ldtRecord.Dispose();
                    ldtRecord = null;
                }
            }
        }
        private void GetSeatDetails()
        {
            SqlConnection lConn = null;
            SqlCommand lCommand = null;
            SqlParameter lParamStDate = null;
            SqlParameter lParamEndDate = null;
            DataSet ldsDetail = new DataSet();
            SqlDataAdapter lDataAdp = null;
            DataTable ldtItinerary = null;
            try
            {
                String strCn = System.Configuration.ConfigurationManager.AppSettings["southernconn"];
                lConn = new SqlConnection(strCn);// For  Live
                lCommand = new SqlCommand(StoredProcedures.GetSeatChange_SP, lConn);
                lCommand.CommandTimeout = 20 * 1000;
                lCommand.CommandType = CommandType.StoredProcedure;
                lCommand.Parameters.AddWithValue("@I_TicketNo", txtticketno.Text.Trim());

                if (lConn.State == ConnectionState.Closed)
                {
                    lConn.Open();
                }
                lCommand.ExecuteNonQuery();

                lDataAdp = new SqlDataAdapter(lCommand);
                lDataAdp.Fill(ldsDetail);
                if (ldsDetail != null && ldsDetail.Tables.Count > 0)
                {
                    if ((ldsDetail.Tables[0] != null && ldsDetail.Tables[0].Rows.Count > 0) ||
                        (ldsDetail.Tables[1] != null && ldsDetail.Tables[1].Rows.Count > 0))
                    {
                        BoookChart = new StringBuilder();
                        if (ldsDetail.Tables[0] != null && ldsDetail.Tables[0].Rows.Count > 0)
                        {
                            for (int lCTR1 = 0; lCTR1 < ldsDetail.Tables[0].Rows.Count; lCTR1++)
                            {
                                BoookChart.Append(seatarr(Convert.ToString(ldsDetail.Tables[0].Rows[lCTR1]["BusType"]),
                                    Convert.ToInt32(ldsDetail.Tables[0].Rows[lCTR1]["TourSerial"]),
                                    Convert.ToInt32(ldsDetail.Tables[0].Rows[lCTR1]["BusSerialNo"]),
                                    Convert.ToString(ldsDetail.Tables[0].Rows[lCTR1]["BusNo"]),
                                    Convert.ToString(ldsDetail.Tables[0].Rows[lCTR1]["SeatNumbers"]), true,
                                    Convert.ToString(ldsDetail.Tables[0].Rows[lCTR1]["BusNo"])));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (lConn != null)
                {
                    if (lConn.State != ConnectionState.Closed)
                    {
                        lConn.Close();
                    }
                    lConn.Dispose();
                    lConn = null;
                }
                if (lCommand != null)
                {
                    lCommand.Dispose();
                    lCommand = null;
                }
                if (ldsDetail != null)
                {
                    ldsDetail.Dispose();
                    ldsDetail = null;
                }
                if (lDataAdp != null)
                {
                    lDataAdp.Dispose();
                    lDataAdp = null;
                }
                if (lParamStDate != null)
                {
                    lParamStDate = null;
                }

                if (lParamEndDate != null)
                {
                    lParamEndDate = null;
                }
                if (ldtItinerary != null)
                {
                    ldtItinerary.Dispose();
                    ldtItinerary = null;
                }
            }
        }
        #endregion
    }
}