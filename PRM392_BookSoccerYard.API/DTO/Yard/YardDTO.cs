namespace PRM392_BookSoccerYard.API.DTO.Yard
{
    public class YardDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public double? Price { get; set; }
        public bool? Status { get; set; }

    }
}
