using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Caching;
using AjaxControlToolkit;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentHotelSearch : System.Web.UI.Page
    {
        protected DateTime startDate, endDate;
        protected short dayDiff;
        protected short[,] paxDetailsArr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }        
            if (IsPostBack)
            {


                if (bookHit.Value == "true")
                {
                    Session["hotelId"] = strHotelId.Value;
                    Session["ratePlanId"] = strRateplanId.Value;
                    Session["roomId"] = strRoomId.Value;
                    Session["strHotelTax"] = strTax.Value;
                    Session["strHotelTotal"] = strTotal.Value;
                    Session["strExtraGuestCharge"] = strExtraGuestCharge.Value;
                    Session["strHotelName"] = strHotelName.Value;
                    Session["strHotelRating"] = strHotelRating.Value;
                    Session["strHotelCityCode"] = strHotelCityCode.Value;
                    Session["strHotelAddress"] = strHotelAddress.Value + ", " + strHotelCityCode.Value;
                    Session["strRoomDetails"] = strRoomDetails.Value;

                    if (hotelMoreInfo.Value == "true")
                    {
                        Response.Redirect("AgenthotelMoreInfo.aspx", true);
                    }
                    Response.Redirect("AgenthotelBook.aspx", true);
                }
            }
            else
            {
                string url = "";
                //if(this.Request.UrlReferrer.ToString();
                try
                {
                    url = this.Request.UrlReferrer.ToString();
                }
                catch (Exception ex) { }

                if (url.IndexOf("index") != -1)
                {

                    //Clear all previous cache
                    if (Cache["dtHotelResults" + Session.SessionID] != null)
                        Cache.Remove("dtHotelResults" + Session.SessionID);

                    if (Cache["dtRates" + Session.SessionID] != null)
                        Cache.Remove("dtRates" + Session.SessionID);

                    if (Cache["dtRateDays" + Session.SessionID] != null)
                        Cache.Remove("dtRateDays" + Session.SessionID);

                    if (Cache["xmlStr" + Session.SessionID] != null)
                        Cache.Remove("xmlStr" + Session.SessionID);

                    //End Clearing

                    setUpData();
                }

                bindHotels();
            }
            //}
        }
        private void bindHotels()
        {
            paxDetailsArr = (short[,])(Session["PaxArr"]);
            string xmlStr = "";
            //form1.Attributes.Add("action","hotelBook.aspx");
            //action="hotelBook.aspx"
            lblLocation.Text = Convert.ToString(Session["strCity"]);
            DateTime dt1 = Convert.ToDateTime(Session["CheckOut"]);
            lblChkOut.Text = dt1.ToString("ddd, dd MMM, yyyy"); ;

            DateTime dt2 = Convert.ToDateTime(Session["CheckIn"]);
            lblChkIn.Text = dt2.ToString("ddd, dd MMM, yyyy");
            lblAdult.Text = Convert.ToString(Session["TotalAdult"]);
            lblChild.Text = Convert.ToString(Session["TotalChild"]);
            if (Cache["xmlStr" + Session.SessionID] != null)
            {

                xmlStr = Convert.ToString(Cache["xmlStr" + Session.SessionID]);
                if (xmlStr.IndexOf("<StayDateRange Start=") != -1)
                {
                    string datePart = xmlStr.Substring(xmlStr.IndexOf("<StayDateRange Start="), xmlStr.IndexOf("/>", xmlStr.IndexOf("<StayDateRange Start=")) - xmlStr.IndexOf("<StayDateRange Start="));
                    string[] tempArr = datePart.Split('\'');
                    string[] tempArrDt = tempArr[1].Split('T');
                    startDate = DateTime.ParseExact(tempArr[1], "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture);  //tempArrDt[0];              
                    endDate = DateTime.ParseExact(tempArr[3], "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture);  //tempArrDt[0];
                    dayDiff = short.Parse(((TimeSpan)(endDate - startDate)).Days.ToString());
                    Session["checkInDate"] = startDate;
                    Session["checkOutDate"] = endDate;
                }
            }
            DataTable dt = new DataTable();

            if (Cache["dtHotelResults" + Session.SessionID] == null)
            {
                dt = getHotelResults(xmlStr);
                Cache.Insert("dtHotelResults" + Session.SessionID, dt, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

                if (Cache["dtRates" + Session.SessionID] != null)
                    Cache.Remove("dtRates" + Session.SessionID);

                if (Cache["dtRateDays" + Session.SessionID] != null)
                    Cache.Remove("dtRateDays" + Session.SessionID);

                if (dtRates != null)
                    Cache.Insert("dtRates" + Session.SessionID, dtRates, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                if (dtRateDays != null)
                    Cache.Insert("dtRateDays" + Session.SessionID, dtRateDays, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);


            }
            else
            {

                dt = (DataTable)Cache["dtHotelResults" + Session.SessionID];
                dtRates = (DataTable)Cache["dtRates" + Session.SessionID];
                dtRateDays = (DataTable)Cache["dtRateDays" + Session.SessionID];

            }

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = dt.DefaultView;

            if (dt.Rows.Count == 0)
            {
                rptResults.Visible = false;
                lblErrMsg.Text = "No Hotel Found For The Given Search Parameters";
                tblPaging.Visible = false;
            }

            // Indicate that the data should be paged
            objPds.AllowPaging = true;

            // Set the number of items you wish to display per page
            objPds.PageSize = 10;
            objPds.CurrentPageIndex = CurrentPage;
            lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + objPds.PageCount.ToString();
            lblCPage1.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + objPds.PageCount.ToString();
            //Disable Prev or Next buttons if necessary
            cmdPrev.Enabled = !objPds.IsFirstPage;
            cmdNext.Enabled = !objPds.IsLastPage;
            cmdPrev1.Enabled = !objPds.IsFirstPage;
            cmdNext1.Enabled = !objPds.IsLastPage;
            rptResults.DataSource = objPds;
            rptResults.DataBind();
            dt = null;
            objPds = null;
            dtRates = null;
            dtRateDays = null;

        }
        string xmlResponse = "";
        DataTable dtRates, dtRateDays;
        private DataTable getHotelResults(string xmlRequestStr)
        {
            //Response.Write("<br>Sending Request<br><br><font size=\"3\" face=\"arial\"  color=\"red\"><b>" + Server.HtmlEncode(xmlStr) + "</b></font><br>");
            XmlDocument doc = new XmlDocument();
            string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            xmlResponse = DataLib.HttpSOAPRequest(xmlRequestStr, uri, null);

            //if(xmlResponse.IndexOf("")
            //   lblErrMsg
            //Response.Write("<br>Response from Desiya <br><br><font size=\"3\" face=\"arial\" color=\"green\" ><b>" + Server.HtmlEncode(s) + "</b></font><br>");

            //doc.LoadXml(System.IO.File.ReadAllText(@"c:\resp.xml"));
            try
            {
                doc.LoadXml(xmlResponse);
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = xmlResponse;
                tblPaging.Visible = false;
                rptResults.Visible = false;
                return new DataTable();
            }
            //try
            //{
            //    //if(File.Exists("res.xml"))  File.Delete("res.xml");
            //    FileStream fs = new FileStream(Server.MapPath("res.xml"),FileMode.Create,FileAccess.Write,FileShare.Write);
            //    byte[] b = System.Text.Encoding.UTF8.GetBytes(xmlResponse);
            //    fs.Write(b,0,b.Length);
            //    fs.Close();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<!--"+ ex.ToString() +"-->");     
            //}

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("trn", "urn:Hotel_Search");
            XmlNode nItem = doc.SelectSingleNode("Success", nsmgr);

            //doc.SelectNodes("/trn:DSHotelAvailRSPL", nsmgr);
            DataSet ds = new DataSet();

            DataTable dtHotel = new DataTable();
            dtRates = new DataTable();


            dtHotel.Columns.Add("hotelCode", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("hotelName", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("hotelRating", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("hotelCityCode", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("RPH", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("CurrencyCode", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("MinRate", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("MaxRate", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("DirectConnect", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("Description", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("Image", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("Position", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("Address", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("ContactNumbers", System.Type.GetType("System.String"));
            dtHotel.Columns.Add("FaxNumber", System.Type.GetType("System.String"));

            dtRates.Columns.Add("RPH", System.Type.GetType("System.String"));
            dtRates.Columns.Add("RoomTypeCode", System.Type.GetType("System.String"));
            dtRates.Columns.Add("RoomTypeName", System.Type.GetType("System.String"));
            dtRates.Columns.Add("RatePlanCode", System.Type.GetType("System.String"));
            dtRates.Columns.Add("INR", System.Type.GetType("System.String"));

            dtRates.Columns.Add("ExtraGuestCharge", System.Type.GetType("System.String"));
            dtRates.Columns.Add("TotalTaxCharges", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Amenities", System.Type.GetType("System.String"));

            dtRates.Columns.Add("Max_Adult_Occupancy", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Max_Child_Occupancy", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Max_Infant_Occupancy", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Max_Guest_Occupancy", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Base_Adult_Occupancy", System.Type.GetType("System.String"));
            dtRates.Columns.Add("Base_Child_Occupancy", System.Type.GetType("System.String"));

            dtRateDays = new DataTable();

            dtRateDays.Columns.Add("Day", System.Type.GetType("System.String"));
            dtRateDays.Columns.Add("Amount", System.Type.GetType("System.String"));
            dtRateDays.Columns.Add("RateType", System.Type.GetType("System.String"));
            dtRateDays.Columns.Add("RoomTypeCode", System.Type.GetType("System.String"));
            dtRateDays.Columns.Add("RatePlanCode", System.Type.GetType("System.String"));

            if (xmlResponse.IndexOf("No Hotel Found For The Given Search Parameters") != -1)
            {
                rptResults.Visible = false;
                lblErrMsg.Text = "No Hotel Found For The Given Search Parameters";
                tblPaging.Visible = false;
                return dtHotel;
            }
            if (xmlResponse.IndexOf("Authentication Failed") != -1)
            {
                rptResults.Visible = false;
                lblErrMsg.Text = "No Hotel Found For The Given Search Parameters";
                tblPaging.Visible = false;
                return dtHotel;
            }
            if (xmlResponse.IndexOf("This PropertyId is deactivated") != -1)
            {
                rptResults.Visible = false;
                lblErrMsg.Text = "No Search allowed, This PropertyId is deactivated";
                tblPaging.Visible = false;
                return dtHotel;
            }

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/BasicPropertyInfo"); // You can filter elements         
            XmlNodeList hotelNodes = root.FirstChild.NextSibling.ChildNodes[0].ChildNodes;
            XmlNodeList rateNodes = root.FirstChild.NextSibling.ChildNodes[0].ChildNodes.Item(0).ChildNodes;

            foreach (XmlNode node in hotelNodes)
            {
                if (node.Name == "BasicPropertyInfo")
                {
                    DataRow dr = dtHotel.NewRow();

                    dr["hotelCode"] = node.Attributes["HotelCode"].Value;
                    dr["hotelName"] = node.Attributes["HotelName"].Value;
                    dr["hotelRating"] = node.Attributes["HotelRating"].Value;
                    dr["hotelCityCode"] = node.Attributes["HotelCityCode"].Value;

                    dr["RPH"] = node.FirstChild.ChildNodes[0].Attributes[0].Value;
                    dr["CurrencyCode"] = node.FirstChild.ChildNodes[1].InnerText;

                    if (node.FirstChild.ChildNodes[2].Attributes.Count > 0)
                        dr["MinRate"] = node.FirstChild.ChildNodes[2].Attributes[0].Value;
                    if (node.FirstChild.ChildNodes[3].Attributes.Count > 0)
                        dr["MaxRate"] = node.FirstChild.ChildNodes[3].Attributes[0].Value;

                    dr["DirectConnect"] = node.FirstChild.ChildNodes[4].Value;
                    dr["Description"] = node.FirstChild.NextSibling.InnerText;
                    dr["Image"] = node.FirstChild.NextSibling.NextSibling.InnerText;
                    dr["Position"] = node.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;

                    string strAddress = "";
                    for (int i = 0; i < node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.ChildNodes.Count; i++)
                    {

                        strAddress += (node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.ChildNodes[i].InnerText + ", ");
                    }

                    dr["Address"] = strAddress.Trim().TrimEnd(',');
                    dr["ContactNumbers"] = node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.Value;
                    dr["FaxNumber"] = node.FirstChild.ChildNodes[1].Value;
                    dtHotel.Rows.Add(dr);
                }
            }
            foreach (XmlNode node in rateNodes)
            {
                if (node.Name == "RoomRate")
                {
                    DataRow dr = dtRates.NewRow();
                    dr["RPH"] = node.Attributes["RPH"].Value;
                    dr["RoomTypeCode"] = node.Attributes["RoomTypeCode"].Value;
                    dr["RoomTypeName"] = node.Attributes["RoomTypeName"].Value;
                    dr["RatePlanCode"] = node.Attributes["RatePlanCode"].Value;
                    dr["INR"] = node.Attributes["Currency"].Value;

                    XmlNodeList allRates = node.FirstChild.ChildNodes;
                    foreach (XmlNode rate in allRates)
                    {
                        DataRow drRates = dtRateDays.NewRow();
                        drRates["Day"] = rate.Attributes["Day"].Value;
                        drRates["Amount"] = rate.Attributes["Amount"].Value;
                        drRates["RateType"] = rate.Attributes["RateType"].Value;
                        drRates["RoomTypeCode"] = node.Attributes["RoomTypeCode"].Value;
                        drRates["RatePlanCode"] = node.Attributes["RatePlanCode"].Value;
                        dtRateDays.Rows.Add(drRates);
                    }

                    dr["ExtraGuestCharge"] = node.FirstChild.NextSibling.InnerText;
                    dr["TotalTaxCharges"] = node.FirstChild.NextSibling.NextSibling.InnerText;

                    //XmlNodeList Occupancys = doc.SelectNodes("/trn:DSHotelAvailRSPL/RoomStays/RoomStay/", nsmgr);

                    // doc.SelectSingleNode("Success", nsmgr);
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "Occupancy")
                        {
                            dr["Max_Adult_Occupancy"] = child.ChildNodes[0].InnerText;
                            dr["Max_Child_Occupancy"] = child.ChildNodes[1].InnerText;
                            dr["Max_Infant_Occupancy"] = child.ChildNodes[2].InnerText;
                            dr["Max_Guest_Occupancy"] = child.ChildNodes[3].InnerText;
                            dr["Base_Adult_Occupancy"] = child.ChildNodes[4].InnerText;
                            dr["Base_Child_Occupancy"] = child.ChildNodes[5].InnerText;
                        }
                        if (child.Name == "Amenities")
                        {
                            string AmenitiesStr = "";
                            foreach (XmlNode amChild in child.ChildNodes)
                            {
                                AmenitiesStr += (amChild.InnerText + ", ");
                            }
                            AmenitiesStr = AmenitiesStr.Trim().Trim(',');
                            dr["Amenities"] = AmenitiesStr;
                        }
                    }
                    dtRates.Rows.Add(dr);
                }
            }
            /*
             * Starting the sorting/blocking of preferred hotels
            Cache.Insert("dsCity&NoiseData",dsCached, null, DateTime.Now.AddHours(12),Cache.NoSlidingExpiration);                
            */
            //Cache.Remove("_dsCached");
            if (Cache["_dsCached"] == null)
            {
                dsCached = new DataSet();
                /*string str = "select id,hotelid,valid,isnull(sorder,(select top 1 (sorder) from tbl_hotelsorder group by sorder order by sorder desc)+1) as sorder from tbl_hotelsorder where valid=1 order by isnull(sorder,(select top 1 (sorder) from tbl_hotelsorder group by sorder order by sorder desc)+1)";
                dsCached.Tables.Add(DataLib.GetDataTable(DataLib.Connection.ConnectionString, str));
                dsCached.Tables.Add(DataLib.GetDataTable(DataLib.Connection.ConnectionString, @"
                    SELECT     id , hotelId , lower(valid) as valid , sOrder 
                    FROM         
                    tbl_hotelsOrder
                    where  valid=0
                    ")); //*/
                ClsAdo clsObj = new ClsAdo();
                dsCached = clsObj.fnGetHotelInfoValid();
                Cache.Insert("_dsCached", dsCached, null, DateTime.Now.AddHours(12), Cache.NoSlidingExpiration);
            }
            else
            {
                dsCached = (DataSet)Cache["_dsCached"];
            }
            if (dsCached.Tables[1].Rows.Count > 0) //removing black listed hotels
            {

                for (short i = 0; i < dsCached.Tables[1].Rows.Count; i++)
                {
                    string hId = dsCached.Tables[1].Rows[i]["hotelId"].ToString();

                    if (dtHotel.Select("hotelCode = " + hId).Length > 0)
                    {
                        dtHotel.Select("hotelCode = " + hId)[0].Delete();
                        dtHotel.AcceptChanges();
                    }
                }
            }
            DataTable dtNewHotel = dtHotel.Clone();
            if (dsCached.Tables[0].Rows.Count > 0) //putting order up
            {

                try
                {
                    for (short i = 0; i < dsCached.Tables[0].Rows.Count; i++)
                    {
                        string hId = dsCached.Tables[0].Rows[i]["hotelId"].ToString();
                        if (dtHotel.Select("hotelCode = " + hId).Length > 0)
                        {
                            dtNewHotel.ImportRow(dtHotel.Select("hotelCode = " + hId)[0]);
                            dtHotel.Select("hotelCode = " + hId)[0].Delete();
                            dtHotel.AcceptChanges();
                            dtNewHotel.AcceptChanges();
                        }
                    }
                }
                catch { }
                dtNewHotel.Merge(dtHotel);
                return dtNewHotel;

            }

            return dtHotel;

        }
        DataSet dsCached;
        HtmlGenericControl spImages, diVratesTable;
        DataRow[] tempRows;
        protected void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                spImages = (HtmlGenericControl)(e.Item.FindControl("imgRating"));
                string ratingImages = "";
                string rating = Convert.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row["hotelRating"]);
                string RPH = Convert.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row["RPH"]).Trim();
                string currencyCode = Convert.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row["CurrencyCode"]).Trim();
                string hotelCode = Convert.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row["hotelCode"]).Trim();
                string hotelCityCode = Convert.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row["hotelCityCode"]).Trim();
                if (spImages != null)
                {
                    if (rating.Trim().Length > 0)
                    {
                        for (short i = 0; i < short.Parse(rating); i++)
                        {
                            ratingImages += "<img width=\"10\" height=\"10\" border=\"0\" src=\"Assets/images/star.gif\" />";
                        }
                    }
                    spImages.InnerHtml = ratingImages;
                }

                string strPriceTable = @"<table width=""100%"" cellspacing=""0"" cellpadding=""2"" border=""0"" class=""resultsPane"">
                        <tbody>
                            <tr>
                                 <td nowrap="""" align=""center"" class=""resultsPane"">
                                    <b>Room type</b></td>
                                <td class=""resultsPane"">
                                    <table width=""100%"" cellspacing=""1"" border=""0"" class=""resultsPane"">
                                        <tbody>
                                            <tr>";

                string tempStr = "";

                short width;
                if (dayDiff > 7) width = 13;
                else
                {
                    width = Convert.ToInt16(Convert.ToInt16(98) / dayDiff);
                }
                for (short i = 1; i <= dayDiff && i < 8; i++)
                {
                    tempStr += @"<td width=""" + width + @"%"" align=""center"" class=""resultsPane"">" + startDate.AddDays(i - 1).ToString("ddd") + "</td> ";
                }
                strPriceTable = strPriceTable + tempStr + @"</tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td align=""center"" class=""resultsPane"">
                                    <b>Total <font size=""3"" class=""hilighted-text"">*</font> </b>
                                </td>
                                <td bgcolor=""#ffffff"" colspan=""3"" class=""resultsPane"">
                                </td>
                            </tr> ";

                tempRows = dtRates.Select("RPH = '" + RPH + "'");
                //
                tempStr = "";
                for (short i = 0; i < tempRows.Length; i++)
                {

                    tempStr += @"<tr>                               
                                <td class=""resultsPane"" align=""left"">
                                    <b>" + tempRows[i]["RoomTypeName"].ToString() + @"</b><br/>" + tempRows[i]["Amenities"].ToString() + @"
                                </td>      
                                <td class=""resultsPane"">
                                    <table width=""100%"" cellspacing=""1"" cellpadding=""1"" border=""0"" class=""table1-light-bg"">
                                        <tbody>
                                            <tr class=""table1-light-bg"">";
                    DataRow[] tempRwSellRates = dtRateDays.Select("RatePlanCode = " + tempRows[i]["RatePlanCode"] + " and RateType='SellRate'");
                    DataRow[] tempRwNetRates = dtRateDays.Select("RatePlanCode = " + tempRows[i]["RatePlanCode"] + " and RateType='NetRate'");
                    short roomCount = Convert.ToInt16(paxDetailsArr.GetLength(0));
                    string tempRates = "";
                    float tax = float.Parse(tempRows[i]["TotalTaxCharges"].ToString());
                    float extraGuestCharge = float.Parse(tempRows[i]["ExtraGuestCharge"].ToString());
                    float perdayAmount = 0.0f, totalAmount = 0.0f;
                    if (tempRwSellRates.Length > 7)
                        tempStr += @"<td align=""center"" class=""table1-light-bg"">Week 1</td>";
                    for (short k = 0; k < tempRwSellRates.Length; k++)
                    {
                        perdayAmount = float.Parse(tempRwSellRates[k]["Amount"].ToString());
                        totalAmount += perdayAmount;
                        tempRates += @"<td width=""14%"" align=""center"" class=""table1-light-bg"">
                                                                    " + perdayAmount / roomCount + @"
                                                                </td>";
                        if (k % 6 == 0 && k != 0)    //tempRwSellRates[k]["Amount"]
                        {
                            tempRates += @"</tr><tr><td align=""center"" class=""table1-light-bg"">Week " + Convert.ToString((k % 6) + 2) + "</td>";
                        }
                    }
                    totalAmount += extraGuestCharge;
                    totalAmount += tax;

                    //Start Checking if any room guest/child count exceeds the max limit.



                    string strBookButton = @"<input width=""62"" type=""image""  height=""19"" border=""0"" onclick=""return bookHotel(" + (e.Item.ItemIndex + 1) + "," + (i + 1) + @");"" class=""noborder"" src=""Assets/images/book_it.gif"" />";
                    for (short rCount = 0; rCount < roomCount; rCount++)
                    {
                        if (paxDetailsArr[rCount, 0] > short.Parse(tempRows[i]["Max_Adult_Occupancy"].ToString()))
                        {
                            strBookButton = "Exceeds Max Adulits";
                            break;
                        }
                        if (paxDetailsArr[rCount, 1] > short.Parse(tempRows[i]["Max_Child_Occupancy"].ToString()))
                        {
                            strBookButton = "Exceeds Max Childs";
                            break;
                        }
                    }
                    ////End Checking Checking if any room guest/child count exceeds the max limit.
                    tempStr = tempStr + tempRates + @" </tr>
                                        </tbody>
                                    </table>
                                </td>                            
                                <td align=""center"" title=""Tax & Service Fee: " + currencyCode + @" " + tax + @" Room Rate  : " + currencyCode + @" " + (totalAmount - tax) + @""" size=""1"" font="""" class=""resultsPane"">
                                    " + currencyCode + @" " + totalAmount + @"</td>
                                <td bgcolor=""#ffffff"" align=""center"" class=""resultsPane"">
                                    <input type=""hidden"" value=""" + extraGuestCharge + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strExtraGuestCharge" + (i + 1) + @""" />                                                                                                                                           
                                    <input type=""hidden"" value=""" + tax + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strHotelTax" + (i + 1) + @""" />                                                                                                                                           
                                    <input type=""hidden"" value=""" + totalAmount + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strHotelTotalAmount" + (i + 1) + @""" />                                                                                                                                           
                                    <input type=""hidden"" value=""" + tempRows[i]["RoomTypeCode"] + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strRoomId" + (i + 1) + @""" />                                                                                                                                           
                                    <input type=""hidden"" value=""" + tempRows[i]["RatePlanCode"] + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strRateplanId" + (i + 1) + @""" />
                                    <input type=""hidden"" value=""" + tempRows[i]["RoomTypeName"].ToString() + "_" + tempRows[i]["Amenities"].ToString() + @""" id=""Hotel" + (e.Item.ItemIndex + 1) + @"strRoomDetails" + (i + 1) + @""" />" +
                    strBookButton + @"                                    
                                </td>                                
                            </tr>";
                }

                strPriceTable = strPriceTable + tempStr + @"</tbody>
                    </table>";
                // diVratesTable = (HtmlGenericControl)(e.Item.FindControl("ratesTable"));
                //if (tempRows.Length > 0) strPriceTable = "";
                // diVratesTable.InnerHtml = strPriceTable;
                AjaxControlToolkit.TabContainer TabContainer1 = (AjaxControlToolkit.TabContainer)e.Item.FindControl("TabContainer1");
                DataList dlGallery = (DataList)TabContainer1.Tabs[4].FindControl("dlGallery");
                Image ImgUrl = (Image)TabContainer1.Tabs[4].FindControl("ImgUrl");
                Literal ltRoomDesc = (Literal)TabContainer1.Tabs[1].FindControl("ltRoomDesc");
                Literal listrFac = (Literal)TabContainer1.Tabs[2].FindControl("listrFac");
                Literal listrInaA = (Literal)TabContainer1.Tabs[3].FindControl("listrInaA");

                diVratesTable = (HtmlGenericControl)(TabContainer1.Tabs[0].FindControl("ratesTable"));
                diVratesTable.InnerHtml = strPriceTable;



                //if (ViewState["_CurrentPage"] == null)
                bindHtDesc(hotelCityCode, hotelCode, dlGallery, ImgUrl, ltRoomDesc, listrInaA, listrFac);
            }
        }
        protected string strDesc = "", strRooms = "", strFac = "", strInaA = "";
        private void bindHtDesc(string strhotelCityCode, string strhotelCode, DataList dlGallery, Image ImgUrl, Literal ltRoomDesc,
           Literal listrInaA, Literal listrFac)
        {
            #region"Pass the value for fatch Hotel detail"
            strDesc = ""; strRooms = ""; strFac = ""; strInaA = "";

            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
            string hotelCityCode = strhotelCityCode;
            string hotelCode = strhotelCode;



            string xmlReqStr = @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:eb=""http://www.ebxml.org/namespaces/messageHeader"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
            <SOAP-ENV:Header/>
            <SOAP-ENV:Body>
                <DS_HotelPropertyDescription_RQ xmlns=""urn:Hotel_Description"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" EchoToken=""String"" TimeStamp=""2001-12-17T09:30:47-05:00"" PrimaryLangID=""en-us"" AltLangID=""en-us"">
                    <POS>
                        <Source Username=""" + userName + @""" Password=""" + passWord + @""" PropertyID=""" + propertyId + @"""/>
                    </POS>
                    <AvailRequestSegments>
                        <AvailRequestSegment>
                            <HotelSearchCriteria>
                                <Criterion>
                                    <HotelRef HotelCityCode=""" + hotelCityCode + @""" HotelCode=""" + hotelCode + @"""/>
                                </Criterion>
                            </HotelSearchCriteria>
                        </AvailRequestSegment>
                    </AvailRequestSegments>
                </DS_HotelPropertyDescription_RQ>
            </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";
            XmlDocument doc = new XmlDocument();
            string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            string xmlResponse = DataLib.HttpSOAPRequest(xmlReqStr, uri, null);
            doc.LoadXml(xmlResponse);

            XmlElement root = doc.DocumentElement;
            XmlNodeList allSubSection = root.FirstChild.NextSibling.FirstChild.FirstChild.ChildNodes;
            #endregion

            foreach (XmlNode eachSubSec in allSubSection)
            {

                #region"Hotel Description"
                if (eachSubSec.Attributes["SubTitle"].Value == "HotelDescription")
                {
                    XmlNodeList allParaGraph = eachSubSec.ChildNodes;
                    foreach (XmlNode singlePara in allParaGraph)
                    {
                        strDesc += ("<p>" + singlePara.FirstChild.InnerText + "</p>");
                    }
                }
                #endregion

                #region"Hotal Rooms"
                if (eachSubSec.Attributes["SubTitle"].Value == "Rooms")
                {

                    strRooms = @"<table cellpadding=""5"" cellspacing=""5""  width=""100%"">
                              
                                <tr>
                                <td align=""center"">";
                    XmlNodeList allRooms = eachSubSec.ChildNodes;
                    foreach (XmlNode singleRoom in allRooms)
                    {

                        strRooms += @"<table border=""0"" cellspacing=""1""  width=""100%"">
                                  <tr>
                                      <td   align=""left""><b>" + singleRoom.Attributes["Name"].Value + @"</b>
              	                      </td>
                                  </tr>
                                  <tr>
                                      <td width=""100%"" bgcolor=#FFFEEC colspan=""2"">
                                          <div align=""center"">
                                            
                                                  <table border=""0"" cellspacing=""0"" width=""100%"">
                                                      <tr> 
						             	                    <td width=""15%"" bgcolor=#FFFEEC ><img border=""1"" src=""" + singleRoom.FirstChild.NextSibling.InnerText + @""" width=""111"" height=""110""></td>
                    						                
						                                    <td align=""left"" bgcolor=#FFFEEC colspan=""2"" width=""85%"" valign=""top""><p  align=""justify"">" + singleRoom.FirstChild.InnerText + @"</p><br>
						                                    </td>
						                              </tr>
                                                  </table>
                                           </div>         

                           			 
			                      </td>
                               </tr>
                             </table>";
                    }
                    strRooms += @" </td>
                               </tr>
                             </table>";
                    ltRoomDesc.Text = strRooms;
                }
                #endregion

                #region"Hotal Facilities"
                if (eachSubSec.Attributes["SubTitle"].Value == "Facilities")
                {
                    strFac = @"<table border=""0"" cellspacing=""2"" cellpadding=""2"" border=""0"" width=""100%"">";
                    XmlNodeList allFac = eachSubSec.ChildNodes;

                    foreach (XmlNode _type in allFac)
                    {
                        strFac += @"
                <tr>
            			<td><b>" + _type.Name + @"</b></td>
                </tr>
                <tr>
        	        <td align=""left"" ></td><td align=""left"">";
                        string innerTable = @"<table border=""0"" cellpadding=""3"" cellspacing=""1"" width=""100%""><tr><td>";
                        XmlNodeList allText = _type.ChildNodes;
                        short i = 1;
                        foreach (XmlNode text in allText)
                        {
                            innerTable += @"<li>" + text.InnerText + @"</li></td><td>";
                            if (i % 3 == 0)
                            {
                                innerTable += @"</td></tr><tr><td>";
                            }
                            i++;
                        }
                        innerTable += @"</td></tr></table>";
                        strFac += (innerTable + @"</td></tr>");
                    }
                    strFac += @"</table>";
                    listrFac.Text = strFac;
                }
                #endregion

                #region"Hotal Attractions"
                if (eachSubSec.Attributes["SubTitle"].Value == "Attractions")
                {


                    XmlNodeList allAtt = eachSubSec.ChildNodes;

                    if (allAtt.Count != 0)
                    {
                        int counter = 1;
                        strInaA = @"<table border=""0"" cellspacing=""2""  class=resultsPane cellpadding=""2"" border=""0"" width=""100%"">";

                        strInaA += @"<tr><td  bgcolor=#e5e5e5 align=""left""><b> SrNo.</b></td>";
                        strInaA += @"<td  bgcolor=#e5e5e5 align=""left""><b>Name of Attraction</b></td>";
                        strInaA += @"<td  bgcolor=#e5e5e5 align=""left""><b>Distance (Km)</b></td></tr>";

                        foreach (XmlNode _type in allAtt)
                        {


                            strInaA += @"
                <tr>
            			<td class=resultsPane>" + counter + @"</font></td>
                        <td class=resultsPane>" + _type.ChildNodes[0].InnerText + @"</td>
                        <td class=resultsPane>" + _type.ChildNodes[1].InnerText + @"</td>
                </tr>";

                            counter++;
                        }
                    }
                    else
                    {
                        strInaA += @"<tr>
            			<td colspan=3 style=font-size:medium;><font ><b>No Information Available. </b></font></td>
                        
                    </tr>";
                    }
                    strInaA += @"</table>";
                    listrInaA.Text = strInaA;
                }
                #endregion

                #region"Hotal Photos

                try
                {


                    if (eachSubSec.Attributes["SubTitle"].Value == "Images")
                    {
                        int counter = 1;
                        XmlNodeList allImages = eachSubSec.ChildNodes;

                        DataTable dtImages = new DataTable();
                        // dtImages.Columns.Add("ID", System.Type.GetType("System.String"));
                        dtImages.Columns.Add("Imageurl", System.Type.GetType("System.String"));

                        foreach (XmlNode _type in allImages)
                        {
                            DataRow dr = dtImages.NewRow();
                            // dr["ID"] = counter.ToString();
                            dr["Imageurl"] = _type.ChildNodes[0].InnerText;

                            dtImages.Rows.Add(dr);
                            counter++;
                        }
                        ImgUrl.ImageUrl = dtImages.Rows[0]["Imageurl"].ToString();
                        //    
                        dlGallery.DataSource = dtImages;
                        dlGallery.DataBind();

                    }
                }
                catch { }

                #endregion

            }

        }
        protected void hotelNo_ServerChange(object sender, EventArgs e)
        {

        }
        protected void cmdPrev_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            bindHotels();

        }
        protected void cmdNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            bindHotels();
        }
        protected void cmdPrev1_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            bindHotels();
        }
        protected void cmdNext1_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            bindHotels();
        }
        //short[,] paxDetailsArr;
        short[,] paxChildAgeArr;
        private void setUpData()
        {
            //strCity=other&strOtherCity=dddddd

            string strCity = Request["strCity"].ToString().Trim();
            if (strCity == "other")
                strCity = Request["strOtherCity"].ToString().Trim();


            string XmlString = "";

            int roomCount = Convert.ToInt16(Request["strRooms"].ToString());
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
            //CheckOutDate=&CheckOutMonth=&CheckOutYear
            //CheckInDate=&CheckInMonth=&CheckInYear
            XmlString =
              @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
            xmlns:eb=""http://www.ebxml.org/namespaces/messageHeader"" xmlns:xlink=""http://www.w3.org/1999/xlink""
            xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
            <SOAP-ENV:Header/>
                <SOAP-ENV:Body>
                    <DSAvailableHotelRQPL xmlns=""urn:Hotel_Search""  xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" PrimaryLangID=""en-us"" AltLangID=""en-us"">
                            <POS>
                                 <Source Username='" + userName + @"' Password='" + passWord + @"' PropertyID='" + propertyId + @"' />
                            </POS>
                            <AvailRequestSegments>
                                <AvailRequestSegment>
                                    <StayDateRange Start='" + Convert.ToInt16(Request["CheckInYear"].ToString()).ToString("00") + "-" + Convert.ToInt16(Request["CheckInMonth"].ToString()).ToString("00") + "-" + Convert.ToInt16(Request["CheckInDate"].ToString()).ToString("00") + @"T01:00:00' End='" + Convert.ToInt16(Request["CheckOutYear"].ToString()).ToString("00") + "-" + Convert.ToInt16(Request["CheckOutMonth"].ToString()).ToString("00") + "-" + Convert.ToInt16(Request["CheckOutDate"].ToString()).ToString("00") + @"T23:59:59'/>
                                        <RoomStayCandidates>";

            string PaxDetails = "";
            paxDetailsArr = new short[roomCount, 2];
            paxChildAgeArr = new short[roomCount, 4];
            for (short a = 1; a <= roomCount; a++)
            {
                string GuestCount = Request["strAdultsRoom" + a.ToString()].ToString();
                short ChildCount = short.Parse(Request["strChildrenRoom" + a.ToString()].ToString());
                paxDetailsArr[a - 1, 0] = short.Parse(GuestCount);
                paxDetailsArr[a - 1, 1] = ChildCount;
                string childString = "<ChildCounts>";
                if (ChildCount > 0)
                {
                    for (short c = 1; c <= ChildCount; c++)
                    {
                        string childAge = Request["strAgeChild" + c + "Room" + a].ToString();
                        childString += "<ChildAge Age='" + childAge + "'/>";
                        paxChildAgeArr[a - 1, c - 1] = Convert.ToInt16(childAge);
                    }
                }
                childString += "</ChildCounts>";

                PaxDetails += @"<RoomStayCandidate> 
                                                        <GuestCounts>
                                                            <GuestCount Count='" + GuestCount + @"'/>
                                                        </GuestCounts> 
                                                        childDesc                         
                                                    </RoomStayCandidate>";
                if (ChildCount == 0) PaxDetails = PaxDetails.Replace("childDesc", "");
                else PaxDetails = PaxDetails.Replace("childDesc", childString);
            }
            Session["PaxArr"] = paxDetailsArr;
            Session["chdAgeArr"] = paxChildAgeArr;




            XmlString += PaxDetails;
            XmlString += @"</RoomStayCandidates>
                                        <HotelSearchCriteria>
                                            <Criterion>
                                                <HotelRef HotelCityName='" + strCity + @"' HotelName='' Area='' Attraction='' Rating=''/>
                                                <Sorting Preference='2'/>
                                            </Criterion>
                                        </HotelSearchCriteria>
                                </AvailRequestSegment>
                            </AvailRequestSegments>
                    </DSAvailableHotelRQPL>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

            Cache.Insert("xmlStr" + Session.SessionID, XmlString, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
            //Response.Write("<!-- " + XmlString + " -->>");

        }
        private int CurrentPage
        {
            get
            {
                object o = this.ViewState["_CurrentPage"];
                if (o == null)
                    return 0; // default page index of 0
                else
                    return (int)o;
            }
            set
            {
                this.ViewState["_CurrentPage"] = value;
            }
        }
    }
}