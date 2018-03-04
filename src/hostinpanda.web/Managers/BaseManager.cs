using System;
using System.Security.Cryptography;
using System.Text;

using hostinpanda.web.Common;

namespace hostinpanda.web.Managers
{
    public class BaseManager
    {
        protected readonly ManagerWrapper Wrapper;

        protected BaseManager(ManagerWrapper wrapper)
        {
            Wrapper = wrapper;
        }

        protected static string HashString(string input)
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