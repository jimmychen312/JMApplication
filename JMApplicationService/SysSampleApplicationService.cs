using JMDataServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;

namespace JMApplicationService
{
    public class SysSampleApplicationService
    {

        ISysSampleDataService _sysSampleDataService;
        //ISysModuleDataService _sysModuleDataService;

        private ISysSampleDataService SysSampleDataService
        {
            get { return _sysSampleDataService; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SysSampleApplicationService(ISysSampleDataService dataService)
        {
            _sysSampleDataService = dataService;
        }



        /// <summary>
        /// GetSysSample By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public SysSample GetSysSampleById(string sysSampleID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {                
                SysSampleDataService.CreateSession();
                SysSample sysSample = SysSampleDataService.GetSysSampleById(sysSampleID);             
                transaction.ReturnStatus = true;                
                return sysSample;
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
                SysSampleDataService.CloseSession();                
            }

        }


        /// <summary>
        /// GetSysSample List
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<SysSampleInquiry> GetSysSampleInquiry(DataGridPagingInformation paging, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                SysSampleDataService.CreateSession();
                List<SysSampleInquiry> sysSampleList = SysSampleDataService.SysSampleInquiry(paging);
                transaction.ReturnStatus = true;
                return sysSampleList;
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
                SysSampleDataService.CloseSession();
            }

        }


        /// <summary>
        /// Create SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        /// <param name="transaction"></param>
        public void CreateSysSample(SysSample sysSample, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            //SysSampleBusinessRules sysSampleBusinessRules = new SysSampleBusinessRules();

            try
            {
                SysSampleDataService.CreateSession();

                //sysSampleBusinessRules.ValidateSysSample(sysSample, SysSampleDataService);

                //if (sysSampleBusinessRules.ValidationStatus == true)
                //{
                SysSampleDataService.BeginTransaction();
                SysSampleDataService.CreateSysSample(sysSample);
                SysSampleDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("SysSample successfully created at " + sysSample.CreateTime.ToString());
                //}
                //else
                //{
                //    transaction.ReturnStatus = sysSampleBusinessRules.ValidationStatus;
                //    transaction.ReturnMessage = sysSampleBusinessRules.ValidationMessage;
                //    transaction.ValidationErrors = sysSampleBusinessRules.ValidationErrors;
                //}

            }
            catch (Exception ex)
            {
                SysSampleDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysSampleDataService.CloseSession();
            }

        }


        /// <summary>
        /// Update SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        /// <param name="transaction"></param>
        public void UpdateSysSample(SysSample sysSample, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            //SysSampleBusinessRules sysSampleBusinessRules = new SysSampleBusinessRules();

            try
            {
                SysSampleDataService.CreateSession();
                //sysSampleBusinessRules.ValidateSysSample(sysSample, SysSampleDataService);

                //if (sysSampleBusinessRules.ValidationStatus == true)
                //{
                    //SysSample originalSysSampleInformation = SysSampleDataService.GetSysSampleById(sysSample.Id);
                    //PopulateSysSampleInformation(sysSample, originalSysSampleInformation);

                    SysSampleDataService.BeginTransaction();
                //SysSampleDataService.UpdateSysSample(originalSysSampleInformation);
                SysSampleDataService.UpdateSysSample(sysSample);
                SysSampleDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("SysSample successfully updated at " + DateTime.Now.ToString());
                //}
                //else
                //{
                //    transaction.ReturnStatus = sysSampleBusinessRules.ValidationStatus;
                //    transaction.ReturnMessage = sysSampleBusinessRules.ValidationMessage;
                //    transaction.ValidationErrors = sysSampleBusinessRules.ValidationErrors;
                //}

            }
            catch (Exception ex)
            {
                SysSampleDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysSampleDataService.CloseSession();
            }
            
        }


        /// <summary>
        /// Delete SysSample
        /// </summary>
        /// <param name="sysSample"></param>
        /// <param name="transaction"></param>
        public void DeleteSysSampleById(string Id,out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                SysSampleDataService.CreateSession();
                SysSampleDataService.BeginTransaction();
                SysSampleDataService.DeleteSysSampleById(Id);
                SysSampleDataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Id SysSample deleted at " + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                SysSampleDataService.RollbackTransaction(true);
                transaction.ReturnMessage = new List<string>();
                string errorMessage = ex.Message;
                transaction.ReturnStatus = false;
                transaction.ReturnMessage.Add(errorMessage);
            }
            finally
            {
                SysSampleDataService.CloseSession();
            }

        }


        /// <summary>
        /// Populate SysSample Information
        /// </summary>
        /// <param name="sysSample"></param>
        /// <param name="originalSysSampleInformation"></param>
        private void PopulateSysSampleInformation(SysSample sysSample, SysSample originalSysSampleInformation)
        {
            originalSysSampleInformation.Name = sysSample.Name;
            originalSysSampleInformation.Age = sysSample.Age;
            originalSysSampleInformation.Bir = sysSample.Bir;
            originalSysSampleInformation.Photo = sysSample.Photo;
            originalSysSampleInformation.Note = sysSample.Note;
            originalSysSampleInformation.CreateTime = sysSample.CreateTime;    

        }

    }
}
