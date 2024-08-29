using NHibernate.Linq;
using NHibernate;
using System.Linq.Expressions;

namespace StudentInformation.Repository
{
    public class StudentRepository<TEntity> : IStudentRepository<TEntity>
    {
        private readonly NHibernate.ISession _session;
        private readonly ISessionFactory _sessionFactory;

        public StudentRepository(NHibernate.ISession session, ISessionFactory sessionFactory)
        {
            _session = session;
            _sessionFactory = sessionFactory;
        }
        public async Task<IList<TEntity>> GetAll()
        {
            var response = await _session.Query<TEntity>().ToListAsync();
            return response;
        }
        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, bool condition = true)
        {
            return await GetQuery(condition).Where(expression).ToListAsync();
        }
        private IQueryable<TEntity> GetQuery(bool includeDeleted = false)
        {
            return includeDeleted ? _session.Query<TEntity>() : _session.Query<TEntity>();
        }
        public async Task<TEntity> GetById(int id)
        {
            var model = await _session.GetAsync<TEntity>(id);
            return model;
        }
        public async Task<TEntity> GetById(Expression<Func<TEntity, bool>> expression, bool condition = true)
        {
            return await _session.Query<TEntity>().FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("model");
            await _session.SaveAsync(entity);
            return entity;
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                await _session.UpdateAsync(entity);
                await transaction.CommitAsync();
                _session.Clear();

            }
            return entity;
        }
        public async Task<int> Delete(int id)
        {
            var result = await _session.GetAsync<TEntity>(id);
            if (result != null)
            {
                _session.Delete(result);
                await _session.FlushAsync();
                return id;
            }
            return 0;
        }
        public async Task<IList<TEntity>> GetAllList()
        {
            var response = await _session.Query<TEntity>().ToListAsync();
            return response;
        }

    }
}