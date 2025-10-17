using HotelReservationSystem.Data.Infrastructure.InterfaceService;
using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Repositores;
using HotelReservationSystem.Models.Enums;

namespace HotelReservationSystem.Services
{

    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public User? Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) return null;

            return user.Password == password ? user : null;
        }

        public bool Register(string username, string password, UserRole role)
        {
            var existing = _userRepository.GetByUsername(username);
            if (existing != null)
            {
                Console.WriteLine("Error: Username already exists!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Error: Username or password cannot be empty!");
                return false;
            }

            var user = new User
            {
                Username = username,
                Password = password,
                Role = role,
                CreatedAt = DateTime.Now
            };

            _userRepository.Add(user);
            Console.WriteLine("Error: User registered successfully!");
            return true;
        }
    }
}
