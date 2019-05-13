using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaturaWeb.Models
{
    public class Cliente : Entidade
    {
        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "char(14)")]
        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000-00}", ApplyFormatInEditMode = true)]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "char(14)")]
        public string InscMunicipal { get; set; }

        [Column(TypeName = "char(12)")]
        public string Fone { get; set; }

        [Column(TypeName = "varchar(120)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(120)")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "char(2)")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(10)")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "char(9)")]
        [DisplayFormat(DataFormatString = "{0:00\\.000\\-000}", ApplyFormatInEditMode = true)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(120)")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(50)")]
        public string NomeBanco { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(20)")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "varchar(20)")]
        public string Conta { get; set; }


        public override string ToString()
        {
            return Nome;
        }

       
    }
}