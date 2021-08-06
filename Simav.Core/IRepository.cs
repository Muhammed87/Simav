using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Save(TEntity entity);
        int Update(TEntity entity);
        TEntity Find(Expression<Func<TEntity, bool>> where);//Find(x=> x.Id==id && x.Durum==1)
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> where);
        List<TEntity> GetAll(); //ListAll
        TEntity GetById(int id);
        int Delete(int id);
        int Delete(TEntity entity);
    }
}
