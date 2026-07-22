using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace FSC.Api.Seguridad.Modelos
{
    [Table("role_has_permissions")]
    public class RolePermission
    {
        public ulong permission_id {  get; set; }
        public ulong role_id { get; set; }

        public Permission Permission { get; set; } = new Permission();

        public Role Role { get; set; } = new Role();
    }
}
