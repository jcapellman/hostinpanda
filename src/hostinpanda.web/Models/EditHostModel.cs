using System.ComponentModel.DataAnnotations;

using hostinpanda.web.Common;

namespace hostinpanda.web.Models
{
    public class EditHostModel
    {
        public int ID { get; set; }

        [Display(Name = "Host Name")]
        [Required]
        [DataType(DataType.Text)]
        public string HostName { get; set; }

        [Display(Name = "Port #")]
        [Required]                
        public int PortNumber { get; set; } = Constants.DEFAULT_PORT_NUMBER;

        [Display(Name = "Allowable Downtime (Minutes)")]
        [Required]                
        public int AllowableDowntimeMinutes { get; set; } = Constants.DEFAULT_ALLOWED_DOWNTIME_MINUTES;
    }
}