using System.Collections.Generic;
using PesquisaRapida.Pattern.Estrutura;

namespace Business.Configuracoes
{
	public class CidadePorEstado : Cidade
	{

		public override IList<Dependente> Dependentes { get; }

		public CidadePorEstado()
		{
			Dependentes = new List<Dependente>();
			Dependentes.Add(new Dependente("Estado", "Cidades.CodigoEstado = @dependente0"));
		}

	}
}
