using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FreeSql
{
    public abstract class FreeSqlEntitySeedData<TEntity> : IEntitySeedData,IEntitySeedData<TEntity> where TEntity:class, new()
    {
        public virtual void SetData(IFreeSql freeSql)
        {
            if (!freeSql.Select<TEntity>().Any())
            {
                var data = HasData();
                if (data?.Count()>0)
                {
                    freeSql.Insert<TEntity>(data).ExecuteAffrows();
                }
            }
        }

        public abstract IEnumerable<TEntity> HasData();
    }
}
