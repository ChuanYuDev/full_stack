using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases
{
    public class GetTodayTransactionsUseCase : IGetTodayTransactionsUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public GetTodayTransactionsUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public IEnumerable<Transaction> Execute(string cashierName)
        {
            return transactionRepository.GetByDayAndCashier(cashierName, DateTime.Now);
        }
    }
}