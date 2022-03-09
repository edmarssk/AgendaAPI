using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Models;
using AgendaCOP.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace AgendaCOP.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {

        protected MainDbContext Db;

        protected DbSet<TEntity> DbSet;

        public Repository(MainDbContext mainDbContext)
        {
            this.Db = mainDbContext;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task Adicionar(TEntity obj)
        {
            //mainDbContext.Set<TEntity>().Add(obj);
            //Melhor maneira
            DbSet.Add(obj);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
            await SaveChanges();
        }

        public virtual async Task<TEntity> BuscarPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> BuscarTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(await this.BuscarPorId(id));
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
