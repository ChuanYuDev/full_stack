using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCases;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ISearchTransactionsUseCase searchTransactionsUseCase;

        public TransactionsController(
            ISearchTransactionsUseCase searchTransactionsUseCase
        )
        {
            this.searchTransactionsUseCase = searchTransactionsUseCase;
        }

        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
            return View(transactionsViewModel);
        }

        // If we don't have Search method for Http Get, we can omit HttpPost attribute?
        // [HttpPost]
        public IActionResult Search(TransactionsViewModel transactionsViewModel)
        {
            if (ModelState.IsValid)
            {
                // transactionsViewModel.Transactions = TransactionsRepository.Search(
                //     cashierName: transactionsViewModel.CashierName,
                //     startDate: transactionsViewModel.StartDate,
                //     endDate: transactionsViewModel.EndDate
                // );

                var transactions = searchTransactionsUseCase.Execute(
                    cashierName: transactionsViewModel.CashierName ?? string.Empty,
                    startDate: transactionsViewModel.StartDate,
                    endDate: transactionsViewModel.EndDate
                );

                transactionsViewModel.Transactions = transactions;

                transactionsViewModel.GrandTotal = transactionsViewModel.Transactions.Sum(
                    x => x.Price * x.SoldQty
                );
            }

            return View("Index", transactionsViewModel);
        }
    }
}