(function (app) {
    'use strict';

    app.controller('SearchController', ['$scope', '$rootScope', '$state', '$location', 'dataService', 'searchResultsService', SearchController]);

    function SearchController($scope, $rootScope, $state, $location, dataService, searchResultsService) {
        $scope.search = function () {
            dataService.searchEmployees()
                .then(function (employees) {
                    searchResultsService.setResults(employees);
                    $location.path(''); //home page (hacky)
                });

            //TODO - loading graphic
        };

        //TODO - consider moving to header bar
        $scope.add = function () {
            $state.go('new-employee');
        };
    }
})(angular.module('app'));