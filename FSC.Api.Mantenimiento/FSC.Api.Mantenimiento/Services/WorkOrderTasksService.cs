using FSC.Api.Mantenimiento.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FSC.Api.Mantenimiento.Services
{
    public class WorkOrderTasksService
    {
        private readonly MantenimientoContext _context;
        public WorkOrderTasksService(MantenimientoContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkOrderTask> GetPendingTasksByUsuario(string userId)
        {
            var tareas = _context.WorkOrderTasks
                .Where(x => x.User == userId)
                .Where(x => x.CloseDate == null)
                .Include(x => x.WorkOrderWorkers)
                .OrderByDescending(x => x.CreateDate).ToList();

            return tareas;
        }
    }
}
