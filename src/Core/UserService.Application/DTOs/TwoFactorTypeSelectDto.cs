using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Enums;

namespace UserService.Application.DTOs
{
    public class TwoFactorTypeSelectDto
    {
        public TwoFactorType TwoFactorType { get; set; }
    }
}
