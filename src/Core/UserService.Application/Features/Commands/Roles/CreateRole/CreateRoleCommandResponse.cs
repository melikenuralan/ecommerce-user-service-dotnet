namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandResponse
    {
        public Guid? RoleId { get; set; }
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
