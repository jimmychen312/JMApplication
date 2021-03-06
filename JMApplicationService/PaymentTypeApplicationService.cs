﻿using JMDataServiceInterface;
using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMApplicationService
{

    public class PaymentTypeApplicationService
    {

        IPaymentTypeDataService _paymentTypeDataService;  

        private IPaymentTypeDataService PaymentTypeDataService
        {
            get { return _paymentTypeDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentTypeApplicationService(IPaymentTypeDataService dataService)
        {
            _paymentTypeDataService = dataService;
        }

         

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypes(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                PaymentTypeDataService.CreateSession();
                List<PaymentType> paymentTypes = PaymentTypeDataService.GetPaymentTypes();
                transaction.ReturnStatus = true;
                return paymentTypes;
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
                PaymentTypeDataService.CloseSession();
            }

        }
         

    }


}
