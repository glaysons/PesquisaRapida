using System.Collections.Generic;

namespace PesquisaRapida
{
	public class ResultadoPesquisa
	{

		public string Pesquisa { get; set; }

		public string[] Titulos { get; set; }

		public string[] Colunas { get; set; }

		public string Chave { get; set; }

		public string Resultado { get; set; }

		public IList<object> Dados { get; set; }

	}
}
