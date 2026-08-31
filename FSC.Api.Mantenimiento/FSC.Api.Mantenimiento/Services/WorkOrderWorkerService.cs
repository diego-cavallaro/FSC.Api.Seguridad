using FSC.Api.Mantenimiento.Modelos;
using FSC.Api.Mantenimiento.Validation;
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
                .FirstOrDefaultAsync(w => w.WoId == woId &&
                                          w.TaskNo == taskNo &&
                                          w.EmployeeId == employeeId
                                    );
        }

        internal async Task<WorkOrderWorker> GetWorkOrderTaskWorker(long woId, string employeeId)
        {
            return await _context.WorkOrderWorkers
                .FirstOrDefaultAsync(w => w.WoId == woId &&
                                          w.EmployeeId == employeeId
                                    );
        }

        public async Task<WorkOrderWorker> CreateAsync(WorkOrderWorker workOrderWorker)
        {
            //El registro debe tener un trabajador asignado
            if (String.IsNullOrEmpty(workOrderWorker.EmployeeId))
                throw new CustomException("La tarea debe tener un Empleado asignado.");

            //Validamos que no exista el trabajador ya asignado a una tarea en la misma OT
            WorkOrderWorker worker = await GetWorkOrderTaskWorker(workOrderWorker.WoId, workOrderWorker.EmployeeId);
            if (worker != null)
                throw new CustomException("Ya existe una tarea con este Empleado asignado en la OT.");

            await _context.WorkOrderWorkers.AddAsync(workOrderWorker);
            await _context.SaveChangesAsync();

            return workOrderWorker;
        }

        public async Task<WorkOrderWorker?> UpdateAsync(WorkOrderWorker updatedWorker)
        {
            var existingWorker = await GetByIdsAsync(updatedWorker.WoId, updatedWorker.TaskNo, updatedWorker.EmployeeId);

            if (existingWorker == null)
                return null;

            // Actualizamos solo los campos modificables (ignoramos las claves primarias)
            existingWorker.WorkedHours = updatedWorker.WorkedHours;
            //existingWorker.User = updatedWorker.User;
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
                return false;

            _context.WorkOrderWorkers.Remove(existingWorker);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
