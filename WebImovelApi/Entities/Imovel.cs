using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebImovelApi.Entities
{
    public class Imovel
    {
        [Key]
        public int ImovelId { get; set; }

        [Required]
        public int Cep { get; set; }

        [Required]
        [Display(Name = "Valor Da Diária")]  
        public double ValorDiaria { get; set; }

        [Required]
        [Display(Name = "Locador")]
        public int LocadorId { get; set; }

        [JsonIgnore]
        public Locador Locador { get; set; }
    }
}
