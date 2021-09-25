﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoEmmyflix.Exceptions
{
    public class SerieNaoCadastradaException : Exception
    {
        public SerieNaoCadastradaException()
            : base("Esta série não está cadastrada.")
        { }
    }
}

