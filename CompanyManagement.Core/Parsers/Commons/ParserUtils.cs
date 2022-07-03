using CompanyManagement.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers.Commons
{
    public static class ParserUtils
    {
        private static readonly Type ignoreAttributeType = typeof(ParserIgnoreAttribute);

        /// <summary>
        /// Reads the content and returns a list of lines.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetLinesAsync(Stream stream)
        {
            if (stream is null)
            {
                return null;
            }

            stream.Position = 0;

            // Read all file.
            using (var streamReader = new StreamReader(stream))
            {
                var data = await streamReader.ReadToEndAsync();

                return data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        /// <summary>
        /// Returns a list of tokens splitted by splitChar.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static List<string> GetTokens(string line, string splitChar)
        {
            if (!line.Contains("\""))
            {
                return line.Split(splitChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            }

            var tokens = new List<string>();

            do
            {
                if (line.StartsWith("\""))
                {
                    line = line.Substring(1);
                    var index = line.IndexOf($"\"{splitChar}");
                    tokens.Add(line.Substring(0, index).Replace("\"\"", "\""));

                    line = line.Remove(0, index + splitChar.Length);
                }
                else
                {
                    tokens.AddRange(line.Split(splitChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList());
                    line = string.Empty;
                }
            } while (line.Length > 0);

            return tokens;
        }

        /// <summary>
        /// Gets a list of properties for the given type sorted by parser's attributes.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyItem> GetColumns(Type type)
        {

            // Get properties that don't have ParserIgnoreAttribute attribute.
            var properties = type.GetProperties()
                                  .Where(x => !x.GetCustomAttributes(ignoreAttributeType).Any())
                                  .Select(y => new PropertyItem(y))
                                  .OrderBy(x => x.Order)
                                  .ToList();

            return properties;
        }

        public static Stream Parse<T>(List<T> data, string splitChar)
        {
            var type = typeof(T);
            var properties = GetColumns(type);


            Stream stream = new MemoryStream();
            var tokens = new List<string>();

            var streamWriter = new StreamWriter(stream);
            foreach (var item in data)
            {
                tokens.Clear();

                // Get values in order.
                foreach (var property in properties)
                {
                    string itemData = property.Property.GetValue(item)?.ToString() ?? string.Empty;

                    if (itemData.Contains("\""))
                    {
                        itemData = itemData.Replace("\"", "\"\"");
                        itemData = $"\"{itemData}\"";
                    }
                    else if (itemData.Contains(splitChar))
                    {
                        itemData = $"\"{itemData}\"";
                    }

                    tokens.Add(itemData);
                }

                // Build line and write it to stream.
                streamWriter.WriteLine(string.Join(splitChar, tokens));
            }

            streamWriter.Flush();
            return stream;
        }

    }
}
