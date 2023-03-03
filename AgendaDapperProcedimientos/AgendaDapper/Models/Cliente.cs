//using System.ComponentModel.DataAnnotations;

using Dapper.Contrib.Extensions;

namespace AgendaDapperProcedimientos.Models
{
    [Table("Cliente")] //usado para el uso de dapper contrib - importar Dapper.Contrib.Extensions  - cliente "nombre de la tabla en la base de datos"
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
