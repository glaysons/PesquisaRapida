using PesquisaRapida.Pattern.Estrutura;
using System.Collections.Generic;

namespace PesquisaRapida.Pattern
{
	public abstract class Configuracao<TResultado> : IConfiguracao
	{

		private IEnumerable<IResultadoPersonalizado> _resultado = null;

		public abstract string TituloConfiguracao { get; }

		public abstract int Rotina { get; }

		public abstract string Tabela { get; }

		public abstract string CampoChave { get; }

		public virtual TipoCampos TipoChave { get; } = TipoCampos.Integer;

		public abstract string CampoResultado { get; }

		public virtual string TituloResultado { get; } = "Nome";

		public IEnumerable<IResultadoPersonalizado> Resultado
		{
			get
			{
				if (_resultado != null)
					foreach (var item in _resultado)
						yield return item;
				else
				{
					if (ResultadoPersonalizado == null)
						yield break;
					foreach (var item in ResultadoPersonalizado)
						yield return item;
				}
			}
			set
			{
				_resultado = value;
			}
		}

		/// <summary>
		/// Ao mudar esta propriedade, as colunas de código e descrição devem ser adicionadas
		/// manualmente, inclusive refletindo a estrutura do objeto ItemPesquisa ou 
		/// personalizado na pasta [Visualizacoes] conforme a mesma convenção.
		/// </summary>
		public virtual IList<ResultadoPersonalizado<TResultado>> ResultadoPersonalizado { get; } = null;

		public virtual IList<Dependente> Dependentes { get; }

		public virtual string Condicao { get; }

		public virtual string Relacionamento { get; }

		public virtual string Ordem { get; }

		public virtual bool PossuiCampoEmpresa { get; } = true;

		public virtual bool PossuiCampoEstabelecimento { get; }

		public virtual bool FiltrarEstabelecimentoUsuario { get; }

		public virtual bool FiltrarApenasAtivos { get; } = true;

	}
}
