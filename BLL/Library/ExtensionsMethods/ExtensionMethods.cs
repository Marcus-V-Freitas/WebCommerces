using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Library.ExtensionsMethods
{
    public static class ExtensionMethods
    {

        public static bool Contains(this string input, string findMe, StringComparison comparisonType)
        {
            return string.IsNullOrWhiteSpace(input) ? false : input.IndexOf(findMe, comparisonType) > -1;
        }

        public static void Use<T>(this T item, Action<T> work)
        {
            work(item);

        }

        public static async Task With<T>(this T value, Func<T, Task> action)
        {
            await action(value);
        }

        public static string TratarExececao(this Exception ex, string mensagemComplementar = "")
        {
            if (ex.InnerException != null)
            {
                return $"{ex.InnerException.MetodoExcecao()} Horário:{DateTime.Now} {mensagemComplementar}";
            }
            return $"{ex.MetodoExcecao()} Horário:{DateTime.Now} {mensagemComplementar}";
        }

        public static string MetodoExcecao(this Exception ex)
        {
            var frames = new StackTrace(ex);
            return frames.GetFrames().Aggregate(new StringBuilder(), (current, next) => current.AppendLine($"Método:{next.GetMethod().Name}")).ToString();
        }

        public static string GetDisplayName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression) where TSource : class
        {
            var attribute = Attribute.GetCustomAttribute(((MemberExpression)expression.Body).Member, typeof(DisplayNameAttribute)) as DisplayNameAttribute;

            if (attribute == null)
            {
                return "";
            }

            return attribute.DisplayName;
        }
    }
}
