using HotelReservationSystem.Data.Infrastructure.InterfaceService;
using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Models.Enums;
using HotelReservationSystem.Repositores;

namespace HotelReservationSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly HotelRoomRepository _roomRepository;
        private readonly UserRepository _userRepository;

        public ReservationService()
        {
            _reservationRepository = new ReservationRepository();
            _roomRepository = new HotelRoomRepository();
            _userRepository = new UserRepository();
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAll();
        }

        public List<Reservation> GetReservationsByUser(int userId)
        {
            return _reservationRepository.GetByUserId(userId);
        }

        public bool CreateReservation(int userId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            var user = _userRepository.GetById(userId);
            var room = _roomRepository.GetById(roomId);

            if (user == null)
            {
                Console.WriteLine("Error: Invalid user!");
                return false;
            }

            if (room == null)
            {
                Console.WriteLine("Error: Invalid room!");
                return false;
            }

            if (checkIn >= checkOut)
            {
                Console.WriteLine("Error: Check-out date must be after check-in date!");
                return false;
            }

            bool tad = _reservationRepository.Tadakhol(roomId,checkIn, checkOut);
            if (tad)
            {
                Console.WriteLine("Error: This room is already booked for the selected dates!");
                return false;
            }

            var reservation = new Reservation
            {
                UserId = userId,
                RoomId = roomId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _reservationRepository.Add(reservation);
            Console.WriteLine($"Nice: Reservation created successfully (Pending) for Room {room.RoomNumber}");
            return true;
        }

        public bool CancelReservation(int reservationId)
        {
            var reservation = _reservationRepository.GetById(reservationId);
            if (reservation == null)
            {
                Console.WriteLine("Error: Reservation not found!");
                return false;
            }

            if (reservation.Status == ReservationStatus.Canceled)
            {
                Console.WriteLine("Error: Reservation already canceled!");
                return false;
            }

            reservation.Status = ReservationStatus.Canceled;
            _reservationRepository.Update(reservation);

            Console.WriteLine($"Nice: Reservation {reservation.Id} canceled successfully.");
            return true;
        }

        public bool ConfirmReservation(int reservationId)
        {
            var reservation = _reservationRepository.GetById(reservationId);
            if (reservation == null)
            {
                Console.WriteLine("Error: Reservation not found!");
                return false;
            }

            if (reservation.Status != ReservationStatus.Pending)
            {
                Console.WriteLine("Error: Only pending reservations can be confirmed!");
                return false;
            }

            reservation.Status = ReservationStatus.Confirmed;
            _reservationRepository.Update(reservation);

            Console.WriteLine($"Nice: Reservation {reservation.Id} confirmed successfully.");
            return true;
        }
    }
}
