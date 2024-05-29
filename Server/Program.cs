using System.Reflection;
using System.Text.Json.Serialization;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Harmonify.Server.Data;
using Harmonify.Server.Hubs;
using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri("https://harmonifykeyvault.vault.azure.net/");
var conf = builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential()).Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApplicationUserRepository>();
builder.Services.AddScoped<AvatarImageRepository>();
builder.Services.AddScoped<FriendshipRepository>();
builder.Services.AddScoped<NotificationRepository>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostLikeRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CommentLikeRepository>();
builder.Services.AddScoped<PostImageRepository>();
builder.Services.AddScoped<MessageRepository>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


builder.Services
    .AddSignalR()
    .AddHubOptions<ChatHub>(options =>
    {
        options.EnableDetailedErrors = true;
    });

builder.Services
    .AddAuthentication()
    .AddGoogle(opts =>
    {
        opts.ClientId = conf["GoogleClientId"]!;
        opts.ClientSecret = conf["GoogleSecret"]!;
        opts.SignInScheme = IdentityConstants.ExternalScheme;
    });



builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.ReferenceHandler = 
            ReferenceHandler.IgnoreCycles
    );

builder.Services.AddRazorPages();

builder.Services.AddLogging(builder => builder
    .AddConsole()
    .SetMinimumLevel(LogLevel.None)
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1"); });

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapHub<ChatHub>("/hubs/chat");

app.UseCors("CorsPolicy");
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();