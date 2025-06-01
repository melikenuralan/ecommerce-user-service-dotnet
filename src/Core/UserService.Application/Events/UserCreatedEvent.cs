using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Events
{
    public class UserCreatedEvent
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
