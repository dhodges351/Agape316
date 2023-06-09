using Agape316.AspNetIdentity.Services;
using Agape316.Data;
using Agape316.Services;
using Agape316.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Serialization;
using SendGrid.Extensions.DependencyInjection;
using AppContext = Agape316.Helpers.AppContext;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification;
using NToastNotify;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));

builder.Services.AddSendGrid(options => {
    options.ApiKey = builder.Configuration.GetSection("SendGridSettings")
    .GetValue<string>("ApiKey");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000
});

// Add ToastNotification
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IApplicationUser, ApplicationUserService>();
builder.Services.AddScoped<IEmailSender, EmailSenderService>();
builder.Services.AddScoped<IUpload, UploadService>();
builder.Services.AddTransient<IEvent, EventService>();
builder.Services.AddScoped<IEventDish, EventDishService>();
builder.Services.AddScoped<IMealSchedule, MealScheduleService>();
builder.Services.AddScoped<IMealDelivery, MealDeliveryService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAgapeDocument, AgapeDocumentService>();

var app = builder.Build();

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
app.UseSession();
AppContext.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();
app.UseNotyf();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();
