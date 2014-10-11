(function (app) {
    'use strict';

    app.controller('SearchController', ['$scope', '$state', '$location', 'dataService', 'searchResultsService', SearchController]);

    function SearchController($scope, $state, $location, dataService, searchResultsService) {
        $scope.search = function () {
            dataService.searchEmployees()
                .then(function (employees) {
                    searchResultsService.setResults(employees);
                    $location.path(''); //home page (hacky)
                });

            //TODO - loading graphic
        };
    }
})(angular.module('app'));