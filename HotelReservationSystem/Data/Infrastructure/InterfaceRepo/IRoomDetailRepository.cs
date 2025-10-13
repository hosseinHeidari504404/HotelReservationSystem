using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceRepo
{
    public interface IRoomDetailRepository
    {
        List<RoomDetail> GetAll();
        RoomDetail? GetById(int roomId);
        void Add(RoomDetail detail);
        void Update(RoomDetail detail);
        void Delete(int roomId);
    }
}
