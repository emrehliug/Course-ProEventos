using System.Threading.Tasks;
using ProEventos.Persistence.DataContext;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        //Injetando o Context
        private readonly ProEventosContext DataContext;
        public GeralPersist(ProEventosContext context)
        {
            DataContext = context;
        }

        //Crud
        public void Add<T>(T entity) where T : class
        {
            DataContext.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            DataContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            DataContext.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            DataContext.Remove(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await DataContext.SaveChangesAsync()) > 0;
        }
    }
}