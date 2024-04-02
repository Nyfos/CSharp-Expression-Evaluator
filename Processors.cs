using System;
using System.Text.RegularExpressions;
using Evaluator;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using UnityEngine;

namespace Evaluator
{
    public class Processors
    {
        public static bool ExpressionProcessing(string encodedExpression)
        {
            string expression = encodedExpression;
            string err = "d'analyser l'op�rateur conditionnel (c?t:f) de";
            try
            {
                // Conditional
                expression = Regex.Replace(expression, Constants.ConditionalOperatorPattern, m =>
                {
                    return ExpressionProcessing(m.Groups[1].Value) ? m.Groups[2].Value : m.Groups[3].Value;
                });
                err = "d'analyser les expressions math�matiques de";

                // Math
                expression = Regex.Replace(expression, Constants.MathPattern, m => {
                    ExpressionEvaluator.Evaluate(m.Groups[0].Value, out float result);
                    return result.ToString();
                });
                err = "d'analyser les op�rateurs de comparaison de";

                // Comparison
                expression = Regex.Replace(expression, Constants.ComparisonOperatorPattern, m =>
                {
                    return ComparisonProcessing(float.Parse(m.Groups[1].Value), m.Groups[2].Value, float.Parse(m.Groups[3].Value)).ToString();
                });
                err = "d'analyser les op�rateurs d'�galit� de";

                // Equality
                expression = Regex.Replace(expression, Constants.EqualityOperatorPattern, m =>
                {
                    return EqualityProcessing(float.Parse(m.Groups[1].Value), m.Groups[2].Value, float.Parse(m.Groups[3].Value)).ToString();
                });
                err = "d'analyser les op�rateurs bool�ens logiques de";

                // Boolean Logical
                expression = Regex.Replace(expression, Constants.BooleanLogicalOperatorPattern, m =>
                {
                    return BooleanLogicalProcessing(m.Groups[1].Value == "true", m.Groups[2].Value, m.Groups[3].Value == "true").ToString();
                });
                err = "d'analyser les op�rateurs conditionnels AND/OR (&&/||) de";

                // Conditionnal AND/OR
                expression = Regex.Replace(expression, Constants.ConditionalAndOrOperatorPattern, m =>
                {
                    return ConditionalAndOrProcessing(m.Groups[1].Value == "true", m.Groups[2].Value, m.Groups[3].Value == "true").ToString();
                });
                err = "de terminer le d�codage de";
            }
            catch (Exception ex)
            {
                Debug.LogError("Impossible " + err + " l'expression :\n" + ex.Message + "\nExpression initiale: " + encodedExpression + "\nDernier �tat de d�codage de l'expression: " + expression);
                return false;
            }

            return expression == "True";
        }

        public static bool ComparisonProcessing(float valueA, string relationalOperator, float valueB)
        {
            if (relationalOperator == "<") return valueA < valueB;
            if (relationalOperator == ">") return valueA > valueB;
            if (relationalOperator == "<=") return valueA <= valueB;
            /*if (relationalOperator == ">=") */return valueA >= valueB;
        }

        public static bool EqualityProcessing(float valueA, string equalityOperator, float valueB)
        {
            if (equalityOperator == "==") return valueA == valueB;
            /*if (equalityOperator == "!=") */return valueA != valueB;
        }

        public static bool BooleanLogicalProcessing(bool valueA, string logicalOperator, bool valueB)
        {
            if (logicalOperator == "&") return valueA & valueB;
            if (logicalOperator == "^") return valueA ^ valueB;
            /*if (logicalOperator == "|") */return valueA | valueB;
        }

        public static bool ConditionalAndOrProcessing(bool valueA, string logicalOperator, bool valueB)
        {
            if (logicalOperator == "&&") return valueA && valueB;
            /*if (logicalOperator == "||") */return valueA || valueB;
        }
    }
}