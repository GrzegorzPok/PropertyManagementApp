namespace PropertyManagement.API.Models
{
    public class Rent
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public User User { get; set; }
        public Property Property { get; set; }
    }
}