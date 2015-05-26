/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;

using Synet.ClearingHouse.Util;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Dao;
using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Exceptions;

using Synet.Common.Logs;
using Synet.Common.Exceptions;

namespace Synet.ClearingHouse.Dao.Impl
{
	/// <summary>
	/// Description of PaymentDAOImpl.
	/// </summary>
	public class PaymentDAOImpl: Synet.ClearingHouse.Dao.IPaymentDAO
	{	
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
		protected static readonly ISysLog Log = SysLog.GetLogger(typeof(PaymentDAOImpl).Name);		
		private IMemberDAO _memberDAO;
		#endregion		
		
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public PaymentDAOImpl(){}
		
		#endregion
		
		#region  PROPERTIES : WRITE ONLY >>>>>>>>>>>>>>>>>>>> (1)
		
		public IMemberDAO MemberDAO
		{
			set { _memberDAO = value; }
		}		
		
		#endregion
		
		#region  METHODS : PUBLIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (0)
		
		public int AddUptAdjust(AdjustmentInfo adjInfo, int memberID, int currID, int transMode)
		{
			Log.General("<PaymentDAOImpl> : createAdjust");
			
			#region 1. Set Variables			
			DataSet _ds = new DataSet();
			SqlDataAdapter _sqlReader = null;
            int transRetVal = -1;
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_AddTransAdj_ClearingHouse", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                
                _sqlComm.Parameters.AddWithValue("@ParentID", adjInfo.refNo);
                _sqlComm.Parameters.AddWithValue("@AdminID", adjInfo.adminCode);
                _sqlComm.Parameters.AddWithValue("@MemberID", memberID);
                _sqlComm.Parameters.AddWithValue("@AffairID", transMode);
                _sqlComm.Parameters.AddWithValue("@PurposeID", adjInfo.purposeID);
                _sqlComm.Parameters.AddWithValue("@CurrencyID", currID);
                _sqlComm.Parameters.AddWithValue("@CurrencyUnit", adjInfo.currUnit);
                _sqlComm.Parameters.AddWithValue("@ExchgRate", adjInfo.exchgRate);
                _sqlComm.Parameters.AddWithValue("@CreditDebitFlag", adjInfo.creditDebit);
                _sqlComm.Parameters.AddWithValue("@Amount", adjInfo.amount);
                _sqlComm.Parameters.AddWithValue("@Description", adjInfo.description);
                _sqlComm.Parameters.AddWithValue("@Status", adjInfo.status); 
                _sqlComm.Parameters.AddWithValue("@trxnid", adjInfo.trxnid);       
                _sqlComm.Parameters.AddWithValue("@trxndate", adjInfo.trxndate);                
                _sqlComm.Parameters.AddWithValue("@admupdate", adjInfo.admupdate); 
                _sqlComm.Parameters.AddWithValue("@updatedate", adjInfo.upddate);    
                 
					
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    
                    _sqlCon.Open();
                    _sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					//Have to go to the last RecordSet 
					//because this stored proc return unknown number of RecordSet
					DataRowCollection _drCollection = _ds.Tables[_ds.Tables.Count - 1].Rows;
					string returnCode = CstError.GENERAL_SUCCESS;
					string returnMsg = "";
					if (_drCollection.Count > 0)
					{
						switch (int.Parse(_drCollection[0]["ReturnValue"].ToString()))
						{
							case 1:
								transRetVal = int.Parse(_drCollection[0]["TransID"].ToString());
								break;
							case -2:
								returnCode = CstError.MEMBER_NOT_FOUND;
								returnMsg = "Member does not exist!";
								break;
							case -3:
								returnCode = CstError.MEMBER_INACTIVE;
								returnMsg = "Member is inactive!";
								break;
							case -4:
								returnCode = CstError.MEMBER_ZERO_BALANCE;
								returnMsg = "Member's credit limit/balance is zero!";
								break;
							case -5:
								returnCode = CstError.MEMBER_NOT_ENOUGH_BALANCE;
								returnMsg = "Member's credit balance not enough!";
								break;
							default:
								returnCode = CstError.GENERAL_ERROR;
								returnMsg = "Save Record Fail!";
								break;
						}
						
					}
					if (returnCode != CstError.GENERAL_SUCCESS)
					{
						throw new ClearingHouseException(returnCode, returnMsg);
					}
                    #endregion
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    if (ex is ClearingHouseException)
                    {
                    	ExceptionManager.ExceptionHandler(ex, ((ClearingHouseException) ex).ErrorCode , ((ClearingHouseException) ex).ErrorMessage);
                    	throw ex;
                    }
                    else
                    {
                    	ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    	throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
                    }                    
                    #endregion
                }
                finally
                {
                    #region 6. Close DB Connection
                    DBUtil.Close(_sqlCon);
                    #endregion
                }
            }
            return transRetVal;
		}
		
		public AdjustmentRetrieval GetDepWithAdjustment(int refNo)
		{
			Log.General("<PaymentDAOImpl> : GetDepWithAdjustment");
			
			#region 1. Set Variables			
			DataSet _ds = new DataSet();
			SqlDataAdapter _sqlReader = null;
			AdjustmentRetrieval adjRetrieval = null;
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_GetTransAdj", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                
                _sqlComm.Parameters.AddWithValue("@ParentID", refNo);
					
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    
                    _sqlCon.Open();                    
                    _sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
					if (_drCollection.Count > 0)
					{
						adjRetrieval = new AdjustmentRetrieval();
						adjRetrieval.returnCode = CstError.GENERAL_SUCCESS;
						adjRetrieval.transID = int.Parse(_drCollection[0]["TransID"].ToString());
						adjRetrieval.refNo = int.Parse(_drCollection[0]["ParentID"].ToString());
						adjRetrieval.adminCode = _drCollection[0]["AdminUpdated"].ToString();
						adjRetrieval.memberCode = _memberDAO.GetMemberCodeByMemberId(int.Parse(_drCollection[0]["MemberID"].ToString()));
						adjRetrieval.creditDebit = int.Parse(_drCollection[0]["CreditDebitFlag"].ToString());						
						adjRetrieval.amount = double.Parse(_drCollection[0]["Amount"].ToString());
						adjRetrieval.description = _drCollection[0]["Description"].ToString();
						adjRetrieval.status = int.Parse(_drCollection[0]["Status"].ToString());
					}
                    #endregion
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);                    
                    #endregion
                }
                finally
                {
                    #region 6. Close DB Connection
                    DBUtil.Close(_sqlCon);
                    #endregion
                }
            }            
            return adjRetrieval;
		}
		
		/*
         * To Create Member Deposite by MemberCode
         */
        public bool CreateMemberFirstDepositInfo(string memberCode)
        {
            #region 1. Set Variables                
            bool blnCreateMember = false;
            #endregion

            Log.General("<PaymentDAOImpl> : CreateMemberFirstDepositInfo(" + memberCode + ")");
            
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_AddMemberFirstDepositInfo", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;

                _sqlComm.Parameters.Add("@memberCode", SqlDbType.VarChar);
                _sqlComm.Parameters["@memberCode"].Value = memberCode;
                _sqlComm.Parameters.Add("@Retval", SqlDbType.Int).Direction = ParameterDirection.Output;
                try
                {
                    _sqlCon.Open();
                    _sqlComm.ExecuteNonQuery();                    
                    if ((int)_sqlComm.Parameters["@Retval"].Value == 1)
                        blnCreateMember = true;
                    else
                        blnCreateMember = false;
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
                    #endregion
                }
                finally
                {
                    DBUtil.Close(_sqlCon);
                }
            }
            return blnCreateMember;		
        }
        
        public CashbalanceOutPut UpdateMemberCashBalance(int memberID,CashBalanceInput cashBalanceObjInput)
        {
        	Log.General("<MemberDAOImpl> : UpdateMemberCashBalance(" + memberID + "," + cashBalanceObjInput + ")");
        	
            #region 1. Set Variables
            SqlDataReader       reader;
            CashbalanceOutPut   casBalanceObj = new CashbalanceOutPut();
            string returnMsg    = "";
            double changeAmount = cashBalanceObjInput.changeAmount;
            int iUpdateSuccess  = 0;
            int intAffairID     = 0;

            if (cashBalanceObjInput.trxnType == "W")
            {
                intAffairID = Constants.WITHDRAWAL;
                // Need to change to negative value for Withdrawal Approval
                changeAmount = -1 * cashBalanceObjInput.changeAmount;
            }
            else if (cashBalanceObjInput.trxnType == "WP")
                intAffairID = Constants.WITHDRAWAL_PENDING;
            else if (cashBalanceObjInput.trxnType == "WR")
            {
                intAffairID = Constants.WITHDRAWAL_REJECTION;
                // Need to change to negative value for Withdrawal Rejection
                changeAmount = -1 * cashBalanceObjInput.changeAmount;
            }
            else
                intAffairID = Constants.DEPOSIT;
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_UptMemberCashBalance_PCH", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;

                _sqlComm.Parameters.AddWithValue("@adminCode", cashBalanceObjInput.adminCode);
                _sqlComm.Parameters.AddWithValue("@memberID", memberID);
                _sqlComm.Parameters.AddWithValue("@changeAmount", changeAmount);
                _sqlComm.Parameters.AddWithValue("@logID", cashBalanceObjInput.refNo);
                _sqlComm.Parameters.AddWithValue("@affairID", intAffairID);
                _sqlComm.Parameters.AddWithValue("@paymenttype", cashBalanceObjInput.paymenttype);

                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                        iUpdateSuccess = int.Parse(reader.GetSqlValue(0).ToString());
                    }
                    reader.Close();

                    switch (iUpdateSuccess)
                    {
                        case 1:
                            casBalanceObj.returnCode = CstError.SUCCESS;
                            returnMsg = "Update succeed";
                            break;
                        case -1:
                            casBalanceObj.returnCode = CstError.ERR_NO_CHG_AMT;
                            returnMsg = "Update failed, no change amount";
                            break;
                        case -2:
                            casBalanceObj.returnCode = CstError.ERR_MEM_NOT_FOUND;
                            returnMsg = "Update failed, member does not exists";
                            break;
                        case -3:
                            casBalanceObj.returnCode = CstError.ERR_MEM_INACTIVE;
                            returnMsg = "Update failed, member is inactive";
                            break;
                        case -4:
                            casBalanceObj.returnCode = CstError.ERR_NO_LODID;
                            returnMsg = "Update failed, no Log ID";
                            break;
                        case -5:
                            casBalanceObj.returnCode = CstError.SUC_DUP_SUBMIT;
                            returnMsg = "Update succeed, duplicate submission";
                            break;
                        case -6:
                            casBalanceObj.returnCode = CstError.ERR_VAL_BAL_INSUF;
                            returnMsg = "Update failed, insufficient balance";
                            break;
                        case -7:
                            casBalanceObj.returnCode = CstError.ERR_WD_BAL_INSUF;
                            returnMsg = "Update failed, insufficient pending withdrawal balance";
                            break;
                        case -8:
                            casBalanceObj.returnCode = CstError.ERR_NO_SUBMISSION_FOUND;
                            returnMsg = "Update failed, no submission found";
                            break;
                        case -9:
                            casBalanceObj.returnCode = CstError.SUC_NO_SUBMISSION_FOUND;
                            returnMsg = "Update succeed, no submission found";
                            break;
                        case -10:
                            casBalanceObj.returnCode = CstError.ERR_TRANS_REJECTED;
                            returnMsg = "Update failed, transaction rejected";
                            break;
                        case -11:
                            casBalanceObj.returnCode = CstError.ERR_TRANS_APPROVED;
                            returnMsg = "Update failed, transaction approved";
                            break;
                        default:
                            casBalanceObj.returnCode = CstError.ERROR;
                            returnMsg = "Update failed";
                            break;

                        #region Old Codes
                        //case 1:
                        //    casBalanceObj.returnCode = CstError.GENERAL_SUCCESS;
                        //    returnMsg = "Update cash balance success";
                        //    break;
                        //case -1:
                        //    casBalanceObj.returnCode = CstError.MEMBER_ZERO_CHANGE_AMOUNT;
                        //    returnMsg = "Change amount is zero, no update require";
                        //    break;
                        //case -2:
                        //    casBalanceObj.returnCode = CstError.MEMBER_NOT_FOUND;
                        //    returnMsg = "Member does not exists";
                        //    break;
                        //case -3:
                        //    casBalanceObj.returnCode = CstError.MEMBER_INACTIVE;
                        //    returnMsg = "Member is inactive";
                        //    break;
                        //case -4:
                        //    casBalanceObj.returnCode = CstError.MEMBER_NOT_ENOUGH_BALANCE;
                        //    returnMsg = "Member balance not enough for withdrawal";
                        //    break;
                        //case -5:
                        //    casBalanceObj.returnCode = CstError.LOGID_ALREADY_EXISTS;
                        //    returnMsg = "Log id already exists in Cash Log";
                        //    break;
                        //case -6:
                        //    casBalanceObj.returnCode = CstError.NO_LOGID;
                        //    returnMsg = "No logID";
                        //    break;
                        //case -7:
                        //    casBalanceObj.returnCode = CstError.LOGID_ALREADY_EXISTS_PWL;
                        //    returnMsg = "Log id already exists in Pending Withdrawal Log";
                        //    break;
                        //case -8:
                        //    casBalanceObj.returnCode = CstError.MEMBER_WS_NOT_FOUND;
                        //    returnMsg = "Withdrawal Submission does not exists";
                        //    break;
                        //case -9:
                        //    casBalanceObj.returnCode = CstError.MEMBER_NOT_ENOUGH_BALANCE_PWL;
                        //    returnMsg = "Pending withdrawal balance not enough for Approval or Rejection";
                        //    break;
                        //default:
                        //    casBalanceObj.returnCode = CstError.GENERAL_ERROR;
                        //    returnMsg = "Save record fail";
                        //    break;
                        #endregion
                    }
                    if (casBalanceObj.returnCode != CstError.SUCCESS &&
                        casBalanceObj.returnCode != CstError.SUC_DUP_SUBMIT &&
                        casBalanceObj.returnCode != CstError.SUC_NO_SUBMISSION_FOUND)
                    {
                        throw new ClearingHouseException(casBalanceObj.returnCode, returnMsg);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    if (ex is ClearingHouseException)
                    {
                        ExceptionManager.ExceptionHandler(ex, ((ClearingHouseException)ex).ErrorCode, ((ClearingHouseException)ex).ErrorMessage);
                        throw ex;
                    }
                    else
                    {
                        ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                        throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
                    }
                    #endregion
                }
                finally
                {
                    DBUtil.Close(_sqlCon);
                }
            }
            return casBalanceObj;	
        }
        
        public CurrencyInfoList GetCurrencyInfoList()
		{
			Log.General("<PaymentDAOImpl> : GetCurrencyInfoList");
			
			#region 1. Set Variables
			DataSet _ds = new DataSet();
			SqlDataAdapter _sqlReader = null;
			CurrencyInfoList curInfoList = new CurrencyInfoList();
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("dbo.admin_GetCurrencyList", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.Clear();
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    List<CurrencyInfo> lst_currInfo = new List<CurrencyInfo>();
                    
                    _sqlCon.Open();
                    _sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
                    curInfoList.returnCode = CstError.GENERAL_SUCCESS;
                    
                    foreach(DataRow row in _drCollection)
                    {
                    	CurrencyInfo currInfo = new CurrencyInfo();
                    	currInfo.isoCode = row["CurrencyCode"].ToString();
                    	currInfo.exchgRate = double.Parse(row["ExchgRate"].ToString());
                    	currInfo.currUnit = int.Parse(row["CurrencyUnit"].ToString());
                    	currInfo.currBase = bool.Parse(_drCollection[0]["Base"].ToString()) ? 1 : 0;
                    	lst_currInfo.Add(currInfo);
                    }                    
					curInfoList.currencyList = lst_currInfo;
                    #endregion
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);                    
                    #endregion
                }
                finally
                {
                    #region 6. Close DB Connection
                    DBUtil.Close(_sqlCon);
                    #endregion
                }
            }
            return curInfoList;
		}
        
        public InvCurrencyInfo GetInvCurrencyInfo(string currCode)
		{
			Log.General("<PaymentDAOImpl> : GetInvCurrencyInfo");
			
			#region 1. Set Variables
			DataSet _ds = new DataSet();
			SqlDataAdapter _sqlReader = null;
			InvCurrencyInfo invCurrency = null;			
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("SELECT top 1 CurrencyCode, ExchgRate, CurrencyUnit, Base FROM tblCurrency(NOLOCK) WHERE CurrencyCode = @currCode", _sqlCon);
                _sqlComm.CommandType = CommandType.Text;
                
                _sqlComm.Parameters.AddWithValue("@currCode", currCode);
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    _sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
                    if(_drCollection.Count > 0)
                    {
                    	invCurrency = new InvCurrencyInfo();
                    	invCurrency.ReturnCode = CstError.GENERAL_SUCCESS;
                    	CurrencyInfo currInfo = new CurrencyInfo();
                    	currInfo.isoCode = _drCollection[0]["CurrencyCode"].ToString();
                    	currInfo.exchgRate = double.Parse(_drCollection[0]["ExchgRate"].ToString());
                    	currInfo.currUnit = int.Parse(_drCollection[0]["CurrencyUnit"].ToString());
                    	currInfo.currBase = bool.Parse(_drCollection[0]["Base"].ToString()) ? 1 : 0;
                    	invCurrency.CurrInfo = currInfo;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
                    #endregion
                }
                finally
                {
                    #region 6. Close DB Connection
                    DBUtil.Close(_sqlCon);
                    #endregion
                }
            }            
            return invCurrency;
		}
		#endregion
	}
}
