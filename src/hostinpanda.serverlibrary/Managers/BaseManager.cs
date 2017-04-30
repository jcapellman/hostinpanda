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
    }
}