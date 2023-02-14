using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventoPersist eventoPersist;
        public EventoService(IGeralPersist geral, IEventoPersist evento)
        {
            geralPersist = geral;
            eventoPersist = evento;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
           try
           {
               geralPersist.Add(model);
               if(await geralPersist.SaveChangesAsync())
               {
                   return await eventoPersist.GetEventoByIdAsync(model.Id, false);
               }
               return null;
           }
           catch (Exception ex)
           {
                throw new Exception(ex.Message);
           }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) return null;

                model.Id = eventoId;

                geralPersist.Update(model);
                if(await geralPersist.SaveChangesAsync())
                {
                   return await eventoPersist.GetEventoByIdAsync(model.Id, false);
                }

               return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
           try
           {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) throw new Exception("Evento para delete não encontrado.");
               
                geralPersist.Delete<Evento>(evento);
                return await geralPersist.SaveChangesAsync();
                
           }
           catch (Exception ex)
           {
                throw new Exception(ex.Message);
           }
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetAllEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(EventoId, includePalestrantes);
                if(evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosBytemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosBytemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}