using System.Threading.Tasks;
using ShoppingList.Shared.Models;

namespace ShoppingList.Shared.Services
{
    public interface IGoogleAuthService
    {
        Task<bool> TrySignIn();
        bool TrySignOut();
    }
}