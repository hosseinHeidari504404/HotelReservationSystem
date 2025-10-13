using HotelReservationSystem.Data;
using HotelReservationSystem.Data.Infrastructure.InterfaceRepo;
using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelContext _db;

        public UserRepository()
        {
            _db = new HotelContext();
        }

        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _db.Users.FirstOrDefault(u => u.Id == id);
            if (existing != null)
            {
                _db.Users.Remove(existing);
                _db.SaveChanges();
            }
        }
    }
}