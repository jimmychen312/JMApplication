using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMDataServiceInterface;
using System.Data.SqlClient;
using JMModels;

namespace JMAdoDataAccess
{
      public class AdoManageService : AdoDataService, IManageDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdoManageService()
        {

        }

        /// <summary>
        /// GetMenu By PersonId
        /// </summary>
        /// <returns></returns>
        public List<SysModuleInquiry> GetMenuByPersonId(string personId,string ParentId)
        {
            List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();
                     
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT SysModule.Id, SysModule.Name, SysModule.EnglishName, SysModule.ParentId, SysModule.Url, SysModule.Iconic, ");
            sqlBuilder.Append(" SysModule.Sort, SysModule.Remark, SysModule.Enable, SysModule.CreatePerson, SysModule.CreateTime,  ");
            sqlBuilder.Append(" SysModule.IsLast,SysModule.State ");
            sqlBuilder.Append(" FROM SysModule INNER JOIN ");
            sqlBuilder.Append(" SysRight ON SysModule.Id = SysRight.ModuleId INNER JOIN ");
            sqlBuilder.Append(" SysRole ON SysRight.RoleId = SysRole.Id INNER JOIN ");
            sqlBuilder.Append(" SysRoleSysUser ON SysRole.Id = SysRoleSysUser.SysRoleId INNER JOIN ");
            sqlBuilder.Append(" SysUser ON SysRoleSysUser.SysUserId = SysUser.Id ");
            sqlBuilder.Append(" WHERE(SysUser.Id =@Id) AND(SysRight.Rightflag = 'True') AND (SysModule.ParentId = @ParentId) AND(SysModule.Id <> '0') ");
            sqlBuilder.Append(" ORDER BY SysModule.Sort ");

            string sql = sqlBuilder.ToString();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = dbConnection;
            sqlCommand.CommandText = sql;

            sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@Id"].Value = personId;
            sqlCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.NVarChar);
            sqlCommand.Parameters["@ParentId"].Value = ParentId;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DataReader dataReader = new DataReader(reader);

                SysModuleInquiry sysModule = new SysModuleInquiry();

