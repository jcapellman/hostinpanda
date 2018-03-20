using System.ComponentModel.DataAnnotations;

using hostinpanda.library.Enums;
using hostinpanda.web.Common;

namespace hostinpanda.web.Models
{
    public class NewHostModel
    {
        [Display(Name = "Host Name")]
        [Required]
        public string HostName { get; set; }

        [Display(Name = "Port #")]
        [Required]        
        public int PortNumber { get; set; } = Constants.DEFAULT_PORT_NUMBER;

        [Display(Name = "Port Type")]
        [Required]
        public PortType PortType { get; set; } = PortType.TCP;

        [Display(Name = "Allowable Downtime (Minutes)")]
        [Required]        
        public int AllowableDowntimeMinutes { get; set; } = Constants.DEFAULT_ALLOWED_DOWNTIME_MINUTES;
    }
}