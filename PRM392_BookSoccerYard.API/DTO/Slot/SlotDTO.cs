namespace PRM392_BookSoccerYard.API.DTO.Slot
{
    public class SlotDTO
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool Status { get; set; }

        public string Name { get; set; }

        public double PriceUp { get; set; }
    }
}
