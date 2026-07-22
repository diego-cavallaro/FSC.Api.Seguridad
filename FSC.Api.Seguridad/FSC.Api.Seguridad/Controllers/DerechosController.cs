using FSC.Api.Seguridad.Modelos;
using FSC.Api.Seguridad.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FSC.Api.Seguridad.Controllers
{
    [Route("FSC/Api/[controller]")]
    [ApiController]
    public class DerechosController : ControllerBase
    {
        private readonly PermissionService _permissionService;
        public DerechosController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("GetDerechos")]
        //[Authorize]
        public async Task<ActionResult<List<Permission>>> GetPermissions()
        {
            // Extrae el token directamente del contexto sin lidiar con strings
            string bearerToken = await HttpContext.GetTokenAsync("access_token");
            // Obtenemos el legajo de la variable User generado por el middleware de validacion del token
            var legajo = User.FindFirst("legajo")?.Value;

            if (bearerToken != null && !String.IsNullOrEmpty(legajo))
            {
                var permissionList = _permissionService.GetPermissionsByLegajo(legajo);

                return Ok(permissionList);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
