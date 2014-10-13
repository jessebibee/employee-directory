(function (app) {
    'use strict';

    app.controller('EmployeeDetailController', ['$scope', '$location', '$window', 'employee', 'dataService', 'searchService', EmployeeDetailController]);

    function EmployeeDetailController($scope, $location, $window, employee, dataService, searchService) {
        $scope.employee = employee;
        var original = angular.copy(employee);
        $scope.isSaving = false;

        $scope.save = function () {
            $scope.isSaving = true;

            dataService.editEmployee($scope.employee)
                .then(function () {
                    //searchResultsService.editResultIfExists($scope.employee);
                    $location.path('');
                }, function (data, status) {
                    $scope.isSaving = false;
                    $window.alert('An error occured trying Saving Changes');
                });
        };

        $scope.reset = function () {
            $scope.employee = original;
            original = angular.copy($scope.employee);
            $scope.employeeForm.$setPristine();
        };

        $scope.back = function () {
            $window.history.back();
        };
    }
})(angular.module('app'));