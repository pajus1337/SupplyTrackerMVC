using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using SupplyTrackerMVC.Application.DI;
using SupplyTrackerMVC.Infrastructure;
using SupplyTrackerMVC.Infrastructure.Interceptors;
using SupplyTrackerMVC.Web.Middleware;

// QuestPDF License settings.
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString = Environment.GetEnvironmentVariable("SUPPLYTRACKER_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string var not found in Environment");

builder.Services.AddDbContext<Context>(
    (sp, options) => options
    .UseSqlServer(connectionString)
    .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Context>();

builder.Services.AddControllersWithViews();

// Identity Custom Configuration 
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 6;

    options.SignIn.RequireConfirmedEmail = false; // TODO: Remember to change after setup the MailSend service.
});

// DI Configuration
//- Services
builder.Services.AddApplication();
//- Repositories
builder.Services.AddInfrastructure();
//- Razor Pages 
builder.Services.AddHttpContextAccessor();
// * End of DI Configuration


//- Exception  Handler 
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// HACK: Remember 
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
