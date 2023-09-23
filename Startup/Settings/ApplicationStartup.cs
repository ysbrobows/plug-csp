using PlugApi.Interfaces.Repositories;
using PlugApi.Interfaces.Services;
using PlugApi.Repository;
using PlugApi.Services;
using System.Net.Http.Headers;
using System.Text;

namespace PlugApi.Startup.Settings;

internal static class ApplicationStartup
{
    public static void AddMyApplicationDependencies(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        // Repositories
        // Jobs
        // Services
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        service.AddScoped<ICustomerRepository, CustomerRepository>();
        service.AddScoped<IBoardService, BoardService>();

        service.AddHttpClient("JiraApi", client =>
        {
            var jiraSettings = configuration.GetSection("JiraSettings").Get<JiraSettings>();

            var credentials = Encoding.UTF8.GetBytes($"{jiraSettings.JiraLogin}:{jiraSettings.JiraApiKey}");
            var base64Credentials = Convert.ToBase64String(credentials);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            client.BaseAddress = jiraSettings.JiraBaseUrl;
        });
    }
}
