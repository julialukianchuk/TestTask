namespace test_task.Task1;

public class BookingDateService
{
    private const int MAX_BOOKING_DAYS = 330;
    private const int MIN_DAYS_BEFORE_CHECKIN = 0;

    /// <summary>
    /// Gets range of dates available to booking
    /// </summary>
    /// <param name="timeZoneId">Timezone parameter (optional)</param>
    /// <returns>Tuple with min and max booking date</returns>
    public (DateTime MinDate, DateTime MaxDate) GetBookingDateRange(string? timeZoneId = null)
    {
        var currentDate = GetCurrentDate(timeZoneId);
        
        var minDate = currentDate.AddDays(MIN_DAYS_BEFORE_CHECKIN);
        minDate = TrimTime(minDate); 
        
        var maxDate = currentDate.AddDays(MAX_BOOKING_DAYS);
        maxDate = TrimTime(maxDate); 
        
        return (minDate, maxDate);
    }

    /// <summary>
    /// Validates if date is within valid booking range 
    /// </summary>
    public bool IsDateValidForBooking(DateTime proposedDate)
    {
        var (minDate, maxDate) = GetBookingDateRange();
        
        return proposedDate >= minDate && proposedDate <= maxDate;
    }

    /// <summary>
    /// Get current date considering specific timezone
    /// </summary>
    private DateTime GetCurrentDate(string? timeZoneId)
    {
        // If no timezone defined use UTC
        if (string.IsNullOrWhiteSpace(timeZoneId))
            return DateTime.UtcNow.Date;

        try 
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).Date;
        }
        catch
        {
            // In case of exception, return UTC timezone
            return DateTime.UtcNow.Date;
        }
    }

    /// <summary>
    /// Removes time from date
    /// </summary>
    private DateTime TrimTime(DateTime date)
    {
        return date.Date; 
    }

    /// <summary>
    /// Gets maximum checkout date based on booking date
    /// </summary>
    public DateTime GetMaxCheckoutDate()
    {
        var (_, maxBookingDate) = GetBookingDateRange();
        return maxBookingDate;
    }
}