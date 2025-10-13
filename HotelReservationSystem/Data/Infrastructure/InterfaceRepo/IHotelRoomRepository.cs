using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceRepo
{
    public interface IHotelRoomRepository
    {
        List<HotelRoom> GetAll();
        HotelRoom? GetById(int id);
        HotelRoom? GetByRoomNumber(string roomNumber);
        void Add(HotelRoom room);
        void Update(HotelRoom room);
        void Delete(int id);
    }
}
