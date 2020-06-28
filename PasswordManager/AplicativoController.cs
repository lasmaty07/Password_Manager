using PasswordManager.db;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordManager
{
    class AplicativoController
    {
        public static Aplicativo GetAplicativo()
        {
            using (var db = new DBContext())
            {
                string Name = "serena";

                //List<Aplicativo> aplicativo = db.Aplicativos.Where(s => s.Name == Name).ToList();
                List<Aplicativo> aplicativos = db.Aplicativos.Where(s => s.Name == Name).ToList();
                
                foreach (var school in aplicativos)
                {
                    System.Console.WriteLine(school.Name);
                }

                return aplicativos[0];
                
                //return _outputBuffer.ToString();

            }
        }

    }
}
