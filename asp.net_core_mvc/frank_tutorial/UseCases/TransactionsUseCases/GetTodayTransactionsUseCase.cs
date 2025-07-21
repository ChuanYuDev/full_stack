using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases
{
    public class GetTodayTransactionsUseCase : IGetTodayTransactionsUseCase
    {
        private readonly ITransactionsRepository transactionsRepository;

        public GetTodayTransactionsUseCase(ITransactionsRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        public IEnumerable<Transaction> Execute(string cashierName)
        {
            return transactionsRepository.GetByDayAndCashier(cashierName, DateTime.Now);
        }
    }
}