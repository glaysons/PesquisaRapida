(function (modulo) {

	modulo.directive("caixaTextoPesquisaRapida", ["configuradorPesquisa",
		function (configuradorPesquisa) {

			return {

				restrict: "E",

				scope: {
					tipo: "@",
					name: "@",
					ngModel: "=",
					ngModelResultado: "=?",
					ngModelRegistro: "=?",
					onClick: "&"
				},

				templateUrl: function (element, attr) {
					return configuradorPesquisa.consultarPathScripts() + "/caixaTextoPesquisaRapida.html";
				},

				link: function (scope, element, attr) {
					scope.ngModelResultado = "";
					scope.ngModelRegistro = null;
					scope.clicouBotaoAcesso = function () {
						event.preventDefault();
						if (scope.onClick)
							scope.onClick();
					}
				}

			};

		}]);

})(angular.module("moduloPesquisaRapida"));
