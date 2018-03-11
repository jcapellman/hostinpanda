using hostinpanda.web.DAL;

namespace hostinpanda.web.Common
{
    public class ManagerWrapper
    {
        public DALdbContext DbContext { get; internal set; }

        public int CurrentUserID { get; internal set; }
    }
}