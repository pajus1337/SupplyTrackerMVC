using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using SupplyTrackerMVC.Application.DI;
using SupplyTrackerMVC.Infrastructure;
using SupplyTrackerMVC.Infrastructure.ExternalServices.Email;
using SupplyTrackerMVC.Infrastructure.Interceptors;
using SupplyTrackerMVC.Web.Adapters;
using SupplyTrackerMVC.Web.Middleware;

// QuestPDF License settings.
QuestPDF.Settings.License = LicenseType.Community;
// End

var builder = WebApplication.CreateBuilder(args);

// Sendmail Configuration + Validation 
builder.Services.AddOptions<SendmailOptions>()
    .Bind(builder.Configuration.GetSection("Sendmail"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Adapter (Identity.IEmailSender -> delegate to Application.IEmailSende)
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IdentityEmailSenderAdapter>();
// End

var connectionString = Environment.GetEnvironmentVariable("SUPPLYTRACKER_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string var not found in Environment");

builder.Services.AddDbContext<Context>(
    (sp, options) => options
    .UseSqlServer(connectionString)
    .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
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

    options.SignIn.RequireConfirmedEmail = true;
});
// End 

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

     WebApplication app;
// End
try
{
     app = builder.Build();
}
catch (Exception ex)
{
    Console.WriteLine(ex);      
    throw;
}

// HACK: Remember 
app.UseExceptionHandler();

// ForwardedHeaders to set confirmation email as https link.
var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};

forwardedHeadersOptions.KnownNetworks.Clear();
forwardedHeadersOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeadersOptions);
// End

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
