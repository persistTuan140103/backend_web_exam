using Application;
using Infracstructure;
using Infracstructure.Persistence;
using Infrastructure.DatabaseSeeder;
using Infrastructure.Identities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    //options.LowercaseQueryStrings = true;
});


builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddDataProtection();


// Add layers services to the container
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("TeacherOnly", policy => policy.RequireRole("Teacher"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
//    await RoleSeeder.SeedRolesAsync(roleManager);
//}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
