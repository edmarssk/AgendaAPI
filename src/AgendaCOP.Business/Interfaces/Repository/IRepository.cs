using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Repository
{
    public interface IRepository<TEntity>: IDisposable where TEntity : Entity
    {
        Task<List<TEntity>> BuscarTodos();

        Task<TEntity> BuscarPorId(Guid id);

        Task Adicionar(TEntity obj);

        Task Atualizar(TEntity obj);

        Task Remover(Guid id);

        Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
