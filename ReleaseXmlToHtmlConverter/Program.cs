using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReleaseXmlToHtmlConverter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var programArguments = new ProgramArguments(args);
                programArguments.ValidateAndSetParameters();

                if (!programArguments.AreValid)
                {
                    Console.WriteLine(programArguments.ErrorMessage);
                    Console.ReadKey();
                    return;
                }

                if (programArguments.Path == null)
                {
                    Console.WriteLine("Unexpected error occured.");
                    return;
                }

                var converter = new Converter(programArguments.Path, programArguments.CustomerIds?.ToArray());
                await converter.Convert();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}