using System.Threading.Tasks;

namespace ShoppingList.Shared.Helpers
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}