using FSC.Api.Seguridad.Modelos;

namespace FSC.Api.Seguridad.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers() 
        {
            return _context.Users.ToList();
        }
        public User getByNickName(string loginName)
        {
            return _context.Users.FirstOrDefault(x => x.nickName == loginName);
        }
    }
}
