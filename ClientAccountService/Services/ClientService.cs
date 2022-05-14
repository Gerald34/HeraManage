using ClientAccountService.Entities;
using ClientAccountService.Repositories;
using ClientAccountService.Config;
using Microsoft.Extensions.Options;
using ClientAccountService.Utils;
using ClientAccountService.Requests;

namespace ClientAccountService.Services
{
    public class ClientService : IClientAccountRepository
    {

        private ClientDbContext _clientDbContext;
        private readonly AppSettings _appSettings;
        public ClientService(
            IOptions<AppSettings> appSettings,
            ClientDbContext clientDbContext)
        {
            _clientDbContext = clientDbContext;
            _appSettings = appSettings.Value;
        }
        public dynamic CreateClientAccount(ClientAccountRequest accountRequest)
        {
            bool userExists = _UserExists(accountRequest.RSAIdNumber);
            if (userExists) return new { error = true, message = "User already exists" };

            ClientEntity client = new ClientEntity
            {
                uid = Guid.NewGuid(),
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                Email = accountRequest.Email,
                Password = PasswordEncryptor.EncriptString(accountRequest.Password, 1000),
                RSAIdNumber = (_ValidateRSAIdNumber(accountRequest.RSAIdNumber) ? accountRequest.RSAIdNumber : 0),
                DateOfBirth = accountRequest.DateOfBirth,
                Gender = accountRequest.Gender,
                Active = 0,
                Verified = false
            };
            _clientDbContext.Add<ClientEntity>(client);
            _clientDbContext.SaveChanges();

            ClientAccountEntity clientAccount = new ClientAccountEntity
            {
                uid = client.uid,
                AccountNumber = AccountNumberUtil.CreateAccountNumber(),
                AccountType = accountRequest.AccountType,
                Active = 0,
                CreatedAt = DateTime.Now
            };
            _clientDbContext.Add<ClientAccountEntity>(clientAccount);
            _clientDbContext.SaveChanges();
            // _clientPointsService.InitializeClientPoints(client.uid);

            return new { error = false, message = "Account created", data = GetClientByRSAId(client.RSAIdNumber) };
        }

        private bool _UserExists(Int64 RSAIdNumber)
        {
            var data = GetClientByRSAId(RSAIdNumber);
            return (data == null) ? false : true;
        }

        public dynamic GetClientByRSAId(Int64 RSAIdNumber)
        {
            var data = _clientDbContext.Clients?.FirstOrDefault
                <ClientEntity>(account => account.RSAIdNumber == RSAIdNumber);

            if (data == null)
            {
                return new { error = true, message = "Account not found" };
            }

            var clientObject = new
            {
                client = data,
                account = _clientDbContext.ClientAccounts?.FirstOrDefault
                    <ClientAccountEntity>(account => account.uid == data.uid)
            };

            dynamic response = new
            {
                error = false,
                message = "Account found",
                data = clientObject
            };

            return response;
        }

        private bool _ValidateRSAIdNumber(Int64 RSAIdNumber)
        {
            return true;
        }
    }
}