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
        public static List<Aplicativo> GetAplicativo(string name)
        {
            using (var db = new DBContext())
            {
                List<Aplicativo> aplicativos = db.Aplicativos.Where(s => s.Name == name).ToList();
                return aplicativos;
            }
        }

    }
}
