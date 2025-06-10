<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentHotelSearch.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentHotelSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControl/AgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControl/Agentfooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel Search reults</title>
    <link href="../StyleSheets/main.css" rel="stylesheet" />
    <link href="../StyleSheets/style.css" rel="stylesheet" />
    <link href="../StyleSheets/stylesheet.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="chrome://firebug/content/highlighter.css" />

    <script language="javascript" type="text/javascript">
      function changeMySrc(obj) {
    
    var arr1= obj.id.split("_");
    document.getElementById("rptResults_"+arr1[1]+"_TabContainer1_TabPhotos_ImgUrl").src=obj.src;
    
}
    <!-- 
        function bookHotel(hotelNo, roomNo)
        {
           
           
            //alert('hOTEL nO' + hotelNo + ' ROOM NO  ' + roomNo);
            document.getElementById('strHotelId').value =    document.getElementById('strHotelId'+hotelNo).value
            document.getElementById('strRoomId').value =    document.getElementById('Hotel'+ hotelNo +'strRoomId'+roomNo).value
            document.getElementById('strRateplanId').value =   document.getElementById('Hotel'+ hotelNo +'strRateplanId'+roomNo).value
            
            document.getElementById('strExtraGuestCharge').value  =    document.getElementById('Hotel'+ hotelNo +'strExtraGuestCharge'+roomNo).value
            document.getElementById('strTax').value  =    document.getElementById('Hotel'+ hotelNo +'strHotelTax'+roomNo).value
            document.getElementById('strTotal').value  =    document.getElementById('Hotel'+ hotelNo +'strHotelTotalAmount'+roomNo).value
            
            document.getElementById('strHotelName').value  =    document.getElementById('strHotelName'+ hotelNo).value
            document.getElementById('strHotelAddress').value  =    document.getElementById('strHotelAddress'+ hotelNo).value
            document.getElementById('strHotelRating').value  =    document.getElementById('strHotelRating'+ hotelNo ).value
            document.getElementById('strHotelCityCode').value  =    document.getElementById('strHotelCityCode'+ hotelNo ).value
            document.getElementById('strRoomDetails').value  =    document.getElementById('Hotel'+ hotelNo+'strRoomDetails'+ roomNo ).value
            
            
            //alert(document.frmHotels.elements.length);
            //alert(document.getElementById('strHotelId').value);
            //alert(document.getElementById('strRoomId').value);
            //alert(document.getElementById('strRateplanId').value);            
            //frmHotels.action = 'hotelBook.aspx?strHotelId=' + document.getElementById('strHotelId').value + '&strRoomId=' + document.getElementById('strRoomId').value  + '&strRateplanId=' + document.getElementById('strRateplanId').value; 
            document.getElementById('bookHit').value = 'true';
            //alert(document.getElementById('bookHit').value );        
            return true;
        }
        function bookHotelMore(hotelNo, roomNo)
        {
           
            //alert('hOTEL nO' + hotelNo + ' ROOM NO  ' + roomNo);
            document.getElementById('strHotelId').value =    document.getElementById('strHotelId'+hotelNo).value
            document.getElementById('strRoomId').value =    document.getElementById('Hotel'+ hotelNo +'strRoomId'+roomNo).value
            document.getElementById('strRateplanId').value =   document.getElementById('Hotel'+ hotelNo +'strRateplanId'+roomNo).value
            
            document.getElementById('strExtraGuestCharge').value  =    document.getElementById('Hotel'+ hotelNo +'strExtraGuestCharge'+roomNo).value
            document.getElementById('strTax').value  =    document.getElementById('Hotel'+ hotelNo +'strHotelTax'+roomNo).value
            document.getElementById('strTotal').value  =    document.getElementById('Hotel'+ hotelNo +'strHotelTotalAmount'+roomNo).value
            
            document.getElementById('strHotelName').value  =    document.getElementById('strHotelName'+ hotelNo).value
            document.getElementById('strHotelAddress').value  =    document.getElementById('strHotelAddress'+ hotelNo).value
            document.getElementById('strHotelRating').value  =    document.getElementById('strHotelRating'+ hotelNo ).value
            document.getElementById('strHotelCityCode').value  =    document.getElementById('strHotelCityCode'+ hotelNo ).value 
            document.getElementById('strRoomDetails').value  =    document.getElementById('Hotel'+ hotelNo+'strRoomDetails'+ roomNo ).value
            //alert(document.frmHotels.elements.length);
            //alert(document.getElementById('strHotelId').value);
            //alert(document.getElementById('strRoomId').value);
            //alert(document.getElementById('strRateplanId').value);            
            //frmHotels.action = 'hotelBook.aspx?strHotelId=' + document.getElementById('strHotelId').value + '&strRoomId=' + document.getElementById('strRoomId').value  + '&strRateplanId=' + document.getElementById('strRateplanId').value; 
            document.getElementById('bookHit').value = 'true';            
            document.getElementById('hotelMoreInfo').value = 'true';            
            
            document.Form1.submit();
            
        }
    -->
    </script>

