using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_entityframework.Modelos
{
    public class Tarea
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public Prioridad Prioridad { get; set; }

        public DateTime FechaCreacion { get; set;}

        public virtual Categoria Categoria { get; set; }

        public string Resumen { get; set; } 

    }
}
