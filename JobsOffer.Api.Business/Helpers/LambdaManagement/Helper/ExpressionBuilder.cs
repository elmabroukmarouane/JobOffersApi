using JobsOffer.Api.Business.Helpers.LambdaManagement.Models;
using System.Linq.Expressions;

namespace JobsOffer.Api.Business.Helpers.LambdaManagement.Helper
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildLambda<T>(LambdaExpressionModel model)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var body = BuildGroupExpression<T>(model.RootGroup, parameter);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression BuildGroupExpression<T>(ConditionGroupModel group, ParameterExpression parameter)
        {
            Expression? combined = null;

            if (group.Conditions != null && group.Conditions.Any())
            {
                foreach (var condition in group.Conditions)
                {
                    var property = Expression.Property(parameter, condition.PropertyName);
                    var comparisonValue = Expression.Constant(Convert.ChangeType(condition.ComparisonValue, property.Type));

                    BinaryExpression comparison = condition.ComparisonType switch
                    {
                        "Equal" => Expression.Equal(property, comparisonValue),
                        "NotEqual" => Expression.NotEqual(property, comparisonValue),
                        "GreaterThan" => Expression.GreaterThan(property, comparisonValue),
                        "GreaterThanOrEqual" => Expression.GreaterThanOrEqual(property, comparisonValue),
                        "LessThan" => Expression.LessThan(property, comparisonValue),
                        "LessThanOrEqual" => Expression.LessThanOrEqual(property, comparisonValue),
                        _ => throw new NotSupportedException($"Comparison type '{condition.ComparisonType}' is not supported.")
                    };

                    combined = combined == null ? comparison : CombineExpressions(combined, comparison, group.LogicalOperator);
                }
            }

            if (group.Groups != null && group.Groups.Any())
            {
                foreach (var subgroup in group.Groups)
                {
                    var subgroupExpression = BuildGroupExpression<T>(subgroup, parameter);
                    combined = combined == null ? subgroupExpression : CombineExpressions(combined, subgroupExpression, group.LogicalOperator);
                }
            }

            return combined ?? Expression.Constant(true);
        }

        private static Expression CombineExpressions(Expression left, Expression right, string logicalOperator) => logicalOperator switch
        {
            "AND" => Expression.AndAlso(left, right),
            "OR" => Expression.OrElse(left, right),
            _ => throw new NotSupportedException($"Logical operator '{logicalOperator}' is not supported.")
        };
    }
}
