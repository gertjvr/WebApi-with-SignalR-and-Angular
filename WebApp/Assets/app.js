(function () {
    "use strict";

    angular
        .module("app", [
            "SignalR"
        ])
        .controller("Shell", Shell);

    Shell.$inject = ["Hub", "$http", "$log", "$timeout"];

    function Shell(Hub, $http, $log, $timeout) {
        var vm = this;
        vm.message = null;
        vm.products = [];

        var hub = new Hub("AppHub", {
            useSharedConnection: false,
            logging: false,
            methods: [],
            listeners: {
                tick: tick
            },
            errorHandler: function (e) {
                $log.error("SignalR connection failed", e);
            },
            rootPath: "/signalr/",
            hubDisconnected: function () {
                $log.warn("SignalR hub disconnected.");
                reconnect(hub);
            }
        });

        activate();

        function activate() {
            $http.get("/api/products").then(function (response) {
                vm.products = response.data;
            });
        }

        function tick(time) {
            $timeout(function() {
                vm.message = time;
            });
        }

        function reconnect(hub) {
            $timeout(function () {
                hub.connection.start()
                    .done(function () {
                        $log.info("SignalR hub reconnected");
                    })
                    .fail(function (reason) {
                        $log.error("SignalR hub failed to reconnect", reason);
                        reconnect(hub);
                    });
            }, 5000);
        }
    }

})();