using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("currency")]
    public class CurrencyInfo
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _isoCode;
		private double _exchgRate;
        private int _currBase;
        private int _currUnit;        
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public CurrencyInfo()
		{
		}
		
		#endregion
		
		#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        [XmlAttribute("isocode")]
        public string isoCode
        {
            get { return _isoCode; }
            set { _isoCode = value; }
        }

        [XmlElement("exchgrate")]
        public double exchgRate
		{
            get { return _exchgRate; }
            set { _exchgRate = value; }
		}

        [XmlElement("base")]
        public int currBase
		{
            get { return _currBase; }
            set { _currBase = value; }
		}

        [XmlElement("currunit")]
        public int currUnit
		{
            get { return _currUnit; }
            set { _currUnit = value; }
		}

		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
            if (obj is CurrencyInfo)
		    {
                CurrencyInfo currObj = (CurrencyInfo)obj;
                return (this.isoCode == currObj.isoCode &&
                        this.exchgRate == currObj.exchgRate &&
                        this.currBase == currObj.currBase &&
                        this.currUnit == currObj.currUnit);
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
