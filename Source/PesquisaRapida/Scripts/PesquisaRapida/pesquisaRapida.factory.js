(function (modulo) {

	modulo.factory("pesquisaRapida", ["$http", "configuradorPesquisa",
		function ($http, configuradorPesquisa) {

			var vm = {

				consultarConfiguracoes: function () {
					document.PesquisaRapida = document.PesquisaRapida || {};
					if (document.PesquisaRapida.janelaCarregada == null)
						document.PesquisaRapida.janelaCarregada = false;
					return document.PesquisaRapida;
				},

				executarConsulta: function (tipo, termo, pesquisa, pagina) {

					if ((pagina == null) || (pagina < 1))
						pagina = 1;

					var acoes = {

						tipo: tipo,
						termo: termo,
						pagina: pagina,

						encontrado: function (retorno, item) {
							if (pesquisa.seEncontrar == null)
								return;
							pesquisa.seEncontrar(item[retorno.Chave], item[retorno.Resultado], item);
						},

						naoEncontrado: function (mensagem) {
							if (pesquisa.seNaoEncontrar == null)
								return;
							pesquisa.seNaoEncontrar(mensagem);
						}

					};

					vm.consultar(tipo, pagina, termo,

						// noSucesso ...
						function (retorno) {
							if (pesquisa.reconsulta)
								vm.apenasCarregarDadosJanelaDeConsulta(retorno);
							else
								if (retorno.Dados.lenght == 0)
									acoes.naoEncontrado();
								else if (retorno.Dados.length == 1)
									acoes.encontrado(retorno, retorno.Dados[0]);
								else
									vm.carregarJanelaDeConsulta(retorno, acoes);
						},

						// noErro
						function (mensagem) {
							acoes.naoEncontrado(mensagem);
						});

				},

				consultar: function (tipo, pagina, termo, noSucesso, noErro) {
					var url = configuradorPesquisa.consultarPathMvc() + "/" + tipo + "/" + pagina + "/?termo=" + termo;
					$http.post(url, {})
						.success(function (retorno) {
							if (retorno.error)
								noErro(retorno.error);
							else if ((retorno == null) || (!retorno.Dados))
								noErro("Retorno não identificado ao pesquisar por [" + termo + "]!");
							else
								noSucesso(retorno);
						})
						.error(function () {
							noErro("Não foi possível pesquisar [" + termo + "]!");
						});
				},

				apenasCarregarDadosJanelaDeConsulta: function (retorno) {
					var $janela = $("#janelaModalPesquisaRapida");
					var $scope = $janela.scope();
					$scope.carregarRetorno(retorno);
				},

				carregarJanelaDeConsulta: function (retorno, acoes) {
					if (vm.janelaCarregada())
						vm.executarJanelaDeConsulta(retorno, acoes);
					else
						acoes.naoEncontrado("Não foi possível carregar janela de consulta!");
				},

				janelaCarregada: function () {
					return ($("#janelaModalPesquisaRapida").length > 0);
				},

				executarJanelaDeConsulta: function (retorno, acoes) {
					var $janela = $("#janelaModalPesquisaRapida");
					var $scope = $janela.scope();

					vm.retornoAtual = retorno;

					$scope.tipo = acoes.tipo;
					$scope.termo = acoes.termo;
					$scope.pagina = acoes.pagina;
					$scope.carregarRetorno(retorno);

					$janela.modal("show");
				},

				retornoAtual: null,
				pesquisaAtual: null

			};

			return {

				aoFecharJanela: function () {
					if ((vm.pesquisaAtual == null) || (vm.retornoAtual == null))
						return;
					var $janela = $("#janelaModalPesquisaRapida");
					var $scope = $janela.scope();
					var item = $scope.itemSelecionado;
					if ((item == null) || (!$scope.janelaConfirmada))
						vm.pesquisaAtual.seNaoEncontrar("");
					else
						vm.pesquisaAtual.seEncontrar(item[vm.retornoAtual.Chave], item[vm.retornoAtual.Resultado], item);
				},

				configuracao: vm.consultarConfiguracoes(),

				consultar: function (tipo, termo) {

					var pesquisa = {
						reconsulta: false,
						seEncontrar: null,
						seNaoEncontrar: null
					};

					var resultado = {
						seEncontrar: function (acao) {
							pesquisa.seEncontrar = acao;
							return this;
						},
						seNaoEncontrar: function (acao) {
							pesquisa.seNaoEncontrar = acao;
							return this;
						}
					};

					vm.pesquisaAtual = pesquisa;

					vm.executarConsulta(tipo, termo, pesquisa);

					return resultado;
				},

				consultarNovamente: function (tipo, termo, pagina) {
					vm.pesquisaAtual.reconsulta = true;
					vm.executarConsulta(tipo, termo, vm.pesquisaAtual, pagina);
				}

			};

		}]);

})(angular.module("moduloPesquisaRapida"));
