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
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartDemoUseFile();
            if (ShowInputDialog(ref Crypto.admin_pass) == DialogResult.OK)
            {
                Application.Run(new Form1());
            }
        }

        public static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form2 inputBox = new Form2();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.MaximizeBox = false;
            inputBox.Text = "Contraseña";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
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
            context.Set<Aplicativo>().Add(new Aplicativo
            {
                Id = 3,
                Name = "kibana",
                User = "L0690228",
                Env = "Prod",
                Password = "testpasswordprod"
            });

            context.SaveChanges();
        }

    }
}
