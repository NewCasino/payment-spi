/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Synet.Common.Logs;

using Synet.ClearingHouse.Dao;
using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Exceptions;
using Synet.ClearingHouse.Util;

using Synet.Common.Exceptions;

namespace Synet.ClearingHouse.Manager.Impl
{
	/// <summary>
	/// Description of MemberManagerImpl.
	/// </summary>
	public class MemberManagerImpl: Synet.ClearingHouse.Manager.IMemberManager
	{
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        protected static readonly ISysLog Log = SysLog.GetLogger(typeof(MemberManagerImpl).Name);
        private IMemberDAO _memberDAO;
        #endregion 
        
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
        
        public MemberManagerImpl(){}
        
        #endregion 
        
        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
        
        public IMemberDAO MemberDAO
		{
			set { _memberDAO = value; }
		}
        
       	#endregion 
        
        #region  METHODS : PUBLIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (7)
        
		public ChurnResponse GetMemberChurn(string memberCode)
		{
			Log.General("<MemberManagerImpl> : GetMemberChurn(" + memberCode + ")");
			
			if (memberCode == null) {
				ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid User Code!");
				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid User Code!");
				throw ex;
			}
			
			if (!_memberDAO.CheckMemberExist(memberCode))
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	throw ex;
            }
			
			return _memberDAO.GetMemberChurn(memberCode);
		}
		
		public MemberInfo GetMemberInfoPersonal(string memberCode)
        {
			Log.General("<MemberManagerImpl> : GetMemberInfoPersonal(" + memberCode + ")");
			
			int memberID = _memberDAO.GetMemberIdByMemberCode(memberCode);				
            return _memberDAO.GetMemberInfoPersonal(memberID);
        }
		
		public bool GetVerifyDocumentStatus(string memberCode, int docTypeId)
        {
            Log.General("<MemberManagerImpl> : GetVerifyDocumentStatus(" + memberCode + " , " + docTypeId + ")");
            
            int memberID = _memberDAO.GetMemberIdByMemberCode(memberCode);
            
            return _memberDAO.GetVerifyDocumentStatus(memberID, docTypeId);
        }
		
		public AdminInfo GetAdminUserInfo(string adminCode)
        {
			Log.General("<MemberManagerImpl> : GetAdminUserInfo(" + adminCode + ")");
			
			if (adminCode == null) {
				ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid Admin Code!");
				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid Admin Code!");
				throw ex;
			}
			
            return _memberDAO.GetAdminUserInfo(adminCode);
        }
		
		public  SignUpResponse GetListOfMemberSignUp(int month,int year)
        {
            Log.General("<MemberManagerImpl> : GetListOfMemberSignUp(" + month + "," + year + ")");
            
            if (month <= 0 || month > 12 || year <= 0)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.GENERAL_ERROR, "Invalid Date!");
				ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Invalid Date!");
				throw ex;
            }
            
            return _memberDAO.GetListOfMemberSignUp(month, year);
        }

        public AuthToken VerifyAuthenticationToken(string memberCode, string txtkey, string ipAddress, string oucode)
        {
            Log.General("<MemberDAOImpl> : VerifyAuthenticationToken(" + memberCode + "," + txtkey + "," + ipAddress + "," + oucode + ")");
            
            if (memberCode == null) {
				ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid Member Code!");
				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid Member Code!");
				throw ex;
			}
            
            string decryptedSessionID = CryptoUtil.GetSessionIDFromKey(txtkey, _memberDAO.GetSSLKey());
            AuthToken auTokenObj = null;
            if (decryptedSessionID == null)
            {
            	auTokenObj = new AuthToken();
            	auTokenObj.ReturnCode = CstError.GENERAL_SUCCESS;
            	auTokenObj.Passed = false;
            	return auTokenObj;
            }
            
            auTokenObj = _memberDAO.VerifyAuthenticationToken(memberCode, decryptedSessionID, ipAddress, oucode);
            if (auTokenObj == null)
            {
            	auTokenObj = new AuthToken();
            	auTokenObj.ReturnCode = CstError.GENERAL_ERROR;
            	auTokenObj.Passed = false;
            }
            return auTokenObj;
        }
		
        public int VerifyAdminToken(string adminCode, string txtKey, string ipAddress, string ouCode) {
        	string sessionId = CryptoUtil.GetSessionIDFromKey(txtKey, _memberDAO.GetSSLKey());
        	if (string.IsNullOrEmpty(sessionId)) {
        		return 1;
        	}
        	return _memberDAO.VerifyAdminToken(adminCode, sessionId, ipAddress, ouCode);
        }

        public TotalMemberSignup GetTotalMemberSignup(int month, int year)
        {
            Log.General("<MemberManagerImpl> : GetTotalMemberSignup(" + month + ", " + year + ")");
            
            if (month <= 0 || month > 12 || year <= 0)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.GENERAL_ERROR, "Invalid Date!");
				ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Invalid Date!");
				throw ex;
            }
            
            return _memberDAO.GetTotalMemberSignup(month, year);
        }
        
        public VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode, string strPass)
        {
            Log.General("<MemberDAOImpl> : VerifyMemberCredentials(" + memberCode + "," + oucode + ")");
            return _memberDAO.VerifyMemberCredentials(memberCode, oucode, strPass);
        }
        
        public UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger)
        {
            Log.General("<MemberDAOImpl> : SetMemberUnverified(" + strAdminCode + "," + strMemCode + "," + strEventTrigger + ")");
            return _memberDAO.SetMemberUnverified(strAdminCode, strMemCode, strEventTrigger);
        }

        public ExDocMemberList GetExpiredDocMemberList(string strDate)
        {
            Log.General("<MemberDAOImpl> : GetExpiredDocMemberList(" +strDate+ ")");
            return _memberDAO.GetExpiredDocMemberList(strDate);
        }
        #endregion
	}
}
