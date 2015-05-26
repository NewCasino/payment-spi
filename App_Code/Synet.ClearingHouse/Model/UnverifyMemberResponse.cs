using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("member-unverified-response")]
    public class UnverifyMemberResponse
    {
        private string _returnCode;
        
        public UnverifyMemberResponse()
        {
        }
        
        [XmlElement("returncode")]
        public string returnCode
        {
            get { return _returnCode; }
            set { _returnCode = value; }
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(UnverifyMemberResponse)))
            {
                UnverifyMemberResponse unVerifyResponseObj = (UnverifyMemberResponse)obj;
                return (this._returnCode == unVerifyResponseObj._returnCode);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
