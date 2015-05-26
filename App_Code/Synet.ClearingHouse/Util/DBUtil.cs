using System;
using System.Data.SqlClient;
using System.Configuration;

using Synet.Common.Config;
using Synet.Common.Context;
using Synet.Common.Logs;

namespace Synet.ClearingHouse.Util
{
    /// <summary>
    /// Description of DBUtil.
    /// </summary>
    public class DBUtil
    {

        #region  FIELDS : CONSTANT >>>>>>>>>>>>>>>>>>>>>>>>>> (2)

        private const int SOURCE_188BET = 2;
        private const int SOURCE_SB1888 = 1;

        #endregion

        #region  FIELDS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>>> (1)

        protected static readonly ISysLog Log = SysLog.GetLogger(typeof(DBUtil).Name);

        #endregion

        #region  METHODS : STATIC >>>>>>>>>>>>>>>>>>>>>>>>>>> (2)

        public static void Close(SqlConnection sqlCon)
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
            }
        }

        public static SqlConnection GetConnection(String strConnectionString)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DBConfig.getProperty(strConnectionString, strConnectionString);
            return con;
        }

        #endregion

    }
}
