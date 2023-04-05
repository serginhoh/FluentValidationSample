using FluentValidation;
using FluentValidation.Results;
using FluentValidationSample.Models;
using FluentValidationSample.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Validators
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/person", async (IValidator<Person> validator, Person person) =>
{
    ValidationResult validationResult = await validator.ValidateAsync(person);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    return Results.Ok(person);
})
.WithName("Person")
.WithOpenApi();

app.Run();