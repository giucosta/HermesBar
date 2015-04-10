﻿using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Cliente
{
    public class ClienteModel : IModel
    {
        public string Nome { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
    }
}
