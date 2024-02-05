using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CloverParser.Services
{
    public class FileParser : IFileParser
    {
        private const string DATA_REGEX = "^(.*)(_[0-9]{4}\\-[0-9]{1,2}\\-[0-9]{1,2})\\.txt$";
        private const string DATA_PATH = "../../../Data/";
        private const string SPEC_PATH = "../../../Specs/";
        private const string OUTPUT_PATH = "../../../Output/";
        private readonly ISpecProcessor _specProcessor;
        private readonly IDataProcessor _dataProcessor;

        public FileParser(ISpecProcessor specProcessor, IDataProcessor dataProcessor)
        {
            _specProcessor = specProcessor;
            _dataProcessor = dataProcessor;
        }

        public void Parse()
        {
            try
            {
                var dataFiles = Directory.GetFiles(DATA_PATH).Select(f => Path.GetFileName(f));
                foreach (var dataFile in dataFiles)
                {
                    if (!Regex.IsMatch(dataFile, DATA_REGEX))
                    {
                        Console.WriteLine($"{dataFile} file name is in the wrong format.");
                        continue;
                    }

                    var specFile = $"{Regex.Match(dataFile, DATA_REGEX).Groups[1].Value}";
                    var specFilePath = $"{SPEC_PATH}{Regex.Match(dataFile, DATA_REGEX).Groups[1].Value}.csv";
                    if (!File.Exists(specFilePath))
                    {
                        Console.WriteLine($"{specFile}.csv does not exist.");
                        continue;
                    }

                    var specs = _specProcessor.Process(specFilePath);
                    if (specs.Count == 0)
                    {
                        Console.WriteLine($"{specFile}.csv contains no values.");
                        continue;
                    }

                    var data = _dataProcessor.Process($"{DATA_PATH}{dataFile}", specs);
                    if (data.Count == 0)
                    {
                        Console.WriteLine($"{dataFile} could not be processed.");
                        continue;
                    }

                    var outputFile = $"{specFile}{Regex.Match(dataFile, DATA_REGEX).Groups[2].Value}.ndjson";
                    WriteToOutput(OUTPUT_PATH + outputFile, data);
                    Console.WriteLine($"{outputFile} created.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void WriteToOutput(string filePath, List<Dictionary<string, object>> data)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                foreach (var line in data)
                {
                    var json = JsonConvert.SerializeObject(line);
                    streamWriter.WriteLine(json);
                }
            }
        }
    }
}
