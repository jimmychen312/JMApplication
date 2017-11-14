using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{

    public class SysModuleApplicationService
    {
        ISysModuleDataService _sysModuleDataService;

        private ISysModuleDataService SysModuleDataService
        {
            get { return _sysModuleDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysModuleApplicationService(ISysModuleDataService dataService)
        {
            _sysModuleDataService = dataService;
        }

        /// <summary>
        /// SysModule Inquiry By Id
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="personId"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<SysModuleInquiry> GetMenuByPersonId(string personId, string ParentId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysModuleDataService.CreateSession();
                List<SysModuleInquiry> sysModules = SysModuleDataService.GetMenuByPersonId(personId, ParentId);
                transaction.ReturnStatus = true;
                return sysModules;
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
                SysModuleDataService.CloseSession();
            }

        }

        ///// <summary>
        ///// Get SysModule By SysModule ID
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="personId"></param>
        ///// <param name="sysModuleID"></param>
        ///// <returns></returns>        
        //public SysModule GetSysModuleBySysModuleID(string personId,string sysModuleID, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();

        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        SysModule sysModule = SysModuleDataService.GetSysModuleBySysModuleID(personId,sysModuleID);
        //        transaction.ReturnStatus = true;
        //        return sysModule;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //        return null;
        //    }
        //    finally
        //    {
        //        SysModuleDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// SysModule Inquiry
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //public List<SysModuleInquiry> SysModuleInquiry(string personId,out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        SysModuleDataService.CreateSession();
        //        List<SysModuleInquiry> sysModules = SysModuleDataService.SysModuleInquiry(personId);
        //        transaction.ReturnStatus = true;
        //        return sysModules;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //        return null;
        //    }
        //    finally
        //    {
        //        SysModuleDataService.CloseSession();
        //    }

        //}



    }
}


//using JMDataServiceInterface;
//using JMModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JMApplicationService
//{

//    public class SysModuleApplicationService
//    {

//        ISysModuleDataService _sysModuleDataService;  

//        private ISysModuleDataService SysModuleDataService
//        {
//            get { return _sysModuleDataService; }
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public SysModuleApplicationService(ISysModuleDataService dataService)
//        {
//            _sysModuleDataService = dataService;
//        }


//        /// <summary>
//        /// Get SysModule By SysModule ID
//        /// </summary>
//        /// <param name="sysModuleID"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public SysModule GetSysModuleBySysModuleID(String sysModuleID, out TransactionalInformation transaction)
//        {
//            transaction = new TransactionalInformation();

//            try
//            {
//                SysModuleDataService.CreateSession();
//                SysModule sysModule = SysModuleDataService.GetSysModuleBySysModuleID(sysModuleID);
//                transaction.ReturnStatus = true;
//                return sysModule;
//            }
//            catch (Exception ex)
//            {
//                transaction.ReturnMessage = new List<string>();
//                string errorMessage = ex.Message;
//                transaction.ReturnStatus = false;
//                transaction.ReturnMessage.Add(errorMessage);
//                return null;
//            }
//            finally
//            {
//                SysModuleDataService.CloseSession();
//            }

//        }

//        /// <summary>
//        /// SysModule Inquiry
//        /// </summary>
//        /// <param name="firstName"></param>
//        /// <param name="lastName"></param>
//        /// <param name="paging"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public List<SysModuleInquiry> SysModuleInquiry(out TransactionalInformation transaction)
//        {
//            transaction = new TransactionalInformation();
//            try
//            {
//                SysModuleDataService.CreateSession();
//                List<SysModuleInquiry> sysModules = SysModuleDataService.SysModuleInquiry();
//                transaction.ReturnStatus = true;
//                return sysModules;
//            }
//            catch (Exception ex)
//            {
//                transaction.ReturnMessage = new List<string>();
//                string errorMessage = ex.Message;
//                transaction.ReturnStatus = false;
//                transaction.ReturnMessage.Add(errorMessage);
//                return null;
//            }
//            finally
//            {
//                SysModuleDataService.CloseSession();
//            }

//        }

//        ///// <summary>
//        ///// Get Payment Types
//        ///// </summary>
//        ///// <param name="transaction"></param>
//        ///// <returns></returns>
//        //public List<PaymentType> GetPaymentTypes(out TransactionalInformation transaction)
//        //{
//        //    transaction = new TransactionalInformation();
//        //    try
//        //    {
//        //        SysModuleDataService.CreateSession();
//        //        List<PaymentType> paymentTypes = SysModuleDataService.GetPaymentTypes();
//        //        transaction.ReturnStatus = true;
//        //        return paymentTypes;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //        return null;
//        //    }
//        //    finally
//        //    {
//        //        SysModuleDataService.CloseSession();
//        //    }

//        //}

//        ///// <summary>
//        ///// Create SysModule
//        ///// </summary>
//        ///// <param name="sysModule"></param>
//        ///// <param name="transaction"></param>
//        //public void CreateSysModule(SysModule sysModule, out TransactionalInformation transaction)
//        //{
//        //    transaction = new TransactionalInformation();
//        //    SysModuleBusinessRules sysModuleBusinessRules = new SysModuleBusinessRules();

//        //    try
//        //    {
//        //        SysModuleDataService.CreateSession();

//        //        sysModuleBusinessRules.ValidateSysModule(sysModule, SysModuleDataService);

//        //        if (sysModuleBusinessRules.ValidationStatus == true)
//        //        {
//        //            SysModuleDataService.BeginTransaction();
//        //            SysModuleDataService.CreateSysModule(sysModule);
//        //            SysModuleDataService.CommitTransaction(true);
//        //            transaction.ReturnStatus = true;
//        //            transaction.ReturnMessage.Add("SysModule successfully created at " + sysModule.DateCreated.ToString());
//        //        }
//        //        else
//        //        {
//        //            transaction.ReturnStatus = sysModuleBusinessRules.ValidationStatus;
//        //            transaction.ReturnMessage = sysModuleBusinessRules.ValidationMessage;
//        //            transaction.ValidationErrors = sysModuleBusinessRules.ValidationErrors;
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SysModuleDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        SysModuleDataService.CloseSession();
//        //    }

//        //}

//        ///// <summary>
//        ///// Update SysModule
//        ///// </summary>
//        ///// <param name="sysModule"></param>
//        ///// <param name="transaction"></param>
//        //public void UpdateSysModule(SysModule sysModule, out TransactionalInformation transaction)
//        //{

//        //    transaction = new TransactionalInformation();
//        //    SysModuleBusinessRules sysModuleBusinessRules = new SysModuleBusinessRules();

//        //    try
//        //    {

//        //        SysModuleDataService.CreateSession();
//        //        sysModuleBusinessRules.ValidateSysModule(sysModule, SysModuleDataService);

//        //        if (sysModuleBusinessRules.ValidationStatus == true)
//        //        {
//        //            SysModule originalSysModuleInformation = SysModuleDataService.GetSysModuleBySysModuleID(sysModule.SysModuleID);
//        //            PopulateSysModuleInformation(sysModule, originalSysModuleInformation);
//        //            SysModuleDataService.BeginTransaction();
//        //            SysModuleDataService.UpdateSysModule(originalSysModuleInformation);
//        //            SysModuleDataService.CommitTransaction(true);
//        //            transaction.ReturnStatus = true;
//        //            transaction.ReturnMessage.Add("SysModule successfully updated at " + DateTime.Now.ToString());
//        //        }
//        //        else
//        //        {
//        //            transaction.ReturnStatus = sysModuleBusinessRules.ValidationStatus;
//        //            transaction.ReturnMessage = sysModuleBusinessRules.ValidationMessage;
//        //            transaction.ValidationErrors = sysModuleBusinessRules.ValidationErrors;
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SysModuleDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        SysModuleDataService.CloseSession();
//        //    }


//        //}

//        //public void DeleteAllSysModules(out TransactionalInformation transaction)
//        //{
//        //    transaction = new TransactionalInformation();

//        //    try
//        //    {

//        //        SysModuleDataService.CreateSession();

//        //        SysModuleDataService.BeginTransaction();
//        //        SysModuleDataService.DeleteAllSysModules();
//        //        SysModuleDataService.CommitTransaction(true);
//        //        transaction.ReturnStatus = true;
//        //        transaction.ReturnMessage.Add("All sysModules deleted at " + DateTime.Now.ToString());

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        SysModuleDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        SysModuleDataService.CloseSession();
//        //    }

//        //}


////        /// <summary>
////        /// Populate SysModule Information
////        /// </summary>
////        /// <param name="sysModule"></param>
////        /// <param name="originalSysModuleInformation"></param>
////        private void PopulateSysModuleInformation(SysModule sysModule, SysModule originalSysModuleInformation)
////        {
////            originalSysModuleInformation.FirstName = sysModule.FirstName;
////            originalSysModuleInformation.LastName = sysModule.LastName;
////            originalSysModuleInformation.CreditCardNumber = sysModule.CreditCardNumber;
////            originalSysModuleInformation.CreditCardSecurityCode = sysModule.CreditCardSecurityCode;
////            originalSysModuleInformation.CreditLimit = sysModule.CreditLimit;
////            originalSysModuleInformation.Address = sysModule.Address;
////            originalSysModuleInformation.City = sysModule.City;
////            originalSysModuleInformation.Country = sysModule.Country;
////            originalSysModuleInformation.PhoneNumber = sysModule.PhoneNumber;
//////            originalSysModuleInformation.TelePhone = sysModule.TelePhone;
////            originalSysModuleInformation.PostalCode = sysModule.PostalCode;
////            originalSysModuleInformation.Region = sysModule.Region;
////            originalSysModuleInformation.EmailAddress = sysModule.EmailAddress;
////            originalSysModuleInformation.PaymentTypeID = sysModule.PaymentTypeID;

////        }

//    }
//}
