using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSC.Api.Seguridad.Modelos
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public ulong id { get; set; }
        public string name { get; set; }
        public string guard_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public ICollection<ModelRole> ModelRoles { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = []; 
    }
}
