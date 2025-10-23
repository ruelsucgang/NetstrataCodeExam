using System.Text;
using System.Text.RegularExpressions;
using SharpToScript.Core.Abstractions;

namespace SharpToScript.Services.Helpers
{
    internal sealed class CaseConverterService : ICaseConverter
    {
        public string ToCamel(string pascal)
        {
            if (string.IsNullOrWhiteSpace(pascal)) return pascal;
            if (pascal.Length == 1) return pascal.ToLowerInvariant();
            if (Regex.IsMatch(pascal, @"^[A-Z0-9]+$")) return pascal.ToLowerInvariant();

            var sb = new StringBuilder(pascal);
            sb[0] = char.ToLowerInvariant(sb[0]);
            return sb.ToString();
        }
    }
}
