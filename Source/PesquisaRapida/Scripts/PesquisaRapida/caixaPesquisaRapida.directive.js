(function (modulo) {

	modulo.directive("caixaPesquisaRapida", ["$rootScope", "$compile", "$timeout", "pesquisaRapida",
		function ($rootScope, $compile, $timeout, pesquisaRapida) {

			var vm = {

				carregarJanela: function (scope) {
					if (pesquisaRapida.configuracao.janelaCarregada)
						return;
					pesquisaRapida.configuracao.janelaCarregada = true;
					var janela = $compile("<janela-Pesquisa-Rapida></janela-Pesquisa-Rapida>")($rootScope);
					janela.on("hidden.bs.modal", pesquisaRapida.aoFecharJanela);
					$("body").append(janela);
				},

				valorFoiModificado: function (scope, element) {
					return (element.val() != scope.valorAnterior);
				},

				salvarValorConsulta: function (scope, element) {
					scope.valorAnterior = element.val();
				},

				definirValor: function (scope, element, valor) {
					element.val(valor)
					if (scope.ngModel)
						scope.ngModel = valor;
					scope.valorAnterior = valor;
				},

				definirResultado: function (scope, resultado) {
					var elemento = angular.element(document.querySelector("#" + scope.controleResultado));
					if (elemento != null)
						elemento.text(resultado);
					$timeout(function () {
						scope.$apply(function () {
							scope.ngModelResultado = resultado;
						});
					});
				},

				definirRegistro: function (scope, registro) {
					$timeout(function () {
						scope.$apply(function () {
							scope.ngModelRegistro = registro;
						});
					});
				}

			};

			return {

				restrict: "A",

				scope: {
					ngModel: "=",
					caixaPesquisaRapida: "@",
					controleResultado: "@",
					ngModelResultado: "=?",
					ngModelRegistro: "=?"
				},

				link: function (scope, element, attr) {

					vm.carregarJanela(scope);

					scope.resultadoEncontrado = true;
					scope.valorAnterior = element.val();

					scope.limparPesquisa = function (elemento, resultado) {
						if (scope.resultadoEncontrado)
							return;
						vm.definirValor(scope, element, null);
						vm.definirResultado(scope, resultado);
						vm.definirRegistro(scope, null);
					};

					element.on("blur", function () {
						if (!vm.valorFoiModificado(scope, element))
							return;

						scope.resultadoEncontrado = false;

						var valorAtual = element.val();
						if ((valorAtual == null) || (valorAtual.trim() == "")) {
							scope.limparPesquisa(element, "");
							return;
						}

						vm.salvarValorConsulta(scope, element);

						pesquisaRapida.consultar(scope.caixaPesquisaRapida, element.val())
							.seEncontrar(function (valor, resultado, registro) {
								scope.resultadoEncontrado = true;
								vm.definirValor(scope, element, valor);
								vm.definirResultado(scope, resultado);
								vm.definirRegistro(scope, registro);
							})
							.seNaoEncontrar(function (mensagem) {
								scope.limparPesquisa(element, (mensagem == null)
									? "Código não Encontrado ou Inválido!!!"
									: mensagem);
							});
					});
				}

			};

		}]);

})(angular.module("moduloPesquisaRapida"));
