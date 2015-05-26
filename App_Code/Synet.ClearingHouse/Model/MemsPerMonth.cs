using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("month")]
	public class MemsPerMonth
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
	
		private string _month;
		private int _memNums;
			
		#endregion 
			
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
	
	    public MemsPerMonth()
		{
		}
			
		#endregion
			
		#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
	
		[XmlAttribute("value")]
		public string month
		{
			get { return _month; }
			set { _month = value; }
		}
		
		[XmlTextAttribute()]
		public int memNums
		{
			get { return _memNums; }
			set { _memNums = value; }
		}
	
	    #endregion
			
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)
	
		public override bool Equals(object obj)
		{
		    if(obj is MemsPerMonth)
		    {
		    	MemsPerMonth memsObj = (MemsPerMonth) obj;
		        return (this.month == memsObj.month &&
	                    this.memNums == memsObj.memNums);
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
