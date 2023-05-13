using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Session
{
    public sealed class UserSession
    {
        private static readonly UserSession instance = new UserSession();

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                return instance;
            }
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
    }
}
