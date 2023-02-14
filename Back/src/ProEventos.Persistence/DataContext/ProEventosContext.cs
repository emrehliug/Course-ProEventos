using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.DataContext
{
    public class ProEventosContext : DbContext
    {
        //Enviando minha conexão SQL para o constructor do DbContext
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {
            
        }
        //Objetos para criação de dados no SQL
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new{PE.EventoId, PE.PalestranteId});
            
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);                      
        }

    }
}