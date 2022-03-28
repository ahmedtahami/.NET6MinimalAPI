using MinimalAPI.DTOs;

namespace MinimalAPI.Repositories
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private List<UserDto> _users => new() { new("admin", "Admin@123"), }; 
        public UserDto GetUser(UserModel userModel)
        {
            return _users.FirstOrDefault(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
        }
    }
}
