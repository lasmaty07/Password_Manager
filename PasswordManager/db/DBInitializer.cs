using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace PasswordManager.db
{
    public class DBInitializer : SqliteDropCreateDatabaseWhenModelChanges<DBContext>
    {
        public DBInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        { }

        protected override void Seed(DBContext context)
        {
            // Here you can seed your core data if you have any.
        }
    }
}
