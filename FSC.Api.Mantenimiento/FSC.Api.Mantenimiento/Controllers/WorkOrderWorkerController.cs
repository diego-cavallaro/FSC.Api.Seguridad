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
        public WorkOrderWorkerController(WorkOrderWorkerService workOrderWorkerService)
        {
            _workOrderWorkerService = workOrderWorkerService;
        }

        // GET: api/WorkOrderWorker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderWorker>>> GetAll()
        {
            var workers = await _workOrderWorkerService.GetAllAsync();
            return Ok(workers); // Retorna 200 OK con la lista
        }

        // GET: api/WorkOrderWorker/5/1/EMP001
        [HttpGet("{woId}/{taskNo}/{employeeId}")]
        public async Task<ActionResult<WorkOrderWorker>> GetByIds(long woId, int taskNo, string employeeId)
        {
            var worker = await _workOrderWorkerService.GetByIdsAsync(woId, taskNo, employeeId);

            if (worker == null)
            {
                return NotFound(new { message = "No se encontró el registro asignado a la orden de trabajo." }); // 404 Not Found
            }

            return Ok(worker); // 200 OK
        }

        // POST: api/WorkOrderWorker
        [HttpPost]
        public async Task<ActionResult<WorkOrderWorker>> Create([FromBody] WorkOrderWorker workOrderWorker)
        {
            var createdWorker = await _workOrderWorkerService.CreateAsync(workOrderWorker);

            // Retorna 201 Created y especifica la URL de donde se puede consultar el nuevo recurso
            return CreatedAtAction(
                nameof(GetByIds),
                new
                {
                    woId = createdWorker.WoId,
                    taskNo = createdWorker.TaskNo,
                    employeeId = createdWorker.EmployeeId
                },
                createdWorker);
        }

        // PUT: api/WorkOrderWorker/5/1/EMP001
        [HttpPut("{woId}/{taskNo}/{employeeId}")]
        public async Task<ActionResult> Update(long woId, int taskNo, string employeeId, [FromBody] WorkOrderWorker updatedWorker)
        {
            // Validamos que los IDs de la URL coincidan con los del objeto enviado en el Body (buena práctica)
            if (woId != updatedWorker.WoId || taskNo != updatedWorker.TaskNo || employeeId != updatedWorker.EmployeeId)
            {
                return BadRequest(new { message = "Los identificadores de la ruta no coinciden con los datos enviados." }); // 400 Bad Request
            }

            var result = await _workOrderWorkerService.UpdateAsync(woId, taskNo, employeeId, updatedWorker);

            if (result == null)
            {
                return NotFound(new { message = "El registro que intentas actualizar no existe." }); // 404 Not Found
            }

            return NoContent(); // 204 No Content (es el estándar para un PUT exitoso)
        }

        // DELETE: api/WorkOrderWorker/5/1/EMP001
        [HttpDelete("{woId}/{taskNo}/{employeeId}")]
        public async Task<ActionResult> Delete(long woId, int taskNo, string employeeId)
        {
            var success = await _workOrderWorkerService.DeleteAsync(woId, taskNo, employeeId);

            if (!success)
            {
                return NotFound(new { message = "El registro que intentas eliminar no existe." }); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }
    }
}
