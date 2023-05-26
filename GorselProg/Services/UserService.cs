using GorselProg.Model;
using GorselProg.Objects;
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
            loadingIndicator = false;
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
                    if (isDuplicate != null)
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
        public static async Task<bool> LoginUser(string email, string password)
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
        public static async Task<bool> UpdateUser(User user, string currentPassword, Guid currentId)
        {
            try
            {
                ShowLoadingIndicator();


                using (var db = new qAppDBContext())
                {
                    var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Id == currentId);

                    if (existingUser != null)
                    {
                        byte[] saltValue = Convert.FromBase64String(existingUser.Salt);
                        var keyGenerator = new Rfc2898DeriveBytes(currentPassword, saltValue, 10000);
                        byte[] encryptionKey = keyGenerator.GetBytes(32); // 256 bit = 32 byte
                        string encryptionKeyString = Convert.ToBase64String(encryptionKey);

                        if (encryptionKeyString.Equals(existingUser.Password))
                        {
                            //return true; // Şifre doğru
                            string[] passSalt = PassSaltGenerator(user.Password);

                            existingUser.UserName = user.UserName;
                            existingUser.Email = user.Email;
                            existingUser.Password = passSalt[1];
                            existingUser.Salt = passSalt[0];

                            UserSession.Instance.SetCurrentUser(existingUser);
                            await db.SaveChangesAsync();

                            return true;
                        }
                        else return false;
                    }

                    else
                    {
                        return false;
                    }
                }
            }
            //catch
            //{
            //    return false;
            //}
            finally
            {
                HideLoadingIndicator();
            }
        }

        public static async Task<bool> UpdateUserLevel(Guid userId, int newLevel, int newXp)
        {
            try
            {
                ShowLoadingIndicator();

                using (var db = new qAppDBContext())
                {
                    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                    if (user != null)
                    {
                        user.Level = newLevel;
                        user.Xp = newXp;

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

        public static async Task<UserGamesSummary> GetUserGamesSummary(Guid userId)
        {
            ShowLoadingIndicator();
            using (var context = new qAppDBContext())
            {
                var userGamesSummary = new UserGamesSummary();

                userGamesSummary.TotalGamesPlayed = await context.Answers
                    .Where(a => a.UserId == userId)
                    .Select(a => a.GameId)
                    .Distinct()
                    .CountAsync();

                var distinctGameIds = await context.Answers
                    .Where(a => a.UserId == userId)
                    .Select(a => a.GameId)
                    .Distinct()
                    .ToListAsync();

                userGamesSummary.WonGames = distinctGameIds.Count(g => context.Answers
                    .Where(a => a.UserId == userId && a.GameId == g)
                    .Sum(a => a.GainedXp) == distinctGameIds.Max(g2 => context.Answers
                        .Where(a => a.UserId == userId && a.GameId == g2)
                        .Sum(a => a.GainedXp)));

                userGamesSummary.CorrectAnswers = await context.Answers
                    .CountAsync(a => a.UserId == userId && a.GainedXp >= 50);

                // Kategori bazında doğru sayısını hesapla ve atama yap
                var categoryIds = await context.Categories.OrderBy(c => c.Index).Select(c => c.Id).ToListAsync();

                Guid? Category_1 = categoryIds[0];
                Guid? Category_2 = categoryIds[1];
                Guid? Category_3 = categoryIds[2];
                Guid? Category_4 = categoryIds[3];
                Guid? Category_5 = categoryIds[4];

                userGamesSummary.Category1Correct = await GetCategoryCorrectCount(userId, (Guid)Category_1);
                userGamesSummary.Category2Correct = await GetCategoryCorrectCount(userId, (Guid)Category_2);
                userGamesSummary.Category3Correct = await GetCategoryCorrectCount(userId, (Guid)Category_3);
                userGamesSummary.Category4Correct = await GetCategoryCorrectCount(userId, (Guid)Category_4);
                userGamesSummary.Category5Correct = await GetCategoryCorrectCount(userId, (Guid)Category_5);
                HideLoadingIndicator();
                return userGamesSummary;
            }
        }

        private static async Task<int> GetCategoryCorrectCount(Guid userId, Guid categoryId)
        {
            using (var context = new qAppDBContext())
            {
                var questionIds = await context.Questions
                    .Where(q => q.CategoryId == categoryId)
                    .Select(q => q.Id)
                    .ToListAsync();

                return await context.Answers
                    .CountAsync(a => a.UserId == userId && questionIds.Contains((Guid)a.QuestionId) && a.GainedXp >= 50);
            }
        }

    }
}
