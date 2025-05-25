using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public sealed class SocialLink : ValueObject
    {
        public string Platform { get; private set; }
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
