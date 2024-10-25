namespace PRM392_BookSoccerYard.API.DTO.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Img { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
