namespace roscoe_capital_worker
{
    public class ExcelWorker : BackgroundService
    {
        private readonly ILogger<ExcelWorker> _logger;

        public ExcelWorker(ILogger<ExcelWorker> logger, IExcelHandler excelhandler)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(2.5), ct);
            }
        }
    }
}
