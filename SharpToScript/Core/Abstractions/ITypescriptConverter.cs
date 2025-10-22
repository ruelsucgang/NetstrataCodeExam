using SharpToScript.Core.Models;

namespace SharpToScript.Core.Abstractions
{
    public interface ITypescriptConverter
    {
        string Convert(ParsedClass root);
    }
}
