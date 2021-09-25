using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoEmmyflix.Entities
{
    public class Serie
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Premio { get; set; }
        public int NumeroTemporadas { get; set; }
    }
}
