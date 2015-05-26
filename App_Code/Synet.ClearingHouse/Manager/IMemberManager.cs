/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

using Synet.ClearingHouse.Model;
using Synet.Common.Manager;

namespace Synet.ClearingHouse.Manager
{
	/// <summary>
	/// Description of IMemberManager.
	/// </summary>
	public interface IMemberManager: IManager
	{
		ChurnResponse GetMemberChurn(string memberCode);
		
		MemberInfo GetMemberInfoPersonal(string memberCode);
		
		bool GetVerifyDocumentStatus(string memberCode, int docTypeId);
		
		AdminInfo GetAdminUserInfo(string adminCode);
		
		SignUpResponse GetListOfMemberSignUp(int month, int year);
        
		AuthToken VerifyAuthenticationToken(string memberCode, string txtkey, string ipAddress, string oucode);
		
		TotalMemberSignup GetTotalMemberSignup(int month, int year);
        
		int VerifyAdminToken(string adminCode, string txtKey, string ipAddress, string oucode);
        
		VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode, string strPass);
	
		UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger);

        ExDocMemberList GetExpiredDocMemberList(string strDate);
    }
}
