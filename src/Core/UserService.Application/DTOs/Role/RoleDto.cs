namespace UserService.Application.DTOs.Role
{
    public class RoleDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public IList<string>? Permissions { get; set; }
    }
}
