using HotelReservationSystem.Data;
using HotelReservationSystem.Data.Infrastructure.InterfaceRepo;
using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
namespace HotelReservationSystem.Repositores
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelContext _db;

        public ReservationRepository()
        {
            _db = new HotelContext();
        }

        public List<Reservation> GetAll()
        {
            return _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Room)
                .ToList();
        }

        public Reservation? GetById(int id)
        {
            return _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Room)
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetByUserId(int userId)
        {
            return _db.Reservations
                .Include(r => r.Room)
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public List<Reservation> GetByRoomId(int roomId)
        {
            return _db.Reservations
                .Include(r => r.User)
                .Where(r => r.RoomId == roomId)
                .ToList();
        }

        public void Add(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
        }

        public void Update(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _db.Reservations.FirstOrDefault(r => r.Id == id);
            if (existing != null)
            {
                _db.Reservations.Remove(existing);
                _db.SaveChanges();
            }
        }

        public bool HasConflict(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return _db.Reservations
                .Any(r => r.RoomId == roomId
                    && r.Status != ReservationStatus.Canceled
                    && (
                        (checkIn >= r.CheckInDate && checkIn < r.CheckOutDate) ||
                        (checkOut > r.CheckInDate && checkOut <= r.CheckOutDate) ||
                        (checkIn <= r.CheckInDate && checkOut >= r.CheckOutDate)
                    ));
        }
    }
}
