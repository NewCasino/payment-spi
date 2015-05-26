using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("verifymember-response")]
    public class VerifyMemberResponse
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _returnCode;
        private bool _valid;
      
        #endregion 

        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public VerifyMemberResponse()
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
        [XmlElement("valid")]
        public bool valid
        {
            get { return _valid; }
            set { _valid = value; }
        }       
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj is VerifyMemberResponse)
            {
                VerifyMemberResponse veriMemberResObj = (VerifyMemberResponse)obj;
                return (this.returnCode == veriMemberResObj.returnCode &&
                        this.valid == veriMemberResObj.valid);
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
