namespace hostinpanda.daemon
{
    class Program
    {   
        static void Main(string[] args)
        {
            var mainService = new MainService();

            mainService.Init(ConfigObject.Load("config.json"));

            mainService.Run();
        }
    }
}