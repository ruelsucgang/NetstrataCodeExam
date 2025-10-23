using System.Text.RegularExpressions;
using SharpToScript.Core.Abstractions;

namespace SharpToScript.Services.Helpers
{
    internal sealed class TypeMapperService : ITypeMapper
    {
        public (string tsType, bool isArray) Map(string csType)
        {
            var m = Regex.Match(csType, @"^List\s*<\s*(?<inner>[A-Za-z_][A-Za-z0-9_]*)\s*>$");
            if (m.Success)
            {
                var innerTs = MapScalar(m.Groups["inner"].Value);
                return (innerTs, true);
            }

            return (MapScalar(csType), false);
        }

        private static string MapScalar(string cs) => cs switch
        {
            "string" => "string",
            "int" => "number",
            "long" => "number",
            _ => cs 
        };
    }
}
