using HotelReservationSystem.Data.Infrastructure.InterfaceService;
using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Repositores;

namespace HotelReservationSystem.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly HotelRoomRepository _roomRepository;
        private readonly RoomDetailRepository _roomDetailRepository;

        public HotelRoomService()
        {
            _roomRepository = new HotelRoomRepository();
            _roomDetailRepository = new RoomDetailRepository();
        }

        public List<HotelRoom> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }

        public HotelRoom? GetRoomById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public HotelRoom? GetRoomByNumber(string roomNumber)
        {
            return _roomRepository.GetByRoomNumber(roomNumber);
        }

        public bool AddRoom(string roomNumber, int capacity, int pricePerNight, string description, bool hasWifi, bool hasAC)
        {
            var existing = _roomRepository.GetByRoomNumber(roomNumber);
            if (existing != null)
            {
                Console.WriteLine("Error: A room with this number already exists!");
                return false;
            }

            if (capacity <= 0 || pricePerNight <= 0)
            {
                Console.WriteLine("Error: Invalid capacity or price!");
                return false;
            }

            var newRoom = new HotelRoom
            {
                RoomNumber = roomNumber,
                Capacity = capacity,
                PricePerNight = pricePerNight,
                CreatedAt = DateTime.Now
            };

            _roomRepository.Add(newRoom);

            var detail = new RoomDetail
            {
                RoomId = newRoom.Id,
                Description = description,
                HasWifi = hasWifi,
                HasAirConditioner = hasAC
            };

            _roomDetailRepository.Add(detail);

            Console.WriteLine($"Nice: Room {roomNumber} added successfully!");
            return true;
        }
    }
}
