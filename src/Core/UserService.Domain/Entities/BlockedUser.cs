using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.Entities
{
    public class BlockedUser : BaseEntity
    {
        public Guid UserId { get; private set; }         // engelleyen
        public Guid BlockedUserId { get; private set; }  // engellenen
        public DateTime BlockedAt { get; private set; }

        private BlockedUser() { }
        public BlockedUser(Guid userId, Guid blockedUserId)
        {
            UserId = userId;
            BlockedUserId = blockedUserId;
            BlockedAt = DateTime.UtcNow;
        }
    }
}
