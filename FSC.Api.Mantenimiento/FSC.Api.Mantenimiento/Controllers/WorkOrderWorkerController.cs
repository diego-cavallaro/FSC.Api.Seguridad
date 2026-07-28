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
        [Route("ObtenerTodos")]
        public async Task<ActionResult<IEnumerable<WorkOrderWorker>>> GetAll()
        {
            var workers = await _workOrderWorkerService.GetAllAsync();
            return Ok(workers); // Retorna 200 OK con la lista
        }

        // GET: api/WorkOrderWorker/5/1/EMP001
        [HttpGet()]
        [Route("ObtenerPorId")]
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
        [Route("Nuevo")]
        public async Task<ActionResult<WorkOrderWorker>> Create([FromBody] WorkOrderWorker workOrderWorker)
        {
            var createdWorker = await _workOrderWorkerService.CreateAsync(workOrderWorker);

            return await _workOrderWorkerService.GetByIdsAsync(createdWorker.WoId, createdWorker.TaskNo, createdWorker.EmployeeId);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult> Update([FromBody] WorkOrderWorker updatedWorker)
        {
            var result = await _workOrderWorkerService.UpdateAsync(updatedWorker);

            if (result == null)
            {
                return NotFound(new { message = "El registro que intentas actualizar no existe." }); // 404 Not Found
            }

            return NoContent(); // 204 No Content (es el estándar para un PUT exitoso)
        }

        // DELETE: api/WorkOrderWorker/5/1/EMP001
        [HttpDelete]
        [Route("Eliminar")]
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
