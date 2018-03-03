namespace hostinpanda.serverlibrary.DAL.Tables
{
    public class Hosts : BaseTable
    {
       public string HostName { get; set; }

       public bool AlertsEnabled { get; set; }

       public int UserID { get; set; }
    }
}