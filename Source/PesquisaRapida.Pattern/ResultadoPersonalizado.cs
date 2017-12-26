using PesquisaRapida.Pattern.Estrutura;
using PesquisaRapida.Pattern.Exceptions;
using PesquisaRapida.Pattern.Helpers;
using System;
using System.Linq.Expressions;

namespace PesquisaRapida.Pattern
{
	public class ResultadoPersonalizado<T> : IResultadoPersonalizado
	{

		public string Campo { get; }

		public string Titulo { get; }

		public string ExpressaoSql { get; }

		public bool ColunaVisivel { get; }

		public bool Chave { get; }

		public bool Resultado { get; }

		public ResultadoPersonalizado(Expression<Func<T, object>> campo, string expressaoSql, bool chave = false, bool resultado = false)
		{
			Campo = ConsultarNomeDoCampo(campo);
			Titulo = string.Empty;
			ExpressaoSql = expressaoSql;
			ColunaVisivel = false;
			Chave = chave;
			Resultado = resultado;
		}

		private string ConsultarNomeDoCampo(Expression<Func<T, object>> campo)
		{
			var propriedade = ExpressionHelper.PropriedadeDaExpressao(campo);
			if (propriedade == null)
				throw new CampoInvalidoException();
			return propriedade.Name;
		}

		public ResultadoPersonalizado(Expression<Func<T, object>> campo, string titulo, string expressaoSql, bool chave = false, bool resultado = false)
		{
			Campo = ConsultarNomeDoCampo(campo);
			Titulo = titulo;
			ExpressaoSql = expressaoSql;
			ColunaVisivel = true;
			Chave = chave;
			Resultado = resultado;
		}

	}
}
