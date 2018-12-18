using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    interface IDataMapper<TEntity>
    {
        bool Insert (TEntity item);
        bool Update(TEntity item);
        bool Delete(TEntity item);
        List<TEntity> GetAll();
        TEntity GetById(TEntity item);
    }
}
