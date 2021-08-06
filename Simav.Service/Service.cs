using Simav.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simav.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public int Delete(int id)
        {
            return _repository.Delete(id);
        }

        public int Delete(TEntity entity)
        {
            return _repository.Delete(entity);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return _repository.Find(where);
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> where)
        {
            return _repository.FindAll(where);
        }

        public List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int Save(TEntity entity)
        {
            return _repository.Save(entity);
        }

        public int Update(TEntity entity)
        {
            return _repository.Update(entity);
        }
    }
}
