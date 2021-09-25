﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoEmmyflix.Exceptions
{
    public class SerieJaCadastradaException : Exception
    {
      public SerieJaCadastradaException() 
            : base("Esta série já está cadastrada.")
        { }
    }
}
