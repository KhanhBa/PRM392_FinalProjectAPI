namespace PRM392_BookSoccerYard.API.DTO.Order
{
    public class CalendarDTO
    {
        public int SlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int OrderId { get; set; }
    }
}
