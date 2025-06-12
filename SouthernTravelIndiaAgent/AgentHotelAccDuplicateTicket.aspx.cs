using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentHotelAccDuplicateTicket : System.Web.UI.Page
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


        StringBuilder strticket = new StringBuilder();

        protected int sDeluxe, ssuperdeluxe, sexecutive, sroyalsouthern;
        protected string snoofpersons, TypeofRooms = "", shddeluxe, shdsuperdeluxe, shdexecutive, shdroyal, rommrentperday;
        string orderid;
        DateTime sBookingDate;
        DateTime srrivaldate, sdeparturedate;
        decimal sfareor;
        ClsAdo pclsObj = null;
        STSPLOrOther pvClaSpl = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                if (!IsPostBack)
                {
                    //BindData();
                }
                sDelete = tmpDelete.Value.ToString();
                if (sDelete == "1")
                {
                    // BindData();
                    sendMail();
                    tmpDelete.Value = "0";

                }
            }
            else
            {
                Response.Redirect("agentlogin.aspx");
            }

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            func();
        }
        public void func()
        {

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
            pvClaSpl = null;
            DataSet ldsRecSet = null;
            DataTable dtnew = null;
            try
            {
                pclsObj = new ClsAdo();
                ldsRecSet = pclsObj.fnAgentHTLAccDupTKT(pnr, ticket, contactno, emailid, lFromdate, Convert.ToString(Session["AgentId"]));
                dtnew = new DataTable();
                if (ldsRecSet != null)
                    dtnew = ldsRecSet.Tables[0];
                if (dtnew.Rows.Count > 0)
                {
                    ViewState["orderid"] = dtnew.Rows[0]["OrderId"].ToString();
                    dgDuplicateTickets.DataSource = dtnew;
                    //dgDuplicateTickets.DataBind();
                    Globals.CheckData(ref dgDuplicateTickets, dtnew, ref lblMsg);
                }
                else
                {
                    lblMsg.Text = "No Data Found !";
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
                }
                if (dtnew != null)
                {
                    dtnew = null;
                }
            }
        }

        protected void dgDuplicateTickets_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgDuplicateTickets.CurrentPageIndex = e.NewPageIndex;

            func();
        }
        protected void dgDuplicateTickets_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int AccID = Convert.ToInt32(((System.Data.DataRowView)(e.Item.DataItem)).Row["RowId"]);
                DataTable dtHtNoroom = null;
                pclsObj = null;
                try
                {
                    pclsObj = new ClsAdo();
                    dtHtNoroom = pclsObj.fnGetRoomFareWithRoomType(AccID);
                }
                finally
                {
                    if (pclsObj != null)
                    {
                        pclsObj = null;
                    }
                }
                bool flag = false;
                string TypeofRooms = "";
                if (dtHtNoroom.Rows.Count > 0)
                {
                    for (int k = 0; k < dtHtNoroom.Rows.Count; k++)
                    {
                        if (flag == false)
                        {
                            if (dtHtNoroom.Rows[k]["noofroom"] != null && Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"]) > 0)
                            {
                                TypeofRooms = dtHtNoroom.Rows[k]["RoomType"].ToString() + ", ";
                                flag = true;
                            }
                        }
                        else
                        {
                            if (dtHtNoroom.Rows[k]["noofroom"] != null && Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"]) > 0)
                            {
                                TypeofRooms = TypeofRooms + dtHtNoroom.Rows[k]["RoomType"].ToString() + ", ";

                            }
                        }

                    }

                }
                TypeofRooms = TypeofRooms.Trim();
                e.Item.Cells[5].Text = TypeofRooms.TrimEnd(',');
            }

            //if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            //{
            //    DataRowView rv = (DataRowView)e.Item.DataItem;
            //    // DataView vw =(DataView)e.Item.DataItem;
            //    string k = Convert.ToString(rv.Row["ticketno"]);


            //    if (k.Substring(0, 3) == "EBK")
            //    {

            //        e.Item.Cells[6].Text = "Bus Ticket Booking";
            //    }
            //    if (k.Substring(0, 3) == "CAB")
            //    {

            //        e.Item.Cells[6].Text = "Car Booking";
            //    }

            //    if (k.Substring(0, 3) == "STR")
            //    {

            //        e.Item.Cells[6].Text = "Hotel Booking";
            //    }

            //    if (k.Substring(0, 3) == "SPL")
            //    {

            //        e.Item.Cells[6].Text = "Special Booking";
            //    }

            //    //if (k.Substring(0, 3) == "HAC")
            //    //{

            //    //    e.Item.Cells[6].Text = "Hotel Packge";
            //    //}

            //}
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
                        DataTable dtTicketDetails = null;
                        try
                        {
                            pclsObj = new ClsAdo();
                            dtTicketDetails = pclsObj.fnGetAccDattTOMail(tkCode);
                        }
                        finally
                        {
                            if (pclsObj != null)
                            {
                                pclsObj = null;
                            }
                            if (dtTicketDetails != null)
                            {
                                dtTicketDetails.Dispose();

                            }
                        }
                        int i;
                        for (i = 0; i < dtTicketDetails.Rows.Count; i++)
                        {
                            TicketString(dtTicketDetails.Rows[i]["OrderId"].ToString(), i + 1, Convert.ToInt32(dtTicketDetails.Rows[i]["CustRowId"]),
                                Convert.ToDateTime(dtTicketDetails.Rows[i]["BookingDate"]), Convert.ToInt32(dtTicketDetails.Rows[i]["RowId"]));
                        }

                    }
                }
            }
            this.RegisterStartupScript("sdad", "<script>alert('Mail(s) sent successfully');</script>");

        }
        protected StringBuilder TicketString(string orderid, int i, int rowid, DateTime dob, int id)
        {
            orderid = ViewState["orderid"].ToString();
            string sduplicate = Convert.ToString(Request.QueryString["duplicate"]);
            StringBuilder lMailHtml = new StringBuilder();
            lMailHtml.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            lMailHtml.Append("<TR><TD align=\"center\"><b>");
            lMailHtml.Append("PLEASE DO NOT REPLY TO THIS MAIL AS THIS IS AN AUTO GENERATED MAIL AND REPLIES / QUERIES TO THIS EMAIL ID ARE NOT ATTENDED. FOR ANY ASSISTANCE / QUERIES PLEASE MAIL US AT <a href=\"mailto:support@southerntravels.in\" title='' >support@southerntravels.in</a>");
            lMailHtml.Append("</b></TD></TR></TABLE>");
            pvClaSpl = null;
            pclsObj = null;
            DataTable ldtRec = null;
            DataSet dtFare = null;
            DataSet ldsRecSet = null; DataTable dtterm = null;
            try
            {
                pclsObj = new ClsAdo();
                pvClaSpl = new STSPLOrOther();
                ldtRec = pclsObj.fnHotelReceipt_Details(orderid);
                dtFare = new DataSet();
                if (ldtRec != null)
                {
                    dtFare.Tables.Add(ldtRec);
                }

                sBookingDate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["BookingDate"].ToString().Trim());
                string sorderid = dtFare.Tables[0].Rows[0]["OrderId"].ToString().Trim();

                string snoofadults = Convert.ToString(dtFare.Tables[0].Rows[0]["noofadults"]);
                decimal snoofchildren = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["noofchildren"].ToString().Trim());

                snoofpersons = snoofadults + "Adults";
                if (snoofchildren > 0)
                {
                    snoofpersons = snoofadults + "Aults" + "+" + Convert.ToString(snoofchildren) + "childerns";

                }




                string snoofrooms = dtFare.Tables[0].Rows[0]["noofrooms"].ToString().Trim();
                string sticketno = dtFare.Tables[0].Rows[0]["ticketno"].ToString().Trim();

                decimal Fare = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Totalamount"].ToString().Trim());
                decimal stax = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["STaxValue"].ToString().Trim());
                decimal stotalwithtax = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["AmtWithTax"].ToString().Trim());

                decimal advance = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Advance"].ToString().Trim());
                decimal sbalance = Convert.ToDecimal(stotalwithtax - advance);

                DataTable dtHtNoroom = pclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(id));
                bool flag = false;
                int noofroom = 0;
                if (dtHtNoroom.Rows.Count > 0)
                {
                    for (int k = 0; k < dtHtNoroom.Rows.Count; k++)
                    {
                        if (flag == false)
                        {
                            TypeofRooms = dtHtNoroom.Rows[k]["RoomType"].ToString();
                            rommrentperday = dtHtNoroom.Rows[k]["noofroom"].ToString() + "x" + dtHtNoroom.Rows[k]["Fare"].ToString();
                            //hotelRoomtypes = ddlNoRoom.SelectedValue + "x" + lblrtype.Text;
                            flag = true;
                        }
                        else
                        {
                            TypeofRooms = TypeofRooms + "+" + dtHtNoroom.Rows[k]["RoomType"].ToString();
                            rommrentperday = rommrentperday + "+" + dtHtNoroom.Rows[k]["noofroom"].ToString() + "x" + dtHtNoroom.Rows[k]["Fare"].ToString();
                            //hotelRoomtypes = hotelRoomtypes + "+" + ddlNoRoom.SelectedValue + "x" + lblrtype.Text;
                        }
                        noofroom = noofroom + Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"].ToString());
                    }

                }




                int j = 1;
                StringBuilder TicketStr = new StringBuilder();
                TicketStr.Append("<Table id=Table2 cellSpacing=0 cellPadding=0 width=95% border=0>");
                TicketStr.Append("<TR>");
                TicketStr.Append("<TD>");
                TicketStr.Append("<table width=100%>");
                TicketStr.Append("<tr>");
                TicketStr.Append("<td width=40% align=right>");
                TicketStr.Append("<INPUT class=intdtxth id=Button1 onclick=framePrint('print_area" + j + "');  type=button value=Print name=Button1>");
                TicketStr.Append("</TD>");
                TicketStr.Append("</TR>");
                TicketStr.Append("</Table>");
                TicketStr.Append("</TR>");
                TicketStr.Append("</TD>");
                TicketStr.Append("<TR>");
                TicketStr.Append("<TD>");
                TicketStr.Append("<DIV id=print_area" + j + ">");

                StringBuilder strTable = new StringBuilder();
                strTable.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\HotelReciept.html")));
                strTable = strTable.Replace("#arrival", srrivaldate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#arrtime", srrivaldate.ToShortTimeString());
                strTable = strTable.Replace("#departure", sdeparturedate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#departime", sdeparturedate.ToShortTimeString());


                strTable = strTable.Replace("#vocher", dtFare.Tables[0].Rows[0]["ticketno"].ToString());
                strTable = strTable.Replace("#name", dtFare.Tables[0].Rows[0]["FirstName"].ToString());
                strTable = strTable.Replace("#mob", dtFare.Tables[0].Rows[0]["Mobile"].ToString());
                strTable = strTable.Replace("#add", dtFare.Tables[0].Rows[0]["Addr1"].ToString());
                strTable = strTable.Replace("#cdate", sBookingDate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#City", "New Delhi");
                strTable = strTable.Replace("#Typeofroom", dtFare.Tables[0].Rows[0]["BookingRoomTypes"].ToString());
                strTable = strTable.Replace("#noofper", snoofpersons.ToString());
                strTable = strTable.Replace("#rooms", dtFare.Tables[0].Rows[0]["noofrooms"].ToString());

                flag = false;
                noofroom = 0;
                int ExtraBed = 0;
                decimal ExtraFare = 0;
                StringBuilder lRoomType = new StringBuilder();
                lRoomType.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");


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
                                //hotelRoomtypes = ddlNoRoom.SelectedValue + "x" + lblrtype.Text;
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
                                //hotelRoomtypes = hotelRoomtypes + "+" + ddlNoRoom.SelectedValue + "x" + lblrtype.Text;
                            }
                        }
                        noofroom = noofroom + Convert.ToInt32(dtHtNoroom.Rows[k]["noofroom"].ToString());
                        if (ExtraBed > 0)
                        {
                            lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + dtHtNoroom.Rows[k]["RoomType"].ToString() + " " +
                                dtHtNoroom.Rows[k]["noofroom"].ToString() + " <b>x</b> " + dtHtNoroom.Rows[k]["Fare"].ToString() + " + " +
                                ExtraFare + " (Extra Bed " + ExtraBed1 + ")" + "</span></TD></TR>");
                        }
                        else
                        {
                            lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + dtHtNoroom.Rows[k]["RoomType"].ToString() + " " +
                                dtHtNoroom.Rows[k]["noofroom"].ToString() + " <b>x</b> " + dtHtNoroom.Rows[k]["Fare"].ToString() + "</span></TD></TR>");
                        }
                    }

                }
                lRoomType.Append("</TABLE>");

                strTable = strTable.Replace("#rentperday", lRoomType.ToString());

                strTable = strTable.Replace("#amount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Totalamount"].ToString()))));
                //strTable = strTable.Replace("#jdate", JourneyDate.ToString("dd-MMM-yyyy"));
                //strTable = strTable.Replace("#departTime", JourneyDate.ToShortTimeString());
                strTable = strTable.Replace("#stax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STaxValue"].ToString()))));
                //strTable = strTable.Replace("#tourname", strans.ToString());
                strTable = strTable.Replace("#Totawithtax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["AmtWithTax"].ToString()))));
                //strTable = strTable.Replace("#discount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Discount"].ToString()))));
                strTable = strTable.Replace("#advance", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Advance"].ToString()))));

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

                // string sfaredyna = Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Fare"].ToString())));
                string sfaredyna = Convert.ToString(Math.Round(float.Parse(sfareor.ToString())));
                string staxdyna = Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STaxValue"].ToString())));
                StringBuilder strTable1 = new StringBuilder();

                if ((dis) > 0)
                {
                    strTable1.Append("<tr>");
                    strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                    strTable1.Append(" <span class=cgi>Fare&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + sfaredyna + "</span>");
                    strTable1.Append("</td>");
                    strTable1.Append("<td style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                    strTable1.Append("<span class=cgi>Discount&nbsp;&nbsp;Rs&nbsp;:</span><span class=hlinks>" + dis + "</span>");
                    strTable1.Append("</td>");
                    strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                    strTable1.Append(" <span class=cgi>GST&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + staxdyna + "</span>");
                    strTable1.Append("</td>");
                    strTable1.Append("</tr");
                    // sdiscountdynamic = strTable1.ToString();
                    strTable = strTable.Replace("#amount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Totalamount"].ToString()))));
                }
                else
                {
                    strTable1.Append("<tr>");
                    strTable1.Append("<td  style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                    strTable1.Append(" <span class=cgi>Fare &nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + sfaredyna + "</span>");
                    strTable1.Append("</td>");

                    strTable1.Append("<td colspan=2 style=\"font-family:Verdana, Arial, Helvetica; line-height:1.4; color:#000000;font-size:12px\">");
                    strTable1.Append(" <span class=cgi>GST&nbsp;&nbsp;Rs&nbsp;:</span> <span class=hlinks>" + staxdyna + "</span>");
                    strTable1.Append("</td>");
                    strTable1.Append("</tr");
                    //sdiscountdynamic = strTable1.ToString();
                }
                //strTable = strTable.Replace("#trinserDynamic", sdiscountdynamic.ToString());
                strTable = strTable.Replace("#lblServiceTax", "GST : ");
                strTable = strTable.Replace("#tax", Convert.ToString(dtFare.Tables[0].Rows[0]["STaxValue"]));
                strTable = strTable.Replace("#BookingExecutive", "Booking Executive: ");
                strTable = strTable.Replace("#branchUser", dtFare.Tables[0].Rows[0]["Username"].ToString());
                strTable = strTable.Replace("#totalamount", Convert.ToString(decimal.Round(stotalwithtax)));
                strTable = strTable.Replace("#noofdays", Convert.ToString(dtFare.Tables[0].Rows[0]["noofdays"].ToString()));

                if (decimal.Round((stotalwithtax - advance)) > 0)
                {
                    strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(stotalwithtax - advance)));
                }
                else
                {
                    strTable = strTable.Replace("#balance", "Nil");
                }
                ldsRecSet = pclsObj.fnGetPayModeOrTerm(sticketno);
                if (PaymentMode.Trim().Length < 3)
                {
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
                //888888888

                TicketStr.Append(strTable);
                /*string str = "SELECT HotelTermsCondition FROM CompanyDetailsForRpt";
                DataTable dtterm = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
                dtterm = ldsRecSet.Tables[1];

                if (dtterm.Rows.Count > 0)
                {
                    //terms.InnerHtml = dtterm.Rows[0]["CarTermsCondition"].ToString();
                    TicketStr.Append(dtterm.Rows[0]["HotelTermsCondition"].ToString());

                }
                TicketStr.Append("</DIV></TD></TR></Table>"); lMailHtml.Append(TicketStr);
                SendMail(dtFare.Tables[0].Rows[0]["email"].ToString(), "etickets@southerntravels.in", "", "Duplicate Accommodation Reservation - Southern Travels", lMailHtml.ToString(), "");
                return TicketStr;
            }
            finally
            {
                if (pvClaSpl != null)
                {
                    pvClaSpl = null;
                }
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ldtRec != null)
                {
                    ldtRec.Dispose();
                    ldtRec = null;
                }
                if (dtFare != null)
                {
                    dtFare.Dispose();
                    dtFare = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtterm != null)
                {
                    dtterm.Dispose();
                    dtterm = null;
                }
            }
        }
        private void SendMail(string To, string From, string Bcc, string subject, string body, string cc)
        {
            string strRetMail = "";
            if (Convert.ToString(ViewState["agentid"]) != "")
            {
                try
                {
                    pclsObj = new ClsAdo();
                    strRetMail = pclsObj.fnGetEmailIDAgent(Convert.ToInt32(ViewState["agentid"]));
                }
                finally
                {
                    if (pclsObj != null)
                    {
                        pclsObj = null;
                    }
                }
            }
            if (Convert.ToString(ViewState["branchcode"]) != "")
            {
                try
                {
                    pclsObj = new ClsAdo();
                    strRetMail = strRetMail + "," + pclsObj.fnEmailSecurity(Convert.ToString(ViewState["branchcode"]), Convert.ToString(ViewState["username"]));
                }
                finally
                {
                    if (pclsObj != null)
                    {
                        pclsObj = null;
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

                string ticketMailBCC = string.Empty;

                if (To != "")
                {
                    if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                    {
                        ticketMailBCC = ConfigurationSettings.AppSettings["iticketemailAcc"].ToString();
                    }
                    else
                    {
                        string ticketmail = ConfigurationSettings.AppSettings["ticketemailAcc"].ToString();
                        string ss = strRetMail + "," + ticketmail;
                        ticketMailBCC = ss.TrimStart(',').TrimEnd(',');
                    }
                }
                else
                {
                    To = "support@southerntravels.in";
                    if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                    {
                        ticketMailBCC = ConfigurationSettings.AppSettings["iticketemailAcc"].ToString();
                    }
                    else
                    {
                        string ticketmail = ConfigurationSettings.AppSettings["ticketemailAcc"].ToString();
                        string ss = strRetMail + "," + ticketmail;
                        ticketMailBCC = ss.TrimStart(',').TrimEnd(',');
                    }
                }
                ClsCommon.sendmail(To, ticketMailBCC, "", "etickets@southerntravels.in", subject, body, "");
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
    }
}