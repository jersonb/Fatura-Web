
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaturaWeb.Models
{
    public class Fatura : Entidade
    {
        public Fatura()
        {
            this.Itens = new List<Item>();
            
        }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [DisplayName("Número")]
        public int Numero { get; set; }

        public virtual Cliente Emissor { get; set; }

        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        [Column(TypeName = "Date")]
        [DisplayName("Data de Emissão")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEmissao { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Empenho { get; set; }

        [DisplayName("Valor Total")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false, NullDisplayText = "R$ 0,00")]
        public decimal ValorTotal { get; set; }

        public virtual List<Item> Itens { get; set; }

        [DisplayName("Tempo de Prestação")]
        public string TempoPrestacao { get; set; }

        public string Observacao { get; set; }
    }
}
