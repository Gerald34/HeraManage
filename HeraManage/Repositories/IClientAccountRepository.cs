using HeraManage.Requests;
namespace HeraManage.Repositories
{
    public interface IClientAccountRepository
    {
        dynamic CreateClientAccount(ClientAccountRequest clientAccount);
        dynamic GetClientByRSAId(Int64 RSAIdNumber);
    }
}