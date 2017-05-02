using System;
using System.Linq;
using System.Threading.Tasks;

using hostinpanda.clientlibrary;
using hostinpanda.serverlibrary.DAL;
using hostinpanda.serverlibrary.DAL.Tables;
using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.serverlibrary.Managers
{
    public class UserManager : BaseManager
    {
        public UserManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public async Task<ReturnContainer<bool>> CreateUser(string username, string password)
        {
            using (var eFactory = new EntityFactory(Wrapper.DBConnectionString))
            {
                var usersResult = await eFactory.GetListAsync<Users>("Users");

                if (usersResult.HasError)
                {
                    throw new Exception(usersResult.ErrorString);
                }

                if (usersResult.ObjectValue.Any(a => a.Username == username))
                {
                    throw new Exception("User already exists");
                }

                usersResult.ObjectValue.Add(new Users
                {
                    Username = username,
                    Password = HashString(password),
                    Active = true,
                    Created = DateTimeOffset.Now,
                    Modified = DateTimeOffset.Now,
                    ID = GenerateID(usersResult.ObjectValue.Select(a => a.ID).ToList())
                });

                return await eFactory.WriteAsync("Users", usersResult.ObjectValue);
            }
        }
    }
}