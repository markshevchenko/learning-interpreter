﻿namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class ArrayVariable : ScalarVariable
    {
        public IReadOnlyList<IExpression> Indexes { get; private set; }

        public ArrayVariable(string name, IReadOnlyList<IExpression> indexes)
            : base(name)
        {
            Indexes = indexes;
        }

        public override Expression Compile(IRunTimeEnvironment rte)
        {
            var array = base.Compile(rte);
            var indexes = Indexes.Select(i => i.Compile(rte))
                                 .ToArray();
            return Expression.ArrayAccess(array, indexes);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", base.ToString(), string.Join(", ", Indexes));
        }
    }
}