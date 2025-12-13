//--------------------------- Variables ---------------------------//

using JustTip.API.Middleware.Exceptions;
using JustTip.API.Setup;
using JustTip.Application;
using JustTip.Infrastructure;
using Scalar.AspNetCore;

var _builder = WebApplication.CreateBuilder(args);
var _services = _builder.Services;
var _env = _builder.Environment;
var _configuration = _builder.Configuration;
var _logging = _builder.Logging;
var _startupData = new JtStartupData(_configuration);

//################################################################//


_services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
_services.AddOpenApi();


_builder
    .InstallArchitecture(_startupData)
    .InstallLogging(_startupData);

_services.AddEndpointsApiExplorer();
_services.AddSwaggerGen();

_services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(_startupData.GetAllowedOrigins())
        )
    );


//------------------------- Configure AppBuilder -------------------------//

// Add services to the container.

_builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
_builder.Services.AddOpenApi();

var app = _builder.Build();


app.UseCustomExceptionHandler(new JtExceptionConverter());


app.UseRouting();
app.UseCors();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
    app.MapScalarApiReference(_startupData.ScalarSection.GetEndPointPrefix(), opts =>
    {
        opts
        .WithTitle(_startupData.ScalarSection.GetTitle())
        .WithTheme(ScalarTheme.Solarized)
        .WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.HttpClient);
    });
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



await app.UseJtApplicationAsync();
app.UseJtInfrastructure("jt-job-dashboard");

app.Run();
