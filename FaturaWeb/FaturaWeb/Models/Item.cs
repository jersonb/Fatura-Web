using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FaturaWeb.Models
{
    public class Item : Entidade
    {
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Este campo é Obrigatério")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatério")]
        public int Quantidade { get; set; }

        [DisplayName("Valor Unitário")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false, NullDisplayText = "R$ 0,00")]
        [Required(ErrorMessage = "Este campo é Obrigatório")]
        public decimal Valor { get; set; }

    }
}
