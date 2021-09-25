using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoEmmyflix.InputModel
{
    public class SerieInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "O nome da série deve ter entre 1 e 200 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O gênero da série deve ter entre 1 e 100 caracteres.")]
        public string Genero { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "A premiação da série deve ter entre 1 e 100 caracteres.")]
        public string Premio { get; set; }

        [Required]
        [Range(3, 12, ErrorMessage = "O número de temporadas da série deve ser entre 1 e 30.")]
        public int NumeroTemporadas { get; set; }
    }
}
