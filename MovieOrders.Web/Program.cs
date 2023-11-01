using Microsoft.EntityFrameworkCore;
using MovieOrders.Web.Consumer;
using MovieOrders.Web.Data;
using MovieOrders.Web.Interface.DomainServices;
using MovieOrders.Web.Interface.Repository;
using MovieOrders.Web.Service;

var policyName = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//DBContext
builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});

//Build services

//Domain services
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));

//Background services
builder.Services.AddHostedService<CreateOrderConsumer>();

//Build repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();

app.Run();