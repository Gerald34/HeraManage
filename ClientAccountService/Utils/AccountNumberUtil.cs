namespace ClientAccountService.Utils
{
    public class AccountNumberUtil
    {
        public static dynamic CreateAccountNumber()
        {
            Random rnd = new Random();
            double rndNumber = Convert.ToDouble(DateTime.Now.ToString("dMMyyyyHH00")) + rnd.Next(10, 99);
            return rndNumber;
        }
    }
}