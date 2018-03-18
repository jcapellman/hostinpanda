using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hostinpanda.library.DAL.Tables
{
    public class Hosts : BaseTable
    {
        public string HostName { get; set; }

        public bool AlertsEnabled { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public Users User { get; set; }

        public int UserID { get; set; }

        public int PortNumber { get; set; }

        public int AllowableDowntimeMinutes { get; set; }
    }
}