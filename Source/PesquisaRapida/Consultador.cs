using RepositorioGenerico.Pattern.Contextos.Tables;
using System;
using System.Collections.Generic;
using RepositorioGenerico.Pattern.Buscadores;
using System.Text;
using System.Linq;
using PesquisaRapida.Pattern.Estrutura;
using System.Reflection;
using System.Data;

namespace PesquisaRapida
{
	internal class Consultador
	{

		private IContexto _contexto;
		private Pattern.Estrutura.IConfiguracao _configuracao;

		public Consultador(IContexto contexto, Pattern.Estrutura.IConfiguracao configuracao)
		{
			_contexto = contexto;
			_configuracao = configuracao;
		}

		public IList<object> Executar(int pagina, string termo, string[] dependentes)
		{
			if (pagina < 1)
				pagina = 1;

			if (string.IsNullOrEmpty(termo))
				termo = string.Empty;

			var config = _contexto.Buscar().CriarQuery(_configuracao.Tabela);

			config.DefinirLimite(pagina * 10);

			foreach (var resultado in _configuracao.Resultado)
				config.AdicionarResultado(string.Concat("(", resultado.ExpressaoSql, ")as[", resultado.Campo, "]"));

			if (!string.IsNullOrEmpty(_configuracao.Relacionamento))
				config.AdicionarRelacionamento(_configuracao.Relacionamento);

			AdicionarFiltroDosDependentesInformados(_configuracao as IDependente, config, dependentes);

			if (!string.IsNullOrEmpty(_configuracao.Condicao))
				config.AdicionarCondicaoPersonalizada(_configuracao.Condicao);

			AdicionarFiltroDoTermoPesquisado(config, termo);

			if (string.IsNullOrEmpty(_configuracao.Ordem))
				config.AdicionarOrdem("2, 1");
			else
				config.AdicionarOrdem(_configuracao.Ordem);

			PersonalizarConsulta(_configuracao as IPesquisaPersonalizada, config);

			return ConverterConsultaEmObjeto(config, pagina);
		}

		private void AdicionarFiltroDosDependentesInformados(IDependente configuracaoDependentes, IConfiguracaoQuery config, string[] dependentes)
		{
			if (configuracaoDependentes == null)
				return;
			var camposDependentes = new List<Dependente>();
			configuracaoDependentes.ConfigurarDependentes(camposDependentes);
			for (int indice = 0; indice < camposDependentes.Count; indice++)
			{
				var nomeParametro = string.Concat("@dependente", indice + 1);
				config.AdicionarCondicaoPersonalizada(camposDependentes[indice].CondicaoSql);
				if (indice < dependentes.Length)
					config.DefinirParametro(nomeParametro).Valor(dependentes[indice]);
				else
					config.DefinirParametro(nomeParametro).Nulo();
			}
		}

		private void AdicionarFiltroDoTermoPesquisado(IConfiguracaoQuery config, string termo)
		{
			var pesquisaChave = config.DefinirParametro("pesquisaChave");
			pesquisaChave.Valor(true);

			var sql = new StringBuilder();
			sql.Append("(");

			sql.Append("((@pesquisaChave=1) and ");

			sql.Append(CriarFiltroPorChaveIgualAoTermo(termo));

			sql.Append(")");

			sql.Append("or");

			sql.Append("((@pesquisaChave=0) and ");

			sql.Append(CriarFiltroPorResultadoContendo(termo));

			sql.Append(")");

			sql.Append(")");

			config.AdicionarCondicaoPersonalizada(sql.ToString());

			if (!_contexto.Buscar().Existe(config))
				pesquisaChave.Valor(false);
		}

		private string CriarFiltroPorChaveIgualAoTermo(string termo)
		{
			var sql = new StringBuilder();

			sql.Append("(");
			sql.Append(_configuracao.Resultado.FirstOrDefault(c => c.Chave).ExpressaoSql);

			if (_configuracao.TipoChave == TipoCampos.Integer)
			{
				int numero;
				if (int.TryParse(termo.Replace(" ", string.Empty), out numero))
				{
					sql.Append(" = ");
					sql.Append(termo);
				}
				else
					sql.Append(" is null");
			}
			else if (_configuracao.TipoChave == TipoCampos.Double)
			{
				double numero;
				if (double.TryParse(termo.Replace(" ", string.Empty), out numero))
				{
					sql.Append(" = ");
					sql.Append(numero.ToString().Replace(",", "."));
				}
				else
					sql.Append(" is null");
			}
			else
			{
				sql.Append(" = '");
				sql.Append(termo.Replace("'", "''"));
				sql.Append("'");
			}

			sql.Append(")");

			return sql.ToString();
		}

		private string CriarFiltroPorResultadoContendo(string termo)
		{
			var sql = new StringBuilder();

			sql.Append("(");

			sql.Append(_configuracao.Resultado.FirstOrDefault(c => c.Resultado).ExpressaoSql);
			sql.Append(" LIKE '");

			if (termo.StartsWith("*"))
			{
				sql.Append("%");
				sql.Append(termo.Substring(1).Replace("'", "''"));
			}
			else
				sql.Append(termo.Replace("'", "''"));

			sql.Append("%'");

			sql.Append(")");

			return sql.ToString();
		}

		private void PersonalizarConsulta(IPesquisaPersonalizada personalizacao, IConfiguracaoQuery config)
		{
			if (personalizacao != null)
				personalizacao.PersonalizarPesquisa(config);
		}


		private IList<object> ConverterConsultaEmObjeto(IConfiguracaoQuery config, int pagina)
		{
			var resultado = new List<object>();
			var tabela = _contexto.Buscar().Varios(config);
			var propriedades = _configuracao.TipoObjeto.GetProperties();
			for (int indice = (pagina - 1) * 10; indice < pagina * 10; indice++)
			{
				if (indice >= tabela.Rows.Count)
					break;
				var registro = tabela.Rows[indice];
				resultado.Add(ConverterRegistroEmObjeto(propriedades, tabela, registro));
			}
			return resultado;
		}

		private object ConverterRegistroEmObjeto(PropertyInfo[] propriedades, DataTable tabela, DataRow registro)
		{
			var objeto = Activator.CreateInstance(_configuracao.TipoObjeto);
			foreach (var propriedade in propriedades.AsParallel())
			{
				var nome = propriedade.Name;
				if ((tabela.Columns.Contains(nome)) && (registro[nome] != DBNull.Value))
					propriedade.SetValue(objeto, registro[nome], null);
			}
			return objeto;
		}

	}
}
