using System;

namespace PesquisaRapida.Exceptions
{
	public class AConfiguracaoNaoPermiteMaisQueUmCampoChaveOuResultadoException : Exception
	{
		public AConfiguracaoNaoPermiteMaisQueUmCampoChaveOuResultadoException(string tipoCampo)
			: base(string.Concat("A configuração não permite mais que um campo [", tipoCampo, "] em campo de pesquisa!"))
		{

		}
	}
}
