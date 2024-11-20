using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Task2;
using Task2.Models;
using Task2.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.IncludeFields = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var reservations = new List<Reservation>
{
    new(
        "John Smith", 
        DateTime.Now.AddDays(10), 
        DateTime.Now.AddDays(15),
        "Late check-in, non-smoking room",
        Guid.NewGuid()
    ),
    new(
        "Emily Johnson", 
        DateTime.Now.AddDays(30), 
        DateTime.Now.AddDays(35),
        "Room with sea view",
        Guid.NewGuid()
    ),
    // ... other reservations as before
};

// Reservation endpoints
app.MapPost("/reservations", ([FromBody] Reservation reservation) =>
    {
        try
        {
            reservation.Validate();
            reservations.Add(reservation);
            return Results.CreatedAtRoute("GetReservation", new { id = reservation.ReservationId }, reservation);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    })
    .Produces<Reservation>(201)
    .Produces(400)
    .WithName("CreateReservation")
    .WithTags("Reservations");

app.MapGet("/reservations/{id}", (Guid id) =>
    {
        var reservation = reservations.FirstOrDefault(r => r.ReservationId == id);
        return reservation is null ? Results.NotFound() : Results.Ok(reservation);
    })
    .Produces<Reservation>()
    .Produces(404)
    .WithName("GetReservation")
    .WithTags("Reservations");

app.MapGet("/reservations", () => reservations)
    .Produces<List<Reservation>>()
    .WithTags("Reservations");

app.MapDelete("/reservations/{id}", (Guid id) =>
    {
        var reservation = reservations.FirstOrDefault(r => r.ReservationId == id);
        if (reservation is null)
            return Results.NotFound();

        reservations.Remove(reservation);
        return Results.NoContent();
    })
    .Produces(204)
    .Produces(404)
    .WithTags("Reservations");

app.Run();
