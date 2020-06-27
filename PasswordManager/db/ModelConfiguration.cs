using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace PasswordManager.db
{
    class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureSelfReferencingEntities(modelBuilder);
        }

        private static void ConfigureSelfReferencingEntities(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aplicativo>();
        }
    }
}
