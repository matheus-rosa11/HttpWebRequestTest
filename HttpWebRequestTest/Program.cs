using HttpWebRequestTest.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("Teste", async () =>
{
    var httpClient = new HttpClient();

    // Defina a URL da requisi��o
    var url = "https://api.chucknorris.io/jokes/random";

    // Envie uma requisi��o GET e obtenha a resposta
    var response = await httpClient.GetAsync(url);

    // Verifique o c�digo de status da resposta
    if (response.IsSuccessStatusCode)
    {
        // Leia o conte�do da resposta
        var json = await response.Content.ReadAsStringAsync();

        var chuckNorrisJoke = JsonSerializer.Deserialize<ChuckNorrisJoke>(json);

        return Results.Ok(chuckNorrisJoke);

    // ... processe o conte�do da resposta ...
    }
else
{
        return Results.BadRequest($"Erro na requisi��o: { response.StatusCode}");
}
})
.Produces<ChuckNorrisJoke>(StatusCodes.Status200OK)
.WithTags("HttpTest")
.WithName("TesteHttpRequest")
.WithOpenApi();

app.Run();
