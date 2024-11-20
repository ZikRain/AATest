using GRPC.DB;
using GRPC.DB.Repositories;
using GRPC.DB.RepositoryInterfaces;
using GRPC.Services;
using Microsoft.AspNetCore.Grpc.Swagger;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

//Database connections
builder.Services.AddDbContexts(builder.Configuration);

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
});


builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();

var app = builder.Build();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

// Configure the HTTP request pipeline.
app.MapGrpcService<WorkerService>();
app.UseHttpsRedirection();
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.MapGet("/", () => "/swagger/index.html");

app.Run();
