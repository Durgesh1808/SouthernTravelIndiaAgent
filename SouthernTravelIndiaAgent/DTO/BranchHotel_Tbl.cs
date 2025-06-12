using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace SouthernTravelIndiaAgent.DTO
{
    public partial class BranchHotel_Tbl : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private decimal _Rowid;

        private System.Nullable<decimal> _CustRowId;

        private System.Nullable<decimal> _noofadults;

        private System.Nullable<decimal> _noofchildren;

        private System.Nullable<int> _noofrooms;

        private System.Nullable<int> _Deluxe;

        private System.Nullable<int> _SuperDeluxe;

        private System.Nullable<int> _Executive;

        private System.Nullable<int> _Royal;

        private System.Nullable<decimal> _Totalamount;

        private System.Nullable<decimal> _STaxValue;

        private System.Nullable<decimal> _AmtWithTax;

        private System.Nullable<decimal> _Discount;

        private System.Nullable<decimal> _Advance;

        private string _Paymode;

        private string _CheckNo;

        private string _bankname;

        private System.Nullable<System.DateTime> _TransactionDate;

        private System.DateTime _BookingDate;

        private System.Nullable<System.DateTime> _depttime;

        private System.Nullable<System.DateTime> _arrtime;

        private string _Username;

        private string _Branchcode;

        private System.Nullable<char> _Iscancel;

        private string _OrderId;

        private string _ticketno;

        private string _Remarks;

        private string _BookingRoomTypes;

        private string _Status;

        private System.Nullable<int> _AgentId;

        private System.Nullable<bool> _Cancelled;

        private System.Nullable<decimal> _CancellationAmt;

        private System.Nullable<decimal> _RefundAmt;

        private System.Nullable<decimal> _noofdays;

        private string _IDSNo;

        private System.Nullable<bool> _IDSStatus;

        private string _PickUpVeh;

        private string _PickVehNo;

        private string _PickTime;

        private string _DropVeh;

        private string _DropVehNo;

        private string _DropTime;

        private string _Station;

        private string _PkStation;

        private System.Nullable<decimal> _ExtraFare;

        private System.Nullable<int> _ExtraBed;

        private System.Nullable<decimal> _MasterDiscount;

        private System.Nullable<int> _HotelID;

        private string _Occupancy;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnCreated();
        partial void OnRowidChanging(decimal value);
        partial void OnRowidChanged();
        partial void OnCustRowIdChanging(System.Nullable<decimal> value);
        partial void OnCustRowIdChanged();
        partial void OnnoofadultsChanging(System.Nullable<decimal> value);
        partial void OnnoofadultsChanged();
        partial void OnnoofchildrenChanging(System.Nullable<decimal> value);
        partial void OnnoofchildrenChanged();
        partial void OnnoofroomsChanging(System.Nullable<int> value);
        partial void OnnoofroomsChanged();
        partial void OnDeluxeChanging(System.Nullable<int> value);
        partial void OnDeluxeChanged();
        partial void OnSuperDeluxeChanging(System.Nullable<int> value);
        partial void OnSuperDeluxeChanged();
        partial void OnExecutiveChanging(System.Nullable<int> value);
        partial void OnExecutiveChanged();
        partial void OnRoyalChanging(System.Nullable<int> value);
        partial void OnRoyalChanged();
        partial void OnTotalamountChanging(System.Nullable<decimal> value);
        partial void OnTotalamountChanged();
        partial void OnSTaxValueChanging(System.Nullable<decimal> value);
        partial void OnSTaxValueChanged();
        partial void OnAmtWithTaxChanging(System.Nullable<decimal> value);
        partial void OnAmtWithTaxChanged();
        partial void OnDiscountChanging(System.Nullable<decimal> value);
        partial void OnDiscountChanged();
        partial void OnAdvanceChanging(System.Nullable<decimal> value);
        partial void OnAdvanceChanged();
        partial void OnPaymodeChanging(string value);
        partial void OnPaymodeChanged();
        partial void OnCheckNoChanging(string value);
        partial void OnCheckNoChanged();
        partial void OnbanknameChanging(string value);
        partial void OnbanknameChanged();
        partial void OnTransactionDateChanging(System.Nullable<System.DateTime> value);
        partial void OnTransactionDateChanged();
        partial void OnBookingDateChanging(System.DateTime value);
        partial void OnBookingDateChanged();
        partial void OndepttimeChanging(System.Nullable<System.DateTime> value);
        partial void OndepttimeChanged();
        partial void OnarrtimeChanging(System.Nullable<System.DateTime> value);
        partial void OnarrtimeChanged();
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
        partial void OnBranchcodeChanging(string value);
        partial void OnBranchcodeChanged();
        partial void OnIscancelChanging(System.Nullable<char> value);
        partial void OnIscancelChanged();
        partial void OnOrderIdChanging(string value);
        partial void OnOrderIdChanged();
        partial void OnticketnoChanging(string value);
        partial void OnticketnoChanged();
        partial void OnRemarksChanging(string value);
        partial void OnRemarksChanged();
        partial void OnBookingRoomTypesChanging(string value);
        partial void OnBookingRoomTypesChanged();
        partial void OnStatusChanging(string value);
        partial void OnStatusChanged();
        partial void OnAgentIdChanging(System.Nullable<int> value);
        partial void OnAgentIdChanged();
        partial void OnCancelledChanging(System.Nullable<bool> value);
        partial void OnCancelledChanged();
        partial void OnCancellationAmtChanging(System.Nullable<decimal> value);
        partial void OnCancellationAmtChanged();
        partial void OnRefundAmtChanging(System.Nullable<decimal> value);
        partial void OnRefundAmtChanged();
        partial void OnnoofdaysChanging(System.Nullable<decimal> value);
        partial void OnnoofdaysChanged();
        partial void OnIDSNoChanging(string value);
        partial void OnIDSNoChanged();
        partial void OnIDSStatusChanging(System.Nullable<bool> value);
        partial void OnIDSStatusChanged();
        partial void OnPickUpVehChanging(string value);
        partial void OnPickUpVehChanged();
        partial void OnPickVehNoChanging(string value);
        partial void OnPickVehNoChanged();
        partial void OnPickTimeChanging(string value);
        partial void OnPickTimeChanged();
        partial void OnDropVehChanging(string value);
        partial void OnDropVehChanged();
        partial void OnDropVehNoChanging(string value);
        partial void OnDropVehNoChanged();
        partial void OnDropTimeChanging(string value);
        partial void OnDropTimeChanged();
        partial void OnStationChanging(string value);
        partial void OnStationChanged();
        partial void OnPkStationChanging(string value);
        partial void OnPkStationChanged();
        partial void OnExtraFareChanging(System.Nullable<decimal> value);
        partial void OnExtraFareChanged();
        partial void OnExtraBedChanging(System.Nullable<int> value);
        partial void OnExtraBedChanged();
        partial void OnMasterDiscountChanging(System.Nullable<decimal> value);
        partial void OnMasterDiscountChanged();
        partial void OnHotelIDChanging(System.Nullable<int> value);
        partial void OnHotelIDChanged();
        partial void OnOccupancyChanging(string value);
        partial void OnOccupancyChanged();
        #endregion

        public BranchHotel_Tbl()
        {
            OnCreated();
        }

        public decimal Rowid
        {
            get
            {
                return this._Rowid;
            }
            set
            {
                if ((this._Rowid != value))
                {
                    this.OnRowidChanging(value);
                    this.SendPropertyChanging();
                    this._Rowid = value;
                    this.SendPropertyChanged("Rowid");
                    this.OnRowidChanged();
                }
            }
        }

        public System.Nullable<decimal> CustRowId
        {
            get
            {
                return this._CustRowId;
            }
            set
            {
                if ((this._CustRowId != value))
                {
                    this.OnCustRowIdChanging(value);
                    this.SendPropertyChanging();
                    this._CustRowId = value;
                    this.SendPropertyChanged("CustRowId");
                    this.OnCustRowIdChanged();
                }
            }
        }

        public System.Nullable<decimal> noofadults
        {
            get
            {
                return this._noofadults;
            }
            set
            {
                if ((this._noofadults != value))
                {
                    this.OnnoofadultsChanging(value);
                    this.SendPropertyChanging();
                    this._noofadults = value;
                    this.SendPropertyChanged("noofadults");
                    this.OnnoofadultsChanged();
                }
            }
        }

        public System.Nullable<decimal> noofchildren
        {
            get
            {
                return this._noofchildren;
            }
            set
            {
                if ((this._noofchildren != value))
                {
                    this.OnnoofchildrenChanging(value);
                    this.SendPropertyChanging();
                    this._noofchildren = value;
                    this.SendPropertyChanged("noofchildren");
                    this.OnnoofchildrenChanged();
                }
            }
        }

        public System.Nullable<int> noofrooms
        {
            get
            {
                return this._noofrooms;
            }
            set
            {
                if ((this._noofrooms != value))
                {
                    this.OnnoofroomsChanging(value);
                    this.SendPropertyChanging();
                    this._noofrooms = value;
                    this.SendPropertyChanged("noofrooms");
                    this.OnnoofroomsChanged();
                }
            }
        }

        public System.Nullable<int> Deluxe
        {
            get
            {
                return this._Deluxe;
            }
            set
            {
                if ((this._Deluxe != value))
                {
                    this.OnDeluxeChanging(value);
                    this.SendPropertyChanging();
                    this._Deluxe = value;
                    this.SendPropertyChanged("Deluxe");
                    this.OnDeluxeChanged();
                }
            }
        }

        public System.Nullable<int> SuperDeluxe
        {
            get
            {
                return this._SuperDeluxe;
            }
            set
            {
                if ((this._SuperDeluxe != value))
                {
                    this.OnSuperDeluxeChanging(value);
                    this.SendPropertyChanging();
                    this._SuperDeluxe = value;
                    this.SendPropertyChanged("SuperDeluxe");
                    this.OnSuperDeluxeChanged();
                }
            }
        }

        public System.Nullable<int> Executive
        {
            get
            {
                return this._Executive;
            }
            set
            {
                if ((this._Executive != value))
                {
                    this.OnExecutiveChanging(value);
                    this.SendPropertyChanging();
                    this._Executive = value;
                    this.SendPropertyChanged("Executive");
                    this.OnExecutiveChanged();
                }
            }
        }

        public System.Nullable<int> Royal
        {
            get
            {
                return this._Royal;
            }
            set
            {
                if ((this._Royal != value))
                {
                    this.OnRoyalChanging(value);
                    this.SendPropertyChanging();
                    this._Royal = value;
                    this.SendPropertyChanged("Royal");
                    this.OnRoyalChanged();
                }
            }
        }

        public System.Nullable<decimal> Totalamount
        {
            get
            {
                return this._Totalamount;
            }
            set
            {
                if ((this._Totalamount != value))
                {
                    this.OnTotalamountChanging(value);
                    this.SendPropertyChanging();
                    this._Totalamount = value;
                    this.SendPropertyChanged("Totalamount");
                    this.OnTotalamountChanged();
                }
            }
        }

        public System.Nullable<decimal> STaxValue
        {
            get
            {
                return this._STaxValue;
            }
            set
            {
                if ((this._STaxValue != value))
                {
                    this.OnSTaxValueChanging(value);
                    this.SendPropertyChanging();
                    this._STaxValue = value;
                    this.SendPropertyChanged("STaxValue");
                    this.OnSTaxValueChanged();
                }
            }
        }

        public System.Nullable<decimal> AmtWithTax
        {
            get
            {
                return this._AmtWithTax;
            }
            set
            {
                if ((this._AmtWithTax != value))
                {
                    this.OnAmtWithTaxChanging(value);
                    this.SendPropertyChanging();
                    this._AmtWithTax = value;
                    this.SendPropertyChanged("AmtWithTax");
                    this.OnAmtWithTaxChanged();
                }
            }
        }

        public System.Nullable<decimal> Discount
        {
            get
            {
                return this._Discount;
            }
            set
            {
                if ((this._Discount != value))
                {
                    this.OnDiscountChanging(value);
                    this.SendPropertyChanging();
                    this._Discount = value;
                    this.SendPropertyChanged("Discount");
                    this.OnDiscountChanged();
                }
            }
        }

        public System.Nullable<decimal> Advance
        {
            get
            {
                return this._Advance;
            }
            set
            {
                if ((this._Advance != value))
                {
                    this.OnAdvanceChanging(value);
                    this.SendPropertyChanging();
                    this._Advance = value;
                    this.SendPropertyChanged("Advance");
                    this.OnAdvanceChanged();
                }
            }
        }

        public string Paymode
        {
            get
            {
                return this._Paymode;
            }
            set
            {
                if ((this._Paymode != value))
                {
                    this.OnPaymodeChanging(value);
                    this.SendPropertyChanging();
                    this._Paymode = value;
                    this.SendPropertyChanged("Paymode");
                    this.OnPaymodeChanged();
                }
            }
        }

        public string CheckNo
        {
            get
            {
                return this._CheckNo;
            }
            set
            {
                if ((this._CheckNo != value))
                {
                    this.OnCheckNoChanging(value);
                    this.SendPropertyChanging();
                    this._CheckNo = value;
                    this.SendPropertyChanged("CheckNo");
                    this.OnCheckNoChanged();
                }
            }
        }

        public string bankname
        {
            get
            {
                return this._bankname;
            }
            set
            {
                if ((this._bankname != value))
                {
                    this.OnbanknameChanging(value);
                    this.SendPropertyChanging();
                    this._bankname = value;
                    this.SendPropertyChanged("bankname");
                    this.OnbanknameChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> TransactionDate
        {
            get
            {
                return this._TransactionDate;
            }
            set
            {
                if ((this._TransactionDate != value))
                {
                    this.OnTransactionDateChanging(value);
                    this.SendPropertyChanging();
                    this._TransactionDate = value;
                    this.SendPropertyChanged("TransactionDate");
                    this.OnTransactionDateChanged();
                }
            }
        }

        public System.DateTime BookingDate
        {
            get
            {
                return this._BookingDate;
            }
            set
            {
                if ((this._BookingDate != value))
                {
                    this.OnBookingDateChanging(value);
                    this.SendPropertyChanging();
                    this._BookingDate = value;
                    this.SendPropertyChanged("BookingDate");
                    this.OnBookingDateChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> depttime
        {
            get
            {
                return this._depttime;
            }
            set
            {
                if ((this._depttime != value))
                {
                    this.OndepttimeChanging(value);
                    this.SendPropertyChanging();
                    this._depttime = value;
                    this.SendPropertyChanged("depttime");
                    this.OndepttimeChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> arrtime
        {
            get
            {
                return this._arrtime;
            }
            set
            {
                if ((this._arrtime != value))
                {
                    this.OnarrtimeChanging(value);
                    this.SendPropertyChanging();
                    this._arrtime = value;
                    this.SendPropertyChanged("arrtime");
                    this.OnarrtimeChanged();
                }
            }
        }

        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                if ((this._Username != value))
                {
                    this.OnUsernameChanging(value);
                    this.SendPropertyChanging();
                    this._Username = value;
                    this.SendPropertyChanged("Username");
                    this.OnUsernameChanged();
                }
            }
        }

        public string Branchcode
        {
            get
            {
                return this._Branchcode;
            }
            set
            {
                if ((this._Branchcode != value))
                {
                    this.OnBranchcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Branchcode = value;
                    this.SendPropertyChanged("Branchcode");
                    this.OnBranchcodeChanged();
                }
            }
        }

        public System.Nullable<char> Iscancel
        {
            get
            {
                return this._Iscancel;
            }
            set
            {
                if ((this._Iscancel != value))
                {
                    this.OnIscancelChanging(value);
                    this.SendPropertyChanging();
                    this._Iscancel = value;
                    this.SendPropertyChanged("Iscancel");
                    this.OnIscancelChanged();
                }
            }
        }

        public string OrderId
        {
            get
            {
                return this._OrderId;
            }
            set
            {
                if ((this._OrderId != value))
                {
                    this.OnOrderIdChanging(value);
                    this.SendPropertyChanging();
                    this._OrderId = value;
                    this.SendPropertyChanged("OrderId");
                    this.OnOrderIdChanged();
                }
            }
        }

        public string ticketno
        {
            get
            {
                return this._ticketno;
            }
            set
            {
                if ((this._ticketno != value))
                {
                    this.OnticketnoChanging(value);
                    this.SendPropertyChanging();
                    this._ticketno = value;
                    this.SendPropertyChanged("ticketno");
                    this.OnticketnoChanged();
                }
            }
        }

        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                if ((this._Remarks != value))
                {
                    this.OnRemarksChanging(value);
                    this.SendPropertyChanging();
                    this._Remarks = value;
                    this.SendPropertyChanged("Remarks");
                    this.OnRemarksChanged();
                }
            }
        }

        public string BookingRoomTypes
        {
            get
            {
                return this._BookingRoomTypes;
            }
            set
            {
                if ((this._BookingRoomTypes != value))
                {
                    this.OnBookingRoomTypesChanging(value);
                    this.SendPropertyChanging();
                    this._BookingRoomTypes = value;
                    this.SendPropertyChanged("BookingRoomTypes");
                    this.OnBookingRoomTypesChanged();
                }
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this.OnStatusChanging(value);
                    this.SendPropertyChanging();
                    this._Status = value;
                    this.SendPropertyChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }

        public System.Nullable<int> AgentId
        {
            get
            {
                return this._AgentId;
            }
            set
            {
                if ((this._AgentId != value))
                {
                    this.OnAgentIdChanging(value);
                    this.SendPropertyChanging();
                    this._AgentId = value;
                    this.SendPropertyChanged("AgentId");
                    this.OnAgentIdChanged();
                }
            }
        }

        public System.Nullable<bool> Cancelled
        {
            get
            {
                return this._Cancelled;
            }
            set
            {
                if ((this._Cancelled != value))
                {
                    this.OnCancelledChanging(value);
                    this.SendPropertyChanging();
                    this._Cancelled = value;
                    this.SendPropertyChanged("Cancelled");
                    this.OnCancelledChanged();
                }
            }
        }

        public System.Nullable<decimal> CancellationAmt
        {
            get
            {
                return this._CancellationAmt;
            }
            set
            {
                if ((this._CancellationAmt != value))
                {
                    this.OnCancellationAmtChanging(value);
                    this.SendPropertyChanging();
                    this._CancellationAmt = value;
                    this.SendPropertyChanged("CancellationAmt");
                    this.OnCancellationAmtChanged();
                }
            }
        }

        public System.Nullable<decimal> RefundAmt
        {
            get
            {
                return this._RefundAmt;
            }
            set
            {
                if ((this._RefundAmt != value))
                {
                    this.OnRefundAmtChanging(value);
                    this.SendPropertyChanging();
                    this._RefundAmt = value;
                    this.SendPropertyChanged("RefundAmt");
                    this.OnRefundAmtChanged();
                }
            }
        }

        public System.Nullable<decimal> noofdays
        {
            get
            {
                return this._noofdays;
            }
            set
            {
                if ((this._noofdays != value))
                {
                    this.OnnoofdaysChanging(value);
                    this.SendPropertyChanging();
                    this._noofdays = value;
                    this.SendPropertyChanged("noofdays");
                    this.OnnoofdaysChanged();
                }
            }
        }

        public string IDSNo
        {
            get
            {
                return this._IDSNo;
            }
            set
            {
                if ((this._IDSNo != value))
                {
                    this.OnIDSNoChanging(value);
                    this.SendPropertyChanging();
                    this._IDSNo = value;
                    this.SendPropertyChanged("IDSNo");
                    this.OnIDSNoChanged();
                }
            }
        }

        public System.Nullable<bool> IDSStatus
        {
            get
            {
                return this._IDSStatus;
            }
            set
            {
                if ((this._IDSStatus != value))
                {
                    this.OnIDSStatusChanging(value);
                    this.SendPropertyChanging();
                    this._IDSStatus = value;
                    this.SendPropertyChanged("IDSStatus");
                    this.OnIDSStatusChanged();
                }
            }
        }

        public string PickUpVeh
        {
            get
            {
                return this._PickUpVeh;
            }
            set
            {
                if ((this._PickUpVeh != value))
                {
                    this.OnPickUpVehChanging(value);
                    this.SendPropertyChanging();
                    this._PickUpVeh = value;
                    this.SendPropertyChanged("PickUpVeh");
                    this.OnPickUpVehChanged();
                }
            }
        }

        public string PickVehNo
        {
            get
            {
                return this._PickVehNo;
            }
            set
            {
                if ((this._PickVehNo != value))
                {
                    this.OnPickVehNoChanging(value);
                    this.SendPropertyChanging();
                    this._PickVehNo = value;
                    this.SendPropertyChanged("PickVehNo");
                    this.OnPickVehNoChanged();
                }
            }
        }

        public string PickTime
        {
            get
            {
                return this._PickTime;
            }
            set
            {
                if ((this._PickTime != value))
                {
                    this.OnPickTimeChanging(value);
                    this.SendPropertyChanging();
                    this._PickTime = value;
                    this.SendPropertyChanged("PickTime");
                    this.OnPickTimeChanged();
                }
            }
        }

        public string DropVeh
        {
            get
            {
                return this._DropVeh;
            }
            set
            {
                if ((this._DropVeh != value))
                {
                    this.OnDropVehChanging(value);
                    this.SendPropertyChanging();
                    this._DropVeh = value;
                    this.SendPropertyChanged("DropVeh");
                    this.OnDropVehChanged();
                }
            }
        }

        public string DropVehNo
        {
            get
            {
                return this._DropVehNo;
            }
            set
            {
                if ((this._DropVehNo != value))
                {
                    this.OnDropVehNoChanging(value);
                    this.SendPropertyChanging();
                    this._DropVehNo = value;
                    this.SendPropertyChanged("DropVehNo");
                    this.OnDropVehNoChanged();
                }
            }
        }

        public string DropTime
        {
            get
            {
                return this._DropTime;
            }
            set
            {
                if ((this._DropTime != value))
                {
                    this.OnDropTimeChanging(value);
                    this.SendPropertyChanging();
                    this._DropTime = value;
                    this.SendPropertyChanged("DropTime");
                    this.OnDropTimeChanged();
                }
            }
        }

        public string Station
        {
            get
            {
                return this._Station;
            }
            set
            {
                if ((this._Station != value))
                {
                    this.OnStationChanging(value);
                    this.SendPropertyChanging();
                    this._Station = value;
                    this.SendPropertyChanged("Station");
                    this.OnStationChanged();
                }
            }
        }

        public string PkStation
        {
            get
            {
                return this._PkStation;
            }
            set
            {
                if ((this._PkStation != value))
                {
                    this.OnPkStationChanging(value);
                    this.SendPropertyChanging();
                    this._PkStation = value;
                    this.SendPropertyChanged("PkStation");
                    this.OnPkStationChanged();
                }
            }
        }


        public System.Nullable<decimal> ExtraFare
        {
            get
            {
                return this._ExtraFare;
            }
            set
            {
                if ((this._ExtraFare != value))
                {
                    this.OnExtraFareChanging(value);
                    this.SendPropertyChanging();
                    this._ExtraFare = value;
                    this.SendPropertyChanged("ExtraFare");
                    this.OnExtraFareChanged();
                }
            }
        }

        public System.Nullable<int> ExtraBed
        {
            get
            {
                return this._ExtraBed;
            }
            set
            {
                if ((this._ExtraBed != value))
                {
                    this.OnExtraBedChanging(value);
                    this.SendPropertyChanging();
                    this._ExtraBed = value;
                    this.SendPropertyChanged("ExtraBed");
                    this.OnExtraBedChanged();
                }
            }
        }

        public System.Nullable<decimal> MasterDiscount
        {
            get
            {
                return this._MasterDiscount;
            }
            set
            {
                if ((this._MasterDiscount != value))
                {
                    this.OnMasterDiscountChanging(value);
                    this.SendPropertyChanging();
                    this._MasterDiscount = value;
                    this.SendPropertyChanged("MasterDiscount");
                    this.OnMasterDiscountChanged();
                }
            }
        }

        public System.Nullable<int> HotelID
        {
            get
            {
                return this._HotelID;
            }
            set
            {
                if ((this._HotelID != value))
                {
                    this.OnHotelIDChanging(value);
                    this.SendPropertyChanging();
                    this._HotelID = value;
                    this.SendPropertyChanged("HotelID");
                    this.OnHotelIDChanged();
                }
            }
        }
        public string Occupancy
        {
            get
            {
                return this._Occupancy;
            }
            set
            {
                if ((this._Occupancy != value))
                {
                    this.OnOccupancyChanging(value);
                    this.SendPropertyChanging();
                    this._Occupancy = value;
                    this.SendPropertyChanged("Occupancy");
                    this.OnOccupancyChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}