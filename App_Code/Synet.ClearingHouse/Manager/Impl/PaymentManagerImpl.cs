/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:39 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Dao;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Exceptions;
using Synet.Common.Logs;
using Synet.Common.Exceptions;

namespace Synet.ClearingHouse.Manager.Impl
{
	/// <summary>
	/// Description of PaymentManagerImpl.
	/// </summary>
	public class PaymentManagerImpl: Synet.ClearingHouse.Manager.IPaymentManager
	{
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        protected static readonly ISysLog Log = SysLog.GetLogger(typeof(PaymentManagerImpl).Name);
        private IPaymentDAO _paymentDAO;
        private IMemberDAO _memberDAO;
        
        #endregion 
        
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
        
        public PaymentManagerImpl(){}
        
        #endregion 
        
        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
        
        public IPaymentDAO PaymentDAO
		{
			set { _paymentDAO = value; }
		}
        
        public IMemberDAO MemberDAO
		{
			set { _memberDAO = value; }
		}
        
       	#endregion 
        
        #region  METHODS : PUBLIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (7)
        
        public int AddUptAdjust(AdjustmentInfo adjInfo, int transMode)
        {
        	Log.General("<PaymentManagerImpl> : AddUptAdjust");
        	if (adjInfo == null || adjInfo.refNo < 0) {
				ClearingHouseException ex = new ClearingHouseException(CstError.GENERAL_ERROR, "Invalid Adjustment Info!");
				ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Invalid Adjustment Info!");
				throw ex;
			}
            //Commented by Son Tran
        	/*if (!_memberDAO.CheckAdminExist(adjInfo.adminCode))
			{
				ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Admin Code does not exist!");
				ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Admin Code does not exist!");
				throw ex;
			}*/
        	if (adjInfo.description == null)
            {
            	adjInfo.description = "";
            }
        	int memberID = _memberDAO.GetMemberIdByMemberCode(adjInfo.memberCode);
        	int currID = 0;
        	try
        	{
				currID = _memberDAO.GetCurrencyIDByCurrencyCode(adjInfo.currCode);
        	}
        	catch(ClearingHouseException)
        	{
        		ClearingHouseException ex = new ClearingHouseException(CstError.DB_OBJECT_NOT_FOUND, "Currency Code not exist!");
				ExceptionManager.ExceptionHandler(ex, CstError.DB_OBJECT_NOT_FOUND, "Currency Code not exist!");
				throw ex;
        	}
        	return _paymentDAO.AddUptAdjust(adjInfo, memberID, currID, transMode);
        }
        
        public AdjustmentRetrieval GetDepWithAdjustment(int refNo)
        {
        	Log.General("<PaymentManagerImpl> : GetDepWithAdjustment");
        	AdjustmentRetrieval adjRetrieval = _paymentDAO.GetDepWithAdjustment(refNo);
        	if (adjRetrieval == null)
            {
            	adjRetrieval = new AdjustmentRetrieval();
            	adjRetrieval.returnCode = CstError.DB_OBJECT_NOT_FOUND;
            	adjRetrieval.transID = 0;
            	adjRetrieval.refNo = 0;
            	adjRetrieval.adminCode = "";
            	adjRetrieval.memberCode = "";
            	adjRetrieval.creditDebit = 0;
            	adjRetrieval.amount = 0;
            	adjRetrieval.description = "";
            	adjRetrieval.status = 0;
            }
        	return adjRetrieval;
        }
        
        public bool CreateMemberFirstDepositInfo(string memberCode)
        {
            Log.General("<PaymentManagerImpl> : CreateMemberFirstDepositInfo(" + memberCode + ")");
            if (memberCode == null)
            {
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
            return _paymentDAO.CreateMemberFirstDepositInfo(memberCode);
        }
        
        public CashbalanceOutPut UpdateMemberCashBalance(string memberCode,CashBalanceInput cashBalanceObjInput)
        {
            Log.General("<PaymentManagerImpl> : UpdateMemberCashBalance");
            
            if (cashBalanceObjInput == null)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.GENERAL_ERROR, "Invalid Cash Balance Info!");
				ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Invalid Cash Balance Info!");
				throw ex;
            }
            
            int memberID = _memberDAO.GetMemberIdByMemberCode(memberCode);
            
            //Commented by Son Tran
            /*if (!_memberDAO.CheckAdminExist(cashBalanceObjInput.adminCode))
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Admin Code does not exist!");
				ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Admin Code does not exist!");
				throw ex;
            }*/
            
            return _paymentDAO.UpdateMemberCashBalance(memberID, cashBalanceObjInput);
        }
        
        public CurrencyInfoList GetCurrencyInfoList()
        {
        	Log.General("<PaymentManagerImpl> : GetCurrencyInfoList");        	
        	return _paymentDAO.GetCurrencyInfoList();
        }
        
        public InvCurrencyInfo GetInvCurrencyInfo(string currCode)
        {
        	Log.General("<PaymentManagerImpl> : GetInvCurrencyInfo");        	
        	
        	if (currCode == null)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid Currency Code!");
				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid Currency Code!");
				throw ex;
            }
        	InvCurrencyInfo invCurrency = _paymentDAO.GetInvCurrencyInfo(currCode);
        	if (invCurrency == null)
		    {
		    	ClearingHouseException ex = new ClearingHouseException(CstError.DB_OBJECT_NOT_FOUND, "Currency Code '" + currCode + "' does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.DB_OBJECT_NOT_FOUND, "Currency Code '" + currCode + "' does not exist!");
            	throw ex;
		    }
        	
        	return invCurrency;
        }
        #endregion 
	}
}
