using System.ComponentModel.DataAnnotations;

namespace FaturaWeb.Models
{
    public abstract class Entidade
    {
        [Key]
        public int Id { get; set; }
    }
}