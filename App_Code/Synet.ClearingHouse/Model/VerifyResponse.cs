using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
	[XmlRoot("docverifyinfo")]
    public class VerifyResponse
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _returnCode;
        private bool _verifyStatus;

        #endregion

        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public VerifyResponse()
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

        [XmlElement("verifystatus")]
        public bool verifyStatus
        {
            get { return _verifyStatus; }
            set { _verifyStatus = value; }
        }

        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(VerifyResponse)))
            {
                VerifyResponse verifyObj = (VerifyResponse)obj;
                return (this._returnCode == verifyObj._returnCode && 
                        this._verifyStatus == verifyObj._verifyStatus);
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
