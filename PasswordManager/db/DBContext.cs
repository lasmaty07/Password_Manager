using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PasswordManager.Model;

namespace PasswordManager.db
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base(@"data source=.\db\pass_man.db;foreign keys=true")
        {
            Configure();
        }

        public DBContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        public DbSet<Aplicativo> Aplicativos { get; set; }
        public DbSet<Config> Config { get; set; }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new DBInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
