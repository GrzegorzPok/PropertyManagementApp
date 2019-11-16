namespace PropertyManagement.API.Dtos
{
    public class PropertyForListDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public string PhotoUrl { get; set; }
    }
}