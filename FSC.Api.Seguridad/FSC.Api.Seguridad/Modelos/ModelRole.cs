using System.ComponentModel.DataAnnotations.Schema;

namespace FSC.Api.Seguridad.Modelos
{
    [Table("model_has_roles")]
    public class ModelRole
    {
        public ulong role_id { get; set; }
        public string model_type { get; set; }
        public ulong model_id { get; set; }

        public Role Role { get; set; } = new Role();

        public User User { get; set; } = new User();
    }
}
