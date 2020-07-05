using PasswordManager.db;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager
{
    class AplicativoController
    {
        public static List<Aplicativo> GetAplicativo(string name, string user, string env)
        {
            List<Aplicativo> aplicativos = new List<Aplicativo> { };

            // TO-DO refactoring de este codigo
            /*if (name.Length != 0 && user.Length != 0 && env.Length != 0)
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
            }*/

            IQueryable<Aplicativo> result ;
            using (var db = new DBContext())
            {
                result = db.Aplicativos;
                if (!string.IsNullOrEmpty(name))
                {
                    result = result.Where(t => t.Name.ToLower().Contains(name.ToLower()));
                }
                if (!string.IsNullOrEmpty(user))
                {
                    result = result.Where(t => t.User.ToLower().Contains(user.ToLower()));
                }
                if (!string.IsNullOrEmpty(env))
                {
                    result = result.Where(t => t.Env.ToLower().Contains(env.ToLower()));
                }
                aplicativos = result.ToList();
            }
            return aplicativos;
        }

        public static bool UpdateAplicativo(Aplicativo aplicativo)
        {
            using (var db = new DBContext())
            {
                var result = db.Aplicativos.SingleOrDefault(b => b.Id == aplicativo.Id);
                if (result != null)
                {
                    db.Entry(result).CurrentValues.SetValues(aplicativo);
                    db.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public static bool DeleteAplicativo(Aplicativo aplicativo)
        {
            using (var db = new DBContext())
            {
                var result = db.Aplicativos.FirstOrDefault(b => b.Id == aplicativo.Id);
                if (result != null)
                {
                    db.Aplicativos.Remove(result);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool AddAplicativo(Aplicativo aplicativo)
        {
            using (var db = new DBContext())
            {
                var result = db.Aplicativos.FirstOrDefault(b => b.Name.ToLower() == aplicativo.Name.ToLower() && 
                                                                b.User.ToLower() == aplicativo.User.ToLower() && 
                                                                b.Env.ToLower() == aplicativo.Env.ToLower());
                if (result == null)
                {
                    db.Aplicativos.Add(aplicativo);
                    db.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}