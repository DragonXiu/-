using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace HILYCode
{
    class SqlData
    {
        private static object lockObj = new object();
        private static SqlConnection sqlConnection;
        #region 建立数据库连接
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            String server_string = ConfigurationManager.AppSettings["server_connect_string1"];
            sqlConnection = new SqlConnection(server_string);
            sqlConnection.Open();//打开数据库
            return sqlConnection;//返回sqlconnection对象的信息


        }
        #endregion
        #region 关闭数据库
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void CloseSqlConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)//判断数据库是否打开   
            {
                sqlConnection.Close();//关闭数据库
                sqlConnection.Dispose();//释放My_con变量的所有空间

            }
        }


        #endregion
        #region 读取指定表中的数据
        /// <summary>
        /// 读取指定表中的数据
        /// </summary>
        /// <param name="SqlStr">sql语句</param>
        /// <returns></returns>
        public static SqlDataReader GetSqlDataReader(string SqlStr)
        {
            GetSqlConnection();//打开数据的链接
            SqlCommand my_con = sqlConnection.CreateCommand();//创建一个sqlcommand对象，用于执行sql语句
            my_con.CommandText = SqlStr;//获取指定的sql语句
            SqlDataReader my_read = my_con.ExecuteReader();//执行sql语句，生成一个SQLDataReader对象
            return my_read;
        }
        #endregion
        #region 执行sql语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        public static void ExecutSqlCommend(string sqlstr)
        {
            lock (lockObj)
            {

                try
                {
                    GetSqlConnection();//打开数据库
                    SqlCommand sqlcom = new SqlCommand(sqlstr, sqlConnection);//创建一个sqlcommend独享，用于执行SQL语句
                    int result = sqlcom.ExecuteNonQuery();//执行sql语句，返回影响的行数
                    sqlcom.Dispose();//释放所有空间
                    CloseSqlConnection();//关闭与数据库的链接
                }
                catch (Exception ex)
                {


                }

            }

        }
        #endregion
        #region 创建DataSet对象
        /// <summary>
        /// 创建DataSet
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlstr, string tablename)
        {
            lock (lockObj)
            {
                DataSet my_dataset = new DataSet();//创建dataset对象
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        GetSqlConnection();
                        SqlDataAdapter sqldata = new SqlDataAdapter(sqlstr, sqlConnection);
                        sqldata.Fill(my_dataset, tablename);
                        CloseSqlConnection();
                        return my_dataset;//返回dataset对象的信息
                    }
                    catch (Exception)
                    {


                    }
                }
                return my_dataset;
            }

        }
        #endregion
    }
}
