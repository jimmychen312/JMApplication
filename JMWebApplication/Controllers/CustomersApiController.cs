using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JM.ViewModels;
using JM.ViewModels.Customers;
using JM.Helpers;
using JMApplicationService;
using JMModels;
using JMEFDataAccess;
using JMDataServiceInterface;
using JM.Filters;
using System.Web.Security;
//using JMApplication.Models;

namespace JMWebApplication.Controllers
{

    [RoutePrefix("api/customers")]
    public class CustomersApiController : ApiController
    {

        ICustomerDataService customerDataService;

        /// <summary>
        /// Constructor with Dependency Injection using Ninject
        /// </summary>
        /// <param name="dataService"></param>
        public CustomersApiController(ICustomerDataService dataService)
        {
            customerDataService = dataService;
        }


        /// <summary>
        /// Customer Login
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //[HttpPost("LoginCustomer")]
        [HttpPost,Route("LoginCustomer")]
        public HttpResponseMessage CustomerLogin(HttpRequestMessage request, [FromBody] LoginUser loginUser)
        {
            TransactionalInformation transaction = new TransactionalInformation();

            if (loginUser.LoginID == null || loginUser.LoginID == string.Empty)
            {
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add("Login ID is invalid.");
                var badresponse = Request.CreateResponse<TransactionalInformation>(HttpStatusCode.BadRequest, transaction);
                return badresponse;
            }

            FormsAuthentication.SetAuthCookie(loginUser.LoginID, createPersistentCookie: false);

            transaction.ReturnStatus = true;
            transaction.ReturnMessage.Add("User Authenicated.");
            var response = Request.CreateResponse<TransactionalInformation>(HttpStatusCode.OK, transaction);
            return response;

        }


        /// <summary>
        /// Customer Inquiry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerInquiryDTO"></param>
        /// <returns></returns>
        [WebApiAuthenication]
        //[HttpGet("GetCustomers")]
        [HttpGet,Route("GetCustomers")]
        public HttpResponseMessage CustomerInquiry(string FirstName, string LastName, int CurrentPageNumber, string SortExpression, string SortDirection, int PageSize)
        {

            TransactionalInformation transaction;

            if (FirstName == null) FirstName = string.Empty;
            if (LastName == null) LastName = string.Empty;
            if (SortDirection == null) SortDirection = string.Empty;
            if (SortExpression == null) SortExpression = string.Empty;

            CustomerInquiryViewModel customerInquiryViewModel = new CustomerInquiryViewModel();

            DataGridPagingInformation paging = new DataGridPagingInformation();
            paging.CurrentPageNumber = CurrentPageNumber;
            paging.PageSize = PageSize;
            paging.SortExpression = SortExpression;
            paging.SortDirection = SortDirection;

            if (paging.SortDirection == "") paging.SortDirection = "ASC";
            if (paging.SortExpression == "") paging.SortExpression = "LastName";

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            List<CustomerInquiry> customers = customerApplicationService.CustomerInquiry(FirstName, LastName, paging, out transaction);

            customerInquiryViewModel.Customers = customers;
            customerInquiryViewModel.ReturnStatus = transaction.ReturnStatus;
            customerInquiryViewModel.ReturnMessage = transaction.ReturnMessage;
            customerInquiryViewModel.TotalPages = paging.TotalPages;
            customerInquiryViewModel.TotalRows = paging.TotalRows;
            customerInquiryViewModel.PageSize = paging.PageSize;

            if (transaction.ReturnStatus == true)
            {
                var response = Request.CreateResponse<CustomerInquiryViewModel>(HttpStatusCode.OK, customerInquiryViewModel);
                return response;
            }

            var badResponse = Request.CreateResponse<CustomerInquiryViewModel>(HttpStatusCode.BadRequest, customerInquiryViewModel);
            return badResponse;


        }

        /// <summary>
        /// Get Customer Maintenance Information
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [WebApiAuthenication]
        //[HttpGet("GetCustomerMaintenanceInformation")]
        [HttpGet,Route("GetCustomerMaintenanceInformation")]
        public HttpResponseMessage GetCustomerMaintenanceInformation(HttpRequestMessage request, Guid customerID)
        {

            TransactionalInformation customerTransaction;
            TransactionalInformation paymentTransaction;

            CustomerMaintenanceViewModel customerMaintenanceViewModel = new CustomerMaintenanceViewModel();

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);

