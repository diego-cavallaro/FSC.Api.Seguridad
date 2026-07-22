using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSC.Api.Seguridad.Modelos
{
    [Table("users")]
    public class User
    {
        [Key]
        public ulong id { get; set; }
        public string name { get; set; }
        public string nickName {  get; set; }
        public string email { get; set; }
        public DateTime? email_verified_at { get; set; }
        public string password { get; set; }
        public string? two_factor_secret { get; set; }
        public string? two_factor_recovery_codes { get; set; }
        public DateTime? two_factor_confirmed_at { get; set; }
        public string? remember_token { get; set; }
        public ulong? current_team_id { get; set; }
        public string? profile_photo_path { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool enable { get; set; }
        public string legajo {  get; set; }

        public ICollection<ModelRole> ModelRoles { get; set; } = [];
    }
}
