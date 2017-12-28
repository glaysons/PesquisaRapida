using PesquisaRapida.Pattern.Estrutura;
using RepositorioGenerico.Pattern.Contextos.Tables;
using System;
using System.Collections.Generic;

namespace PesquisaRapida
{
	internal class Consultador
	{

		private IContexto _contexto;
		private IConfiguracao _configuracao;

		public Consultador(IContexto contexto, IConfiguracao configuracao)
		{
			_contexto = contexto;
			_configuracao = configuracao;
		}

		public IList<object> Executar(int pagina, string termo, string[] dependentes)
		{
			throw new NotImplementedException();
		}

	}
}
