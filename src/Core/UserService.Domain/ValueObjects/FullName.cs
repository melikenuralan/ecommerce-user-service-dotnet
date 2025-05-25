using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public sealed class FullName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private FullName() { } // EF için

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Ad ve soyad boş olamaz.");

            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }

}
