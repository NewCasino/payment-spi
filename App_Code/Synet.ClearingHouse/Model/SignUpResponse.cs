using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("membersignupinfo")]
    public class SignUpResponse
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

		private string _returnCode;
		private List<string> _memberList = new List<string>();
		
		#endregion 
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public SignUpResponse()
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

        //[XmlArray("memberlist")]
        [XmlElement("membercode")]
        public List<string> memberList
		{
            get { return _memberList; }
            set { _memberList = value; }
		}
		
		#endregion
		
		#region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

		public override bool Equals(object obj)
		{
		    if(obj is SignUpResponse)
		    {
		    	SignUpResponse signUpResponse = (SignUpResponse) obj;
		        return (this.returnCode == signUpResponse.returnCode &&
                        this.memberList.Equals(signUpResponse.memberList));
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
