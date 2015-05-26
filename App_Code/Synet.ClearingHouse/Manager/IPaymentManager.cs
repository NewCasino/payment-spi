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
	/// Description of IPaymentManager.
	/// </summary>
	public interface IPaymentManager: IManager
	{
		int AddUptAdjust(AdjustmentInfo adjInfo, int transMode);
		AdjustmentRetrieval GetDepWithAdjustment(int refNo);
		bool CreateMemberFirstDepositInfo(string memberCode);
		CashbalanceOutPut UpdateMemberCashBalance(string memberCode, CashBalanceInput casBalanceObjInput);
		CurrencyInfoList GetCurrencyInfoList();
		InvCurrencyInfo GetInvCurrencyInfo(string currCode);
	}
}
