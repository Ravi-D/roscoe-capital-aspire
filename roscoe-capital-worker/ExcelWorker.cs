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
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, ct);
            }
        }
    }
}
