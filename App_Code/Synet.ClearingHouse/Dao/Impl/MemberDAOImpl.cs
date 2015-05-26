/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/16/2007
 * Time: 4:24 PM
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
using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Exceptions;

using Synet.Common.Logs;
using Synet.Common.Exceptions;

using objCrypto;

namespace Synet.ClearingHouse.Dao.Impl
{
	/// <summary>
	/// Description of MemberDAOImpl.
	/// </summary>
	public class MemberDAOImpl: Synet.ClearingHouse.Dao.IMemberDAO
	{
		#region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)
		protected static readonly ISysLog Log = SysLog.GetLogger(typeof(MemberDAOImpl).Name);
		
		#endregion
				
		#region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

		public MemberDAOImpl(){}
		
		#endregion
		
		#region  PROPERTIES : WRITE ONLY >>>>>>>>>>>>>>>>>>>> (1)

		#endregion
		
		#region  METHODS : PUBLIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (0)
		
		/*
		 * To check if member exist or not
		 */
		public bool CheckMemberExist(string memberCode)
		{
			Log.General("<MemberDAOImpl> : CheckMemberExist(" + memberCode + ")");
			
			if (memberCode == null) {
				return false;
			}
			
			#region 1. Set Variables			
			SqlDataReader reader;
            bool blnMemberExist = false;
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("SELECT 1 FROM dbo.tblMember(NOLOCK) WHERE memberCode = @memberCode", _sqlCon);
                _sqlComm.Parameters.Add("@memberCode", SqlDbType.VarChar);
                _sqlComm.Parameters["@memberCode"].Value = memberCode;
					
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    //to check if there 's any result set or not
                    blnMemberExist = reader.Read();
                    reader.Close();
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
            return blnMemberExist;            
		}
		
		/*
		 * To check if member exist or not
		 */
		public bool CheckAdminExist(string adminCode)
		{
			Log.General("<MemberDAOImpl> : CheckAdminExist(" + adminCode + ")");
			
			if (adminCode == null) {
				return false;
			}
			
			#region 1. Set Variables			
			SqlDataReader reader;
            bool blnAdminExist = false;
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("SELECT 1 FROM dbo.tblAdmin(NOLOCK) WHERE AdminCode = @adminCode", _sqlCon);
                _sqlComm.Parameters.Add("@adminCode", SqlDbType.VarChar);
                _sqlComm.Parameters["@adminCode"].Value = adminCode;
					
                #endregion

                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    //to check if there 's any result set or not
                    blnAdminExist = reader.Read();
                    reader.Close();
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
            return blnAdminExist;            
		}
		
