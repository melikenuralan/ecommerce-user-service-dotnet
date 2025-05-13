using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class BlockedUser
    {
        public Guid UserId { get; set; }         // engelleyen
        public Guid BlockedUserId { get; set; }  // engellenen
        public DateTime BlockedAt { get; set; }
    }
}
