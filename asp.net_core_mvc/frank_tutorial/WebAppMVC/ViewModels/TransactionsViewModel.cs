using System.ComponentModel.DataAnnotations;
using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    public class TransactionsViewModel
    {
        [Display(Name = "Cashier Name")]
        public string CashierName { get; set; } = string.Empty;

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();

        public double GrandTotal { get; set; }

    }
}