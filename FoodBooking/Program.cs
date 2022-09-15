using AutoMapper;
using FoodBooking.Data;
using FoodBooking.Data.Models.Middleware;
using FoodBooking.Mapper;
using FoodBooking.Reponsitory.Restaurants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod(); ;
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config DB
builder.Services.AddDbContext<FoodBookingContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoodBooking")));

//Config AutoMapper
builder.Services.AddScoped<IMapper>(sp =>
{
    return new Mapper(AutoMapperConfig.RegisterMappings());
});
//builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings());

//Config Mediator
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//Config DI Repo
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddControllers().AddNewtonsoftJson();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthorization();

app.MapControllers();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseMiddleware<ExceptionMiddleware>();
//}
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
