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

    //管理树菜单接口
    public interface ISysModuleDataService : IDataService, IDisposable
    {
        //void CreateCustomer(SysModule sysModule);
        //void UpdateCustomer(SysModule sysModule);
        SysModule GetSysModuleBySysModuleID(string sysModuleID);
        //List<SysModuleInquiry> GetSysModuleBySysModuleID(string sysModuleID);
        //Customer GetCustomerByCustomerID(Guid customerID);
        //List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        List<SysModuleInquiry> SysModuleInquiry();
        List<SysModuleInquiry> SysModuleInquiry(string sysModuleId);
        //List<PaymentType> GetPaymentTypes();
        //PaymentType GetPaymentType(Guid paymentTypeID);
        //void DeleteAllCustomers();

    }


    //系统异常
    public interface ISysLogDataService : IDataService, IDisposable
    {
        void CreateSysLog(SysLog SysLog);
        //void UpdateCustomer(SysModule sysModule);
        SysLog GetSysLogById(string Id);
        List<SysLogList> GetSysLogList();
        //List<SysModuleInquiry> GetSysModuleBySysModuleID(string sysModuleID);
        //Customer GetCustomerByCustomerID(Guid customerID);
        //List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        //List<SysModuleInquiry> SysModuleInquiry();
        //List<SysModuleInquiry> SysModuleInquiry(string sysModuleId);
        //List<PaymentType> GetPaymentTypes();
        //PaymentType GetPaymentType(Guid paymentTypeID);
        void DeleteSysLogById(string Id);
        

    }


    //public interface ISysLogRepository
    //{
    //    int Create(SysLog entity);
    //    void Delete(DBContainer db, string[] deleteCollection);
    //    IQueryable<SysLog> GetList(DBContainer db);
    //    SysLog GetById(string id);
    //}


    //SysSample
    public interface ISysSampleDataService : IDataService, IDisposable
    {
        void CreateSysSample(SysSample sysSample);
        void UpdateSysSample(SysSample sysSample);
        SysSample GetSysSampleById(string Id);
        List<SysSampleInquiry> SysSampleInquiry(DataGridPagingInformation paging);
        //List<SysModuleInquiry> GetSysModuleBySysModuleID(string sysModuleID);
        //Customer GetCustomerByCustomerID(Guid customerID);
        //List<CustomerInquiry> CustomerInquiry(string firstName, string lastName, DataGridPagingInformation paging);
        //List<SysModuleInquiry> SysModuleInquiry();
        //List<SysModuleInquiry> SysModuleInquiry(string sysModuleId);
        //List<PaymentType> GetPaymentTypes();
        //PaymentType GetPaymentType(Guid paymentTypeID);
        void DeleteSysSampleById(string Id);


    }


}
