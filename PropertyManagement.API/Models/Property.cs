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
        public ICollection<Photo> Photos { get; set; }
    }
}