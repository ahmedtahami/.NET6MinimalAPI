using MinimalAPI.DTOs;

namespace MinimalAPI.Repositories
{
    public interface IUserRepositoryService 
    {
        UserDto GetUser(UserModel userModel); 
    }
}
