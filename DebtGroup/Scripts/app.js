var app = angular.module('app', ['ngRoute']);


app.config(function($routeProvider) {

    $routeProvider
        .when('/',
        {
            templateUrl: "/Template/detailsModal.html",
            controller: "transCtrl",
        })

})