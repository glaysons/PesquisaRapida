using System;

namespace PesquisaRapida.Pattern.Exceptions
{
	public class CampoInvalidoException : Exception
	{
		public CampoInvalidoException()
			: base("Favor informar um campo válido para configuração da pesquisa!")
		{

		}
	}
}
