using PlugApi.Data;
using PlugApi.Interfaces;
using PlugApi.Middleware;
using PlugApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CSPContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("PokemonApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://pokeapi.co");
});
builder.Services.AddHttpClient("JiraApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://jira.com/api/v2/");
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPokemonService, PokemonService>();

//builder.ConfigureWebHostDefaults(webBuilder =>
//{
//    webBuilder.UseStartup<ApplicationStartup>(); // Aqui, use a classe ApplicationStartup
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
