using SkyWheels.Notification.DomainLayer.Common;

namespace SkyWheels.Notification.DomainLayer.Entities
{
    public class ReservationNotification : BaseEntity
    {
        public string carModel { get; set; }
        public string carDeliveryLocation { get; set; }
        public DateTime reservationDate { get; set; }
    }
}
