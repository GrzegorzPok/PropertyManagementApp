using System.Collections.Generic;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Dtos
{
    public class PropertyForDetailedDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}