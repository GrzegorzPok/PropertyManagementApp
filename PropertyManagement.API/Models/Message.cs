using System;

namespace PropertyManagement.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? MessageSent { get; set; }
        public bool SenderDeleted { get; set; }
        public bool FlatDeleted { get; set; }
    }
}