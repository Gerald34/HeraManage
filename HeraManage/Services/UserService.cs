using Microsoft.Extensions.Options;
using HeraManage.Entities;
using HeraManage.Config;
using HeraManage.Requests;
using HeraManage.Repositories;
using HeraManage.Utils;

namespace HeraManage.Services
{
    public class UserService : IUserRepository
    {
        private UsersDbContext _usersDbContext;
        private readonly AppSettings _appSettings;
        private dynamic _response = null;

        public UserService(IOptions<AppSettings> appSettings, UsersDbContext usersDbContext)
        {
            _appSettings = appSettings.Value;
            _usersDbContext = usersDbContext;
        }

        /// <summary>
        /// Authenticate user
        /// Generate Json Web Token
        /// </summary>
        /// <param name="authenticateRequest"></param>
        /// <returns>Dynamic object including user record and token</returns>
        public dynamic Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = _usersDbContext.AdminUsers.FirstOrDefault(
                user => user.username == authenticateRequest.username
            );

            if (user == null) return new { error = true, message = "User not found." };

            if (!PasswordEncryptor.decryptString(authenticateRequest!.password, user.password))
            {
                _response = new { error = true, message = "Username or password is incorrect." };
                return _response;
            }

            var token = JwtTokenGenerator.GenerateJwtToken(user, _appSettings.Secret);
            _response = new
            {
                error = false,
                message = "User found",
                data = new AuthenticateResponse(user, token)
            };

            return _response;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User entity</returns>
        public UserEntity GetById(int id)
        {
            return _usersDbContext.AdminUsers.FirstOrDefault<UserEntity>(collection => collection.id == id);
        }

        /// <summary>
        /// Get list of existing user records
        /// </summary>
        /// <returns>User records</returns>
        public IEnumerable<UserEntity> GetAll()
        {
            List<UserEntity> allUsers = _usersDbContext.AdminUsers.ToList<UserEntity>();

            return allUsers;
        }

        /// <summary>
        /// Create new account from UserEntity
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns>Dynamic response</returns>
        public dynamic CreateAccount(UserEntity userEntity)
        {
            if (_UserExists(userEntity.username))
            {
                return new
                {
                    error = true,
                    message = "Account with username " + userEntity.username + " already exists."
                };
            }
            _response = _saveUserData(userEntity);
            return _response;
        }

        /// <summary>
        /// Checks if user with provided email exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolean</returns>
        private bool _UserExists(string username)
        {
            var user = _usersDbContext.AdminUsers.FirstOrDefault<UserEntity>(user => user.username == username);
            return (user == null) ? false : true;
        }

        /// <summary>
        /// Save processed data to admin user's table
        /// </summary>
        /// <param name="userEntity">Admin user entity</param>
        /// <returns> dynamic object</returns>
        private dynamic _saveUserData(UserEntity userEntity)
        {
            try
            {
                _usersDbContext.AdminUsers.Add(userEntity);
                _usersDbContext.SaveChanges();
                return new
                {
                    error = false,
                    message = "Account successfully created",
                    data = userEntity
                };
            }
            catch (Exception e)
            {
                return new { error = true, message = e.Message };
            }
        }

        /// <summary>
        /// Activate user account
        /// Update active and verified columns
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public dynamic ActivateAccount(int userID, string username)
        {
            var user = _usersDbContext.AdminUsers.FirstOrDefault<UserEntity>
            (data => data.id == userID && data.username == username);
            if (user == null)
            {
                return new { error = true, message = "Account not found." };
            }

            if (user.active > 0)
            {
                return new { error = true, message = "Account already activated on " + user.updatedAt };
            }

            user.active = 1;
            user.verified = true;
            user.updatedAt = DateTime.Now;
            _usersDbContext.SaveChanges();
            return new { error = false, message = "Account successfully verified and updated." };
        }

    }
}