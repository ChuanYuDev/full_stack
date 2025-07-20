using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            return View(transactionsViewModel);
        }

        [HttpPost]
        public IActionResult Index(TransactionsViewModel transactionsViewModel)
        {
            if (ModelState.IsValid)
            {
                transactionsViewModel.Transactions = TransactionsRepository.Search(
                    cashierName: transactionsViewModel.CashierName,
                    startDate: transactionsViewModel.StartDate,
                    endDate: transactionsViewModel.EndDate
                );

                transactionsViewModel.GrandTotal = transactionsViewModel.Transactions.Sum(
                    x => x.Price * x.SoldQty
                );
            }

            return View(transactionsViewModel);
        }
    }
}