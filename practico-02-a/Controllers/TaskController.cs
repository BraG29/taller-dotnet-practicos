using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TaskDO = practico_02_a.Models.Task;

namespace practico_02_a.Controllers;

[ApiController]
// Controlador para todas las request que se hagan a /api/task
[Route("api/task")]
public class TaskController : Controller
{

    private readonly ILogger<TaskController> _logger;

    private IList<TaskDO> _tasks;

    public TaskController(ILogger<TaskController> logger)
    {
        _logger = logger;
        this._tasks = new List<TaskDO>
        {
            new TaskDO(1L, "Tarea 1", "Descripcion 1", 2, "Pepe"),
            new TaskDO(2L, "Tarea 2", "Descripcion 2", 5, "Miguel"),
            new TaskDO(3L, "Tarea 3", "Descripcion 3", 3, "Sandra")
        };
    }

    [HttpGet]
    public ActionResult<IList<TaskDO>> GetAll()
    {
        _logger.LogInformation("Retorno lista de tareas");
        return Ok(_tasks);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<TaskDO> GetById(long id)
    {
        _logger.LogInformation($"Retorno tarea con id {id}");
        return Ok(_tasks.First(task => task.Id == id));
    }

    [HttpPost]
    public ActionResult Create([FromBody]TaskDO task)
    {
        _logger.LogInformation($"Creo una nueva tarea con valores:\n{task}");
        this._tasks.Add(task);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(long id)
    {
        
        _logger.LogInformation($"Eliminando tarea con id {id}");
        
        // En un CRUD mas complejo la operacion para recuperar tarea por ID estaria en un servicio
        // evitando la repeticion de codigo.
        TaskDO? task = _tasks.FirstOrDefault(task => task.Id == id);
        
        if (task == null) return BadRequest();
        
        _tasks.Remove(task);
        return Ok();

    }
}