using Microsoft.EntityFrameworkCore;
using Simav.Core;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDBContext _context;
        private DbSet<TEntity> _dbset;

        public Repository(AppDBContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }


        public int Delete(int id)
        {
            var entity = _dbset.Find(id);
            _dbset.Remove(entity);
            return _context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbset.Remove(entity);
            return _context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public List<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbset.Find(id);
        }

        public int Save(TEntity entity)
        {
            //reflection işlemi denir
            if (entity.GetType().Name == "Kullanici")
            {
                //property'ler kontrol edilecek ve property'lerden Parola olan varsa şifrelenecek  
            }
            _dbset.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
