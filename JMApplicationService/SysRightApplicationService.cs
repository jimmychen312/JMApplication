using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{

    public class SysRightApplicationService
    {

        ISysRightDataService _sysRightDataService;  

        private ISysRightDataService SysRightDataService
        {
            get { return _sysRightDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysRightApplicationService(ISysRightDataService dataService)
        {
            _sysRightDataService = dataService;
        }



        /// <summary>
        /// Get Permissions
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Permission> GetPermissions(string accountId,string controller, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysRightDataService.CreateSession();                
                List<Permission> permissions = SysRightDataService.GetPermissions(accountId,controller);                
                transaction.ReturnStatus = true;
                return permissions;
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
                SysRightDataService.CloseSession();
            }

        }

        ///// <summary>
        ///// Get Permission By Permission ID
        ///// </summary>
        ///// <param name="permissionID"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public Permission GetPermissionByPermissionID(Guid permissionID, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();

        //    try
        //    {
        //        PermissionDataService.CreateSession();
        //        Permission permission = PermissionDataService.GetPermissionByPermissionID(permissionID);
        //        transaction.ReturnStatus = true;
        //        return permission;
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
        //        PermissionDataService.CloseSession();
        //    }

        //}

        ///// <summary>
        ///// Permission Inquiry
        ///// </summary>
        ///// <param name="firstName"></param>
        ///// <param name="lastName"></param>
        ///// <param name="paging"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public List<PermissionInquiry> PermissionInquiry(string firstName, string lastName, DataGridPagingInformation paging, out TransactionalInformation transaction)
        //{
        //    transaction = new TransactionalInformation();
        //    try
        //    {
        //        PermissionDataService.CreateSession();
        //        List<PermissionInquiry> permissions = PermissionDataService.PermissionInquiry(firstName, lastName, paging);
        //        transaction.ReturnStatus = true;
        //        return permissions;
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
        //        PermissionDataService.CloseSession();
        //    }

        //}


        //        /// <summary>
        //        /// Create Permission
        //        /// </summary>
        //        /// <param name="permission"></param>
        //        /// <param name="transaction"></param>
        //        public void CreatePermission(Permission permission, out TransactionalInformation transaction)
        //        {
        //            transaction = new TransactionalInformation();
        //            PermissionBusinessRules permissionBusinessRules = new PermissionBusinessRules();

        //            try
        //            {
        //                PermissionDataService.CreateSession();

        //                permissionBusinessRules.ValidatePermission(permission, PermissionDataService);

        //                if (permissionBusinessRules.ValidationStatus == true)
        //                {
        //                    PermissionDataService.BeginTransaction();
        //                    PermissionDataService.CreatePermission(permission);
        //                    PermissionDataService.CommitTransaction(true);
        //                    transaction.ReturnStatus = true;
        //                    transaction.ReturnMessage.Add("Permission successfully created at " + permission.DateCreated.ToString());
        //                }
        //                else
        //                {
        //                    transaction.ReturnStatus = permissionBusinessRules.ValidationStatus;
        //                    transaction.ReturnMessage = permissionBusinessRules.ValidationMessage;
        //                    transaction.ValidationErrors = permissionBusinessRules.ValidationErrors;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                PermissionDataService.RollbackTransaction(true);
        //                transaction.ReturnMessage = new List<string>();
        //                string errorMessage = ex.Message;
        //                transaction.ReturnStatus = false;
        //                transaction.ReturnMessage.Add(errorMessage);
        //            }
        //            finally
        //            {
        //                PermissionDataService.CloseSession();
        //            }

        //        }

        //        /// <summary>
        //        /// Update Permission
        //        /// </summary>
        //        /// <param name="permission"></param>
        //        /// <param name="transaction"></param>
        //        public void UpdatePermission(Permission permission, out TransactionalInformation transaction)
        //        {

        //            transaction = new TransactionalInformation();
        //            PermissionBusinessRules permissionBusinessRules = new PermissionBusinessRules();

        //            try
        //            {

        //                PermissionDataService.CreateSession();
        //                permissionBusinessRules.ValidatePermission(permission, PermissionDataService);

        //                if (permissionBusinessRules.ValidationStatus == true)
        //                {
        //                    Permission originalPermissionInformation = PermissionDataService.GetPermissionByPermissionID(permission.PermissionID);
        //                    PopulatePermissionInformation(permission, originalPermissionInformation);
        //                    PermissionDataService.BeginTransaction();
        //                    PermissionDataService.UpdatePermission(originalPermissionInformation);
        //                    PermissionDataService.CommitTransaction(true);
        //                    transaction.ReturnStatus = true;
        //                    transaction.ReturnMessage.Add("Permission successfully updated at " + DateTime.Now.ToString());
        //                }
        //                else
        //                {
        //                    transaction.ReturnStatus = permissionBusinessRules.ValidationStatus;
        //                    transaction.ReturnMessage = permissionBusinessRules.ValidationMessage;
        //                    transaction.ValidationErrors = permissionBusinessRules.ValidationErrors;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                PermissionDataService.RollbackTransaction(true);
        //                transaction.ReturnMessage = new List<string>();
        //                string errorMessage = ex.Message;
        //                transaction.ReturnStatus = false;
        //                transaction.ReturnMessage.Add(errorMessage);
        //            }
        //            finally
        //            {
        //                PermissionDataService.CloseSession();
        //            }


        //        }

        //        public void DeleteAllPermissions(out TransactionalInformation transaction)
        //        {
        //            transaction = new TransactionalInformation();

        //            try
        //            {

        //                PermissionDataService.CreateSession();

        //                PermissionDataService.BeginTransaction();
        //                PermissionDataService.DeleteAllPermissions();
        //                PermissionDataService.CommitTransaction(true);
        //                transaction.ReturnStatus = true;
        //                transaction.ReturnMessage.Add("All permissions deleted at " + DateTime.Now.ToString());

        //            }
        //            catch (Exception ex)
        //            {
        //                PermissionDataService.RollbackTransaction(true);
        //                transaction.ReturnMessage = new List<string>();
        //                string errorMessage = ex.Message;
        //                transaction.ReturnStatus = false;
        //                transaction.ReturnMessage.Add(errorMessage);
        //            }
        //            finally
        //            {
        //                PermissionDataService.CloseSession();
        //            }

        //        }


        //        /// <summary>
        //        /// Populate Permission Information
        //        /// </summary>
        //        /// <param name="permission"></param>
        //        /// <param name="originalPermissionInformation"></param>
        //        private void PopulatePermissionInformation(Permission permission, Permission originalPermissionInformation)
        //        {
        //            originalPermissionInformation.FirstName = permission.FirstName;
        //            originalPermissionInformation.LastName = permission.LastName;
        //            originalPermissionInformation.CreditCardNumber = permission.CreditCardNumber;
        //            originalPermissionInformation.CreditCardSecurityCode = permission.CreditCardSecurityCode;
        //            originalPermissionInformation.CreditLimit = permission.CreditLimit;
        //            originalPermissionInformation.Address = permission.Address;
        //            originalPermissionInformation.City = permission.City;
        //            originalPermissionInformation.Country = permission.Country;
        //            originalPermissionInformation.PhoneNumber = permission.PhoneNumber;
        ////            originalPermissionInformation.TelePhone = permission.TelePhone;
        //            originalPermissionInformation.PostalCode = permission.PostalCode;
        //            originalPermissionInformation.Region = permission.Region;
        //            originalPermissionInformation.EmailAddress = permission.EmailAddress;
        //            originalPermissionInformation.PaymentTypeID = permission.PaymentTypeID;

        //        }

    }


}
