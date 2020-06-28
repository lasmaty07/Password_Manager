using Microsoft.Extensions.DependencyInjection;
using PasswordManager.db;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartDemoUseFile();
            Application.Run(new Form1());
        }

        private static void StartDemoUseFile()
        {
            using (var context = new DBContext())
            {
                CreateAndSeedDatabase(context);
            }
        }

        private static void CreateAndSeedDatabase(DbContext context)
        {
            if (context.Set<Aplicativo>().Count() != 0)
            {
                return;
            }

            context.Set<Aplicativo>().Add(new Aplicativo
            {
                Id = 1,
                Name = "control-m",
                User = "Nachn",
                Env = "Desa",
                Password = "testpassword"
            });
            context.Set<Aplicativo>().Add(new Aplicativo
            {
                Id = 2,
                Name = "serena",
                User = "L0690228",
                Env = "Prod",
                Password = "testpasswordprod"
            });

            context.SaveChanges();
        }

    }
}
