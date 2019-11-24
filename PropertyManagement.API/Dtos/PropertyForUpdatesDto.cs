namespace PropertyManagement.API.Dtos
{
    public class PropertyForUpdatesDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public string Description { get; set; }
        public double FlatArea { get; set; }
        public int RoomNumbers { get; set; }
        public int Level { get; set; }
        public Models.User Owner { get; set; }
        public int OwnerId { get; set; }
    }
}