using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;//配置文件包
namespace DAL
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    public class DBHelper
    {
        #region SqlConnection
        /// <summary>
        /// SqlConnection
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConn()
        {
           // string st ="server=.;database=JXC_db;uid=hhy;pwd=123456;" ;
            string st = ConfigurationManager.ConnectionStrings["PPWContext"] == null ? null : ConfigurationManager.ConnectionStrings["PPWContext"].ConnectionString; 
            //  return new SqlConnection(ConfigurationManager.AppSettings["JXC_db"]);ConfigurationSettings
            return new SqlConnection(st);
        }
        #endregion
    }

    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class SQLHelper : DBHelper
    {
        #region ADO.NET 组件
        private SqlConnection conn;         //数据库连接对象
        private SqlCommand command;         //数据库命令对象
        private SqlDataAdapter dataAdapter; //填充对象
        private DataSet dataSet;            //数据集
        #endregion

        #region [SQL语句]增.删.改.查
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public int RunSQL(string sql)
        {
            int num = 0;
            try
            {
                conn = GetConn();
                //打开连接
                conn.Open();
                command = new SqlCommand(sql, conn);
                num = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                conn.Close();
            }
            return num;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            try
            {
                conn = GetConn();
                dataAdapter = new SqlDataAdapter(sql,conn);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

            }
            catch (Exception ex)
            {
                conn.Close();      
                throw ex;
            }
            return dataSet;
        } 
        #endregion

        #region [存储过程]增.删.改.查
        /// <summary>
        /// 增.删.改.方法
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="sp">参数</param>
        /// <returns></returns>
        public int RunSQL(string procName, SqlParameter[] sp)
        {
            int i = 0;

            try
            {
                conn = GetConn();
                //打开数据库
                conn.Open();
                //实例化 command
                command = new SqlCommand(procName, conn);
                //指定指令类型
                command.CommandType = CommandType.StoredProcedure;

                //获得存储过程输入参数
                if (sp != null)
                {
                    foreach (SqlParameter para in sp)
                    {
                        command.Parameters.Add(para);
                    }
                }

                //执行
                i = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库
                conn.Close();
            }

            return i;
        }

        /// <summary>
        /// 查询.方法
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet(string procName, SqlParameter[] sp)
        {
            try
            {
                conn = GetConn();
                //实例化 command
                command = new SqlCommand(procName, conn);
                //指定指令类型
                command.CommandType = CommandType.StoredProcedure;

                //获得存储过程输入参数
                if (sp != null)
                {
                    foreach (SqlParameter para in sp)
                    {
                        command.Parameters.Add(para);
                    }
                }

                //实例化 dataAdapter
                dataAdapter = new SqlDataAdapter();
                //让 dataAdapter 与 command 关联
                dataAdapter.SelectCommand = command;
                //实例化 dataSet
                dataSet = new DataSet();
                //填充 dataSet
                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }
        #endregion

       //从配置文件中读取连接字符串
      //  private static readonly string _connString = ConfigurationManager.AppSettings["JXC_db"].ToString();
      //  private static readonly string _connString = "server=.;database=JXC_db;uid=hhy;pwd=123456;";
        private static readonly string _connString = ConfigurationManager.ConnectionStrings["PPWContext"] == null ? null : ConfigurationManager.ConnectionStrings["PPWContext"].ConnectionString; 
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnString
        {
            get { return SQLHelper._connString; }
        }

        /// <summary>
        /// 执行命令前，预先设置Command对象
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmd">要设置的Command对象</param>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型(文本、存储过程等)</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">命令参数数组</param>
        private void PrepareCommand(SqlTransaction tran, SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            if (tran != null)
            {
                cmd.Transaction = tran;
            }

            //设置Command对象
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 执行增、删、改操作，返回受影响的行数
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParameters">参数数组</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(SqlTransaction tran, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
        {
            int rowcount = -1;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(tran, cmd, conn, cmdType, cmdText, cmdParameters);
                rowcount = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return rowcount;
        }

        /// <summary>
        /// 执行查询，返回首行首列
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns>Object类型的值</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                PrepareCommand(null, cmd, conn, cmdType, cmdText, cmdParams);
                Object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行查询，返回包含结果集的SqlDataReader对象
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns>包含结果集的SqlDataReader对象</returns>
        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);

            //这里用try-catch块是为了避免该方法引发异常时不会关闭连接
            try
            {
                PrepareCommand(null, cmd, conn, cmdType, cmdText, cmdParams);
                SqlDataReader dRead = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dRead;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParams">命令参数数组</param>
        /// <returns>包含数组的DataSet对象</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParams)
        {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(null, cmd, conn, cmdType, cmdText, cmdParams);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
        }

        public DataSet RunSqlBySel(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql,GetConn());
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataSet RunSQLBySel(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, GetConn());
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

    }
}
