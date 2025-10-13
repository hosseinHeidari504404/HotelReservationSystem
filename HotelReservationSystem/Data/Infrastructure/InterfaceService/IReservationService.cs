using HotelReservationSystem.Models.Entities;

namespace HotelReservationSystem.Data.Infrastructure.InterfaceService
{
    public interface IReservationService
    {
        List<Reservation> GetAllReservations();
        List<Reservation> GetReservationsByUser(int userId);
        bool CreateReservation(int userId, int roomId, DateTime checkIn, DateTime checkOut);
        bool CancelReservation(int reservationId);
        bool ConfirmReservation(int reservationId);
    }
}
