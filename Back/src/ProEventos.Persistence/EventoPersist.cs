using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.DataContext;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        //Injetando o Context
        private readonly ProEventosContext DataContext;
        public EventoPersist(ProEventosContext context)
        {
            DataContext = context;
            DataContext.ChangeTracker.QueryTrackingBehavior =  QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante)
        {
            IQueryable<Evento> query = DataContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrante)
            {
                query = query.Include(e => e.PalestranteEventos)
                             .ThenInclude(e => e.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = DataContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrante)
            {
                query = query.Include(e => e.PalestranteEventos)
                             .ThenInclude(e => e.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosBytemaAsync(string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = DataContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(includePalestrante)
            {
                query = query.Include(e => e.PalestranteEventos)
                             .ThenInclude(e => e.Palestrante);
            }
            
            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}