using FSC.Api.Mantenimiento.Modelos;
using FSC.Api.Mantenimiento.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace FSC.Api.Mantenimiento.Controllers
{
    [Route("FSC/api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkOrderTaskController : ControllerBase
    {
        private readonly WorkOrderTasksService _workOrderTasksService;

        public WorkOrderTaskController(WorkOrderTasksService workOrderTasksService)
        {
            _workOrderTasksService = workOrderTasksService;
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
    }
}
