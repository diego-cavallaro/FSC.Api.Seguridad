using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSC.Api.Seguridad.Modelos
{
    [Table("permissions")]
    public class Permission
    {
        [Key]
        public ulong id {  get; set; }
        public string name { get; set; }
        public string guard_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
