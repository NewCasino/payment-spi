using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("expire-document-memberlist")]
    public class ExDocMemberList
    {
        private string _returnCode;
        private List<string> _memberList = new List<string>();

        public ExDocMemberList()
        {
        }

        [XmlElement("returncode")]
        public string returnCode
        {
            get { return _returnCode; }
            set { _returnCode = value; }
        }

        [XmlElement("membercode")]
        public List<string> memberList
        {
            get { return _memberList; }
            set { _memberList = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is ExDocMemberList)
            {
                ExDocMemberList exdocMemList = (ExDocMemberList)obj;
                return (this.returnCode == exdocMemList.returnCode &&
                        this.memberList.Equals(exdocMemList.memberList));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
