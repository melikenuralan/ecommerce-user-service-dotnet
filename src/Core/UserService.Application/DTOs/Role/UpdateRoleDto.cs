namespace UserService.Application.DTOs.Role
{
    public class UpdateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
