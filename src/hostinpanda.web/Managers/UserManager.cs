using System;
using System.Linq;
using System.Threading.Tasks;
using hostinpanda.library.Common;
using hostinpanda.web.Common;
using hostinpanda.library.DAL.Tables;

namespace hostinpanda.web.Managers
{
    public class UserManager : BaseManager
    {
        public UserManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public ReturnContainer<Users> Login(string username, string password)
        {
            try
            {
                var result = Wrapper.DbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == HashString(password));

                if (result == null)
                {
                    throw new Exception("User doesn't exist");
                }

                return new ReturnContainer<Users>(result);
            } catch (Exception ex)
            {
                return new ReturnContainer<Users>(null, ex.ToString());
            }
        }

        public async Task<ReturnContainer<int>> CreateUser(string username, string password)
        {
            if (Wrapper.DbContext.Users.Any(a => a.Username == username && a.Active))
            {
                throw new Exception("User already exists");
            }

            var user = new Users
            {
                Username = username,
                Password = HashString(password)
            };

            await Wrapper.DbContext.Users.AddAsync(user);

            await Wrapper.DbContext.SaveChangesAsync();

            return new ReturnContainer<int>(user.ID);         
        }
    }
}