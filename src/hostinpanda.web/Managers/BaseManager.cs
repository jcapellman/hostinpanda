using System;
using System.Security.Cryptography;
using System.Text;
using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.web.Managers
{
    public class BaseManager
    {
        protected ManagerWrapper Wrapper;

        public BaseManager(ManagerWrapper wrapper)
        {
            Wrapper = wrapper;
        }

        protected string HashString(string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));

                var hashbytes = algorithm.ComputeHash(hash);

                return Convert.ToBase64String(hashbytes);
            }
        }
    }
}