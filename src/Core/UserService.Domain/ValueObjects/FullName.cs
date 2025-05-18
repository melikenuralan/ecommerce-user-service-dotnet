using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string Value { get; }

        private FullName() { } // EF için

        public FullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("İsim boş olamaz.");
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }

}
