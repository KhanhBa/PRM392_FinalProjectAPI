namespace PRM392_BookSoccerYard.API.DTO.Order
{
    public class CreatedOrder
    {
        public int? YardId { get; set; }
        public int? CustomerId { get; set; }
        public int? Duration { get; set; }
        public string Status { get; set; }
        public int SlotId { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? BookingDate { get; set; }
        public CreatedPayment payment { get; set; }
        public List<CreatedOrderDetail> orderDetails { get; set; }
    }
    public class CreatedPayment
    {
        public double? Price { get; set; }

        public string Status { get; set; }

        public string Method { get; set; }

    }
    public class CreatedOrderDetail
    {
        public int? QuantityService { get; set; }

        public double? FinalPrice { get; set; }

        public double? TotalPrice { get; set; }

        public int? ServiceId { get; set; }
    }
}
