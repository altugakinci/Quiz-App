using GorselProg.Model;
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
    class UserService
    {

        private readonly qAppDBContext _context;
        public bool loadingIndicator = false;


        public void ShowLoadingIndicator()
        {
            this.loadingIndicator = true;
        }
        public void HideLoadingIndicator()
        {
            this.loadingIndicator = true;
        }
        public string[] PassSaltGenerator(string password)
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


        public UserService(qAppDBContext context)
        {
            _context = context;
        }

        // Tüm kullanıcıları listeleme işlemi
        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                ShowLoadingIndicator();
                var users = await  _context.Users.ToListAsync();
                return users;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        // ID'ye göre kullanıcı getirme işlemi
        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                ShowLoadingIndicator();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            finally
            {
                HideLoadingIndicator();
            }

        }

        // Kullanıcı ekleme işlemi (Register)
        public async Task<bool> AddUser(User user)
        {
            try
            {
                ShowLoadingIndicator();
                string[] passSalt = PassSaltGenerator(user.Password);
                using (var db = _context)
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
        public async Task<bool> LoginUser(string email,string password)
        {
            try
            {
                ShowLoadingIndicator();
                bool isValid = false;

                List<User> allUser = await _context.Users.ToListAsync();
                var user = await _context.Users.FirstAsync(u => u.Email == email);

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

                        if (encryptionKeyString == user.Password)
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
            catch
            {
                // An error 
                return false;
            }
            finally
            {
                HideLoadingIndicator();
            }
        }

        // Kullanıcı güncelleme işlemi
        public void UpdateUser(User user)
        {
            //_context.Users.Update(user);
            _context.SaveChanges();
           
        }

        // Kullanıcı oturum kapatma işlemi
        public void LogoutUser()
        {
            UserSession.Instance.SetCurrentUser(null);
        }
    }
}
