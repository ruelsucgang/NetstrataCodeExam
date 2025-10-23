using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpToScript.Services.Helpers
{
    internal static class SourceInput
    {
        /// <summary>
        /// The C# class definition string that will be parsed and converted to TypeScript.
        /// Update this string whenever the system needs a different class structure.
        /// </summary>
        public static string CSharpClassDefinition => @"
            public class PersonDto {
                public string Name { get; set; }
                public int Age { get; set; }
                public string Gender { get; set; }
                public long? DriverLicenceNumber { get; set; }
                public List<Address> Addresses { get; set; }
                public class Address
                {
                    public int StreetNumber { get; set; }
                    public string StreetName { get; set; }
                    public string Suburb { get; set; }
                    public int PostCode { get; set; }
                }
            }";  
    }
}


