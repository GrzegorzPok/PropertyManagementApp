using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
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

            property.Country = "";
            property.City = "";
            property.PropertyNumber = "";
            property.Street = "";

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

            return users;
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
    }
}