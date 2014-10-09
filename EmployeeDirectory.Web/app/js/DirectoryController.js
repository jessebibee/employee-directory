(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', 'dataService', DirectoryController]);

    function DirectoryController($scope, dataService) {
        $scope.employees = [];

        $scope.search = function () {
            dataService.searchEmployees()
                .then(function (employees) {
                    $scope.employees = employees;
                });
        };

        //TODO - consider moving to header bar
        $scope.add = function () {
            console.log('add');
        };

        $scope.editEmployee = function (employee) {
            console.log('edit', employee);
            //on success update the employee in the $scope.employees array
        };

        $scope.deleteEmployee = function (index) {
            //TODO - implement a confirmation box, could use window.confirm but rather you a modal
            //set is deleting flag to disable all other delete buttons until this one returns?  two delete operations returning out of order will mess up splicing the $scope.employees array

            dataService.deleteEmployee($scope.employees[index].employeeId)
                .then(function () {
                    $scope.employees.splice(index, 1);
                });
        };
    }
})(angular.module('app'));