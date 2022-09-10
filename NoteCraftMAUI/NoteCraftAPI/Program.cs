using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NoteCraftAPI.Mapper;
using NoteCraftAPI.Repository;
using NoteCraftAPI.Repository.Implementation;
using NoteCraftAPI.Service;
using NoteCraftAPI.Service.Implementation;
using NoteCraftAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//telling asp.net core to add all settings from section "NoteCraftDatabaseSettings" in appsettings.json and map them to NoteCraftDatabaseSettings class
builder.Services.Configure<NoteCraftDatabaseSettings>(builder.Configuration.GetSection(nameof(NoteCraftDatabaseSettings)));
//tying INoteCraftDatabaseSettings to NoteCraftDatabaseSettings class
builder.Services.AddSingleton<INotecraftDatabaseSettings>(sp => sp.GetRequiredService<IOptions<NoteCraftDatabaseSettings>>().Value);
//specifying to mongo client database connection string
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("NoteCraftDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
