using System.Text.RegularExpressions;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{

    public sealed class Email : ValueObject
    {
        public string Value { get; private set; }

        private Email() { }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!IsValid(value))
                throw new ArgumentException("Invalid email format.");

            Value = value.ToLower(); // normalize etmek için
        }
        private bool IsValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Email email) => email.Value;
    }
}
