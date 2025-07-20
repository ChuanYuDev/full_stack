namespace WebAppMVC.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime TimeStamp { get; set; }

        public int ProductId { get; set; }

        // Transaction is historical information
        //      Record all the information in case they change
        public string ProductName { get; set; } = "";

        public double Price { get; set; }

        public int BeforeQty { get; set; }

        public int SoldQty { get; set; }

        public string CashierName { get; set; } = "";
    }
}
