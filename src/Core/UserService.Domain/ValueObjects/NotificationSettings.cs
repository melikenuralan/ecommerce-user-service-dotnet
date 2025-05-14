using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;

namespace UserService.Domain.ValueObjects
{
    public class NotificationSettings : ValueObject
    {
        public bool ReceivePromotions { get; private set; }
        public bool ReceiveOrderUpdates { get; private set; }
        public bool ReceiveSecurityAlerts { get; private set; }

        private NotificationSettings() { }

        public NotificationSettings(bool promotions, bool orderUpdates, bool securityAlerts)
        {
            ReceivePromotions = promotions;
            ReceiveOrderUpdates = orderUpdates;
            ReceiveSecurityAlerts = securityAlerts;
        }

        public static NotificationSettings Default()
        {
            return new NotificationSettings(
                promotions: true,
                orderUpdates: true,
                securityAlerts: true
            );
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ReceivePromotions;
            yield return ReceiveOrderUpdates;
            yield return ReceiveSecurityAlerts;
        }
    }

}
