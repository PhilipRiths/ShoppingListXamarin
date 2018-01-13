using System.Threading.Tasks;

using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Services
{
    public interface IUserDataStore
    {
        Task<bool> DeleteByEmailAsync(string email);

        Task<User> GetByEmailAsync(string email);
    }
}