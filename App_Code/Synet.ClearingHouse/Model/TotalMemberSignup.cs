using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("totalsignupinfo")]
    public class TotalMemberSignup
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		private List<MemsPerMonth> _currentMonthList = new List<MemsPerMonth>();
		private List<MemsPerMonth> _toDateList = new List<MemsPerMonth>();
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public TotalMemberSignup()
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

        //[XmlArray("currentmonthlist")]
        [XmlElement("currentmonth")]        
        public List<MemsPerMonth> currentMonthList
		{
            get { return _currentMonthList; }
            set { _currentMonthList = value; }
		}
		
        //[XmlArray("todatelist")]
        [XmlElement("todate")]
        public List<MemsPerMonth> toDateList
		{
            get { return _toDateList; }
            set { _toDateList = value; }
		}
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
		    if(obj is TotalMemberSignup)
		    {
		    	TotalMemberSignup totalMemberObj = (TotalMemberSignup) obj;
		        return (this.returnCode == totalMemberObj.returnCode &&
                        this.currentMonthList.Equals(totalMemberObj.currentMonthList));
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
