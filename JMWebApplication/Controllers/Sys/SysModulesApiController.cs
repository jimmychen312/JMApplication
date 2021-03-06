using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JMApplication.ViewModels;
using JMApplication.Helpers;
using JMApplicationService;
using JMModels;
using JMEFDataAccess;
using JMDataServiceInterface;
using JMApplication.Filters;
using System.Web.Security;
using JMApplication.ViewModels.Manage;
using JMApplication.ViewModels.Customers;
using System.Web;


namespace JMWebApplication.Controllers
{
    [RoutePrefix("api/sysModules")]
    public class SysModuleApiController : ApiController
    {
        ISysModuleDataService sysModuleDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public SysModuleApiController(ISysModuleDataService dataService)
        {
            sysModuleDataService = dataService;
        }
        

        /// <summary>
        /// Get SysModules Maintenance Information
        /// </summary>
        /// <param name="sysModuleID"></param>
        /// <returns></returns>
        //[WebApiAuthenication]
        //[HttpGet("GetCustomerMaintenanceInformation")]
        [HttpGet, Route("GetSysModuleById")]
        public HttpResponseMessage GetSysModuleMaintenanceInformation(string personId,HttpRequestMessage request, string sysModuleID)
        {

            TransactionalInformation sysModuleTransaction;
            
            SysModuleMaintenanceViewModel sysModuleMaintenanceViewModel = new SysModuleMaintenanceViewModel();
            
            SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);

            if (sysModuleID != string.Empty)
            {
                SysModule sysModule = sysModuleApplicationService.GetSysModuleBySysModuleID(personId,sysModuleID, out sysModuleTransaction);
                sysModuleMaintenanceViewModel.SysModule = sysModule;
                sysModuleMaintenanceViewModel.ReturnStatus = sysModuleTransaction.ReturnStatus;
                sysModuleMaintenanceViewModel.ReturnMessage = sysModuleTransaction.ReturnMessage;
            }
                       
            if (sysModuleMaintenanceViewModel.ReturnStatus == true)
            {
                var response = Request.CreateResponse<SysModuleMaintenanceViewModel>(HttpStatusCode.OK, sysModuleMaintenanceViewModel);
                return response;
            }

            var badResponse = Request.CreateResponse<SysModuleMaintenanceViewModel>(HttpStatusCode.BadRequest, sysModuleMaintenanceViewModel);
            return badResponse;

        }


        /// <summary>
        /// SysModules Inquiry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sysModuleInquiryDTO"></param>
        /// <returns></returns>
        //[WebApiAuthenication]
        [HttpGet,Route("GetSysModule")]
        public HttpResponseMessage SysModuleInquiry()
        {
            if (HttpContext.Current.Session["Account"] != null)
            {
                Account account = (Account)HttpContext.Current.Session["Account"];


                TransactionalInformation transaction;

                SysModuleInquiryViewModel sysModuleInquiryViewModel = new SysModuleInquiryViewModel();

                SysModuleApplicationService sysModuleApplicationService = new SysModuleApplicationService(sysModuleDataService);
                List<SysModuleInquiry> sysModules = sysModuleApplicationService.SysModuleInquiry(account.Id, out transaction);
                                                
                sysModuleInquiryViewModel.SysModules = sysModules;
                sysModuleInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
                sysModuleInquiryViewModel.ReturnMessage = transaction.ReturnMessage;

                if (transaction.ReturnStatus == true)
                {
                    var response = Request.CreateResponse<SysModuleInquiryViewModel>(HttpStatusCode.OK, sysModuleInquiryViewModel);
                    return response;
                }                         

                var badResponse = Request.CreateResponse<SysModuleInquiryViewModel>(HttpStatusCode.BadRequest, sysModuleInquiryViewModel);
                return badResponse;

            }
            else
            {
                return Request.CreateResponse("0");
            }

        }
    }
}
