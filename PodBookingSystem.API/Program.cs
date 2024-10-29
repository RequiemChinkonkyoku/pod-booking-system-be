using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Models.DTOs;
using Repositories.Implement;
using Repositories.Interface;
using Services.Implement;
using Services.Interface;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepositoryBase<Area>, AreaRepository>();
builder.Services.AddScoped<IRepositoryBase<BookingDetail>, BookingDetailRepository>();
builder.Services.AddScoped<IRepositoryBase<Booking>, BookingRepository>();
builder.Services.AddScoped<IRepositoryBase<BookingStatus>, BookingStatusRepository>();
builder.Services.AddScoped<IRepositoryBase<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepositoryBase<Membership>, MembershipRepository>();
builder.Services.AddScoped<IRepositoryBase<Pod>, PodRepository>();
builder.Services.AddScoped<IRepositoryBase<PodType>, PodTypeRepository>();
builder.Services.AddScoped<IRepositoryBase<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepositoryBase<Role>, RoleRepository>();
builder.Services.AddScoped<IRepositoryBase<Schedule>, ScheduleRepository>();
builder.Services.AddScoped<IRepositoryBase<SelectedProduct>, SelectedProductRepository>();
builder.Services.AddScoped<IRepositoryBase<Product>, ProductRepository>();
builder.Services.AddScoped<IRepositoryBase<Slot>, SlotRepository>();
builder.Services.AddScoped<IRepositoryBase<Models.Transaction>, TransactionRepository>();
builder.Services.AddScoped<IRepositoryBase<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryBase<UserOtp>, UserOtpRepository>();
builder.Services.AddScoped<IRepositoryBase<Method>, MethodRepository>();
builder.Services.AddScoped<IRepositoryBase<StaffArea>, StaffAreaRepository>();

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IBookingDetailService, BookingDetailService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingStatusService, BookingStatusService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IPodService, PodService>();
builder.Services.AddScoped<IPodTypeService, PodTypeService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<ISelectedProductService, SelectedProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMethodService, MethodService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStaffAreaService, StaffAreaService>();

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();

builder.Services.AddScoped<IVnPayService, VnPayService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pod Booking System API", Version = "v1" });

    // Add JWT authentication support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "PBS_Policy",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerAuth",
        policy => policy.RequireClaim(ClaimTypes.Role, "1"));
    options.AddPolicy("StaffAuth",
        policy => policy.RequireClaim(ClaimTypes.Role, "2"));
    options.AddPolicy("ManagerAuth",
        policy => policy.RequireClaim(ClaimTypes.Role, "3"));
    options.AddPolicy("AdminAuth",
        policy => policy.RequireClaim(ClaimTypes.Role, "4"));
});

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PBS_Policy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
