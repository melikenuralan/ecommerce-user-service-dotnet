using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public sealed class LanguagePreference : ValueObject
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        private LanguagePreference() { }

        public LanguagePreference(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static readonly LanguagePreference TR = new("tr", "Turkish");
        public static readonly LanguagePreference EN = new("en", "English");
        public static readonly LanguagePreference ES = new("es", "Spanish");

        public static LanguagePreference From(string code)
        {
            return code.ToLowerInvariant() switch
            {
                "tr" => TR,
                "en" => EN,
                "es" => ES,
                _ => throw new ArgumentException($"Desteklenmeyen dil kodu: {code}")
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code.ToLowerInvariant();
        }
    }
}

