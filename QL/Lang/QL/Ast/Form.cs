﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Lang.QL
{
    public class Form
    {
        public string Name { get; set; }
        public IList<Statement> Statements { get; set; }
    }
}