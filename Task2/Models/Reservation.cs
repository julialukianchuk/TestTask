using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Task2.Models;

public record Reservation(
    [property: JsonPropertyName("guest_name")] string GuestName,
    [property: JsonPropertyName("check_in_date")] DateTime CheckInDate,
    [property: JsonPropertyName("check_out_date")] DateTime CheckOutDate,
    [property: JsonPropertyName("special_requests")] string? SpecialRequests,
    [property: JsonPropertyName("reservation_id")] Guid? ReservationId
);