using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 根据类特性，DbLocatorAttribute 创建仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class DbRepository<TEntity> : DbRepository<TEntity, long>,IFreeSqlRepository<TEntity>, IDbRepository<TEntity> where TEntity : class, new()
    {
        public DbRepository(IDbRepositoryFactory repositoryFactory):base(repositoryFactory)
        {
        }
    }
    /// <summary>
    /// 装饰器，实现 IFreeSqlRepository<TEntity, TKey> 接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public partial class DbRepository<TEntity, TKey> : IFreeSqlRepository<TEntity, TKey>, IDbRepository<TEntity, TKey> where TEntity : class, new()
    {
        private readonly IFreeSqlRepository<TEntity, TKey> repository;

        public DbRepository(IDbRepositoryFactory repositoryFactory)
        {
            repository = repositoryFactory.CreateDbRepository<TEntity, TKey>();
        }

        public dynamic DynamicDbContext => repository.DynamicDbContext;

        public IAdo Ado => repository.Ado;

        public ISelect<TEntity> Entities => repository.Entities;

        public IDataFilter<TEntity> DataFilter => repository.DataFilter;

        public ISelect<TEntity> Select => repository.Select;

        public IUpdate<TEntity> UpdateDiy => repository.UpdateDiy;

        public Type EntityType => repository.EntityType;

        public IUnitOfWork UnitOfWork { get => repository.UnitOfWork; set => repository.UnitOfWork = value; }

        public IFreeSql Orm => repository.Orm;

        public DbContextOptions DbContextOptions { get => repository.DbContextOptions; set => repository.DbContextOptions = value; }

        public bool Any(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false)
        {
            return repository.Any(whereExpression, ignoreQueryFilters);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false)
        {
            return repository.AnyAsync(whereExpression, ignoreQueryFilters);
        }

        public ISelect<TEntity> AsQueryable()
        {
            return repository.AsQueryable();
        }

        public void AsTable(Func<string, string> rule)
        {
            repository.AsTable(rule);
        }

        public void AsType(Type entityType)
        {
            repository.AsType(entityType);
        }

        public void Attach(TEntity entity)
        {
            repository.Attach(entity);
        }

        public void Attach(IEnumerable<TEntity> entity)
        {
            repository.Attach(entity);
        }

        public IBaseRepository<TEntity> AttachOnlyPrimary(TEntity data)
        {
            return repository.AttachOnlyPrimary(data);
        }

        public void BeginEdit(List<TEntity> data)
        {
            repository.BeginEdit(data);
        }

        public IFreeSqlRepository<TChangeEntity, long> Change<TChangeEntity>() where TChangeEntity : class, new()
        {
            return repository.Change<TChangeEntity>();
        }

        public IFreeSqlRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>() where TChangeEntity : class, new()
        {
            return repository.Change<TChangeEntity, TChangeKey>();
        }

        public long Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return repository.Count(whereExpression);
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return repository.CountAsync(whereExpression);
        }

        public int Delete(TKey id)
        {
            return repository.Delete(id);
        }

        public int Delete(TEntity entity)
        {
            return repository.Delete(entity);
        }

        public int Delete(IEnumerable<TEntity> entitys)
        {
            return repository.Delete(entitys);
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Delete(predicate);
        }

        public Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return repository.DeleteAsync(id, cancellationToken);
        }

        public Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return repository.DeleteAsync(entity, cancellationToken);
        }

        public Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            return repository.DeleteAsync(entitys, cancellationToken);
        }

        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return repository.DeleteAsync(predicate, cancellationToken);
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public int EndEdit(List<TEntity> data = null)
        {
            return repository.EndEdit(data);
        }

        public TEntity Find(TKey id)
        {
            return repository.Find(id);
        }

        public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return repository.FindAsync(id, cancellationToken);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return repository.FirstOrDefaultAsync(whereExpression);
        }

        public void FlushState()
        {
            repository.FlushState();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> exp)
        {
            return repository.Get(exp);
        }

        public TDto Get<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return repository.Get<TDto>(exp);
        }

        public TEntity Get(TKey id)
        {
            return repository.Get(id);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return repository.GetAsync(exp);
        }

        public Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return repository.GetAsync<TDto>(exp);
        }

        public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return repository.GetAsync(id, cancellationToken);
        }

        public TEntity Insert(TEntity entity)
        {
            return repository.Insert(entity);
        }

        public List<TEntity> Insert(IEnumerable<TEntity> entitys)
        {
            return repository.Insert(entitys);
        }

        public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return repository.InsertAsync(entity, cancellationToken);
        }

        public Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            return repository.InsertAsync(entitys, cancellationToken);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return repository.InsertOrUpdate(entity);
        }

        public Task<TEntity> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return repository.InsertOrUpdateAsync(entity, cancellationToken);
        }

        public void SaveMany(TEntity entity, string propertyName)
        {
            repository.SaveMany(entity, propertyName);
        }

        public Task SaveManyAsync(TEntity entity, string propertyName, CancellationToken cancellationToken = default)
        {
            return repository.SaveManyAsync(entity, propertyName, cancellationToken);
        }

        public TEntity Single(dynamic id)
        {
            return repository.Single(id);
        }

        public Task<TEntity> SingleAsync(dynamic id)
        {
            return repository.SingleAsync(id);
        }

        public bool SoftDelete(TKey id)
        {
            return repository.SoftDelete(id);
        }

        public bool SoftDelete(Expression<Func<TEntity, bool>> exp)
        {
            return repository.SoftDelete(exp);
        }

        public Task<bool> SoftDeleteAsync(TKey id)
        {
            return repository.SoftDeleteAsync(id);
        }

        public Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp)
        {
            return repository.SoftDeleteAsync(exp);
        }

        public int Update(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return repository.Update(entity, ignoreNullValues, ignoreQueryFilters);
        }

        public int Update(TEntity entity)
        {
            return ((IBaseRepository<TEntity>)repository).Update(entity);
        }

        public int Update(IEnumerable<TEntity> entitys)
        {
            return repository.Update(entitys);
        }

        public Task<int> UpdateAsync(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return repository.UpdateAsync(entity, ignoreNullValues, ignoreQueryFilters);
        }

        public Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return repository.UpdateAsync(entity, cancellationToken);
        }

        public Task<int> UpdateAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            return repository.UpdateAsync(entitys, cancellationToken);
        }

        public int UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return repository.UpdateExclude(entity, propertyNames, ignoreNullValues, ignoreQueryFilters);
        }

        public Task<int> UpdateExcludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return repository.UpdateExcludeAsync(entity, propertyNames, ignoreNullValues, ignoreQueryFilters);
        }

        public int UpdateInclude(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return repository.UpdateInclude(entity, columns, ignoreQueryFilters);
        }

        public Task<int> UpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return repository.UpdateIncludeAsync(entity, columns, ignoreQueryFilters);
        }

        public ISelect<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return repository.Where(condition, predicate, ignoreQueryFilters);
        }

        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return repository.Where(predicate, ignoreQueryFilters);
        }

        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> exp)
        {
            return ((IBaseRepository<TEntity>)repository).Where(exp);
        }

        public ISelect<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> exp)
        {
            return repository.WhereIf(condition, exp);
        }
    }
}
