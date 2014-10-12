(function (app) {
    'use strict';

    app.controller('SearchController', ['$scope', '$state', '$location', 'dataService', 'searchService', SearchController]);

    function SearchController($scope, $state, $location, dataService, searchService) {
        //$scope.search = function () {
        //    dataService.searchEmployees()
        //        .then(function (employees) {
        //            searchService.setResults(employees);
        //            $location.path(''); //home page (hacky)
        //        });

        //    //TODO - loading graphic
        //};
    }
})(angular.module('app'));