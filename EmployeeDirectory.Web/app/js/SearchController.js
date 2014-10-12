(function (app) {
    'use strict';

    app.controller('SearchController', ['$scope', '$location', 'searchService', SearchController]);

    function SearchController($scope, $location, searchService) {
        $scope.location = 0;
        $scope.query = '';

        $scope.search = function () {
            searchService.query(1, 25, ($scope.location > 0 ? $scope.location : null), $scope.query);
        };
    }
})(angular.module('app'));