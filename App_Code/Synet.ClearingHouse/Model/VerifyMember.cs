using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Synet.ClearingHouse.Model
{
    [XmlRoot("verifymember")]
    public class VerifyMember
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _credential;

        #endregion 

        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public VerifyMember()
		{
		}
		
		#endregion

        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)
        [XmlElement("credential")]
        public string credential
        {
            get { return _credential; }
            set { _credential = value; }
        }
        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj is VerifyMember)
            {
                VerifyMember veriMemberObj = (VerifyMember)obj;
                return (this.credential == veriMemberObj.credential);
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
