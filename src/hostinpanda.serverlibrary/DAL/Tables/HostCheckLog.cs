namespace hostinpanda.serverlibrary.DAL.Tables
{
    public class HostCheckLog : BaseTable
    {
        public int HostID { get; set; }

        public bool IsUp { get; set; }
    }
}