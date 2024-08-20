using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using practico_02_a.Controllers;
using Serilog;using Task = practico_02_a.Models.Task;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TaskController>();
// Esta instruccion se agrega para evitar algunos errores a la hora de serializar objetos
// Al final del archico se encuentra la implementacion de JsonContext
builder.Services.AddMvc().AddControllersAsServices().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, JsonContext.Default);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{
    loggerConf
        .WriteTo.Console()
        .WriteTo.Debug()
        .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();

// Clase que sirve para decirle al programa que objetos se deben serializar a JSON 
[JsonSerializable((typeof(Task)))]

// Segun lo que me he encontrado a partir de .NET 8 es necesario tambien definir la lista de objeto para serializar
// por eso agregamos tambien esta linea
[JsonSerializable(typeof(List<Task>))]

// ProblemDetails tambien lo marcamos como serializable para poder devolver errores en las respuestas 
// Particularmente me encontre nuevamente con un problema de serializacion cuando quise devolver un BadRequest
// ya que dicha respuesta no es simplemente un 400 vacio sino que lleva dentro un objeto con un poco mas de info 
// sobre el error y dicho objeto debe serializarse
// Aqui un ejemplo del objeto en cuestion: 
// {
//     "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
//     "title": "Bad Request",
//     "status": 400,
//     "traceId": "00-7bc345f5d18f19febc725350120f9c1a-86dfa31e8afafc0d-00"
// }
[JsonSerializable(typeof(ProblemDetails))]
internal partial class JsonContext : JsonSerializerContext
{
}
