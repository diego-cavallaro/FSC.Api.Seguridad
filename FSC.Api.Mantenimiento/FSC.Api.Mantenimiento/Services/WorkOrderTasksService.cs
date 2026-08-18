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
        public async Task<WorkOrderTask?> GetByIdsAsync(long woId, int taskNo)
        {
            return await _context.WorkOrderTasks
                .FirstOrDefaultAsync(w => w.WoId == woId &&
                                          w.TaskNo == taskNo
                                    );
        }

        public async Task<WorkOrderTask> CreateAsync(WorkOrderTask workOrderTask)
        {
            //Sobreescribimos la fecha de creacion
            workOrderTask.CreateDate = DateTime.Now;

            await _context.WorkOrderTasks.AddAsync(workOrderTask);
            await _context.SaveChangesAsync();

            return workOrderTask;
        }

        public async Task<WorkOrderTask?> UpdateAsync(WorkOrderTask updatedWorkOrderTask)
        {
            var existingWorkOrderTask = await GetByIdsAsync(updatedWorkOrderTask.WoId, updatedWorkOrderTask.TaskNo);

            if (existingWorkOrderTask == null)
                return null;

            if(!String.IsNullOrEmpty(updatedWorkOrderTask.CloseUser) && updatedWorkOrderTask.CloseDate.HasValue)
            {
                //Verificamos primero que haya personal asignado
                if(existingWorkOrderTask.WorkOrderWorkers.Count == 0)
                    throw new InvalidOperationException("No se puede cerrar una Tarea sin haber asignado Personal.");

                //Verificamos que el Personal involucrado tenga horas cargadas
                foreach (WorkOrderWorker worker in existingWorkOrderTask.WorkOrderWorkers)
                {
                    if(!worker.WorkedHours.HasValue && !worker.ExtraHours.HasValue && 
                        !worker.HighHours.HasValue && !worker.DepthHours.HasValue)
                       throw new InvalidOperationException("Al cerrar la Tarea, no puede haber personal con horas en cero.");
                }
            }

            // Actualizamos solo los campos modificables (ignoramos las claves primarias)
            existingWorkOrderTask.StartDate = updatedWorkOrderTask.StartDate;
            existingWorkOrderTask.EndDate = updatedWorkOrderTask.EndDate;
            existingWorkOrderTask.Observations = updatedWorkOrderTask.Observations;
            existingWorkOrderTask.CloseDate = updatedWorkOrderTask.CloseDate;
            existingWorkOrderTask.CloseUser = updatedWorkOrderTask.CloseUser;

            _context.WorkOrderTasks.Update(existingWorkOrderTask);
            await _context.SaveChangesAsync();

            return existingWorkOrderTask;
        }

        public async Task<bool> DeleteAsync(long woId, int taskNo)
        {
            var existingWorkOrderTask = await GetByIdsAsync(woId, taskNo);

            if (existingWorkOrderTask == null)
                return false;

            _context.WorkOrderTasks.Remove(existingWorkOrderTask);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