</head>
<body topmargin="0" leftmargin="0px">
    <form id="Form1" method="post" runat="server" target="_parent">
    <div> <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" runat="server" value="" id="strHotelId" />
        <input type="hidden" runat="server" value="" id="strRoomId" />
        <input type="hidden" runat="server" value="" id="strRateplanId" />
        <input type="hidden" runat="server" value="" id="strExtraGuestCharge" />
        <input type="hidden" runat="server" value="" id="strTax" />
        <input type="hidden" runat="server" value="" id="strTotal" />
        <input type="hidden" runat="server" value="" id="strHotelName" />
        <input type="hidden" runat="server" value="" id="strHotelAddress" />
        <input type="hidden" runat="server" value="" id="strHotelRating" />
        <input type="hidden" runat="server" value="" id="strHotelCityCode" />
        <input id="bookHit" type="hidden" value="" runat="server" />
        <input id="hotelNo" type="hidden" value="" runat="server" />
        <input id="hotelMoreInfo" type="hidden" value="" runat="server" />
        <input id="strRoomDetails" type="hidden" value="" runat="server" />
        <table cellspacing="0" style="width: 100%" cellpadding="0" border="0" align="center"
            id="desiya">
            <tbody>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                            <tr>
                                <td colspan="2">
                                    <uc1:AgentHeader ID="AgentHeader1" EnableViewState="false" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="tab-content">
                            <table cellspacing="1" cellpadding="1" border="0" class="bg2-left" id="CenterTable" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" width="210" bgcolor="#ffffff" align="left" id="LeftCell">
                                        </td>
                                        <td valign="top" bgcolor="#ffffff" align="right" id="RightCell">

                                            <script>
                                                    var indicator=0;
                                                    function NewWindow(mypage,myname,var1,var2)
                                                      {

                                                       var w = screen.width/2;
                                                       var h = screen.height/2;
                                                       LeftPosition = (screen.width) ? ((screen.width-w)/2+200) : 0;
                                                       TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
                                                       url=mypage+"?hotelName="+var1+"&roomType="+var2
	                                                    window.open(url,'myname','width='+w+',height='+h+',scrollbars=yes,resizable=yes');
                                                      }
                                            </script>

                                            <script language="JavaScript">   
                                                     onload();
                                                     function onload()
                                                     {
                                                      var preloaderObj = document.getElementById('preloader');
                                                      if(preloaderObj!=null){
                                                       preloaderObj.style.display='none';
                                                      }
                                                     }
                                            </script>

                                            <table width="100%" cellspacing="0" cellpadding="0" border="0" align="left">
                                                <tbody>
                                                    <tr>
                                                        <td width="6" valign="top" align="left">
                                                            <img  loading="lazy" alt="board1" width="6" height="5" border="0" src="images/board1.gif" />
                                                        </td>
                                                        <td valign="top" background="images/board_top.gif" align="left" />
                                                        <td>
                                                            <img  loading="lazy" alt="board2" width="5" height="5" border="0" src="images/board2.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" background="images/board_lft.gif" align="left" />
                                                        <td width="98%" valign="top" align="center">
                                                            <br />
                                                            <center>
                                                            </center>
                                                            <table id="Table2" runat="server" width="100%" cellspacing="0" cellpadding="3" border="0"
                                                                bgcolor="#FFFEEC">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left" colspan="3" bgcolor="#FFFEEC">
                                                                            <table width="100%" bgcolor="#FFFEEC" cellpadding="3">
                                                                                <tr>
                                                                                    <td width="100%" colspan="2" align="left" bgcolor="#FFFEEC">
                                                                                        &nbsp;Location:
                                                                                        <asp:Label ID="lblLocation" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        &nbsp; Check In:
                                                                                        <asp:Label ID="lblChkIn" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        &nbsp;Check Out :
                                                                                        <asp:Label ID="lblChkOut" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        &nbsp;Adult :
                                                                                        <asp:Label ID="lblAdult" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        &nbsp; Child :
                                                                                        <asp:Label ID="lblChild" runat="server" Text="" Font-Bold="true"></asp:Label>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table id="tblPaging1" runat="server" width="100%" class="table1-border">
                                                                                <tr>
                                                                                    <td width="33%" align="left">
                                                                                    </td>
                                                                                    <td align="center" class="td">
                                                                                        <asp:Button ID="cmdPrev1" runat="server" Text=" << " OnClick="cmdPrev1_Click" OnClientClick="pagingChk();">
                                                                                        </asp:Button>
                                                                                        <asp:Button ID="cmdNext1" runat="server" Text=" >> " OnClick="cmdNext1_Click" OnClientClick="pagingChk();">
                                                                                        </asp:Button>
                                                                                    </td>
                                                                                    <td width="33%" align="right">
                                                                                        <asp:Label ID="lblCPage1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table cellspacing="0" cellpadding="0" border="0" width="98%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <asp:Repeater runat="server" ID="rptResults" EnableViewState="false" OnItemDataBound="rptResults_ItemDataBound">
                                                                                                <HeaderTemplate>
                                                                                                    <table style="width: 100%;">
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table width="99%" cellspacing="1" border="0" class="table1-border">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td width="65%" class="table1-heading-bg" style="background-image: url('hotel-images/yl-bg.gif');"
                                                                                                                            align="left">
                                                                                                                            <span id="imgRating" runat="server"></span>
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "hotelName") %>
                                                                                                                        </td>
                                                                                                                        <td bgcolor="#ffffff" align="right" width="35%">
                                                                                                                            <font class="hilighted-text">Lowest average rate: </font><font class="special-rate">
                                                                                                                                <%# DataBinder.Eval(Container.DataItem, "CurrencyCode") %>
                                                                                                                                &nbsp;<%# DataBinder.Eval(Container.DataItem, "MinRate") %>
                                                                                                                            </font>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td width="100%" bgcolor="#ffffff" colspan="2">
                                                                                                                            <div align="center">
                                                                                                                                <center>
                                                                                                                                    <table width="100%" cellspacing="0" border="0">
                                                                                                                                        <tbody>
                                                                                                                                            <tr>
                                                                                                                                                <td width="10%" height="22" align="left" valign="top" bgcolor="#FFFFFF">
                                                                                                                                                    <img  loading="lazy" alt="images" src="<%# DataBinder.Eval(Container.DataItem, "Image") %>" width="90" height="90" />
                                                                                                                                                </td>
                                                                                                                                                <td width="90%" align="left">
                                                                                                                                                    <b>
                                                                                                                                                        <%# DataBinder.Eval(Container.DataItem, "hotelCityCode") %></b>, Area:
                                                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "Address") %><br />
                                                                                                                                                    <br />
                                                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                                                                                                                                    <br />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "hotelCode") %>"
                                                                                                                                                        id="strHotelId<%#Container.ItemIndex+1%>" />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "hotelCode") %>"
                                                                                                                                                        id="link<%#Container.ItemIndex+1%>" />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "HotelName") %>"
                                                                                                                                                        id="strHotelName<%#Container.ItemIndex+1%>" />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "Address") %>"
                                                                                                                                                        id="strHotelAddress<%#Container.ItemIndex+1%>" />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "HotelRating") %>"
                                                                                                                                                        id="strHotelRating<%#Container.ItemIndex+1%>" />
                                                                                                                                                    <input type="hidden" value="<%#DataBinder.Eval(Container.DataItem, "hotelCityCode") %>"
                                                                                                                                                        id="strHotelCityCode<%#Container.ItemIndex+1%>" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="2">
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="2">
                                                                                                                                                    <div align="center">
                                                                                                                                                        <center>
                                                                                                                                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                                                                                                                <tbody>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td width="10%" align="left">
                                                                                                                                                                            Availability request :
                                                                                                                                                                        </td>
                                                                                                                                                                        <td align="left">
                                                                                                                                                                            <b>
                                                                                                                                                                                <%=paxDetailsArr.GetLength(0)%>
                                                                                                                                                                                Room<%if (paxDetailsArr.GetLength(0) > 1) { Response.Write("s"); } %>
                                                                                                                                                                            </b>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                            </table>
                                                                                                                                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td align="left" width="17%">
                                                                                                                                                                        <font class="hilighted-text">Special Rates applicable for stay</font>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <font class="hilighted-text"><b>
                                                                                                                                                                            <%=startDate.ToString("ddd, dd MMM, yyyy") %>
                                                                                                                                                                            to
                                                                                                                                                                            <%=endDate.ToString("ddd, dd MMM, yyyy")%></b> </font>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </center>
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="3">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <!-------------------------------------------------------------------------------------------- More Info Details------------------------------------------------------------------------------------------ -->
                                                                                                                                                <td width="100%" colspan="3" align="left" style="background-color: #FFFEEC;">
                                                                                                                                                    <ajaxtoolkit:tabcontainer id="TabContainer1" runat="server" activetabindex="0" cssclass="ajax__tab_technorati-theme"
                                                                                                                                                        width="100%">
                                                                                                                                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Detailed Rates">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <table width="100%" cellspacing="1" cellpadding="0">
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <div align="center" id="ratesTable" runat="server">
                                                                                                                                                            
                                                                                                                                                            </div>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </ajaxToolkit:TabPanel>
                                                                                                                                        <ajaxToolkit:TabPanel ID="tbP" runat="server" HeaderText="Room Description">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <table width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td id="td2" runat="server" align="left" valign="top" bgcolor="#FFFEEC" >
                                                                                                                                                            <asp:Literal ID="ltRoomDesc" runat="server"></asp:Literal>
                                                                                                                                                            <%-- <%=strRooms %>--%>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </ajaxToolkit:TabPanel>
                                                                                                                                        <ajaxToolkit:TabPanel ID="tbPnl2" runat="server" HeaderText="Room Facilities">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <table width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td id="td1" runat="server" align="left" valign="top" bgcolor="#FFFEEC">
                                                                                                                                                            <asp:Literal ID="listrFac" runat="server"></asp:Literal>
                                                                                                                                                            <%--<%=strFac%>--%>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </ajaxToolkit:TabPanel>
                                                                                                                                        <ajaxToolkit:TabPanel ID="TabIaA" runat="server" HeaderText="In and Around">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <table width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td id="td3" runat="server" align="left" valign="top" bgcolor="#FFFEEC" >
                                                                                                                                                            <asp:Literal ID="listrInaA" runat="server"></asp:Literal>
                                                                                                                                                            <%--<%=strInaA%>--%>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </ajaxToolkit:TabPanel>
                                                                                                                                        <ajaxToolkit:TabPanel ID="TabPhotos" runat="server" HeaderText="Photos">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <table width="100%" border="0">
                                                                                                                                                    <tr>
                                                                                                                                                        <td id="td4" runat="server" align="left" valign="top" bgcolor="#FFFFFF" width="17%">
                                                                                                                                                            <div style="overflow: auto; height: 330px;">
                                                                                                                                                                <asp:DataList ID="dlGallery" runat="server" RepeatColumns="2">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <table width="100%" cellspacing="1px" cellpadding="1px">
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td align="center" valign="top" bgcolor="#FFFFFF">
                                                                                                                                                                                    <asp:Image runat="server" ID="RoomSImg" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Imageurl") %>'
                                                                                                                                                                                        onclick='changeMySrc(this)' Width="65px" Height="65px" Style="cursor: pointer;" />
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                        </table>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:DataList></div>
                                                                                                                                                        </td>
                                                                                                                                                        <td valign="top" align="center" width="83%" style="background-color:#FFFEEC">
                                                                                                                                                            <asp:Image runat="server" ID="ImgUrl" />
                                                                                                                                                            <%--<img  loading="lazy" alt=""  width="330px" height="330px" id="ImgUrl_TabPhotos" />--%>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </ajaxToolkit:TabPanel>
                                                                                                                                    </ajaxtoolkit:tabcontainer>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </tbody>
                                                                                                                                    </table>
                                                                                                                                </center>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    </table>
                                                                                                </FooterTemplate>
                                                                                            </asp:Repeater>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table id="tblPaging" runat="server" width="98%" cellspacing="0" cellpadding="3"
                                                                border="0" class="table1-border">
                                                                <tbody>
                                                                    <tr>
                                                                        <td width="33%" align="left">
                                                                        </td>
                                                                        <td align="center" class="td">
                                                                            <asp:Button ID="cmdPrev" runat="server" Text=" << " OnClick="cmdPrev_Click"></asp:Button>
                                                                            <asp:Button ID="cmdNext" runat="server" Text=" >> " OnClick="cmdNext_Click"></asp:Button>
                                                                        </td>
                                                                        <td width="33%" align="right">
                                                                            <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <div>
                                                                <asp:Label ID="lblErrMsg" CssClass="cgi" runat="server"></asp:Label>
                                                            </div>
                                                            <div align="center">
                                                                <center>
                                                                    <table width="523" cellspacing="0" cellpadding="0" border="0" bgcolor="#bbd1ff">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td width="100%" bgcolor="#ffffff" class="tab-content" colspan="2">
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </center>
                                                            </div>
                                                            <br />
                                                        </td>
                                                        <td height="113" background="images/board_rt.gif">
                                                            <img  loading="lazy" alt="board_rt" width="5" height="1" border="0" src="Assets/images/board_rt.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="6" height="5">
                                                            <img  loading="lazy" alt="board3" width="6" height="5" border="0" src="images/board3.gif" />
                                                        </td>
                                                        <td width="6" height="5" background="images/board_bot.gif" />
                                                        <td height="5">
                                                            <img  loading="lazy" alt="board4" width="5" height="5" border="0" src="images/board4.gif" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table align="center">
            <tr>
                <td>
                    <uc1:AgentFooter ID="BranchFooter1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
