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
                    $location.path('');
                }, function (data, status) {
                    $scope.isSaving = false;
                    $window.alert('An error occured saving changes');
                });
        };

        $scope.reset = function () {
            $scope.employee = original;
            original = angular.copy($scope.employee);
            $scope.employeeForm.$setPristine();
        };

        //link needs conditional display logic to check whether or not the user came from the directory grid/search 
        //$scope.back = function () {
        //    $window.history.back();
        //};
    }
})(angular.module('app'));