using SkyWheels.Notification.DomainLayer.Common;

namespace SkyWheels.Notification.DomainLayer.Entities
{
    public class VerificationNotification : BaseEntity
    {
        public int verificationCode { get; set; }
        public string verificationType { get; set; }
    }
}
