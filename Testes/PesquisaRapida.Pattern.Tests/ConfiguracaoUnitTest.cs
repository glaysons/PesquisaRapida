using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Configuracoes;
using System.Linq;
using FluentAssertions;

namespace PesquisaRapida.Pattern.Tests
{
	[TestClass]
	public class ConfiguracaoUnitTest
	{

		[TestMethod]
		public void SeCriarObjetoDeConfiguracaoDoEstadoDeveGerarCamposCorretamente()
		{
			var estado = new Estado();
			var campos = estado.Resultado.ToList();

			campos
				.Should()
				.HaveCount(2);

			campos.Count(c => c.Chave)
				.Should()
				.Be(1, "porque deve ser definido um campo chave!");

			campos.Count(c => c.Resultado)
				.Should()
				.Be(1, "porque deve ser definido um campo resultado!");

			estado.TipoObjeto
				.Should()
				.NotBeNull();

			estado.TipoObjeto.Name
				.Should()
				.Be("CodigoResultado");
		}

		[TestMethod]
		public void SeCriarObjetoDeConfiguracaoDaCidadeDeveGerarCamposCorretamente()
		{
			var cidade = new Cidade();
			var campos = cidade.Resultado.ToList();

			campos
				.Should()
				.HaveCount(3);

			campos.Count(c => c.Chave)
				.Should()
				.Be(1, "porque deve ser definido um campo chave!");

			campos.Count(c => c.Resultado)
				.Should()
				.Be(1, "porque deve ser definido um campo resultado!");

			campos.Count(c => !(c.Chave || c.Resultado))
				.Should()
				.Be(1, "porque deve ser definido um campo adicional!");

			cidade.TipoObjeto
				.Should()
				.NotBeNull();

			cidade.TipoObjeto.Name
				.Should()
				.Be("CidadeView");
		}

	}
}
