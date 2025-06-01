using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Events
{
    public class PasswordResetEvent
    {
        public string Email { get; }
        public string ResetToken { get; }

        public PasswordResetEvent(string email, string resetToken)
        {
            Email = email;
            ResetToken = resetToken;
        }
    }
}
