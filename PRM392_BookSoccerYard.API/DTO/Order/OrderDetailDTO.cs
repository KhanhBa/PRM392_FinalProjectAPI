namespace PRM392_BookSoccerYard.API.DTO.Order
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? QuantityService { get; set; }

        public double? FinalPrice { get; set; }

        public double? TotalPrice { get; set; }

        public int? ServiceId { get; set; }

        public string ServiceName {  get; set; } 
    }
}
