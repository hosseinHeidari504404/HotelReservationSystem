using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Models.Enums;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User? GetById(int id);
        User? Login(string username, string password);
        bool Register(string username, string password, UserRole role);
    }
}
