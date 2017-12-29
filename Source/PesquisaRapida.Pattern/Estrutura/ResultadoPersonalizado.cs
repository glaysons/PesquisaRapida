namespace PesquisaRapida.Pattern.Estrutura
{
	internal class ResultadoPersonalizado<T> : IResultadoPersonalizado
	{

		public string Campo { get; }

		public string Titulo { get; }

		public string ExpressaoSql { get; }

		public bool ColunaVisivel { get; }

		public bool Chave { get; }

		public bool Resultado { get; }

		public ResultadoPersonalizado(string campo, string titulo, string expressaoSql, bool colunaVisivel, bool chave = false, bool resultado = false)
		{
			Campo = campo;
			Titulo = titulo;
			ExpressaoSql = expressaoSql;
			ColunaVisivel = colunaVisivel;
			Chave = chave;
			Resultado = resultado;
		}

	}
}
