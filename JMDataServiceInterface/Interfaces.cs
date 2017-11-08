using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMDataServiceInterface
{
    public interface IDataService
    {
        void CreateSession();
        void BeginTransaction();
        void CommitTransaction(Boolean closeSession);
        void RollbackTransaction(Boolean closeSession);
        void CloseSession();
    }

    public interface ICustomerDataService : IDataService, IDisposable
    {
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer GetCustomerByCustomerID(Guid customerID);
        List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        List<PaymentType> GetPaymentTypes();
        PaymentType GetPaymentType(Guid paymentTypeID);
        void DeleteAllCustomers();

    }

    //SysSample
    public interface ISysSampleDataService : IDataService, IDisposable
    {
        void CreateSysSample(SysSample sysSample);
        void UpdateSysSample(SysSample sysSample);
        SysSample GetSysSampleById(string Id);
        List<SysSampleInquiry> SysSampleInquiry(string queryStr, DataGridPagingInformation paging);
        void DeleteSysSampleById(string Id);
    }


    //管理树菜单接口
    public interface ISysModuleDataService : IDataService, IDisposable
    {       
        SysModule GetSysModuleBySysModuleID(string sysModuleID);
        List<SysModuleInquiry> SysModuleInquiry();
        List<SysModuleInquiry> SysModuleInquiry(string sysModuleId);        
    }


    //系统日志
    public interface ISysLogDataService : IDataService, IDisposable
    {
        void CreateSysLog(SysLog SysLog);       
        SysLog GetSysLogById(string Id);
        List<SysLogInquiry> SysLogInquiry(string queryStr, DataGridPagingInformation paging);       
        void DeleteSysLogById(string Id);
    }


    //系统异常
    public interface ISysExceptionDataService : IDataService, IDisposable
    {
        void CreateSysException(SysException SysException);
        SysException GetSysExceptionById(string Id);
        List<SysExceptionInquiry> SysExceptionInquiry(string queryStr, DataGridPagingInformation paging);
        void DeleteSysExceptionById(string Id);
    }

}
