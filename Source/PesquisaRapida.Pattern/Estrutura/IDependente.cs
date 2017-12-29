using System.Collections.Generic;

namespace PesquisaRapida.Pattern.Estrutura
{
	public interface IDependente
	{

		void ConfigurarDependentes(IList<Dependente> dependentes);

	}
}
