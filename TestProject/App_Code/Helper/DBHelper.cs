using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Oracle.DataAccess.Client;
using System.Web.Configuration;
using System.Data.Common;
using System.Collections;

/// <summary>
/// Summary description for DBHelper
/// </summary>
namespace Agile.Helper {
    public class DBHelper {
        #region ConnectionString, Database
        public static string ConnString {
            get {
                return WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }

        private static OracleConnection getConnection() {
            OracleConnection retVal = new OracleConnection(DBHelper.ConnString);
            retVal.Open();
            return retVal;
        }

        private static void disposeConnection(OracleConnection con) {
            con.Close();
            con.Dispose();
            con = null;
        }
        #endregion

        #region SQL
        public static DataTable SelectDataTable(string sql, params object[] paramList) {

            DataTable dt = new DataTable();
            OracleConnection con = getConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            addParamters(cmd, paramList);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);
            cmd.Dispose();
            disposeConnection(con);
            return dt;
        }

        public static DataSet SelectDataSet(string sql, params object[] paramList) {

            DataSet ds = new DataSet();
            OracleConnection con = getConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            addParamters(cmd, paramList);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
            cmd.Dispose();
            disposeConnection(con);
            return ds;
        }

        public static String SelectString(string sql, params object[] paramList) {
            string returnVal = "";
            OracleConnection con = getConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            addParamters(cmd, paramList);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if (!reader.GetOracleString(0).IsNull)
                    returnVal = reader.GetString(0);
            }

            reader.Close();
            cmd.Dispose();
            disposeConnection(con);
            return returnVal;
        }
        public static Decimal SelectDecimal(string sql, params object[] paramList) {
            decimal returnVal = 0;
            OracleConnection con = getConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            addParamters(cmd, paramList);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                returnVal = reader.GetDecimal(0);
            }

            reader.Close();
            cmd.Dispose();
            disposeConnection(con);
            return returnVal;
        }
        public static decimal SelectCount(string sql, params object[] paramList) {
            OracleConnection con = getConnection();
            OracleTransaction tx = con.BeginTransaction();
            OracleCommand cmd = con.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = sql;
            addParamters(cmd, paramList);
            cmd.ExecuteNonQuery();
            decimal cnt = (decimal)cmd.ExecuteScalar();
            tx.Commit();
            tx.Dispose();
            cmd.Dispose();
            disposeConnection(con);
            return cnt;
        }
        public static void Execute(string sql, params object[] paramList) {
            OracleConnection con = getConnection();
            OracleTransaction tx = con.BeginTransaction();
            OracleCommand cmd = con.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = sql;
            addParamters(cmd, paramList);
            cmd.ExecuteNonQuery();
            tx.Commit();
            tx.Dispose();
            cmd.Dispose();
            disposeConnection(con);
        }



        public static void CallSotredProc(string procNme, params object[] paramList) {
            OracleConnection con = getConnection();
            OracleTransaction tx = con.BeginTransaction();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procNme;
            addParamters(cmd, paramList);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            disposeConnection(con);
        }
        #endregion

        #region mp - Make Parameter
        public static OracleParameter mp(string PName, object PValue) {
            OracleParameter prm = new OracleParameter(PName, PValue);
            prm.Direction = ParameterDirection.Input;
            return prm;
        }

        public static DictionaryEntry spmp(string PName, object PValue) {
            DictionaryEntry prm = new DictionaryEntry(PName, PValue);
            return prm;
        }
        private static void addParamters(OracleCommand cmd, object[] paramList) {
            foreach (OracleParameter param in paramList)
                cmd.Parameters.Add(param);
        }
        #endregion
    }
}