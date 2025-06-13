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
    public partial class AgentCarDuplicateTicket : System.Web.UI.Page
    {

        ClsAdo pclsObj = new ClsAdo();
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

        string orderid, rmarks, sqlQuery = "", toorderidmail, TicketNo, BranchCode, TourName, PickUppoint, Name, BusNo, SeatNumbers;
        DateTime JourneyDate;
        decimal Amount, calTax = 0.0m, advancePaid = 0.0m, STaxValue, TotalAmount, cctax, strMinPay = 0.0m;
        StringBuilder strticket = new StringBuilder();
        string Toemail;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                if (!IsPostBack)
                {
                    //  BindData();


                }
                sDelete = tmpDelete.Value.ToString();
                if (sDelete == "1")
                {
                    sendMail();
                    tmpDelete.Value = "0";

                }
            }
            else
            {
                Response.Redirect("agentlogin.aspx");
            }
        }
        protected void BindData()
        {
            #region Optimize Code
            //dgDuplicateTickets.Dispose();
            /*string str = @"SELECT     tbl_CarBookings_Log.CabId, OnlineCustomer.FirstName + '/' + OnlineCustomer.Mobile AS Customer, CONVERT(varchar(10), 
                          tbl_CarBookings_Log.PickTupTime, 105) AS PickTupTime, CONVERT(varchar(10), tbl_CarBookings_Log.BookedOn, 105) AS BookedOn, 
                          tbl_CarBookings_Log.TicketNo, tbl_CarBookings_Log.NoOfPax, tbl_CarBookings_Log.TourName, OnlineCustomer.Mobile, OnlineCustomer.email
                          FROM         tbl_CarBookings_Log INNER JOIN
                          OnlineCustomer ON tbl_CarBookings_Log.customerrowid = OnlineCustomer.RowId
                          WHERE     (tbl_CarBookings_Log.TicketNo IS NOT NULL) AND (tbl_CarBookings_Log.PickTupTime >= CONVERT(varchar(10), GETDATE(), 101))
             * AND 
                          (tbl_CarBookings_Log.Cancelled <> '1' and tbl_CarBookings_Log.AgentId='" + Session["AgentId"] + "')";
            //string str = "select a.orderid,convert(varchar(10),a.doj,105) as doj, convert(varchar(10),a.dob,105) as dob,a.ticketcode
             * ,b.groupleader,b.agentid,b.noofseats,a.tourname from onlinetoursbooking a,ticketdetails b where a.ticketcode is not null and a.doj 
             * >=convert(varchar(10),getdate(),101) and a.ticketcode=b.ticketno and (b.cancelled<>'Y' or b.cancelled<>'y')";
            DataTable dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
            #endregion
            DataTable dt = pclsObj.fnGetAgentCarTourDupInfo(Convert.ToInt32(Session["AgentId"]));
            dgDuplicateTickets.DataSource = dt;
            //dgDuplicateTickets.DataBind();
            Globals.CheckData(ref dgDuplicateTickets, dt, ref lblMsg);
        }
        public void func()
        {
            #region Optimize Code
            /*string strSQL = "", strWhere = "";

            strSQL = @"SELECT     tbl_CarBookings_Log.CabId, OnlineCustomer.FirstName + '/' + OnlineCustomer.Mobile AS Customer, tbl_CarBookings_Log.PickTupTime, 
                          tbl_CarBookings_Log.BookedOn, tbl_CarBookings_Log.TicketNo, tbl_CarBookings_Log.NoOfPax, tbl_CarBookings_Log.TourName, 
                          OnlineCustomer.Mobile, OnlineCustomer.email, tbl_CarBookings_Log.customerrowid
                           FROM         tbl_CarBookings_Log INNER JOIN
                          OnlineCustomer ON tbl_CarBookings_Log.customerrowid = OnlineCustomer.RowId
                          WHERE     (tbl_CarBookings_Log.TicketNo IS NOT NULL) AND (tbl_CarBookings_Log.PickTupTime >= CONVERT(varchar(10), GETDATE(), 101)) AND 
                          (tbl_CarBookings_Log.Cancelled <> '1' and tbl_CarBookings_Log.AgentId='" + Session["AgentId"] + "')";
            strWhere = "";
            string pnr = txtPnrNo.Value.Trim();
            string ticket = txtticketno.Value.Trim();
            string contactno = txtContact.Value.Replace("'", "''").Trim();
            string emailid = txtEmail.Value.Replace("'", "''").Trim();
            string journeydate = txtjdate.Value;
            if (journeydate != "")
            {
                string[] dd = journeydate.Split('/');
                journeydate = dd[1] + "/" + dd[0] + "/" + dd[2];
            }
            if (pnr != "")
            {
                strWhere = strWhere + " and tbl_CarBookings_Log.CabId='" + pnr + "'";
            }
            if (ticket != "")
            {
                strWhere = strWhere + " and tbl_CarBookings_Log.TicketNo='" + ticket + "'";
            }

            if (contactno != "")
            {
                strWhere = strWhere + " and OnlineCustomer.Mobile='" + contactno + "'";
            }
            if (emailid != "")
            {
                strWhere = strWhere + " and OnlineCustomer.email='" + emailid + "'";
            }

            if (journeydate != "")
            {
                strWhere = strWhere + " and tbl_CarBookings_Log.PickTupTime='" + journeydate + "'";
            }

            strSQL = strSQL + strWhere;
            DataTable dtnew = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strSQL);*/
            #endregion
            string pnr = txtPnrNo.Value.Trim();
            string ticket = txtticketno.Value.Trim();
            string contactno = txtContact.Value.Replace("'", "''").Trim();
            string emailid = txtEmail.Value.Replace("'", "''").Trim();
            DateTime? lFromdate = null;
            if (txtjdate.Value != "")
            {
                string[] pJDate = Request[txtjdate.UniqueID].Split('/');
                lFromdate = new DateTime(Convert.ToInt32(pJDate[2]), Convert.ToInt32(pJDate[1]), Convert.ToInt32(pJDate[0]));
            }
            DataSet ldsRecSet = pclsObj.fnGetAgentCarTourDupTKT(Convert.ToString(Session["AgentId"]), pnr, ticket, contactno, emailid,
                 lFromdate);
            DataTable dtnew = new DataTable();
            if (ldsRecSet != null)
                dtnew = ldsRecSet.Tables[0];
            dgDuplicateTickets.DataSource = dtnew;
            //dgDuplicateTickets.DataBind();
            Globals.CheckData(ref dgDuplicateTickets, dtnew, ref lblMsg);
        }

        void sendMail()
        {

            //  bool issomedel=false;
            foreach (DataGridItem di in dgDuplicateTickets.Items)
            {
                bool CheckSelected;
                if (di.ItemType != ListItemType.Item || di.ItemType != ListItemType.AlternatingItem)
                {
                    CheckSelected = ((CheckBox)(di.Cells[0].FindControl("chkAccept"))).Checked;

                    if (CheckSelected == true)
                    {
                        string tkCode = Convert.ToString(dgDuplicateTickets.DataKeys[di.ItemIndex].ToString());
                        DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "SELECT  * FROM tbl_CarBookings_Log WHERE TicketNo ='" + tkCode + "'");
                        int i;

                        for (i = 0; i < dtTicketDetails.Rows.Count; i++)
                        {
                            TicketString(dtTicketDetails.Rows[i]["CabId"].ToString(), i + 1, Convert.ToInt32(dtTicketDetails.Rows[i]["customerrowid"]), Convert.ToDateTime(dtTicketDetails.Rows[i]["BookedOn"]));
                        }

                    }
                }
            }
            this.RegisterStartupScript("sdad", "<script>alert('Mail(s) sent successfully');</script>");

        }


        protected StringBuilder TicketString(string orderid, int i, int rowid, DateTime dob)
        {
            #region Optiomize Code
            //Toemail = DataLib.GetStringData(DataLib.Connection.ConnectionString, "SELECT     OnlineCustomer.email,tbl_CarBookings_Log.CabId FROM  tbl_CarBookings_Log INNER JOIN OnlineCustomer ON tbl_CarBookings_Log.customerrowid = OnlineCustomer.RowId where tbl_CarBookings_Log.customerrowid='" + rowid + "'");

            //string sqlemail = "SELECT     OnlineCustomer.email,tbl_CarBookings_Log.CabId FROM  tbl_CarBookings_Log INNER JOIN OnlineCustomer ON tbl_CarBookings_Log.customerrowid = OnlineCustomer.RowId where tbl_CarBookings_Log.customerrowid='" + rowid + "'";
            //DataTable dtemailid = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sqlemail);
            //if (dtemailid.Rows.Count > 0)
            //{
            //    Toemail = dtemailid.Rows[0]["email"].ToString().Trim();
            //    toorderidmail = dtemailid.Rows[0]["CabId"].ToString().Trim();
            //}
            #endregion
            string pCabID = "";
            int pStatus = pclsObj.fnGetEmailCarBooking(Convert.ToString(rowid), ref Toemail, ref pCabID);

            #region Oprimize Code
            /*SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cabid", orderid);
            sqlQuery = "sp_Agent_CarTicket";

            DataSet dtFare = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlQuery, param);*/
            #endregion
            DataTable ldtRecSet = pclsObj.fnGetAgent_CarTicket(orderid);
            DataSet dtFare = new DataSet();
            if (ldtRecSet != null)
                dtFare.Tables.Add(ldtRecSet);
            string Bookingdate = dtFare.Tables[0].Rows[0]["BookedOn"].ToString().Trim();
            string PNR = dtFare.Tables[0].Rows[0]["CabId"].ToString().Trim();
            string ticketno = dtFare.Tables[0].Rows[0]["TicketNo"].ToString().Trim();
            JourneyDate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["PickTupTime"].ToString());
            string cityname = dtFare.Tables[0].Rows[0]["CityName"].ToString().Trim();

            string transfername = dtFare.Tables[0].Rows[0]["TourName"].ToString().Trim();


            string agentid = Convert.ToString(dtFare.Tables[0].Rows[0]["AgentId"]);
            string cartype = dtFare.Tables[0].Rows[0]["Carname"].ToString().Trim();
            string pax = dtFare.Tables[0].Rows[0]["NoOfPax"].ToString().Trim();
            string pickup = dtFare.Tables[0].Rows[0]["PickupAddress"].ToString().Trim();
            string drop = dtFare.Tables[0].Rows[0]["DropOffAddress"].ToString().Trim();

            string customercontact = dtFare.Tables[0].Rows[0]["Customer"].ToString().Trim();
            string alternativephoneno = dtFare.Tables[0].Rows[0]["altercontact"].ToString().Trim();
            string customeraddress = dtFare.Tables[0].Rows[0]["Addr1"].ToString().Trim();

            decimal Fare = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Fare"].ToString().Trim());
            decimal stax = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["STax"].ToString().Trim());
            decimal sTotalamount = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["NetTotal"].ToString().Trim());

            decimal advance = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Advance"].ToString().Trim());
            string sbalance = "Nil";
            string sbranch = dtFare.Tables[0].Rows[0]["branchCode"].ToString();
            string suserid = dtFare.Tables[0].Rows[0]["agentid"].ToString();

            string sbranchcode = "";
            string sbranchname = "";


            decimal ccValue = 0.0m;
            if (Convert.ToString(Session["CarUserMode"]) == "User")
                ccValue = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["CC"].ToString());

            float stValueAmt = float.Parse(dtFare.Tables[0].Rows[0]["STax"].ToString());



            int g = 1;
            StringBuilder lMailHtml = new StringBuilder();
            lMailHtml.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            lMailHtml.Append("<TR><TD align=\"center\"><b>");
            lMailHtml.Append("PLEASE DO NOT REPLY TO THIS MAIL AS THIS IS AN AUTO GENERATED MAIL AND REPLIES / QUERIES TO THIS EMAIL ID ARE NOT ATTENDED. FOR ANY ASSISTANCE / QUERIES PLEASE MAIL US AT <a href=\"mailto:support@southerntravels.in\" title='' >support@southerntravels.in</a>");
            lMailHtml.Append("</b></TD></TR></TABLE>");

            StringBuilder TicketStr = new StringBuilder();
            TicketStr.Append("<Table id=Table2 cellSpacing=0 cellPadding=0 width=95% border=0>");
            //TicketStr.Append("<TR>");
            //TicketStr.Append("<TD>");
            //TicketStr.Append("<table width=100%>");
            // TicketStr.Append("<tr>");
            // TicketStr.Append("<td width=40% align=right>");
            // TicketStr.Append("<INPUT class=intdtxth id=Button1 onclick=framePrint('print_area" + g + "');  type=button value=Print name=Button1>");
            // TicketStr.Append("</TD>");
            // TicketStr.Append("</TR>");
            // TicketStr.Append("</Table>");
            //TicketStr.Append("</TR>");
            // TicketStr.Append("</TD>");
            TicketStr.Append("<TR>");
            TicketStr.Append("<TD>");
            TicketStr.Append("<DIV id=print_area" + g + ">");

            StringBuilder strTable = new StringBuilder();
            strTable.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\CarTicket.html")));

            strTable = strTable.Replace("#bookingdate", dtFare.Tables[0].Rows[0]["BookedOn"].ToString());
            strTable = strTable.Replace("#pnrno", dtFare.Tables[0].Rows[0]["CabId"].ToString());
            strTable = strTable.Replace("#ticketno", dtFare.Tables[0].Rows[0]["TicketNo"].ToString());
            strTable = strTable.Replace("#jdate", JourneyDate.ToString("dd-MMM-yyyy"));
            strTable = strTable.Replace("#departTime", JourneyDate.ToShortTimeString());
            strTable = strTable.Replace("#city", dtFare.Tables[0].Rows[0]["CityName"].ToString());
            strTable = strTable.Replace("#tourname", dtFare.Tables[0].Rows[0]["TourName"].ToString().Trim());
            strTable = strTable.Replace("#vehicletype", dtFare.Tables[0].Rows[0]["Carname"].ToString());
            strTable = strTable.Replace("#pax", dtFare.Tables[0].Rows[0]["NoOfPax"].ToString());
            strTable = strTable.Replace("#Pickupaddre", dtFare.Tables[0].Rows[0]["PickupAddress"].ToString());
            strTable = strTable.Replace("#dropaddre", dtFare.Tables[0].Rows[0]["DropOffAddress"].ToString());
            strTable = strTable.Replace("#custContact", dtFare.Tables[0].Rows[0]["Customer"].ToString());
            strTable = strTable.Replace("#custaddress", dtFare.Tables[0].Rows[0]["Addr1"].ToString());
            strTable = strTable.Replace("#alterContact", dtFare.Tables[0].Rows[0]["altercontact"].ToString());



            strTable = strTable.Replace("#fare", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Fare"].ToString()))));
            strTable = strTable.Replace("#tax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STax"].ToString()))));

            strTable = strTable.Replace("#totalamount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["NetTotal"].ToString()))));
            strTable = strTable.Replace("#advance", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Advance"].ToString()))));
            strTable = strTable.Replace("#balance", "Nil");

            strTable = strTable.Replace("#branchCode", dtFare.Tables[0].Rows[0]["branchCode"].ToString());
            string sagentuserid = Convert.ToString(Session["UserId"]);
            strTable = strTable.Replace("#branchUser", sagentuserid);
            decimal dis = 0;
            if (Convert.ToString(dtFare.Tables[0].Rows[0]["discount"]) != "")
                dis = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["discount"]);
            string PaymentMode = "AgentCash";
            string chknumber = "";
            string bank = "";
            string chqdate = "";

            string bookMode = "";
            if ((Convert.ToString(dtFare.Tables[0].Rows[0]["branchCode"]) != "EBK0001") && (Convert.ToString(dtFare.Tables[0].Rows[0]["agentid"]) == "0"))
            {
                bookMode = "Branch";
            }

            if ((dis) > 0)
            {
                strTable = strTable.Replace("#lbldiscount", "Discount:");
                strTable = strTable.Replace("#discount", Convert.ToString(decimal.Round(dis)));
                strTable = strTable.Replace("#amount", Convert.ToString(decimal.Round(Amount + dis)));

            }
            else
            {

                strTable = strTable.Replace("#lbldiscount", "");
                strTable = strTable.Replace("#discount", "");
                strTable = strTable.Replace("#Amount", Convert.ToString(decimal.Round(Amount)));

            }

            strTable = strTable.Replace("#lblServiceTax", "GST : ");
            strTable = strTable.Replace("#tax", Convert.ToString(dtFare.Tables[0].Rows[0]["stax"]));
            strTable = strTable.Replace("#BookingExecutive", "Booking Executive: ");
            strTable = strTable.Replace("#branchUser", dtFare.Tables[0].Rows[0]["agentid"].ToString());

            strTable = strTable.Replace("#totalamount", Convert.ToString(decimal.Round(sTotalamount)));
            if ((advance) > 0)
            {
                strTable = strTable.Replace("#advance", Convert.ToString(decimal.Round(advance)));
            }
            else
            {
                strTable = strTable.Replace("#advance", "Nil");
            }
            if (decimal.Round((sTotalamount - advance)) > 0)
            {
                strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(sTotalamount - advance)));
            }
            else
            {
                strTable = strTable.Replace("#balance", "Nil");
            }

            if (PaymentMode.Trim().Length < 3)
            {
                DataSet ldsRecSet = pclsObj.fnGetPayModeOrTerm(ticketno);
                /*string ss = "select Paymentmode from tbl_paymodes where rowid='" + PaymentMode + "'";
                string pm = DataLib.GetStringData(DataLib.Connection.ConnectionString, ss);*/
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


            TicketStr.Append(strTable);


            TicketStr.Append("</DIV>");

            TicketStr.Append("</TD>");

            TicketStr.Append("</TR>");

            TicketStr.Append("</Table>"); lMailHtml.Append(TicketStr);

            if (Toemail == orderid + "@temp.com")
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
                //if (Toemail == "")
                //    Toemail = "support@southerntravels.in";

                //sendMail.To = Toemail;
                //if ((BranchCode == "EBK0001") && (Convert.ToInt32(agentid) == 0))
                //{
                //    string iticketmail = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                //    sendMail.Bcc = iticketmail;
                //}
                //else
                //{
                //    string ticketmail = ConfigurationSettings.AppSettings["ticketemail"].ToString();
                //    sendMail.Bcc = ticketmail;
                //}
                //sendMail.From = "etickets@southerntravels.in";
                //sendMail.Body = TicketStr.ToString();
                //sendMail.BodyFormat = MailFormat.Html;
                //sendMail.Subject = "Duplicate ticket- Southern Travels";
                //SmtpMail.Send(sendMail);

                if (Toemail == "")
                    Toemail = "support@southerntravels.in";

                string tickeMailBCC = string.Empty;
                if ((BranchCode == "EBK0001") && (Convert.ToInt32(agentid) == 0))
                {
                    tickeMailBCC = ConfigurationSettings.AppSettings["iticketemail"].ToString();
                }
                else
                {
                    tickeMailBCC = ConfigurationSettings.AppSettings["ticketemail"].ToString();
                }
                ClsCommon.sendmail(Toemail, tickeMailBCC, "", "etickets@southerntravels.in", "Duplicate ticket- Southern Travels", lMailHtml.ToString(), "");

            }
            catch (Exception ex)
            {
                //Response.Write("<!-- " + ex.ToString() + " -->");
            }
            finally
            {

            }

            return TicketStr;
        }

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

        protected string CustAddress(string orderid)
        {
            string add = "";
            try
            {
                //grp = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select FirstName + ' ' + isnull(LastName,'') as Name1 from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid='" + orderid + "'");
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@orderid", orderid);
                add = DataLib.GetStringDataWithParam(DataLib.Connection.ConnectionString, "select Addr1 + ', ' + isnull(addr2,'') +', '+isnull(city,'')+', '+isnull(state,'') as address from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.RowID=OnlineCustByOrder.OCustRowID where OnlineCustByOrder.orderid=@orderid", param);
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


        protected void Submit_Click(object sender, EventArgs e)
        {
            func();
        }
        protected void dgDuplicateTickets_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgDuplicateTickets.CurrentPageIndex = e.NewPageIndex;
            //BindData();
            func();
        }
        protected void dgDuplicateTickets_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView rv = (DataRowView)e.Item.DataItem;
                // DataView vw =(DataView)e.Item.DataItem;
                string k = Convert.ToString(rv.Row["TicketNo"]);


                if (k.Substring(0, 3) == "EBK")
                {

                    e.Item.Cells[6].Text = "Bus Ticket Booking";
                }
                if (k.Substring(0, 3) == "CAB")
                {

                    e.Item.Cells[6].Text = "Car Booking";
                }

                if (k.Substring(0, 3) == "STR")
                {

                    e.Item.Cells[6].Text = "Hotel Booking";
                }

                if (k.Substring(0, 3) == "SPL")
                {

                    e.Item.Cells[6].Text = "Special Booking";
                }


            }

        }
    }
}