using API.Entities;
using API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Map a POST endpoint to confirm participation in an excursion.
// This endpoint receives participation confirmation input and returns a PDF file.
app.MapPost("/excursion-participation-confirmation", (ParticipationConfirmationInputDto input) =>
{
    // Call the PDF generation helper to create a confirmation PDF and return it as a file result.
    return Results.File(GenerationPdf.GenerateParticipationConfirmationPdf(input), "application/pdf", "generated.pdf");
});

app.Run();
