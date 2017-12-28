using System;

namespace PesquisaRapida.Exceptions
{
	public class AConfiguracaoExigeQueSejaDefinidoUmCampoChaveOuResultadoException : Exception
	{
		public AConfiguracaoExigeQueSejaDefinidoUmCampoChaveOuResultadoException(string tipoCampo)
			: base(string.Concat("Favor informar um campo [", tipoCampo, "] válido para pesquisa!"))
		{

		}
	}
}
