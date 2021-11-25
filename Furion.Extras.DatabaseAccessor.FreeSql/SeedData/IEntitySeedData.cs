using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public interface IEntitySeedData
    {
        void SetData(IFreeSql freeSql);
    }
    public interface IEntitySeedData<TEntity>
    {
        IEnumerable<TEntity> HasData();
    }
}
