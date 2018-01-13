using System.Threading.Tasks;

namespace ShoppingList.Shared.Services
{
    public interface IUserDataStore
    {
        Task<bool> DeleteUserByEmailAsync(string email);
    }
}