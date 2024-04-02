using System;

namespace Evaluator
{
    static class Constants
    {
        public const string MathPattern = @"(?:(?:(?:(?:sqrt|floor|ceil|round|cos|sin|tan)\(\d+\))|pi|\d+)\s*[-+*\/^%]\s*)+(?:(?:(?:sqrt|floor|ceil|round|cos|sin|tan)\(\d+\))|pi|\d+)";
        public const string ConditionalOperatorPattern = @"\s*(.*\S+)\s*\?\s*(.*\S+)\s*:\s*(.*\S+)";
        public const string ComparisonOperatorPattern = @"(\w+)\s*(<|>|<=|>=)\s*(\w+)";
        public const string EqualityOperatorPattern = @"(-?\w+)\s*(==|!=)\s*(-?\w+)";
        public const string BooleanLogicalOperatorPattern = @"(true|false)\s*([&^|])\s*(true|false)";
        public const string ConditionalAndOrOperatorPattern = @"(true|false)\s*(&&|\|\|)\s*(true|false)";
    }
}