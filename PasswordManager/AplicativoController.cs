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
        public static List<Aplicativo> GetAplicativo(string name,string user,string env)
        {
            using (var db = new DBContext())
            {
                List<Aplicativo> aplicativos = db.Aplicativos.Where(
                                t => db.Aplicativos.All(
                                    s => t.Name.ToLower().Contains(name)
                                    )
                                ).ToList();
                return aplicativos;
            }
        }
    }
}