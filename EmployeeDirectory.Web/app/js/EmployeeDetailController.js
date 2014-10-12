(function (app) {
    'use strict';

    app.controller('EmployeeDetailController', ['$scope', '$location', '$window', 'employee', 'dataService', 'searchService', EmployeeDetailController]);

    function EmployeeDetailController($scope, $location, $window, employee, dataService, searchService) {
        $scope.employee = employee;
        var original = angular.copy(employee);

        $scope.save = function () {
            dataService.editEmployee($scope.employee)
                .then(function () {
                    //searchResultsService.editResultIfExists($scope.employee);
                    $location.path(''); //home page (hacky)
                }, function (data, status) {
                    console.log('error saving employee', data, status); //TODO - probably pop up a modal (cases: 404 and server validation errors)
                });
        };

        $scope.cancel = function () {
            $scope.employee = original;
            original = angular.copy($scope.employee);
        };

        $scope.back = function () {
            $window.history.back();
        };
    }
})(angular.module('app'));