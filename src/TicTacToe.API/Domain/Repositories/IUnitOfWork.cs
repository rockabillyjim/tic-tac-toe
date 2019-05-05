using System.Threading.Tasks;

namespace TicTacToe.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}