using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public interface IPropertyManagementRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entoty) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<IEnumerable<Property>> GetProperties();
         Task<Property> GetProperty(int id);
         Task<Property> GetEmptyProperty();
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForProperty(int propertyId);
         Task<Rent> GetRent(int userId, int propertyId);
         Task<IEnumerable<int>> GetUserRents(int userId);
    }
}