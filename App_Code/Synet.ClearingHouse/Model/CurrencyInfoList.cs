using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("currencyinfo")]
    public class CurrencyInfoList
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		private List<CurrencyInfo> _currencyList = new List<CurrencyInfo>();
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public CurrencyInfoList()
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

        //[XmlArray("CurrencyList")]
        [XmlElement("currency")]
        public List<CurrencyInfo> currencyList
		{
            get { return _currencyList; }
            set { _currencyList = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
//            if (obj is CurrencyInfoList)
//		    {
//                CurrencyInfoList list = (CurrencyInfoList)obj;
//		        return (this.returnCode == list.returnCode &&
//                        this.currencyList.Equals(list.currencyList));
//		    }
		    return false;
		}
		
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
		#endregion
    }
}
