using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
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
    public partial class AgentDuplicateTicket : System.Web.UI.Page
    {
        string sDelete
        {
            get
            {
                if (ViewState["sDelete"] == null)
                    return "";
                else
                    return (string)ViewState["sDelete"];
            }
            set { ViewState["sDelete"] = value; }
        }
        string orderid, TicketNo, BranchCode, TourName, PickUppoint, rmarks;
        decimal NAdult, adulttwin, adulttriple, singleadult, NChild, childwithoutbed, dormitory, flatDiscount = 0.0m, strFair = 0.0m;
        DateTime ReportTime, JourneyDate, DepartTime, bookingdate;
        int Age, TourNo;
        decimal Amount, calTax = 0.0m, advancePaid = 0.0m, STaxValue, TotalAmount, cctax, ccCharges = 0.0m, strMinPay = 0.0m;
        string PaymentMode, BankName, TransNumber, TelNo, Sex, Name, BusNo, SeatNumbers;
        decimal AdultFare, adulttwinfare, adulttriplefare, singleadultfare, ChildFare, childwithoutbedfare, dormitoryfare;
        StringBuilder strticket = new StringBuilder();
        string passengar1, groupleader1, Toemail, agentname, agentid, IsOnLine = "", IsDisActive = "";
        ClsAdo pclsObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                sDelete = tmpDelete.Value.ToString();
                if (sDelete == "1")
                {
                    sendMail();
                    tmpDelete.Value = "0";
                }
            }
            else
                Response.Redirect("agentlogin.aspx");
        }
        protected void BindData()
        {
            #region Optimize Code
            /*string str = "select b.rowid,a.orderid,a.doj,a.dob,a.ticketcode,b.groupleader,b.agentid,b.noofseats from onlinetoursbooking a,ticketdetails 
             * b where a.ticketcode is not null and a.doj >= convert(varchar(10),getdate(),101) and a.ticketcode=b.ticketno and (b.cancelled<>'Y' 
             * or b.cancelled<>'y') and b.agentid=" + Session["AgentId"] + "";
            DataTable dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
            #endregion
            pclsObj = new ClsAdo();
            DataTable dt = pclsObj.fnGetAgentFixedTourDupInfo(Convert.ToInt32(Session["AgentId"]));
            try
            {
                dgDuplicateTickets.DataSource = dt;
                Globals.CheckData(ref dgDuplicateTickets, dt, ref lblMsg);
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }
        protected void dgDuplicateTickets_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgDuplicateTickets.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
        public void func()
        {
            #region Optimize Code
            /*string strSQL, strWhere;
            strSQL = "select a.rowid, a.orderid,a.doj,a.dob,a.ticketcode,b.groupleader,b.agentid,b.noofseats,b.email,b.telno 
             * from onlinetoursbooking a,ticketdetails b where a.ticketcode is not null and a.doj >= convert(varchar(10),getdate(),101)
             * and a.ticketcode=b.ticketno and (b.cancelled<>'Y' or b.cancelled<>'y') and b.agentid=" + Session["AgentId"] + "";
            strWhere = "";
            string pnr = txtPnrNo.Value.Trim();
            string ticket = txtticketno.Value.Trim();
            string journeydate = txtjdate.Value;
            string contactno = txtContact.Value.Replace("'", "''").Trim();
            string emailid = txtEmail.Value.Replace("'", "''").Trim();
            if (journeydate != "")
            {
                string[] dd = journeydate.Split('/');
                journeydate = dd[1] + "/" + dd[0] + "/" + dd[2];
            }
            if (pnr != "")
                strWhere = strWhere + " and a.orderid='" + pnr + "'";
            if (ticket != "")
                strWhere = strWhere + " and a.ticketcode='" + ticket + "'";
            if (contactno != "")
                strWhere = strWhere + " and b.TelNo='" + contactno + "'";
            if (emailid != "")
                strWhere = strWhere + " and b.Email='" + emailid + "'";
            if (journeydate != "")
                strWhere = strWhere + " and a.doj='" + journeydate + "'";

            strSQL = strSQL + strWhere;
            DataTable dtnew = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strSQL);*/
            #endregion
            string pnr = txtPnrNo.Value.Trim();
            string ticket = txtticketno.Value.Trim();
            string journeydate = txtjdate.Value;
            string contactno = txtContact.Value.Replace("'", "''").Trim();
            string emailid = txtEmail.Value.Replace("'", "''").Trim();

            DateTime? lFromdate = null;
            if (txtjdate.Value != "")
            {
                string[] pJDate = Request[txtjdate.UniqueID].Split('/');
                lFromdate = new DateTime(Convert.ToInt32(pJDate[2]), Convert.ToInt32(pJDate[1]), Convert.ToInt32(pJDate[0]));
            }
            pclsObj = new ClsAdo();
            DataSet ldsRecSet = pclsObj.fnGetAgentFixedTourDupTKT(Convert.ToString(Session["AgentId"]), pnr, ticket, contactno, emailid,
                lFromdate);
            DataTable dtnew = new DataTable();
            if (ldsRecSet != null)
                dtnew = ldsRecSet.Tables[0];
            try
            {
                dgDuplicateTickets.DataSource = dtnew;
                Globals.CheckData(ref dgDuplicateTickets, dtnew, ref lblMsg);
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
                if (dtnew != null)
                {
                    dtnew.Dispose();
                    dtnew = null;
                }
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            func();
        }
        protected void dgDuplicateTickets_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView rv = (DataRowView)e.Item.DataItem;
                string k = Convert.ToString(rv.Row["ticketcode"]);
                if (k.Substring(0, 3) == "EBK")
                    e.Item.Cells[6].Text = "Bus Ticket Booking";
                if (k.Substring(0, 3) == "CAB")
                    e.Item.Cells[6].Text = "Car Booking";
                if (k.Substring(0, 3) == "STR")
                    e.Item.Cells[6].Text = "Hotel Booking";
                if (k.Substring(0, 3) == "SPL")
                    e.Item.Cells[6].Text = "Special Booking";
            }
        }
        public void CheckData(ref System.Web.UI.WebControls.DataGrid oDataGrid, DataTable dt, ref System.Web.UI.WebControls.Label oLbl)
        {
            oLbl.Text = "";
            if (Convert.ToInt32(dt.Rows.Count) == 0)
            {
                oLbl.Text = "No Data Found for the Search Criteria you Entered";
                oDataGrid.Visible = false;
            }
            else
            {
                oDataGrid.Visible = true;
                try
                {
                    oDataGrid.DataBind();
                }
                catch
                {
                    try
                    {
                        oDataGrid.CurrentPageIndex = 0;
                        oDataGrid.DataBind();
                    }
                    catch
                    {
                        oLbl.Text = "No data for selection";
                    }
                    oLbl.Text = "";
                }
            }
        }
        void sendMail()
        {
            foreach (DataGridItem di in dgDuplicateTickets.Items)
            {
                bool CheckSelected;
                if (di.ItemType != ListItemType.Item || di.ItemType != ListItemType.AlternatingItem)
                {
                    CheckSelected = ((CheckBox)(di.Cells[0].FindControl("chkAccept"))).Checked;
                    if (CheckSelected == true)
                    {
                        #region Optimize Code
                        string tkCode = Convert.ToString(dgDuplicateTickets.DataKeys[di.ItemIndex].ToString());
                        /*DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select ticketcode,tourid,rowid,
                         * orderid,busserialno,dob from OnlineToursBooking where ticketcode='" + tkCode + "'");*/
                        #endregion
                        pclsObj = new ClsAdo();
                        DataTable dtTicketDetails = pclsObj.TicketCodeByOrderDetail(tkCode);
                        int i;
                        try
                        {
                            for (i = 0; i < dtTicketDetails.Rows.Count; i++)
                            {
                                TicketString(dtTicketDetails.Rows[i]["orderid"].ToString(), i + 1, Convert.ToInt32(dtTicketDetails.Rows[i]["rowid"]), Convert.ToString(dtTicketDetails.Rows[i]["busserialno"]), Convert.ToDateTime(dtTicketDetails.Rows[i]["dob"]));
                            }
                        }
                        finally
                        {
                            pclsObj = null;
                        }
                    }
                }
            }
            this.RegisterStartupScript("sdad", "<script>alert('Mail(s) sent successfully');</script>");
        }
        protected decimal CalcAdult()
        {
            decimal result = NAdult * AdultFare;
            return result;
        }
        protected decimal CalcAdulttwin()
        {
            decimal result;
            result = adulttwin * adulttwinfare;
            return result;
        }
        protected decimal CalcAdulttriple()
        {
            decimal result;
            result = adulttriple * adulttriplefare;
            return result;
        }
        protected decimal CalcSingleadult()
        {
            decimal result = singleadult * singleadultfare;
            return result;
        }
        protected decimal CalcChildWithOutBed()
        {
            decimal result = childwithoutbed * childwithoutbedfare;
            return result;
        }
        protected decimal CalcChild()
        {
            decimal result = NChild * ChildFare;
            return result;
        }
        protected decimal Calcdormitory()
        {
            decimal result = dormitory * dormitoryfare;
            return result;
        }
        protected StringBuilder TicketString(string orderid, int i, int rowid, string bsno, DateTime dob)
        {
            /*Toemail = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select email from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid='" + orderid + "'");*/
            pclsObj = new ClsAdo();
            DataSet dsTicketDetails = pclsObj.fnGetTickets_PanelDetail(orderid);
            DataTable dtETicketDetails = dsTicketDetails.Tables[0];
            Toemail = dtETicketDetails.Rows[0]["email"].ToString();

            StringBuilder MailSubject = new StringBuilder();
            StringBuilder s = new StringBuilder();
            s.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\ticket_template.html")));
            decimal discount = 0;
            string chknumber = "", bank = "", chqdate = "";
            decimal strCanCharge = 0.0m, refundamt = 0m;
            #region Commented
            /*string sql = "select Td.ticketno,ba.busno,otb.calctaxvalue,otb.seatno,otb.amount,otb.AdvancePaid,td.advance,
             * dbo.Pickuppoint_address_fn(Td.ticketno) as pickuppoint,td.reporttime,td.departtime,otb.noofadults,otb.noofadultstwin,
             * otb.noofadultstriple,otb.singleadult,otb.noofchild,otb.childwithoutbed,otb.dormitory,tn.name,tn.age,
             * tn.sex,dbo.initcap(tm.tourname) as tourname,tm.tourno,otb.doj,otb.dob,tt.paymentmode,tt.number,tt.bankname,isnull(tt.chqdate,'') as chequedate,
             * td.branchcode,otb.busenvtype,otb.adultfare,otb.childfare,otb.adultstwinfare,otb.adultstriplefare,otb.childwithoutbedfare,otb.singleadultfare,
             * otb.dormitoryfare,td.agentid,td.username,td.telno,td.reltelno,td.manual_ticketno,otb.totalamount,otb.tax,otb.CreditCardFee,otb.discount,
             * otb.Remarks,otb.onlineDis,tm.IsOnline,tm.isDiscountActive,isnull(otb.MinimumPay,0) as MinimumPay from ticketdetails td,busallot ba,
             * onlinetoursbooking otb,ticketnames tn,tourmaster tm,Onlinetransactiontable tt 
             * where otb.orderid='" + orderid + "' and otb.rowid=" + rowid + " and otb.ticketcode=td.ticketno and tn.ticketno=otb.ticketcode and tm.tourno=otb.tourid and tt.refno=otb.ticketcode and ba.rowid in(" + bsno + ")";
            DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sql);*/
            #endregion
            DataSet ldsRecSet = pclsObj.fnGetDupTicketPrintAgent(orderid, rowid, bsno);
            DataTable dtTicketDetails = ldsRecSet.Tables[0];
            DataTable dtprecan = null, dtpreticket = null, dtseats = null, dtPay = null, ldtHierarchy = null, dtterms = null, dtCan = null;
            StringBuilder lMailHtml = new StringBuilder();
            lMailHtml.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            lMailHtml.Append("<TR><TD align=\"center\"><b>");
            lMailHtml.Append("PLEASE DO NOT REPLY TO THIS MAIL AS THIS IS AN AUTO GENERATED MAIL AND REPLIES / QUERIES TO THIS EMAIL ID ARE NOT ATTENDED. FOR ANY ASSISTANCE / QUERIES PLEASE MAIL US AT <a href=\"mailto:support@southerntravels.in\" title='' >support@southerntravels.in</a>");
            lMailHtml.Append("</b></TD></TR></TABLE>");
            try
            {
                if (dtTicketDetails.Rows.Count > 0)
                {
                    TourNo = Convert.ToInt16(dtTicketDetails.Rows[0]["tourno"].ToString());
                    TicketNo = dtTicketDetails.Rows[0]["ticketno"].ToString();
                    BranchCode = dtTicketDetails.Rows[0]["branchcode"].ToString();
                    TourName = dtTicketDetails.Rows[0]["tourname"].ToString();
                    NAdult = Convert.ToDecimal(dtTicketDetails.Rows[0]["noofadults"].ToString());
                    adulttwin = Convert.ToDecimal(dtTicketDetails.Rows[0]["noofadultstwin"].ToString());
                    adulttriple = Convert.ToDecimal(dtTicketDetails.Rows[0]["noofadultstriple"].ToString());
                    singleadult = Convert.ToDecimal(dtTicketDetails.Rows[0]["singleadult"].ToString());
                    dormitory = Convert.ToDecimal(dtTicketDetails.Rows[0]["dormitory"].ToString());
                    ReportTime = Convert.ToDateTime(dtTicketDetails.Rows[0]["reporttime"].ToString());
                    PickUppoint = dtTicketDetails.Rows[0]["pickuppoint"].ToString();
                    JourneyDate = Convert.ToDateTime(dtTicketDetails.Rows[0]["doj"].ToString());
                    JourneyDate = Convert.ToDateTime((JourneyDate).ToShortDateString().ToString());
                    bookingdate = Convert.ToDateTime(dtTicketDetails.Rows[0]["dob"].ToString());
                    bookingdate = Convert.ToDateTime((bookingdate).ToShortDateString().ToString());
                    DepartTime = Convert.ToDateTime(dtTicketDetails.Rows[0]["departtime"].ToString());
                    NChild = Convert.ToDecimal(dtTicketDetails.Rows[0]["noofchild"].ToString());
                    childwithoutbed = Convert.ToInt16(dtTicketDetails.Rows[0]["childwithoutbed"].ToString());
                    Amount = Convert.ToDecimal(dtTicketDetails.Rows[0]["amount"].ToString());
                    calTax = Convert.ToDecimal(dtTicketDetails.Rows[0]["Calctaxvalue"].ToString());
                    STaxValue = Convert.ToDecimal(dtTicketDetails.Rows[0]["tax"].ToString());
                    TotalAmount = Convert.ToDecimal(dtTicketDetails.Rows[0]["totalamount"].ToString());
                    cctax = Convert.ToDecimal(dtTicketDetails.Rows[0]["CreditCardFee"].ToString());
                    PaymentMode = dtTicketDetails.Rows[0]["paymentmode"].ToString();
                    chknumber = Convert.ToString(dtTicketDetails.Rows[0]["number"]);
                    bank = Convert.ToString(dtTicketDetails.Rows[0]["bankname"]);
                    chqdate = Convert.ToString(dtTicketDetails.Rows[0]["chequedate"]);
                    TelNo = dtTicketDetails.Rows[0]["telno"].ToString();
                    BusNo = dtTicketDetails.Rows[0]["busno"].ToString();
                    SeatNumbers = dtTicketDetails.Rows[0]["seatno"].ToString();
                    AdultFare = Convert.ToDecimal(dtTicketDetails.Rows[0]["AdultFare"].ToString());
                    adulttwinfare = Convert.ToDecimal(dtTicketDetails.Rows[0]["adultstwinfare"].ToString());
                    adulttriplefare = Convert.ToDecimal(dtTicketDetails.Rows[0]["adultstriplefare"].ToString());
                    singleadultfare = Convert.ToDecimal(dtTicketDetails.Rows[0]["singleadultfare"].ToString());
                    dormitoryfare = Convert.ToDecimal(dtTicketDetails.Rows[0]["dormitoryfare"].ToString());
                    ChildFare = Convert.ToDecimal(dtTicketDetails.Rows[0]["ChildFare"].ToString());
                    childwithoutbedfare = Convert.ToDecimal(dtTicketDetails.Rows[0]["childwithoutbedfare"].ToString());
                    agentid = dtTicketDetails.Rows[0]["agentid"].ToString();
                    agentname = dtTicketDetails.Rows[0]["username"].ToString();
                    rmarks = Convert.ToString(dtTicketDetails.Rows[0]["Remarks"]);
                    IsOnLine = dtTicketDetails.Rows[0]["IsOnline"].ToString();

                    if (Convert.ToString(dtTicketDetails.Rows[0]["onlineDis"]) != "")
                        flatDiscount = Convert.ToDecimal(dtTicketDetails.Rows[0]["onlineDis"].ToString());
                    else
                        flatDiscount = 0;
                    IsDisActive = dtTicketDetails.Rows[0]["isDiscountActive"].ToString();
                    strMinPay = Convert.ToDecimal(dtTicketDetails.Rows[0]["MinimumPay"].ToString());
                    if (IsOnLine == "Y" && IsDisActive == "Y")
                    {
                        decimal strAddDiscount = 0.0m, strActualVal = 0.0m; ;
                        strActualVal = ((flatDiscount * 100) / (100 - flatDiscount));
                        strFair = TotalAmount - calTax;
                        strAddDiscount = (((100 + strActualVal) * strFair) / 100);
                        strFair = decimal.Round(strFair + strAddDiscount - Amount);
                    }

                    if ((Convert.ToString(dtTicketDetails.Rows[0]["discount"]) != "") && (Convert.ToString(dtTicketDetails.Rows[0]["discount"]) != null))
                        discount = Convert.ToDecimal(dtTicketDetails.Rows[0]["discount"]);
                    else
                        discount = 0;

                    if ((Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != "") && (Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != null))
                    {
                        advancePaid = Convert.ToDecimal(dtTicketDetails.Rows[0]["AdvancePaid"].ToString());
                    }
                    else
                    {
                        //if ((BranchCode == "EBK0001") || (Convert.ToInt32(agentid) > 0)) advancePaid = TotalAmount; else advancePaid = 0;
                        if ((Convert.ToInt32(agentid) > 0)) advancePaid = TotalAmount; else advancePaid = 0;
                    }
                }
                dtprecan = new DataTable();
                dtpreticket = new DataTable();

                /* string can = "select * from cancel where newticket='" + TicketNo + "'";
                 dtprecan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, can);*/
                dtprecan = ldsRecSet.Tables[1];
                if (dtprecan.Rows.Count > 0)
                {
                    /*string pretic = "select * from onlinetoursbooking where ticketcode='" + Convert.ToString(dtprecan.Rows[0]["ticketno"]) + "'";
                    dtpreticket = DataLib.GetDataTable(DataLib.Connection.ConnectionString, pretic);*/
                    dtpreticket = ldsRecSet.Tables[2];
                    if (dtpreticket.Rows.Count > 0)
                    {
                        if ((BranchCode == "EBK0001") || (Convert.ToInt32(agentid) > 0)) advancePaid = Convert.ToDecimal(dtpreticket.Rows[0]["TotalAmount"]);
                    }
                }

                //===============================================//
                StringBuilder AgentDetail = new StringBuilder();

                AgentDetail.Append("<TABLE id=Table2 cellSpacing=0 cellPadding=6 width=100% border=0>");
                AgentDetail.Append("<TR>");
                AgentDetail.Append("<TD class=style5 style=\"HEIGHT: 15px\" align=center></TD>");
                AgentDetail.Append("<TD class=style5 style=\"HEIGHT: 15px\" align=center></TD>");
                AgentDetail.Append("</TR>");

                AgentDetail.Append("<TR><td colspan=2><table width=100% border=1 cellpadding=0 cellspacing=0 bgcolor=#CCCCCC> <tr> <td><table width=100% border=0 cellpadding=10 cellspacing=0 bgcolor=#FFFFFF>");
                AgentDetail.Append("<tr>");
                string saveName = "Logo\\" + Session["UserId"].ToString();
                string exist = "N";
                if (System.IO.File.Exists(Server.MapPath(saveName + ".jpg"))) { saveName = saveName + ".jpg"; exist = "Y"; }
                if (System.IO.File.Exists(Server.MapPath(saveName + ".gif"))) { saveName = saveName + ".gif"; exist = "Y"; }
                if (System.IO.File.Exists(Server.MapPath(saveName + ".png"))) { saveName = saveName + ".png"; exist = "Y"; }
                string address1 = Globals.AgentAddress;
                string ph = Globals.AgentPhone;
                if (exist == "Y")
                {
                    AgentDetail.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><img src='" + saveName + "' width=\"157\" height=\"69\" /><br />" + address1 + "," + ph + "</font></td>");
                }
                else
                {
                    string AgentName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                    AgentDetail.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><string>'" + AgentName + "'</string><br/>" + address1 + "," + ph + "</font></td>");
                }

                AgentDetail.Append("</TR>");
                AgentDetail.Append("</table>");
                s = s.Replace("#Agentd", "block");
                s = s.Replace("#Agent", AgentDetail.ToString());




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
                if (dormitory > 0)
                {
                    lCostingHTML.Append(ClsCommon.GetCostingRow("Dormitory Accommodation", dormitory, dormitoryfare, Calcdormitory()));
                }
                if (adulttwin > 0)
                {
                    lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Twin Sharing", adulttwin, adulttwinfare, CalcAdulttwin()));
                }
                if (adulttriple > 0)
                {
                    lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Triple Sharing", adulttriple,
                        adulttriplefare, CalcAdulttriple()));
                }
                if (singleadult > 0)
                {
                    lCostingHTML.Append(ClsCommon.GetCostingRow("Adults On Single Sharing", singleadult,
                        singleadultfare, CalcSingleadult()));
                }
                if (childwithoutbed > 0)
                {
                    lCostingHTML.Append(ClsCommon.GetCostingRow("Child Without Bed", childwithoutbed,
                        childwithoutbedfare, CalcChildWithOutBed()));
                }
                if (lCostingHTML.ToString().Trim() != string.Empty)
                {
                    string lCosting = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" + lCostingHTML.ToString() +
                        "</table>";
                    lCostingHTML = new StringBuilder();
                    lCostingHTML.Append(lCosting);
                }
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
                #region Commented
                //StringBuilder Strname = new StringBuilder();
                //Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100% border=1>");
                //Strname.Append("<TR><TD class=cgi align=center>Passenger Names</TD><TD class=cgi align=center width=50px>Age</TD><TD class=cgi align=center width=50px>Sex</TD><TD class=cgi align=center width=50px>Bus No</TD><TD class=cgi align=center>Seat</TD></TR>");
                /*dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select * from ticketnames where ticketno='" + TicketNo + "'");

                string ticketbusno = "select busno,seatnumbers from ticketbusno where ticketno='" + TicketNo + "' order by busno";
                DataTable dtseats = DataLib.GetDataTable(DataLib.Connection.ConnectionString, ticketbusno);*/
                #endregion
                dtTicketDetails = ldsRecSet.Tables[3];
                dtseats = ldsRecSet.Tables[4];

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
                for (int m = 0; m < dtTicketDetails.Rows.Count; m++)
                {
                    Name = dtTicketDetails.Rows[m]["Name"].ToString();
                    Age = Convert.ToInt16(dtTicketDetails.Rows[m]["Age"].ToString());
                    Sex = dtTicketDetails.Rows[m]["Sex"].ToString();

                    string[] bno = busseat[m].Split('-');
                    string aa = bno[0], lTopBottomBorder = ""; ;
                    string bb = bno[1];

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

                s = s.Replace("#pnr", orderid);
                s = s.Replace("#bookDate", Convert.ToString(bookingdate.ToString("dd-MMM-yyyy")));
                s = s.Replace("#pickName", PickUppoint);
                s = s.Replace("#ticket", TicketNo);
                s = s.Replace("#jdate", Convert.ToString(JourneyDate.ToString("dd-MMM-yyyy")));
                s = s.Replace("#tourName", TourName);
                // s = s.Replace("#reprtingTime", Convert.ToString(ReportTime.ToShortTimeString()));
                s = s.Replace("#reprtingTime", Convert.ToString(DepartTime.AddMinutes(-15).ToShortTimeString()));
                s = s.Replace("#departTime", Convert.ToString(DepartTime.ToShortTimeString()));
                s = s.Replace("#Costing", Convert.ToString(lCostingHTML));
                s = s.Replace("#passengerslist", Convert.ToString(Strname));
                s = s.Replace("#Amount", Convert.ToString(decimal.Round(Amount)));

                if (BranchCode == "EBK0001" && IsOnLine == "Y" && IsDisActive == "Y")
                {
                    s = s.Replace("#lblFair", "Fare: ");
                    s = s.Replace("#Fair", Convert.ToString(decimal.Round(strFair)));
                    s = s.Replace("#FlatDiscount", "Flat Discount");
                    s = s.Replace("#Flat", Convert.ToString(decimal.Round(strFair - Amount)) + "(" + flatDiscount + "%)");
                }
                else
                {
                    s = s.Replace("#lblFair", "");
                    s = s.Replace("#Fair", "");
                    s = s.Replace("#FlatDiscount", "");
                    s = s.Replace("#Flat", "");
                }

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
                        if (dtprecan.Rows.Count > 0)
                        {
                            s = s.Replace("#lblDiscount", "Cancellation Charges for( " + Convert.ToString(dtprecan.Rows[0]["ticketno"]) + " ): ");
                            s = s.Replace("#discount", Convert.ToString(dtprecan.Rows[0]["cancharges"]));
                        }
                        else
                        {
                            s = s.Replace("#lblDiscount", "");
                            s = s.Replace("#discount", "");
                        }
                    }
                }
                if ((agentid == "0") && (BranchCode == "EBK0001"))
                {
                    s = s.Replace("#lblServiceTax", "GST : ");
                    s = s.Replace("#st", Convert.ToString(decimal.Round(amt3)));
                    s = s.Replace("#BookingExecutive", "");
                    s = s.Replace("#branchUser", "");
                }
                else
                {
                    s = s.Replace("#lblServiceTax", "GST : ");
                    s = s.Replace("#st", Convert.ToString(decimal.Round(amt3)));
                    s = s.Replace("#BookingExecutive", "Booking Executive: ");
                    s = s.Replace("#branchUser", agentname);
                }
                s = s.Replace("#totalAmt", Convert.ToString(decimal.Round(TotalAmount)));
                if ((advancePaid) > 0)
                {
                    s = s.Replace("#advPaid", Convert.ToString(decimal.Round(advancePaid)));
                    if (decimal.Round((TotalAmount - advancePaid)) > 0)
                    {
                        s = s.Replace("#balDue", Convert.ToString(decimal.Round(TotalAmount - advancePaid)));
                    }
                    else
                    {
                        s = s.Replace("#advPaid", "Nil");
                    }
                }
                else if ((strMinPay) > 0)
                {
                    s = s.Replace("#advPaid", Convert.ToString(decimal.Round(strMinPay)));
                    if (decimal.Round((TotalAmount - strMinPay)) > 0)
                    {
                        s = s.Replace("#balDue", Convert.ToString(decimal.Round(TotalAmount - strMinPay)));
                    }
                    else
                    {
                        s = s.Replace("#balDue", "Nil");
                    }
                }
                if (PaymentMode.Trim().Length < 3)
                {
                    /* string ss = "select Paymentmode from tbl_paymodes where rowid='" + PaymentMode + "'";
                     string pm = DataLib.GetStringData(DataLib.Connection.ConnectionString, ss);*/
                    dtPay = ldsRecSet.Tables[5];

                    string pm = dtPay.Rows[0]["Paymentmode"].ToString();
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
                StringBuilder ms = new StringBuilder();
                if ((advancePaid - TotalAmount) > 0)
                {
                    refundamt = advancePaid - TotalAmount;
                    decimal refun = refundamt;
                    if (refun > 0)
                    {
                        ms.Append("<SPAN class=cgi>Refund Amount:</SPAN>" + decimal.Round(refun));
                    }
                    ms.Append("</SPAN>");
                }
                else
                {
                    if (dtpreticket.Rows.Count > 0)
                    {
                        refundamt = 0;
                        if (Convert.ToString(dtpreticket.Rows[0]["advancepaid"]) != "")
                        {
                            refundamt = Convert.ToDecimal(dtpreticket.Rows[0]["advancepaid"]) - TotalAmount;
                        }
                        else
                        {
                            refundamt = Convert.ToDecimal(dtpreticket.Rows[0]["TotalAmount"]) - TotalAmount;
                        }

                        decimal refun = refundamt;
                        if (refun > 0)
                        {
                            ms.Append("<SPAN class=cgi>Refund Amount:</SPAN>" + decimal.Round(refun));
                        }
                        ms.Append("</SPAN>");
                    }
                    else
                    {
                        ms.Append("");
                    }
                }
                s = s.Replace("#refund", Convert.ToString(ms));
                // string gpldr = groupleader(orderid);
                string gpldr = ldsRecSet.Tables[8].Rows[0]["Name1"].ToString();
                s = s.Replace("#custContact", gpldr + " / " + TelNo);
                //string custadd = CustAddress(orderid);
                string custadd = ldsRecSet.Tables[8].Rows[0]["address"].ToString();
                s = s.Replace("#custaddress", custadd);

                if ((Convert.ToInt32(agentid) > 0))
                {
                    s = s.Replace("#BookingOffice", "Booking Agent :");
                    s = s.Replace("#branchCode", BranchCode);
                }
                else if ((BranchCode != "EBK0001") && (agentid == "0"))
                {
                    s = s.Replace("#BookingOffice", "Booking Branch :");
                    s = s.Replace("#branchCode", BranchCode);
                }
                else
                {
                    s = s.Replace("#BookingOffice", "Booking from :");
                    s = s.Replace("#branchCode", "Online Booking");
                }
                MailSubject.Append(s);
                if (rmarks != "")
                {
                    StringBuilder RemarksText = new StringBuilder();
                    RemarksText.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\TktRemarks.htm")));

                    RemarksText.Replace("#Remarks", rmarks);
                    MailSubject.Replace("#ShowRemarksText", RemarksText.ToString());

                }
                else
                {
                    MailSubject.Replace("#ShowRemarksText", "");

                }
                #region Commented
                /*if (rmarks != "")
               {
                   MailSubject.Replace("#Remarks", rmarks);
                   MailSubject.Replace("#ShowRemarks", "block");

                   //MailSubject.Append("<table width=\"100%\" border=\"1\" cellSpacing=\"0\" cellPadding=\"1\">");
                   //MailSubject.Append("<TR>");
                   //MailSubject.Append("<TD><span class=\"cgi\">Remarks :</span><span class=\"hlinks\"><b>" + rmarks + "</b></span></TD>");
                   //MailSubject.Append("</TR></table>");

               }
               else
               {
                   MailSubject.Replace("#Remarks", "");
                   MailSubject.Replace("#ShowRemarks", "none");
               } */
                /*if (dtprecan.Rows.Count > 0)
                {
                    MailSubject.Append("<table width=40% border=0>");
                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD colspan=2><span class=cgi><u>Calculations :</u></span></TD>");
                    MailSubject.Append("</TR>");
                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD><span class=cgi align=left>Old Ticket Amount (" + Convert.ToString(dtpreticket.Rows[0]["ticketcode"]) + ") :</span></TD>");
                    MailSubject.Append("<TD><span class=hlinks align=left>" + Convert.ToString(dtpreticket.Rows[0]["TotalAmount"]) + "</span></TD>");

                    MailSubject.Append("</TR>");
                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD><span class=cgi align=left>Cancellation Charges per (" + Convert.ToString(dtprecan.Rows[0]["cannotick"]) + ") Pax :</span></TD>");
                    MailSubject.Append("<TD><span class=hlinks align=left>" + Convert.ToString(dtprecan.Rows[0]["cancharges"]) + "</span></TD>");

                    MailSubject.Append("</TR>");
                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD><span class=cgi align=left>New Ticket Amount (" + TicketNo + ") :</span></TD>");
                    MailSubject.Append("<TD><span class=hlinks align=left>" + decimal.Round(TotalAmount) + "</span></TD>");
                    MailSubject.Append("</TR>");
                    MailSubject.Append("</table>");
                }*/
                #endregion
                if (dtprecan.Rows.Count > 0)
                {
                    int? lHierarchyCount = 0;
                    ldtHierarchy = pclsObj.fnGetCancelTktHierarchy(Convert.ToString(dtprecan.Rows[0]["NewTicket"]),
                        ref lHierarchyCount);

                    if (lHierarchyCount > 1)
                    {
                        refundamt -= Convert.ToDecimal(ldtHierarchy.Rows[1]["TktCanCharge"]);
                        MailSubject = MailSubject.Replace("#refund", Convert.ToString(decimal.Round(refundamt)));
                    }
                    else
                    {
                        MailSubject = MailSubject.Replace("#refund", Convert.ToString(decimal.Round(refundamt)));
                    }
                    StringBuilder CanCalculationText = new StringBuilder();
                    // CanCalculationText.Append(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\FixedTourCalculation.htm")));
                    CanCalculationText.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() +
                   "\\FixedTourCalculation.htm")));
                    #region Commented
                    /*MailSubject = MailSubject.Replace("#CanCalculation", "block");
                    MailSubject = MailSubject.Replace("#CancelTktNo", Convert.ToString(dtpreticket.Rows[0]["ticketcode"]));
                    decimal lPrevTktAmount = 0;
                    lPrevTktAmount = ((Convert.ToDecimal(ldtHierarchy.Rows[0]["TktAmount"]) +
                        Convert.ToDecimal(ldtHierarchy.Rows[0]["TktServiceTax"])));
                    MailSubject = MailSubject.Replace("#OldTktAmount", lPrevTktAmount.ToString());
                    MailSubject = MailSubject.Replace("#PersonCancelled", Convert.ToString(dtprecan.Rows[0]["cannotick"]));
                    MailSubject = MailSubject.Replace("#CanCharge", Convert.ToString(dtprecan.Rows[0]["cancharges"]));*/
                    #endregion
                    CanCalculationText = CanCalculationText.Replace("#CancelTktNo", Convert.ToString(dtpreticket.Rows[0]["ticketcode"]));
                    decimal lPrevTktAmount = 0;
                    lPrevTktAmount = ((Convert.ToDecimal(ldtHierarchy.Rows[0]["TktAmount"]) +
                        Convert.ToDecimal(ldtHierarchy.Rows[0]["TktServiceTax"])));
                    CanCalculationText = CanCalculationText.Replace("#OldTktAmount", lPrevTktAmount.ToString());

                    CanCalculationText = CanCalculationText.Replace("#CanCharge", Convert.ToString(dtprecan.Rows[0]["cancharges"]));

                    CanCalculationText = CanCalculationText.Replace("#CanchaNew", Convert.ToString(dtprecan.Rows[0]["cancharges"]));
                    CanCalculationText = CanCalculationText.Replace("#PersonCancelled", "Cancellation Charges per (" + Convert.ToString(dtprecan.Rows[0]["ticketno"]) + ") Pax (" + Convert.ToString(dtprecan.Rows[0]["CanNoTick"]) + ") :");
                    CanCalculationText = CanCalculationText.Replace("#totalAmt", Convert.ToString(decimal.Round(TotalAmount)));
                    CanCalculationText = CanCalculationText.Replace("#ticket", TicketNo);
                    MailSubject = MailSubject.Replace("#CanCalculationText", CanCalculationText.ToString());
                }
                else
                {
                    MailSubject = MailSubject.Replace("#CanCalculationText", "");
                    MailSubject = MailSubject.Replace("#CanCalculation", "none");
                }
                #region Commented
                /* string strterms = "SELECT FixedTermsCondition FROM CompanyDetailsForRpt";
                 DataTable dtterms = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strterms);*/
                /* DataTable dtterms = ldsRecSet.Tables[6];

               if (dtterms.Rows.Count > 0)
                {
                    MailSubject.Append(dtterms.Rows[0][0].ToString());
                }
                if (dtterms.Rows.Count > 0)
                {
                    MailSubject = MailSubject.Replace("#Terms&Condition", dtterms.Rows[0]["FixedTermsCondition"].ToString());
                }
                else
                {
                    MailSubject = MailSubject.Replace("#Terms&Condition", string.Empty);
                }*/
                /*string strCancel = "select distinct tourno, noofdays,cast(percentage as int) as percentage from  CancelMaster(nolock) where tourno=" + TourNo + " order by noofdays desc";
                DataTable dtCan = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strCancel);*/
                #endregion
                //Banner Display
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
                dtCan = ldsRecSet.Tables[7];

                #region Commented
                /*if (dtCan.Rows.Count > 0)
                {
                    MailSubject.Append("<p class=\"hlinks\">&nbsp;");
                    MailSubject.Append("<font face=\"Arial\" color=\"red\"><b>Cancellation Charges For " + TourName + " :</b></font><br />");
                    for (int jj = 0; jj < dtCan.Rows.Count; jj++)
                    {
                        MailSubject.Append("<li></li>Before " + dtCan.Rows[jj]["noofdays"] + " Hours charges are " + dtCan.Rows[jj]["percentage"] + "% ");
                    }
                }*/
                #endregion
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
                MailSubject = MailSubject.Replace("#Terms&Condition", TermConditionText.ToString());
                MailSubject.Append("	</td>");
                MailSubject.Append("	</tr>");
                MailSubject.Append("	</table>");
                MailSubject.Append("</DIV>");
                lMailHtml.Append(MailSubject);
                string supportEmail = ConfigurationSettings.AppSettings["SupportEmail"].ToString();
                if (Toemail == orderid + "@temp.com")
                {
                    Toemail = supportEmail;
                }
                else if (Toemail.Length > 23)
                {
                    string k = Toemail.Substring(19, 4);
                    if (k == "temp")
                        Toemail = supportEmail;
                }
                try
                {
                    #region Commented
                    //MailMessage sendMail = new MailMessage();
                    //sendMail.To = Toemail;
                    //string ticketmail = ConfigurationSettings.AppSettings["ticketemail"].ToString();
                    //sendMail.Bcc = ticketmail;
                    //sendMail.From = "etickets@southerntravels.in";
                    //sendMail.Body = MailSubject.ToString();
                    //sendMail.BodyFormat = MailFormat.Html;
                    //sendMail.Subject = "Duplicate ticket- Southern Travels";
                    //SmtpMail.Send(sendMail);


                    // ClsCommon.sendmail(Toemail, ConfigurationSettings.AppSettings["ticketemailCar"].ToString(), "", "etickets@southerntravels.in", "Duplicate ticket- Southern Travels", MailSubject.ToString(), "");
                    #endregion
                    string eTicketsEmail= ConfigurationSettings.AppSettings["eTicketEmail"].ToString();
                    if (Toemail == "")
                        Toemail = ConfigurationSettings.AppSettings["SupportEmail"].ToString(); 

                    string tickeMailBCC = string.Empty;
                    if ((BranchCode == "EBK0001") && (Convert.ToInt32(agentid) == 0))
                    {
                        tickeMailBCC = ConfigurationSettings.AppSettings["iticketemailCar"].ToString();
                    }
                    else
                    {
                        tickeMailBCC = ConfigurationSettings.AppSettings["ticketemailCar"].ToString();
                    }
                    ClsCommon.sendmail(Toemail, tickeMailBCC, "", eTicketsEmail, "Duplicate ticket- Southern Travels", lMailHtml.ToString(), "");



                }
                catch (Exception ex)
                {
                    //Response.Write("<!-- " + ex.ToString() + " -->");
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
                if (dtprecan != null)
                {
                    dtprecan.Dispose();
                    dtprecan = null;
                }
                if (dtpreticket != null)
                {
                    dtpreticket.Dispose();
                    dtpreticket = null;
                }
                if (dtseats != null)
                {
                    dtseats.Dispose();
                    dtseats = null;
                }
                if (dtPay != null)
                {
                    dtPay.Dispose();
                    dtPay = null;
                }
                if (ldtHierarchy != null)
                {
                    ldtHierarchy.Dispose();
                    ldtHierarchy = null;
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
                if (dsTicketDetails != null)
                {
                    dsTicketDetails.Dispose();
                    dsTicketDetails = null;
                }
                if (dtETicketDetails != null)
                {
                    dtETicketDetails.Dispose();
                    dtETicketDetails = null;
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
            }
            return MailSubject;
        }
        protected string groupleader(string orderid)
        {
            ClsAdo cls = new ClsAdo();

            string grp = "";
            try
            {
                grp = cls.GetCustomerFullName(orderid);
                //grp = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select FirstName + ' ' + isnull(LastName,'') as Name1 from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid='" + orderid + "'");
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
        protected string CustAddress(string orderid)
        {
            string add = "";
            try
            {
                ClsAdo cls = new ClsAdo();
                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@orderid", orderid);
                add = cls.GetCustomerAddress(orderid);
                //add = DataLib.GetStringDataWithParam(DataLib.Connection.ConnectionString, "select Addr1 + ', ' + isnull(addr2,'') +', '+isnull(city,'')+', '+isnull(state,'') as address from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid=@orderid", param);
                add = add.Replace(", ,", "");
            }
            catch
            {
            }
            finally
            {
                if (add == "")
                    add = "";
            }
            return add;
        }
    }
}