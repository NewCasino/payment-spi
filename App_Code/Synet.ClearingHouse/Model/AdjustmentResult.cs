/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/19/2007
 * Time: 11:20 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Xml.Serialization;

namespace Synet.ClearingHouse.Model
{
    [XmlRoot("adjustment-response")]
	public class AdjustmentResult
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (11)

        private string _returnCode;
        private int _transID;

        #endregion
               
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        public AdjustmentResult()
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

        [XmlElement("trxnid")]
        public int transID
        {
            get { return _transID; }
            set { _transID = value; }
        }

        #endregion

        #region  METHODS : OVERRIDEN >>>>>>>>>>>>>>>>>>>>>>>> (2)

        public override bool Equals(object obj)
        {
            if (obj is AdjustmentResult)
            {
                AdjustmentResult adjResult = (AdjustmentResult)obj;
                return (this.returnCode == adjResult.returnCode && 
                        this.transID == adjResult.transID);
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
