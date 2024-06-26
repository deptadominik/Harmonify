using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Harmonify.Client;
using Harmonify.Client.Helpers;
using Harmonify.Client.Services.ApplicationUser;
using Harmonify.Client.Services.AvatarImage;
using Harmonify.Client.Services.CommentLike;
using Harmonify.Client.Services.CommentService;
using Harmonify.Client.Services.Friendship;
using Harmonify.Client.Services.Message;
using Harmonify.Client.Services.Notification;
using Harmonify.Client.Services.Post;
using Harmonify.Client.Services.PostImage;
using Harmonify.Client.Services.PostLike;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Harmonify.ServerAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Harmonify.ServerAPI"));

builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IAvatarImageService, AvatarImageService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostLikeService, PostLikeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentLikeService, CommentLikeService>();
builder.Services.AddScoped<IPostImageService, PostImageService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<AzureStorageHelper>();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();