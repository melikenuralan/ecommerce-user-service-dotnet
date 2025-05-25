namespace UserService.Application.DTOs
{
    public class RoleDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public IList<string>? Permissions { get; set; }
    }
}
