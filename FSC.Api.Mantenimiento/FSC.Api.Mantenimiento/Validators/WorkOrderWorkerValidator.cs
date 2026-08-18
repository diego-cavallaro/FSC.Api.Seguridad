using FluentValidation;
using FSC.Api.Mantenimiento.Modelos;

namespace FSC.Api.Mantenimiento.Validators
{
    public class WorkOrderWorkerValidator : AbstractValidator<WorkOrderWorker>
    {
        public WorkOrderWorkerValidator()
        {
            RuleFor(x => x.WorkedHours)
                .NotNull().WithMessage("Horas Trabajadas debe tener un valor");
            RuleFor(x => x.DepthHours)
                .NotNull().WithMessage("Horas en Profundidad debe tener un valor");
            RuleFor(x => x.ExtraHours)
                .NotNull().WithMessage("Horas Extras debe tener un valor");
            RuleFor(x => x.HighHours)
                .NotNull().WithMessage("Horas en Altura debe tener un valor");
            RuleFor(X => X.EmployeeId)
                .NotEmpty().WithMessage("Debe tener un Empleado asignado")
                .MaximumLength(15).WithMessage("El Legajo de Empleado no puede superar los 15 caracteres");
        }
    }
}
