using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("currencyinfo")]
    public class InvCurrencyInfo
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		private CurrencyInfo currInfo;
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public InvCurrencyInfo()
		{
		}
		
		#endregion
		
		#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		[XmlElement("returncode")]
		public string ReturnCode
		{
			get { return _returnCode; }
			set { _returnCode = value; }
		}

        //[XmlArray("CurrencyList")]
        [XmlElement("currency")]
        public CurrencyInfo CurrInfo
		{
            get { return currInfo; }
            set { currInfo = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
            if (obj is InvCurrencyInfo)
		    {
                InvCurrencyInfo castobj = (InvCurrencyInfo)obj;
		        return (this._returnCode == castobj.ReturnCode &&
                        this.currInfo != null &&
                        this.currInfo.Equals(castobj.CurrInfo));
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
