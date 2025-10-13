using HotelReservationSystem.Data;
using HotelReservationSystem.Data.Infrastructure.InterfaceRepo;
using HotelReservationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace HotelReservationSystem.Repositores
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly HotelContext _db;

        public HotelRoomRepository()
        {
            _db = new HotelContext();
        }

        public List<HotelRoom> GetAll()
        {
            return _db.HotelRooms.Include(r => r.RoomDetail).ToList();
        }

        public HotelRoom? GetById(int id)
        {
            return _db.HotelRooms
                .Include(r => r.RoomDetail)
                .FirstOrDefault(r => r.Id == id);
        }

        public HotelRoom? GetByRoomNumber(string roomNumber)
        {
            return _db.HotelRooms
                .Include(r => r.RoomDetail)
                .FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public void Add(HotelRoom room)
        {
            _db.HotelRooms.Add(room);
            _db.SaveChanges();
        }

        public void Update(HotelRoom room)
        {
            _db.HotelRooms.Update(room);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _db.HotelRooms.FirstOrDefault(r => r.Id == id);
            if (existing != null)
            {
                _db.HotelRooms.Remove(existing);
                _db.SaveChanges();
            }
        }
    }
}
