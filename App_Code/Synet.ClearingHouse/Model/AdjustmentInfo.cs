/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/19/2007
 * Time: 11:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("adjustment-request")]
	public class AdjustmentInfo
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private int _refNo;
		private string _adminCode;
		private string _memberCode;
		private string _currCode;
		private int _currUnit;
		private double _exchgRate;
		private int _purposeID;
		private int _creditDebit;
		private double _amount;		
		private string _description;
		private int _status;
		private string _trxnid;	
		private string _admupdate;
		private string _upddate;	
		private string _trxndate;

		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public AdjustmentInfo()
		{
		}
		
		#endregion
				
        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        [XmlElement("refno")]
        public int refNo
        {
            get { return _refNo; }
            set { _refNo = value; }
        }

        [XmlElement("admincode")]
        public string adminCode
        {
            get { return _adminCode; }
            set { _adminCode = value; }
        }
        
        [XmlElement("membercode")]
        public string memberCode
        {
            get { return _memberCode; }
            set { _memberCode = value; }
        }
        
        [XmlElement("currcode")]
        public string currCode
        {
            get { return _currCode; }
            set { _currCode = value; }
        }
        
        [XmlElement("currunit")]
        public int currUnit
		{
            get { return _currUnit; }
            set { _currUnit = value; }
		}

        [XmlElement("exchgrate")]
        public double exchgRate
		{
            get { return _exchgRate; }
            set { _exchgRate = value; }
		}
        
        [XmlElement("purposeid")]
        public int purposeID
        {
            get { return _purposeID; }
            set { _purposeID = value; }
        }
        
        [XmlElement("creditdebit")]
        public int creditDebit
        {
            get { return _creditDebit; }
            set { _creditDebit = value; }
        }
        
        [XmlElement("amount")]
        public double amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        
        [XmlElement("description")]
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        [XmlElement("status")]
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        
        [XmlElement("trxnid")]
        public string trxnid
        {
            get { return _trxnid; }
            set { _trxnid = value; }
        }
        
        [XmlElement("trxndate")]
        public string trxndate
        {
            get { return _trxndate; }
            set { _trxndate = value; }
        }

        [XmlElement("admupdate")]
        public string admupdate
        {
            get { return _admupdate; }
            set { _admupdate = value; }
        }
        
        [XmlElement("upddate")]
        public string upddate
        {
            get { return _upddate; }
            set { _upddate = value; }
        }
        
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj is AdjustmentInfo)
            {
                AdjustmentInfo adjInfo = (AdjustmentInfo)obj;
                return (this.refNo == adjInfo.refNo && 
		    	        this.adminCode == adjInfo.adminCode &&
		    	       	this.memberCode == adjInfo.memberCode &&
		    	       	this.currCode == adjInfo.currCode &&
		    	       	this.purposeID == adjInfo.purposeID &&
		    	       	this.creditDebit == adjInfo.creditDebit &&
		    	       	this.amount == adjInfo.amount &&
		    	       	this.description == adjInfo.description &&
		    	       	this.status == adjInfo.status &&
		    	       	this.trxnid == adjInfo.trxnid &&
		    	       	this.trxndate == adjInfo.trxndate &&
		    	       	this.upddate == adjInfo.upddate);
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
