using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebImovelApi.Entities
{
    public class Locador
    {
        [Key]
        public int LocadorId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1})")]
        public string Name { get; set; }

        [Required]
        public int Telefone { get; set; }

        [JsonIgnore]
        [Display(Name = "Seu Imovel")]
        public ICollection<Imovel> Imovels { get; set; } = new List<Imovel>();
    }
}
