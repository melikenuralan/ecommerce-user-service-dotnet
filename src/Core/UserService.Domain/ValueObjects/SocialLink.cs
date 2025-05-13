using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public class SocialLink : ValueObject
    {
        public string Platform { get; private set; } // Örn: "Instagram"
        public string Url { get; private set; }
        private SocialLink() { }
        public SocialLink(string platform, string url)
        {
            Platform = platform;
            Url = url;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Platform.ToLower();
            yield return Url.ToLower();
        }
    }
}
