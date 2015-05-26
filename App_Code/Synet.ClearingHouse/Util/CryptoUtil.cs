/*
 * Created by SharpDevelop.
 * User: xuantt
 * Date: 11/7/2007
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

using objCrypto;

using Synet.Common.Logs;

namespace Synet.ClearingHouse.Util
{
	/// <summary>
	/// Description of CryptoUtil.
	/// </summary>
	public class CryptoUtil
	{
		#region  FIELDS : CONSTANT >>>>>>>>>>>>>>>>>>>>>>>>>> (2)
		const int USER_CODE = 0;
		const int LANGUAGE = 1;
		const int SESSION_ID	= 2;
		const int CURRENCY_CODE = 3;
		const int USER_ID	= 4;
        #endregion

        #region  FIELDS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        protected static readonly ISysLog Log = SysLog.GetLogger(typeof(CryptoUtil).Name);

        #endregion

        #region  METHODS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (2)

		public static string DoDecrypt(string data, string key)
        {
            clsCryptoClass clsCrypt = new clsCryptoClass();
            clsCrypt.strData = data;
            clsCrypt.let_strKey(key);
            
            return clsCrypt.fncDecrypt();         
        }
        
		public static string DoEncrypt(string data, string key)
        {
            clsCryptoClass clsCrypt = new clsCryptoClass();
            clsCrypt.strData = data;
            clsCrypt.let_strKey(key);
            
            return clsCrypt.fncEncrypt();
        }
		
		public static string GetSessionIDFromKey(string txtkey, string key)
        {
            string sessionID = null;
            string strDecrypted = DoDecrypt(txtkey, key);            
            if (strDecrypted != null)
	        {
	            string[] array = strDecrypted.Split('^');
	            for (int i = 0; i < array.Length; i++)
	            {
	                switch (i)
	                {
//	                    case USER_CODE:	                        
//	                        break;
//	                    case LANGUAGE:	                        
//	                        break;
	                    case SESSION_ID:
	                        sessionID = array[2];
	                        break;
//	                    case CURRENCY_CODE:	                        
//	                        break;
//	                    case USER_ID:	                        
//	                        break;
	                }
	            }
	        }
            return sessionID;
        }
		
        #endregion
	}
}
