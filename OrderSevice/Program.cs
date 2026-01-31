using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using OrderSevice.Data;
using OrderSevice.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order API",
        Version = "v1"
    });

    // 🔑 REQUIRED for YARP
    c.AddServer(new OpenApiServer
    {
        Url = "/"
    });
});*/

// Optional Swagger only for Development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
    });

    #region for nginx 

    /*  app.UseSwagger(c =>
      {
          c.RouteTemplate = "swagger/{documentName}/swagger.json";
      });

      app.UseSwaggerUI(c =>
      {
          c.SwaggerEndpoint("/order/swagger/v1/swagger.json", "Order API");
          c.RoutePrefix = "order/swagger";
      });*/

    #endregion

}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.MapGet("/ping", () => "Order service alive");
app.Run();
