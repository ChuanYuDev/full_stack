namespace WebAppMVC.Models
{
    public static class TransactionsRepository
    {
        private static List<Transaction> _transactions = new List<Transaction>();

        public static IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))

                // Date property
                //      Returns the date part of this DateTime. The resulting value
                //      corresponds to this DateTime with the time-of-day part set to
                //      zero (midnight)
                return _transactions.Where(x => x.TimeStamp.Date == date.Date);

            else
                return _transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp.Date == date.Date
                );
        }

        public static IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                // Add 1 day
                //      If we want to search endDate is July 1st
                //      The result will include transactions on July 1st 
                //      Add 1 day to endDate.Date will be July 2nd
                return _transactions.Where(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date);

            else
                return _transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date
                );
        }

        public static void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            var transaction = new Transaction
            {
                TimeStamp = DateTime.Now,
                ProductId = productId,
                ProductName = productName,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                CashierName = cashierName
            };

            int maxId = 0;

            if (_transactions.Count > 0)
            {
                maxId = _transactions.Max(x => x.TransactionId);
            }

            transaction.TransactionId = maxId + 1;

            _transactions?.Add(transaction);
        }
    }
}