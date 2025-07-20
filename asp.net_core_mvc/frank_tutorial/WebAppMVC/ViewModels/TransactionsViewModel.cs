using System.ComponentModel.DataAnnotations;
using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    public class TransactionsViewModel
    {
        // User may or may not input the cashier name
        [Display(Name = "Cashier Name")]
        // public string CashierName { get; set; } = string.Empty;
        public string? CashierName { get; set; }

        // Transactions view shows the start and end date as today in default
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();

        public double GrandTotal { get; set; }

    }
}