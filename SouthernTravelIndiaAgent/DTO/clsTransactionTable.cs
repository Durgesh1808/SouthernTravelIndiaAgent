using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    /// <summary>
    /// Summary description for clsBusinessEntity
    /// </summary>
    public class clsTransactionTable
    {
        #region "Member Variable(s)"
        int? pvAgentID = 0;
        string pvRefNo = "", pvPaymentMode = "", pvNumber = "", pvBankName = "", pvRemarks = "",
            pvUserName = "", pvBranchCode = "", pvChqDate = "", pvTransType = "",
            pvImpersonatingUserName = "", pvImpersonatingBranchCode = "", pvCashier = "", pvStatus = "";
        decimal? pvTicketAmount = null, pvCredit = null, pvServiceTax = null, pvAgentCredit = null,
            pvAvailableBalance = null, pvAgentDebit = null, pvCommission = null, pvDebit = null, pvTDS = null,
            pvDiscount = null;
        char? transState = null;
        #endregion
        #region "Property(s)"
        public int? fldAgentID
        {
            get { return pvAgentID; }
            set { pvAgentID = value; }
        }
        public string fldRefNo
        {
            get { return pvRefNo; }
            set { pvRefNo = value; }
        }

        public string fldPaymentMode
        {
            get { return pvPaymentMode; }
            set { pvPaymentMode = value; }
        }

        public string fldNumber
        {
            get { return pvNumber; }
            set { pvNumber = value; }
        }

        public string fldBankName
        {
            get { return pvBankName; }
            set { pvBankName = value; }
        }

        public string fldRemarks
        {
            get { return pvRemarks; }
            set { pvRemarks = value; }
        }

        public string fldUserName
        {
            get { return pvUserName; }
            set { pvUserName = value; }
        }

        public string fldBranchCode
        {
            get { return pvBranchCode; }
            set { pvBranchCode = value; }
        }

        public string fldChqDate
        {
            get { return pvChqDate; }
            set { pvChqDate = value; }
        }

        public string fldTransType
        {
            get { return pvTransType; }
            set { pvTransType = value; }
        }

        public string fldImpersonatingUserName
        {
            get { return pvImpersonatingUserName; }
            set { pvImpersonatingUserName = value; }
        }

        public string fldImpersonatingBranchCode
        {
            get { return pvImpersonatingBranchCode; }
            set { pvImpersonatingBranchCode = value; }
        }

        public string fldCashier
        {
            get { return pvCashier; }
            set { pvCashier = value; }
        }

        public string fldStatus
        {
            get { return pvStatus; }
            set { pvStatus = value; }
        }

        public decimal? fldTicketAmount
        {
            get { return pvTicketAmount; }
            set { pvTicketAmount = value; }
        }

        public decimal? fldCredit
        {
            get { return pvCredit; }
            set { pvCredit = value; }
        }

        public decimal? fldServiceTax
        {
            get { return pvServiceTax; }
            set { pvServiceTax = value; }
        }

        public decimal? fldAgentCredit
        {
            get { return pvAgentCredit; }
            set { pvAgentCredit = value; }
        }

        public decimal? fldAvailableBalance
        {
            get { return pvAvailableBalance; }
            set { pvAvailableBalance = value; }
        }

        public decimal? fldAgentDebit
        {
            get { return pvAgentDebit; }
            set { pvAgentDebit = value; }
        }

        public decimal? fldCommission
        {
            get { return pvCommission; }
            set { pvCommission = value; }
        }

        public decimal? fldDebit
        {
            get { return pvDebit; }
            set { pvDebit = value; }
        }

        public decimal? fldTDS
        {
            get { return pvTDS; }
            set { pvTDS = value; }
        }

        public decimal? fldDiscount
        {
            get { return pvDiscount; }
            set { pvDiscount = value; }
        }

        public char? fldTransState
        {
            get { return transState; }
            set { transState = value; }
        }
        #endregion
        #region "Constructor"
        public clsTransactionTable()
        {
        }
        #endregion
    }

    public class clsTransaction_Advance
    {
        int RowID;
        string RefNo, BranchCode, UserID, TourName, CashierID, CashierBranch, Number, BankName, ChqDate,
            NoOfPax, OldTicketNo, CashSheetID, PaymentMode, TransactionType;
        decimal TicketAmount, AdvancePaid, RefundAmount;
        DateTime PaymentDate, CashReceiptDate, DOJ;
        char IsPartialCancel;

        public int fldRowID
        {
            get { return RowID; }
            set { RowID = value; }
        }

        public string fldTransactionType
        {
            get { return TransactionType; }
            set { TransactionType = value; }
        }

        public string fldRefNo
        {
            get { return RefNo; }
            set { RefNo = value; }
        }

        public string fldBranchCode
        {
            get { return BranchCode; }
            set { BranchCode = value; }
        }

        public string fldUserID
        {
            get { return UserID; }
            set { UserID = value; }
        }

        public string fldTourName
        {
            get { return TourName; }
            set { TourName = value; }
        }

        public string fldCashierID
        {
            get { return CashierID; }
            set { CashierID = value; }
        }

        public string fldCashierBranch
        {
            get { return CashierBranch; }
            set { CashierBranch = value; }
        }

        public string fldNumber
        {
            get { return Number; }
            set { Number = value; }
        }

        public string fldBankName
        {
            get { return BankName; }
            set { BankName = value; }
        }

        public string fldChqDate
        {
            get { return ChqDate; }
            set { ChqDate = value; }
        }

        public string fldNoOfPax
        {
            get { return NoOfPax; }
            set { NoOfPax = value; }
        }

        public string fldOldTicketNo
        {
            get { return OldTicketNo; }
            set { OldTicketNo = value; }
        }

        public string fldCashSheetID
        {
            get { return CashSheetID; }
            set { CashSheetID = value; }
        }

        public decimal fldTicketAmount
        {
            get { return TicketAmount; }
            set { TicketAmount = value; }
        }

        public decimal fldAdvancePaid
        {
            get { return AdvancePaid; }
            set { AdvancePaid = value; }
        }

        public decimal fldRefundAmount
        {
            get { return RefundAmount; }
            set { RefundAmount = value; }
        }

        public DateTime fldPaymentDate
        {
            get { return PaymentDate; }
            set { PaymentDate = value; }
        }

        public DateTime fldCashReceiptDate
        {
            get { return CashReceiptDate; }
            set { CashReceiptDate = value; }
        }

        public DateTime fldDOJ
        {
            get { return DOJ; }
            set { DOJ = value; }
        }

        public string fldPaymentMode
        {
            get { return PaymentMode; }
            set { PaymentMode = value; }
        }

        public char fldIsPartialCancel
        {
            get { return IsPartialCancel; }
            set { IsPartialCancel = value; }
        }
    }

    public class clsAcc_Voucher
    {
        int i_hotelId, i_duration, i_noofrooms, i_singlerooms, i_extrabed;
        string i_ticketno, i_ac, i_username, i_branchcode;
        decimal i_doubleRoomFare, i_singleRoomFare, i_extraBedFare, i_heaterFare;
        DateTime i_checkindate, i_checkoutdate, i_addedon;
        char i_isheaterprovided;

        public int pHotelId
        {
            get { return i_hotelId; }
            set { i_hotelId = value; }
        }
        public int pDuration
        {
            get { return i_duration; }
            set { i_duration = value; }
        }
        public int pNoofRooms
        {
            get { return i_noofrooms; }
            set { i_noofrooms = value; }
        }
        public int pSingleRooms
        {
            get { return i_singlerooms; }
            set { i_singlerooms = value; }
        }
        public int pExtrabed
        {
            get { return i_extrabed; }
            set { i_extrabed = value; }
        }
        public string pTicketNo
        {
            get { return i_ticketno; }
            set { i_ticketno = value; }
        }

        public string pAC
        {
            get { return i_ac; }
            set { i_ac = value; }
        }
        public string pUserName
        {
            get { return i_username; }
            set { i_username = value; }
        }
        public string pBranchCode
        {
            get { return i_branchcode; }
            set { i_branchcode = value; }
        }

        public decimal pDoubleRoomFare
        {
            get { return i_doubleRoomFare; }
            set { i_doubleRoomFare = value; }
        }
        public decimal pSingleRoomFare
        {
            get { return i_singleRoomFare; }
            set { i_singleRoomFare = value; }
        }
        public decimal pExtraBedFare
        {
            get { return i_extraBedFare; }
            set { i_extraBedFare = value; }
        }
        public decimal pHeaterFare
        {
            get { return i_heaterFare; }
            set { i_heaterFare = value; }
        }

        public DateTime pCheckinDate
        {
            get { return i_checkindate; }
            set { i_checkindate = value; }
        }
        public DateTime pCheckoutDate
        {
            get { return i_checkoutdate; }
            set { i_checkoutdate = value; }
        }
        public DateTime pAddedOn
        {
            get { return i_addedon; }
            set { i_addedon = value; }
        }

        public char pIsHeaterProvided
        {
            get { return i_isheaterprovided; }
            set { i_isheaterprovided = value; }
        }
    }

    public class clsIntEstimateDetails
    {
        int i_tourId, i_noOfPax, i_noOfchild;
        string i_pnrNo, i_emailId, i_mobile, i_phone, i_address, i_city, i_state, i_pincode, i_paymentType, i_strTourName, i_groupLeader, i_branchCode,
            i_estRaisedBy;
        decimal i_amount, i_serviceTax, i_discount, i_totalAmount, i_adultPerHead, i_childCostPerHead, i_strAdvance, i_strBalance;
        DateTime i_journeyDate, i_returnDate;

        public int pTourId
        {
            get { return i_tourId; }
            set { i_tourId = value; }
        }
        public int pNoOfPax
        {
            get { return i_noOfPax; }
            set { i_noOfPax = value; }
        }
        public int pNoOfchild
        {
            get { return i_noOfchild; }
            set { i_noOfchild = value; }
        }


        public string pPnrNo
        {
            get { return i_pnrNo; }
            set { i_pnrNo = value; }
        }
        public string pEmailId
        {
            get { return i_emailId; }
            set { i_emailId = value; }
        }
        public string pMobile
        {
            get { return i_mobile; }
            set { i_mobile = value; }
        }
        public string pPhone
        {
            get { return i_phone; }
            set { i_phone = value; }
        }
        public string pAddress
        {
            get { return i_address; }
            set { i_address = value; }
        }
        public string pCity
        {
            get { return i_city; }
            set { i_city = value; }
        }
        public string pState
        {
            get { return i_state; }
            set { i_state = value; }
        }
        public string pPincode
        {
            get { return i_pincode; }
            set { i_pincode = value; }
        }
        public string pPaymentType
        {
            get { return i_paymentType; }
            set { i_paymentType = value; }
        }
        public string pTourName
        {
            get { return i_strTourName; }
            set { i_strTourName = value; }
        }
        public string pGroupLeader
        {
            get { return i_groupLeader; }
            set { i_groupLeader = value; }
        }
        public string pBranchCode
        {
            get { return i_branchCode; }
            set { i_branchCode = value; }
        }
        public string pEstRaisedBy
        {
            get { return i_estRaisedBy; }
            set { i_estRaisedBy = value; }
        }

        public decimal pAmount
        {
            get { return i_amount; }
            set { i_amount = value; }
        }
        public decimal pServiceTax
        {
            get { return i_serviceTax; }
            set { i_serviceTax = value; }
        }
        public decimal pDiscount
        {
            get { return i_discount; }
            set { i_discount = value; }
        }
        public decimal pTotalAmount
        {
            get { return i_totalAmount; }
            set { i_totalAmount = value; }
        }
        public decimal pAdultPerHead
        {
            get { return i_adultPerHead; }
            set { i_adultPerHead = value; }
        }
        public decimal pChildCostPerHead
        {
            get { return i_childCostPerHead; }
            set { i_childCostPerHead = value; }
        }
        public decimal pAdvance
        {
            get { return i_strAdvance; }
            set { i_strAdvance = value; }
        }
        public decimal pBalance
        {
            get { return i_strBalance; }
            set { i_strBalance = value; }
        }

        public DateTime pJourneyDate
        {
            get { return i_journeyDate; }
            set { i_journeyDate = value; }
        }
        public DateTime pReturnDate
        {
            get { return i_returnDate; }
            set { i_returnDate = value; }
        }

    }

    public class clsNewGroupbooking_Tbl
    {
        int i_Adult, i_Child, i_AdultFare, i_ChildFare, i_VegPerHead, i_Vegnoofpersons, i_nonvegperhead, i_nonvegnoofpersons, i_RowID, i_CustID, i_ZoneID;

        string i_tourName, i_PnrNo, i_GroupLeaderName, i_PhoneNo, i_MobileNo, i_Email, i_Address, i_City, i_State, i_Paymode, i_BusType, i_DeptTime,
           i_ArrivalTime, i_username, i_branchcode, i_accm, i_food, i_status, i_refno, i_pickup, i_drop, i_remarks, i_estRaisedBy;

        decimal i_Amount, i_Discount, i_Advance, i_CostAfterDiscount, i_DriverReward, i_totalamount, i_stax, i_strBalance, i_StaxPer;

        DateTime i_Departure, i_Arrival;

        char i_AC, i_TotalCalType, i_iscancel;

        bool i_IsMultiPurpose;
        public int pRowID
        {
            get { return i_RowID; }
            set { i_RowID = value; }
        }
        public int pCustID
        {
            get { return i_CustID; }
            set { i_CustID = value; }
        }
        public int pZoneID
        {
            get { return i_ZoneID; }
            set { i_ZoneID = value; }
        }
        public int pAdult
        {
            get { return i_Adult; }
            set { i_Adult = value; }
        }
        public int pChild
        {
            get { return i_Child; }
            set { i_Child = value; }
        }
        public int pAdultFare
        {
            get { return i_AdultFare; }
            set { i_AdultFare = value; }
        }
        public int pChildFare
        {
            get { return i_ChildFare; }
            set { i_ChildFare = value; }
        }
        public int pVegPerHead
        {
            get { return i_VegPerHead; }
            set { i_VegPerHead = value; }
        }
        public int pVegnoofpersons
        {
            get { return i_Vegnoofpersons; }
            set { i_Vegnoofpersons = value; }
        }
        public int pnonvegperhead
        {
            get { return i_nonvegperhead; }
            set { i_nonvegperhead = value; }
        }
        public int pnonvegnoofpersons
        {
            get { return i_nonvegnoofpersons; }
            set { i_nonvegnoofpersons = value; }
        }

        public string pTourName
        {
            get { return i_tourName; }
            set { i_tourName = value; }
        }
        public string pPnrNo
        {
            get { return i_PnrNo; }
            set { i_PnrNo = value; }
        }
        public string pGroupLeaderName
        {
            get { return i_GroupLeaderName; }
            set { i_GroupLeaderName = value; }
        }
        public string pPhoneNo
        {
            get { return i_PhoneNo; }
            set { i_PhoneNo = value; }
        }
        public string pMobileNo
        {
            get { return i_MobileNo; }
            set { i_MobileNo = value; }
        }
        public string pEmail
        {
            get { return i_Email; }
            set { i_Email = value; }
        }
        public string pAddress
        {
            get { return i_Address; }
            set { i_Address = value; }
        }
        public string pCity
        {
            get { return i_City; }
            set { i_City = value; }
        }
        public string pState
        {
            get { return i_State; }
            set { i_State = value; }
        }
        public string pPaymode
        {
            get { return i_Paymode; }
            set { i_Paymode = value; }
        }
        public string pBusType
        {
            get { return i_BusType; }
            set { i_BusType = value; }
        }

        public string pArrivalTime
        {
            get { return i_ArrivalTime; }
            set { i_ArrivalTime = value; }
        }
        public string pDeptTime
        {
            get { return i_DeptTime; }
            set { i_DeptTime = value; }
        }
        public string pUserName
        {
            get { return i_username; }
            set { i_username = value; }
        }
        public string pBranchCode
        {
            get { return i_branchcode; }
            set { i_branchcode = value; }
        }
        public string pAccm
        {
            get { return i_accm; }
            set { i_accm = value; }
        }
        public string pFood
        {
            get { return i_food; }
            set { i_food = value; }
        }
        public string pStatus
        {
            get { return i_status; }
            set { i_status = value; }
        }
        public string pRefno
        {
            get { return i_refno; }
            set { i_refno = value; }
        }
        public string pPickup
        {
            get { return i_pickup; }
            set { i_pickup = value; }
        }
        public string pDrop
        {
            get { return i_drop; }
            set { i_drop = value; }
        }
        public string pRemarks
        {
            get { return i_remarks; }
            set { i_remarks = value; }
        }
        public string pEstRaisedBy
        {
            get { return i_estRaisedBy; }
            set { i_estRaisedBy = value; }
        }

        public decimal pAmount
        {
            get { return i_Amount; }
            set { i_Amount = value; }
        }
        public decimal pDiscount
        {
            get { return i_Discount; }
            set { i_Discount = value; }
        }
        public decimal pAdvance
        {
            get { return i_Advance; }
            set { i_Advance = value; }
        }
        public decimal pCostAfterDiscount
        {
            get { return i_CostAfterDiscount; }
            set { i_CostAfterDiscount = value; }
        }
        public decimal pDriverReward
        {
            get { return i_DriverReward; }
            set { i_DriverReward = value; }
        }
        public decimal pTotalAmount
        {
            get { return i_totalamount; }
            set { i_totalamount = value; }
        }
        public decimal pStax
        {
            get { return i_stax; }
            set { i_stax = value; }
        }
        public decimal pStaxPer
        {
            get { return i_StaxPer; }
            set { i_StaxPer = value; }
        }
        public DateTime pDeparture
        {
            get { return i_Departure; }
            set { i_Departure = value; }
        }
        public DateTime pArrival
        {
            get { return i_Arrival; }
            set { i_Arrival = value; }
        }

        public char pAC
        {
            get { return i_AC; }
            set { i_AC = value; }
        }
        public char pTotalCalType
        {
            get { return i_TotalCalType; }
            set { i_TotalCalType = value; }
        }
        public char piscancel
        {
            get { return i_iscancel; }
            set { i_iscancel = value; }
        }

        public bool pIsMultiPurpose
        {
            get { return i_IsMultiPurpose; }
            set { i_IsMultiPurpose = value; }
        }

    }

    public class clsTbl_CarOperatorFareMaster
    {
        int i_CarId, i_OperatorId, i_OutPerKmAcFare, i_OutPerKmNonAcFare, i_OutMinKmAllowed, i_OutExtraKmFare, i_OutExtraKmNonAcFare,
            i_OutNightHalt, i_OutDriverReward, i_Local4Hr40KmAc, i_Local6Hr60KmAc, i_Local8Hr80KmAc, i_Local4Hr40KmNonAc,
            i_Local6Hr60KmNonAc, i_Local8Hr80KmNonAc, i_LocalExtrakmFareAc, i_LocalExtraHrFareAc, i_LocalExtrakmFareNonAc,
            i_LocalExtraHrFareNonAc, i_FareId;
        string i_BranchCode, i_AddedBy, i_UpdatedBy;
        DateTime i_AddedOn, i_UpdatedOn;
        char i_IsActive;

        public int pCarId
        {
            get { return i_CarId; }
            set { i_CarId = value; }
        }
        public int pOperatorId
        {
            get { return i_OperatorId; }
            set { i_OperatorId = value; }
        }
        public int pOutPerKmAcFare
        {
            get { return i_OutPerKmAcFare; }
            set { i_OutPerKmAcFare = value; }
        }
        public int pOutPerKmNonAcFare
        {
            get { return i_OutPerKmNonAcFare; }
            set { i_OutPerKmNonAcFare = value; }
        }
        public int pOutMinKmAllowed
        {
            get { return i_OutMinKmAllowed; }
            set { i_OutMinKmAllowed = value; }
        }
        public int pOutExtraKmFare
        {
            get { return i_OutExtraKmFare; }
            set { i_OutExtraKmFare = value; }
        }
        public int pOutExtraKmNonAcFare
        {
            get { return i_OutExtraKmNonAcFare; }
            set { i_OutExtraKmNonAcFare = value; }
        }
        public int pOutNightHalt
        {
            get { return i_OutNightHalt; }
            set { i_OutNightHalt = value; }
        }
        public int pOutDriverReward
        {
            get { return i_OutDriverReward; }
            set { i_OutDriverReward = value; }
        }
        public int pLocal4Hr40KmAc
        {
            get { return i_Local4Hr40KmAc; }
            set { i_Local4Hr40KmAc = value; }
        }

        public int pLocal6Hr60KmAc
        {
            get { return i_Local6Hr60KmAc; }
            set { i_Local6Hr60KmAc = value; }
        }

        public int pLocal8Hr80KmAc
        {
            get { return i_Local8Hr80KmAc; }
            set { i_Local8Hr80KmAc = value; }
        }
        public int pLocal4Hr40KmNonAc
        {
            get { return i_Local4Hr40KmNonAc; }
            set { i_Local4Hr40KmNonAc = value; }
        }
        public int pLocal6Hr60KmNonAc
        {
            get { return i_Local6Hr60KmNonAc; }
            set { i_Local6Hr60KmNonAc = value; }
        }
        public int pLocal8Hr80KmNonAc
        {
            get { return i_Local8Hr80KmNonAc; }
            set { i_Local8Hr80KmNonAc = value; }
        }
        public int pLocalExtrakmFareAc
        {
            get { return i_LocalExtrakmFareAc; }
            set { i_LocalExtrakmFareAc = value; }
        }
        public int pLocalExtraHrFareAc
        {
            get { return i_LocalExtraHrFareAc; }
            set { i_LocalExtraHrFareAc = value; }
        }
        public int pLocalExtrakmFareNonAc
        {
            get { return i_LocalExtrakmFareNonAc; }
            set { i_LocalExtrakmFareNonAc = value; }
        }
        public int pLocalExtraHrFareNonAc
        {
            get { return i_LocalExtraHrFareNonAc; }
            set { i_LocalExtraHrFareNonAc = value; }
        }
        public int pFareId
        {
            get { return i_FareId; }
            set { i_FareId = value; }
        }

        public string pBranchCode
        {
            get { return i_BranchCode; }
            set { i_BranchCode = value; }
        }
        public string pAddedBy
        {
            get { return i_AddedBy; }
            set { i_AddedBy = value; }
        }
        public string pUpdatedBy
        {
            get { return i_UpdatedBy; }
            set { i_UpdatedBy = value; }
        }
        public DateTime pUpdatedOn
        {
            get { return i_UpdatedOn; }
            set { i_UpdatedOn = value; }
        }
        public DateTime pAddedOn
        {
            get { return i_AddedOn; }
            set { i_AddedOn = value; }
        }

        public char pIsActive
        {
            get { return i_IsActive; }
            set { i_IsActive = value; }
        }
    }

    public class clsTbl_CarOperatorMaster
    {
        int i_OperatorId_1;
        string i_OperatorName, i_OperatingBranch, i_Address, i_City, i_State, i_Country, i_EmailId, i_Zip, i_PhoneNo, i_MobileNo, i_UserName, i_BranchCode;
        char i_Activated; string i_OperatingBranch_2;

        public int pOperatorId_1
        {
            get { return i_OperatorId_1; }
            set { i_OperatorId_1 = value; }
        }

        public string pOperatorName
        {
            get { return i_OperatorName; }
            set { i_OperatorName = value; }
        }

        public string pOperatingBranch
        {
            get { return i_OperatingBranch; }
            set { i_OperatingBranch = value; }
        }
        public string pAddress
        {
            get { return i_Address; }
            set { i_Address = value; }
        }
        public string pCity
        {
            get { return i_City; }
            set { i_City = value; }
        }

        public string pState
        {
            get { return i_State; }
            set { i_State = value; }
        }
        public string pCountry
        {
            get { return i_Country; }
            set { i_Country = value; }
        }
        public string pEmailId
        {
            get { return i_EmailId; }
            set { i_EmailId = value; }
        }

        public string pZip
        {
            get { return i_Zip; }
            set { i_Zip = value; }
        }
        public string pPhoneNo
        {
            get { return i_PhoneNo; }
            set { i_PhoneNo = value; }
        }
        public string pMobileNo
        {
            get { return i_MobileNo; }
            set { i_MobileNo = value; }
        }
        public string pUserName
        {
            get { return i_UserName; }
            set { i_UserName = value; }
        }
        public string pBranchCode
        {
            get { return i_BranchCode; }
            set { i_BranchCode = value; }
        }


        public char pActivated
        {
            get { return i_Activated; }
            set { i_Activated = value; }
        }
        public string pOperatingBranch_2
        {
            get { return i_OperatingBranch_2; }
            set { i_OperatingBranch_2 = value; }
        }
    }

    public class clsAgentCommissionLevelMaster_tbl
    {
        decimal i_BlueComm, i_SilverComm, i_GoldComm, i_Platinum;



        public decimal pBlueComm
        {
            get { return i_BlueComm; }
            set { i_BlueComm = value; }
        }
        public decimal pSilverComm
        {
            get { return i_SilverComm; }
            set { i_SilverComm = value; }
        }
        public decimal pGoldComm
        {
            get { return i_GoldComm; }
            set { i_GoldComm = value; }
        }

        public decimal pPlatinum
        {
            get { return i_Platinum; }
            set { i_Platinum = value; }
        }
    }
    [Serializable]
    public class BarCodeInfo
    {

        private int _TourNo;

        private string _TourCode;

        private string _TourName;

        private string _OrderID;

        private string _TicketNo;

        private int _TotalPax;

        private System.Nullable<System.DateTime> _BookingDate;

        private System.Nullable<System.DateTime> _JourneyDate;

        private string _TourSerial;

        private string _BusSerialNo;

        public BarCodeInfo()
        {
        }

        public int TourNo
        {
            get
            {
                return this._TourNo;
            }
            set
            {
                if ((this._TourNo != value))
                {
                    this._TourNo = value;
                }
            }
        }

        public string TourCode
        {
            get
            {
                return this._TourCode;
            }
            set
            {
                if ((this._TourCode != value))
                {
                    this._TourCode = value;
                }
            }
        }

        public string TourName
        {
            get
            {
                return this._TourName;
            }
            set
            {
                if ((this._TourName != value))
                {
                    this._TourName = value;
                }
            }
        }

        public string OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                if ((this._OrderID != value))
                {
                    this._OrderID = value;
                }
            }
        }

        public string TicketNo
        {
            get
            {
                return this._TicketNo;
            }
            set
            {
                if ((this._TicketNo != value))
                {
                    this._TicketNo = value;
                }
            }
        }

        public int TotalPax
        {
            get
            {
                return this._TotalPax;
            }
            set
            {
                if ((this._TotalPax != value))
                {
                    this._TotalPax = value;
                }
            }
        }

        public System.Nullable<System.DateTime> BookingDate
        {
            get
            {
                return this._BookingDate;
            }
            set
            {
                if ((this._BookingDate != value))
                {
                    this._BookingDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> JourneyDate
        {
            get
            {
                return this._JourneyDate;
            }
            set
            {
                if ((this._JourneyDate != value))
                {
                    this._JourneyDate = value;
                }
            }
        }

        public string TourSerial
        {
            get
            {
                return this._TourSerial;
            }
            set
            {
                if ((this._TourSerial != value))
                {
                    this._TourSerial = value;
                }
            }
        }

        public string BusSerialNo
        {
            get
            {
                return this._BusSerialNo;
            }
            set
            {
                if ((this._BusSerialNo != value))
                {
                    this._BusSerialNo = value;
                }
            }
        }
    }

    /// <summary>
    /// Summary description for Code39.
    /// </summary>
    public class BC39
    {
        private const int _itemSepHeight = 3;

        SizeF _titleSize = SizeF.Empty;
        SizeF _barCodeSize = SizeF.Empty;
        SizeF _codeStringSize = SizeF.Empty;

        #region Barcode Title

        private string _titleString = null;
        private Font _titleFont = null;

        public string Title
        {
            get { return _titleString; }
            set { _titleString = value; }
        }

        public Font TitleFont
        {
            get { return _titleFont; }
            set { _titleFont = value; }
        }
        #endregion

        #region Barcode code string

        private bool _showCodeString = false;
        private Font _codeStringFont = null;

        public bool ShowCodeString
        {
            get { return _showCodeString; }
            set { _showCodeString = value; }
        }

        public Font CodeStringFont
        {
            get { return _codeStringFont; }
            set { _codeStringFont = value; }
        }
        #endregion

        #region Barcode Font

        private Font _c39Font = null;
        private float _c39FontSize = 12;
        private string _c39FontFileName = null;
        private string _c39FontFamilyName = null;

        public string FontFileName
        {
            get { return _c39FontFileName; }
            set { _c39FontFileName = value; }
        }

        public string FontFamilyName
        {
            get { return _c39FontFamilyName; }
            set { _c39FontFamilyName = value; }
        }

        public float FontSize
        {
            get { return _c39FontSize; }
            set { _c39FontSize = value; }
        }

        private Font Code39Font
        {
            get
            {
                if (_c39Font == null)
                {
                    // Load the barcode font			
                    PrivateFontCollection pfc = new PrivateFontCollection();
                    pfc.AddFontFile(_c39FontFileName);
                    FontFamily family = new FontFamily(_c39FontFamilyName, pfc);
                    _c39Font = new Font(family, _c39FontSize);
                }
                return _c39Font;
            }
        }

        #endregion

        public BC39()
        {
            _titleFont = new Font("Arial", 10);
            _codeStringFont = new Font("Arial", 10);
        }

        #region Barcode Generation

        public Bitmap GenerateBarcode(string barCode)
        {

            int bcodeWidth = 0;
            int bcodeHeight = 0;

            // Get the image container...
            Bitmap bcodeBitmap = CreateImageContainer(barCode, ref bcodeWidth, ref bcodeHeight);
            Graphics objGraphics = Graphics.FromImage(bcodeBitmap);

            // Fill the background			
            objGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, bcodeWidth, bcodeHeight));

            int vpos = 0;

            // Draw the title string
            if (_titleString != null)
            {
                objGraphics.DrawString(_titleString, _titleFont, new SolidBrush(Color.Black), XCentered((int)_titleSize.Width, bcodeWidth), vpos);
                vpos += (((int)_titleSize.Height) + _itemSepHeight);
            }
            // Draw the barcode
            objGraphics.DrawString(barCode, Code39Font, new SolidBrush(Color.Black), XCentered((int)_barCodeSize.Width, bcodeWidth), vpos);

            // Draw the barcode string
            if (_showCodeString)
            {
                vpos += (((int)_barCodeSize.Height));
                objGraphics.DrawString(barCode, _codeStringFont, new SolidBrush(Color.Black), XCentered((int)_codeStringSize.Width, bcodeWidth), vpos);
            }

            // return the image...									
            return bcodeBitmap;
        }

        private Bitmap CreateImageContainer(string barCode, ref int bcodeWidth, ref int bcodeHeight)
        {

            Graphics objGraphics;

            // Create a temporary bitmap...
            Bitmap tmpBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            objGraphics = Graphics.FromImage(tmpBitmap);

            // calculate size of the barcode items...
            if (_titleString != null)
            {
                _titleSize = objGraphics.MeasureString(_titleString, _titleFont);
                bcodeWidth = (int)_titleSize.Width;
                bcodeHeight = (int)_titleSize.Height + _itemSepHeight;
            }

            _barCodeSize = objGraphics.MeasureString(barCode, Code39Font);
            bcodeWidth = Max(bcodeWidth, (int)_barCodeSize.Width);
            bcodeHeight += (int)_barCodeSize.Height;

            if (_showCodeString)
            {
                _codeStringSize = objGraphics.MeasureString(barCode, _codeStringFont);
                bcodeWidth = Max(bcodeWidth, (int)_codeStringSize.Width);
                bcodeHeight += (_itemSepHeight + (int)_codeStringSize.Height);
            }

            // dispose temporary objects...
            objGraphics.Dispose();
            tmpBitmap.Dispose();

            return (new Bitmap(bcodeWidth, bcodeHeight, PixelFormat.Format32bppArgb));
        }

        #endregion


        #region Auxiliary Methods

        private int Max(int v1, int v2)
        {
            return (v1 > v2 ? v1 : v2);
        }

        private int XCentered(int localWidth, int globalWidth)
        {
            return ((globalWidth - localWidth) / 2);
        }

        #endregion

    }

    [Serializable]
    public class Airlines
    {
        private int _AirlineID;

        private System.Nullable<int> _Curr_VersionID;

        private System.Nullable<bool> _IsActive;

        private int _Ver_VersionID;

        private string _AirlineName;

        public Airlines(int pAirlineID, int pCurrentVersionID, bool pIsActive, int pVerVersionID, string pAirlineName)
        {
            AirlineID = pAirlineID;
            Curr_VersionID = pCurrentVersionID;
            IsActive = pIsActive;
            Ver_VersionID = pVerVersionID;
            AirlineName = pAirlineName;
        }

        public int AirlineID
        {
            get
            {
                return this._AirlineID;
            }
            set
            {
                if ((this._AirlineID != value))
                {
                    this._AirlineID = value;
                }
            }
        }

        public System.Nullable<int> Curr_VersionID
        {
            get
            {
                return this._Curr_VersionID;
            }
            set
            {
                if ((this._Curr_VersionID != value))
                {
                    this._Curr_VersionID = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                if ((this._IsActive != value))
                {
                    this._IsActive = value;
                }
            }
        }

        public int Ver_VersionID
        {
            get
            {
                return this._Ver_VersionID;
            }
            set
            {
                if ((this._Ver_VersionID != value))
                {
                    this._Ver_VersionID = value;
                }
            }
        }

        public string AirlineName
        {
            get
            {
                return this._AirlineName;
            }
            set
            {
                if ((this._AirlineName != value))
                {
                    this._AirlineName = value;
                }
            }
        }
    }

    [Serializable]
    public class Source
    {
        private int _SourceID;

        private System.Nullable<int> _Curr_VersionID;

        private System.Nullable<bool> _IsActive;

        private int _Ver_VersionID;

        private string _SourceName;

        private System.Nullable<int> _SourceTypeID;

        public Source(int pSourceID, int pCurrVersionID, bool pIsActive, int pVerVersionID, string pSourceName, int pSourceTypeID)
        {
            SourceID = pSourceID;
            Curr_VersionID = pCurrVersionID;
            IsActive = pIsActive;
            Ver_VersionID = pVerVersionID;
            SourceName = pSourceName;
            SourceTypeID = pSourceTypeID;
        }

        public int SourceID
        {
            get
            {
                return this._SourceID;
            }
            set
            {
                if ((this._SourceID != value))
                {
                    this._SourceID = value;
                }
            }
        }

        public System.Nullable<int> Curr_VersionID
        {
            get
            {
                return this._Curr_VersionID;
            }
            set
            {
                if ((this._Curr_VersionID != value))
                {
                    this._Curr_VersionID = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                if ((this._IsActive != value))
                {
                    this._IsActive = value;
                }
            }
        }

        public int Ver_VersionID
        {
            get
            {
                return this._Ver_VersionID;
            }
            set
            {
                if ((this._Ver_VersionID != value))
                {
                    this._Ver_VersionID = value;
                }
            }
        }

        public string SourceName
        {
            get
            {
                return this._SourceName;
            }
            set
            {
                if ((this._SourceName != value))
                {
                    this._SourceName = value;
                }
            }
        }

        public System.Nullable<int> SourceTypeID
        {
            get
            {
                return this._SourceTypeID;
            }
            set
            {
                if ((this._SourceTypeID != value))
                {
                    this._SourceTypeID = value;
                }
            }
        }
    }
}