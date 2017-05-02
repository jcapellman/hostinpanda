using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.serverlibrary.Managers
{
    public class BaseManager
    {
        protected ManagerWrapper Wrapper;

        public BaseManager(ManagerWrapper wrapper)
        {
            Wrapper = wrapper;
        }

        public int GenerateID(List<int> ids)
        {
            var rand = new Random((int)DateTime.Now.Ticks);

            var id = 0;

            while (!ids.Contains(id))
            {
                id = rand.Next();
            }

            return id;
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