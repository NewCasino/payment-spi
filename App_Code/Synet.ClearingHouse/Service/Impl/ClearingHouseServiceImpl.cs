/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Manager;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Exceptions;
using Synet.Common.Logs;


namespace Synet.ClearingHouse.Service.Impl
{
	/// <summary>
	/// Description of ClearingHouseServiceImpl.
	/// </summary>
	public class ClearingHouseServiceImpl: Synet.ClearingHouse.Service.IClearingHouseService
	{
    	
    	#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
     	protected static readonly ISysLog Log = SysLog.GetLogger(typeof(ClearingHouseServiceImpl).Name);
    	private IMemberManager _memberManager;
    	private IPaymentManager _paymentManager;
    	#endregion 
    
    	#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
    
    	public ClearingHouseServiceImpl(){}
    	
    	#endregion 

    	#region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (3)
    
    	public IMemberManager MemberManager
		{
			set { _memberManager = value; }
		}
    	
    	public IPaymentManager PaymentManager
		{
			set { _paymentManager = value; }
		}
    	
    	#endregion 
    
    	#region  METHODS : PUBLIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (7)
    
    	public ChurnResponse GetMemberChurnObj(string memberCode)
    	{
    		Log.General("<ClearingHouseServiceImpl> : GetMemberChurnObj(" + memberCode + ")");
    		
    		ChurnResponse churnObj = new ChurnResponse();
    		try
    		{
    			// churnObj.churnamt = _memberManager.GetMemberChurn(memberCode);    			
    			churnObj = _memberManager.GetMemberChurn(memberCode);    			
    			churnObj.returnCode = CstError.GENERAL_SUCCESS;
    		}
    		catch (ClearingHouseException ex)
    		{
    			churnObj.totalbet = 0;
    			churnObj.totaldepositadj = 0;
    			churnObj.returnCode = ex.ErrorCode;
    		}
    		return churnObj;
    	}
    	
    	public VerifyResponse GetVerifyDocumentStatus(string memberCode, int docTypeId)
        {
    		Log.General("<ClearingHouseServiceImpl> : GetVerifyDocumentStatus(" + memberCode + ", " + docTypeId + ")");
    		
            VerifyResponse docObj = new VerifyResponse();
            try
            {
                docObj.verifyStatus = _memberManager.GetVerifyDocumentStatus(memberCode, docTypeId);
                docObj.returnCode = CstError.GENERAL_SUCCESS;
            }
            catch (ClearingHouseException ex)
            {
                docObj.verifyStatus = false;
                docObj.returnCode = ex.ErrorCode;
            }
            return docObj;
        }
    	
    	public MemberInfo GetMemberInfoPersonal(string memberCode)
    	{
			Log.General("<ClearingHouseServiceImpl> : GetMemberInfoPersonal(" + memberCode + ")");
			
			MemberInfo memObj = new MemberInfo();
			try
			{
				memObj = _memberManager.GetMemberInfoPersonal(memberCode);
			}
			catch(ClearingHouseException ex)
			{
				memObj.returnCode = ex.ErrorCode;
				memObj.memberCode = "";
				memObj.userID = 0;
				memObj.currCode = "";
				memObj.email = "";
				memObj.websiteName = "";
				memObj.balance = 0;
				memObj.vendorID = "0";
				memObj.country = "";
				memObj.firstname = "";
				memObj.lastname = "";
				memObj.verifyID = 0;
				memObj.dtupdatedmember = "";
				memObj.dtupdatedadmin = "";
				memObj.mobileno = "";
				memObj.active = "";
				memObj.ccverifyid = 0;
			}
    		return memObj;
    	}

    	public AdjustmentResult AddUptAdjust(AdjustmentInfo adjInfo, int transMode)
    	{
    		Log.General("<ClearingHouseServiceImpl> : AddUptAdjust");
    		
    		AdjustmentResult adjObj = new AdjustmentResult();
			try
			{
				adjObj.transID = _paymentManager.AddUptAdjust(adjInfo, transMode);
				adjObj.returnCode = CstError.GENERAL_SUCCESS;
			}
			catch(ClearingHouseException ex)
			{
				adjObj.returnCode = ex.ErrorCode;
				adjObj.transID = 0;
			}
    		return adjObj;
    	}
    	
