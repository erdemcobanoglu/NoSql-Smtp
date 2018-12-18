using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    interface IBusinessLayer<TEntity> where TEntity:class
    {
        bool Insert(TEntity item);
        bool Update(TEntity item);
        bool Delete(TEntity item);
        List<TEntity> GetAll();
        TEntity GetById(TEntity item);
    }
}
