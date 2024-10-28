namespace PRM392_BookSoccerYard.API.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? YardId { get; set; }

        public int? CustomerId { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public int? Duration { get; set; }

        public string Status { get; set; }

        public int SlotId { get; set; }

        public int? TotalPrice { get; set; }

        public DateTime? BookingDate { get; set; }
    }
}
