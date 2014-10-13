(function (app) {
    'use strict';

    app.controller('NewEmployeeController', ['$scope', 'dataService', NewEmployeeController]);

    function NewEmployeeController($scope, dataService) {
        $scope.employee = {};
        $scope.isSaving = false;

        $scope.create = function () {
            $scope.isSaving = true;
            $scope.duplicateEmailAddress = null;
            $scope.createdEmployee = null;
            var emailClosure = $scope.employee.email;

            dataService.createEmployee($scope.employee)
                .then(function (result) {
                    $scope.createdEmployee = angular.copy(result.employee);
                    $scope.createdEmployee.password = result.password;
                    $scope.isSaving = false;
                    $scope.employee = {};
                }, function (status) {
                    $scope.isSaving = false;
                    if (status === 409) {
                        $scope.duplicateEmailAddress = emailClosure;
                    }
                });
        };
    }
})(angular.module('app'));