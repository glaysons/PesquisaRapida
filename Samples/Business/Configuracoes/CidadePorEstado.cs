using System.Collections.Generic;
using PesquisaRapida.Pattern.Estrutura;
using PesquisaRapida.Pattern;

namespace Business.Configuracoes
{
	public class CidadePorEstado : Configuracao
	{

		public override string TituloConfiguracao { get; } = "Cadastro de Cidades";

		public override string Tabela { get; } = "Cidades";

		public override string CampoChave { get; } = "CodigoCidade";

		public override string CampoResultado { get; } = "Nome + '/' + CodigoEstado";

		public override string Ordem { get; } = "Nome, CodigoEstado";

		public override IList<Dependente> Dependentes { get; }

		public CidadePorEstado()
		{
			Dependentes = new List<Dependente>();
			Dependentes.Add(new Dependente("Estado", "Cidades.CodigoEstado = @dependente0"));
		}

	}
}
