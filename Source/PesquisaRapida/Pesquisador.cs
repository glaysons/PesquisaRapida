using PesquisaRapida.Exceptions;
using PesquisaRapida.Pattern.Estrutura;
using RepositorioGenerico.Pattern.Contextos.Tables;
using System;
using System.Collections.Generic;

namespace PesquisaRapida
{
	public class Pesquisador<TTipo> where TTipo: struct, IConvertible, IComparable, IFormattable
	{

		private TTipo _tipo;
		private IContexto _contexto;
		private IConfiguracao _configuracao;

		public Pesquisador(TTipo tipo, IContexto contexto, IConfiguracao configuracao)
		{
			ValidarTipoEnum();
			_tipo = tipo;
			_contexto = contexto;
			_configuracao = configuracao;
		}

		private static void ValidarTipoEnum()
		{
			if (!typeof(TTipo).IsEnum)
				throw new TipoDeveSerObrigatoriamenteEnumException();
		}

		public ResultadoPesquisa Consultar(int pagina, string termo, string[] dependentes)
		{
			var resultado = new ResultadoPesquisa();
			resultado.Pesquisa = _configuracao.TituloConfiguracao;
			resultado.Titulos = ConsultarTitulosDaConsulta();
			resultado.Colunas = ConsultarColunasDaConsulta();
			resultado.Dados = ConsultarResultadoDaPesquisa(termo, pagina, dependentes);
			resultado.Chave = ConsultarCampoChave();
			resultado.Resultado = ConsultarCampoResultado();
			return resultado;
		}

		private string[] ConsultarTitulosDaConsulta()
		{
			return CarregarListaDoResultado(r => r.Titulo);
		}

		private string[] CarregarListaDoResultado(Func<IResultadoPersonalizado, string> consultarResultado)
		{
			var itens = new List<string>();
			foreach (var item in _configuracao.Resultado)
				if (!string.IsNullOrEmpty(item.Titulo))
					itens.Add(consultarResultado(item));
			return itens.ToArray();
		}

		private string[] ConsultarColunasDaConsulta()
		{
			return CarregarListaDoResultado(r => r.Campo);
		}

		private IList<object> ConsultarResultadoDaPesquisa(int pagina, string termo, string[] dependentes)
		{
			return new Consultador(_contexto, _configuracao)
				.Executar(pagina, termo, dependentes);
		}

		private string ConsultarCampoChave()
		{
			return ConsultarCampo(c => c.Chave, "Chave");
		}

		private string ConsultarCampo(Func<IResultadoPersonalizado, bool> condicao, string tipoCampo)
		{
			var campo = string.Empty;

			foreach (var resultado in _configuracao.Resultado)
				if (condicao(resultado))
					if (string.IsNullOrEmpty(campo))
						campo = resultado.Campo;
					else
						throw new AConfiguracaoNaoPermiteMaisQueUmCampoChaveOuResultadoException(tipoCampo);

			if (string.IsNullOrEmpty(campo))
				throw new AConfiguracaoExigeQueSejaDefinidoUmCampoChaveOuResultadoException(tipoCampo);

			return campo;
		}

		private string ConsultarCampoResultado()
		{
			return ConsultarCampo(c => c.Resultado, "Resultado");
		}
	}
}
