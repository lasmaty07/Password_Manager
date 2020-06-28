using PasswordManager.db;
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
                                    t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name) &&
                                                                 t.User.ToLower().Contains(user) &&
                                                                 t.Env.ToLower().Contains(env))
                                    ).ToList();
                    
                }
            }
            else {
                if (name.Length != 0 && user.Length == 0 && env.Length == 0)
                {
                    using (var db = new DBContext())
                    {
                        aplicativos = db.Aplicativos.Where(
                                        t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name))
                                        ).ToList();

                    }
                }
                else {
                    if (name.Length != 0 && user.Length != 0 && env.Length == 0) 
                    {
                        using (var db = new DBContext())
                        {
                            aplicativos = db.Aplicativos.Where(
                                            t => db.Aplicativos.Any(s => t.Name.ToLower().Contains(name)) &&
                                                                        t.User.ToLower().Contains(user)
                                            ).ToList();

                        }
                    }
                }
            }
            return aplicativos;
        }

        public static string Encrypt(string plainText, string password)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = AplicativoController.Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        public static string Decrypt(string encryptedText, string admin_pass)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (admin_pass == null)
            {
                admin_pass = String.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(admin_pass);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = AplicativoController.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}