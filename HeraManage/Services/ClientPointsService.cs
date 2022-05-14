using HeraManage.Entities;
using HeraManage.Config;

namespace HeraManage.Services
{
    public class ClientPointsService
    {
        private ClientDbContext _clientDbContext;
        public ClientPointsService(ClientDbContext clientDbContext)
        {
            _clientDbContext = clientDbContext;
        }

        public void InitializeClientPoints(Guid uid)
        {
            var clientPoints = new AccountPointsEntity
            {
                uid = uid,
                Points = 0,
                LastUpdate = DateTime.Now
            };
            _clientDbContext.Add<AccountPointsEntity>(clientPoints);
            _clientDbContext.SaveChanges();
        }

        public dynamic GetClientPoints(Guid uid)
        {
            var accountPointsEntity = _clientDbContext.AccountPoints!.FirstOrDefault<AccountPointsEntity>(data => data.uid == uid);

            return accountPointsEntity!;
        }

        public dynamic AddPoints(Guid uid, double purchaseAmount)
        {
            double[] range = { 45.00, 60.00, 80.00 };
            int[] pointsAllocation = { 5, 10, 15, 20 };
            var collection = _clientDbContext.AccountPoints!.FirstOrDefault
                <AccountPointsEntity>(data => data.uid == uid);

            if (collection == null)
            {
                return new { error = true, message = "Point record not found" };
            }
            else if (purchaseAmount <= range[0])
            {
                collection!.Points = collection!.Points + pointsAllocation[0];
            }
            else if (purchaseAmount > range[0] && purchaseAmount <= range[1])
            {
                collection!.Points = collection!.Points + pointsAllocation[1];
            }
            else if (purchaseAmount > range[1] && purchaseAmount <= range[2])
            {
                collection!.Points = collection!.Points + pointsAllocation[2];
            }
            else if (purchaseAmount > (range[2] + 5))
            {
                collection!.Points = collection!.Points + pointsAllocation[3];
            }
            _clientDbContext.SaveChanges();

            return new
            {
                error = false,
                message = "Points successfully addaed to account",
                totalPoint = collection!.Points
            };
        }

        public dynamic RedeemClientPoints(Guid uid, int usedPoints)
        {
            var collection = _clientDbContext.AccountPoints!.FirstOrDefault<AccountPointsEntity>(data => data.uid == uid);

            if (collection == null) return new { error = true, message = "No point collect found for user" };
            if (collection.Points < usedPoints) return new { error = true, message = "Insufficient points." };

            collection.Points = collection.Points - usedPoints;
            _clientDbContext.SaveChanges();

            return new { error = false, message = "Points successfully redeemed." };
        }
    }
}