            if (customerID != Guid.Empty)
            {
                Customer customer = customerApplicationService.GetCustomerByCustomerID(customerID, out customerTransaction);
                customerMaintenanceViewModel.Customer = customer;
                customerMaintenanceViewModel.ReturnStatus = customerTransaction.ReturnStatus;
                customerMaintenanceViewModel.ReturnMessage = customerTransaction.ReturnMessage;
            }

            List<PaymentType> paymentTypes = customerApplicationService.GetPaymentTypes(out paymentTransaction);
            customerMaintenanceViewModel.PaymentTypes = paymentTypes;
            if (paymentTransaction.ReturnStatus == false)
            {
                customerMaintenanceViewModel.ReturnStatus = paymentTransaction.ReturnStatus;
                customerMaintenanceViewModel.ReturnMessage = paymentTransaction.ReturnMessage;
            }

            if (customerMaintenanceViewModel.ReturnStatus == true)
            {
                var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.OK, customerMaintenanceViewModel);
                return response;
            }

            var badResponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
            return badResponse;

        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [WebApiAuthenication]
        //[HttpPost("create")]
        [HttpPost,Route("create")]
        public HttpResponseMessage CreateCustomer(HttpRequestMessage request, [FromBody] CustomerMaintenanceDTO customerDTO)
        {
            TransactionalInformation transaction;

            CustomerMaintenanceViewModel customerMaintenanceViewModel = new CustomerMaintenanceViewModel();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Errors();
                customerMaintenanceViewModel.ReturnMessage = ModelStateHelper.ReturnErrorMessages(errors);
                customerMaintenanceViewModel.ValidationErrors = ModelStateHelper.ReturnValidationErrors(errors);
                customerMaintenanceViewModel.ReturnStatus = false;
                var badresponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                return badresponse;
            }

            Customer customer = new Customer();

            ModelStateHelper.UpdateViewModel(customerDTO, customer);

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            customerApplicationService.CreateCustomer(customer, out transaction);

            customerMaintenanceViewModel.Customer = customer;
            customerMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            customerMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            customerMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;

            if (transaction.ReturnStatus == false)
            {
                var badresponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                return badresponse;
            }
            else
            {
                var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.Created, customerMaintenanceViewModel);
                return response;
            }


        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [WebApiAuthenication]
        //[HttpPost("update")]
        [HttpPost,Route("update")]
        public HttpResponseMessage UpdateCustomer(HttpRequestMessage request, [FromBody] CustomerMaintenanceDTO customerDTO)
        {
            TransactionalInformation transaction;

            CustomerMaintenanceViewModel customerMaintenanceViewModel = new CustomerMaintenanceViewModel();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Errors();
                customerMaintenanceViewModel.ReturnMessage = ModelStateHelper.ReturnErrorMessages(errors);
                customerMaintenanceViewModel.ValidationErrors = ModelStateHelper.ReturnValidationErrors(errors);
                customerMaintenanceViewModel.ReturnStatus = false;
                var badresponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                return badresponse;
            }

            Customer customer = new Customer();

            ModelStateHelper.UpdateViewModel(customerDTO, customer);

            CustomerApplicationService customerApplicationService = new CustomerApplicationService(customerDataService);
            customerApplicationService.UpdateCustomer(customer, out transaction);

            customerMaintenanceViewModel.Customer = customer;
            customerMaintenanceViewModel.ReturnStatus = transaction.ReturnStatus;
            customerMaintenanceViewModel.ReturnMessage = transaction.ReturnMessage;
            customerMaintenanceViewModel.ValidationErrors = transaction.ValidationErrors;

            if (transaction.ReturnStatus == false)
            {
                var badresponse = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.BadRequest, customerMaintenanceViewModel);
                return badresponse;
            }
            else
            {
                var response = Request.CreateResponse<CustomerMaintenanceViewModel>(HttpStatusCode.Created, customerMaintenanceViewModel);
                return response;
            }


        }


    }
}
