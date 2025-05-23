using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    public class UpdateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
