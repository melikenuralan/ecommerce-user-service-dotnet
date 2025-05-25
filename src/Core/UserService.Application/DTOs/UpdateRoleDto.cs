namespace UserService.Application.DTOs
{
    public class UpdateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
