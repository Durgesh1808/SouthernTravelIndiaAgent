<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmexPGReceipt.aspx.cs" Inherits="SouthernTravelIndiaAgent.AmexPGReceipt" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <input type="hidden" id="type" value="" runat="server" />
    <div class="row" style="font-size: 12px;">
        <asp:Panel ID="pnlResponse" runat="server">
            <table border='0' align='center' width='100%' cellspacing='1' cellpadding='1' style='border-style: solid;
                border-width: 1px; border-color: #BD3410; margin-top: 20px;'>
                <tbody>
                    <tr class='even'>
                        <th colspan='2'>
                            <h1 class='orange24'>
                            Transaction Response
                        </th>
                    </tr>
                    <tr class='even'>
                        <td>
                            Transaction Amount
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_Amount" runat="server" />
                        </td>
                    </tr>
                    <tr class='odd'>
                        <td>
                            MerchTxnRef
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_MerchTxnRef" runat="server" />
                        </td>
                    </tr>
                    <tr class='even'>
                        <td>
                            Transaction Response Code
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_TxnResponseCode" runat="server" />
                        </td>
                    </tr>
                    <tr class='odd'>
                        <td>
                            Transaction Response Code Description
                        </td>
                        <td>
                            <asp:Label ID="Label_TxnResponseCodeDesc" runat="server" />
                        </td>
                    </tr>
                    <tr class='even'>
                        <td>
                            Payment Server Message
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_Message" runat="server" />
                        </td>
                    </tr>
                    <tr class='odd'>
                        <td>
                            Acquirer Response Code
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_AcqResponseCode" runat="server" />
                        </td>
                    </tr>
                    <tr class='even'>
                        <td>
                            Shopping Transaction Number
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_TransactionNo" runat="server" />
                        </td>
                    </tr>
                    <tr class='odd'>
                        <td>
                            Receipt Number
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_ReceiptNo" runat="server" />
                        </td>
                    </tr>
                    <tr class='even'>
                        <td>
                            Authorization ID
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_AuthorizeId" runat="server" />
                        </td>
                    </tr>
                    <tr class='odd'>
                        <td>
                            Batch Number for this transaction
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_BatchNo" runat="server" />
                        </td>
                    </tr>
                    <tr class='even'>
                        <td>
                            Card Type
                        </td>
                        <td>
                            <asp:Label ID="Label_vpc_Card" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlReceiptError" runat="server">
            <table border='0' align='center' width='100%' cellspacing='1' cellpadding='1' style='border-style: solid;
                border-width: 1px; border-color: #BD3410; margin-top: 20px;'>
                <tbody>
                    <tr class='even'>
                        <th colspan='2'>
                            <h1 class='orange24'>
                            Error Information
                        </th>
                    </tr>
                    <tr class='odd'>
                        <td>
                            Error Message
                        </td>
                        <td>
                            <asp:Label ID="lblReceiptErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

