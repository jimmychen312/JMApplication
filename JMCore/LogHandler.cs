using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JMCommon;
//using App.DAL;
//using App.IBLL;
using JMAdoDataAccess;
using JMModels;
//using Microsoft.Practices.Unity;
using JMDataServiceInterface;
using JMApplicationService;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JMCore

{
    public class LogHandler
    {

        ISysLogDataService sysLogDataService;
        
        ///// <summary>
        ///// Constructor with Dependency Injection using Ninject
        ///// </summary>
        ///// <param name="dataService"></param>
        //public LogHandler(ISysLogDataService dataService)
        //{
        //    sysLogDataService = dataService;
        //}
        

        //[Dependency]
        //public static ISysLogBLL logBLL { get; set; }
        

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="oper">操作人</param>
        /// <param name="mes">操作信息</param>
        /// <param name="result">结果</param>
        /// <param name="type">类型</param>
        /// <param name="module">操作模块</param>
        public void WriteServiceLog(string oper, string mes, string result, string type, string module)
        {
            TransactionalInformation transaction;

            //SysLogMaintenanceViewModel sysLogMaintenanceViewModel = new SysLogMaintenanceViewModel();

            SysLog sysLog = new SysLog();
            sysLog.Id = ResultHelper.NewId;
            sysLog.Operator = oper;
            sysLog.Message = mes;
            sysLog.Result = result;
            sysLog.Type = type;
            sysLog.Module = module;
            sysLog.CreateTime = ResultHelper.NowTime;
            //ModelStateHelper.UpdateViewModel(sysLogDTO, sysLog);

            //SysLogDataService.BeginTransaction();
            //SysLogDataService.CreateSysLog(sysLog);
            //SysLogDataService.CommitTransaction(true);

            SysLogApplicationService sysLogApplicationService = new SysLogApplicationService(sysLogDataService);
            sysLogApplicationService.CreateSysLog(sysLog, out transaction);

            //sysLogMaintenanceViewModel.SysLog = sysLog;
            //sysLogMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            //sysLogMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            //sysLogMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;


            //SysLog entity = new SysLog();
            //entity.Id = ResultHelper.NewId;
            //entity.Operator = oper;
            //entity.Message = mes;
            //entity.Result = result;
            //entity.Type = type;
            //entity.Module = module;
            //entity.CreateTime = ResultHelper.NowTime;
            //using (SysLogApplicationService logRepository = new SysLogRepository())
            //{
            //    logRepository.Create(entity);
            //}

        }
    }

    public static class DB
    {

        private static SqlConnection connection = null;
        private static SqlCommand command = null;
        private static SqlDataAdapter adapter = null;
        private static string errorString;
        public static string ErrorString
        {
            get { return errorString; }
        }

        /// <summary>  
        /// 初始化  
        /// </summary>  
        public static void Init(string ConnectionString)
        {
            connection = new SqlConnection(ConnectionString);
            command = connection.CreateCommand();
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
        }

        /// <summary>  
        /// 打开连接  
        /// </summary>  
        /// <returns></returns>  
        public static bool Connect()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 关闭连接  
        /// </summary>  
        /// <returns></returns>  
        public static bool CloseConnect()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 根据查询语句从数据库检索数据  
        /// </summary>  
        /// <param name="strSelect">查询语句</param>  
        /// <returns>有数据则返回DataTable对象，否则返回null</returns>  
        public static DataTable DTSelect(string SelectString)
        {
            errorString = "";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.SelectCommand.CommandText = SelectString;
                DataTable myDT = new DataTable();
                adapter.Fill(myDT);
                return myDT;
            }
            catch (Exception ex)
            {
                errorString = "数据查询失败：" + ex.Message;
                return null;
            }
        }

        /// <summary>  
        /// 根据Sql语句更新数据库  
        /// </summary>  
        /// <param name="UpdataString">更新语句</param>  
        /// <returns>更新成功则返回true</returns>  
        public static bool dbUpdate(string UpdataString)
        {
            errorString = "";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.CommandType = CommandType.Text;
                command.CommandText = UpdataString;
                int intCount = command.ExecuteNonQuery();
                return intCount > 0;
            }
            catch (Exception ex)
            {
                errorString = "更新数据库失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="SQLStringList">多条SQL语句</param>          
        public static bool ExecuteSqlTran(ArrayList SQLStringList)
        {

            SqlTransaction tx = connection.BeginTransaction();
            command.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        command.CommandText = strsql;
                        command.ExecuteNonQuery();
                    }
                }
                tx.Commit();
                return true;
            }
            catch (Exception Etranca)
            {
                tx.Rollback();
                errorString = "数据库操作失败：" + Etranca.Message;
                return false;
            }

        }

    }
}



