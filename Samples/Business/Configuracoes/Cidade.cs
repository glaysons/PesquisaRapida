using PesquisaRapida.Pattern;

namespace Business.Configuracoes
{
	public class Cidade : Configuracao<CidadeView>
	{
		public override string TituloConfiguracao { get; } = "Cadastro de Cidades";

		public override string Tabela { get; } = "Cidades";

		protected override void PersonalizarResultadoPadrao()
		{
			AdicionarCampo(c => c.Cod, "Cód.", "CodigoCidade", chave: true);
			AdicionarCampo(c => c.Nome, "Nome", "Nome", resultado: true);
			AdicionarCampo(c => c.Estado, "Estado", "CodigoEstado");
		}

	}
}
