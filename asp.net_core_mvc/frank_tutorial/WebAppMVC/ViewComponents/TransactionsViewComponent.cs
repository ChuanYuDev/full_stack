using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

// Partial view is not really self-contained
//      It's not very much encapsulated, at least in terms of behavior, everything mixed up with other action methods
//      For example
//      `_Category.cshtml` partial view depends on `Add.cshtml` to populate model
//      And `Add.cshtml` depends on `Add` action method of `CategoriesController` to populate model

// ViewComponent
//      Another way to create a resuable component
//      It's more self-contained than partial view
//      It can be directly rendered in the View
//      Also it can contain behavior

namespace WebAppMVC.ViewComponents
{
    // Tells the compile that this class is ViewComponent
    //      1. Class name is ended with ViewComponent
    //      2. Add ViewComponent attribute to decorate the class
    //      3. Derive from ViewComponent class
    [ViewComponent]
    public class TransactionsViewComponent : ViewComponent
    {
        // Similar to mini controller

        // Only show the transactions for the current day and login cashier
        public IViewComponentResult Invoke(string userName)
        {
            var transactions = TransactionsRepository.GetByDayAndCashier(userName, DateTime.Now);
            return View(transactions);
        }
    }
}