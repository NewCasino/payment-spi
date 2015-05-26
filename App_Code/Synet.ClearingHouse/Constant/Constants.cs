using System;

namespace Synet.ClearingHouse.Constant
{
    /// <summary>
    /// Description of Constants.
    /// </summary>
    public class Constants
    {
    	#region  FIELDS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>>> (13)
    	
    	// 	Transfer mode
    	public static readonly int DEPOSIT					= 100;
		public static readonly int WITHDRAWAL    			= 110;
		
        public static readonly int DEPOSIT_ADJUSTMENT       = 102;
		public static readonly int WITHDRAWAL_ADJUSTMENT    = 112;

        public static readonly int WITHDRAWAL_PENDING       = 113;
        public static readonly int WITHDRAWAL_REJECTION     = 114;
		#endregion 

		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public Constants()
        {
        }
		
		#endregion 

		#region  METHODS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (6)

		#endregion 
    }
}
