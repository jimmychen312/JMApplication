﻿using JMDataServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMApplicationService
{
    public class SysLogApplicationService
    {

        ISysLogDataService _sysLogDataService;
        
        private ISysLogDataService SysLogDataService
        {
            get { return _sysLogDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysLogApplicationService(ISysLogDataService dataService)
        {
            _sysLogDataService = dataService;
        }



        /// <summary>
        /// GetSysLog By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public SysLog GetSysLogById(string Id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {                
                SysLogDataService.CreateSession();
                SysLog sysLog = SysLogDataService.GetSysLogById(Id);
                transaction.ReturnStatus = true;                
                return sysLog;
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
                SysLogDataService.CloseSession();                
            }

        }


        /// <summary>
        /// SysLog Inquiry  
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<SysLogInquiry> SysLogInquiry(string queryStr, DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysLogDataService.CreateSession();
                List<SysLogInquiry> sysLogInquiry = SysLogDataService.SysLogInquiry(queryStr,paging);
                transaction.ReturnStatus = true;
                return sysLogInquiry;
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
                SysLogDataService.CloseSession();
            }

        }


        /// <summary>
        /// Create SysLog
        /// </summary>
        /// <param name="sysLog"></param>
        /// <param name="transaction"></param>
        public void CreateSysLog(SysLog sysLog, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            //CustomerBusinessRules customerBusinessRules = new CustomerBusinessRules();

            try
            {
                SysLogDataService.CreateSession();

                //customerBusinessRules.ValidateCustomer(customer, CustomerDataService);

                //if (customerBusinessRules.ValidationStatus == true)
                //{
                SysLogDataService.BeginTransaction();
                SysLogDataService.CreateSysLog(sysLog);
                SysLogDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("SysLog successfully created at " + sysLog.CreateTime.ToString());
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
                SysLogDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysLogDataService.CloseSession();
            }

        }

        public void DeleteSysLogById(string Id,out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                SysLogDataService.CreateSession();

                SysLogDataService.BeginTransaction();
                SysLogDataService.DeleteSysLogById(Id);
                SysLogDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Id SysLog deleted at " + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                SysLogDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysLogDataService.CloseSession();
            }

        }

    }
}
