using System;

namespace PesquisaRapida.Exceptions
{
	public class TipoDeveSerObrigatoriamenteEnumException : Exception
	{
		public TipoDeveSerObrigatoriamenteEnumException()
			: base("O tipo deve ser obrigatoriamente enum para utilizar como padronizador de pesquisa!")
		{

		}
	}
}
