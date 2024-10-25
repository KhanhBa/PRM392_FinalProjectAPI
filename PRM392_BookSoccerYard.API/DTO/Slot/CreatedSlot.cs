namespace PRM392_BookSoccerYard.API.DTO.Slot
{
    public class CreatedSlot
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Name { get; set; }

        public double PriceUp { get; set; }
    }
}
