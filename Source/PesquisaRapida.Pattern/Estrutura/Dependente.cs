namespace PesquisaRapida.Pattern.Estrutura
{
	public class Dependente
	{

		public string Titulo { get; }

		public string CondicaoSql { get; }

		public Dependente(string titulo, string condicaoSql)
		{
			Titulo = titulo;
			CondicaoSql = condicaoSql;
		}

	}
}
