using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{
    public class AccountApplicationService
    {

        IAccountDataService _accountDataService;
        
        private IAccountDataService AccountDataService
        {
            get { return _accountDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountApplicationService(IAccountDataService dataService)
        {
            _accountDataService = dataService;
        }
        

        /// <summary>
        /// SysUser Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public SysUser Login(string username,string pwd, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                AccountDataService.CreateSession();
                SysUser sysUser = AccountDataService.Login(username,pwd);
                transaction.ReturnStatus = true;
                return sysUser;
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
                AccountDataService.CloseSession();
            }

        }



        ///// <summary>
        ///// GetAccount By Id
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public Account GetAccountById(string accountID, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();

        //    try
        //    {                
        //        AccountDataService.CreateSession();
        //        Account account = AccountDataService.GetAccountById(accountID);             
        //        transaction.ReturnStatus = true;                
        //        return account;
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
        //        AccountDataService.CloseSession();                
        //    }

        //}


        ///// <summary>
        ///// GetAccount List
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public List<AccountInquiry> GetAccountInquiry(string queryStr,DataGridPagingInformation paging, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        AccountDataService.CreateSession();
        //        List<AccountInquiry> accountList = AccountDataService.AccountInquiry(queryStr,paging);                
        //        transaction.ReturnStatus = true;
        //        return accountList;
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
        //        AccountDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// Create Account
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="transaction"></param>
        //public void CreateAccount(Account account, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    //AccountBusinessRules accountBusinessRules = new AccountBusinessRules();

        //    try
        //    {                
        //        AccountDataService.CreateSession();

        //        //accountBusinessRules.ValidateAccount(account, AccountDataService);

        //        //if (accountBusinessRules.ValidationStatus == true)
        //        //{
        //        AccountDataService.BeginTransaction();
        //        AccountDataService.CreateAccount(account);
        //        AccountDataService.CommitTransaction(true);
        //        transaction.ReturnStatus = true;
        //        transaction.ReturnMessage.Add("Account successfully created at " + account.CreateTime.ToString());
        //        //}
        //        //else
        //        //{
        //        //    transaction.ReturnStatus = accountBusinessRules.ValidationStatus;
        //        //    transaction.ReturnMessage = accountBusinessRules.ValidationMessage;
        //        //    transaction.ValidationErrors = accountBusinessRules.ValidationErrors;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        AccountDataService.RollbackTransaction(true);
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);

        //        //ExceptionHander exceptionHander = new ExceptionHander(sysLogDataService);
        //        //ExceptionHander.WriteException(ex);
        //    }
        //    finally
        //    {
        //        AccountDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// Update Account
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="transaction"></param>
        //public void UpdateAccount(Account account, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    //AccountBusinessRules accountBusinessRules = new AccountBusinessRules();

        //    try
        //    {
        //        AccountDataService.CreateSession();
        //        //accountBusinessRules.ValidateAccount(account, AccountDataService);

        //        //if (accountBusinessRules.ValidationStatus == true)
        //        //{
        //            Account originalAccountInformation = AccountDataService.GetAccountById(account.Id);
        //            PopulateAccountInformation(account, originalAccountInformation);

        //            AccountDataService.BeginTransaction();
        //            AccountDataService.UpdateAccount(originalAccountInformation);
        //            AccountDataService.CommitTransaction(true);
        //            transaction.ReturnStatus = true;
        //            transaction.ReturnMessage.Add("Account successfully updated at " + DateTime.Now.ToString());
        //        //}
        //        //else
        //        //{
        //        //    transaction.ReturnStatus = accountBusinessRules.ValidationStatus;
        //        //    transaction.ReturnMessage = accountBusinessRules.ValidationMessage;
        //        //    transaction.ValidationErrors = accountBusinessRules.ValidationErrors;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        AccountDataService.RollbackTransaction(true);
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //    }
        //    finally
        //    {
        //        AccountDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// Delete Account
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="transaction"></param>
        //public void DeleteAccountById(string Id,out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();

        //    try
        //    {
        //        AccountDataService.CreateSession();
        //        AccountDataService.BeginTransaction();
        //        AccountDataService.DeleteAccountById(Id);
        //        AccountDataService.CommitTransaction(true);
        //        transaction.ReturnStatus = true;
        //        transaction.ReturnMessage.Add("Id Account deleted at " + DateTime.Now.ToString());

        //    }
        //    catch (Exception ex)
        //    {
        //        AccountDataService.RollbackTransaction(true);
        //        transaction.ReturnMessage = new List<string>();
        //        string errorMessage = ex.Message;
        //        transaction.ReturnStatus = false;
        //        transaction.ReturnMessage.Add(errorMessage);
        //    }
        //    finally
        //    {
        //        AccountDataService.CloseSession();
        //    }

        //}


        ///// <summary>
        ///// Populate Account Information
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="originalAccountInformation"></param>
        //private void PopulateAccountInformation(Account account, Account originalAccountInformation)
        //{
        //    originalAccountInformation.Name = account.Name;
        //    originalAccountInformation.Age = account.Age;
        //    originalAccountInformation.Bir = account.Bir;
        //    originalAccountInformation.Photo = account.Photo;
        //    originalAccountInformation.Note = account.Note;
        //    originalAccountInformation.CreateTime = account.CreateTime;    

        //}

    }
}
