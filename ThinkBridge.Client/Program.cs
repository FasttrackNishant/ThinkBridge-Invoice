using ThinkBridge.Client.Components;

namespace ThinkBridge.Client;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddServerSideBlazor();

        // Injected Http
        builder.Services.AddHttpClient();

        builder.Services.AddHttpClient("API", client =>
        {
            client.BaseAddress = new Uri("https://thinkbridge-api.azurewebsites.net/");
        });

        var app = builder.Build();
        
        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}