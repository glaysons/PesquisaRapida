using PesquisaRapida.Pattern.Exceptions;
using System.Linq.Expressions;
using System.Reflection;

namespace PesquisaRapida.Pattern.Helpers
{
	public static class ExpressionHelper
	{

		public static PropertyInfo PropriedadeDaExpressao(LambdaExpression campo)
		{
			var prop = campo.Body as MemberExpression;
			if (prop != null)
				return (PropertyInfo)prop.Member;
			var body = campo.Body as UnaryExpression;
			if (body == null)
				throw new PropriedadeInvalidaException();
			return (PropertyInfo)((MemberExpression)body.Operand).Member;
		}

	}
}
