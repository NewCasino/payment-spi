using System;
using Synet.Common.Exceptions;

namespace Synet.ClearingHouse.Exceptions
{
	/// <summary>
	/// Description of ClearingHouseException.
	/// </summary>
	public class ClearingHouseException: GenericException
	{
        
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (3)

		public ClearingHouseException(String errorCode, String errorMsg, Exception ex) : base(errorCode, errorMsg, ex)
        {
            this._strErrorCode = errorCode;
            this._strErrorMsg = errorMsg;
        }
		
		public ClearingHouseException(String errorCode, String errorMsg) : base(errorCode, errorMsg)
        {
            this._strErrorCode = errorCode;
            this._strErrorMsg = errorMsg;
        }
		
		public ClearingHouseException(){ }
		
		#endregion 

	}
}
