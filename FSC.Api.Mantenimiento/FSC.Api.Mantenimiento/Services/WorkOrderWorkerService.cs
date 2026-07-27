using FSC.Api.Mantenimiento.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FSC.Api.Mantenimiento.Services
{
    public class WorkOrderWorkerService
    {
        private readonly MantenimientoContext _context;
        public WorkOrderWorkerService(MantenimientoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkOrderWorker>> GetAllAsync()
        {
            return await _context.WorkOrderWorkers
                .AsNoTracking() // Optimización: no rastreamos entidades de solo lectura
                .ToListAsync();
        }

        public async Task<WorkOrderWorker?> GetByIdsAsync(long woId, int taskNo, string employeeId)
        {
            return await _context.WorkOrderWorkers
                // .Include(w => w.WorkOrderTask) // Descomenta si necesitas traer los datos de la tarea relacional
                .FirstOrDefaultAsync(w => w.WoId == woId &&
                                          w.TaskNo == taskNo &&
                                          w.EmployeeId == employeeId
                                    );
        }

        public async Task<WorkOrderWorker> CreateAsync(WorkOrderWorker workOrderWorker)
        {
            await _context.WorkOrderWorkers.AddAsync(workOrderWorker);
            await _context.SaveChangesAsync();

            return workOrderWorker;
        }

        public async Task<WorkOrderWorker?> UpdateAsync(long woId, int taskNo, string employeeId, WorkOrderWorker updatedWorker)
        {
            var existingWorker = await GetByIdsAsync(woId, taskNo, employeeId);

            if (existingWorker == null)
            {
                return null; // O puedes lanzar una excepción tipo NotFoundException
            }

            // Actualizamos solo los campos modificables (ignoramos las claves primarias)
            existingWorker.WorkedHours = updatedWorker.WorkedHours;
            existingWorker.User = updatedWorker.User;
            existingWorker.ExtraHours = updatedWorker.ExtraHours;
            existingWorker.HighHours = updatedWorker.HighHours;
            existingWorker.DepthHours = updatedWorker.DepthHours;

            _context.WorkOrderWorkers.Update(existingWorker);
            await _context.SaveChangesAsync();

            return existingWorker;
        }

        public async Task<bool> DeleteAsync(long woId, int taskNo, string employeeId)
        {
            var existingWorker = await GetByIdsAsync(woId, taskNo, employeeId);

            if (existingWorker == null)
            {
                return false;
            }

            _context.WorkOrderWorkers.Remove(existingWorker);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
