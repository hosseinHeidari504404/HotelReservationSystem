using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceService
{
    public interface IHotelRoomService
    {
        List<HotelRoom> GetAllRooms();
       // HotelRoom? GetRoomById(int id);
       // HotelRoom? GetRoomByNumber(string roomNumber);
        bool AddRoom(string roomNumber, int capacity, int pricePerNight, string description, bool hasWifi, bool hasAC);
    }
}
