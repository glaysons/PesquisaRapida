namespace PesquisaRapida.Pattern.Estrutura
{
	public class Dependente
	{

		public string Componente { get; }

		public string CondicaoSql { get; }

		public Dependente(string componente, string condicaoSql)
		{
			Componente = componente;
			CondicaoSql = condicaoSql;
		}

	}
}
