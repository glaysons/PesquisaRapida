using PesquisaRapida.Pattern.Estrutura;
using PesquisaRapida.Pattern.Exceptions;
using PesquisaRapida.Pattern.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PesquisaRapida.Pattern
{
	public abstract class Configuracao<TResultado> : IConfiguracao
	{

		private IEnumerable<IResultadoPersonalizado> _resultado;

		public Type TipoObjeto
		{
			get { return typeof(TResultado); }
		}

		public abstract string TituloConfiguracao { get; }

		public abstract string Tabela { get; }

		public virtual TipoCampos TipoChave { get; } = TipoCampos.Integer;

		public virtual string TituloResultado { get; } = "Nome";

		public IEnumerable<IResultadoPersonalizado> Resultado
		{
			get
			{
				if (_resultado == null)
					CriarResultadoPadrao();
				return _resultado;
			}
		}

		public virtual string Condicao { get; }

		public virtual string Relacionamento { get; }

		public virtual string Ordem { get; }

		private void CriarResultadoPadrao()
		{
			_resultado = new List<ResultadoPersonalizado<TResultado>>();
			PersonalizarResultadoPadrao();
		}

		protected abstract void PersonalizarResultadoPadrao();

		protected void AdicionarCampo(Expression<Func<TResultado, object>> campo, string titulo, string expressaoSql, bool chave = false, bool resultado = false)
		{
			var lista = (List<ResultadoPersonalizado<TResultado>>)_resultado;
			lista.Add(new ResultadoPersonalizado<TResultado>(ConsultarNomeDoCampo(campo), titulo, expressaoSql, colunaVisivel: true, chave: chave, resultado: resultado));
		}

		private string ConsultarNomeDoCampo(Expression<Func<TResultado, object>> campo)
		{
			var propriedade = ExpressionHelper.PropriedadeDaExpressao(campo);
			if (propriedade == null)
				throw new CampoInvalidoException();
			return propriedade.Name;
		}

		protected void AdicionarCampoOculto(Expression<Func<TResultado, object>> campo, string expressaoSql, bool chave = false, bool resultado = false)
		{
			var lista = (List<ResultadoPersonalizado<TResultado>>)_resultado;
			lista.Add(new ResultadoPersonalizado<TResultado>(ConsultarNomeDoCampo(campo), null, expressaoSql, colunaVisivel: false, chave: chave, resultado: resultado));
		}

	}
}
