using JMDataServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMApplicationService
{
    public class SysExceptionApplicationService
    {

        ISysExceptionDataService _sysExceptionDataService;
        //ISysModuleDataService _sysModuleDataService;

        private ISysExceptionDataService SysExceptionDataService
        {
            get { return _sysExceptionDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysExceptionApplicationService(ISysExceptionDataService dataService)
        {
            _sysExceptionDataService = dataService;
        }



        /// <summary>
        /// GetSysException By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public SysException GetSysExceptionById(string Id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {                
                SysExceptionDataService.CreateSession();
                SysException sysException = SysExceptionDataService.GetSysExceptionById(Id);
                transaction.ReturnStatus = true;                
                return sysException;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                SysExceptionDataService.CloseSession();                
            }

        }


        /// <summary>
        /// SysException Inquiry  
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<SysExceptionInquiry> SysExceptionInquiry(string queryStr, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysExceptionDataService.CreateSession();
                List<SysExceptionInquiry> sysExceptionInquiry = SysExceptionDataService.SysExceptionInquiry(queryStr,paging);
                transaction.ReturnStatus = true;
                return sysExceptionInquiry;
            }
            catch (Exception ex)
            {
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
                return null;
            }
            finally
            {
                SysExceptionDataService.CloseSession();
            }

        }


        /// <summary>
        /// Create SysException
        /// </summary>
        /// <param name="sysException"></param>
        /// <param name="transaction"></param>
        public void CreateSysException(SysException sysException, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            //CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();

            try
            {
                SysExceptionDataService.CreateSession();

                //customerBusinessRules.ValidateCustomer(customer, CustomerDataService);

                //if (customerBusinessRules.ValidationStatus == true)
                //{
                SysExceptionDataService.BeginTransaction();
                SysExceptionDataService.CreateSysException(sysException);
                SysExceptionDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("SysException successfully created at " + sysException.CreateTime.ToString());
                //}
                //else
                //{
                //    transaction.ReturnStatus = customerBusinessRules.ValidationStatus;
                //    transaction.ReturnMessage = customerBusinessRules.ValidationMessage;
                //    transaction.ValidationErrors = customerBusinessRules.ValidationErrors;
                //}

            }
            catch (Exception ex)
            {
                SysExceptionDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysExceptionDataService.CloseSession();
            }

        }

        public void DeleteSysExceptionById(string Id,out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                SysExceptionDataService.CreateSession();

                SysExceptionDataService.BeginTransaction();
                SysExceptionDataService.DeleteSysExceptionById(Id);
                SysExceptionDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Id SysException deleted at " + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                SysExceptionDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysExceptionDataService.CloseSession();
            }

        }

    }
}
