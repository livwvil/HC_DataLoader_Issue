using HC_DataLoader_Issue;

var builder = WebApplication.CreateBuilder(args);
builder.Services
            .AddHttpContextAccessor()
            .AddGraphQLServer()
            .InitializeOnStartup()
            .AddQueryType()
            .AddTypeExtension<TestQueries>()
            .AddMutationType()
            .AddTypeExtension<TestMutations>()
            .AddGlobalObjectIdentification();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
    .AllowAnyMethod()
    .AllowAnyHeader();

    if (app.Environment.IsDevelopment())
    {
        corsPolicyBuilder
        .AllowAnyOrigin();
    }
    else
    {
        corsPolicyBuilder
        .WithOrigins(
            "https://127.0.0.1",
            "https://localhost",
            "https://localhost:8000",

            "http://127.0.0.1",
            "http://localhost",
            "http://localhost:8000")
        .AllowCredentials();
    }
});

app.MapGraphQL();
app.Run();
