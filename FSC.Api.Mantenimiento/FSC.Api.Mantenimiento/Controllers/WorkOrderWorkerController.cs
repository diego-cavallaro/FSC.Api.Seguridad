using FluentValidation;
using FSC.Api.Mantenimiento.Errors;
using FSC.Api.Mantenimiento.Modelos;
using FSC.Api.Mantenimiento.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSC.Api.Mantenimiento.Controllers
{
    [Route("FSC/api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkOrderWorkerController : ControllerBase
    {
        private readonly WorkOrderWorkerService _workOrderWorkerService;
        private readonly IValidator<WorkOrderWorker> _validator;
        public WorkOrderWorkerController(IValidator<WorkOrderWorker> validator, WorkOrderWorkerService workOrderWorkerService)
        {
            _workOrderWorkerService = workOrderWorkerService;
            _validator = validator;
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public async Task<ActionResult<IEnumerable<WorkOrderWorker>>> GetAll()
        {
            var workers = await _workOrderWorkerService.GetAllAsync();
            return Ok(workers); // Retorna 200 OK con la lista
        }

        [HttpGet()]
        [Route("ObtenerPorId")]
        public async Task<ActionResult<WorkOrderWorker>> GetByIds(long woId, int taskNo, string employeeId)
        {
            var worker = await _workOrderWorkerService.GetByIdsAsync(woId, taskNo, employeeId);

            if (worker == null)
            {
                return NotFound(
                    new ErrorStructure("No se encontró el registro.")
                    {
                        Message = "No Encontrado",
                        StatusCode = 404
                    }

                    ); // 404 Not Found
            }

            return Ok(worker); // 200 OK
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<ActionResult<WorkOrderWorker>> Create([FromBody] WorkOrderWorker workOrderWorker)
        {
            // Ejecutar validación
            var validationResult = await _validator.ValidateAsync(workOrderWorker);
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
            var createdWorker = await _workOrderWorkerService.CreateAsync(workOrderWorker);

            return await _workOrderWorkerService.GetByIdsAsync(createdWorker.WoId, createdWorker.TaskNo, createdWorker.EmployeeId);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Update([FromBody] WorkOrderWorker updatedWorker)
        {
            // Ejecutar validación
            var validationResult = await _validator.ValidateAsync(updatedWorker);
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
            var result = await _workOrderWorkerService.UpdateAsync(updatedWorker);

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
        public async Task<ActionResult> Delete(long woId, int taskNo, string employeeId)
        {
            var success = await _workOrderWorkerService.DeleteAsync(woId, taskNo, employeeId);

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
