using HotelReservationSystem.Models.Enums;
namespace HotelReservationSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}


