using JMDataServiceInterface;
using JMModels;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using JMEFDataAccess;
using System.Linq.Dynamic;


namespace JMEFDataAccess
{
    public class EFSysModuleService : EFDataService, ISysModuleDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EFSysModuleService()
        {

        }

        ///// <summary>
        ///// Get Payment Types
        ///// </summary>
        ///// <returns></returns>
        //public List<PaymentType> GetPaymentTypes()
        //{

        //    var paymentTypesQuery = dbConnection.PaymentTypes.AsQueryable();
        //    var paymentTypes = (from p in paymentTypesQuery.OrderBy("Description") select p).ToList();

        //    int numberOfRows = paymentTypes.Count();

        //    return paymentTypes;

        //}

        ///// <summary>
        ///// Get SysModule List
        ///// </summary>        
        //public IQueryable<SysModule> GetSysModuleList()
        //{
        //    //IQueryable<SysModule> list = dbConnection.SysModules.AsQueryable();
        //    //return list;

        //    var SysModuleQuery = dbConnection.SysModules.AsQueryable();
        //    return SysModuleQuery;

        //}

        ///// <summary>
        ///// Get SysModule List
        ///// </summary>        
        //public List<SysModule> GetSysModuleList(string parentId)
        //{
        //    //IQueryable<SysModule> list = dbConnection.SysModules.AsQueryable();
        //    //return list;
        //    //var paymentTypesQuery = dbConnection.PaymentTypes.AsQueryable();
        //    var sysModuleQuery = dbConnection.SysModules.AsQueryable();
        //    //var paymentTypes = (from p in paymentTypesQuery.OrderBy("Description") select p).ToList();
        //    var sysModules = (from m in sysModuleQuery.Where(m=> m.ParentId==parentId) select m).ToList();
        //    return sysModules;

        //}

        /// <summary>
        /// Get SysModules 
        /// </summary>
        /// <returns></returns>
        public List<SysModule> GetSysModuleList(string parentId)
        {           
            var sysModuleQuery = dbConnection.SysModules.AsQueryable();
            //var sysModules = (from p in sysModuleQuery.Where(p => p.ParentId == parentId && p.Id !="0").OrderBy(p => p.Sort) select p).ToList();
            var sysModules = (from p in sysModuleQuery.Where(p => p.Id != "0").OrderBy(p => p.Sort) select p).ToList();

            int numberOfRows = sysModules.Count();

            return sysModules;

        }

        ///// <summary>
        ///// Get Module By System
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //public List<SysModule> GetSysModuleBySystem(string parentId)
        //{
        //    var sysModuleQuery = dbConnection.SysModules.AsQueryable();
        //    var sysModules = (from p in sysModuleQuery.Where(a => a.ParentId == parentId).OrderBy("ParentId") select p).ToList();
        //    return sysModules;
        //}


        /// <summary>
        /// Create SysModule
        /// </summary>
        /// <param name="sysModule"></param>
        public void CreateSysModule(SysModule sysModule)
        {
            DateTime dateCreated = System.DateTime.Now;

            sysModule.Id = Guid.NewGuid().ToString ();
            sysModule.CreateTime = dateCreated;
            sysModule.CreatePerson = "Admin";
            //sysModule.DateUpdated = dateCreated;
            dbConnection.SysModules.Add(sysModule);
            //return dbConnection.SaveChanges();


            //customer.CustomerID = Guid.NewGuid();
            //customer.DateCreated = dateCreated;
            //customer.DateUpdated = dateCreated;
            //dbConnection.Customers.Add(customer);
        }

        public void DeleteSysModuleById(string id)
        {
            SysModule entity = dbConnection.SysModules.SingleOrDefault(a => a.Id == id);
            if (entity != null)
            {

                //删除SysRight表数据
                var sr = dbConnection.SysRights.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var o in sr)
                {
                    //删除SysRightOperate表数据
                    var sro = dbConnection.SysRightOperates.AsQueryable().Where(a => a.RightId == o.Id);
                    foreach (var o2 in sro)
                    {
                        dbConnection.SysRightOperates.Remove(o2);
                    }
                    dbConnection.SysRights.Remove(o);
                }
                //删除SysModuleOperate数据
                var smo = dbConnection.SysModuleOperates.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var o3 in smo)
                {
                    dbConnection.SysModuleOperates.Remove(o3);
                }
                dbConnection.SysModules.Remove(entity);

            }
        }

        ///// <summary>
        ///// Update SysModule
        ///// </summary>
        ///// <param name="sysModule"></param>
        //public int UpdateSysModule(SysModule sysModule)
        //{
        //    DateTime dateUpdated = System.DateTime.Now;
        //    sysModule.CreateTime = dateUpdated;
        //    return dbConnection.SaveChanges();
        //}



        /// <summary>
        /// Get SysModule By Module Id
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public SysModule GetSysModuleById(string id)
        {
            var sysModulesInformation = dbConnection.SysModules.First(c => c.Id == id);
            SysModule sysModule = sysModulesInformation as SysModule;
            return sysModule;

            //return dbConnection.SysModules.SingleOrDefault(a => a.Id == id);

        }


        public bool IsExist(string id)
        {
            SysModule entity = GetSysModuleById(id);
            if (entity != null)
                return true;
            return false;

        }

        /// <summary>
        /// 执行分配给角色的存储过程        
        /// </summary>
        public void InsertSysRight()
        {
            dbConnection.Database.ExecuteSqlCommand("exec P_Sys_InsertSysRight");
            dbConnection.SaveChanges();
        }

        /// <summary>
        ///清理无用的项的存储过程        
        /// </summary>
        public void ClearUnusedRightOperate()
        {
            dbConnection.Database.ExecuteSqlCommand("exec P_Sys_ClearUnusedRightOperate");
            dbConnection.SaveChanges();
        }
        
    }
}
