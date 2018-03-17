namespace hostinpanda.daemon
{
    class Program
    {   
        static void Main(string[] args)
        {
            var mainService = new MainService();

            mainService.Init(ConfigObject.Load("config.json"));

            while (true)
            {
                mainService.Run();
                
                System.Threading.Tasks.Task.Delay(300000).Wait();
            }
        }
    }
}