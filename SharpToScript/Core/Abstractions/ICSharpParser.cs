using SharpToScript.Core.Models;

namespace SharpToScript.Core.Abstractions
{
    public interface ICSharpParser
    {
        ParsedClass Parse(string input);
    }
}