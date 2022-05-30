using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebImovelApi.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1})")]
        public string Name { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Numero do  Imovel")]
        public int ImovelId { get; set; }

        [JsonIgnore]
        public Imovel Imovel { get; set; }

    }
}
