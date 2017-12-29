using System.Collections.Generic;
using PesquisaRapida.Pattern.Estrutura;
using PesquisaRapida.Pattern;
using System;

namespace Business.Configuracoes
{
	public class CidadePorEstado : Configuracao, IDependente
	{

		public override string TituloConfiguracao { get; } = "Cadastro de Cidades";

		public override string Tabela { get; } = "Cidades";

		public override string CampoChave { get; } = "CodigoCidade";

		public override string CampoResultado { get; } = "Nome + '/' + CodigoEstado";

		public override string Ordem { get; } = "Nome, CodigoEstado";

		public void ConfigurarDependentes(IList<Dependente> dependentes)
		{
			dependentes.Add(new Dependente("Estado", "Cidades.CodigoEstado = @dependente0"));
		}
	}
}