    	public AdjustmentRetrieval GetDepWithAdjustment(int refNo)
    	{
    		Log.General("<ClearingHouseServiceImpl> : GetDepWithAdjustment");
    		
    		AdjustmentRetrieval adjRetrieval = null;
			try
			{
				adjRetrieval = _paymentManager.GetDepWithAdjustment(refNo);				
			}
			catch(ClearingHouseException)
			{
				adjRetrieval = new AdjustmentRetrieval();
            	adjRetrieval.returnCode = CstError.GENERAL_ERROR;
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
    	
    	public MemberDeposit CreateMemberFirstDepositInfo(string memberCode)
        {
            Log.General("<ClearingHouseServiceImpl> : CreateMemberFirstDepositInfo(" + memberCode + ")");            
            
            MemberDeposit memDepositObj = new MemberDeposit();
            try
            {
                if (_paymentManager.CreateMemberFirstDepositInfo(memberCode))
                    memDepositObj.returnCode = CstError.GENERAL_SUCCESS;
                else
                    memDepositObj.returnCode = CstError.DB_OBJECT_ALREADY_EXIST;
            }
            catch (ClearingHouseException ex)
            {
                memDepositObj.returnCode = ex.ErrorCode;                
            }
            return memDepositObj;
        }
    	
    	public CashbalanceOutPut UpdateMemberCashBalance(string memberCode, CashBalanceInput casBalanceObjInput)
        {
            Log.General("<MemberDAOImpl> : UpdateMemberCashBalance");
            CashbalanceOutPut casBalanceObj = new CashbalanceOutPut();
            try
            {
                casBalanceObj = _paymentManager.UpdateMemberCashBalance(memberCode, casBalanceObjInput);
            }
            catch (ClearingHouseException ex)
            {
                casBalanceObj.returnCode = ex.ErrorCode;
            }
            return casBalanceObj;
        }
    	
    	public AdminInfo GetAdminUserInfo(string adminCode)
    	{
			Log.General("<ClearingHouseServiceImpl> : GetAdminUserInfo(" + adminCode + ")");
			
			AdminInfo admObj = new AdminInfo();
			try
			{
				admObj = _memberManager.GetAdminUserInfo(adminCode);
			}
			catch(ClearingHouseException ex)
			{
				admObj.returnCode = ex.ErrorCode;
				admObj.adminname = "";
				admObj.admintypeid = 0;
				
			}
    		return admObj;
    	}
    	
    	public SignUpResponse GetListOfMemberSignUp(int month, int year)
        {
            Log.General("<ClearingHouseServiceImpl> : GetListOfMemberSignUp(" + month + "," + year + ")");

            SignUpResponse signUpResObj = new SignUpResponse();
            try
            {
                signUpResObj = _memberManager.GetListOfMemberSignUp(month, year);
            }
            catch (ClearingHouseException ex)
            {
                signUpResObj.returnCode = ex.ErrorCode;
                signUpResObj.memberList = new List<string>();
            }
            return signUpResObj;
        }

        public AuthToken VerifyAuthenticationToken(string memberCode, string txtkey, string ipAddress, string oucode)
        {
            Log.General("VerifyMemberTo(" + memberCode + "," + txtkey + "," + ipAddress + "," + oucode + ")");

            AuthToken auTokenObj = new AuthToken();
            try
            {
                auTokenObj = _memberManager.VerifyAuthenticationToken(memberCode, txtkey, ipAddress, oucode);
            }
            catch (ClearingHouseException ex)
            {
                auTokenObj.ReturnCode = ex.ErrorCode;
                auTokenObj.Passed = false;
            }
            catch (Exception)
            {
                auTokenObj.ReturnCode = CstError.GENERAL_ERROR;
                auTokenObj.Passed = false;
            }
            
            return auTokenObj;
        }
        
		public AdminAuthToken VerifyAdminToken(string adminCode, string txtKey, string ipAddress, string ouCode)
        {
      		Log.General(String.Format("VerifyAdminToken({0}, {1}, {2}, {3})", adminCode, txtKey, ipAddress, ouCode));
        	AdminAuthToken retValue = new AdminAuthToken();

            try
            {
                int codeVal = _memberManager.VerifyAdminToken(adminCode, txtKey, ipAddress, ouCode);
                retValue.ReturnCode = CstError.GENERAL_SUCCESS;
                retValue.Passed = (codeVal == 0);
            }
            catch (ClearingHouseException ex)
            {
                retValue.ReturnCode = ex.ErrorCode;
                retValue.Passed = false;
            }
            catch (Exception)
            {
                retValue.ReturnCode = CstError.GENERAL_ERROR;
                retValue.Passed = false;
            }

            return retValue;
        }
        		
    	public CurrencyInfoList GetCurrencyInfoList()
    	{
			Log.General("<ClearingHouseServiceImpl> : GetCurrencyInfoList()");
			
			CurrencyInfoList currLstObj = new CurrencyInfoList();
			try
			{
				currLstObj = _paymentManager.GetCurrencyInfoList();
			}
			catch(ClearingHouseException ex)
			{
				currLstObj.returnCode = ex.ErrorCode;
				currLstObj.currencyList = new List<CurrencyInfo>();
			}
    		return currLstObj;
    	}
    	
    	public InvCurrencyInfo GetInvCurrencyInfo(string currCode)
    	{
			Log.General("<ClearingHouseServiceImpl> : GetInvCurrencyInfo()");
			
			InvCurrencyInfo currInfo = new InvCurrencyInfo();
			try
			{
				currInfo = _paymentManager.GetInvCurrencyInfo(currCode);
			}
			catch(ClearingHouseException ex)
			{
				currInfo.ReturnCode = ex.ErrorCode;
				currInfo.CurrInfo = null;
			}
    		return currInfo;
    	}
    	
    	public TotalMemberSignup GetTotalMemberSignup(int month, int year)
        {
            Log.General("<ClearingHouseServiceImpl> : GetTotalMemberSignup(" + month + ", " + year + ")");

            TotalMemberSignup totalMemberObj = new TotalMemberSignup();
            try
            {
                totalMemberObj = _memberManager.GetTotalMemberSignup(month, year);
            }
            catch (ClearingHouseException ex)
            {
                totalMemberObj.returnCode = ex.ErrorCode;
                totalMemberObj.currentMonthList = new List<MemsPerMonth>();
                totalMemberObj.toDateList = new List<MemsPerMonth>();
            }
            return totalMemberObj;
        }
        
    	public VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode, string strPass)
        {
            Log.General("<ClearingHouseServiceImpl> : VerifyMemberCredentials(" + memberCode + ", " + oucode + ")");

            VerifyMemberResponse veriMemResObj = new VerifyMemberResponse();
            try
            {
                veriMemResObj = _memberManager.VerifyMemberCredentials(memberCode, oucode, strPass);
            }
            catch (ClearingHouseException ex)
            {
                veriMemResObj.returnCode = ex.ErrorCode;
            }
            return veriMemResObj;
        }
    	
    	public UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger)
    	{
    	    Log.General("<ClearingHouseServiceImpl> : SetMemberUnverified(" + strAdminCode + ", " + strMemCode + ", " + strEventTrigger + ")");
    	    
    	    UnverifyMemberResponse unverifyMemResObj = new UnverifyMemberResponse();
    	    try
    	    {
    	        unverifyMemResObj = _memberManager.SetMemberUnverified(strAdminCode, strMemCode, strEventTrigger);
    	    }
    	    catch(ClearingHouseException ex)
    	    {   
    	        unverifyMemResObj.returnCode = ex.ErrorCode;
    	    }
    	    return unverifyMemResObj;
    	}

        public ExDocMemberList GetExpiredDocMemberList(string strDate)
        {
            Log.General("<ClearingHouseServiceImpl> : GetExpiredDocMemberList("+ strDate + ")");

            ExDocMemberList _exdocMemList = new ExDocMemberList();
            try
            {
                _exdocMemList = _memberManager.GetExpiredDocMemberList(strDate);
            }
            catch (ClearingHouseException ex)
            {
                _exdocMemList.returnCode = ex.ErrorCode;
                _exdocMemList.memberList = new List<string>();
            }
            return _exdocMemList;
        }
        #endregion
    
    }
}
