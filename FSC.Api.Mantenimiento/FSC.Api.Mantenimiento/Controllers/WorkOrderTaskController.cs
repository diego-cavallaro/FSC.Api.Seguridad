using FluentValidation;
using FSC.Api.Mantenimiento.Errors;
using FSC.Api.Mantenimiento.Modelos;
using FSC.Api.Mantenimiento.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace FSC.Api.Mantenimiento.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WorkOrderTaskController : ControllerBase
    {
        private readonly WorkOrderTasksService _workOrderTasksService;
        private readonly IValidator<WorkOrderTask> _validator;

        public WorkOrderTaskController(IValidator<WorkOrderTask> validator, WorkOrderTasksService workOrderTasksService)
        {
            _workOrderTasksService = workOrderTasksService;
            _validator = validator;
        }

        [HttpGet]
        [Route("GetTareasPendientes")]
        public async Task<ActionResult<List<WorkOrderTask>>> GetPendingWorkOrderTasks()
        {
            // Extrae el token directamente del contexto sin lidiar con strings
            string bearerToken = await HttpContext.GetTokenAsync("access_token");
            // Obtenemos el legajo de la variable User generado por el middleware de validacion del token
            var usuarioId = User.FindFirst("nickName")? .Value;

            if (bearerToken != null && !String.IsNullOrEmpty(usuarioId))
            {
                var workOrderTasksList = _workOrderTasksService.GetPendingTasksByUsuario(usuarioId);
            
                return Ok(workOrderTasksList);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpGet()]
        [Route("ObtenerPorId")]
        public async Task<ActionResult<WorkOrderTask>> GetByIds(long woId, int taskNo)
        {
            var task = await _workOrderTasksService.GetByIdsAsync(woId, taskNo);

            if (task == null)
            {
                return NotFound(
                    new ErrorStructure("No se encontró el registro.")
                    {
                        Message = "No Encontrado",
                        StatusCode = 404
                    }

                    ); // 404 Not Found
            }

            return Ok(task); // 200 OK
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<ActionResult<WorkOrderTask>> Create([FromBody] WorkOrderTask workOrderTask)
        {
            // Ejecutar validación
            var validationResult = await _validator.ValidateAsync(workOrderTask);
            if (!validationResult.IsValid)
            {
                // Mapear a tu estructura propia directamente
                var errorResponse = new ErrorStructure
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Errores de Integridad de Datos",
                    Details = validationResult.Errors.Select(e => new DetailError
                    {
                        Detail = e.ErrorMessage
                    }).ToList()
                };
                return BadRequest(errorResponse);
            }

            //var e = Convert.ToInt32("HOLA");
            var createdTask = await _workOrderTasksService.CreateAsync(workOrderTask);

            return await _workOrderTasksService.GetByIdsAsync(createdTask.WoId, createdTask.TaskNo);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Update([FromBody] WorkOrderTask updatedTask)
        {
            // Ejecutar validación
            var validationResult = await _validator.ValidateAsync(updatedTask);
            if (!validationResult.IsValid)
            {
                // Mapear a nuestra estructura de manejo de errores
                var errorResponse = new ErrorStructure
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Errores de Integridad de Datos",
                    Details = validationResult.Errors.Select(e => new DetailError
                    {
                        Detail = e.ErrorMessage
                    }).ToList()
                };
                return BadRequest(errorResponse);
            }

            //var e = Convert.ToInt32("HOLA");
            var result = await _workOrderTasksService.UpdateAsync(updatedTask);

            if (result == null)
            {
                return NotFound(
                                 new ErrorStructure("El registro que intentas actualizar no existe.")
                                 {
                                     Message = "No Encontrado",
                                     StatusCode = 404
                                 }
                               ); // 404 Not Found
            }

            return NoContent(); // 204 No Content (es el estándar para un PUT exitoso)
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<ActionResult> Delete(long woId, int taskNo)
        {
            var success = await _workOrderTasksService.DeleteAsync(woId, taskNo);

            if (!success)
            {
                return NotFound(
                                 new ErrorStructure("El registro que intentas actualizar no existe.")
                                 {
                                     Message = "No Encontrado",
                                     StatusCode = 404
                                 }
                    ); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }
    }
}
