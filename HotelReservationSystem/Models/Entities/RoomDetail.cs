using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Models.Entities
{
    public class RoomDetail
    {
        public int RoomId { get; set; }
        public string? Description { get; set; }
        public bool HasWifi { get; set; }
        public bool HasAirConditioner { get; set; }
        public HotelRoom Room { get; set; } = null!;
    }
}

