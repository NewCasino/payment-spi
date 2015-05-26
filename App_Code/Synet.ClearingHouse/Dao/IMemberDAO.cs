using System;
using System.Collections;
using System.Data;

using Synet.ClearingHouse.Model;

namespace Synet.ClearingHouse.Dao
{
	/// <summary>
	/// Description of ICasinoDAO.
	/// </summary>
	public interface IMemberDAO : Synet.Common.Dao.IDAO
	{
		bool CheckMemberExist(string memberCode);
		
		bool CheckAdminExist(string adminCode);
		
		int GetMemberIdByMemberCode(string memberCode);
		
		string GetMemberCodeByMemberId(int memberID);
		
		ChurnResponse GetMemberChurn(string memberCode);
		
		MemberInfo GetMemberInfoPersonal(int memberID);
		
		bool GetVerifyDocumentStatus(int memberID, int docTypeId);
		
		int GetCurrencyIDByCurrencyCode(string currencyCode);
		
		AdminInfo GetAdminUserInfo(string adminCode);
		
		SignUpResponse GetListOfMemberSignUp(int month, int year);
        
		AuthToken VerifyAuthenticationToken(string memberCode, string decryptedSessionID, string ipAddress, string oucode);
        
		int VerifyAdminToken(string adminCode, string sessionId, string ipAddress, string oucode);
		
		TotalMemberSignup GetTotalMemberSignup(int month, int year);
		
		string GetSSLKey();
        
		VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode, string strPass);
	
		UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger);

        ExDocMemberList GetExpiredDocMemberList(string strDate);
    }
}