		/*
		 * To return UserID when key in member Code 
		 */
		public int GetMemberIdByMemberCode(string memberCode)
        {
			Log.General("<MemberDAOImpl> : GetMemberIdByMemberCode(" + memberCode + ")");
			
			if (memberCode == null) {
				ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid User Code!");
				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid User Code!");
				throw ex;
			}
			
            #region 1. Set Variables
            SqlDataReader reader;
            int _intUserId = -1;
            #endregion
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("SELECT TOP 1 UserID FROM dbo.tblMember(NOLOCK) WHERE memberCode = @memberCode", _sqlCon);
                _sqlComm.Parameters.Add("@MemberCode", SqlDbType.VarChar);
                _sqlComm.Parameters["@MemberCode"].Value = memberCode;
                
                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                    	_intUserId = (int) reader.GetInt32(0);
                    }                    
                    reader.Close();
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
            if (_intUserId == -1)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	throw ex;
            }
            return _intUserId;
        }		
		
		/*
		 * To return UserID when key in member Code 
		 */
		public string GetMemberCodeByMemberId(int memberID)
        {
			Log.General("<MemberDAOImpl> : GetMemberCodeByMemberId(" + memberID + ")");
			
			#region 1. Set Variables
            SqlDataReader reader;
            string _memberCode = null;
            #endregion
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("SELECT TOP 1 MemberCode FROM dbo.tblMember(NOLOCK) WHERE UserId = @userId", _sqlCon);
                _sqlComm.Parameters.Add("@userId", SqlDbType.Int);
                _sqlComm.Parameters["@userId"].Value = memberID;
                
                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                    	_memberCode = reader.GetString(0);
                    }                    
                    reader.Close();
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
            
            if (_memberCode == null)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	throw ex;
            }
            return _memberCode;
        }
		
		/*
		 * To get member's churn value
		 */
		public ChurnResponse GetMemberChurn(string memberCode)
		{
			Log.General("<MemberDAOImpl> : GetMemberChurn(" + memberCode + ")");
			ChurnResponse churnObj = null;
			
			#region 1. Set Variables            
            //double churnValue = 0;
            #endregion
            
            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion
            
            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_GetMemberChurn", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.CommandTimeout = 90;

                _sqlComm.Parameters.AddWithValue("@MemberCode", memberCode);
                //_sqlComm.Parameters.Add("@retval", SqlDbType.Float).Direction = ParameterDirection.Output;
                 _sqlComm.Parameters.Add("@totalbet", SqlDbType.Float).Direction = ParameterDirection.Output;
                  _sqlComm.Parameters.Add("@totaldepositadj", SqlDbType.Float).Direction = ParameterDirection.Output;

                #endregion
                // start of arif
                //DataSet _ds 				= new DataSet();
				//SqlDataAdapter _sqlReader 	= null;
				
				try
				{
					_sqlCon.Open();
                    _sqlComm.ExecuteNonQuery();
                    churnObj = new  ChurnResponse();
                    //churnValue = (double) _sqlComm.Parameters["@retval"].Value;
                    //churnValue = churnValue * 100;
                    churnObj.totalbet = (double) _sqlComm.Parameters["@totalbet"].Value;
                    churnObj.totaldepositadj = (double) _sqlComm.Parameters["@totaldepositadj"].Value;
					
					/*
					_sqlCon.Open();
					_sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
					if (_drCollection.Count > 0)
					{						
						churnObj = new ChurnResponse();
						churnObj.returnCode = CstError.GENERAL_SUCCESS;
						churnObj.totalbet = double.Parse( _drCollection[0]["totalbet"].ToString());
						churnObj.totaldepositadj = double.Parse( _drCollection[0]["totaldepositadj"].ToString());
						*/
                        /*memInfo.returnCode = CstError.GENERAL_SUCCESS;
						memInfo.memberCode = _drCollection[0]["MemberCode"].ToString();
						memInfo.userID = int.Parse(_drCollection[0]["UserID"].ToString());
						memInfo.currCode = _drCollection[0]["CurrencyCode"].ToString();
						memInfo.email = _drCollection[0]["Email"].ToString();
						memInfo.websiteName = _drCollection[0]["WebsiteName"].ToString();
						memInfo.balance = double.Parse( _drCollection[0]["CreditAmt"].ToString());
                        if (_drCollection[0]["vendorID"].ToString() != null)
                            memInfo.vendorID = _drCollection[0]["vendorID"].ToString();
                        else
                            memInfo.vendorID = "0";
                        memInfo.country = _drCollection[0]["ISOCountryCodeAlp"].ToString();
						memInfo.firstname = _drCollection[0]["firstname"].ToString();
						memInfo.lastname = _drCollection[0]["lastname"].ToString();
						memInfo.verifyID = int.Parse(_drCollection[0]["Verified"].ToString());
						memInfo.dtupdatedmember = _drCollection[0]["dtupdatedmember"].ToString()=="" ? "" : DateTime.Parse(_drCollection[0]["dtupdatedmember"].ToString()).ToString("ddMMyyyy");
						memInfo.dtupdatedadmin = _drCollection[0]["dtupdatedadmin"].ToString()=="" ? "" : DateTime.Parse(_drCollection[0]["dtupdatedadmin"].ToString()).ToString("ddMMyyyy");
						*/
					//}					
				}
				catch(Exception ex)
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
                
                
                
                
                //end of arif
               /*
                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    _sqlComm.ExecuteNonQuery();
                    churnValue = (double) _sqlComm.Parameters["@retval"].Value;
                    churnValue = churnValue * 100;
                    #endregion
                } */
                /*
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
                } */
            }
            //return churnValue;
            return churnObj;
		}
		
		/*
		 * To get Member Information by Member ID
		 */
		public MemberInfo GetMemberInfoPersonal(int memberID)
		{
			Log.General("<MemberDAOImpl> : GetMemberInfoPersonal(" + memberID + ")");
			MemberInfo memInfo = null;
			
		    SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);

		    if (_sqlCon != null)
		    {
		    	SqlCommand _sqlComm 	= new SqlCommand("dbo.adm_GetMemberInfoPersonal", _sqlCon);
				_sqlComm.CommandType 	= CommandType.StoredProcedure;
			
				_sqlComm.Parameters.AddWithValue("@memberID", memberID);
			
				DataSet _ds 				= new DataSet();
				SqlDataAdapter _sqlReader 	= null;
				
				try
				{
					_sqlCon.Open();
					_sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
					if (_drCollection.Count > 0)
					{						
						memInfo = new MemberInfo();
						memInfo.returnCode = CstError.GENERAL_SUCCESS;
						memInfo.memberCode = _drCollection[0]["MemberCode"].ToString();
						memInfo.userID = int.Parse(_drCollection[0]["UserID"].ToString());
						memInfo.currCode = _drCollection[0]["CurrencyCode"].ToString();
						memInfo.email = _drCollection[0]["Email"].ToString();
						memInfo.websiteName = _drCollection[0]["WebsiteName"].ToString();
						memInfo.balance = double.Parse( _drCollection[0]["CreditAmt"].ToString());
                        if (_drCollection[0]["vendorID"].ToString() != null)
                            memInfo.vendorID = _drCollection[0]["vendorID"].ToString();
                        else
                            memInfo.vendorID = "0";
                        memInfo.country = _drCollection[0]["ISOCountryCodeAlp"].ToString();
						memInfo.firstname = _drCollection[0]["firstname"].ToString();
						memInfo.lastname = _drCollection[0]["lastname"].ToString();
						memInfo.verifyID = int.Parse(_drCollection[0]["Verified"].ToString());
						memInfo.dtupdatedmember = _drCollection[0]["dtupdatedmember"].ToString()=="" ? "" : DateTime.Parse(_drCollection[0]["dtupdatedmember"].ToString()).ToString("ddMMyyyy");
						memInfo.dtupdatedadmin = _drCollection[0]["dtupdatedadmin"].ToString()=="" ? "" : DateTime.Parse(_drCollection[0]["dtupdatedadmin"].ToString()).ToString("ddMMyyyy");						
						memInfo.mobileno  = _drCollection[0]["MobilePhone"].ToString();
						if(_drCollection[0]["Active"].ToString() == "True")
						    memInfo.active = "A";
						else
						    memInfo.active = "I";
						if(_drCollection[0]["ccverifyid"].ToString() == "True")
						    memInfo.ccverifyid = 1;
						else
						    memInfo.ccverifyid = 0;
					}					
				}
				catch(Exception ex)
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
		    if (memInfo == null)
		    {
		    	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Member does not exist!");
            	throw ex;
		    }
		    return memInfo;			
        }
		
		public bool GetVerifyDocumentStatus(int memberID, int docTypeId)
        {
            Log.General("<DocumentDAOImpl> : GetVerifyDocumentStatus");

            #region 1. Set Variables
            SqlDataReader reader;
            bool _blnDocument = false;            
            #endregion

            #region 2. Set SQL Connection
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            #endregion

            if (_sqlCon != null)
            {
                #region 3. Set Stored Proc and Parameters
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_GetVerifyDocStatus", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;

                _sqlComm.Parameters.Add("@UserID", SqlDbType.Int);
                _sqlComm.Parameters.Add("@DocTypeID", SqlDbType.SmallInt);
                _sqlComm.Parameters["@UserID"].Value = memberID;
                _sqlComm.Parameters["@DocTypeID"].Value = docTypeId;

                #endregion

                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                        _blnDocument = reader.GetBoolean(0);
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
            return _blnDocument;
        }
		
		public int GetCurrencyIDByCurrencyCode(string currencyCode)
		{
			Log.General("<MemberDAOImpl> : GetCurrencyIDByCurrencyCode(" + currencyCode + ")");
			
//			if (currencyCode == null) {
//				ClearingHouseException ex = new ClearingHouseException(CstError.INVALID_NULL_DATA, "Invalid Currency Code!");
//				ExceptionManager.ExceptionHandler(ex, CstError.INVALID_NULL_DATA, "Invalid Currency Code!");
//				throw ex;
//			}
			
            #region 1. Set Variables
            SqlDataReader reader;
            int _intCurrencyID = -1;
            #endregion
            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("currency_GetCurrencyID", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                
                _sqlComm.Parameters.Add("@CurrencyCode", SqlDbType.VarChar);
                _sqlComm.Parameters["@CurrencyCode"].Value = currencyCode;
                
                try
                {
                    #region 4. Retrieve Data
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    {
                    	_intCurrencyID = int.Parse(reader.GetSqlValue(0).ToString());
                    }                    
                    reader.Close();
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
            if (_intCurrencyID == -1)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.DB_OBJECT_NOT_FOUND, "Currency Code does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.DB_OBJECT_NOT_FOUND, "Currency Code does not exist!");
            	throw ex;
            }
            return _intCurrencyID;
		}
		
		public AdminInfo GetAdminUserInfo(string adminCode)
		{
			Log.General("<MemberDAOImpl> : GetAdminUserInfo(" + adminCode + ")");
			AdminInfo admInfo = null;
			
			SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
			
		    if (_sqlCon != null)
		    {
		    	SqlCommand _sqlComm 	= new SqlCommand("dbo.adm_GetAdminUser", _sqlCon);
				_sqlComm.CommandType 	= CommandType.StoredProcedure;
			
				_sqlComm.Parameters.AddWithValue("@adminCode", adminCode);
			
				DataSet _ds 				= new DataSet();
				SqlDataAdapter _sqlReader 	= null;
				
				try
				{
					_sqlCon.Open();
					_sqlReader = new SqlDataAdapter(_sqlComm);
					_sqlReader.Fill(_ds);
					
					DataRowCollection _drCollection = _ds.Tables[0].Rows;
					
					if (_drCollection.Count > 0)
					{
						admInfo = new AdminInfo();
						admInfo.returnCode = CstError.GENERAL_SUCCESS;
						admInfo.adminname = _drCollection[0]["AdminCode"].ToString();
						admInfo.admintypeid = int.Parse(_drCollection[0]["TypeID"].ToString());
					}					
				}
				catch(Exception ex)
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
		    if (admInfo == null)
            {
            	ClearingHouseException ex = new ClearingHouseException(CstError.MEMBER_NOT_FOUND, "Admin does not exist!");
            	ExceptionManager.ExceptionHandler(ex, CstError.MEMBER_NOT_FOUND, "Admin does not exist!");
            	throw ex;
            }
		    return admInfo;
        }
		
		public SignUpResponse GetListOfMemberSignUp(int month, int year)
        {
            Log.General("<MemberDAOImpl> : GetListOfMemberSignUp(" + month + ", " + year + ")");

            #region 1. Set Variables
            List<string> slistMemberSignUp = new List<string>();
            SignUpResponse signUpdaResObj = new SignUpResponse();
            SqlDataReader reader;            
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.GetListOfMemberSignUp", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.AddWithValue("@month", month);
                _sqlComm.Parameters.AddWithValue("@year", year);
                try
                {
                    #region 4. Retrieve Data

                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        slistMemberSignUp.Add(reader.GetSqlValue(0).ToString());
                    }
                    reader.Close();

                    signUpdaResObj.returnCode = CstError.GENERAL_SUCCESS;
                    signUpdaResObj.memberList = slistMemberSignUp;
                   
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
                    DBUtil.Close(_sqlCon);
                }
            }
            return signUpdaResObj;           
        }

        public AuthToken VerifyAuthenticationToken(string memberCode, string decryptedSessionID, string ipAddress, string oucode)
        {
            Log.General("<MemberDAOImpl> : VerifyAuthenticationToken(" + memberCode + "," + decryptedSessionID + "," + ipAddress + "," + oucode + ")");

            #region 1. Set Variables
            AuthToken auTokenObj = null;
            SqlDataReader reader;
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188_LOG);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.sec_verifyAuthenticationToken", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.AddWithValue("@memberCode", memberCode);
                _sqlComm.Parameters.AddWithValue("@sessionid", decryptedSessionID);
                _sqlComm.Parameters.AddWithValue("@ipaddress", ipAddress);
                try
                {
                    #region 4. Retrieve Data

                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    
                    auTokenObj = new AuthToken();
                    if (reader.Read())
                        auTokenObj.Passed = true;
                    else
                        auTokenObj.Passed = false;                    
                    auTokenObj.ReturnCode = CstError.GENERAL_SUCCESS;
                    reader.Close();

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
                    DBUtil.Close(_sqlCon);
                }
            }
            return auTokenObj;	
        }
		
        public int VerifyAdminToken(string adminCode, string sessionId, string ipAddress, string oucode)
        {
        	Log.General(String.Format("VerifyAdminToken({0}, {1}, {2}, {3})", adminCode, sessionId, ipAddress, oucode));

            #region 1. Set Variables
            int retValue = -1;
            SqlDataReader reader = null;
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_verifyAuthToken", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.AddWithValue("@AdminCode", adminCode);
                _sqlComm.Parameters.AddWithValue("@SessionID", sessionId);
                _sqlComm.Parameters.AddWithValue("@IPAddress", ipAddress);
                try
                {
                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    
                    if (reader.Read())
                    {
                		retValue = reader.GetInt32(0);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    #region 5. Log Error
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "General Datebase Error!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "General Datebase Error!", ex);
                    #endregion
                }
                finally
                {
                    DBUtil.Close(_sqlCon);
                }
            }
            return retValue;
        }

        public TotalMemberSignup GetTotalMemberSignup(int month, int year)
        {
            Log.General("<MemberDAOImpl> : GetTotalMemberSignup(" + month + ", " + year + ")");

            #region 1. Set Variables
            TotalMemberSignup totalMemsObj = new TotalMemberSignup();
            SqlDataReader reader;            
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188_STATISTIC);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.Get_totalMemberSignup", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.Add("@date", SqlDbType.DateTime);
                _sqlComm.Parameters["@date"].Value = new DateTime(year, month, 1);
                try
                {
                    #region 4. Retrieve Data

                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                    	MemsPerMonth mems = new MemsPerMonth();
                    	MemsPerMonth memsToDate = new MemsPerMonth();
                    	int memberCount = reader.GetInt32(1);
                    	string strMonth = reader.GetDateTime(0).ToString("MMyyyy");
                    	
                    	if (memberCount > 0)
                    	{
                    		mems.month = strMonth;
                    		mems.memNums = memberCount;
                    		
                    		memsToDate.month = strMonth;
                    		memsToDate.memNums = reader.GetInt32(2);
                    	
                        	totalMemsObj.currentMonthList.Add(mems);
                        	totalMemsObj.toDateList.Add(memsToDate);
                    	}
                    }
                    reader.Close();

                    totalMemsObj.returnCode = CstError.GENERAL_SUCCESS;
                                       
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
                    DBUtil.Close(_sqlCon);
                }
            }
            return totalMemsObj;           
        }
		
		public string GetSSLKey()
		{
			Log.General("<MemberDAOImpl> : GetSSLKey");
			
			#region 1. Set Variables
            string strKeyValue = null;
            SqlDataReader reader;
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.Mem_Bet188GetSSLKey", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                try
                {
                    #region 4. Retrieve Data

                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    if (reader.Read())
                    	strKeyValue = reader.GetSqlValue(0).ToString();                    
                    reader.Close();

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
                    DBUtil.Close(_sqlCon);
                }
            }
            return strKeyValue; 
		}
        
        public VerifyMemberResponse VerifyMemberCredentials(string memberCode, string oucode,string strPass)
        {
            Log.General("<MemberDAOImpl> : VerifyMemberCredentials(" + memberCode + ", " + oucode + "," + strPass + ")");

            #region 1. Set Variables
            VerifyMemberResponse veriMemResObj = new VerifyMemberResponse();
            SqlDataReader reader;
            int isValid = 0;
            #endregion

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188_PASS);
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.VerifyMemberCredentials", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.AddWithValue("@memberCode", memberCode);
                _sqlComm.Parameters.AddWithValue("@inputPass", strPass);
                try
                {
                    #region 4. Retrieve Data

                    _sqlCon.Open();
                    reader = _sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        isValid = int.Parse(reader.GetSqlValue(0).ToString());
                    }
                    reader.Close();

                    veriMemResObj.returnCode = CstError.GENERAL_SUCCESS;
                    if (isValid == 1)
                        veriMemResObj.valid = true;
                    else
                        veriMemResObj.valid = false;
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
                    DBUtil.Close(_sqlCon);
                }
            }
            return veriMemResObj;       
        }
        
		public UnverifyMemberResponse SetMemberUnverified(string strAdminCode, string strMemCode, string strEventTrigger)
		{
		    Log.General("<MemberDAOImpl> : SetMemberUnverified(" + strAdminCode + "," + strMemCode + "," + strEventTrigger + ")");
		    
		    UnverifyMemberResponse _unverifyMemRes = new UnverifyMemberResponse();
		    
		    SqlConnection _sqlConn = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
		    if(_sqlConn != null)
		    {
		        SqlCommand _sqlComm = new SqlCommand("dbo.adm_SetMemberUnverified", _sqlConn);
		        _sqlComm.CommandType = CommandType.StoredProcedure;
		        _sqlComm.Parameters.AddWithValue("@AdminCode", strAdminCode);
		        _sqlComm.Parameters.AddWithValue("@MemCode", strMemCode);
		        _sqlComm.Parameters.AddWithValue("@ETrigger", strEventTrigger);
		        
		        try
		        {
		            _sqlConn.Open();
                    _sqlComm.ExecuteNonQuery();
                    
                    _unverifyMemRes.returnCode = CstError.GENERAL_SUCCESS;
		        }
		        catch (Exception ex)
		        {
		            ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
		        }
		        finally
		        {
		            DBUtil.Close(_sqlConn);
		        }
		    }

		    return _unverifyMemRes;
		}

        public ExDocMemberList GetExpiredDocMemberList(string strDate)
        {
            Log.General("<MemberDAOImpl> : GetExpiredDocMemberList(" + strDate + ")");

            List<string> _lstMemberCode         = new List<string>();
            ExDocMemberList _objExDocMemList    = new ExDocMemberList();

            SqlConnection _sqlCon = DBUtil.GetConnection(CstConfig.CONSTR_BET188);
            
            if (_sqlCon != null)
            {
                SqlCommand _sqlComm = new SqlCommand("dbo.adm_GetExpiredDocuMemList", _sqlCon);
                _sqlComm.CommandType = CommandType.StoredProcedure;
                _sqlComm.Parameters.AddWithValue("@date", strDate);

                try
                {
                    _sqlCon.Open();
                    SqlDataReader reader = _sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        _lstMemberCode.Add(reader.GetSqlValue(0).ToString());
                    }
                    reader.Close();

                    _objExDocMemList.returnCode = CstError.GENERAL_SUCCESS;
                    _objExDocMemList.memberList = _lstMemberCode;
                }
                catch (Exception ex)
                {
                    ExceptionManager.ExceptionHandler(ex, CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!");
                    throw new ClearingHouseException(CstError.DB_CONNECTION_ERROR, "Cannot connect to ClearingHouse!", ex);
                }
                finally
                {
                    DBUtil.Close(_sqlCon);
                }
            }

            return _objExDocMemList;
        }
        #endregion
	}
}
