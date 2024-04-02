using System;
using Evaluator;

namespace Evaluator
{
    public partial class Evaluator
    {
        public static bool EvalExpression(string expression)
        {
            return Processors.ExpressionProcessing(expression);
        }
    }
}