using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMModels;
using System.Data;
using JMDataServiceInterface;


namespace JMEFDataAccess
{
   public class EFSysModuleOperateDataService : EFDataService, ISysModuleOperateDataService
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public EFSysModuleOperateDataService()
        {

        }

        //public IQueryable<SysModuleOperate> GetModuleOperateList()
        //    {
        //        IQueryable<SysModuleOperate> list = dbConnection.SysModuleOperates.AsQueryable();
        //        return list;

        //    }

        public List<SysModuleOperate> GetModuleOperateList()
        {
            
            var SysModuleOperatesQuery = dbConnection.SysModuleOperates.AsQueryable();
            var SysModuleOperatesQuerys = (from p in SysModuleOperatesQuery select p ).ToList();

            int numberOfRows = SysModuleOperatesQuerys.Count();

            return SysModuleOperatesQuerys;
        }


        public int CreateSysModuleOperate(SysModuleOperate entity)
            {
                //using (DBContainer db = new DBContainer())
                //{
                    dbConnection.SysModuleOperates.Add(entity);
                    return dbConnection.SaveChanges();
                //}
            }

            public int DeleteSysModuleOperate(string id)
            {
                //using (DBContainer db = new DBContainer())
                //{
                    SysModuleOperate entity = dbConnection.SysModuleOperates.SingleOrDefault(a => a.Id == id);
                    if (entity != null)
                    {

                dbConnection.SysModuleOperates.Remove(entity);
                    }
                    return dbConnection.SaveChanges();
                //}
            }

            public SysModuleOperate GetSysModuleOperateById(string id)
            {
                //using (DBContainer db = new DBContainer())
                //{
                    return dbConnection.SysModuleOperates.SingleOrDefault(a => a.Id == id);
                //}
            }

            public bool IsExist(string id)
            {
                //using (DBContainer db = new DBContainer())
                //{
                    SysModuleOperate entity = GetSysModuleOperateById(id);
                    if (entity != null)
                        return true;
                    return false;
                //}
            }

            //public void Dispose()
            //{

            //}
        }
    }

    
