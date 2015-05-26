using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("memberauthinfo")]
    public class AuthToken
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
        private bool _memberauthenticate;
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public AuthToken()
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
		
		[XmlElement("authenticated")]
        public bool Passed
		{
            get { return _memberauthenticate; }
            set { _memberauthenticate = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
            if (obj is AuthToken)
		    {
                AuthToken autokenObj = (AuthToken) obj;
                return (this.ReturnCode == autokenObj.ReturnCode && 
                        this.Passed == autokenObj.Passed);
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
