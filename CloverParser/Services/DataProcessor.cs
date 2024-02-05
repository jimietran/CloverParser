using CloverParser.Helpers;
using CloverParser.Models;

namespace CloverParser.Services
{
    public class DataProcessor : IDataProcessor
    {
        private readonly ITypeHelper _typeHelper;

        public DataProcessor(ITypeHelper typeHelper)
        {
            _typeHelper = typeHelper;
        }

        public List<Dictionary<string, object>> Process(string filePath, List<SpecModel> specs)
        {
            var processedData = new List<Dictionary<string, object>>();
            using (var streamReader = new StreamReader(filePath))
            {
                var line = "";
                while ((line = streamReader.ReadLine()) != null)
                {
                    var lineData = new Dictionary<string, object>();
                    var index = 0;
                    foreach (var spec in specs)
                    {
                        if (index + spec.Width > line.Length)
                        {
                            Console.WriteLine("Reached end of line.");
                            break;
                        }
                        var value = line.Substring(index, spec.Width).Trim();
                        var convertedValue = _typeHelper.ConvertValue(spec.DataType, value);
                        if (convertedValue != null)
                            lineData.Add(spec.ColumnName, convertedValue);
                        index += spec.Width;
                    }
                    processedData.Add(lineData);
                }
            }

            return processedData;
        }
    }
}
