using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceService
{
    public interface IHotelRoomService
    {
        List<HotelRoom> GetAllRooms();
        bool AddRoom(string roomNumber, int capacity, int pricePerNight, string description, bool hasWifi, bool hasAC);
    }
}
