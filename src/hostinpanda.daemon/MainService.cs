using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

using hostinpanda.library.DAL;
using hostinpanda.library.DAL.Tables;

namespace hostinpanda.daemon
{
    public class MainService
    {
        private ConfigObject _config;

        public void Init(ConfigObject config)
        {
            _config = config;
        }

        private async Task<bool> IsAliveAsync(Hosts host)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(host.HostName);

                    return response.IsSuccessStatusCode;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);

                return false;
            }
        }

        private void SendEmail(string receiver, string host, string subject)
        {
            using (var smtpClient = new SmtpClient(_config.SMTPHostName, _config.SMTPPort))
            {
                smtpClient.Credentials = new NetworkCredential(_config.SMTPUsername, _config.SMTPPassword);

                MailMessage message = new MailMessage("no-reply@hostinpanda.com", receiver)
                {
                    Subject = subject
                };

                smtpClient.Send(message);
            }
        }

        void ProcessSuccess(Hosts host)
        {
            using (var db = new DALdbContext(_config.DatabaseConnectionString))
            {
                var lastLog = db.HostLog.Where(a => a.HostID == host.ID && a.Active && a.IsUp).OrderByDescending(a => a.Created).FirstOrDefault();

                var lastUp = host.Created.DateTime;

                if (lastLog == null)
                {
                    return;
                }

                SendEmail(host.User.Username, host.HostName, $"Server {host} is backup after being down since {lastUp} eom");
            }
        }

        private void ProcessFailure(Hosts host)
        {
            using (var db = new DALdbContext(_config.DatabaseConnectionString))
            {
                var lastLog = db.HostLog.Where(a => a.HostID == host.ID && a.Active && a.IsUp).OrderByDescending(a => a.Created).FirstOrDefault();

                var lastUp = host.Created.DateTime;

                if (lastLog != null)
                {
                    lastUp = lastLog.Created.DateTime;
                }

                SendEmail(host.User.Username, host.HostName, $"Server {host} has been down since {lastUp} eom");
            }
        }

        public async void Run()
        {
            while (true)
            {
                using (var db = new DALdbContext(_config.DatabaseConnectionString))
                {
                    var hosts = db.Hosts.Where(a => a.Active).ToList();

                    foreach (var host in hosts)
                    {
                        var alive = await IsAliveAsync(host);

                        var hostLog = new HostCheckLog
                        {
                            HostID = host.ID,
                            IsUp = alive
                        };

                        db.HostLog.Add(hostLog);
                        db.SaveChanges();

                        if (host.AlertsEnabled)
                        {
                            if (!alive)
                            {
                                ProcessFailure(host);
                            } else
                            {
                                ProcessSuccess(host);
                            }
                        }
                    }
                }
                
                await Task.Delay(60000);
            }
        }
    }
}