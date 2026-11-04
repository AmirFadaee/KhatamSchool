using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.Interfaces.Services;
using KhatamSchool_Application.Services;
using KhatamSchool_Data.Context;
using KhatamSchool_Data.Repository;
using KhatamSchool_Data.Repository.Services;
using KhatamSchool_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IO.Compression;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<KhatamSchool_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<KhatamSchool_DbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<KhatamSchool_DbContext>()
    .AddDefaultTokenProviders();

//UserManager<ApplicationUser>

//SignInManager<ApplicationUser>

//builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
//    .AddEntityFrameworkStores<KhatamSchool_DbContext>()
//    .AddDefaultTokenProviders();



builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(AddNewsCommandHandler).Assembly));

//builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AddNewsCommandHandler).Assembly));


builder.Services.AddTransient<IAchievementItemRepository, AchievementItemRepository>();
builder.Services.AddTransient<IAchievementCategoryRepository, AchievementCategoryRepository>();
builder.Services.AddTransient<IExecutiveStaffRepository, ExecutiveStaffRepository>();
builder.Services.AddTransient<ICounselingTicketRepository, CounselingTicketRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<IGalleryItemRepository, GalleryItemRepository>();
builder.Services.AddTransient<IGalleryAlbumRepository, GalleryAlbumRepository>();
builder.Services.AddTransient<IGradeRepository, GradeRepository>();
builder.Services.AddTransient<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomizedMediator, CustomizedMediator>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));


builder.Services.AddAutoMapper(cfg =>
{
}, AppDomain.CurrentDomain.GetAssemblies());


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.SlidingExpiration = true;
});

//builder.Services.AddAuthorization(x => x.AddPolicy("", p => p.RequireAssertion(c => c.User.Claims.Any())));

builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;

    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int duration = 60 * 60 * 24 * 365;

        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            $"public,max-age={duration}";
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
