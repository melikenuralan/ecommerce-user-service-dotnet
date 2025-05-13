using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public class ThemePreference : ValueObject
    {

        public static readonly ThemePreference Light = new("light");
        public static readonly ThemePreference Dark = new("dark");
        public static readonly ThemePreference System = new("system");

        public string Value { get; }

        private ThemePreference(string value)
        {
            Value = value;
        }

        public static ThemePreference From(string value)
        {
            return value.ToLowerInvariant() switch
            {
                "light" => Light,
                "dark" => Dark,
                "system" => System,
                _ => throw new ArgumentException("Geçersiz tema", nameof(value))
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
