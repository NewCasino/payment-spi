/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/18/2007
 * Time: 9:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("churninfo")]
	public class ChurnResponse
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		/* private double _churnamt; */
		private double _totalbet;
		private double _totaldepositadj;
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public ChurnResponse()
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
		
		/* [XmlElement("churnamt")]
		public double churnamt
		{
			get { return _churnamt; }
			set { _churnamt = value; }
		} */
		
		[XmlElement("totalbet")]
		public double totalbet
		{
			get { return _totalbet; }
			set { _totalbet = value; }
		}
		
		[XmlElement("totaldepositadj")]
		public double totaldepositadj
		{
			get { return _totaldepositadj; }
			set { _totaldepositadj = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
		    if(obj.GetType().Equals(typeof(ChurnResponse)))
		    {
		    	ChurnResponse churnObj = (ChurnResponse) obj;
		        return (this._returnCode == churnObj._returnCode && this._totalbet == churnObj._totalbet && this._totaldepositadj == churnObj._totaldepositadj);
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
