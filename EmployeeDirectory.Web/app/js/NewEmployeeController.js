(function (app) {
    'use strict';

    app.controller('NewEmployeeController', ['$scope', 'dataService', NewEmployeeController]);

    function NewEmployeeController($scope, dataService) {
        $scope.employee = {};
        $scope.employeePassword = null;

        $scope.create = function () {
            dataService.createEmployee($scope.employee)
                .then(function (result) {
                    $scope.employeePassword = result.password;
                });
        };
    }
})(angular.module('app'));