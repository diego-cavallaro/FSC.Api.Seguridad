using FluentValidation;
using FSC.Api.Mantenimiento.Modelos;

namespace FSC.Api.Mantenimiento.Validators
{
    public class WorkOrderTaskValidator : AbstractValidator<WorkOrderTask>
    {
        public WorkOrderTaskValidator()
        {
            RuleFor(x => x.Observations)
                .NotNull().WithMessage("La descripción de la tarea no puede estar en blanco.")
                .NotEmpty().WithMessage("La descripción de la tarea no puede estar en blanco.");
            RuleFor(x => x.User)
                .NotNull().WithMessage("Debe tener asignado un usuario responsable.")
                .NotEmpty().WithMessage("Debe tener asignado un usuario responsable.");
            // Se evalúa que Fecha Cierre y Usuario Cierre vengan de la mano
            RuleFor(x => x)
                .Must(x =>
                    // Si vino Fecha Cierre sin Usuario de Cierre (Error)
                    (string.IsNullOrEmpty(x.CloseUser) && x.CloseDate.HasValue) ||
                    // Si vino Usuario de Cierre pero NO la Fecha de Cierre (Error)
                    (!string.IsNullOrEmpty(x.CloseUser) && !x.CloseDate.HasValue)
                )
                .WithMessage("Fecha Cierre y Usuario de Cierre vienen juntos si traen valor.");
        }
    }
}
