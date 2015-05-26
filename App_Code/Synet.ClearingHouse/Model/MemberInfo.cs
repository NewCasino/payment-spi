/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/17/2007
 * Time: 8:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("memberinfo")]
	public class MemberInfo
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		private string _membercode;
		private int _userid;
		private string _curr;
		private string _email;		
		private string _websiteName;
		private double _balance;		
		private string _vendorid;		
		private string _country;
		private string _fname;
		private string _lname;
		private int _verifyid;
		private string _dtupdatedmember;
		private string _dtupdatedadmin;		
		private string _mobileno;
		private string _active;
		private int _ccverifyid;

		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public MemberInfo()
		{
		}
		
		#endregion
		
		#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		[XmlElement("returncode")]
		public virtual string returnCode
		{
			get { return _returnCode; }
			set { _returnCode = value; }
		}
		[XmlElement("membercode")]
		public virtual string memberCode
		{
			get { return _membercode; }
			set { _membercode = value; }
		}
		[XmlElement("userid")]
		public virtual int userID
		{
			get { return _userid; }
			set { _userid = value; }
		}
		[XmlElement("curr")]
		public virtual string currCode
		{
			get { return _curr; }
			set { _curr = value; }
		}
		[XmlElement("email")]
		public virtual string email
		{
			get { return _email; }
			set { _email = value; }
		}
		[XmlElement("websitename")]
		public virtual string websiteName
		{
			get { return _websiteName; }
			set { _websiteName = value; }
		}
		[XmlElement("balance")]
		public virtual double balance
		{
			get { return _balance; }
			set { _balance = value; }
		}
		[XmlElement("vendorid")]
		public virtual string vendorID
		{
			get { return _vendorid; }
			set { _vendorid = value; }
		}
		[XmlElement("country")]
		public virtual string country
		{
			get { return _country; }
			set { _country = value; }
		}
		[XmlElement("firstname")]
		public virtual string firstname
		{
			get { return _fname; }
			set { _fname = value; }
		}
		[XmlElement("lastname")]
		public virtual string lastname
		{
			get { return _lname; }
			set { _lname = value; }
		}
		[XmlElement("verifyid")]
		public virtual int verifyID
		{
			get { return _verifyid; }
			set { _verifyid = value; }
		}
		[XmlElement("dtupdatedmember")]
		public virtual string dtupdatedmember
		{
			get { return _dtupdatedmember; }
			set { _dtupdatedmember = value; }
		}
		[XmlElement("dtupdatedadmin")]
		public virtual string dtupdatedadmin
		{
			get { return _dtupdatedadmin; }
			set { _dtupdatedadmin = value; }
		}
		[XmlElement("mobileno")]
		public virtual string mobileno {
			get { return _mobileno; }
			set { _mobileno = value; }
		}
		[XmlElement("active")]
		public virtual string active
		{
		    get { return this._active; }
		    set { this._active = value;}
		}
		[XmlElement("ccverifyid")]
		public virtual int ccverifyid
		{
		    get { return this._ccverifyid; }
		    set { this._ccverifyid = value;}
		}
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
		    if(obj.GetType().Equals(typeof(MemberInfo)))
		    {
		    	MemberInfo memObj = (MemberInfo) obj;
		        return (this.returnCode == memObj.returnCode && 
		    	        this.memberCode == memObj.memberCode &&
		    	       	this.userID == memObj.userID &&
		    	       	this.email == memObj.email &&
		    	       	this.currCode == memObj.currCode &&
		    	       	this.balance == memObj.balance &&
		    	       	this.websiteName == memObj.websiteName &&
		    	       	this.vendorID == memObj.vendorID &&
		    	       	this.country == memObj.country &&
		    	       	this.firstname == memObj.firstname &&
		    	       	this.lastname == memObj.lastname &&
		    	       	this.verifyID == memObj.verifyID &&
		    	       	this.dtupdatedmember == memObj.dtupdatedmember &&
		    	       	this.dtupdatedadmin == memObj.dtupdatedadmin &&
		    	       	this.mobileno == memObj.mobileno &&
		    	       	this.active == memObj.active); 
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
