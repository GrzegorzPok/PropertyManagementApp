using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class PropertyManagementRepository : IPropertyManagementRepository
    {
        private readonly DataContext _context;

        public PropertyManagementRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            var properties = await _context.Properties.Include(p => p.Photos).ToListAsync();

            return properties;
        }

        public async Task<Property> GetProperty(int id)
        {
            var property = await _context.Properties.Include(p => p.Photos).FirstOrDefaultAsync(prop => prop.Id == id);

            return property;
        }

        public async Task<Property> GetEmptyProperty()
        {
            var property = new Property(); 

            return  property;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            //var userRents = await GetUserRents()

            return users;
        }

        public async Task<IEnumerable<int>> GetUserRents(int userId)
        {
            var user = await _context.Users.Include(c => c.Rents).FirstOrDefaultAsync(u => u.Id == userId);

            return user.Rents.Where(u => u.UserId == userId).Select(i => i.PropertyId);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetMainPhotoForProperty(int propertyId)
        {
            return await _context.Photos.Where(p => p.PropertyId == propertyId)
                .FirstOrDefaultAsync(p => p.isMain);
        }

        public async Task<Rent> GetRent(int userId, int propertyId)
        {
            return await _context.Rents.FirstOrDefaultAsync(r => r.UserId == userId && r.PropertyId == propertyId);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessagesForUser(bool? isInbox, int userId)
        {
            var messages = await _context.Messages
                .Include(i => i.Sender)
                .Include(i => i.Property)
                .ToListAsync();

            if (isInbox.HasValue && isInbox.Value) {
                messages = messages.Where(u => u.SenderId == userId && u.SenderDeleted == false).ToList();
            }
            else if (isInbox.HasValue && !isInbox.Value) {
                var myPropierties = _context.Rents
                    .Where(r => r.UserId == userId)
                    .Select(r => r.PropertyId)
                    .ToList();

                var reveivers = _context.Rents
                    .Where(r => myPropierties.Any(m => m == r.PropertyId))
                    .Select(r => r.UserId)
                    .Distinct()
                    .ToList();

                messages = messages.Where(m => reveivers.Any(r => r == m.SenderId) && m.SenderId != userId).ToList();
            }
            else {
                messages = messages.Where(m => m.SenderId == userId && m.IsRead == false && m.SenderDeleted == false).ToList();
            }

            messages = messages.OrderByDescending(d => d.MessageSent).ToList();
            return messages;
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int propertyId)
        {
            var messages = await _context.Messages
                .Include(i => i.Sender)
                .Include(i => i.Property)
                .Where(m => m.PropertyId == propertyId && m.SenderDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}