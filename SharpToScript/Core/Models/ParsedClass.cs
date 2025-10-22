using System.Collections.Generic;

namespace SharpToScript.Core.Models
{
    public sealed class ParsedClass
    {
        public string Name { get; }
        public List<ParsedProperty> Properties { get; }
        public ParsedClass? NestedClass { get; }

        public ParsedClass(string name, List<ParsedProperty> properties, ParsedClass? nestedClass = null)
        {
            Name = name;
            Properties = properties;
            NestedClass = nestedClass;
        }
    }
}