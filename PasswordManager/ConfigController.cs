using PasswordManager.db;
using PasswordManager.Model;
using System.Linq;

namespace PasswordManager
{
    class ConfigController
    {
        public static Config GetConfigValue(string key)
        {
            Config config = new Config();
            using (var db = new DBContext())
            {
                config = db.Config.SingleOrDefault(t => t.Key == key);
            }
            return config;
        }

        public static bool ValidateAdminPassword(string admin_pass) 
        {
            var result = false;
            var hashedPassword = Crypto.ComputeSha256Hash(admin_pass);

            string storedPassword ;
            using (var db = new DBContext())
            {
                storedPassword = db.Config.SingleOrDefault(t => t.Key == "Pass").Value;
            }
            
            result = hashedPassword == storedPassword;
            return result;
        }

    }
}
