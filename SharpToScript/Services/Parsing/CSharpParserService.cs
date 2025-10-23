using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SharpToScript.Core.Abstractions;
using SharpToScript.Core.Models;

namespace SharpToScript.Services.Parsing
{
    internal sealed class CSharpParserService : ICSharpParser
    {
        private static readonly Regex ClassLine =
            new(@"^public\s+class\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*\{?\s*$",
                RegexOptions.Compiled);

        private static readonly Regex PropertyLine =
            new(@"^public\s+(?<type>[A-Za-z0-9_<>\?\s]+)\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*\{\s*get;\s*set;\s*\}\s*$",
                RegexOptions.Compiled);

        public ParsedClass Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input is empty.");

            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            string? rootName = null;
            var rootProps = new List<ParsedProperty>();

            string? nestedName = null;
            var nestedProps = new List<ParsedProperty>();

            var collectingNested = false;
            var braceDepth = 0;

            foreach (var raw in lines)
            {
                var line = raw.Trim();

                // this will count braces to know when nested ends
                foreach (var ch in line)
                {
                    if (ch == '{') braceDepth++;
                    else if (ch == '}') braceDepth--;
                }

                // class declarations detection
                var mClass = ClassLine.Match(line);
                if (mClass.Success)
                {
                    var name = mClass.Groups["name"].Value;

                    if (rootName is null)
                    {
                        rootName = name;           // if first class is root
                    }
                    else
                    {
                        nestedName = name;         // if the second class is nested
                        collectingNested = true;
                        nestedProps.Clear();
                    }

                    continue;
                }

                // detect properties
                var mProp = PropertyLine.Match(line);
                if (mProp.Success)
                {
                    var csType = CleanupType(mProp.Groups["type"].Value);
                    var propName = mProp.Groups["name"].Value;
                    var isNullable = csType.EndsWith("?");

                    csType = csType.Replace("?", "");

                    var target = collectingNested ? nestedProps : rootProps;
                    target.Add(new ParsedProperty(csType, propName, isNullable));
                }

                if (collectingNested && braceDepth == 1 && line == "}")
                {
                    // nested class block ended
                    collectingNested = false;
                }
            }

            var nested = nestedName is not null
                ? new ParsedClass(nestedName, nestedProps)
                : null;

            return new ParsedClass(rootName ?? "Unknown", rootProps, nested);
        }

        private static string CleanupType(string t)
        {
            // this are normalize spaces inside generic List<T>
            var s = Regex.Replace(t, @"\s+", " ").Trim();
            s = s.Replace("List < ", "List<").Replace(" >", ">");
            return s;
        }
    }
}



