using System.Threading.Tasks;

using ShoppingList.Shared.Models;
using ShoppingList.Shared.Wrappers;

namespace ShoppingList.Shared.Services
{
    public interface IGroceryListDataStore
    {
        Task<bool> DeleteSharedListUser(int listId, UserWrapper selectedUser);
    }
}