                sysModule.Id = dataReader.GetString("Id");
                sysModule.Name = dataReader.GetString("Name");
                sysModule.EnglishName = dataReader.GetString("EnglishName");
                sysModule.ParentId = dataReader.GetString("ParentId");
                sysModule.Url = dataReader.GetString("Url");
                sysModule.Iconic = dataReader.GetString("Iconic");
                sysModule.Sort = dataReader.GetInt32("Sort");
                sysModule.Remark = dataReader.GetString("Remark");
                sysModule.Enable = dataReader.GetBoolean("Enable");
                sysModule.CreatePerson = dataReader.GetString("CreatePerson");
                sysModule.CreateTime = dataReader.GetDateTime("CreateTime");
                sysModule.IsLast = dataReader.GetBoolean("IsLast");
                sysModuleList.Add(sysModule);

            }

            reader.Close();
            return sysModuleList;

        }

        //public List<SysModuleInquiry> GetSysModuleList()
        //{
        //    List<SysModuleInquiry> sysModuleList = new List<SysModuleInquiry>();

        //    StringBuilder sqlBuilder = new StringBuilder();

        //    sqlBuilder.Append(" SELECT SysModule.Id, SysModule.Name, SysModule.EnglishName, SysModule.ParentId, SysModule.Url, SysModule.Iconic, ");
        //    sqlBuilder.Append(" SysModule.Sort, SysModule.Remark, SysModule.Enable, SysModule.CreatePerson, SysModule.CreateTime, ");
        //    sqlBuilder.Append(" SysModule.IsLast, SysModule.Version ");
        //    sqlBuilder.Append(" FROM SysModule ");
        //    sqlBuilder.Append(" ORDER BY SysModule.Sort ");

        //    string sql = sqlBuilder.ToString();

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    //sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
        //    //sqlCommand.Parameters["@Id"].Value = personId;
        //    //sqlCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.NVarChar);
        //    //sqlCommand.Parameters["@ParentId"].Value = ParentId;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        SysModuleInquiry sysModule = new SysModuleInquiry();

        //        sysModule.Id = dataReader.GetString("Id");
        //        sysModule.Name = dataReader.GetString("Name");
        //        sysModule.EnglishName = dataReader.GetString("EnglishName");
        //        sysModule.ParentId = dataReader.GetString("ParentId");
        //        sysModule.Url = dataReader.GetString("Url");
        //        sysModule.Iconic = dataReader.GetString("Iconic");
        //        sysModule.Sort = dataReader.GetInt32("Sort");
        //        sysModule.Remark = dataReader.GetString("Remark");
        //        sysModule.Enable = dataReader.GetBoolean("Enable");
        //        sysModule.CreatePerson = dataReader.GetString("CreatePerson");
        //        sysModule.CreateTime = dataReader.GetDateTime("CreateTime");
        //        sysModule.IsLast = dataReader.GetBoolean("IsLast");
        //        sysModuleList.Add(sysModule);

        //    }

        //    reader.Close();
        //    return sysModuleList;

        //    //IQueryable<SysModule> list = db.SysModule.AsQueryable();
        //    //return list;
        //}

        ///// <summary>
        ///// Create SysModule
        ///// </summary>
        ///// <param name="sysModule"></param>
        //public void CreateSysModule(SysModule sysModule)
        //{
        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.Transaction = dbTransaction;

        //    DateTime dateCreated = System.DateTime.Now;

        //    sysModule.CreateTime = dateCreated;

        //    StringBuilder sqlBuilder = new StringBuilder();

        //    sqlBuilder.Append(" INSERT INTO SysModule (Id,Name,EnglishName,ParentId,Url,Iconic,Sort,Remark,Enable,CreatePerson,CreateTime,IsLast,Version )");
        //    sqlBuilder.Append(" VALUES (@Id,@Name,@EnglishName,@ParentId,@Url,@Iconic,@Sort,@Remark,@Enable,@CreatePerson,@CreateTime,@IsLast,@Version)");

        //    sqlCommand.CommandText = sqlBuilder.ToString();
        //    sqlCommand.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@EnglishName", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@ParentId", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Url", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Iconic", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Sort", System.Data.SqlDbType.Int);
        //    sqlCommand.Parameters.Add("@Remark", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Enable", System.Data.SqlDbType.Bit);
        //    sqlCommand.Parameters.Add("@CreatePerson", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Enable", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTime);
        //    sqlCommand.Parameters.Add("@IsLast", System.Data.SqlDbType.NVarChar);
        //    sqlCommand.Parameters.Add("@Version", System.Data.SqlDbType.Timestamp);


        //    sqlCommand.Parameters["@Id"].Value = Utilities.GetString(sysModule.Id);
        //    sqlCommand.Parameters["@Name"].Value = Utilities.GetString(sysModule.Name);
        //    sqlCommand.Parameters["@EnglishName"].Value = Utilities.GetString(sysModule.EnglishName);
        //    sqlCommand.Parameters["@ParentId"].Value = Utilities.GetString(sysModule.ParentId);
        //    sqlCommand.Parameters["@Url"].Value = Utilities.GetString(sysModule.Url);
        //    sqlCommand.Parameters["@Iconic"].Value = Utilities.GetString(sysModule.Iconic);            
        //    sqlCommand.Parameters["@Sort"].Value = sysModule.Sort;
        //    sqlCommand.Parameters["@Remark"].Value = Utilities.GetString(sysModule.Remark);
        //    sqlCommand.Parameters["@Enable"].Value = sysModule.Enable;
        //    sqlCommand.Parameters["@CreatePerson"].Value = Utilities.GetString(sysModule.CreatePerson);
        //    sqlCommand.Parameters["@CreateTime"].Value = sysModule.CreateTime;
        //    sqlCommand.Parameters["@IsLast"].Value = sysModule.IsLast;
        //    sqlCommand.Parameters["@Version"].Value = sysModule.version;

        //    sqlCommand.ExecuteNonQuery();

        //}

        ////public int CreateSysModule(SysModule sysModule)
        ////{
        ////    using (DBContainer db = new DBContainer())
        ////    {
        ////        db.SysModule.AddObject(entity);
        ////        return db.SaveChanges();
        ////    }
        ////}


       
        ////public void DeleteSysModuleById(string Id)
        ////{
        ////    SqlCommand sqlCommand;
        ////    string sql = string.Empty;

        ////    sql = "DELETE FROM SysSample Where Id ='" + Id.ToString() + "'";

        ////    sqlCommand = new SqlCommand();
        ////    sqlCommand.Connection = dbConnection;
        ////    sqlCommand.Transaction = dbTransaction;
        ////    sqlCommand.CommandText = sql;
        ////    sqlCommand.ExecuteNonQuery();

        ////}

        ///// <summary>
        ///// Delete SysSample By Id
        ///// </summary>
        //public void DeleteSysModuleById(string id)
        //{
            
        //    SysModule entity = db.SysModule.SingleOrDefault(a => a.Id == id);
        //    if (entity != null)
        //    {

        //        //删除SysRight表数据
        //        var sr = db.SysRight.AsQueryable().Where(a => a.ModuleId == id);
        //        foreach (var o in sr)
        //        {
        //            //删除SysRightOperate表数据
        //            var sro = db.SysRightOperate.AsQueryable().Where(a => a.RightId == o.Id);
        //            foreach (var o2 in sro)
        //            {
        //                db.SysRightOperate.DeleteObject(o2);
        //            }
        //            db.SysRight.DeleteObject(o);
        //        }
        //        //删除SysModuleOperate数据
        //        var smo = db.SysModuleOperate.AsQueryable().Where(a => a.ModuleId == id);
        //        foreach (var o3 in smo)
        //        {
        //            db.SysModuleOperate.DeleteObject(o3);
        //        }
        //        db.SysModule.DeleteObject(entity);
        //    }
        //}

        //public int Edit(SysModule entity)
        //{
        //    using (DBContainer db = new DBContainer())
        //    {
        //        db.SysModule.Attach(entity);
        //        db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        //        return db.SaveChanges();
        //    }
        //}

        //public SysModule GetById(string id)
        //{


        //    SysModule sysModule = new SysModule();

        //    string sql = "SELECT * FROM SysModule WHERE Id = '" + id.ToString() + "'";

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = dbConnection;
        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        DataReader dataReader = new DataReader(reader);

        //        sysModule.Id = dataReader.GetString("Id");
        //        sysModule.Name = dataReader.GetString("Name");
                
        //    }
        //    reader.Close();

        //    return sysModule;


        //    //using (DBContainer db = new DBContainer())
        //    //{
        //    //    return db.SysModule.SingleOrDefault(a => a.Id == id);
        //    //}
        //}

        //public bool IsExist(string id)
        //{
        //    using (DBContainer db = new DBContainer())
        //    {
        //        SysModule entity = GetById(id);
        //        if (entity != null)
        //            return true;
        //        return false;
        //    }
        //}
        //public void Dispose()
        //{

        //}

    }

}
