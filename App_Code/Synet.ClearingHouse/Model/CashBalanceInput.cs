using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("cashbalance-request")]
    public class CashBalanceInput
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _oucode;
        private string _adminCode;
        private double _changeAmount;
        private string _refNo;
        private string _trxnType;
        private string _paymenttypedesc;
        #endregion

        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public CashBalanceInput()
        {
        }

        #endregion
        
        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
	
        [XmlElement("oucode")]
        public string oucode
        {
            get { return _oucode; }
            set { _oucode = value; }
        }
        [XmlElement("admincode")]
        public string adminCode
        {
            get { return _adminCode; }
            set { _adminCode = value; }
        }
        [XmlElement("changeamount")]
        public double changeAmount
        {
            get { return _changeAmount; }
            set { _changeAmount = value; }
        }
        [XmlElement("refno")]
        public string refNo
        {
            get { return _refNo; }
            set { _refNo = value; }
        }
        [XmlElement("trxntype")]
        public string trxnType
        {
            get { return _trxnType; }
            set { _trxnType = value; }
        }
        [XmlElement("paymenttype")]
        public string paymenttype
        {
            get { return _paymenttypedesc; }
            set { _paymenttypedesc = value; }
        }
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(CashBalanceInput)))
            {
                CashBalanceInput casBalanceObj = (CashBalanceInput)obj;
                return (this.oucode == casBalanceObj.oucode
                            && this.adminCode == casBalanceObj.adminCode
                            && this.changeAmount == casBalanceObj.changeAmount
                            && this.refNo == casBalanceObj._refNo
                            && this.trxnType == casBalanceObj.trxnType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion 
    }
}
