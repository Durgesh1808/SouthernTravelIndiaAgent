using SouthernTravelIndiaAgent.BAL;
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
    public partial class Hotel_Reciept : System.Web.UI.Page
    {
        #region "Member variables"
        protected int sDeluxe, ssuperdeluxe, sexecutive, sroyalsouthern;
        protected string orderid, sqlQuery, snoofpersons, TypeofRooms = "", shddeluxe, shdsuperdeluxe, shdexecutive, shdroyal, rommrentperday, turms;
        protected StringBuilder strticket = new StringBuilder();
        DateTime sBookingDate, sdepttime, sarrtime;
        DateTime srrivaldate, sdeparturedate;
        decimal sfareor;
        ClsAdo pclsObj = null;

        #endregion
        #region "Event's"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }

            orderid = Request.QueryString["orderid"].ToString();
            if (!IsPostBack)
            {
                processHotelReceipt();

            }
            Session["sendmail"] = "no";
        }
        #endregion
        #region "Methods"
        private void processHotelReceipt()
        {
            strticket.Append(HotelReceiptticket(orderid));

        }
        private StringBuilder HotelReceiptticket(string orderid)
        {
            orderid = Request.QueryString["orderid"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string sduplicate = Convert.ToString(Request.QueryString["duplicate"]);

            /*SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@OrderId", orderid);
            sqlQuery = "sp_HotelReceipt_Details";

            DataSet dtFare = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlQuery, param);*/
            pclsObj = null;
            DataTable dtterm = null, dtHtNoroom = null, ldtRecSet = null;
            DataSet dtFare = null, ldsRecSet = null;
            StringBuilder lMailHtml = new StringBuilder();
            lMailHtml.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            lMailHtml.Append("<TR><TD align=\"center\"><b>");
            lMailHtml.Append("PLEASE DO NOT REPLY TO THIS MAIL AS THIS IS AN AUTO GENERATED MAIL AND REPLIES / QUERIES TO THIS EMAIL ID ARE NOT ATTENDED. FOR ANY ASSISTANCE / QUERIES PLEASE MAIL US AT <a href=\"mailto:support@southerntravels.in\" title='' >support@southerntravels.in</a>");
            lMailHtml.Append("</b></TD></TR></TABLE>");
            try
            {
                pclsObj = new ClsAdo();
                ldtRecSet = pclsObj.fnHotelReceipt_Details(orderid);
                dtFare = new DataSet();
                if (ldtRecSet != null)
                {
                    dtFare.Tables.Add(ldtRecSet);
                }
                //DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sql);
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
                //string orderid = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["orderid"].ToString().Trim());
                DateTime Bookingdate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["Bookingdate"].ToString().Trim());

                string pickveh, pickvehno, pickuptime, dropveh, dropvehno, droptime, PickupFrom = "", DropAt = "";
                string strPkStation = "", strDPStation = "";
                string lHotelID = Convert.ToString(dtFare.Tables[0].Rows[0]["HotelID"]);
                pickveh = Convert.ToString(dtFare.Tables[0].Rows[0]["PickUpVeh"]);
                pickvehno = Convert.ToString(dtFare.Tables[0].Rows[0]["PickVehNo"]);
                pickuptime = Convert.ToString(dtFare.Tables[0].Rows[0]["PickTime"]);
                dropveh = Convert.ToString(dtFare.Tables[0].Rows[0]["DropVeh"]);
                dropvehno = Convert.ToString(dtFare.Tables[0].Rows[0]["DropVehNo"]);
                droptime = Convert.ToString(dtFare.Tables[0].Rows[0]["DropTime"]);
                strPkStation = Convert.ToString(dtFare.Tables[0].Rows[0]["pkstation"]);
                strDPStation = Convert.ToString(dtFare.Tables[0].Rows[0]["station"]);
                string rmarks = Convert.ToString(dtFare.Tables[0].Rows[0]["Remarks"]);

                try
                {
                    if (Session["BranchId"] != null)
                    {
                        pclsObj = new ClsAdo();
                        string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        int Val = pclsObj.fnSaveBranchToAgentBooking(sticketno, Convert.ToInt32(Session["BranchUserId"]),
                            Convert.ToInt32(Session["AgentId"]), REMOTE_ADDR);
                    }
                }
                catch { }


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
                /*SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@AccmBookID", Convert.ToInt32(Session["AccID"]));

                //DataTable dtHtNoroom = DataLib.GetDataTableWithparamSP(DataLib.Connection.ConnectionString, "sp_GetRoomFareWithRoomType",param1);*/

                // dtHtNoroom = pclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(dtFare.Tables[0].Rows[0]["RowID"]));
                if (Convert.ToInt32(lHotelID) > 0)
                {
                    dtHtNoroom = pclsObj.fnGetRoomTypeOccupancyNew(Convert.ToInt32(dtFare.Tables[0].Rows[0]["RowID"]));
                }
                else
                {

                    dtHtNoroom = pclsObj.fnGetRoomFareWithRoomType(Convert.ToInt32(dtFare.Tables[0].Rows[0]["RowID"]));
                }

                bool flag = false;
                int noofroom = 0;
                int ExtraBed = 0;
                decimal ExtraFare = 0;
                StringBuilder lRoomType = new StringBuilder();
                int totalCWB = 0;
                decimal CWBFare = 0;
                lRoomType.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");

                // totalCWB = Convert.ToInt32(dtHtNoroom.Rows[0]["TotalChildWB"]);
                // CWBFare = Convert.ToInt32(dtHtNoroom.Rows[0]["ChildWbFare"]);
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
                        lRoomType.Append("<TD  align=\"left\"><span style=\"font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000; font-size: 12px\">&nbsp; " + "+" + Convert.ToString(CWBFare * totalCWB) + " Inclusive of GSTes (" + "Child WB&nbsp;&nbsp;" + Convert.ToString(totalCWB) + ")" + "</span></TD>");
                    }
                    lRoomType.Append("</TR>");

                }
                lRoomType.Append("</TABLE>");
                srrivaldate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["arrtime"].ToString().Trim());
                sdeparturedate = Convert.ToDateTime(dtFare.Tables[0].Rows[0]["depttime"].ToString().Trim());

                #region Commented
                //string strpickup = "SELECT RoomTypeId, RoomType, Fare FROM  HotelRoomTypes_tbl";
                //DataTable dtpickup = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strpickup);
                //if (dtpickup.Rows.Count > 0)
                //{
                //    shddeluxe = dtpickup.Rows[0]["Fare"].ToString();
                //    shdsuperdeluxe= dtpickup.Rows[1]["Fare"].ToString();
                //    shdexecutive= dtpickup.Rows[2]["Fare"].ToString();
                //    shdroyal = dtpickup.Rows[3]["Fare"].ToString();
                //}



                //if ((sDeluxe > 0) && (ssuperdeluxe == 0)&&(sexecutive == 0)&&(sroyalsouthern == 0))
                //{
                //    TypeofRooms = sDeluxe + "Deluxe";
                //    rommrentperday = sDeluxe + "x" + shddeluxe;
                //}
                //else if ((ssuperdeluxe > 0) && (sDeluxe == 0) && (sexecutive == 0) && (sroyalsouthern == 0))
                //{
                //    TypeofRooms = ssuperdeluxe + "SuperDeluxe";
                //    rommrentperday = ssuperdeluxe + "x" + shdsuperdeluxe;
                //}


                //else if ((sexecutive > 0) && (sDeluxe == 0) && (ssuperdeluxe == 0) && (sroyalsouthern == 0))
                //{
                //    TypeofRooms = sexecutive + "Executive Suite";
                //    rommrentperday = sexecutive + "x" + shdexecutive;
                //}
                //else if ((sroyalsouthern > 0) && (sDeluxe == 0) && (sexecutive == 0) && (ssuperdeluxe == 0))
                //{
                //    TypeofRooms = sroyalsouthern + "Royal Southern";
                //    rommrentperday = sroyalsouthern + "x" + shdroyal;
                //}

                //else if ((sDeluxe > 0) && (ssuperdeluxe > 0) && (sexecutive == 0) && (sroyalsouthern == 0))
                //{
                //    TypeofRooms = sDeluxe + "Deluxe" + "+" + ssuperdeluxe+"Super Deluxe";
                //    rommrentperday = sDeluxe + "x" + shddeluxe + "+" + ssuperdeluxe + "x" + shdsuperdeluxe;
                //}
                //else if ((sDeluxe > 0) && (ssuperdeluxe > 0) && (sexecutive > 0) && (sroyalsouthern == 0))
                //{
                //    TypeofRooms = sDeluxe + "Deluxe" + "+" + ssuperdeluxe + "Super Deluxe"+"+"+sexecutive+"Executive Suite";
                //    rommrentperday = sDeluxe + "x" + shddeluxe + "+" + ssuperdeluxe + "x" + shdsuperdeluxe + "+" + sexecutive + "x" + shdexecutive;
                //}

                //else if ((sDeluxe > 0) && (ssuperdeluxe > 0) && (sexecutive > 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms = sDeluxe + "Deluxe" + "+" + ssuperdeluxe + "Super Deluxe" + "+" + sexecutive + "Executive Suite" + "+" + sroyalsouthern+"Royal Southern";
                //    rommrentperday = sDeluxe + "x" + shddeluxe + "+" + ssuperdeluxe + "x" + shdsuperdeluxe + "+" + sexecutive + "x" + shdexecutive + "+" + sroyalsouthern + "x" + shdroyal;
                //}

                //else if ((sDeluxe == 0) && (ssuperdeluxe > 0) && (sexecutive > 0) && (sroyalsouthern == 0))
                //{
                //    TypeofRooms =ssuperdeluxe + "Super Deluxe" + "+" + sexecutive + "Executive Suite";
                //    rommrentperday =  ssuperdeluxe + "x" + shdsuperdeluxe + "+" + sexecutive + "x" + shdexecutive;
                //}
                //else if ((sDeluxe == 0) && (ssuperdeluxe > 0) && (sexecutive > 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms =ssuperdeluxe + "Super Deluxe" + "+" + sexecutive + "Executive Suite" + "+" + sroyalsouthern + "Royal Southern";
                //    rommrentperday =ssuperdeluxe + "x" + shdsuperdeluxe + "+" + sexecutive + "x" + shdexecutive + "+" + sroyalsouthern + "x" + shdroyal;
                //}
                //else if ((sDeluxe == 0) && (ssuperdeluxe == 0) && (sexecutive > 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms =sexecutive + "Executive Suite" + "+" + sroyalsouthern + "Royal Southern";
                //    rommrentperday = sexecutive + "x" + shdexecutive + "+" + sroyalsouthern + "x" + shdroyal;
                //}
                //else if ((sDeluxe == 0) && (ssuperdeluxe == 0) && (sexecutive == 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms =sroyalsouthern + "Royal Southern";
                //    rommrentperday = sroyalsouthern + "x" + shdroyal;
                //}
                //else if ((sDeluxe > 0) && (ssuperdeluxe == 0) && (sexecutive == 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms = sDeluxe + "Deluxe" + "+" +  sroyalsouthern + "Royal Southern";
                //    rommrentperday = sDeluxe + "x" + shddeluxe + sroyalsouthern + "x" + shdroyal;
                //}
                //else if ((sDeluxe == 0) && (ssuperdeluxe > 0) && (sexecutive == 0) && (sroyalsouthern > 0))
                //{
                //    TypeofRooms = ssuperdeluxe + "Super Deluxe" + "+" + sroyalsouthern + "Royal Southern";
                //    rommrentperday =ssuperdeluxe + "x" + shdsuperdeluxe + sroyalsouthern + "x" + shdroyal;
                //}

                #endregion


                int i = 1;


                StringBuilder strTable = new StringBuilder();
                strTable.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() +
                    "\\HotelReciept.html")));
                strTable = strTable.Replace("#bookDate", Convert.ToString(Bookingdate.ToString("dd-MMM-yyyy hh:mmtt")));
                strTable = strTable.Replace("#pnr", orderid);
                strTable = strTable.Replace("#arrival", srrivaldate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#arrival", srrivaldate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#arrtime", srrivaldate.ToShortTimeString());
                strTable = strTable.Replace("#departure", sdeparturedate.ToString("dd-MMM-yyyy"));
                strTable = strTable.Replace("#departime", sdeparturedate.ToShortTimeString());


                strTable = strTable.Replace("#vocher", dtFare.Tables[0].Rows[0]["ticketno"].ToString());
                strTable = strTable.Replace("#name", dtFare.Tables[0].Rows[0]["FirstName"].ToString());

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


                string lAlterMBLNO = "";


                if (Convert.ToString(dtFare.Tables[0].Rows[0]["AlternativeNo"]) != "")
                {
                    lAlterMBLNO = "<br/><span class=cgi>Emergency Contact No : </span><span class=hlinks>" + Convert.ToString(dtFare.Tables[0].Rows[0]["AlternativeNo"]) + "</span>";
                    strTable = strTable.Replace("#mob", dtFare.Tables[0].Rows[0]["Mobile"].ToString() + lAlterMBLNO);
                }
                else
                {
                    strTable = strTable.Replace("#mob", dtFare.Tables[0].Rows[0]["Mobile"].ToString());
                }
                //strTable = strTable.Replace("#mob", dtFare.Tables[0].Rows[0]["Mobile"].ToString());
                strTable = strTable.Replace("#add", dtFare.Tables[0].Rows[0]["Addr1"].ToString());
                strTable = strTable.Replace("#cdate", sarrtime.ToString("dd-MMM-yyyy HH:mm tt"));
                strTable = strTable.Replace("#codate", sdepttime.ToString("dd-MMM-yyyy HH:mm tt"));
                // strTable = strTable.Replace("#City", "New Delhi");
                // strTable = strTable.Replace("#HotelAddress", "Hotel Southern 18/2,Arya Samaj Road,Karol Bagh,New Delhi-110005 ");
                strTable = strTable.Replace("#Typeofroom", dtFare.Tables[0].Rows[0]["BookingRoomTypes"].ToString());
                strTable = strTable.Replace("#noofper", snoofpersons.ToString());
                //strTable = strTable.Replace("#rooms", noofroom.ToString());//dtFare.Tables[0].Rows[0]["noofrooms"].ToString());
                // strTable = strTable.Replace("#rooms", noofroom.ToString() + " + " + ExtraBed.ToString() + "(Extra Bed) ");//dtFare.Tables[0].Rows[0]["noofrooms"].ToString());
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
                //strTable = strTable.Replace("#jdate", JourneyDate.ToString("dd-MMM-yyyy"));
                //strTable = strTable.Replace("#departTime", JourneyDate.ToShortTimeString());
                strTable = strTable.Replace("#stax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["STaxValue"].ToString()))));
                //strTable = strTable.Replace("#tourname", strans.ToString());
                strTable = strTable.Replace("#Totawithtax", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["AmtWithTax"].ToString()))));
                //strTable = strTable.Replace("#discount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Discount"].ToString()))));
                //string STAcc = Convert.ToString(pclsObj.fnGetServiceTaxValue("Acc"));
                //string STLT = Convert.ToString(pclsObj.fnGetServiceTaxValue("LT"));

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
                    STAcc = Convert.ToString(pclsObj.fnGetServiceTaxValue("Acc"));
                    STLT = Convert.ToString(pclsObj.fnGetServiceTaxValue("LT"));
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

                decimal advancePaid = 0;
                if ((Convert.ToInt32(dtFare.Tables[0].Rows[0]["Agentid"]) > 0))
                    advancePaid = stotalwithtax;
                else advancePaid = 0;
                strTable = strTable.Replace("#advance", Convert.ToString(Math.Round(advancePaid)));

                ViewState["branchcode"] = dtFare.Tables[0].Rows[0]["Branchcode"].ToString();
                ViewState["agentid"] = Convert.ToString(dtFare.Tables[0].Rows[0]["agentid"]);
                ViewState["username"] = dtFare.Tables[0].Rows[0]["Username"].ToString();
                strTable = strTable.Replace("#branchCode", dtFare.Tables[0].Rows[0]["Branchcode"].ToString());
                strTable = strTable.Replace("#branchUser", dtFare.Tables[0].Rows[0]["Username"].ToString());
                strTable = strTable.Replace("#BookingOffice", "BookingAgent");
                if (rmarks != "")
                {
                    strTable.Replace("#Remarks", rmarks);
                    strTable.Replace("#ShowRemarks", "block");

                }
                else
                {
                    strTable.Replace("#ShowRemarks", "none");
                }
                decimal dis = Convert.ToDecimal(dtFare.Tables[0].Rows[0]["Discount"]);

                if ((dis) > 0)
                {
                    strTable = strTable.Replace("#lblDiscount", " Special Discount: ");
                    strTable = strTable.Replace("#discount", Convert.ToString(decimal.Round(dis)));
                    //strTable = strTable.Replace("#amount", Convert.ToString(Math.Round(float.Parse(dtFare.Tables[0].Rows[0]["Totalamount"].ToString()))));
                }
                else
                {
                    strTable = strTable.Replace("#lblDiscount", "");
                    strTable = strTable.Replace("#discount", "");
                }
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

                if (decimal.Round((stotalwithtax - advancePaid)) > 0)
                {
                    strTable = strTable.Replace("#balance", Convert.ToString(decimal.Round(stotalwithtax - advancePaid)));
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

                dtterm = ldsRecSet.Tables[1];

                if (dtterm.Rows.Count > 0)
                {
                    //terms.InnerHtml = dtterm.Rows[0]["CarTermsCondition"].ToString();
                    // strTable.Append(dtterm.Rows[0]["HTLAccTermsCondition"].ToString());
                    //turms = dtterm.Rows[0]["HotelTermsCondition"].ToString();
                    strTable.Append(dtterm.Rows[0]["HTLAccTermsCondition"].ToString().Replace("#STAcc", STAcc.ToString()).Replace("#STLT", STLT.ToString()));
                }
                strTable.Replace("#GenerationTime", DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
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


                strTable = AppendGuestDetails(strTable);
                TicketStr.Append(strTable);
                lMailHtml.Append(strTable);
                /*string str = "SELECT HotelTermsCondition FROM CompanyDetailsForRpt";
                DataTable dtterm = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/

                TicketStr.Append("</DIV></TD></TR></Table>");
                //string htdup = Request.QueryString["duplicate"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
                // if (htdup.ToString() != "htdup")
                //{
                if (Session["sendmail"] == "yes")
                {

                    SendMail(dtFare.Tables[0].Rows[0]["email"].ToString(), ConfigurationSettings.AppSettings["eTicketEmail"].ToString(), "", "Accomodation Reservation  - Southern Travels", lMailHtml.ToString(), "", Convert.ToString(ldtRecSet.Rows[0]["CityName"]));
                }

                //}

                return TicketStr;
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
                if (dtterm != null)
                {
                    dtterm.Dispose();
                    dtterm = null;
                }
                if (dtHtNoroom != null)
                {
                    dtHtNoroom.Dispose();
                    dtHtNoroom = null;
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
            }

        }

        private void SendMail(string To, string From, string Bcc, string subject, string body, string cc, string lCityName)
        {
            string strRetMail = "";
            if (Convert.ToString(ViewState["agentid"]) != "")
            {
                #region Optimize Code
                /* string strLocalbranch = "select localBranch from agent_agentdetails(nolock) where agentid='" + Convert.ToString(ViewState["agentid"]) + "' ";
                string strLocalbranch1 = DataLib.GetStringData(DataLib.Connection.ConnectionString, strLocalbranch);
                string strMailId = "select Email from Branchmaster(nolock) where BranchCode='" + strLocalbranch1.Trim().ToString() + "' ";
                strRetMail = DataLib.GetStringData(DataLib.Connection.ConnectionString, strMailId);
                strLocalbranch = "select email from agent_agentdetails(nolock) where agentid='" + Convert.ToString(ViewState["agentid"]) + "' ";
                strRetMail = strRetMail + "," + DataLib.GetStringData(DataLib.Connection.ConnectionString, strLocalbranch);*/
                #endregion
                pclsObj = null;
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
                #region Optimize Code
                /*string strMailId = "select Email from security(nolock) where BranchCode='" + Convert.ToString(ViewState["branchcode"]) + "' and username='" + Convert.ToString(ViewState["username"]) + "' ";
                strRetMail = DataLib.GetStringData(DataLib.Connection.ConnectionString, strMailId);*/
                #endregion
                pclsObj = null;
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
                    if (lCityName.ToUpper().Trim() == "VIJAYAWADA")
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketMailBCC = ConfigurationSettings.AppSettings["iticketemailAccVijaywada"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["ticketemailAccVijaywada"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketMailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                    else
                    {
                        if ((Convert.ToString(ViewState["branchcode"]) == "EBK0001") && (Convert.ToInt32(Convert.ToString(ViewState["agentid"])) == 0))
                        {
                            ticketMailBCC = ConfigurationSettings.AppSettings["iticketemailAcc"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["ticketemailAcc"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketMailBCC = strRetMail + "," + ticketmail;
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
                            ticketMailBCC = ConfigurationSettings.AppSettings["iticketemailAccVijaywada"].ToString();
                        }
                        else
                        {
                            string ticketmail = ConfigurationSettings.AppSettings["ticketemailAccVijaywada"].ToString();
                            string ss = strRetMail + "," + ticketmail;
                            ticketMailBCC = ss.TrimStart(',').TrimEnd(',');
                        }
                    }
                    else
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
                }
                ClsCommon.sendmail(To, ticketMailBCC, "", ConfigurationSettings.AppSettings["eTicketEmail"].ToString(), subject, body, "");
            }
            catch (Exception)
            {

            }
            finally
            {

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

            string GetTickeDetailsForExtraServicetax = "GetGuestDetails_sp";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@I_OrderNo", orderid);
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
                return hactckstr.Replace("#TCS", "");
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