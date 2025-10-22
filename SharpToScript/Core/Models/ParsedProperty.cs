namespace SharpToScript.Core.Models
{
    public sealed class ParsedProperty
    {
        public string CsType { get; }
        public string Name { get; }
        public bool IsNullable { get; }

        public ParsedProperty(string csType, string name, bool isNullable)
        {
            CsType = csType;
            Name = name;
            IsNullable = isNullable;
        }
    }
}
