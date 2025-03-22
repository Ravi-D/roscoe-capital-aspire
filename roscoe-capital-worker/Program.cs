namespace roscoe_capital_worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<ExcelWorker>();
            builder.Services.AddSingleton<IExcelHandler, ExcelHandler>(); 

            IHost host = builder.Build();
            host.Run();
        }
    }
}