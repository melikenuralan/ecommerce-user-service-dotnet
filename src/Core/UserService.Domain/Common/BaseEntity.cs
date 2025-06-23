namespace UserService.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private List<object> _domainEvents;
        public IReadOnlyCollection<object> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents ??= new List<object>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
