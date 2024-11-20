using Task2.Models;

namespace Task2.Utils;

public static class ReservationUtils
{
    public static void Validate(this Reservation reservation)
    {
        if (string.IsNullOrWhiteSpace(reservation.GuestName))
            throw new ArgumentException("Guest name cannot be empty", nameof(reservation.GuestName));
        if (reservation.CheckInDate >= reservation.CheckOutDate)
            throw new ArgumentException("Check-in date must be before check-out date", nameof(reservation.CheckInDate));
        if (reservation.CheckInDate < DateTime.Today)
            throw new ArgumentException("Check-in date cannot be in the past", nameof(reservation.CheckInDate));
    }
}