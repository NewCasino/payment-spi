using System;

using Synet.ClearingHouse.Model;
	
namespace Synet.ClearingHouse.Dao
{
	/// <summary>
	/// Description of IPaymentDAO.
	/// </summary>
	public interface IPaymentDAO : Synet.Common.Dao.IDAO
	{
		int AddUptAdjust(AdjustmentInfo adjInfo, int memberID, int currID, int transMode);
		AdjustmentRetrieval GetDepWithAdjustment(int refNo);
		bool CreateMemberFirstDepositInfo(string memberCode);
		CashbalanceOutPut UpdateMemberCashBalance(int memberID, CashBalanceInput cashBalanceObjInput);
		CurrencyInfoList GetCurrencyInfoList();
		InvCurrencyInfo GetInvCurrencyInfo(string currCode);
	}
}
