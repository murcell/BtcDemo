using AutoMapper;
using BtcDemo.API.BackgroundServices;
using BtcDemo.API.Extensions;
using BtcDemo.API.ProfileMap;
using BtcDemo.API.Service;
using BtcDemo.Core.Configurations;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Repositories;
using BtcDemo.Core.Services;
using BtcDemo.Core.UnitOfWorks;
using BtcDemo.Data.Context;
using BtcDemo.Data.Repositories;
using BtcDemo.Data.UnitOfWorks;
using BtcDemo.Service.AutoMapper.Profiles;
using BtcDemo.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mappingConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new RequestJsonModelProfile());
	mc.AddProfile(new CoinProfile());
	mc.AddProfile(new AppUserProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Scoped her request için bir kere oluþsun
// Singleton uygulama boyunca tek bir nesne örneði üzerinden iþlem yap.
// transient her interface için ayrý bir nesne oluþturtur.
// Baðýmlýlýklarý ekliyoruz
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<BtcDemo.Core.Services.IAuthenticationService, BtcDemo.Service.Services.AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICoinloreService, CoinloreService>();

//builder.Services.AddScoped<ICoinloreNetApiService, CoinloreNetApiService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
	{
		sqlOptions.MigrationsAssembly("BtcDemo.Data");
	});
});

//builder.Services.AddHttpClient<CoinloreNetApiService>(opt =>
//{
//	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
//});

builder.Services.AddHttpClient<ICoinloreService, CoinloreService>(opt =>
{
	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
});
//builder.Services.AddHttpClient();
builder.Services.AddHostedService<CoinloreBackgroundService>();


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
	options.User.AllowedUserNameCharacters = "@abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._çÇöÖýIüÜðÐ";
	options.Password.RequiredLength = 4;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireDigit = false;
	options.User.RequireUniqueEmail = true;
	options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// Bir token geldiðnde doðrulama iþlemi
// aþaðýda yaplan ayarlara gerçekleþtirilecek.
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
	var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
	opt.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidIssuer = tokenOptions.Issuer,
		ValidAudience = tokenOptions.Audience[0],
		IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

		ValidateIssuerSigningKey = true,
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero // serverlar arasý saat farký eþit olacak.  ya da tek server varsa

	};
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//	// Help dökümaný açýklamasýný görmek için 
//	c.IncludeXmlComments(System.AppDomain.CurrentDomain.BaseDirectory + "\\BtcDemo.API.xml");

//	c.SwaggerDoc("v1", new OpenApiInfo { Title = "BtcDemo.API", Version = "v1" });
//	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//	{
//		In = ParameterLocation.Header,
//		Description = "login metodundan dönen tokeni giriniz.",
//		Name = "Authorization",
//		Type = SecuritySchemeType.Http,
//		BearerFormat = "JWT",
//		Scheme = "Bearer"
//	});

//	c.AddSecurityRequirement(new OpenApiSecurityRequirement
//				{
//					{
//						new OpenApiSecurityScheme
//						{
//							Reference = new OpenApiReference
//							{
//								Type=ReferenceType.SecurityScheme,
//								Id="Bearer"
//							}
//						},
//						new string[]{}
//					}
//				});
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCustomException();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseForbiddenMiddleware();
app.MapControllers();

app.Run();
