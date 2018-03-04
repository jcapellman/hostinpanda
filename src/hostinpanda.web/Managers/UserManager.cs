using System;
using System.Linq;
using System.Threading.Tasks;

using hostinpanda.web.Common;
using hostinpanda.web.DAL;
using hostinpanda.web.DAL.Tables;

namespace hostinpanda.web.Managers
{
    public class UserManager : BaseManager
    {
        public UserManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public ReturnContainer<Users> Login(string username, string password)
        {
            using (var eFactory = new EntityFactory(Wrapper.GSettings.DatabaseConnection))
            {
                var result =
                    eFactory.Users.FirstOrDefault(a => a.Username == username && a.Password == HashString(password));

                if (result == null)
                {
                    throw new Exception("User doesn't exist");
                }
                
                return new ReturnContainer<Users>(result);
            }
        }

        public async Task<ReturnContainer<bool>> CreateUser(string username, string password)
        {
            using (var eFactory = new EntityFactory(Wrapper.GSettings.DatabaseConnection))
            {
                if (eFactory.Users.Any(a => a.Username == username && a.Active))
                {
                    throw new Exception("User already exists");
                }

                var user = new Users
                {
                    Username = username,
                    Password = HashString(password)
                };

                await eFactory.Users.AddAsync(user);

                await eFactory.SaveChangesAsync();

                return new ReturnContainer<bool>(true);
            }
        }
    }
}