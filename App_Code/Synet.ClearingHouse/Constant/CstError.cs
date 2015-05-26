using System;

namespace Synet.ClearingHouse.Constant
{
    /// <summary>
    /// Description of CstError.
    /// </summary>
    public class CstError : Synet.Common.Constant.CstError
    {

        #region  FIELDS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>>> (5)

        public new static readonly string GENERAL_SUCCESS			    = "000";
        public new static readonly string GENERAL_ERROR         	    = "111";
    
        public new static readonly string DB_CONNECTION_ERROR		    = "100";
        public new static readonly string DB_OBJECT_NOT_FOUND		    = "101";
        public     static readonly string DB_OBJECT_ALREADY_EXIST	    = "102";

        public 	   static readonly string INVALID_NULL_DATA			    = "001";
        public     static readonly string MEMBER_NOT_FOUND			    = "002";
        public     static readonly string MEMBER_INACTIVE			    = "003";
        public     static readonly string MEMBER_ZERO_BALANCE		    = "004";
        public     static readonly string MEMBER_NOT_ENOUGH_BALANCE	    = "005";
        public     static readonly string MEMBER_ZERO_CHANGE_AMOUNT     = "006";
        public     static readonly string MEMBER_NOT_ENOUGH_BALANCE_PWL = "007";
        public     static readonly string MEMBER_WS_NOT_FOUND           = "008";
        public     static readonly string LOGID_ALREADY_EXISTS          = "010";
        public     static readonly string NO_LOGID                      = "011";
        public     static readonly string LOGID_ALREADY_EXISTS_PWL      = "012";
        

        //NEW CONSTANTS
        public static readonly string SUCCESS                   = "000";
        public static readonly string SUC_DUP_SUBMIT            = "001";
        public static readonly string SUC_NO_SUBMISSION_FOUND   = "002";

        public static readonly string ERROR                     = "110";
        public static readonly string ERR_NO_CHG_AMT            = "111";
        public static readonly string ERR_MEM_NOT_FOUND         = "112";
        public static readonly string ERR_MEM_INACTIVE          = "113";
        public static readonly string ERR_NO_LODID              = "114";
        public static readonly string ERR_VAL_BAL_INSUF        = "115";
        public static readonly string ERR_WD_BAL_INSUF          = "116";
        public static readonly string ERR_NO_SUBMISSION_FOUND   = "117";
        public static readonly string ERR_TRANS_REJECTED        = "118";
        public static readonly string ERR_TRANS_APPROVED        = "119";
        #endregion

    }
}
