using Microsoft.EntityFrameworkCore;
using proyecto_entityframework.Modelos;

namespace proyecto_entityframework
{
    public class ProyectoContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<Categoria> categoriasIniciales = new List<Categoria>();
            categoriasIniciales.Add(new Categoria() { 
                Id = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), 
                Nombre = "Pending activities", 
                Peso = 20 });
            categoriasIniciales.Add(new Categoria() {
                Id = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), 
                Nombre = "Personal activities", 
                Peso = 50 });

            List<Tarea> tareasIniciales = new List<Tarea>();
            tareasIniciales.Add(new Tarea()
            {
                Id = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"),
                CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"),
                Prioridad = Prioridad.Media,
                Titulo = "Payment of public services",
                FechaCreacion = DateTime.Now
            });
            tareasIniciales.Add(new Tarea()
            {
                Id = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"),
                CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"),
                Prioridad = Prioridad.Baja,
                Titulo = "Finish watching movie",
                FechaCreacion = DateTime.Now
            });

            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(categoria => categoria.Id);
                categoria.Property(categoria => categoria.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(categoria => categoria.Descripcion).IsRequired(false);
                categoria.Property(categoria => categoria.Peso);

                categoria.HasData(categoriasIniciales);
            });

            modelBuilder.Entity<Tarea>(tarea=> {

                tarea.ToTable("Tarea");
                tarea.HasKey(tarea => tarea.Id);
                tarea.HasOne(tarea => tarea.Categoria).WithMany(tarea => tarea.Tareas).HasForeignKey(tarea => tarea.CategoriaId);
                tarea.Property(tarea => tarea.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(tarea => tarea.Descripcion).IsRequired(false);
                tarea.Property(tarea => tarea.Prioridad);
                tarea.Property(tarea => tarea.FechaCreacion);
                tarea.Ignore(tarea => tarea.Resumen);

                tarea.HasData(tareasIniciales);

            });
        }

    }
}
