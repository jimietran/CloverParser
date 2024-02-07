using CloverParser.Models;

namespace CloverParser.Services
{
    public class SpecProcessor : ISpecProcessor
    {
        public List<SpecModel> Process(string filePath)
        {
            var specs = new List<SpecModel>();
            using (var streamReader = new StreamReader(filePath))
            {
                var line = "";
                while ((line = streamReader.ReadLine()) != null)
                {
                    var columns = line.Split(',');
                    if (columns.Length != 3)
                    {
                        Console.WriteLine("Specification contains more than 3 columns, skipping current line.");
                        continue;
                    }

                    var columnName = columns[0].Trim();
                    _ = int.TryParse(columns[1].Trim(), out int width);
                    if (width == 0)
                    {
                        Console.WriteLine("Width is 0, skipping current line.");
                        continue;
                    }

                    Enum.TryParse(columns[2].Trim(), true, out DataType dataType);
                    specs.Add(new SpecModel
                    {
                        ColumnName = columnName,
                        Width = width,
                        DataType = dataType
                    });
                }
            }

            return specs;
        }
    }
}
