using ClientAccountService.Requests;

namespace ClientAccountService.Repositories
{
    public interface IClientAccountRepository
    {
        dynamic CreateClientAccount(ClientAccountRequest clientAccount);
        dynamic GetClientByRSAId(Int64 RSAIdNumber);
    }
}