using NUnit.Framework;
using System;

namespace ReleaseXmlToHtmlConverter.UnitTests
{
    public class ConverterTests
    {
        [Test]
        public void Converter_WhenCreatedWithEmptyPathToXmlFile_Throws()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Converter(pathToXmlFile: "", null));
        }
    }
}