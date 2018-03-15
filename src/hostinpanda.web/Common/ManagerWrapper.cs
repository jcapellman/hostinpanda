using hostinpanda.library.DAL;

namespace hostinpanda.web.Common
{
    public class ManagerWrapper
    {
        public DALdbContext DbContext { get; internal set; }

        public HostinUser CurrentUser { get; internal set; }
    }
}