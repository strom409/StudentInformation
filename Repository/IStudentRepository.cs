using System.Linq.Expressions;

namespace StudentInformation.Repository
{
    public interface IStudentRepository<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<int> Delete(int id);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> expression, bool condition = true);
        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, bool condition = true);
        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> GetAllList();
    }
}
