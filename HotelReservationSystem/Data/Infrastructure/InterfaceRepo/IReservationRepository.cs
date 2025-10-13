using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceRepo
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        Reservation? GetById(int id);
        List<Reservation> GetByUserId(int userId);
        List<Reservation> GetByRoomId(int roomId);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);
        bool HasConflict(int roomId, DateTime checkIn, DateTime checkOut);
    }
}
