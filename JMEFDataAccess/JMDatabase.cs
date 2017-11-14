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
        public DbSet<SysModule> SysModule { get; set; }
        public DbSet<SysLog> SysLog { get; set; }
        public DbSet<SysUser> SysUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("dbo.Customers");
            modelBuilder.Entity<PaymentType>().ToTable("dbo.PaymentTypes");
            modelBuilder.Entity<SysModule>().ToTable("dbo.SysModule");
            modelBuilder.Entity<SysLog>().ToTable("dbo.SysLog");
            modelBuilder.Entity<SysUser>().ToTable("dbo.SysUser");
        }
               
    }

}
