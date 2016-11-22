using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Utilities
{
    public static class ExpressionHelper
    {

        public static bool IsValueAStringConstant(LambdaExpression expression)
        {
            if (expression == null)
                return false;
            if (expression.Body.NodeType == ExpressionType.Constant)
                return true;
            return false;
        }
        
        public static  string GetStringConstant(LambdaExpression expression)
        {
            if (expression == null)
                return null;
            if (expression.Body.NodeType == ExpressionType.Constant && expression.Body.Type == typeof(string))
            {
                ConstantExpression ce = (ConstantExpression)expression.Body;
                return (string)ce.Value;
            }
            throw new InvalidCastException("Not a Constant Expression");
        }

        public static bool IsValueAModelRefererence(LambdaExpression expression)
        {
            if (expression == null)
                return false;
            var e = expression.ToString();
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
                return true;
            return false;
        }

        public static bool IsEnumerationModelRefererence(LambdaExpression expression)
        {
            if (expression == null)
                return false;
            if (expression.Body.NodeType == ExpressionType.MemberAccess && 
                expression.Body.Type.IsInstanceOfType(typeof(IEnumerable)) == true)
                return true;
            return false;
        }

        public static string GetMemberReference(LambdaExpression expression)
        {
            if (expression == null)
                return null;
            
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression current = expression.Body as MemberExpression;
                var e = current.ToString();
                var stringResult = "";
                var separator = "";
                while (current != null)
                {
                    stringResult = current.Member.Name + separator + stringResult;
                    if (current.Member.Name == "Model")
                    {
                        return   stringResult; 
                    }
                    separator = ".";
                    if (current.Expression is MethodCallExpression)
                    {
                        var methodCall = current.Expression as MethodCallExpression;
                        if (methodCall.Method.Name == "Current")
                        {
                            stringResult = "~" + separator + stringResult;
                        }
                        current = methodCall.Arguments[0] as MemberExpression;
                    }
                    else
                    {
                        current = current.Expression as MemberExpression;
                    }
                }
            }
            throw new InvalidCastException("Not a Model Expression");
        }

    }
}
