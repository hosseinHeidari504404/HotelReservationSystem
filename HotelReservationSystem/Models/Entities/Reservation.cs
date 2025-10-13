using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Models.Enums;


namespace HotelReservationSystem.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; } = null!;
        public HotelRoom Room { get; set; } = null!;
    }
}

