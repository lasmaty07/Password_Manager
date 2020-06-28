﻿using PasswordManager.db;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    class AplicativoController
    {      
        public static List<Aplicativo> GetAplicativo(string name,string user,string env)
        {
            List<Aplicativo> aplicativos = new List<Aplicativo> { } ;

            // TO-DO refactoring de este codigo
            if (name.Length != 0  && user.Length != 0 && env.Length != 0)
            {
                using (var db = new DBContext())
                {
                    aplicativos = db.Aplicativos.Where(
                                    t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name.ToLower()) &&
                                                                 t.User.ToLower().Contains(user.ToLower()) &&
                                                                 t.Env.ToLower().Contains(env.ToLower()))
                                    ).ToList();
                    
                }
            }
            else {
                if (name.Length != 0 && user.Length == 0 && env.Length == 0)
                {
                    using (var db = new DBContext())
                    {
                        aplicativos = db.Aplicativos.Where(
                                        t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name.ToLower()))
                                        ).ToList();

                    }
                }
                else {
                    if (name.Length != 0 && user.Length != 0 && env.Length == 0) 
                    {
                        using (var db = new DBContext())
                        {
                            aplicativos = db.Aplicativos.Where(
                                            t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name.ToLower())) &&
                                                                        t.User.ToLower().Contains(user.ToLower())
                                            ).ToList();

                        }
                    }
                }
            }
            return aplicativos;
        }
    }
}