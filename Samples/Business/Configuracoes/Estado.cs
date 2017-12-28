using PesquisaRapida.Pattern;
using PesquisaRapida.Pattern.Estrutura;

namespace Business.Configuracoes
{
	public class Estado : Configuracao
	{

		public override string TituloConfiguracao { get; } = "Cadastro de Estados";

		public override string Tabela { get; } = "Estados";

		public override string CampoChave { get; } = "CodigoEstado";

		public override TipoCampos TipoChave { get; } = TipoCampos.String;

		public override string CampoResultado { get; } = "Nome";

		public override string Ordem { get; } = "CodigoEstado";

	}
}
