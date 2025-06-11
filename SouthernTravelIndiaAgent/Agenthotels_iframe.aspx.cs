using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Caching;

namespace SouthernTravelIndiaAgent
{
    public partial class Agenthotels_iframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                //Response.Redirect("sessionexpire.aspx");
                Response.Redirect("agentlogin.aspx");
            }
            if (!IsPostBack)
            {
                SetDates();
            }
            if (Cache["dtHotelResults" + Session.SessionID] != null)
                Cache.Remove("dtHotelResults" + Session.SessionID);

        }
        private void SetDates()
        {
            // Set default check-in and check-out dates to tomorrow and the day after tomorrow
            DateTime dtStart = DateTime.Now.AddDays(1);
            DateTime dtEnd = dtStart.AddDays(1);
            int dayPart = dtStart.Day;
            int monthPart = dtStart.Month;
            int yearPart = dtStart.Year;


            // Set the dropdowns for check-in and check-out dates
            strCheckindate.Items.FindByValue(dayPart.ToString("00")).Selected = true;
            strCheckinmonth.Items.FindByValue(monthPart.ToString("00")).Selected = true;
            strCheckinyear.Items.FindByValue(yearPart.ToString()).Selected = true;

            dayPart = dtEnd.Day;
            monthPart = dtEnd.Month;
            yearPart = dtEnd.Year;

            strCheckoutdate.Items.FindByValue(dayPart.ToString("00")).Selected = true;
            strCheckoutmonth.Items.FindByValue(monthPart.ToString("00")).Selected = true;
            strCheckoutyear.Items.FindByValue(yearPart.ToString()).Selected = true;


        }
        protected void btnHhotelSearch_ServerClick(object sender, EventArgs e)
        {

            //Response.Write("city value" + hidCity.Value);
            string xmlString = BuildXml();
            Response.Redirect("Agenthotelsearch.aspx", true);
            //Page.RegisterStartupScript("ChangePage", "<script language='javascript'>parent.window.location='hotelsearch.aspx'</script>");
            //ClientScript.RegisterStartupScript(GetType(),"ChangePage", "<script language='javascript'>window.open('hotelsearch.aspx');</script>");


        }
        short[,] paxDetailsArr;
        short[,] paxChildAgeArr;
        string BuildXml()
        {
            string XmlString = "";
            string strCity = hidCity.Value;
            Session["strCity"] = strCity;
            Session["CheckIn"] = strCheckoutmonth.Value + "/" + strCheckindate.Value + "/" + strCheckinyear.Value;
            Session["CheckOut"] = strCheckoutmonth.Value + "/" + strCheckindate.Value + "/" + strCheckinyear.Value;

            int roomCount = int.Parse(strRooms.Value);
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
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
                                    <StayDateRange Start='" + strCheckinyear.Value + "-" + strCheckoutmonth.Value + "-" + strCheckindate.Value + @"T01:00:00' End='" + strCheckoutyear.Value + "-" + strCheckoutmonth.Value + "-" + strCheckoutdate.Value + @"T23:59:59'/>
                                        <RoomStayCandidates>";

            string PaxDetails = "";
            paxDetailsArr = new short[roomCount, 2];
            paxChildAgeArr = new short[roomCount, 4];
            for (short a = 1; a <= roomCount; a++)
            {
                string GuestCount = ((HtmlSelect)(this.FindControl("strAdultsRoom" + a))).Value;
                short ChildCount = short.Parse(((HtmlSelect)(this.FindControl("strChildrenRoom" + a))).Value);
                paxDetailsArr[a - 1, 0] = short.Parse(GuestCount);
                paxDetailsArr[a - 1, 1] = ChildCount;
                string childString = "<ChildCounts>";
                if (ChildCount > 0)
                {
                    for (short c = 1; c <= ChildCount; c++)
                    {
                        string childAge = ((HtmlSelect)(this.FindControl("strAgeChild" + c + "Room" + a))).Value;
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

            int totalAdult = 0, totalChild = 0;
            for (short k = 0; k < paxDetailsArr.GetLength(0); k++)
            {
                totalAdult += paxDetailsArr[k, 0];
                totalChild += paxDetailsArr[k, 1];
            }

            Session["TotalAdult"] = totalAdult;
            Session["TotalChild"] = totalChild;

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

            if (Cache["xmlStr" + Session.SessionID] != null)
                Cache.Remove("xmlStr" + Session.SessionID);

            Cache.Insert("xmlStr" + Session.SessionID, XmlString, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

            //Session["XmlString"] = XmlString;
            return "";

            //Response.Write("<br>Sending Request<br><br><font size=\"3\" face=\"arial\"  color=\"red\"><b>" + Server.HtmlEncode(xmlString) + "</b></font><br>");
            //string uri = ConfigurationManager.AppSettings["DesiyaWebServiceURL"].ToString();
            //string s = DataLib.HttpSOAPRequest(xmlString, uri, null);        
            //Response.Write("<br>Response from Desiya <br><br><font size=\"3\" face=\"arial\" color=\"green\" ><b>" + Server.HtmlEncode(s) + "</b></font><br>");
        }
    }
}