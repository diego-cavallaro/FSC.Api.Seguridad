using FSC.Api.Seguridad.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FSC.Api.Seguridad.Services
{
    public class PermissionService
    {
        private readonly AppDbContext _context;
        public PermissionService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Permission> GetPermissionsByLegajo(string legajo) 
        {
            var permisos = _context.Users
            .Where(u => u.legajo == legajo)
            .SelectMany(u => u.ModelRoles)
            .Select(mr => mr.Role)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission)
            .Distinct().ToList();

            return permisos;
        }
    }
}
