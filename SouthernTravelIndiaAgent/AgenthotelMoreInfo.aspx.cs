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
    public partial class AgenthotelMoreInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["AgentId"] == null) && (Session["BranchId"] == null))
            //{
            //    Response.Redirect("sessionexpire.aspx");
            //}
            //else
            //{

            //}
            bindPage();
        }
        protected string strDesc = "", strRooms = "", strFac = "";
        private void bindPage()
        {
            HtmlGenericControl spImages = (HtmlGenericControl)(FindControl("imgRating"));
            string userName = ConfigurationManager.AppSettings["DesiyaUserName"];
            string passWord = ConfigurationManager.AppSettings["DesiyaPassword"];
            string propertyId = ConfigurationManager.AppSettings["DesiyaPropertyID"];
            string hotelCityCode = Session["strHotelCityCode"].ToString();
            string hotelCode = Session["hotelId"].ToString();



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

            foreach (XmlNode eachSubSec in allSubSection)
            {

                if (eachSubSec.Attributes["SubTitle"].Value == "HotelDescription")
                {
                    XmlNodeList allParaGraph = eachSubSec.ChildNodes;
                    foreach (XmlNode singlePara in allParaGraph)
                    {
                        strDesc += ("<p>" + singlePara.FirstChild.InnerText + "</p>");
                    }
                }
                if (eachSubSec.Attributes["SubTitle"].Value == "Rooms")
                {

                    strRooms = @"<table cellpadding=""5"" cellspacing=""5"" width=""100%"">
                                <tr>
                                <td class=""moreinfo-h2"">Room Description
        		                            <hr noshade class=""moreinfo-hr"" size=""1""></td>
    		                            </tr>
                                <tr>
                                <td align=""center"">";
                    XmlNodeList allRooms = eachSubSec.ChildNodes;
                    foreach (XmlNode singleRoom in allRooms)
                    {

                        strRooms += @"<table border=""0"" cellspacing=""1"" width=""99%"" class=""moreinfo-table1-border"">
                                  <tr>
                                      <td width=""300"" class=""moreinfo-table1-heading-bg""><b>" + singleRoom.Attributes["Name"].Value + @"</b>
              	                      </td>
                                  </tr>
                                  <tr>
                                      <td width=""100%"" bgcolor=""#FFFFFF"" colspan=""2"">
                                          <div align=""center"">
                                              <center>
                                                  <table border=""0"" cellspacing=""0"" width=""100%"">
                                                      <tr> 
						             	                    <td width=""115""><img border=""1"" src=""" + singleRoom.FirstChild.NextSibling.InnerText + @""" width=""111"" height=""110""></td>
                    						                
						                                    <td align=""left"" colspan=""2"" width=""420"" valign=""top""><p class=""bigfont"" align=""justify"">" + singleRoom.FirstChild.InnerText + @"</p><br>
						                                    </td>
						                              </tr>
                                                  </table>

                           			 
			                      </td>
                               </tr>
                             </table><img height=""1"" src=""images/dot.gif"">";
                    }

                }


                if (eachSubSec.Attributes["SubTitle"].Value == "Facilities")
                {
                    strFac = @"<table border=""0"" cellspacing=""2"" cellpadding=""2"" border=""0"" width=""100%"">";
                    XmlNodeList allFac = eachSubSec.ChildNodes;

                    foreach (XmlNode _type in allFac)
                    {
                        strFac += @"
                <tr>
            			<td><font size=""2""><b>" + _type.Name + @"</b></font></td>
                </tr>
                <tr>
        	        <td align=""left"">";
                        string innerTable = @"<table border=""0"" cellpadding=""3"" cellspacing=""1"" width=""100%""><tr><td>";
                        XmlNodeList allText = _type.ChildNodes;
                        short i = 1;
                        foreach (XmlNode text in allText)
                        {
                            innerTable += @"<li><font class=""bigfont"">" + text.InnerText + @"</font></li></td><td>";
                            if (i % 3 == 0)
                            {
                                innerTable += @"</td></tr><tr><td>";
                            }
                            i++;
                        }
                        innerTable += @"</td></tr></table>";
                        strFac += (innerTable + @"</td></tr>");
                    }

                }
                strFac += @"</table>";
            }

        }
    }
}