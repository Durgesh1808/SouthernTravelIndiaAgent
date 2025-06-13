<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAjentmenu.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UcAjentmenu" %>
   <style type="text/css" media="all" >
    .adjustedZIndex 
    { 
       z-index: 1;
    }
    .MainMenu
     {
     	  background-color:#348de7;
     	 /*  border-color:Black;
     	  border-style:solid;
     	  border-width:1px; */
     	  font-style:normal;
     	  font-family:Verdana;
     	  font-size:8pt;
     	  color:White;
     	  height:25px;
     	  
     }
   .StaticMenuStyle
     {
     	padding-top:5px;
     	padding-bottom:2px; 
     	/*text-align:center;*/
     /*	width:20px;*/
     	color:White;    
     }
     .DynamicHover
      {
      	background-color:#348de7;  
        
      }
     .DynamicMenu
      {
        background-color:#9bc8f1 ;
        border-color:White;
        border-style:solid;
        border-width:1px;
        padding-top:1px; 
        z-index: 1;
        text-align:left;
        color:White;    
        
      }
     .DynamicMenuItem
      {
       
       font-family:Verdana;
       font-size:8pt;    
       color:White; 
       border-bottom-style:solid;
       border-bottom-width:thin;
       border-color:White;          
      }
    
     
  </style>
   <asp:Menu ID="Menu1" runat="server" CssClass="MainMenu" EnableTheming="True" 
                 MaximumDynamicDisplayLevels="10" Orientation="Horizontal" 
                 StaticEnableDefaultPopOutImage="False" StaticPopOutImageTextFormatString="" 
                 StaticSubMenuIndent="46px">
                 <StaticMenuItemStyle CssClass="StaticMenuStyle" />
                 <DynamicHoverStyle CssClass="DynamicHover" />
                 <DynamicMenuStyle CssClass="DynamicMenu" />
                 <DynamicMenuItemStyle CssClass="DynamicMenuItem" HorizontalPadding="2px" 
                     ItemSpacing="2px" VerticalPadding="2px" />
                 <StaticHoverStyle BorderStyle="None" ForeColor="Black" />
                 <Items>
                     <asp:MenuItem NavigateUrl="~/agenthomepage.aspx" Text="Home" Value="Home">
                     </asp:MenuItem>
                     <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                     <asp:MenuItem Text="Bookings" Value="Bookings">
                         <asp:MenuItem Text="Tour Packages" Value="Tour Packages">
                             <asp:MenuItem NavigateUrl="~/agenthomepage.aspx?pmode=tour" 
                                 Text="Fixed Tours" Value="Fixed Tours"></asp:MenuItem>
                             <asp:MenuItem NavigateUrl="~/AgentSplPackageTours.aspx" 
                                 Text="Special Package" Value="Special Package"></asp:MenuItem>
                             <asp:MenuItem NavigateUrl="~/GroupBookingRequestForm.aspx" 
                                 Text="Group Booking" Value="Group Booking"></asp:MenuItem>
                         </asp:MenuItem>
                         <asp:MenuItem Text="Car Booking" Value="Car Booking" 
                             NavigateUrl="~/agenthomepage.aspx?pmode=car">
                         </asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/AgentHotelBooking.aspx" 
                             Text="Accommodation Booking" Value="New Item"></asp:MenuItem>
                         <%--<asp:MenuItem NavigateUrl="~/Agent/agenthomepage.aspx?pmode=hotel" 
                             Text="Hotel Booking" Value="New Item"></asp:MenuItem>--%>
                     </asp:MenuItem>
                     <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                     <asp:MenuItem Text=" Cancellation" Value="Cancellation">
                         <asp:MenuItem NavigateUrl="~/AgentCancellation.aspx" Text="Fixed Tours" 
                             Value="Fixed Tours"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/AgentSpecialTourCancellation.aspx" 
                             Text="Special Package" Value="BranchSpecialTourCancellation"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/AgentCarCancellation.aspx" 
                             Text="Car Cancellation" Value="BranchCarCancellation"></asp:MenuItem>
                         <%--<asp:MenuItem NavigateUrl="~/Agent/AgentCancelHotelTicket.aspx" 
                             Text="Hotel Cancellation" Value="HotelCancellation"></asp:MenuItem>--%>
                         <asp:MenuItem NavigateUrl="~/AccommodationCancellation.aspx" 
                             Text="Accomodation Cancellation" Value="Accomodation Cancellation">
                         </asp:MenuItem>
                     </asp:MenuItem>
                     <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                     <asp:MenuItem NavigateUrl="~/AgentBalanceClearence.aspx" Text="Balance Clearance" Value="Balance">
                     </asp:MenuItem>
                     <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                     <asp:MenuItem Text="Duplicate Tickets" Value="Duplicate Tickets">
                         <asp:MenuItem NavigateUrl="~/AgentDuplicateTicket.aspx" 
                             Text="Fixed Tours" Value="Fixed Tours"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/Agentspduplicatetickets.aspx" 
                             Text="Special Package" Value="SpecialPackage"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/AgentcarDuplicateTicket.aspx" 
                             Text="Car Booking" Value="CarBooking"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/AgentHotelAccDuplicateTicket.aspx" 
                             Text="Accommodation Booking" Value="Accommodation Booking"></asp:MenuItem>
                             
                     </asp:MenuItem>
                  <%--    <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                    <asp:MenuItem 
                         Text="Manage SubAgent" Value="ManageSubAgent">
                         
                         <asp:MenuItem NavigateUrl="~/Agent/AgentRegistration.aspx" 
                             Text="SubAgent Registration" Value="SubAgent Registration"></asp:MenuItem>
                             
                         <asp:MenuItem NavigateUrl="~/Agent/subagentwallet.aspx" 
                             Text="Manage SubAgent Wallet" Value="subagentwallet"></asp:MenuItem>
                             
                         <asp:MenuItem NavigateUrl="~/Agent/ManageSubAgentCommission.aspx" 
                             Text="Manage SubAgent Commission" Value="Manage SubAgent Commission"></asp:MenuItem>
                             
                     </asp:MenuItem>--%>
                       <asp:MenuItem Text="|" Value="|"></asp:MenuItem>
                     <asp:MenuItem 
                         Text="Reports" Value="Reports">
                         <asp:MenuItem NavigateUrl="~/agentviewreports.aspx" 
                             Text="Agent Commission" Value="agentviewreports"></asp:MenuItem>
                         <asp:MenuItem NavigateUrl="~/agenttransactiondetails.aspx" 
                             Text="Transaction Details" Value="Transaction Details"></asp:MenuItem>
                     </asp:MenuItem>
                 </Items>
             </asp:Menu>