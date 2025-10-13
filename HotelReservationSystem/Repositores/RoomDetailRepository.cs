using HotelReservationSystem.Data;
using HotelReservationSystem.Data.Infrastructure.InterfaceRepo;
using HotelReservationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace HotelReservationSystem.Repositores
{
    public class RoomDetailRepository : IRoomDetailRepository
    {
        private readonly HotelContext _db;

        public RoomDetailRepository()
        {
            _db = new HotelContext();
        }

        public List<RoomDetail> GetAll()
        {
            return _db.RoomDetails
                .Include(d => d.Room)
                .ToList();
        }

        public RoomDetail? GetById(int roomId)
        {
            return _db.RoomDetails
                .Include(d => d.Room)
                .FirstOrDefault(d => d.RoomId == roomId);
        }

        public void Add(RoomDetail detail)
        {
            _db.RoomDetails.Add(detail);
            _db.SaveChanges();
        }

        public void Update(RoomDetail detail)
        {
            _db.RoomDetails.Update(detail);
            _db.SaveChanges();
        }

        public void Delete(int roomId)
        {
            var existing = _db.RoomDetails.FirstOrDefault(d => d.RoomId == roomId);
            if (existing != null)
            {
                _db.RoomDetails.Remove(existing);
                _db.SaveChanges();
            }
        }
    }
}
