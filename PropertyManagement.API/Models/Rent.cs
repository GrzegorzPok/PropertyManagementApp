namespace PropertyManagement.API.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int Price { get; set; }
    }
}