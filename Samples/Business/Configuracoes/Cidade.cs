using System.Collections.Generic;
using PesquisaRapida.Pattern;

namespace Business.Configuracoes
{
	public class Cidade : Configuracao<CidadeView>
	{
		public override string TituloConfiguracao { get; } = "Cadastro de Cidades";

		public override string Tabela { get; } = "Cidades";

		public override IList<ResultadoPersonalizado<CidadeView>> ResultadoPersonalizado { get; }

		public Cidade()
		{
			ResultadoPersonalizado = new List<ResultadoPersonalizado<CidadeView>>();
			ResultadoPersonalizado.Add(new ResultadoPersonalizado<CidadeView>(c => c.Cod, "CodigoCidade", chave: true));
			ResultadoPersonalizado.Add(new ResultadoPersonalizado<CidadeView>(c => c.Nome, "Nome", resultado: true));
			ResultadoPersonalizado.Add(new ResultadoPersonalizado<CidadeView>(c => c.Estado, "CodigoEstado"));
		}
	}
}
