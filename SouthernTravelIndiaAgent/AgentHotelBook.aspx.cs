using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentHotelBook : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        private string hotelRating = "0";
        protected string RateString = "", strTerms = "";
        DateTime startDate, endDate;
        protected short totalAdult = 0, totalChild = 0;
        string provID = "";

        protected float tax = 0.0f, totalAmount = 0.0f, extraGuest = 0.0f;
        short dateDiff;
        protected short[,] paxDetailsArr;
        string strHotelName = "", strHotelAddress = "", strHotelRating;
        protected string priceTable = "";
        string hotelCode = "", ratePlanType = "", roomTypeCode = "";

        short[,] chdArr;

        float revisedPrice = 0.0f;
        string msg = "", provBookId = "";
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)

        {
            try
            {
                if (!IsPostBack)
                {
                    provID = bookProvisional();
                    if (provID == "NA")
                    {
                        spMsg.InnerHtml = "Sorry, Accomodation is not Available";
                        btnSubmit.Enabled = false;
                    }
                    displayData();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<1-- " + ex.ToString() + "--->");
                mainDiv.Visible = false;
            }

        }
        protected void btnSubmit_Click1(object sender, ImageClickEventArgs e)
        {
            //Book a provisional

            //childDesc

            //End book
            //string provBookId = bookProvisional();
            //if (provBookId.Trim().Length > 0)
            //{
            // Session["provHotelBookId"] = provBookId;
            if ((Session["AgentId"] != null) || (Session["BranchId"] != null))
            {
                Response.Redirect("AgentHotelLastBooking.aspx", true);
            }
            else
            {
                Response.Redirect("HotelLastBookingCust.aspx", true);
            }
            //}
            //Transfer to payment page
        }
        #endregion
        #region "Method(s)"
        private string bookProvisional()
        {
            float totalAmount = float.Parse(Session["strHotelTotal"].ToString());
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
            hotelCode = Convert.ToString(Session["hotelId"]);
            ratePlanType = Convert.ToString(Session["ratePlanId"]);
            roomTypeCode = Convert.ToString(Session["roomId"]);

            string xmlString = Convert.ToString(Cache["xmlStr" + Session.SessionID]);

            string startDate = xmlString.Substring(xmlString.IndexOf("Start") + 7, 19);
            string endDate = xmlString.Substring(xmlString.IndexOf("End") + 5, 19);

            chdArr = (short[,])Session["chdAgeArr"];
            paxDetailsArr = (short[,])(Session["PaxArr"]);
            totalAdult = 0;
            totalChild = 0;
            for (short k = 0; k < paxDetailsArr.GetLength(0); k++)
            {
                totalAdult += paxDetailsArr[k, 0];
                totalChild += paxDetailsArr[k, 1];
            }

            short startStayPos = (Int16)xmlString.ToString().IndexOf("<RoomStayCandidates>");
            short endStayPos = (Int16)(xmlString.ToString().IndexOf("</RoomStayCandidates>") + 21);



            //string strRoomStay = xmlString.Substring(startStayPos, endStayPos - startStayPos);
            //startStayPos = (Int16)strRoomStay.ToString().IndexOf("<ChildCounts>");
            //endStayPos = (Int16)(strRoomStay.ToString().IndexOf("</ChildCounts>") + 21);
            ////strRoomStay = strRoomStay.Remove(startStayPos, endStayPos - startStayPos);


            string provBookReq = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:eb=""http://www.ebxml.org/namespaces/messageHeader"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
        <SOAP-ENV:Header/>
        <SOAP-ENV:Body>
        <DS_RoomBlock_RQ xmlns=""urn:Hotel_Reserve"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" PrimaryLangID=""en-us"" AltLangID=""en-us"">
            <POS>
                <Source Username=""" + userName + @""" Password=""" + passWord + @""" PropertyID=""" + propertyId + @""" />
            </POS>
            <HotelReservations>
                <HotelReservation>    
                    <RoomStays>
                        <RoomStay>
                            <RoomTypes>
                                <RoomType RoomTypeCode=""" + roomTypeCode + @""" RatePlanCode=""" + ratePlanType + @""" NumberOfUnits=""" + paxDetailsArr.GetLength(0) + @"""/>
                            </RoomTypes>
                            roomStayCandidates                             
                            <TimeSpan Start=""" + startDate + @""" End=""" + endDate + @"""/>
                            <BasicPropertyInfo HotelCode=""" + hotelCode + @""" AmountCharged=""" + totalAmount + @"""/>
                        </RoomStay>
                    </RoomStays>
                </HotelReservation>
            </HotelReservations>
        </DS_RoomBlock_RQ>
        </SOAP-ENV:Body>
        </SOAP-ENV:Envelope>";

            string PaxDetails = "";
            for (short a = 0; a < paxDetailsArr.GetLength(0); a++)
            {
                string GuestCount = Convert.ToString(paxDetailsArr[a, 0]);
                short ChildCount = paxDetailsArr[a, 1];
                string childString = "<Children>";
                if (ChildCount > 0)
                {
                    for (short c = 0; c < ChildCount; c++)
                    {

                        childString += "<Child Age='" + chdArr[a, c] + "'/>";

                    }
                }
                childString += "</Children>";

                PaxDetails += @"<RoomStayCandidate> 
                                                        <GuestCounts>
                                                            <GuestCount Count='" + GuestCount + @"'/>
                                                        </GuestCounts> 
                                                        childDesc                         
                                                    </RoomStayCandidate>";
                if (ChildCount == 0) PaxDetails = PaxDetails.Replace("childDesc", "");
                else PaxDetails = PaxDetails.Replace("childDesc", childString);
            }

            provBookReq = provBookReq.Replace("roomStayCandidates", PaxDetails);

            System.Text.StringBuilder sbChild = new System.Text.StringBuilder();
            //sbChild.Append("<Children>");
            //for (short n = 0; n < paxDetailsArr.GetLength(0); n++)
            //{
            //    for (short cCount = 0; cCount < paxDetailsArr[n, 1]; cCount++)
            //    {
            //        sbChild.Append("<Child Age='" + chdArr[n, cCount] + "'/>");
            //    }
            //}
            //sbChild.Append("</Children>");
            //if (totalChild == 0) sbChild.Remove(0, sbChild.Length);

            //provBookReq = provBookReq.Replace("childDesc", sbChild.ToString());

            XmlDocument doc = new XmlDocument();
            string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            string xmlResponse = "";
            try
            {
                xmlResponse = DataLib.HttpSOAPRequest(provBookReq, uri, null);
                if ((ConfigurationManager.AppSettings["probeMailId"] != null) && (ConfigurationManager.AppSettings["probeMailId"].ToString() != ""))
                {
                    SendMail(ConfigurationManager.AppSettings["probeMailId"].ToString(), provBookReq, "Prov request");
                    SendMail(ConfigurationManager.AppSettings["probeMailId"].ToString(), xmlResponse, "Prov response");

                }
            }
            catch (Exception ex)
            {
                btnSubmit.Enabled = false;
                RegisterStartupScript("Error", "<Script>alert('Our Desiya booking service is under main');</Script>");
            }


            //        xmlResponse = @"
            //                <DS_HotelAvailCheck_RS xmlns='urn:Hotel_Reserve' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' PrimaryLangID='en-us' AltLangID='en-us'>
            //                    <Error>Sorry, Your Availabity request could not be processed</Error>
            //                </DS_HotelAvailCheck_RS>";

            //        xmlResponse = @"
            //                <DS_HotelAvailCheck_RS xmlns='urn:Hotel_Reserve' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' PrimaryLangID='en-us' AltLangID='en-us'>
            //                    <RateChanged ModifiedRates='1355.0'>Sorry,the rates for the room you selected has been updated.</RateChanged>
            //                </DS_HotelAvailCheck_RS>";

            //        xmlResponse = @" 
            //                <DS_HotelAvailCheck_RS xmlns='urn:Hotel_Reserve' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' PrimaryLangID='en-us' AltLangID='en-us'>
            //                    <Inventory ProvisionalBookingId='0000004063'>Available</Inventory>
            //                </DS_HotelAvailCheck_RS>";


            doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlResponse);
                XmlElement root = doc.DocumentElement;
                if (root.FirstChild.Name.Trim().ToUpper() == "ERROR")
                {
                    msg = root.FirstChild.InnerText;
                    provBookId = "NA";
                }
                else if (root.FirstChild.Name.Trim().ToUpper() == "RATECHANGED")
                {
                    revisedPrice = float.Parse(root.FirstChild.Attributes["ModifiedRates"].Value);
                    hotelSpecificDetails();
                    provBookId = bookProvisional();
                    msg = root.FirstChild.InnerText;
                }
                else if (root.FirstChild.Name.Trim().ToUpper() == "INVENTORY")
                {
                    provBookId = root.FirstChild.Attributes["ProvisionalBookingId"].Value;

                }
                Session["provHotelBookId"] = provBookId;
                spMsg.InnerHtml = msg;
                return provBookId;
                //if (provBookId.Trim().Length > 0)
                //{
                //    Session["provHotelBookId"] = provBookId;
                //    Response.Redirect("HotelLastBooking.aspx", true);
                //}
                //
            }
            catch (Exception ex)
            {
                //Response.Write("<!-- " + ex.ToString() + " -->");
                return "ERROR";
            }
        }
        private void hotelSpecificDetails()
        {
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
            hotelCode = Convert.ToString(Session["hotelId"]);
            ratePlanType = Convert.ToString(Session["ratePlanId"]);
            roomTypeCode = Convert.ToString(Session["roomId"]);

            //RoomStayCandidates
            string xmlString = Convert.ToString(Cache["xmlStr" + Session.SessionID]);

            short startPos = (Int16)xmlString.IndexOf("<RoomStayCandidates>");
            short endtPos = (Int16)(xmlString.IndexOf("</RoomStayCandidates>") + 21);
            string roomStayStr = xmlString.Substring(startPos, endtPos - startPos);

            string startDate = xmlString.Substring(xmlString.IndexOf("Start") + 7, 19);
            string endDate = xmlString.Substring(xmlString.IndexOf("End") + 5, 19);

            string hotelSpecReq = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""  xmlns:eb=""http://www.ebxml.org/namespaces/messageHeader"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
                <SOAP-ENV:Header/>
                <SOAP-ENV:Body>
                <DS_HotelSpecific_RQ xmlns=""urn:Hotel_Specific_Search"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" PrimaryLangID=""en-us"" AltLangID=""en-us"">
                    <POS>
                        <Source Username=""" + userName + @""" Password=""" + passWord + @""" PropertyID=""" + propertyId + @""" />
                    </POS>
                    <AvailRequestSegments>
                        <AvailRequestSegment>
                            <StayDateRange Start=""" + startDate + @""" End=""" + endDate + @"""/>
                                RoomStayCandidatesStr
                            <HotelSearchCriteria>
                                <Criterion>
                                    <HotelCriteria HotelCode= """ + hotelCode + @""" RoomTypeCode='" + roomTypeCode + @"' RatePlanCode='" + ratePlanType + @"'/>
                                </Criterion>
                            </HotelSearchCriteria>
                        </AvailRequestSegment>
                    </AvailRequestSegments>
                </DS_HotelSpecific_RQ>
                </SOAP-ENV:Body>
                </SOAP-ENV:Envelope>";
            hotelSpecReq = hotelSpecReq.Replace("RoomStayCandidatesStr", roomStayStr);
            XmlDocument doc = new XmlDocument();
            string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            string xmlResponse = DataLib.HttpSOAPRequest(hotelSpecReq, uri, null);

            doc.LoadXml(xmlResponse);
            XmlElement root = doc.DocumentElement;

            extraGuest = 0.0f;
            tax = 0.0f;
            totalAmount = 0.0f;


            XmlNodeList rates = root.FirstChild.NextSibling.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes;
            foreach (XmlNode xn in rates)
            {
                totalAmount += (float.Parse(xn.Attributes["Amount"].Value));
            }
            extraGuest = float.Parse(root.FirstChild.NextSibling.FirstChild.FirstChild.FirstChild.FirstChild.NextSibling.InnerText);
            tax = float.Parse(root.FirstChild.NextSibling.FirstChild.FirstChild.FirstChild.FirstChild.NextSibling.NextSibling.InnerText);
            totalAmount += (extraGuest + tax);

            Session["strExtraGuestCharge"] = extraGuest;
            Session["strHotelTax"] = tax;
            Session["strHotelTotal"] = totalAmount;
        }
        private void SendMail(string to, string strToSend, string sub)
        {
            try
            {

                //MailMessage sendMail = new MailMessage();
                //sendMail.To = to;
                //sendMail.From = ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                //sendMail.Body = strToSend;
                //sendMail.BodyFormat = MailFormat.Text;
                //sendMail.Subject = sub;
                //SmtpMail.Send(sendMail);


                ClsCommon.sendmail(to, "", "", ConfigurationSettings.AppSettings["mailAddress"].ToString(), sub, strToSend, "");


            }
            catch (Exception ex)
            {
                Response.Write("<!-- " + ex.ToString() + " -->");
            }
            finally
            {
            }
        }
        private void displayData()
        {

            HtmlGenericControl spImages = (HtmlGenericControl)(FindControl("imgRating"));
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];



            string ratingImages = "";

            paxDetailsArr = (short[,])(Session["PaxArr"]);
            hotelCode = Convert.ToString(Session["hotelId"]);
            ratePlanType = Convert.ToString(Session["ratePlanId"]);
            roomTypeCode = Convert.ToString(Session["roomId"]);

            extraGuest = float.Parse(Session["strExtraGuestCharge"].ToString());
            tax = float.Parse(Session["strHotelTax"].ToString());
            totalAmount = float.Parse(Session["strHotelTotal"].ToString());

            startDate = Convert.ToDateTime(Session["checkInDate"].ToString());
            endDate = Convert.ToDateTime(Session["checkOutDate"].ToString());

            int roomCount = paxDetailsArr.GetLength(0);

            TimeSpan dtSpan = endDate - startDate;
            int nights = dtSpan.Days;

            lblCheckOutDate.Text = endDate.ToString("ddd, dd MMM, yyyy");
            lblCheckInDate.Text = startDate.ToString("ddd, dd MMM, yyyy");
            lblHotelName.Text = Session["strHotelName"].ToString();
            lblHotelAddress.Text = Session["strHotelAddress"].ToString();
            strHotelRating = Session["strHotelRating"].ToString();

            float perNightRoomRate = totalAmount - (tax + extraGuest);
            perNightRoomRate = perNightRoomRate / (nights * roomCount);
            float perNightTax = tax / (nights * roomCount);
            priceTable = @"<table width=""100%"" cellspacing=""0"" cellpadding=""2"" border=""0"" style=""border-collapse: collapse;"">
        <tr>
            <td colspan=""5"">
                <img width=""15"" height=""16"" border=""0"" src=""Images/2.gif"" />
                <font class=""h2"">Review Rate Details</font><hr size=""1"" noshade="""" color=""#e1e1e1"" />
            </td>
        </tr>";
            for (short n = 0; n < paxDetailsArr.GetLength(0); n++)
            {
                priceTable += @"<tr>
                            <td>
                                <b><font size=""2"" color=""#31349b"">Room : " + (n + 1) + @" </font>Adult: " + paxDetailsArr[n, 0] + @" Children: " + paxDetailsArr[n, 1] + @"</b>
                            </td>
                        </tr>
                        <tr align=""center"" class=""resultsPane"">
                            <td>
                            </td>
                        </tr>";
                string nights_Str = "";
                for (short nt = 0; nt < nights; nt++)
                {
                    nights_Str += @"<tr>
                                                    <td>
                                                        Room Rate Night " + (nt + 1) + " :INR " + perNightRoomRate + @"
                                                    </td>
                                                </tr>";
                }
                priceTable = priceTable + nights_Str + @"<tr>
                            <td>
                                Tax & Service Charge Per Night :INR " + perNightTax + @"
                            </td>
                        </tr>";
            }
            priceTable += @"
            <tr>
                <td>
                    <b>Total Rate Including Taxes & Services : <font size=""3"" color=""#31349b"">INR " + totalAmount + @"</font></b>
                </td>
            </tr>
            <tr id=""couponNewDetail1"" style=""display: none;"">
                <td>
                    <b>Discounted Amount:</b> <b><font size=""3"" color=""#31349b"" id=""couponAmount"" /></b>
                </td>
            </tr>
            <tr id=""couponNewDetail2"" style=""display: none;"">
                <td>
                    <b>Total rate after discount :</b> <b><font size=""3"" color=""#31349b"" id=""DiscountedRate"" />
                    </b>
                </td>
            </tr>
        <!-- start of changes for coupon discount -->
    </table>";
            totalAdult = 0;
            for (short k = 0; k < paxDetailsArr.GetLength(0); k++)
            {
                totalAdult += paxDetailsArr[k, 0];
                totalChild += paxDetailsArr[k, 1];
            }
            lblAdultCount.Text = totalAdult.ToString();
            dateDiff = short.Parse(((TimeSpan)(endDate - startDate)).Days.ToString());
            lblDateDiff.Text = dateDiff.ToString();


            if (spImages != null)
            {
                for (short i = 0; i < short.Parse(hotelRating); i++)
                    ratingImages += "<img width=\"10\" height=\"10\" border=\"0\" src=\"Images/star.gif\" />";
                spImages.InnerHtml = ratingImages;
            }
            //
            string strHotelAgreementReq = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:eb=""http://www.ebxml.org/namespaces/messageHeader"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
        <SOAP-ENV:Header> </SOAP-ENV:Header>
            <SOAP-ENV:Body>
                <DSHotelAgreementRQPL xmlns=""urn:Hotel_Agreement"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" AltLangID=""en-us"" PrimaryLangID=""en-us"">
                    <POS>
                        <Source Username='" + userName + "' Password='" + passWord + @"'  PropertyID='" + propertyId + @"'/>
                    </POS>
                    <SelectCriteria>
                        <HotelCode>" + hotelCode + @"</HotelCode>
                        <RatePlanType>" + ratePlanType + @"</RatePlanType>
                        <RoomTypeCode>" + roomTypeCode + @"</RoomTypeCode>
                    </SelectCriteria>
                </DSHotelAgreementRQPL>
            </SOAP-ENV:Body>
        </SOAP-ENV:Envelope>";
            XmlDocument doc = new XmlDocument();
            string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            string xmlResponse = DataLib.HttpSOAPRequest(strHotelAgreementReq, uri, null);

            //Response.Write("<br>Response from Desiya <br><br><font size=\"3\" face=\"arial\" color=\"green\" ><b>" + Server.HtmlEncode(s) + "</b></font><br>");
            string[] strCancellationArr, strHotelRulesArr;
            strTerms = "";
            if (xmlResponse.IndexOf("Success") != -1)
            {
                strCancellationArr = xmlResponse.Replace("<Policy>", "^").Split('^');
                strHotelRulesArr = xmlResponse.Replace("<HotelPolicy>", "^").Split('^');

                if (strHotelRulesArr.Length > 2)
                {
                    for (short i = 1; i < strHotelRulesArr.Length; i++)
                    {
                        strTerms += @"<li> " + strHotelRulesArr[i] + " </li>";
                    }
                }
                if (strCancellationArr.Length > 2)
                {
                    for (short i = 1; i < strCancellationArr.Length; i++)
                    {
                        strTerms += @"<li> " + strCancellationArr[i] + " </li>";
                    }
                }
            }
            //doc.LoadXml(xmlResponse); 
        }
        #endregion
    }
}