using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface ITransactionRepository
    {
        public void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date);
        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate);
    }
}