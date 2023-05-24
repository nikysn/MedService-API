
namespace MedService.DAL.Abstraction.Repositories
{
    public interface IBaseRepository<T>
    {
        Task SaveChanges();
        Task<T> GetUserByLoginAsync(string login);
    }
}
