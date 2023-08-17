using System.ComponentModel.DataAnnotations;

namespace proyecto_entityframework.Modelos
{
    public class Categoria
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set;}

        public ICollection<Tarea> Tareas { get; set; }

        public int Peso { get; set; }

    }
}
