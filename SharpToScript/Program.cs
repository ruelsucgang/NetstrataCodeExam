using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SharpToScript.Core.Abstractions;
using SharpToScript.Services.Parsing;
using SharpToScript.Services.Helpers;
using SharpToScript.Services.Conversion;
using System.Diagnostics.Metrics;

namespace SharpToScript
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<ICSharpParser, CSharpParserService>()
                .AddSingleton<ITypeMapper, TypeMapperService>()
                .AddSingleton<ICaseConverter, CaseConverterService>()
                .AddSingleton<ITypescriptConverter, TypescriptConverterService>()
                .BuildServiceProvider();

            var parser = services.GetRequiredService<ICSharpParser>();
            var converter = services.GetRequiredService<ITypescriptConverter>();

            // this line loads the sample C# class definition string from a separate helper class (SourceInput).
            // purpose: to isolate the input definition for easier maintenance and future modifications.
            var input = SourceInput.CSharpClassDefinition;

            try
            {
                var parsed = parser.Parse(input);
                var ts = converter.Convert(parsed);
                Console.WriteLine("=== TypeScript Output ===");
                Console.WriteLine();
                Console.WriteLine(ts);
                Console.WriteLine("==========================");
                Console.ReadLine();
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
                return 1;
            }
        }

       private static string ReadAllFromStdIn()
        {
            var sb = new StringBuilder();
            string? line;
            while ((line = Console.ReadLine()) is not null)
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}