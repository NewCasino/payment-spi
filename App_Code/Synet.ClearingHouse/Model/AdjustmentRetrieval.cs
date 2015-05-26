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
    [XmlRoot("adjustmentinfo")]
	public class AdjustmentRetrieval
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
		private string _returnCode;
		private int _transID;
		private int _refNo;
		private string _adminCode;
		private string _memberCode;
		private int _creditDebit;
		private double _amount;		
		private string _description;
		private int _status;		

		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public AdjustmentRetrieval()
		{
		}
		
		#endregion
				
        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
		[XmlElement("returncode")]
        public string returnCode
        {
            get { return _returnCode; }
            set { _returnCode = value; }
        }

        [XmlElement("trxnid")]
        public int transID
        {
            get { return _transID; }
            set { _transID = value; }
        }
        
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
        
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj is AdjustmentRetrieval)
            {
                AdjustmentRetrieval adjRetrieval = (AdjustmentRetrieval)obj;
                return (this.returnCode == adjRetrieval.returnCode &&
		    	        this.transID == adjRetrieval.transID &&
                		this.refNo == adjRetrieval.refNo &&
		    	        this.adminCode == adjRetrieval.adminCode &&
		    	       	this.memberCode == adjRetrieval.memberCode &&
		    	       	//this.currCode == adjRetrieval.currCode &&
		    	       	//this.purposeID == adjRetrieval.purposeID &&
		    	       	this.creditDebit == adjRetrieval.creditDebit &&
		    	       	this.amount == adjRetrieval.amount &&
		    	       	this.description == adjRetrieval.description &&
		    	       	this.status == adjRetrieval.status);
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
