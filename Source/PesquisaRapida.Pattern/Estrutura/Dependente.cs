namespace PesquisaRapida.Pattern.Estrutura
{
	public class Dependente
	{

		public string VariavelScope { get; }

		public string CondicaoSql { get; }

		public Dependente(string scope, string condicaoSql)
		{
			VariavelScope = scope;
			CondicaoSql = condicaoSql;
		}

	}
}
