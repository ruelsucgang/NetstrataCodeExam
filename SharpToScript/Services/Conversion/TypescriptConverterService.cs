using System.Text;
using SharpToScript.Core.Abstractions;
using SharpToScript.Core.Models;

namespace SharpToScript.Services.Conversion
{
    internal sealed class TypescriptConverterService : ITypescriptConverter
    {
        private readonly ITypeMapper _typeMapper;
        private readonly ICaseConverter _caseConverter;

        public TypescriptConverterService(ITypeMapper typeMapper, ICaseConverter caseConverter)
        {
            _typeMapper = typeMapper;
            _caseConverter = caseConverter;
        }
        public string Convert(ParsedClass root)
        {
            var sb = new StringBuilder();
            WriteInterface(sb, root);

            if (root.NestedClass is not null)
            {
                sb.AppendLine();
                WriteInterface(sb, root.NestedClass);
            }

            return sb.ToString();
        }
        private void WriteInterface(StringBuilder sb, ParsedClass cls) 
        {
            sb.AppendLine($"export interface {cls.Name} {{");
            foreach (var p in cls.Properties)
                AppendTsProperty(sb, p);
            sb.AppendLine("}");    
        }
        private void AppendTsProperty(StringBuilder sb, ParsedProperty prop)
        {
            var (tsType, isArray) = _typeMapper.Map(prop.CsType);
            var name = _caseConverter.ToCamel(prop.Name);
            var optional = prop.IsNullable ? "?" : "";
            var finalType = isArray ? $"{tsType}[]" : tsType;
            sb.AppendLine($"    {name}{optional}: {finalType};");
        }
    }
}
