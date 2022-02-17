using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReleaseXmlToHtmlConverter
{
    internal class Converter
    {
        readonly string _customerOutputDirectory;
        readonly string _htmlFilePath;
        readonly string _pathToXmlFile;
        readonly IEnumerable<string>? _customerIds;

        internal Converter(string pathToXmlFile, IEnumerable<string>? customerIds)
        {
            _pathToXmlFile = pathToXmlFile != ""
                ? pathToXmlFile
                : throw new ArgumentException("Path to xml file can not be empty.", nameof(pathToXmlFile));
            _customerIds = customerIds?.Distinct();
            _customerOutputDirectory = "output";
            _htmlFilePath = $"{_customerOutputDirectory}\\Sprint_11945.html";
        }

        internal async Task Convert()
        {
            if (!File.Exists(_pathToXmlFile))
                throw new FileNotFoundException("Xml file is not found.");

            PrepareOutputDirectory();

            var reader = XmlReader.Create(_pathToXmlFile,
                new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Parse,
                    Async = true
                });

            await reader.MoveToContentAsync();

            if (_customerIds?.Any() == true)
                await CreateHtmlFileForEachCustomers(reader);
            else
                await CreateHtmlFileForReleaseNotes(reader);
        }

        void PrepareOutputDirectory()
        {
            var di = new DirectoryInfo(_customerOutputDirectory);

            if (!di.Exists)
                di.Create();
            else
            {
                foreach (var file in di.GetFiles())
                    file.Delete();
            }
        }

        async Task CreateHtmlFileForEachCustomers(XmlReader reader)
        {
            if (_customerIds?.Any() != true)
                return;

            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        reader.MoveToAttribute(Constants.CustomerIdXmlAttributeName);

                        if (_customerIds.Contains(reader.Value))
                        {
                            await HandleCustomerFile(reader);
                            reader.MoveToElement();
                        }

                        break;
                    default:
                        continue;
                }
            }
        }

        async Task CreateHtmlFileForReleaseNotes(XmlReader reader)
        {
            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name != Constants.ReleaseNoteNodeName)
                            return;

                        await WriteReleaseNote(reader, _htmlFilePath);
                        break;
                    case XmlNodeType.XmlDeclaration:
                        await BeginHtmlFile(_htmlFilePath);
                        break;
                    case XmlNodeType.EndElement:
                        await EndHtmlFile(_htmlFilePath);
                        break;
                    default:
                        continue;
                }
            }
        }

        async Task HandleCustomerFile(XmlReader reader)
        {
            if (File.Exists(GetCustomerFileName(reader.Value)))
                await WriteReleaseNote(reader, GetCustomerFileName(reader.Value));
            else
                await CreateHtmlFile(reader, GetCustomerFileName(reader.Value));
        }

        string GetCustomerFileName(string customerId) =>
            _htmlFilePath.Insert(_htmlFilePath.IndexOf('.'), $"_{customerId}");

        static async Task CreateHtmlFile(XmlReader reader, string fileName)
        {
            await BeginHtmlFile(fileName);
            await WriteReleaseNote(reader, fileName);
            await EndHtmlFile(fileName);
        }

        static async Task BeginHtmlFile(string fileName)
        {
            await File.AppendAllTextAsync(fileName,
                @"<html>" +
                "<head>" +
                "<meta charset=\"utf-8\">" +
                "<title>Release Notes</title>" +
                "</head>" +
                "<body>");
        }

        static async Task EndHtmlFile(string fileName)
        {
            await File.AppendAllTextAsync(fileName, "</body></html>");
        }

        static async Task WriteReleaseNote(XmlReader reader, string fileName)
        {
            reader.MoveToElement();

            for (var i = 0; i < reader.AttributeCount; i++)
            {
                reader.MoveToAttribute(i);
                await File.AppendAllTextAsync(fileName, $"<p>{reader.Name}:{reader.Value}</p>", Encoding.UTF8);
            }

            await File.AppendAllTextAsync(fileName, "<br>");
            reader.MoveToElement();
        }
    }
}