using Microsoft.EntityFrameworkCore;
using TalkBack.ContactsControl.Data;
using TalkBack.ContactsControl.Data.Hubs;
using TalkBack.ContactsControl.Data.Repositories;
using TalkBack.ContactsControl.Data.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddDbContext<TalkBackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TalkBackConnectionString")));

builder.Services.AddScoped<IContactRepository, ContactsRepository>();
builder.Services.AddScoped<IWebAPIService, WebAPIService>();
builder.Services.AddScoped<ISignalrService, SignalrService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<UserHub>("/userHub");

app.Run();
