(function (modulo) {

	modulo.factory("configuradorPesquisa", [

		function () {

			var vm = {
				pathScripts: "/Scripts/PesquisaRapida",
				pathMvc: "/pr",

				formatarPath: function (path) {
					if (path.length < 1)
						return "/";
					path = path.trim();
					var tamanho = path.length;
					if (path.substring(tamanho - 1, 1) == "/")
						return path.substring(0, tamanho - 1);
					return path;
				}
			};

			return {

				definirPathScripts: function (path) {
					if (path != null)
						vm.pathScripts = vm.formatarPath(path);
				},

				definirPathMvc: function (path) {
					if (path != null)
						vm.pathMvc = vm.formatarPath(path);
				},

				consultarPathScripts: function () {
					return vm.pathScripts;
				},

				consultarPathMvc: function () {
					return vm.pathMvc;
				}

			};

		}]);

})(angular.module("moduloPesquisaRapida", []));
