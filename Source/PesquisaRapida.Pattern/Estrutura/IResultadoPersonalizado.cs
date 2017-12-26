namespace PesquisaRapida.Pattern.Estrutura
{
	public interface IResultadoPersonalizado
	{

		string Campo { get; }

		string Titulo { get; }

		string ExpressaoSql { get; }

		bool ColunaVisivel { get; }

		bool Chave { get; }

		bool Resultado { get; }

	}
}
