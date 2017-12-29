using System;
using System.Collections.Generic;

namespace PesquisaRapida.Pattern.Estrutura
{
	public interface IConfiguracao
	{

		Type TipoObjeto { get; }

		string TituloConfiguracao { get; }

		string Tabela { get; }

		TipoCampoChave TipoChave { get; }

		string TituloResultado { get; }

		IEnumerable<IResultadoPersonalizado> Resultado { get; }

		string Condicao { get; }

		string Relacionamento { get; }

		string Ordem { get; }

	}
}
