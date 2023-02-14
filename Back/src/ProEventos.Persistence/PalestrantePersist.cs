using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.DataContext;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        //Injetando o Context
        private readonly ProEventosContext DataContext;
        public PalestrantePersist(ProEventosContext context)
        {
            DataContext = context;
            DataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEvento)
        {
            IQueryable<Palestrante> query = DataContext.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEvento)
            {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(p => p.Evento);
            }
            
            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEvento)
        {
            IQueryable<Palestrante> query = DataContext.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEvento)
            {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(p => p.Evento);
            }
            
            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEvento)
        {
            IQueryable<Palestrante> query = DataContext.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEvento)
            {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(p => p.Evento);
            }
            
            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}