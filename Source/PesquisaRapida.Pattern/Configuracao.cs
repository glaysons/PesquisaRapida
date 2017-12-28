using PesquisaRapida.Pattern.Estrutura;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PesquisaRapida.Pattern
{
	public abstract class Configuracao : IConfiguracao
	{

		private IEnumerable<IResultadoPersonalizado> _resultado;

		public Type TipoObjeto
		{
			get { return typeof(CodigoResultado); }
		}

		public abstract string TituloConfiguracao { get; }

		public abstract string Tabela { get; }

		public abstract string CampoChave { get; }

		public virtual TipoCampos TipoChave { get; } = TipoCampos.Integer;

		public abstract string CampoResultado { get; }

		public virtual string TituloResultado { get; } = "Nome";

		public IEnumerable<IResultadoPersonalizado> Resultado
		{
			get { return _resultado ?? (_resultado = CriarResultadoPadrao()); }
		}

		public virtual IList<Dependente> Dependentes { get; }

		public virtual string Condicao { get; }

		public virtual string Relacionamento { get; }

		public virtual string Ordem { get; }

		private List<IResultadoPersonalizado> CriarResultadoPadrao()
		{
			var resultado = new List<ResultadoPersonalizado<CodigoResultado>>();
			resultado.Add(new ResultadoPersonalizado<CodigoResultado>(c => c.Codigo, CampoChave, chave: true));
			resultado.Add(new ResultadoPersonalizado<CodigoResultado>(c => c.Codigo, CampoResultado, resultado: true));
			return resultado.Cast<IResultadoPersonalizado>().ToList();
		}

	}
}
