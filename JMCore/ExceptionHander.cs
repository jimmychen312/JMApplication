using System;
using System.Web.Configuration;
using JMModels;
using System.IO;
using System.Text;
using JMCommon;
using JMDataServiceInterface;
using JMApplicationService;

namespace JMCore
{

    /// <summary>
    /// 写入一个异常错误
    /// </summary>
    /// <param name="ex">异常</param>
    public  class ExceptionHander
    {
        private ISysExceptionDataService _sysExceptionDataService;

        public ISysExceptionDataService SysExceptionDataService
        {
            get { return _sysExceptionDataService; }
        }

        public ExceptionHander(ISysExceptionDataService dataService)
        {
            _sysExceptionDataService = dataService;
        }

        /// <summary>
        /// 加入异常日志
        /// </summary>
        /// <param name="ex">异常</param>
        public void WriteException(Exception ex)
        {

            try
            {
                TransactionalInformation transaction;

                //SysExceptionMaintenanceViewModel sysExceptionMaintenanceViewModel = new SysExceptionMaintenanceViewModel();

                SysException model = new SysException()
                {
                    Id = ResultHelper.NewId,
                    HelpLink = ex.HelpLink,
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite.ToString(),
                    Data = ex.Data.ToString(),
                    CreateTime = ResultHelper.NowTime

                };
                //ModelStateHelper.UpdateViewModel(sysExceptionDTO, sysException);

                //SysExceptionDataService.BeginTransaction();
                //SysExceptionDataService.CreateSysException(sysException);
                //SysExceptionDataService.CommitTransaction(true);

                SysExceptionApplicationService sysExceptionApplicationService = new SysExceptionApplicationService(SysExceptionDataService);
                sysExceptionApplicationService.CreateSysException(model, out transaction);


                //using (DBContainer db = new DBContainer())
                //{
                //    SysException model = new SysException()
                //    {
                //        Id = ResultHelper.NewId,
                //        HelpLink = ex.HelpLink,
                //        Message = ex.Message,
                //        Source = ex.Source,
                //        StackTrace = ex.StackTrace,
                //        TargetSite = ex.TargetSite.ToString(),
                //        Data = ex.Data.ToString(),
                //        CreateTime = ResultHelper.NowTime

                //    };
                //    db.SysException.AddObject(model);
                //    db.SaveChanges();
                //}
            }
            catch (Exception ep)
            {
                try
                {
                    //异常失败写入txt
                    string path = @"~/exceptionLog.txt";
                    string txtPath = System.Web.HttpContext.Current.Server.MapPath(path);//获取绝对路径
                    using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                    {
                        sw.WriteLine((ex.Message + "|" + ex.StackTrace + "|" + ep.Message + "|" + DateTime.Now.ToString()).ToString());
                        sw.Dispose();
                        sw.Close();
                    }
                    return;
                }
                catch { return; }
            }



        }
    }


}

 