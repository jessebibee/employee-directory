(function (app) {
    'use strict';

    app.controller('NewEmployeeController', ['$scope', 'dataService', NewEmployeeController]);

    function NewEmployeeController($scope, dataService) {
        $scope.employee = {};
        $scope.employeePassword = null;
        $scope.isSaving = false;

        $scope.create = function () {
            $scope.isSaving = true;
            dataService.createEmployee($scope.employee)
                .then(function (result) {
                    $scope.employeePassword = result.password;
                    $scope.isSaving = false;
                }, function (error) {
                    //TODO - check for duplicate email or other errors!
                    $scope.isSaving = false;
                });
        };
    }
})(angular.module('app'));