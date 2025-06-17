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
    public partial class AgentSpDuplicateTickets : System.Web.UI.Page
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
        ClsAdo pclsObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["AgentId"] == null))
            {

                Response.Redirect("AgentLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                }
                else
                {
                    sDelete = tmpDelete.Value.ToString();
                    if (sDelete == "1")
                    {
                        sendMail();
                        tmpDelete.Value = "0";

                    }
                }
            }

        }
        protected void BindData()
        {
            #region Optimize Code
            //string str;

            //if (Session["AgentId"] != null)
            //{
            /* str = @"select a.id as rowid,a.ticketno as ticketcode,a.journeydate as doj,a.dob as dob,a.firstname as
                      groupleader,isnull(a.agentid,0) as agentid,a.totalpax as noofseats from spl_tourenquiry a where a.ticketno is not null and 
                        a.journeydate>=convert(varchar(10),getdate(),101) and (a.iscancel<>'Y' or a.iscancel<>'y') and a.agentid=" + Session["AgentId"] + "";*/
            //        }
            //        else
            //        {
            //            str = @"select a.id as rowid,a.ticketno as ticketcode,a.journeydate as doj,a.dob as dob,a.firstname as
            //                 groupleader,isnull(a.agentid,0)  as agentid,a.totalpax as noofseats from spl_tourenquiry a where a.ticketno is not null and 
            //                  a.journeydate>=convert(varchar(10),getdate(),101) and (a.iscancel<>'Y' or a.iscancel<>'y') and a.status='S'";
            //        }
            //DataTable dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
            #endregion
            pclsObj = new ClsAdo();
            DataTable dt = pclsObj.fnGetAgentSPLTourDupInfo(Convert.ToInt32(Session["AgentId"]));
            try
            {
                dgspDuplicateTickets.DataSource = dt;
                Globals.CheckData(ref dgspDuplicateTickets, dt, ref lblMsg);
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

        protected void dgspDuplicateTickets_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgspDuplicateTickets.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
        public void func()
        {
            #region Optimize Code
            string strSQL, strWhere;
            //if (Session["AgentId"] != null)
            //{
            /*strSQL = @"select a.id as rowid,a.ticketno as ticketcode,a.journeydate as doj,a.dob as dob,a.firstname as
                            groupleader,a.agentid as agentid,a.totalpax as noofseats from spl_tourenquiry a where a.ticketno is not null and 
                               a.journeydate>=convert(varchar(10),getdate(),101)and (a.iscancel<>'Y' or a.iscancel<>'y') and a.agentid=" + Session["AgentId"] + "";*/
            //        }
            //        else
            //        {
            //            strSQL = @"select a.id as rowid,a.ticketno as ticketcode,a.journeydate as doj,a.dob as dob,a.firstname as
            //                       groupleader,a.agentid as agentid,a.totalpax as noofseats from spl_tourenquiry a where a.ticketno is not null and 
            //                        a.journeydate>=convert(varchar(10),getdate(),101) and (a.iscancel<>'Y' or a.iscancel<>'y') and a.status='S'";
            //        }
            strWhere = "";


            /* if (journeydate != "")
             {
                 string[] dd = journeydate.Split('/');
                 journeydate = dd[1] + "/" + dd[0] + "/" + dd[2];
             }

             if (ticket != "")
             {
                 strWhere = strWhere + " and a.ticketno='" + ticket + "'";
             }

             if (contactno != "")
             {
                 strWhere = strWhere + " and (a.Mobile='" + contactno + "' or a.phone='" + contactno + "')";
             }
             if (emailid != "")
             {
                 strWhere = strWhere + " and a.Email='" + emailid + "'";
             }
             if (journeydate != "")
             {
                 strWhere = strWhere + " and a.journeydate='" + journeydate + "'";
             }
             strSQL = strSQL + strWhere;
             DataTable dtnew = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strSQL);*/
            #endregion
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
            DataSet ldsRecSet = pclsObj.fnGetAgentSPLTourDupTKT(Convert.ToString(Session["AgentId"]), ticket, contactno, emailid, lFromdate);
            DataTable dtnew = new DataTable();
            if (ldsRecSet != null)
                dtnew = ldsRecSet.Tables[0];
            try
            {
                dgspDuplicateTickets.DataSource = dtnew;
                Globals.CheckData(ref dgspDuplicateTickets, dtnew, ref lblMsg);
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
        protected void dgspDuplicateTickets_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView rv = (DataRowView)e.Item.DataItem;
                string k = Convert.ToString(rv.Row["ticketcode"]);
                if (k.Substring(0, 3) == "SPL")
                {
                    e.Item.Cells[5].Text = "Special Tour Booking";
                }
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

        protected void dgspDuplicateTickets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string TourName, TicketNo, mail, name, address, totalpax, depttime, costinclude, costexclude;
        string[] vehicle;
        string agentid, category, pickveh, pickvehno, pickuptime, dropveh, dropvehno, droptime, PickupFrom, DropAt;
        string strBranchCode = "", strPkStation = "", strDPStation = "";
        decimal advancePaid = 0.0m, strCCharges = 0.0m, discount = 0.0m;
        string TransNumber;
        decimal Amount, STaxValue;
        int singlesharing, tourid;
        DateTime JourneyDate, dob;

        void SendDuplicateTicket(string Id)
        {
            StringBuilder TicketStr = new StringBuilder();
            StringBuilder MailSubject = new StringBuilder();
            string id1, strStation = "";
            DataSet dsSplTicket = null;
            DataTable dtTicketDetails = null;
            StringBuilder lMailHtml = new StringBuilder();
            lMailHtml.Append("<TABLE id=\"Table3\" cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            lMailHtml.Append("<TR><TD align=\"center\"><b>");
            lMailHtml.Append("PLEASE DO NOT REPLY TO THIS MAIL AS THIS IS AN AUTO GENERATED MAIL AND REPLIES / QUERIES TO THIS EMAIL ID ARE NOT ATTENDED. FOR ANY ASSISTANCE / QUERIES PLEASE MAIL US AT <a href=\"mailto:support@southerntravels.in\" title='' >support@southerntravels.in</a>");
            lMailHtml.Append("</b></TD></TR></TABLE>");
            try
            {
                pclsObj = new ClsAdo();
                if (Id != null)
                {
                    StringBuilder s = new StringBuilder();
                    s.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\Splticket_templete.html")));

                    // id1 = DataLib.funClear(Convert.ToString(Request.QueryString["Id"]));
                    /* string sql = "select a.id,a.Email,a.firstname,a.lastname,a.address,a.city,a.state,a.tourname,a.journeydate,a.totalpax,a.AdvancePaid,a.branchcode,a.TourId,a.PickUpVeh,a.station,a.PickVehNo,a.PickTime,a.DropVeh,a.DropVehNo,a.DropTime,a.AgentId,b.vehiclename,c.categoryname,a.fare,a.cccharges,a.ticketno,a.Servicetax,a.SingleSharing,a.PkStation,a.dob,a.station, a.mobile,isnull(a.Discount,0) as Discount from spl_tourenquiry a, spl_vehiclemaster b,Spl_Category c where id=" + Id + " and a.farecategorytype=c.categoryid and charindex( ',' +ltrim(rtrim( cast( b.vehicleid  as varchar) ))+ ',', ',' +  ltrim(rtrim( a.cartype )) + ',' )<>0";
                     string mobile = "";
                     DataTable dtTicketDetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sql);*/
                    string mobile = "";
                    dsSplTicket = pclsObj.fnSpecial_Ticket_Print(Convert.ToInt32(Id));
                    dtTicketDetails = new DataTable();
                    if (dsSplTicket != null && dsSplTicket.Tables.Count > 0)
                    {
                        dtTicketDetails = dsSplTicket.Tables[0].Copy();
                    }

                    if (dtTicketDetails.Rows.Count > 0)
                    {
                        mobile = Convert.ToString(dtTicketDetails.Rows[0]["mobile"]);
                        mail = Convert.ToString(dtTicketDetails.Rows[0]["Email"]);
                        TransNumber = Convert.ToString(dtTicketDetails.Rows[0]["id"]);
                        TicketNo = Convert.ToString(dtTicketDetails.Rows[0]["ticketno"]);
                        TourName = dtTicketDetails.Rows[0]["tourname"].ToString();
                        name = Convert.ToString(Convert.ToString(dtTicketDetails.Rows[0]["firstname"]));
                        address = Convert.ToString(dtTicketDetails.Rows[0]["address"]);
                        JourneyDate = Convert.ToDateTime(dtTicketDetails.Rows[0]["journeydate"].ToString());
                        JourneyDate = Convert.ToDateTime((JourneyDate).ToShortDateString().ToString());
                        totalpax = Convert.ToString(dtTicketDetails.Rows[0]["totalpax"]);
                        agentid = Convert.ToString(dtTicketDetails.Rows[0]["AgentId"]);
                        tourid = Convert.ToInt32(dtTicketDetails.Rows[0]["TourId"]);
                        pickveh = Convert.ToString(dtTicketDetails.Rows[0]["PickUpVeh"]);
                        pickvehno = Convert.ToString(dtTicketDetails.Rows[0]["PickVehNo"]);
                        pickuptime = Convert.ToString(dtTicketDetails.Rows[0]["PickTime"]);
                        dropveh = Convert.ToString(dtTicketDetails.Rows[0]["DropVeh"]);
                        dropvehno = Convert.ToString(dtTicketDetails.Rows[0]["DropVehNo"]);
                        droptime = Convert.ToString(dtTicketDetails.Rows[0]["DropTime"]);
                        strBranchCode = Convert.ToString(dtTicketDetails.Rows[0]["BranchCode"]);
                        strPkStation = Convert.ToString(dtTicketDetails.Rows[0]["pkstation"]);
                        strDPStation = Convert.ToString(dtTicketDetails.Rows[0]["station"]);
                        dob = Convert.ToDateTime(dtTicketDetails.Rows[0]["dob"]);
                        discount = Convert.ToDecimal(Convert.ToString(dtTicketDetails.Rows[0]["Discount"]));
                        // TransNumber = TicketNo;

                        if ((Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != null) && (Convert.ToString(dtTicketDetails.Rows[0]["AdvancePaid"]) != ""))
                        {
                            advancePaid = Convert.ToDecimal(dtTicketDetails.Rows[0]["AdvancePaid"].ToString());
                        }
                        else
                        {
                            advancePaid = 0;
                        }

                        if ((Convert.ToString(dtTicketDetails.Rows[0]["Discount"]) != null) && (Convert.ToString(dtTicketDetails.Rows[0]["Discount"]) != ""))
                        {
                            discount = Convert.ToDecimal(dtTicketDetails.Rows[0]["Discount"].ToString());
                        }
                        else
                        {
                            discount = 0;
                        }
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
                        string veh = "";
                        for (int k = 0; k <= dtTicketDetails.Rows.Count - 1; k++)
                        {
                            if (k == 0)
                            {
                                veh = Convert.ToString(dtTicketDetails.Rows[k]["vehiclename"]);
                            }
                            else if (k == (dtTicketDetails.Rows.Count - 1))
                            {
                                veh = veh + "," + Convert.ToString(dtTicketDetails.Rows[k]["vehiclename"]);
                            }
                            else
                            {
                                veh = veh + "," + Convert.ToString(dtTicketDetails.Rows[k]["vehiclename"]);
                            }
                        }
                        string single = Convert.ToString(dtTicketDetails.Rows[0]["SingleSharing"]);
                        if ((single != null) && (single != ""))
                        {
                            singlesharing = Convert.ToInt32(single);
                        }
                        else
                        {
                            singlesharing = 0;
                        }
                        vehicle = veh.Split(',');
                        category = Convert.ToString(dtTicketDetails.Rows[0]["categoryname"]);
                        Amount = Convert.ToDecimal(dtTicketDetails.Rows[0]["fare"].ToString());
                        STaxValue = Convert.ToDecimal(dtTicketDetails.Rows[0]["Servicetax"].ToString());
                        strStation = (dtTicketDetails.Rows[0]["Station"].ToString());
                        //if (Session["BranchId"] == null && Session["AgentId"] == null)
                        //{
                        //    strCCharges = Convert.ToDecimal(dtTicketDetails.Rows[0]["cccharges"].ToString());
                        //}
                        string strCC = Convert.ToString(dtTicketDetails.Rows[0]["cccharges"]);
                        if (strCC == null || strCC == "")
                        {
                            strCCharges = 0;
                        }
                        else
                        {
                            strCCharges = Convert.ToDecimal(strCC);
                        }

                        string PaymentMode = "Branch Cash";
                        /*string cost = "select DeptTime,CostIncludes,CostExcludes from spl_tourmaster where tourid=" + tourid + "";
                        DataTable dtcost = DataLib.GetDataTable(DataLib.Connection.ConnectionString, cost);
                        if (dtcost.Rows.Count > 0)
                        {
                            depttime = Convert.ToString(dtcost.Rows[0]["DeptTime"]);
                            costinclude = Convert.ToString(dtcost.Rows[0]["CostIncludes"]);
                            costexclude = Convert.ToString(dtcost.Rows[0]["CostExcludes"]);
                        }*/
                        if (dsSplTicket != null && dsSplTicket.Tables.Count > 0 && dsSplTicket.Tables[2].Rows.Count > 0)
                        {
                            depttime = Convert.ToString(dsSplTicket.Tables[2].Rows[0]["DeptTime"]);
                            costinclude = Convert.ToString(dsSplTicket.Tables[2].Rows[0]["CostIncludes"]);
                            costexclude = Convert.ToString(dsSplTicket.Tables[2].Rows[0]["CostExcludes"]);
                        }
                    }
                    int i = 1;

                    //======================================//
                    MailSubject.Append("<DIV id='print_area" + i + "'>");
                    MailSubject.Append("<TABLE id=Table2 cellSpacing=0 cellPadding=6 width=100% border=0>");
                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD class=style5 style=HEIGHT: 15px align=center></TD>");
                    MailSubject.Append("<TD class=style5 style=HEIGHT: 15px align=center></TD>");
                    MailSubject.Append("</TR>");
                    MailSubject.Append("<TR><td colspan=2><table width=100% border=1 cellpadding=0 cellspacing=0 bgcolor=#CCCCCC> <tr> <td><table width=100% border=0 cellpadding=10 cellspacing=0 bgcolor=#FFFFFF><tr>");

                    string saveName = "Logo\\" + Convert.ToString(Session["UserId"]);
                    string exist = "N";
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".jpg"))) { saveName = saveName + ".jpg"; exist = "Y"; }
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".gif"))) { saveName = saveName + ".gif"; exist = "Y"; }
                    if (System.IO.File.Exists(Server.MapPath(saveName + ".png"))) { saveName = saveName + ".png"; exist = "Y"; }
                    string address1 = Globals.AgentAddress;
                    string ph = Globals.AgentPhone;
                    if (exist == "Y")
                    {
                        MailSubject.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><img src='" + saveName + "' width=\"157\" height=\"69\" /><br />" + address1 + "," + ph + "</font></td>");
                    }
                    else
                    {
                        string AgentName = Convert.ToString(Session["AgentFname"]).Trim() + " " + Convert.ToString(Session["AgentLname"]).Trim();
                        MailSubject.Append("<TD width=\"69%\"><font color=\"#666666\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><string>'" + AgentName + "'</string><br/>" + address1 + "," + ph + "</font></td>");
                    }
                    MailSubject.Append("<td width=\"31%\" valign=\"top\"><img src=\"../Assets/images/logo.webp\" width=\"231\" height=\"64\" /></td>");
                    MailSubject.Append("</TR></table></td></tr></table></td></tr>");

                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD class=style5 align=center height=7></TD>");
                    MailSubject.Append("<TD class=style5 align=center height=7></TD>");
                    MailSubject.Append("</TR>");
                    MailSubject.Append("<tr>");
                    MailSubject.Append("<td class=cgi style=HEIGHT: 15px align=center>");
                    MailSubject.Append("<table width=100%>");
                    MailSubject.Append("<tr>");
                    MailSubject.Append("<td class=cgi width=60% align=center>");
                    MailSubject.Append("SPECIAL TOUR TICKET");
                    //MailSubject.Append("DEMO TICKET");
                    MailSubject.Append("</td>");
                    MailSubject.Append("</TR>");
                    MailSubject.Append("</Table>");
                    MailSubject.Append("</TD>");
                    MailSubject.Append("<td class=cgi style=HEIGHT: 15px align=center>");
                    MailSubject.Append("</TD>");
                    MailSubject.Append("</TR>");



                    MailSubject.Append("<TR>");
                    MailSubject.Append("<TD>");

                    StringBuilder MailSubject1 = new StringBuilder();
                    if (pickveh == "Flight")
                    {

                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Pick Up From: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(PickupFrom);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD>");
                        MailSubject1.Append("<SPAN class=cgi>Flight No: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(pickvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Pickup Time: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(pickuptime + " " + "Hrs");
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("</TR>");
                    }
                    else if (pickveh == "Train")
                    {

                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Railway Station Name: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(PickupFrom);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD>");
                        MailSubject1.Append("<SPAN class=cgi>Train No: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(pickvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Pickup Time: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(pickuptime + " " + "Hrs");
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("</TR>");
                    }
                    else if (pickveh == "Bus")
                    {

                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD ><SPAN class=cgi>Pickup Address: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(PickupFrom);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD colspan=2>");
                        MailSubject1.Append("<SPAN class=cgi>Street: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(pickvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("</TR>");
                    }

                    if (dropveh == "Flight")
                    {
                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Drop Address: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(DropAt);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD>");
                        MailSubject1.Append("<SPAN class=cgi> Flight No: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(dropvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Drop Time:</SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(droptime + " " + "Hrs");
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("</TR>");
                    }

                    else if (dropveh == "Train")
                    {
                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Railway Station Name: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(DropAt);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD>");
                        MailSubject1.Append("<SPAN class=cgi> Train No: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(dropvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Drop Time:</SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(droptime + " " + "Hrs");
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");
                        MailSubject1.Append("</TR>");
                    }
                    else if (dropveh == "Bus")
                    {
                        MailSubject1.Append("<TR>");
                        MailSubject1.Append("<TD><SPAN class=cgi>Drop Address: </SPAN><SPAN class=hlinks>");
                        MailSubject1.Append(DropAt);
                        MailSubject1.Append("</SPAN></TD>");
                        MailSubject1.Append("<TD colspan=2>");
                        MailSubject1.Append("<SPAN class=cgi> Street: </SPAN> <SPAN class=hlinks>");
                        MailSubject1.Append(dropvehno);
                        MailSubject1.Append("</SPAN>");
                        MailSubject1.Append("</TD>");

                        MailSubject1.Append("</TR>");
                    }

                    //======================================================//
                    s = s.Replace("#dob", dob.ToString("dd/MM/yyyy"));
                    s = s.Replace("#splno", TransNumber);
                    s = s.Replace("#ticketno", TicketNo);
                    s = s.Replace("#journeydate", JourneyDate.ToString("dd/MM/yyyy") + " " + depttime + " " + "Hrs");
                    s = s.Replace("#tournmae", TourName);
                    string veh1 = "";
                    for (int j = 0; j <= vehicle.Length - 1; j++)
                    {
                        veh1 = veh1 + (vehicle[j] + ",<br/>");
                    }
                    veh1 = veh1.TrimEnd(',');
                    s = s.Replace("#vehicle", veh1);
                    s = s.Replace("#category", category);
                    s = s.Replace("#Pickup", Convert.ToString(MailSubject1));
                    if (singlesharing == 0)
                    {
                        s = s.Replace("#noofpax", "No of Pax");
                        s = s.Replace("#pax", totalpax);
                    }
                    else
                    {
                        s = s.Replace("#noofpax", "No of Pax/Single Sharing");
                        s = s.Replace("#pax", totalpax + " / " + singlesharing);
                    }
                    s = s.Replace("#name", name);
                    s = s.Replace("#address", address);

                    s = s.Replace("#amount", Convert.ToString(decimal.Round((Amount + discount), 2)).ToString());
                    s = s.Replace("#tax", Convert.ToString(decimal.Round((STaxValue + strCCharges), 2).ToString("0.00")));
                    s = s.Replace("#totalamount", Convert.ToString(decimal.Round((Amount + STaxValue + strCCharges), 2)));

                    if (advancePaid > 0)
                    {
                        s = s.Replace("#advance", Convert.ToString(advancePaid));
                        s = s.Replace("#balance", Convert.ToString(decimal.Round((Amount + STaxValue - advancePaid), 2)));
                    }
                    else
                    {
                        s = s.Replace("#advance", Convert.ToString(decimal.Round((Amount + STaxValue + strCCharges), 2)));
                        s = s.Replace("#balance", "Nil");
                    }
                    if (agentid == "")
                        agentid = "0";
                    if ((strBranchCode != "EBK0001") && (Convert.ToInt32(agentid) > 0))
                    {
                        s = s.Replace("#bookingoffice", "Agent :" + strBranchCode);
                    }
                    else if ((strBranchCode != "EBK0001") && (agentid == "0"))
                    {
                        s = s.Replace("#bookingoffice", "Branch :" + strBranchCode);
                    }
                    else
                    {
                        s = s.Replace("#bookingoffice", "Online Booking");
                    }
                    //if (strBranchCode != "EBK0001" && Session["UserId"] == null)
                    //{
                    //    s = s.Replace("#bookingoffice", strBranchCode);
                    //}
                    //else if((Convert.ToInt32(agentid) > 0))
                    //{
                    //    s = s.Replace("#bookingoffice", "Agent :" + strBranchCode );
                    //}
                    //else
                    //{
                    //    s = s.Replace("#bookingoffice", "OnLineBooking");
                    //}
                    if ((Convert.ToInt32(agentid) > 0))
                    {
                        s = s.Replace("#lbldiscount", "Discount :");
                        s = s.Replace("#discount", "Nil");
                    }

                    else if ((strBranchCode == "EBK0001") && (agentid == "0"))
                    {
                        s = s.Replace("#lbldiscount", "");
                        s = s.Replace("#discount", "");
                    }
                    else
                    {
                        s = s.Replace("#lbldiscount", "Discount :");
                        s = s.Replace("#discount", Convert.ToString(discount));
                    }
                    s = s.Replace("#CancelCharg", "");
                    s = s.Replace("#ShowRemarksText", "");
                    StringBuilder BannerText = new StringBuilder();
                    BannerText.Append(ClsCommon.GetTourTicketBanner(tourid, "ST", "Agent"));
                    s = s.Replace("#ShowBanner", BannerText.ToString());
                    MailSubject.Append(s);
                    MailSubject.Append("</div>");
                    TicketStr.Append(MailSubject);
                    TicketStr.Append("</TD>");
                    TicketStr.Append("</TR>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td height=5 colspan=3>");
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");
                    //TicketStr.Append("<tr>");
                    //TicketStr.Append("<td colspan=3 class=hlinks>");
                    //TicketStr.Append("<br/>View our Terms and conditions");
                    //TicketStr.Append("</td>");
                    //TicketStr.Append("</tr>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td colspan=3 class=hlinks>");
                    TicketStr.Append("<b>Cost Includes: </b>" + costinclude);
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td colspan=3 class=hlinks>");
                    TicketStr.Append("<b>Cost Excludes: </b>" + costexclude);
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");
                    TicketStr.Append("<tr>");
                    TicketStr.Append("<td colspan=3 class=hlinks>");
                    TicketStr.Append("Note : Please note that the cancellation charges are applicable on the published fare. ");
                    TicketStr.Append("</td>");
                    TicketStr.Append("</tr>");


                    /*string str = "SELECT SpecialTermsCondition FROM CompanyDetailsForRpt";
                    DataTable dtterm = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
                    if (dtterm.Rows.Count > 0)
                    {
                        TicketStr.Append("<tr>");
                        TicketStr.Append("<td colspan=3 class=hlinks>");
                        TicketStr.Append(dtterm.Rows[0]["SpecialTermsCondition"].ToString());
                        TicketStr.Append("</td>");
                        TicketStr.Append("</tr>");
                    }*/
                    if (dsSplTicket != null && dsSplTicket.Tables.Count > 0 && dsSplTicket.Tables[4].Rows.Count > 0)
                    {
                        TicketStr.Append("<tr>");
                        TicketStr.Append("<td colspan=3 class=hlinks>");
                        TicketStr.Append(dsSplTicket.Tables[4].Rows[0]["SpecialTermsCondition"].ToString());
                        TicketStr.Append("</td>");
                        TicketStr.Append("</tr>");
                    }

                    TicketStr.Append("</Table>");
                    lMailHtml.Append(TicketStr);
                    try
                    {
                        //MailMessage mObj = new MailMessage();
                        //mObj.To = mail;                    
                        //mObj.Bcc = "etickets@southerntravels.in";
                        //mObj.From = ConfigurationSettings.AppSettings["splTourEnquiryMailId"].ToString();
                        //mObj.Subject = "Special Tour Ticket ";
                        //mObj.BodyFormat = MailFormat.Html;
                        //mObj.Body = MailSubject.ToString();
                        //SmtpMail.Send(mObj);


                        ClsCommon.sendmail(mail, "etickets@southerntravels.in", "", ConfigurationSettings.AppSettings["splTourEnquiryMailId"].ToString(), "Special Tour Ticket ", lMailHtml.ToString(), "");



                    }

                    catch (Exception ex)
                    {
                        Response.Write("!--" + ex.Message.ToString() + "-->");
                    }

                    //Response.Write(MailSubject.ToString());
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "invalidid", "<script>alert('Ticket is Not generated');</script>");
                }
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (dsSplTicket != null)
                {
                    dsSplTicket.Dispose();
                    dsSplTicket = null;
                }
                if (dtTicketDetails != null)
                {
                    dtTicketDetails.Dispose();
                    dtTicketDetails = null;
                }
            }
        }

        void sendMail()
        {
            foreach (DataGridItem di in dgspDuplicateTickets.Items)
            {
                bool CheckSelected;
                if (di.ItemType != ListItemType.Item || di.ItemType != ListItemType.AlternatingItem)
                {
                    CheckSelected = ((CheckBox)(di.Cells[0].FindControl("chkAccept"))).Checked;
                    if (CheckSelected == true)
                    {
                        string tkCode = Convert.ToString(dgspDuplicateTickets.DataKeys[di.ItemIndex].ToString());
                        SendDuplicateTicket(tkCode);
                    }
                }
            }
            this.RegisterStartupScript("sdad", "<script>alert('Mail(s) sent successfully');</script>");
        }
    }
}