using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using JMModels;
using JMDataServiceInterface;


namespace JMEFDataAccess
{
    public class JMDatabase : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<SysException> SysExceptions { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }
        public DbSet<SysModule> SysModules { get; set; }
        public DbSet<SysModuleOperate> SysModuleOperates { get; set; }
        public DbSet<SysRight> SysRights { get; set; }
        public DbSet<SysRightOperate> SysRightOperates { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysRoleSysUser> SysRoleSysUsers { get; set; }        
        public DbSet<SysSample> SysSamples { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }
        //public DbSet<P_Sys_InsertSysRight> InsertSysRight { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("dbo.Customers");
            modelBuilder.Entity<PaymentType>().ToTable("dbo.PaymentTypes");
            modelBuilder.Entity<SysException>().ToTable("dbo.SysException");            
            modelBuilder.Entity<SysLog>().ToTable("dbo.SysLog");
            modelBuilder.Entity<SysModule>().ToTable("dbo.SysModule");
            modelBuilder.Entity<SysModuleOperate>().ToTable("dbo.SysModuleOperate");
            modelBuilder.Entity<SysRight>().ToTable("dbo.SysRight");
            modelBuilder.Entity<SysRightOperate>().ToTable("dbo.SysRightOperate");
            modelBuilder.Entity<SysRole>().ToTable("dbo.SysRole");
            modelBuilder.Entity<SysRoleSysUser>().ToTable("dbo.SysRoleSysUser");            
            modelBuilder.Entity<SysSample>().ToTable("dbo.SysSample");
            modelBuilder.Entity<SysUser>().ToTable("dbo.SysUser");
            //modelBuilder.Entity<P_Sys_InsertSysRight>().ToTable("dbo.P_Sys_InsertSysRight");
        }
               
    }

}
