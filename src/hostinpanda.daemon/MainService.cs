using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;

using hostinpanda.library.DAL;
using hostinpanda.library.DAL.Tables;

using Microsoft.EntityFrameworkCore;

namespace hostinpanda.daemon
{
    public class MainService
    {
        private ConfigObject _config;

        public void Init(ConfigObject config)
        {
            _config = config;
        }

        private bool IsAlive(Hosts host)
        {
            try
            {
                try
                {
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.Connect(host.HostName, 443);
                        return true;
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex);

                    return false;
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
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

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

                SendEmail(host.User.Username, host.HostName, $"Restored Server {host.HostName} - down since {lastUp} eom");
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

                SendEmail(host.User.Username, host.HostName, $"Dead Server {host} - down since {lastUp} eom");
            }
        }

        public void Run()
        {
            while (true)
            {
                using (var db = new DALdbContext(_config.DatabaseConnectionString))
                {
                    var hosts = db.Hosts.Include(a => a.User).Where(a => a.Active).ToList();

                    foreach (var host in hosts)
                    {
                        var alive = IsAlive(host);

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
                
                Task.Delay(60000);
            }
        }
    }
}