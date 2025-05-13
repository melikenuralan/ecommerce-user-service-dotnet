using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public class Language : ValueObject
    {
        public string Code { get; private set; }  // Örn: "tr", "en"
        public string Name { get; private set; }  // Örn: "Turkish"

        private Language() { }

        public Language(string code, string name)
        {
            Code = code;
            Name = name;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code.ToLower();
            yield return Name.ToLower();
        }
    }
}
