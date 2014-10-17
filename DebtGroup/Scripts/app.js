var app = angular.module('app', ['ngRoute', 'ui.bootstrap']);


app.config(function($routeProvider) {

    $routeProvider
        .when('/',
        {
            templateUrl: "/Template/detailsModal.html",
            controller: "transCtrl",
        })

})