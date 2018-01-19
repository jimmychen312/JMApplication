using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{

    public class ManageApplicationService
    {
        IManageDataService _manageDataService;

        private IManageDataService ManageDataService
        {
            get { return _manageDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ManageApplicationService(IManageDataService dataService)
        {
            _manageDataService = dataService;
        }

        /// <summary>
        /// Manage Inquiry By Id
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
                ManageDataService.CreateSession();
                List<SysModuleInquiry> manages = ManageDataService.GetMenuByPersonId(personId, ParentId);
                transaction.ReturnStatus = true;
                return manages;
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
                ManageDataService.CloseSession();
            }

        }

        ///// <summary>
        ///// Get Manage By Manage ID
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="personId"></param>
        ///// <param name="manageID"></param>
        ///// <returns></returns>        
        //public Manage GetManageByManageID(string personId,string manageID, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();

        //    try
        //    {
        //        ManageDataService.CreateSession();
        //        Manage manage = ManageDataService.GetManageByManageID(personId,manageID);
        //        transaction.ReturnStatus = true;
        //        return manage;
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
        //        ManageDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// Manage Inquiry
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //public List<ManageInquiry> ManageInquiry(string personId,out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        ManageDataService.CreateSession();
        //        List<ManageInquiry> manages = ManageDataService.ManageInquiry(personId);
        //        transaction.ReturnStatus = true;
        //        return manages;
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
        //        ManageDataService.CloseSession();
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

//    public class ManageApplicationService
//    {

//        IManageDataService _manageDataService;  

//        private IManageDataService ManageDataService
//        {
//            get { return _manageDataService; }
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public ManageApplicationService(IManageDataService dataService)
//        {
//            _manageDataService = dataService;
//        }


//        /// <summary>
//        /// Get Manage By Manage ID
//        /// </summary>
//        /// <param name="manageID"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public Manage GetManageByManageID(String manageID, out TransactionalInformation transaction)
//        {
//            transaction = new TransactionalInformation();

//            try
//            {
//                ManageDataService.CreateSession();
//                Manage manage = ManageDataService.GetManageByManageID(manageID);
//                transaction.ReturnStatus = true;
//                return manage;
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
//                ManageDataService.CloseSession();
//            }

//        }

//        /// <summary>
//        /// Manage Inquiry
//        /// </summary>
//        /// <param name="firstName"></param>
//        /// <param name="lastName"></param>
//        /// <param name="paging"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public List<ManageInquiry> ManageInquiry(out TransactionalInformation transaction)
//        {
//            transaction = new TransactionalInformation();
//            try
//            {
//                ManageDataService.CreateSession();
//                List<ManageInquiry> manages = ManageDataService.ManageInquiry();
//                transaction.ReturnStatus = true;
//                return manages;
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
//                ManageDataService.CloseSession();
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
//        //        ManageDataService.CreateSession();
//        //        List<PaymentType> paymentTypes = ManageDataService.GetPaymentTypes();
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
//        //        ManageDataService.CloseSession();
//        //    }

//        //}

//        ///// <summary>
//        ///// Create Manage
//        ///// </summary>
//        ///// <param name="manage"></param>
//        ///// <param name="transaction"></param>
//        //public void CreateManage(Manage manage, out TransactionalInformation transaction)
//        //{
//        //    transaction = new TransactionalInformation();
//        //    ManageBusinessRules manageBusinessRules = new ManageBusinessRules();

//        //    try
//        //    {
//        //        ManageDataService.CreateSession();

//        //        manageBusinessRules.ValidateManage(manage, ManageDataService);

//        //        if (manageBusinessRules.ValidationStatus == true)
//        //        {
//        //            ManageDataService.BeginTransaction();
//        //            ManageDataService.CreateManage(manage);
//        //            ManageDataService.CommitTransaction(true);
//        //            transaction.ReturnStatus = true;
//        //            transaction.ReturnMessage.Add("Manage successfully created at " + manage.DateCreated.ToString());
//        //        }
//        //        else
//        //        {
//        //            transaction.ReturnStatus = manageBusinessRules.ValidationStatus;
//        //            transaction.ReturnMessage = manageBusinessRules.ValidationMessage;
//        //            transaction.ValidationErrors = manageBusinessRules.ValidationErrors;
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        ManageDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        ManageDataService.CloseSession();
//        //    }

//        //}

//        ///// <summary>
//        ///// Update Manage
//        ///// </summary>
//        ///// <param name="manage"></param>
//        ///// <param name="transaction"></param>
//        //public void UpdateManage(Manage manage, out TransactionalInformation transaction)
//        //{

//        //    transaction = new TransactionalInformation();
//        //    ManageBusinessRules manageBusinessRules = new ManageBusinessRules();

//        //    try
//        //    {

//        //        ManageDataService.CreateSession();
//        //        manageBusinessRules.ValidateManage(manage, ManageDataService);

//        //        if (manageBusinessRules.ValidationStatus == true)
//        //        {
//        //            Manage originalManageInformation = ManageDataService.GetManageByManageID(manage.ManageID);
//        //            PopulateManageInformation(manage, originalManageInformation);
//        //            ManageDataService.BeginTransaction();
//        //            ManageDataService.UpdateManage(originalManageInformation);
//        //            ManageDataService.CommitTransaction(true);
//        //            transaction.ReturnStatus = true;
//        //            transaction.ReturnMessage.Add("Manage successfully updated at " + DateTime.Now.ToString());
//        //        }
//        //        else
//        //        {
//        //            transaction.ReturnStatus = manageBusinessRules.ValidationStatus;
//        //            transaction.ReturnMessage = manageBusinessRules.ValidationMessage;
//        //            transaction.ValidationErrors = manageBusinessRules.ValidationErrors;
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        ManageDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        ManageDataService.CloseSession();
//        //    }


//        //}

//        //public void DeleteAllManages(out TransactionalInformation transaction)
//        //{
//        //    transaction = new TransactionalInformation();

//        //    try
//        //    {

//        //        ManageDataService.CreateSession();

//        //        ManageDataService.BeginTransaction();
//        //        ManageDataService.DeleteAllManages();
//        //        ManageDataService.CommitTransaction(true);
//        //        transaction.ReturnStatus = true;
//        //        transaction.ReturnMessage.Add("All manages deleted at " + DateTime.Now.ToString());

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        ManageDataService.RollbackTransaction(true);
//        //        transaction.ReturnMessage = new List<string>();
//        //        string errorMessage = ex.Message;
//        //        transaction.ReturnStatus = false;
//        //        transaction.ReturnMessage.Add(errorMessage);
//        //    }
//        //    finally
//        //    {
//        //        ManageDataService.CloseSession();
//        //    }

//        //}


////        /// <summary>
////        /// Populate Manage Information
////        /// </summary>
////        /// <param name="manage"></param>
////        /// <param name="originalManageInformation"></param>
////        private void PopulateManageInformation(Manage manage, Manage originalManageInformation)
////        {
////            originalManageInformation.FirstName = manage.FirstName;
////            originalManageInformation.LastName = manage.LastName;
////            originalManageInformation.CreditCardNumber = manage.CreditCardNumber;
////            originalManageInformation.CreditCardSecurityCode = manage.CreditCardSecurityCode;
////            originalManageInformation.CreditLimit = manage.CreditLimit;
////            originalManageInformation.Address = manage.Address;
////            originalManageInformation.City = manage.City;
////            originalManageInformation.Country = manage.Country;
////            originalManageInformation.PhoneNumber = manage.PhoneNumber;
//////            originalManageInformation.TelePhone = manage.TelePhone;
////            originalManageInformation.PostalCode = manage.PostalCode;
////            originalManageInformation.Region = manage.Region;
////            originalManageInformation.EmailAddress = manage.EmailAddress;
////            originalManageInformation.PaymentTypeID = manage.PaymentTypeID;

////        }

//    }
//}
