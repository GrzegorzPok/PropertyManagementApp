using System.Collections.Generic;

namespace PropertyManagement.API.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public string Description { get; set; }
        public double FlatArea { get; set; }
        public int RoomNumbers { get; set; }
        public int Level { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}