using PesquisaRapida.Pattern.Estrutura;
using System;
using System.Collections.Generic;

namespace PesquisaRapida.Pattern
{
	public abstract class Configuracao<TResultado> : IConfiguracao
	{

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
				if (ResultadoPersonalizado == null)
					yield break;
				foreach (var item in ResultadoPersonalizado)
					yield return item;
			}
			set
			{
			}
		}

		public abstract IList<ResultadoPersonalizado<TResultado>> ResultadoPersonalizado { get; }

		public virtual IList<Dependente> Dependentes { get; }

		public virtual string Condicao { get; }

		public virtual string Relacionamento { get; }

		public virtual string Ordem { get; }

	}
}
