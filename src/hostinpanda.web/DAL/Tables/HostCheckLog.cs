using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hostinpanda.web.DAL.Tables
{
    public class HostCheckLog : BaseTable
    {
        [Required]
        [ForeignKey("HostID")]
        public Hosts Host { get; set; }

        public int HostID { get; set; }

        public bool IsUp { get; set; }
    }
}