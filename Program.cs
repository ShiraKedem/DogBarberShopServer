using DogBarberShopAPI;
using DogBarberShopBL.Interfaces;
using DogBarberShopBL.Services;
using DogBarberShopDB;
using DogBarberShopDB.EF.Contexts;
using DogBarberShopDB.Interfaces;
using DogBarberShopDB.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserEntites;

var builder = WebApplication.CreateBuilder(args);

// Configure AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration);

// Configure Serilog (הנחתי שאתה משתמש בזה)
builder.UseSerilog();

AppSettings appSettings = builder.Configuration.Get<AppSettings>();

// Add scoped services
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDB, DogBarberShopDB.Services.UserDB>();
builder.Services.AddScoped<IQueueBL, QueueBL>();
builder.Services.AddScoped<IUsersBL, UsersBL>();
builder.Services.AddScoped<IUsersDB, UsersDB>();
builder.Services.AddScoped<IQueueDB, QueueDB>();
builder.Services.AddScoped<IHomeBL, HomeBL>();
builder.Services.AddAutoMapper(typeof(MapperManager));

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.Jwt.Issuer,
            ValidAudience = appSettings.Jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["AccessToken"]; // שים לב לשם העוגייה
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<DogBarberShopContext>(options =>
{
    options.UseSqlServer(appSettings.ConnectionStrings.DogBarberShop);
});

builder.Services.AddSingleton(appSettings);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        );
});

// Add controllers and other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
