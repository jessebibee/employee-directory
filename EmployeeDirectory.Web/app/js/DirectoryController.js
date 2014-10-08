(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', DirectoryController]);

    function DirectoryController($scope) {
        $scope.testMessage = 'hello world';
        $scope.employees = [];
    }
})(angular.module('app'));