using JMDataServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;
using JMCommon;

namespace JMApplicationService
{
    public class SysUserApplicationService
    {
        ISysRightDataService _sysRightDataService;
        
        private ISysRightDataService SysUserDataService
        {
            get { return _sysRightDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysUserApplicationService(ISysRightDataService dataService)
        {
            _sysRightDataService = dataService;
        }


        /// <summary>
        /// GetPermission
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="controller"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Permission> GetPermission(string accountid, string controller, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysUserDataService.CreateSession();
                List<Permission> permission = GetPermission(accountid, controller,out transaction);                
                transaction.ReturnStatus = true;
                return permission;
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
                SysUserDataService.CloseSession();
            }

        }
    }
}
