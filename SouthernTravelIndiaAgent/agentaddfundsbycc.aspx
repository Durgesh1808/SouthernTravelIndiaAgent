<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentaddfundsbycc.aspx.cs" Inherits="SouthernTravelIndiaAgent.agentaddfundsbycc" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc2" TagName="agentRightControl" Src="UserControls/UcAgentRightUsc.ascx" %>
<%@ Register TagPrefix="uc2" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Deposit Funds</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript" src="../Assets/js/calendar.js"></script>
    <link href="../Assets/css/Calenderall.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/calenderall.js" type="text/javascript"></script>
    <style type="text/css">
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 125%;
            background-color: Gray;
            z-index: 1001;
            -moz-opacity: 0.8;
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 25%;
            left: 10%;
            width: 80%;
            height: 65%;
            padding: 16px;
            border: 1px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        .style2
        {
            width: 351px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function chkSelected() {
            alert("hello");

        }

        function chkNumeric() {

            if (event.shiftKey) return false;
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false;
        }

        function Cashvalidation() {
            if (document.form1.txtAmount.value == "") {
                alert('please Select the amount');
                document.form1.txtAmount.focus();
                return false;
            }


            if (document.form1.txtDepositBy.value == "") {
                alert('please Enter the Card Holder Name');
                document.form1.txtDepositBy.focus();
                return false;
            }

            if (document.getElementById("rdoNetBanking").checked == false && document.getElementById("rdoCC").checked == false && document.getElementById("rdoamex").checked == false
       && document.getElementById("rdoCD").checked == false && document.getElementById("rbtnPayu").checked == false && document.getElementById("rbtnAtom").checked == false
       && document.getElementById("rbtnInstamojo").checked == false) {
                alert("please choose the payment option");
                return false;
            }
            else if (document.getElementById("rdoNetBanking").checked == true && document.getElementById("rdoCC").checked == false) {
                //            if(document.getElementById("ddlpaymentoption").value=="0")
                //            {
                //                alert("please choose the Bank Name");
                //                return false;                
                //            }
            }

            if (document.form1.chkTrue.checked == false) {
                alert("Before submit you should agree with terms and conditions");
                return false;
            }

            document.getElementById('Submit').style.display = 'none';
            return true;



        }
        function others() {
            document.getElementById("other").style.display = "none";
            if (document.getElementById("rdoNetBanking").checked == true) {
                document.getElementById("other").style.display = "none"
            }
            else {
                document.getElementById("other").style.display = "none";
            }
        }

        function Existing() {
            if (document.getElementById("rdoCC").checked == true) {
                document.getElementById("other").style.display = "none"
            }

        }

        function MakeReadOnly() {
            document.getElementById('txtDepositDate').readOnly = true;

        }
    
    </script>
</head>
<body onload="MakeReadOnly();">
    <form id="form1" runat="server">
    <div>
        <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:agentheader ID="agHeader" runat="server" />
                </td>
            </tr>
        </table>
        <!--MIDDLE -->
        <table width="982" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" valign="top">
                    <table width="680" height="449" border="0" cellpadding="0" cellspacing="0">
                        <tr height="8">
                            <td align="left" background="Assets/images/left_corner.gif" width="8" height="8">
                            </td>
                            <td bgcolor="#E7E7E7" background="Assets/images/trans.gif" width="1" height="1">
                            </td>
                            <td align="right" background="Assets/images/rgt_corner.gif" width="8" height="8">
                            </td>
                        </tr>
                        <tr height="433">
                            <td align="left" bgcolor="#E7E7E7">
                                &nbsp;
                            </td>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <table width="370" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="13">
                                                        <img  loading="lazy" alt="left_" src="Assets/images/left_.gif" width="13" height="42" />
                                                    </td>
                                                    <td align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                                        Deposit Funds by Credit Card/ Net Banking
                                                    </td>
                                                    <td width="13">
                                                        <img  loading="lazy" alt="right_" src="Assets/images/right_.gif" width="13" height="42" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" class="verdana10r" valign="middle">
                                            <font color="red" size="2">Current Balance:&nbsp;<asp:Label ID="lblBalance" runat="server"
                                                Text="Balance"></asp:Label></font>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <table width="660" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="38" align="left" background="Assets/images/bg1.gif" class="verdana11bk">
                                                        <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="33" height="1" />
                                                    </td>
                                                    <td align="left" background="Assets/images/bg1.gif" class="cgi1">
                                                        Deposit Amount (INR) &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="verdana11bk" MaxLength="6" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="left" background="Assets/images/bg1.gif" class="verdana11bk">
                                                        <asp:RadioButton ID="rdoCash" runat="server" GroupName="rdoDeposit" Checked="true" />
                                                    </td>
                                                    <td align="left" background="Assets/images/bg1.gif" class="cgi1">
                                                        Credit Card / Net Banking
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="98%" border="0" cellspacing="0" cellpadding="1">
                                                <tr id="trShow" runat="server">
                                                    <td bgcolor="#D6F0FE">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                                            <tr bgcolor="#FFFFFF" class="bg">
                                                                <td colspan="3" align="left" class="verdana11bk">
                                                                    Dear Sir,<br />
                                                                    <br />
                                                                    We have deposited the above mentioned amount in your account as per the details
                                                                    given below.
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr class="bg">
                                                                <td align="left" class="verdana11bk">
                                                                    Card Holder's Name :
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDepositBy" runat="server" MaxLength="20" CssClass="verdana11bk"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr class="bg">
                                                                <td align="left" class="verdana11bk">
                                                                    Deposit Date :
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDepositDate" runat="server" Width="70" CssClass="verdana11bk"
                                                                        onclick="showCalendarControl(this);"> </asp:TextBox>
                                                                    <img  loading="lazy" id="Img2" style="cursor: hand" alt="View calendar" src="Assets/images/calendar.gif"
                                                                        onclick="showCalendarControl(document.getElementById('txtDepositDate'));">
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr class="bg">
                                                                <td colspan="3">
                                                                    <table cellpadding="1" cellspacing="1" border="0" width="100%" bgcolor="#FFFFFF"
                                                                        class="bg">
                                                                        <tr class="bg">
                                                                            <td class="verdana11bk" style="height: 0px" align="left" colspan="2">
                                                                                <b>Payment : </b>
                                                                                <br />
                                                                                <asp:RadioButton CssClass="RB2" ID="rbtnInstamojo" runat="server" Text="" GroupName="rdoPayment" />
                                                                                <span>Credit Card/Debit Card/Net Banking/UPI/Wallets- Powered by
                                                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Assets/images/instamojo-logo.png" Height="35px">
                                                                                    </asp:Image>
                                                                                </span>
                                                                                <br />
                                                                                <br />
                                                                                <asp:RadioButton ID="rbtnPayu" runat="server" Text="" GroupName="rdoPayment" CssClass="RB2" />
                                                                                <span>Credit Card/Debit Card/Net Banking/UPI/EMI/Wallets- Powered by<asp:Image ID="Image6"
                                                                                    runat="server" ImageUrl="~/Assets/images/payu.jpg" Height="35px"></asp:Image>
                                                                                </span>&nbsp;
                                                                                <br />
                                                                                <br />
                                                                                <div style="display: none">
                                                                                    <asp:RadioButton ID="rbtnAtom" runat="server" GroupName="rdoPayment" CssClass="RB2" />
                                                                                    <%--   <span style="color: #df411a !important;">( Credit / Debit Card, Net Banking)</span>--%>
                                                                                    <!-- Icon and text change section -->
                                                                                    <span>Credit / Debit Card / Net Banking- Powered by
                                                                                        <asp:Image ID="imgPaymentLogoAtom" runat="server" ImageUrl="~/Assets/images/atompayment.png"
                                                                                            Height="35px"></asp:Image>
                                                                                    </span>
                                                                                </div>
                                                                                <!-- End -->
                                                                                
                                                                                <div style="display: none">
                                                                                <br />
                                                                                <br />
                                                                                <asp:RadioButton ID="rdoCD" runat="server" GroupName="rdoPayment" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'" />
                                                                                <!-- Icon and text change section -->
                                                                                <span>Debit Card- Powered by
                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/hdfcpayment.png" Height="35px">
                                                                                    </asp:Image>
                                                                                </span>
                                                                                <!-- End -->
                                                                                &nbsp;
                                                                                <br />
                                                                                <asp:RadioButton ID="rdoCC" runat="server" GroupName="rdoPayment" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'" />
                                                                                <!-- Icon and text change section -->
                                                                                <span>Credit Card- Powered by
                                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/images/hdfcpayment.png" Height="35px">
                                                                                    </asp:Image>
                                                                                </span>
                                                                                <!-- End -->
                                                                                </div>
                                                                                <br />
                                                                                <asp:RadioButton ID="rdoNetBanking" runat="server" GroupName="rdoPayment" />
                                                                                <%--  <span style="color: #df411a !important;">( Credit / Debit Card, Net Banking)</span>--%>
                                                                                <!-- Icon and text change section -->
                                                                                <span>Credit / Debit Card / Net Banking- Powered by
                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/images/techprocesspayment.png"
                                                                                        Height="40px"></asp:Image>
                                                                                </span>
                                                                                <!-- End -->
                                                                                <br />
                                                                                <asp:RadioButton ID="rdoamex" runat="server" GroupName="rdoPayment" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'; enable();" />
                                                                                <!-- Icon and text change section -->
                                                                                <span>Amex Credit Card- Powered by
                                                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Assets/images/amexpayment.png" Height="35px">
                                                                                    </asp:Image>
                                                                                </span>
                                                                                <!-- End -->
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="#FFFFFF">
                                                                            <td align="center">
                                                                                <br />
                                                                                <asp:CheckBox ID="chkTrue" runat="server" />
                                                                                I agree to the <a href="#" onclick="javascript:approve();"><b>terms & conditions.</b></a>
                                                                                <br />
                                                                                <br />
                                                                            </td>
                                                                            <td height="144px" align="center">
                                                                                <img  loading="lazy" alt="Diners_Club_and_Discover"  src="../Assets/images/Diners_Club_and_Discover.bmp" height="144px" width="288px"
                                                                                    border="0" /><br />
                                                                                <span class="heads"><b>NOTE :&nbsp;</b></span><span class="hlinks">Diners card are accepted.</span>
                                                                                <%--<img  loading="lazy"     src="../Assets/images/4x2.jpg" usemap="#planetmap" border="0" />
                                                                                <map name="planetmap" id="planetmap">
                                                                                    <area shape="rect" coords="184,121,286,144" href="http://www.hdfcbanksmartbuy.com/content.aspx?pgid=35711" />
                                                                                </map>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="hlinks">
                                                                            <td align="center" width="100%" style="height: 20px" colspan="2">
                                                                                <asp:Button ID="Submit" Text="Transfer Funds" runat="Server" BackColor="#348de7"
                                                                                    BorderStyle="None" CssClass="cgi1" OnClick="Submit_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" bgcolor="#E7E7E7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr height="8">
                            <td align="left" background="Assets/images/left_d_corner.gif" width="8" height="8">
                            </td>
                            <td bgcolor="#E7E7E7" background="Assets/images/trans.gif" width="1" height="1">
                            </td>
                            <td align="right" background="Assets/images/rgt_d_corner.gif" width="8" height="8">
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="296" align="right" valign="top">
                    <table width="296" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <uc2:agentRightControl ID="agentRight" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="22" colspan="3" align="center" class="verdana10">
                    <uc2:agentFooter ID="AgentFooter1" runat="server" />
                </td>
            </tr>
        </table>
        <!--MIDDLE END-->
    </div>
    <div id="light" class="white_content">
        <%--<div align="right" class="verdana11bkb"><a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
            Close</a></div>--%>
        <div style="width: 100%;" class="resultsPane2">
            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="verdana11bkb">
                <tr>
                    <td height="30">
                        <input id="Radio1" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                            value="280" />
                        <img  loading="lazy"src="../Assets/images/allahabadbank.jpg" alt="Allahabad Bank" border="0" style="vertical-align: top" />
                        Allahabad Bank
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle;">
                            <input id="Radio2" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="50" />
                            <img  loading="lazy" src="../Assets/images/axis_bank.gif" alt="AXIS Bank" border="0" style="vertical-align: top" />
                            AXIS Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb19" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="340" />
                            <img  loading="lazy"  src="../Assets/images/Bank-of-Bahrain-and-Kuwait.jpg" alt="Bank of Bahrain and Kuwait"
                                border="0" style="vertical-align: middle" />
                            Bank of Bahrain and Kuwait</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy" src="../Assets/images/blank.gif" height="4" alt="blank" /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle;">
                            <input id="Radio3" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="310" />
                            <img  loading="lazy" src="../Assets/images/bob.jpg" alt="Bank of Baroda" border="0" style="vertical-align: middle" />
                            Bank of Baroda</div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio4" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="240" />
                            <img  loading="lazy"  src="../Assets/images/bank_ofindia.gif" alt="Bank of India" border="0" style="vertical-align: top" />
                            Bank of India
                        </div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio11" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="750" />
                            <img  loading="lazy" src="../Assets/images/Bank-of-Maharashtra.jpg" alt="Bank of Maharashtra" width="70px"
                                height="24px" border="0" style="vertical-align: top" />
                            Bank of Maharashtra
                        </div>
                    </td>
                    <%--<td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio5" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="170" />
                            <img  loading="lazy" alt="" src="../Assets/images/bank_of_rajasthan.gif" alt=" Bank Of Rajesthan" border="0" style="vertical-align: middle" />
                            Bank Of Rajasthan
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio6" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="120" />
                            <img  loading="lazy" alt="" src="../Assets/images/corp_bank.gif" alt="Corporation Bank" border="0" style="vertical-align: middle" />
                            Corporation Bank</div>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy" src="../Assets/images/blank.gif" height="4" alt="blank" /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio5" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="320" />
                            <img  loading="lazy"  src="../Assets/images/beam-cash-card.jpg" alt="Beam Cash Card" border="0" style="vertical-align: middle" />
                            Beam Cash Card
                        </div>
                        <%----%>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio6" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="740" />
                            <img  loading="lazy" src="../Assets/images/Central-Bank-Of-India.jpg" alt="Central Bank of India" border="0"
                                style="vertical-align: middle" />
                            Central Bank of India
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb24" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="230" />
                            <img  loading="lazy" src="../Assets/images/Citi-Bank.jpg" alt="Citi Bank" border="0" style="vertical-align: middle" />
                            Citi Bank</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio12" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="440" />
                            <img  loading="lazy" src="../Assets/images/City_Union_Bank.jpg" alt="City Union Bank" border="0" style="vertical-align: middle" />
                            City Union Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio25" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="930" />
                            <img  loading="lazy" src="../Assets/images/Canarabank_Logo.gif" alt="Canara Bank" border="0" style="vertical-align: middle"
                                height="34" width="70" />
                            Canara Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio5" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="1130" />
                            <img  loading="lazy" "src="../Assets/images/csb.jpg" alt="Catholic Syrian Bank" border="0" style="vertical-align: middle"
                                height="34" width="70" />
                            Catholic Syrian Bank</div>
                    </td>
                    <%--<td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio5" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="540" />
                            <img  loading="lazy" alt="" src="../Assets/images/DCBlogo.jpg" alt="Deutsche Bank" border="0" style="vertical-align: middle" height="34" width="70"/>
                            DCB Bank</div>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy" alt="blank" src="../Assets/images/blank.gif" height="4" /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb17" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="330" />
                            <img  loading="lazy" src="../Assets/images/Deutsche-Bank.jpg" alt="Deutsche Bank" border="0" style="vertical-align: middle" />
                            Deutsche Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio13" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="540" />
                            <img  loading="lazy"  src="../Assets/images/Development_Credit_Bank.jpg" alt="Development Credit Bank" border="0"
                                style="vertical-align: middle" />
                            Development Credit Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb18" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="370" />
                            <img  loading="lazy"   src="../Assets/images/Dhanlaxmi-Bank.jpg" alt="Dhanlaxmi Bank" border="0" style="vertical-align: middle" />
                            Dhanlaxmi Bank</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio7" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="270" />
                            <img  loading="lazy"   src="../Assets/images/fbllogo.jpg" alt="Federal Bank" border="0" style="vertical-align: middle" />
                            Federal Bank
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio8" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="300" />
                            <img  loading="lazy"   src="../Assets/images/hdfcbank.jpg" alt="Hdfc Net Banking" border="0" style="vertical-align: middle" />
                            Hdfc Net Banking</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio14" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="460" checked="CHECKED" />
                            <img  loading="lazy"   src="../Assets/images/ICashCard.jpg" alt="I-Cash Card" border="0" style="vertical-align: middle" />
                            I-Cash Card</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio9" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="10" checked="CHECKED" />
                            <img  loading="lazy"   src="../Assets/images/icici_bank.gif" alt="ICICI Bank" border="0" style="vertical-align: middle" />
                            ICICI Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb25" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="520" />
                            <img  loading="lazy"   src="../Assets/images/IDBI-Bank.jpg" alt="IDBI Bank" border="0" style="vertical-align: middle" />
                            IDBI Bank
                        </div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio15" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="490" />
                            <img  loading="lazy"   src="../Assets/images/Indian_Bank.jpg" alt="Indian Bank" border="0" style="vertical-align: middle" />
                            Indian Bank</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio16" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="420" />
                            <img  loading="lazy"   src="../Assets/images/Indian-Overseas-Bank.jpg" alt="Indian Overseas Bank" border="0"
                                style="vertical-align: middle" />
                            Indian Overseas Bank</div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio17" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="830" />
                            <img  loading="lazy"   src="../Assets/images/ING-Vysya.jpg" alt="ING Vysya Bank" border="0" style="vertical-align: middle" />
                            ING Vysya Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio10" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="350" />
                            <img  loading="lazy"   src="../Assets/images/J-and-K-Bank.jpg" alt="J&amp;K Bank" border="0" style="vertical-align: middle" />
                            J&amp;K Bank</div>
                    </td>
                    <%--<td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio10" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="250" />
                            <img  loading="lazy"   src="../Assets/images/itz-cash.jpg" alt="Itz Cash Card" border="0" style="vertical-align: middle" />
                            Itz Cash Card</div>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio24" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="910" />
                            <img  loading="lazy"   src="../Assets/images/Kotak_Logo.gif" alt="Kotak Mahindra bank" border="0" style="vertical-align: middle"
                                height="34" width="70" />
                            Kotak Mahindra bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb20" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="140" />
                            <img  loading="lazy"   src="../Assets/images/Karnataka-Bank.jpg" alt="Karnataka Bank" border="0" style="vertical-align: middle" />
                            Karnataka Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio18" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="760" />
                            <img  loading="lazy"   src="../Assets/images/Karur_Vysya_Bank.jpg" alt="Karur Vysya Bank" border="0" style="vertical-align: middle" />
                            Karur Vysya Bank</div>
                    </td>
                    <%--<td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb13" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="220" />
                            <img  loading="lazy"   src="../Assets/images/oxi-cash.jpg" alt="Oxicash" border="0" style="vertical-align: middle" />
                            Oxicash</div>
                    </td>--%>
                    <%--<td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb22" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="420" />
                            <img  loading="lazy"   src="../Assets/images/Indian-Overseas-Bank.jpg" alt="Indian Overseas Bank" border="0" style="vertical-align: middle" />
                            Indian Overseas Bank</div>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb12" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="160" />
                            <img  loading="lazy"   src="../Assets/images/orintal_bank.gif" alt="Oriental Bank Of Commerce" border="0"
                                style="vertical-align: middle" />Oriental Bank Of Commerce</div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio28" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="1220" />
                            <img  loading="lazy"   src="../Assets/images/PNB.JPG" alt="Punjab National Bank" border="0" style="vertical-align: middle"
                                height="24" width="70" />
                            Punjab National Bank
                        </div>
                    </td>
                    <td height="30">
                        <div align="left" style="vertical-align: middle">
                            <input id="rb14" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="180" />
                            <img  loading="lazy"   src="../Assets/images/south_indian_bank.gif" alt="South Indian Bank" border="0" style="vertical-align: middle" />
                            South Indian Bank
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb21" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="450" />
                            <img  loading="lazy"   src="../Assets/images/Standard-Chartered-Bank.jpg" alt="Standard Chartered Bank" border="0"
                                style="vertical-align: middle" />
                            Standard Chartered Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio19" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="560" />
                            <img  loading="lazy"   src="../Assets/images/State_Bank_of_Hyderabad.jpg" alt="State Bank of Hyderabad" border="0"
                                style="vertical-align: middle" />
                            State Bank of Hyderabad
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb26" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="530" />
                            <img  loading="lazy"   src="../Assets/images/sbi_logo_main.gif" alt="State Bank of India" border="0" style="vertical-align: middle" />
                            State Bank of India
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio26" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="950" />
                            <img  loading="lazy"   src="../Assets/images/State Bank of Bikaner.jpg" alt="State Bank of Mysore" border="0"
                                style="vertical-align: middle" width="70" width="24" />
                            State Bank Of Bikaner and Jaipur
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio27" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="880" />
                            <img  loading="lazy"   src="../Assets/images/State Bank of patiala.jpg" alt="State Bank of Mysore" border="0"
                                style="vertical-align: middle" width="70" width="24" />
                            State Bank of Patiala
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio22" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="680" />
                            <img  loading="lazy"   src="../Assets/images/State-Bank-of-Travencore.png" alt="State Bank of Travencore"
                                width="70px" height="24px" border="0" style="vertical-align: middle" />
                            State Bank of Travencore</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio20" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="550" />
                            <img  loading="lazy"   src="../Assets/images/State_Bank_of_Mysore.jpg" alt="State Bank of Mysore" border="0"
                                style="vertical-align: middle" />
                            State Bank of Mysore
                        </div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio21" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="620" />
                            <img  loading="lazy"   src="../Assets/images/Tamilnad_Mercantile_Bank.jpg" alt="Tamilnad Mercantile Bank"
                                border="0" style="vertical-align: middle" />
                            Tamilnad Mercantile Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb23" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="190" />
                            <img  loading="lazy"   src="../Assets/images/Union-Bank-of-India.jpg" alt="Union Bank of India" border="0"
                                style="vertical-align: middle" />
                            Union Bank of India</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                            <img  loading="lazy"   src="../Assets/images/blank.gif" height="4"   /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="rb15" type="radio" onclick="changeBank(this.value);" name="rbPayOpt" value="570" />
                            <img  loading="lazy"   src="../Assets/images/United-Bank-of-India.jpg" alt="United Bank of India" border="0"
                                style="vertical-align: middle" />
                            United Bank of India</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio23" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="200" />
                            <img  loading="lazy"   src="../Assets/images/vijayabank.gif" alt="Vijaya bank" border="0" style="vertical-align: middle" />
                            Vijaya Bank</div>
                    </td>
                    <td>
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio30" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="820" />
                            <img  loading="lazy"   src="../Assets/images/sbi_logo_main.gif" alt="Visa Master Maestro Credit Card Gateway"
                                border="0" style="vertical-align: middle" />
                            Visa Master Maestro Credit Card Gateway</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div align="left" style="vertical-align: middle">
                            <input id="Radio29" type="radio" onclick="changeBank(this.value);" name="rbPayOpt"
                                value="1180" />
                            <img  loading="lazy"   src="../Assets/images/sbi_logo_main.gif" alt="Visa Master Maestro Debit Card Gateway"
                                border="0" style="vertical-align: middle" />
                            Visa Master Maestro Debit Card Gateway</div>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="vertical-align: middle" class="verdana11bkb">
            <br />
            <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'; enable();">
                <img  loading="lazy"   src="../Assets/images/OK.jpg" style="border: 0" alt="Select Bank" />
            </a>
        </div>
    </div>
    <div id="fade" class="black_overlay">
    </div>
    <input id="CSTBANKID" type="hidden" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        function changeBank(t) {
            document.getElementById('CSTBANKID').value = t;
        }
        x = 20;
        y = 70;
        function placeIt(obj) {
            obj = document.getElementById(obj);
            if (document.documentElement) {
                theLeft = document.documentElement.scrollLeft;
                theTop = document.documentElement.scrollTop;
            }
            else if (document.body) {
                theLeft = document.body.scrollLeft;
                theTop = document.body.scrollTop;
            }
            theLeft += x;
            theTop += y;
            obj.style.left = theLeft + 'px';
            obj.style.top = theTop + 'px';
            setTimeout("placeIt('light')", 500);
        }                              
    </script>
</body>
</html>
