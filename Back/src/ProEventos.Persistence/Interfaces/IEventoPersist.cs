using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersist
    {
        //Eventos
         Task<Evento[]> GetAllEventosBytemaAsync(string tema, bool includePalestrantes); 
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

    }
}