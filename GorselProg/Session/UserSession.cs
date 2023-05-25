using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Session
{
    class UserSession
    {
        private static UserSession _instance;
        private User _currentUser;

        private UserSession() {
            _currentUser = new User();
        }

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
        public User GetCurrentUser()
        {
            return _currentUser;
        }

    }
}
