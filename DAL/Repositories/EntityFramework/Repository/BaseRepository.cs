using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly WebContext _context;
        protected readonly DbSet<T> _entity;

        public BaseRepository(WebContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        public async Task Adicionar(T Entity)
        {
            _entity.Add(Entity);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, T Entity)
        {
            var produtoBD = await ObterId(id);

            _context.Entry(produtoBD).CurrentValues.SetValues(Entity);
            _context.Entry(produtoBD).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<T> ObterId(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _entity.ToListAsync();
        }

        public async Task Remover(int id)
        {
            T entity = await ObterId(id);
            _entity.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
