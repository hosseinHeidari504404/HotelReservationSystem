namespace HotelReservationSystem.Models.Entities
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public int PricePerNight { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public RoomDetail? RoomDetail { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

