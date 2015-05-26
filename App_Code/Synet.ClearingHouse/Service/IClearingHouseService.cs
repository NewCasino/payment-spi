/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

using Synet.Common.Service;
using Synet.ClearingHouse.Model;

namespace Synet.ClearingHouse.Service
{
	/// <summary>
	/// Description of IClearingHouseService.
	/// </summary>
	public interface IClearingHouseService: IService
	{
		ChurnResponse GetMemberChurnObj(string memberCode);
		
		VerifyResponse GetVerifyDocumentStatus(string memberCode, int docTypeId);
		
		MemberInfo GetMemberInfoPersonal(string memberCode);
		
		AdjustmentResult AddUptAdjust(AdjustmentInfo adjInfo, int transMode);
		
		AdjustmentRetrieval GetDepWithAdjustment(int refNo);
		
		MemberDeposit CreateMemberFirstDepositInfo(string memberCode);
		
		CashbalanceOutPut UpdateMemberCashBalance(string memberCode, CashBalanceInput casBalanceObjInput);
		
		AdminInfo GetAdminUserInfo(string adminCode);
		
		SignUpResponse GetListOfMemberSignUp(int month, int year);
        
		AuthToken VerifyAuthenticationToken(string memberCode, string txtkey, string ipAddress, string oucode);
		
		AdminAuthToken VerifyAdminToken(string adminCode, string txtKey, string ipAddress, string ouCode);
		
		CurrencyInfoList GetCurrencyInfoList();
		
		InvCurrencyInfo GetInvCurrencyInfo(string currCode);
		
		TotalMemberSignup GetTotalMemberSignup(int month, int year);
        
		VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode, string strPass);
        
		UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger);

        ExDocMemberList GetExpiredDocMemberList(string strDate);
    }
}
