using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("member-unverified-request")]
    public class UnverifyMember
    {
        private string _strReason;
        private string _strRemarks;
        
        public UnverifyMember()
        {
        }
        
        [XmlElement("reason")]
        public string reason
        {
            get { return _strReason; }
            set { _strReason = value; }
        }
        
        [XmlElement("remarks")]
        public string remarks
        {
            get { return _strRemarks; }
            set { _strRemarks = value; }
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(UnverifyMember)))
            {
                UnverifyMember unVerifyObj = (UnverifyMember)obj;
                return (this._strReason == unVerifyObj._strReason &&
                        this._strRemarks== unVerifyObj._strRemarks);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
