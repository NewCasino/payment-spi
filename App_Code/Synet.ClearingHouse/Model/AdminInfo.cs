/*
 * Created by SharpDevelop.
 * User: Thuynnx
 * Date: 10/25/2007
 * Time: 8:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("admininfo")]
	public class AdminInfo
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (3)

		private string _returnCode;
		private string _adminname;
		private int _admintypeid;

		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public AdminInfo()
		{
		}
		
		#endregion
		
		#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (3)

		[XmlElement("returncode")]
		public virtual string returnCode
		{
			get { return _returnCode; }
			set { _returnCode = value; }
		}
		
		[XmlElement("adminname")]
		public virtual string adminname
		{
			get { return _adminname; }
			set { _adminname = value; }
		}
		
		[XmlElement("admintypeid")]
		public virtual int admintypeid
		{
			get { return _admintypeid; }
			set { _admintypeid = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (1)

		public override bool Equals(object obj)
		{
		    if(obj.GetType().Equals(typeof(AdminInfo)))
		    {
		    	AdminInfo memObj = (AdminInfo) obj;
		        return (this.returnCode == memObj.returnCode && 
		    	        this.adminname == memObj.adminname &&
		    	       	this.admintypeid == memObj.admintypeid  );
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
