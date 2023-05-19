﻿using GorselProg.Model;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Services
{
    static class UserService
    {

        public static bool loadingIndicator = false;


        public static void ShowLoadingIndicator()
        {
            loadingIndicator = true;
        }
        public static void HideLoadingIndicator()
        {
            loadingIndicator = true;
        }
        public static string[] PassSaltGenerator(string password)
        {
            byte[] saltValue = new byte[8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltValue);
            }

            var keyGenerator = new Rfc2898DeriveBytes(password, saltValue, 10000);
            byte[] encryptionKey = keyGenerator.GetBytes(32); // 256 bit = 32 byte

            string encryptionKeyString = Convert.ToBase64String(encryptionKey);
            string saltValueString = Convert.ToBase64String(saltValue);

            string[] result = { saltValueString, encryptionKeyString };
            return result;

        }



        // Tüm kullanıcıları listeleme işlemi
        public static async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var users = await context.Users.ToListAsync();
                    return users;
                }
                    
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        // ID'ye göre kullanıcı getirme işlemi
        public static async Task<User> GetUserById(Guid id)
        {
            try
            {
                ShowLoadingIndicator();
                using (var context = new qAppDBContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
                    return user;
                }
               
            }
            finally
            {
                HideLoadingIndicator();
            }

        }

        // Kullanıcı ekleme işlemi (Register)
        public static async Task<bool> AddUser(User user)
        {
            try
            {
                ShowLoadingIndicator();
                string[] passSalt = PassSaltGenerator(user.Password);
                using (var db = new qAppDBContext())
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = passSalt[1],
                        Salt = passSalt[0],
                        Level = 1,
                    };
                    var isDuplicate = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                    if(isDuplicate != null)
                    {
                        return false;
                    } 
                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
 
        }

        // Kullanıcı Login servisi
        public static async Task<bool> LoginUser(string email,string password)
        {
          ShowLoadingIndicator();
                bool isValid = false;

            using (var context = new qAppDBContext())
            {

                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                    {
                        isValid = false;
                        //return false; // Kullanıcı adı yanlış
                    }
                    else
                    {
                        byte[] saltValue = Convert.FromBase64String(user.Salt);
                        var keyGenerator = new Rfc2898DeriveBytes(password, saltValue, 10000);
                        byte[] encryptionKey = keyGenerator.GetBytes(32); // 256 bit = 32 byte
                        string encryptionKeyString = Convert.ToBase64String(encryptionKey);

                        if (encryptionKeyString.Equals(user.Password))
                        {
                            isValid = true;
                            //return true; // Şifre doğru
                            UserSession.Instance.SetCurrentUser(user);
                        }
                        else
                        {
                            isValid = false;
                            //return false; // Şifre yanlış
                        }
                    }
                
                return isValid;
            }
         
        }

        // Kullanıcı güncelleme işlemi
        public static async Task<bool> UpdateUser(User user)
        {
            try
            {
                ShowLoadingIndicator();
                string[] passSalt = PassSaltGenerator(user.Password);
                using (var db = new qAppDBContext())
                {
                    var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                    if (existingUser != null)
                    {
                        existingUser.UserName = user.UserName;
                        existingUser.Email = user.Email;
                        existingUser.Password = passSalt[1];
                        existingUser.Salt = passSalt[0];
                        existingUser.Level = user.Level;
                        existingUser.Xp = user.Xp;

                        await db.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        // Kullanıcı oturum kapatma işlemi
        public static void LogoutUser()
        {
            UserSession.Instance.SetCurrentUser(null);
        }
    }
}
