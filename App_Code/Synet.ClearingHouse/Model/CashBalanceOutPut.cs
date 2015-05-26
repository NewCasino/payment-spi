using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("cashbalance-response")]
    public class CashbalanceOutPut
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _returnCode;

        #endregion
        
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public CashbalanceOutPut()
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

     
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(CashbalanceOutPut)))
            {
                CashbalanceOutPut casBalanceObj = (CashbalanceOutPut) obj;
                return (this.returnCode == casBalanceObj.returnCode);
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
