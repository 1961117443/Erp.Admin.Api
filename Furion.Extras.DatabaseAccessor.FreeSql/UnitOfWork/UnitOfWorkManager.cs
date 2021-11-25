using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public class UnitOfWorkManager<TDbLocator> : UnitOfWorkManager
    {
        public UnitOfWorkManager(IFreeSql<TDbLocator> fsql):base(fsql)
        {

        }
    }
}
