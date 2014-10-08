(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', DirectoryController]);

    function DirectoryController($scope) {
        $scope.testMessage = 'hello world';
    }
})(angular.module('app'));