using Microsoft.EntityFrameworkCore;
using SmartMedCodeChallenge.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<MedicineContext>(context => context.UseInMemoryDatabase("MedicinesList"));
builder.Services.AddEndpointsApiExplorer();

//'Homepage', description of different api calls.
var app = builder.Build();
app.MapGet("/", () => @"List of paths for api calls: 
/api/v1/medicines/getall - returns a list of all created medicines (GET)
/api/v1/medicines/get/{id} - returns a specified medicine (GET)
/api/v1/medicines/add - takes a json object containing a name, dateCreated and quantity - creates the passed medicine object (POST)
/api/v1/medicines/delete/{id} - takes a medicine Id in the url, deletes the associated medicine (POST)

For testing purposes I used postman to create and delete the medicines.");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
