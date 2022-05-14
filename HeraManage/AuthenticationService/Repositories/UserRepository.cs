using AuthenticationService.Requests;
using AuthenticationService.Entities;

namespace AuthenticationService.Repositories
{
	public interface IUserRepository
	{
        dynamic Authenticate(AuthenticateRequest authenticateRequest);
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(int id);
        dynamic CreateAccount(UserEntity userEntity);
        dynamic ActivateAccount(int userID, string username);
    }
}

