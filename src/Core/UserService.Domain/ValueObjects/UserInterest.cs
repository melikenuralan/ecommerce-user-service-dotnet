using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public sealed class UserInterest : ValueObject
    {
        public string Category { get; private set; }
        private UserInterest() { }

        public UserInterest(string category)
        {
            Category = category;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Category.ToLower();
        }
    }
}
