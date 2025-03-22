using ClosedXML.Excel;

namespace roscoe_capital_worker
{
    public class ExcelHandler : IExcelHandler
    {
        private readonly ILogger<ExcelHandler> _logger;
        public ExcelHandler(ILogger<ExcelHandler> logger)
        {
            _logger = logger;
        }

        public IXLWorkbook OpenXLWorkbook(string filePath)
        {
            return new XLWorkbook(filePath);
        }

        public void ReadXLWorkbook(IXLWorkbook workbook)
        {
            using (workbook) 
            {
                IEnumerable<IXLRangeRow>? rows = workbook.Worksheet(1).RangeUsed()
                            .RowsUsed().Skip(1)
                            .Where(row => !row.IsEmpty());
            }
        }
    }
}