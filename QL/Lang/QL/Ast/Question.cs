﻿using QL.Lang.QL.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Lang.QL.Ast
{
    public class Question : Statement
    {
        public BaseType Type { get; set; }

        public string Text { get; set; }

        public string Id { get; set; }
    }
}
