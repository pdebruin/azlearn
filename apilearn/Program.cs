using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "apilearn";
    config.Title = "apilearn v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "apilearn";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapPost("/ask", async ([FromBody] UserQuery query, IHttpClientFactory httpClientFactory) =>
{
    var searchResults = await SearchDocuments(query.Question, httpClientFactory);
    var extractedInfo = await ExtractInformation(searchResults, httpClientFactory);
    var openAiResponse = await QueryOpenAi(query.Question, extractedInfo, httpClientFactory);
    return Results.Ok(openAiResponse);
});

app.Run();

async Task<string> SearchDocuments(string question, IHttpClientFactory httpClientFactory)
{
    // Implement Azure Search API call
    var client = httpClientFactory.CreateClient();
    var response = await client.GetAsync("Your Azure Search API Endpoint");
    return await response.Content.ReadAsStringAsync();
}

async Task<string> ExtractInformation(string searchResults, IHttpClientFactory httpClientFactory)
{
    // Implement Azure Document Intelligence API call
    var client = httpClientFactory.CreateClient();
    var response = await client.PostAsync("Your Document Intelligence API Endpoint", new StringContent(searchResults));
    return await response.Content.ReadAsStringAsync();
}

async Task<string> QueryOpenAi(string question, string extractedInfo, IHttpClientFactory httpClientFactory)
{
    // Implement Azure OpenAI API call
    var client = httpClientFactory.CreateClient();
    var payload = new
    {
        prompt = $"{question}\n\nContext: {extractedInfo}",
        max_tokens = 150
    };
    var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
    var response = await client.PostAsync("Your OpenAI API Endpoint", content);
    return await response.Content.ReadAsStringAsync();
}

public class UserQuery
{
    public string Question { get; set; }
}


// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
