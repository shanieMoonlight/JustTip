

//--------------------------- Variables ---------------------------//

using JustTip.API.Setup;

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


//------------------------- Configure AppBuilder -------------------------//

// Add services to the container.

_builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
_builder.Services.AddOpenApi();

var app = _builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
