using System.ComponentModel.DataAnnotations;

namespace FaturaWeb.Basica
{
    public  class Banco
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Agencia { get; set; }

        [Required]
        public string Conta { get; set; }
    }
}
