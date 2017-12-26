using System.Collections.Generic;

namespace PesquisaRapida.Pattern.Estrutura
{
	public interface IConfiguracao
	{

		string TituloConfiguracao { get; }

		int Rotina { get; }

		string Tabela { get; }

		string CampoChave { get; }

		TipoCampos TipoChave { get; }

		string CampoResultado { get; }

		string TituloResultado { get; }

		IEnumerable<IResultadoPersonalizado> Resultado { get; set; }

		IList<Dependente> Dependentes { get; }

		string Condicao { get; }

		string Relacionamento { get; }

		string Ordem { get; }

		bool PossuiCampoEmpresa { get; }

		bool PossuiCampoEstabelecimento { get; }

		bool FiltrarEstabelecimentoUsuario { get; }

		bool FiltrarApenasAtivos { get; }

	}
}
