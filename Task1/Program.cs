using test_task.Task1;

var bookingService = new BookingDateService();

// Getting booking date range
var (minDate, maxDate) = bookingService.GetBookingDateRange();
Console.WriteLine($"You can book this hotel from {minDate:d} to {maxDate:d}");

// Checking if date is valid
var testDate = DateTime.Now.AddDays(100);
var isValid = bookingService.IsDateValidForBooking(testDate);
Console.WriteLine($"Is {testDate:d} valid: {isValid}");

// Testing different timezones
var (minDateNy, maxDateNy) = bookingService.GetBookingDateRange("Eastern Standard Time");
Console.WriteLine($"New York date range: {minDateNy:d} - {maxDateNy:d}");