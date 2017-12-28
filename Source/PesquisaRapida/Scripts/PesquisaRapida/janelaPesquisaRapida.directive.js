(function (modulo) {

	modulo.directive("janelaPesquisaRapida", ["pesquisaRapida", "caixaTextoPesquisaRapida",
		function (pesquisaRapida, caixaTextoPesquisaRapida) {

			var vm = {

				fecharJanela: function () {
					var $janela = $("#janelaModalPesquisaRapida");
					$janela.modal("hide");
				}

			};

			return {

				restrict: "E",

				templateUrl: function (element, attr) {
					return caixaTextoPesquisaRapida.consultarPathScripts() + "/janelaPesquisaRapida.html";
				},

				scope: {
				},

				link: function (scope, element, attr) {

					scope.tituloJanela = "";
					scope.tipo = "";
					scope.termo = "";
					scope.titulos = [];
					scope.colunas = [];
					scope.itensEncontrados = [];
					scope.itemSelecionado = null;
					scope.janelaConfirmada = false;
					scope.pagina = 1;

					scope.carregarRetorno = function (retorno) {
						scope.tituloJanela = retorno.Pesquisa;
						scope.titulos = retorno.Titulos;
						scope.colunas = retorno.Colunas;
						scope.itensEncontrados.length = 0;
						for (var indice = 0; indice < retorno.Dados.length; indice++)
							scope.itensEncontrados.push(retorno.Dados[indice]);
						scope.indice = -1;
						scope.itemSelecionado = null;
						scope.janelaConfirmada = false;
					};

					scope.pesquisarNovamente = function () {
						scope.pagina = 1;
						pesquisaRapida.consultarNovamente(scope.tipo, scope.termo);
					};

					scope.selecionarItem = function (indice, item) {
						scope.indice = indice;
						scope.itemSelecionado = item;
					};

					scope.confirmarJanela = function (indice, item) {
						if (item != null)
							scope.selecionarItem(indice, item);
						if (scope.itemSelecionado == null)
							alert("Favor selecionar um registro ...")
						else {
							scope.janelaConfirmada = true;
							vm.fecharJanela();
						}
					};

					scope.cancelarJanela = function () {
						scope.itemSelecionado = null;
						vm.fecharJanela();
					};

					scope.existePaginaAnterior = function () {
						return (scope.pagina > 1);
					};

					scope.voltarPagina = function () {
						scope.pagina--;
						pesquisaRapida.consultarNovamente(scope.tipo, scope.termo, scope.pagina);
					};

					scope.existePaginaPosterior = function () {
						return (scope.itensEncontrados.length == 10);
					};

					scope.avancarPagina = function () {
						scope.pagina++;
						pesquisaRapida.consultarNovamente(scope.tipo, scope.termo, scope.pagina);
					};

				}

			};

		}]);

})(angular.module("moduloPesquisaRapida"));
